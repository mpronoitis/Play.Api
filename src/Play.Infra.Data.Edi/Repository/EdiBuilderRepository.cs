namespace Play.Infra.Data.Edi.Repository;

/// <summary>
///     Repository for building and dispatching EDI documents
/// </summary>
public class EdiBuilderRepository : IEdiBuilderRepository
{
    //needed to fetch documents
    private readonly IEdiDocumentRepository _ediDocumentRepository;

    //needed to fetch user edi profile
    private readonly IEdiProfileRepository _ediProfileRepository;

    //serilog logger
    private readonly ILogger<EdiBuilderRepository> _logger;

    //needed to validate the customer id
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Constructor
    ///     Injects the needed repositories
    /// </summary>
    /// <param name="userRepository">User repository</param>
    /// <param name="ediDocumentRepository">EDI document repository</param>
    /// <param name="ediProfileRepository">EDI profile repository</param>
    /// <param name="logger"></param>
    public EdiBuilderRepository(IUserRepository userRepository, IEdiDocumentRepository ediDocumentRepository,
        IEdiProfileRepository ediProfileRepository, ILogger<EdiBuilderRepository> logger)
    {
        _userRepository = userRepository;
        _ediDocumentRepository = ediDocumentRepository;
        _ediProfileRepository = ediProfileRepository;
        _logger = logger;
    }


