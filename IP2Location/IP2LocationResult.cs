// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

namespace IP2Location
{
    public class IP2LocationResult
    {
        public string IPAddress { get; set; } = string.Empty; // ip
        public string IPNumber { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty; // country_code
        public string CountryName { get; set; } = string.Empty; // country_name
        public string RegionName { get; set; } = string.Empty; // region_name
        public string CityName { get; set; } = string.Empty; // city_name
        public float Latitude { get; set; } = 0.0f; // latitude
        public float Longitude { get; set; } = 0.0f; // longitude
        public string ZipCode { get; set; } = string.Empty; // zip_code
        public string TimeZone { get; set; } = string.Empty; // time_zone
        public string AutonomousSystemNumber { get; set; } = string.Empty; // asn
        public string AutonomousSystemName { get; set; } = string.Empty; // as
        public string InternetServiceProvider { get; set; } = string.Empty; // isp
        public string DomainName { get; set; } = string.Empty; // domain
        public string NetSpeed { get; set; } = string.Empty; // net_speed
        public string IDDCode { get; set; } = string.Empty; // idd_code
        public string AreaCode { get; set; } = string.Empty; // area_code
        public string WeatherStationCode { get; set; } = string.Empty; // weather_station_code
        public string WeatherStationName { get; set; } = string.Empty; // weather_station_name
        public string MCC { get; set; } = string.Empty; // mcc
        public string MNC { get; set; } = string.Empty; // mnc
        public string MobileBrand { get; set; } = string.Empty; // mobile_brand
        public float Elevation { get; set; } = 0.0f; // elevation
        public string UsageType { get; set; } = string.Empty; // usage_type
        public string AddressType { get; set; } = string.Empty; // address_type
        public string AdsCategory { get; set; } = string.Empty; // ads_category
        public string AdsCategoryName { get; set; } = string.Empty; // ads_category_name
        public string District { get; set; } = string.Empty; // district
        public string ContinentName { get; set; } = string.Empty; // continent.name
        public string ContinentCode { get; set; } = string.Empty; // continent.code
        public string ContinentHemisphere { get; set; } = string.Empty; // continent.hemisphere
        public string ContinentTranslation { get; set; } = string.Empty; // continent.translation
        public string CountryName2 { get; set; } = string.Empty; // country.name
        public string CountryCodeAlpha3 { get; set; } = string.Empty; // country.alpha3_code
        public string CountryCodeNumeric { get; set; } = string.Empty; // country.numeric_code
        public string CountryDemonym { get; set; } = string.Empty; // country.demonym
        public string CountryFlag { get; set; } = string.Empty; // country.flag
        public string CountryCapital { get; set; } = string.Empty; // country.capital
        public string CountryTotalArea { get; set; } = string.Empty; // country.total_area
        public string CountryPopulation { get; set; } = string.Empty; // country.population
        public string CountryCurrency { get; set; } = string.Empty; // country.currency
        public string CountryLanguage { get; set; } = string.Empty; // country.language
        public string CountryTld { get; set; } = string.Empty; // country.tld
        public string CountryTranslation { get; set; } = string.Empty; // country.translation
        public string RegionName2 { get; set; } = string.Empty; // region.name
        public string RegionCode { get; set; } = string.Empty; // region.code
        public string RegionTranslation { get; set; } = string.Empty; // region.translation
        public string CityName2 { get; set; } = string.Empty; // city.name
        public string CityTranslation { get; set; } = string.Empty; // city.translation
        public string TimeZoneInfoOlson { get; set; } = string.Empty; // time_zone_info.olson
        public string TimeZoneInfoCurrentTime { get; set; } = string.Empty; // time_zone_info.current_time
        public string TimeZoneInfoGmtOffset { get; set; } = string.Empty; // time_zone_info.gmt_offset
        public string TimeZoneInfoIsDst { get; set; } = string.Empty; // time_zone_info.is_dst
        public string TimeZoneInfoSunrise { get; set; } = string.Empty; // time_zone_info.sunrise
        public string TimeZoneInfoSunset { get; set; } = string.Empty; // time_zone_info.sunset
        public string GeoTargetingMetro { get; set; } = string.Empty; // geotargeting.metro
        public string IsProxy { get; set; } = string.Empty; // is_proxy
        public string ProxyLastSeen { get; set; } = string.Empty; // proxy.last_seen
        public string ProxyType { get; set; } = string.Empty; // proxy.proxy_type
        public string ProxyThreat { get; set; } = string.Empty; // proxy.threat
        public string ProxyProvider { get; set; } = string.Empty; // proxy.provider
        public string ProxyIsVPN { get; set; } = string.Empty; // proxy.is_vpn
        public string ProxyIsTor { get; set; } = string.Empty; // proxy.is_tor
        public string ProxyIsDataCenter { get; set; } = string.Empty; // proxy.is_data_center
        public string ProxyIsPublic { get; set; } = string.Empty; // proxy.is_public_proxy
        public string ProxyIsWeb { get; set; } = string.Empty; // proxy.is_web_proxy
        public string ProxyIsCrawler { get; set; } = string.Empty; // proxy.is_web_crawler
        public string ProxyIsResidential { get; set; } = string.Empty; // proxy.is_residential_proxy
        public string ProxyIsSpammer { get; set; } = string.Empty; // proxy.is_spammer
        public string ProxyIsScanner { get; set; } = string.Empty; // proxy.is_scanner
        public string ProxyIsBotnet { get; set; } = string.Empty; // proxy.is_botnet
        public string Status { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty; // error
        public string ErrorCode { get; set; } = string.Empty; // error_code
        public string ErrorMessage { get; set; } = string.Empty; // error_message
    }
}