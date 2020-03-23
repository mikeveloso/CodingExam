using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodingExam.Common
{
    public enum ServiceType
    {
        [AppSettingSection("ServiceEndpoints", "AbuseContact")]
        AbuseContact,
        [AppSettingSection("ServiceEndpoints", "DnsLookup")]
        DnsLookup,
        [AppSettingSection("ServiceEndpoints", "DnsPropagation")]
        DnsPropagation,
        [AppSettingSection("ServiceEndpoints", "DomainAvailability")]
        DomainAvailability,
        [AppSettingSection("ServiceEndpoints", "GeoLocation")]
        GeoLocation,
        [AppSettingSection("ServiceEndpoints", "IpHistoryCheck")]
        IpHistoryCheck,        
        [AppSettingSection("ServiceEndpoints", "Ping")]
        Ping,
        [AppSettingSection("ServiceEndpoints", "PortScan")]
        PortScanner,
        [AppSettingSection("ServiceEndpoints", "ReverseDns")]
        ReverseDns,
        [AppSettingSection("ServiceEndpoints", "ReverseIp")]
        ReverseIp,
        [AppSettingSection("ServiceEndpoints", "RdapLookup")]
        RdapLookup,
        [AppSettingSection("ServiceEndpoints", "VirusScan")]
        VirusScan
    }
}
