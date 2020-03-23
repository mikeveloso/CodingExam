using System.ComponentModel;

namespace CodingExam.Common
{
    public enum ApiSettingsType
    {
        [Description("NetworkApiSettings")]
        NetworkApiSettings,
        [Description("GeoLocationSettings")]
        GeoLocationSettings,
        [Description("RdapSettings")]
        RdapSettings
    }
}