    /// <summary>
    ///     Function to build all un built documents for a given customer id
    /// </summary>
    /// <param name="customerId">Customer id</param>
    /// <returns>True if all documents were built, false otherwise</returns>
    public async Task<bool> BuildUnparsed(Guid customerId)
    {
        //first we want to validate that the customer id is valid
        var user = await _userRepository.GetByIdAsync(customerId);
        if (user is null) return false;

        //get all documents that are not processed
        //var documents = await _ediDocumentRepository.GetByIsProcessedAndCustomerAsync(false, customerId);
        var documents = await _ediDocumentRepository.GetByIsProcessedAndCustomerAsync(false, customerId);

        //convert to a list
        var documentList = documents.ToList();
        //if there are no documents to process, return true
        if (documentList.Count == 0) return true;

        //get user edi profiles
        var profiles = await _ediProfileRepository.GetByUserIdAsync(customerId);
        //keep just the first one if it exists otherwise return
        var profilesList = profiles.ToList();
        if (profilesList.Count == 0) return false;
        var profile = profilesList[0];


        //we are going to create two lists that will store the following
        // 1- The ediLines - all the profile lines that are not included in the item loop
        // 2- The itemLines - all the profile lines that are included in the item loop
        var ediLines = new List<string>();
        var itemLines = new List<string>();
        var itemLoopStartIndex = 0;

        //parse profile payload
        if (profile != null)
        {
            var profilePayload = ParseProfilePayload(profile.Payload);

            //build the edi lines and item lines
            for (var i = 0; i < profilePayload.Length; i++)
                if (profilePayload[i].Contains("$ITEMS_LOOP_START$"))
                {
                    itemLoopStartIndex = i;
                    i++;
                    while (!profilePayload[i].Contains("$ITEMS_LOOP_END$"))
                    {
                        itemLines.Add(profilePayload[i]);
                        i++;
                    }
                }
                else
                {
                    ediLines.Add(profilePayload[i]);
                }
        }
        else
        {
            //log error
            _logger.LogError("No profile found for user {Name}{0} when trying to build edi documents", "ARG0",
                customerId);
            return false;
        }

        //loop through all documents
        foreach (var document in documentList)
        {
            //clone the edi lines and item lines
            var ediLinesClone = new List<string>(ediLines);
            var itemLinesClone = new List<string>(itemLines);
            //map document payload json to doc data
            var s = new JsonSerializer();
            var docData = s.Deserialize<DocumentDataModel>(
                new JsonTextReader(new StringReader(document.DocumentPayload)));

            if (docData != null)
            {
                //get total count of items in payload
                var itemsCount = int.TryParse(docData.ITEMS_COUNT, out var count) ? count : 0;
                //set the related Document Title, fallback to document name
                var relatedDoc = docData.DESTINATION_DOCUMENT == "UNDEFINED"
                    ? docData.SOURCE_DOCUMENT == "UNDEFINED" ? document.Title : docData.SOURCE_DOCUMENT
                    : docData.DESTINATION_DOCUMENT;
                //strip the text from the document title
                relatedDoc = GetNumbers(relatedDoc ?? document.Title);
                //get the date of the related document
                var relatedDocDate = docData.SOURCE_DOCUMENT_DATE == "UNDEFINED"
                    ? docData.DESTINATION_DOCUMENT_DATE == "UNDEFINED"
                        ? DateTime.Now.ToString("yyMMdd")
                        : docData.DESTINATION_DOCUMENT_DATE
                    : docData.SOURCE_DOCUMENT_DATE;
                //build non item segments
                for (var i = 0; i < ediLinesClone.Count; i++)
                {
                    ediLinesClone[i] = ediLinesClone[i]
                        .Replace("$DATE_TODAY_YYYYMMDD$", DateTime.Now.ToString("yyyyMMdd"));
                    ediLinesClone[i] = ediLinesClone[i]
                        .Replace("$DATE_TOMMOROW_YYYYMMDD$", DateTime.Now.AddDays(1).ToString("yyyyMMdd"));
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DATE_TODAY_YYMMDD$", DateTime.Now.ToString("yyMMdd"));
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DATE_TOMMOROW_YYMMDD$",
                        DateTime.Now.AddDays(1).ToString("yyMMdd"));
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DATE_DOCUMENT$", docData.DATE);
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DATE_DOCUMENT_RELATED$", relatedDocDate);
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DOCUMENT_CODE$", GetNumbers(document.Title));
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DOCUMENT_RELATED_CODE$", relatedDoc);
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DOCUMENT_TOTAL_PRICE_WITH_TAX$", docData.TOTAL_PRICE);
                    ediLinesClone[i] = ediLinesClone[i]
                        .Replace("$DOCUMENT_TOTAL_PRICE_WITHOUT_TAX$", docData.TOTAL_PRICE_NO_TAX);
                    ediLinesClone[i] = ediLinesClone[i].Replace("$DOCUMENT_TOTAL_TAX$", docData.TOTAL_TAX);
                    ediLinesClone[i] = ediLinesClone[i].Replace("$TIME_TODAY$", DateTime.Now.ToString("HHmm"));
                    ediLinesClone[i] = ediLinesClone[i].Replace("$ITEMS_TOTAL_COUNT$", itemsCount.ToString());
                }

                var newItems = new List<string>();
                var itemCount = 1;
                //build items segments
                if (docData.ITEMS != null)
                    for (var i = 0; i < docData.ITEMS.Count; i++)
                    {
                        //if tax = 0 or code is Test skip item
                        if (docData.ITEMS[i].TAX_PERCENT == "0" || docData.ITEMS[i].CODE == "Τεστ") continue;
                        var temp = new List<string>(itemLinesClone);

                        for (var j = 0; j < temp.Count; j++)
                        {
                            temp[j] = temp[j].Replace("$ITEM_INDEX$", itemCount.ToString());
                            temp[j] = temp[j].Replace("$ITEM_NAME$", docData.ITEMS[i].NAME);
                            temp[j] = temp[j].Replace("$ITEM_PIECE_PRICE$", docData.ITEMS[i].PIECE_PRICE);
                            temp[j] = temp[j].Replace("$ITEM_TAX_PERCENT$", docData.ITEMS[i].TAX_PERCENT);
                            temp[j] = temp[j].Replace("$ITEM_TAX_AMOUNT$", docData.ITEMS[i].TAX_VALUE);
                            temp[j] = temp[j].Replace("$ITEM_MEASURMENT_UNIT$", docData.ITEMS[i].MEASURMENT_UNIT);
                            temp[j] = temp[j].Replace("$ITEM_QUANTITY$", docData.ITEMS[i].QUANTITY);
                            temp[j] = temp[j].Replace("$ITEM_CODE$", docData.ITEMS[i].CODE);
                            temp[j] = temp[j].Replace("$ITEM_TOTAL_PRICE_WITH_TAX$", docData.ITEMS[i].PRICE_WITH_TAX);
                            temp[j] = temp[j].Replace("$ITEM_TOTAL_PRICE_NO_TAX$", docData.ITEMS[i].PRICE_NO_TAX);
                            temp[j] = temp[j].Replace("$ITEM_INDEX$", docData.ITEMS[i].ID.ToString());
                            temp[j] = temp[j].Replace("$ITEM_BARCODE$", docData.ITEMS[i].BARCODE);
                        }

                        itemCount++;
                        newItems = newItems.Concat(temp).ToList();
                    }

                //build final edi file
                var finalEdi = new List<string>();
                //get total line count
                var totalLines = ediLinesClone.Count + newItems.Count;
                //add all edi_lines until the items_loop_start_index
                for (var i = 0; i < itemLoopStartIndex; i++) finalEdi.Add(ediLinesClone[i]);
                //add all new_items
                finalEdi.AddRange(newItems);
                //add the remaining edi lines
                for (var i = itemLoopStartIndex; i < ediLinesClone.Count; i++) finalEdi.Add(ediLinesClone[i]);
                //update the $EDI_TOTAL_LINES_COUNT$ count
                for (var i = 0; i < totalLines; i++)
                    finalEdi[i] = finalEdi[i].Replace("$EDI_TOTAL_LINES_COUNT$", (totalLines - 1).ToString());
                //Edi is ready!
                //update document ediPayload
                document.EdiPayload = string.Join(Environment.NewLine, finalEdi);
                //update isProcessed flag
                document.IsProcessed = true;
                //save document
                _ediDocumentRepository.Update(document);
                //log success
                _logger.LogInformation("EDI document {DocumentId} processed successfully", document.Id);
            }
        }

        return await _ediDocumentRepository.UnitOfWork.Commit();
    }

    /// <summary>
    ///     Utility Function to parse PSPAYLOAD from edi user profile
    /// </summary>
    private static string[] ParseProfilePayload(string payload)
    {
        if (payload == null) return new[] { "payload is null" };
        //split profile payload on every new line
        var profilePayloadLines = payload.Split(new[] { "\r\n" }, StringSplitOptions.None);

        return profilePayloadLines;
    }

    /// <summary>
    ///     Returns only the numbers in a string
    /// </summary>
    /// <param name="s">The input string</param>
    /// <returns></returns>
    private string GetNumbers(string s)
    {
        return new string(s.Where(char.IsDigit).ToArray());
    }
}