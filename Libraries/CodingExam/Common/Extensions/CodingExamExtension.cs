using System;
using System.ComponentModel;
using System.Linq;

using CodingExam.Helpers;

namespace CodingExam.Common
{
    public static class CodingExamExtension
    {
        #region Enum Extensions

        public static string GetValue(this Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static Enum GetEnumValue(this ApiSettingsType apiSettingsType)
        {
            Enum value = null;

            switch (apiSettingsType)
            {
                case ApiSettingsType.GeoLocationSettings:
                    value = ApiSettingsType.GeoLocationSettings;

                    break;
                case ApiSettingsType.NetworkApiSettings:
                    value = ApiSettingsType.NetworkApiSettings;

                    break;
                case ApiSettingsType.RdapSettings:
                    value = ApiSettingsType.RdapSettings;

                    break;
            }

            return value;
        }

        public static Enum GetEnumValue(this ApiEndPointType apiEndPointType)
        {
            Enum value = null;

            switch (apiEndPointType)
            {
                case ApiEndPointType.AbuseContactLookup:
                    value = ApiEndPointType.AbuseContactLookup;

                    break;
                case ApiEndPointType.DnsLookup:
                    value = ApiEndPointType.DnsLookup;

                    break;
                case ApiEndPointType.DnsPropagation:
                    value = ApiEndPointType.DnsPropagation;

                    break;
                case ApiEndPointType.DomainAvailability:
                    value = ApiEndPointType.DomainAvailability;

                    break;
                case ApiEndPointType.GeoIpLocation:
                    value = ApiEndPointType.GeoIpLocation;

                    break;
                case ApiEndPointType.IpHistoryCheck:
                    value = ApiEndPointType.IpHistoryCheck;

                    break;
                case ApiEndPointType.Ping:
                    value = ApiEndPointType.Ping;

                    break;
                case ApiEndPointType.PortScanner:
                    value = ApiEndPointType.PortScanner;

                    break;
                case ApiEndPointType.RdapLookup:
                    value = ApiEndPointType.RdapLookup;

                    break;
                case ApiEndPointType.ReverseDnsLookup:
                    value = ApiEndPointType.ReverseDnsLookup;

                    break;
                case ApiEndPointType.ReverseIp:
                    value = ApiEndPointType.ReverseIp;

                    break;
                case ApiEndPointType.VirusScan:
                    value = ApiEndPointType.VirusScan;

                    break;
            }

            return value;
        }

        public static string GetSectionKey(this Enum value, bool includeSectionName = true, bool includeSectionKey = true)
        {
            AppSettingSectionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(AppSettingSectionAttribute), false)
                .SingleOrDefault() as AppSettingSectionAttribute;

            if (includeSectionName && includeSectionKey)
            {
                return attribute == null ? string.Empty : $"{attribute.SectionName}:{attribute.SectionKey}";
            }
            else if (includeSectionName && !includeSectionKey)
            {
                return attribute == null ? string.Empty : attribute.SectionName;
            }
            else if (!includeSectionName && includeSectionKey)
            {
                return attribute == null ? string.Empty : attribute.SectionKey;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetSectionKeys(this ServiceType serviceType)
        {
            string sectionKey = string.Empty;

            switch (serviceType)
            {
                case ServiceType.AbuseContact:
                    sectionKey = GetSectionKey(ServiceType.AbuseContact);

                    break;
                case ServiceType.DnsLookup:
                    sectionKey = GetSectionKey(ServiceType.DnsLookup);

                    break;
                case ServiceType.DnsPropagation:
                    sectionKey = GetSectionKey(ServiceType.DnsPropagation);

                    break;
                case ServiceType.DomainAvailability:
                    sectionKey = GetSectionKey(ServiceType.DomainAvailability);

                    break;
                case ServiceType.GeoLocation:
                    sectionKey = GetSectionKey(ServiceType.GeoLocation);

                    break;
                case ServiceType.IpHistoryCheck:
                    sectionKey = GetSectionKey(ServiceType.IpHistoryCheck);

                    break;
                case ServiceType.Ping:
                    sectionKey = GetSectionKey(ServiceType.Ping);

                    break;
                case ServiceType.PortScanner:
                    sectionKey = GetSectionKey(ServiceType.PortScanner);

                    break;
                case ServiceType.ReverseDns:
                    sectionKey = GetSectionKey(ServiceType.ReverseDns);

                    break;
                case ServiceType.ReverseIp:
                    sectionKey = GetSectionKey(ServiceType.ReverseIp);

                    break;
                case ServiceType.RdapLookup:
                    sectionKey = GetSectionKey(ServiceType.RdapLookup);

                    break;

                case ServiceType.VirusScan:
                    sectionKey = GetSectionKey(ServiceType.VirusScan);

                    break;
            }

            return sectionKey;
        }

        public static string GetSectionName(this ServiceType serviceType)
        {
            string sectionKey = string.Empty;

            switch (serviceType)
            {
                case ServiceType.AbuseContact:
                    sectionKey = GetSectionKey(ServiceType.AbuseContact, true, false);

                    break;
                case ServiceType.DnsLookup:
                    sectionKey = GetSectionKey(ServiceType.DnsLookup, true, false);

                    break;
                case ServiceType.DnsPropagation:
                    sectionKey = GetSectionKey(ServiceType.DnsPropagation, true, false);

                    break;
                case ServiceType.DomainAvailability:
                    sectionKey = GetSectionKey(ServiceType.DomainAvailability, true, false);

                    break;
                case ServiceType.GeoLocation:
                    sectionKey = GetSectionKey(ServiceType.GeoLocation, true, false);

                    break;
                case ServiceType.IpHistoryCheck:
                    sectionKey = GetSectionKey(ServiceType.IpHistoryCheck, true, false);

                    break;
                case ServiceType.Ping:
                    sectionKey = GetSectionKey(ServiceType.Ping, true, false);

                    break;
                case ServiceType.PortScanner:
                    sectionKey = GetSectionKey(ServiceType.PortScanner, true, false);

                    break;
                case ServiceType.ReverseDns:
                    sectionKey = GetSectionKey(ServiceType.ReverseDns, true, false);

                    break;
                case ServiceType.ReverseIp:
                    sectionKey = GetSectionKey(ServiceType.ReverseIp, true, false);

                    break;
                case ServiceType.RdapLookup:
                    sectionKey = GetSectionKey(ServiceType.RdapLookup, true, false);

                    break;

                case ServiceType.VirusScan:
                    sectionKey = GetSectionKey(ServiceType.VirusScan, true, false);

                    break;
            }

            return sectionKey;
        }

        public static string GetSectionKey(this ServiceType serviceType)
        {
            string sectionKey = string.Empty;

            switch (serviceType)
            {
                case ServiceType.AbuseContact:
                    sectionKey = GetSectionKey(ServiceType.AbuseContact, false, true);

                    break;
                case ServiceType.DnsLookup:
                    sectionKey = GetSectionKey(ServiceType.DnsLookup, false, true);

                    break;
                case ServiceType.DnsPropagation:
                    sectionKey = GetSectionKey(ServiceType.DnsPropagation, false, true);

                    break;
                case ServiceType.DomainAvailability:
                    sectionKey = GetSectionKey(ServiceType.DomainAvailability, false, true);

                    break;
                case ServiceType.GeoLocation:
                    sectionKey = GetSectionKey(ServiceType.GeoLocation, false, true);

                    break;
                case ServiceType.IpHistoryCheck:
                    sectionKey = GetSectionKey(ServiceType.IpHistoryCheck, false, true);

                    break;
                case ServiceType.Ping:
                    sectionKey = GetSectionKey(ServiceType.Ping, false, true);

                    break;
                case ServiceType.PortScanner:
                    sectionKey = GetSectionKey(ServiceType.PortScanner, false, true);

                    break;
                case ServiceType.ReverseDns:
                    sectionKey = GetSectionKey(ServiceType.ReverseDns, false, true);

                    break;
                case ServiceType.ReverseIp:
                    sectionKey = GetSectionKey(ServiceType.ReverseIp, false, true);

                    break;
                case ServiceType.RdapLookup:
                    sectionKey = GetSectionKey(ServiceType.RdapLookup, false, true);

                    break;

                case ServiceType.VirusScan:
                    sectionKey = GetSectionKey(ServiceType.VirusScan, false, true);

                    break;
            }

            return sectionKey;
        }

        public static string GetQueryStringParameters(this ApiEndPointType apiEndPointType, string host, string apiKey, string output)
        {
            string value = string.Empty;

            if (apiEndPointType == ApiEndPointType.PortScanner || apiEndPointType == ApiEndPointType.ReverseIp ||
                apiEndPointType == ApiEndPointType.Ping)
            {
                value = string.Format("?host={0}&apikey={1}&output={2}", host, apiKey, output);
            }
            else if (apiEndPointType == ApiEndPointType.GeoIpLocation)
            {
                value = string.Format("?access_key={0}&fields=main", apiKey);
            }
            else if (apiEndPointType == ApiEndPointType.RdapLookup || apiEndPointType == ApiEndPointType.DomainAvailability)
            {
                value = string.Format("?apiKey={0}&domainName={1}&outputFormat={2}", apiKey, host, output);

                if (apiEndPointType == ApiEndPointType.DomainAvailability)
                {
                    value += $"&mode=DNS_AND_WHOIS";
                }
            }
            else if (apiEndPointType == ApiEndPointType.ReverseDnsLookup)
            {
                value = string.Format("?ip={0}&apikey={1}&output={2}", host, apiKey, output);
            }
            else if (apiEndPointType == ApiEndPointType.DnsLookup || apiEndPointType == ApiEndPointType.AbuseContactLookup || 
                apiEndPointType == ApiEndPointType.DnsPropagation || apiEndPointType == ApiEndPointType.IpHistoryCheck)
            {
                value = string.Format("?domain={0}&apikey={1}&output={2}", host, apiKey, output);

                if (apiEndPointType == ApiEndPointType.DnsLookup)
                {
                    value += "&recordtype=any";
                }
            }
            else
            {
                value = string.Format("?ip={0}&apikey={1}&output={2}", host, apiKey, output);
            }

            return value;
        }

        #endregion
    }
}
