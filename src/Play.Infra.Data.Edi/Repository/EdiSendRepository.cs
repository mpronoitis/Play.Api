namespace Play.Infra.Data.Edi.Repository;

/// <summary>
///     Repository for uploading EDI files via FTP
/// </summary>
public class EdiSendRepository : IEdiSendRepository
{
    private readonly IEdiConnectionRepository _ediConnectionRepository;
    private readonly IEdiDocumentRepository _ediDocumentRepository;
    private readonly ILogger<EdiSendRepository> _logger;

    public EdiSendRepository(IEdiDocumentRepository ediDocumentRepository,
        IEdiConnectionRepository ediConnectionRepository, ILogger<EdiSendRepository> logger)

    {
        _ediDocumentRepository = ediDocumentRepository;
        _ediConnectionRepository = ediConnectionRepository;
        _logger = logger;
    }

    /// <summary>
    ///     Function to send all unsent EDI files for a given customer_Id
    /// </summary>
    /// <param name="customerId">The customer primary key (GUID)</param>
    /// <returns>True if all files were sent, false if any failed</returns>
    public async Task<bool> SendUnsentEdiFiles(Guid customerId)
    {
        try
        {
            //var docs = await _ediDocumentRepository.GetByIsSentAndCustomerIdAsync(false, customerId);
            var docs = await _ediDocumentRepository.GetByIsSentAndCustomerIdAsync(false, customerId);
            //convert to list
            var docList = docs.ToList();
            //if empty, return true
            if (!docList.Any())
                return true;

            //keep only the documents where isProcessed is true
            docs = docList.Where(x => x.IsProcessed).ToList();
            //get connection for the given customer
            var connections = await _ediConnectionRepository.GetByCustomerIdAsync(customerId);
            //convert to list and get first
            var connection = connections.ToList()[0];

            //using the ftp credentials from the connection we will call UploadFile for each document
            foreach (var doc in docs)
            {
                var res = await UploadFile(doc.Title + "." + connection.File_Type, doc.EdiPayload, connection.Ftp_Hostname,
                    connection.Ftp_Username,
                    connection.Ftp_Password, "\\", connection.Ftp_Port);
                if (res)
                    //set the document to sent
                    doc.IsSent = true;
            }

            //update the documents
            _ediDocumentRepository.UpdateMultiple(docList);

            //log the result
            _logger.LogInformation("All edi files for customer {CustomerId} were sent", customerId);

            return await _ediDocumentRepository.UnitOfWork.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error sending edi files for customer {CustomerId} , exception {E}", customerId, e);
            throw;
        }
    }

    /// <summary>
    ///     Function to upload a string to an FTP server as a .edi file
    /// </summary>
    /// <param name="FileName">The name of the file to be uploaded</param>
    /// <param name="FileContent">The content of the file to be uploaded</param>
    /// <param name="Ftp_Host">The FTP server to upload to</param>
    /// <param name="Ftp_User">The FTP user to upload as</param>
    /// <param name="Ftp_Password">The FTP password to upload with</param>
    /// <param name="Ftp_Directory">The FTP directory to upload to</param>
    /// <returns>True if the upload was successful, false otherwise</returns>
    private Task<bool> UploadFile(string FileName, string FileContent, string Ftp_Host, string Ftp_User,
        string Ftp_Password, string Ftp_Directory,int Ftp_Port = 21)
    {
        try
        {
            //we will use FluentFtp
            var client = new FtpClient(Ftp_Host, Ftp_User, Ftp_Password, Ftp_Port);
            client.Connect();
            //convert file content to byte array
            var fileBytes = Encoding.UTF8.GetBytes(FileContent);
            //upload the file
            client.UploadBytes(fileBytes, Ftp_Directory + "/" + FileName);
            //disconnect
            client.Disconnect();
            return Task.FromResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file {FileName} to ftp server {FtpHost}, exception {Ex}", FileName,
                Ftp_Host, ex);
            throw;
        }
    }
}