using NetDevPack.Messaging;

namespace Play.Domain.Epp.Commands;

public class EppNameserverCommand : Command
{
    public string Nameserver { get; protected set; } = null!;
    public string DomainName { get; protected set; } = null!;
    public string[] Nameservers { get; protected set; } = null!;
}

public class RegisterEppNameserverCommand : EppNameserverCommand
{
    public RegisterEppNameserverCommand(string nameserver, string domainName)
    {
        Nameserver = nameserver;
        DomainName = domainName;
    }
}

public class RemoveAllEppNameserversCommand : EppNameserverCommand
{
    public RemoveAllEppNameserversCommand(string domainName)
    {
        DomainName = domainName;
    }
}

public class RegisterListEppNameserversCommand : EppNameserverCommand
{
    public RegisterListEppNameserversCommand(string domainName, string[] nameservers)
    {
        DomainName = domainName;
        Nameservers = nameservers;
    }
}