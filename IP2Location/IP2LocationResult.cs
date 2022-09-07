// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

namespace IP2Location
{
    public class IP2LocationResult
    {
        public string IPAddress { get; set; } = string.Empty;
        public string IPNumber { get; set; } = string.Empty;
        public string CountryShort { get; set; } = string.Empty;
        public string CountryLong { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public float Latitude { get; set; } = 0.0f;
        public float Longitude { get; set; } = 0.0f;
        public string ZipCode { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string InternetServiceProvider { get; set; } = string.Empty;
        public string DomainName { get; set; } = string.Empty;
        public string NetSpeed { get; set; } = string.Empty;
        public string IDDCode { get; set; } = string.Empty;
        public string AreaCode { get; set; } = string.Empty;
        public string WeatherStationCode { get; set; } = string.Empty;
        public string WeatherStationName { get; set; } = string.Empty;
        public string MCC { get; set; } = string.Empty;
        public string MNC { get; set; } = string.Empty;
        public string MobileBrand { get; set; } = string.Empty;
        public float Elevation { get; set; } = 0.0f;
        public string UsageType { get; set; } = string.Empty;
        public string AddressType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}