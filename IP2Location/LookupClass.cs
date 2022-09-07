// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

namespace IP2Location
{
    public class IPResult
    {
        public string IPAddress { get; set; } = "?";
        public string IPNumber { get; set; } = "?";
        public string CountryShort { get; set; } = "?";
        public string CountryLong { get; set; } = "?";
        public string Region { get; set; } = "?";
        public string City { get; set; } = "?";
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string ZipCode { get; set; } = "?";
        public string TimeZone { get; set; } = "?";
        public string NetSpeed { get; set; } = "?";
        public string IDDCode { get; set; } = "?";
        public string AreaCode { get; set; } = "?";
        public string WeatherStationCode { get; set; } = "?";
        public string WeatherStationName { get; set; } = "?";
        public string InternetServiceProvider { get; set; } = "?";
        public string DomainName { get; set; } = "?";
        public string MCC { get; set; } = "?";
        public string MNC { get; set; } = "?";
        public string MobileBrand { get; set; } = "?";
        public float Elevation { get; set; }
        public string UsageType { get; set; } = "?";
        public string AddressType { get; set; } = "?";
        public string Category { get; set; } = "?";
        public string Status { get; set; } = "?";
    }
}