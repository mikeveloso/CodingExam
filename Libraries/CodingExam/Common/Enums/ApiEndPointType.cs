using System.ComponentModel;

namespace CodingExam.Common
{
    public enum ApiEndPointType   
    {
        [Description("AbuseLookup")]
        AbuseContactLookup, //domain
        [Description("DnsRecord")]        
        DnsLookup, //domain
        [Description("Propagation")]
        DnsPropagation, //domain
        [Description("DomainAvailability")]
        DomainAvailability, //domain
        [Description("GeoIPLocation")]
        GeoIpLocation,
        [Description("IPHistory")]
        IpHistoryCheck, //domain
        [Description("Ping")]
        Ping,
        [Description("PortScan")]
        PortScanner,
        [Description("ReverseDNS")]
        ReverseDnsLookup, //ip
        [Description("ReverseIP")]
        ReverseIp,
        [Description("WhoisService")]
        RdapLookup, //domain       
        [Description("VirusScan")]
        VirusScan //domain    
    }
}
