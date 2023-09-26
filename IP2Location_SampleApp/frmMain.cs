using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using IP2Location;
using Newtonsoft.Json.Linq;

namespace IP2Location_SampleApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void rbIPv4_CheckedChanged(object sender, EventArgs e)
        {
            txtIP.Text = "188.70.11.199";
        }

        private void rbIPv6_CheckedChanged(object sender, EventArgs e)
        {
            txtIP.Text = "2a00:1851:8018:53a4:d076:f1ff:fe70:7805";
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = ofd.FileName;
            }
        }

        private void btnGetIPInfoToTextBox_Click(object sender, EventArgs e)
        {
            if (rbWebService.Checked)
                GetIPInfo_WebService();
            else
                GetIPInfo_BinFile();
        }

        private async void GetIPInfo_WebService()
        {
            string strURL = "https://api.ip2location.io/?key={{Your_API_Key}}&ip={{IP_Address}}&format=json";
            string strApiKey = txtApiKey.Text.Trim();
            string strIPAddress = txtIP.Text.Trim();

            if (strApiKey.Length == 0)
            {
                txtResult.Text = "API key cannot be blank. You may get your own API key from www.ip2location.io";
                return;
            }
            else if (strIPAddress.Length == 0)
            {
                txtResult.Text = "IP Address cannot be blank.";
                return;
            }
            else
                strURL = strURL.Replace("{{Your_API_Key}}", strApiKey).Replace("{{IP_Address}}", strIPAddress);

            Task<string> strResult = GetIPInfo_WebServiceAsync(strURL);
            txtResult.Text = await strResult;
        }
        private async Task<string> GetIPInfo_WebServiceAsync(string strURL)
        {
            string strResult;

            try
            {
                HttpClient httpClient = new HttpClient();
                strResult = await httpClient.GetStringAsync(strURL);
                IP2LocationResult ip2LocationResult = new IP2LocationResult();
                FillObjectFields(ref ip2LocationResult, strResult);
                strResult = GetIPInfoText(ip2LocationResult);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }

            return strResult;
        }
        private void FillObjectFields(ref IP2LocationResult obj, string strResult)
        {
            try
            {
                if (strResult.Length > 0)
                {
                    JObject jObject = JObject.Parse(strResult);

                    if (jObject.ContainsKey("ip"))
                        obj.IPAddress = GetJObjectValue_String(jObject["ip"]);
                    
                    if (jObject.ContainsKey("country_code"))
                        obj.CountryCode = GetJObjectValue_String(jObject["country_code"]);
                    
                    if (jObject.ContainsKey("country_name"))
                        obj.CountryName = GetJObjectValue_String(jObject["country_name"]);
                    
                    if (jObject.ContainsKey("region_name"))
                        obj.RegionName = GetJObjectValue_String(jObject["region_name"]);
                    
                    if (jObject.ContainsKey("city_name"))
                        obj.CityName = GetJObjectValue_String(jObject["city_name"]);
                    
                    if (jObject.ContainsKey("latitude"))
                        obj.Latitude = GetJObjectValue_Decimal(jObject["latitude"]);
                    
                    if (jObject.ContainsKey("longitude"))
                        obj.Longitude = GetJObjectValue_Decimal(jObject["longitude"]);
                    
                    if (jObject.ContainsKey("zip_code"))
                        obj.ZipCode = GetJObjectValue_String(jObject["zip_code"]);
                    
                    if (jObject.ContainsKey("time_zone"))
                        obj.TimeZone = GetJObjectValue_String(jObject["time_zone"]);
                    
                    if (jObject.ContainsKey("asn"))
                        obj.AutonomousSystemNumber = GetJObjectValue_String(jObject["asn"]);
                    
                    if (jObject.ContainsKey("as"))
                        obj.AutonomousSystemNumber = GetJObjectValue_String(jObject["as"]);
                    
                    if (jObject.ContainsKey("isp"))
                        obj.InternetServiceProvider = GetJObjectValue_String(jObject["isp"]);
                    
                    if (jObject.ContainsKey("domain"))
                        obj.DomainName = GetJObjectValue_String(jObject["domain"]);
                    
                    if (jObject.ContainsKey("NetSpeed"))
                        obj.NetSpeed = GetJObjectValue_String(jObject["NetSpeed"]);
                    
                    if (jObject.ContainsKey("idd_code"))
                        obj.IDDCode = GetJObjectValue_String(jObject["idd_code"]);
                    
                    if (jObject.ContainsKey("area_code"))
                        obj.AreaCode = GetJObjectValue_String(jObject["area_code"]);
                    
                    if (jObject.ContainsKey("weather_station_code"))
                        obj.WeatherStationCode = GetJObjectValue_String(jObject["weather_station_code"]);
                    
                    if (jObject.ContainsKey("weather_station_name"))
                        obj.WeatherStationName = GetJObjectValue_String(jObject["weather_station_name"]);
                    
                    if (jObject.ContainsKey("mcc"))
                        obj.MCC = GetJObjectValue_String(jObject["mcc"]);
                    
                    if (jObject.ContainsKey("mnc"))
                        obj.MNC = GetJObjectValue_String(jObject["mnc"]);
                    
                    if (jObject.ContainsKey("mobile_brand"))
                        obj.MobileBrand = GetJObjectValue_String(jObject["mobile_brand"]);
                    
                    if (jObject.ContainsKey("elevation"))
                        obj.Elevation = GetJObjectValue_Decimal(jObject["elevation"]);
                    
                    if (jObject.ContainsKey("usage_type"))
                        obj.UsageType = GetJObjectValue_String(jObject["usage_type"]);
                    
                    if (jObject.ContainsKey("address_type"))
                        obj.AddressType = GetJObjectValue_String(jObject["address_type"]);
                    
                    if (jObject.ContainsKey("ads_category"))
                        obj.AdsCategory = GetJObjectValue_String(jObject["ads_category"]);
                    
                    if (jObject.ContainsKey("ads_category_name"))
                        obj.AdsCategoryName = GetJObjectValue_String(jObject["ads_category_name"]);
                    
                    if (jObject.ContainsKey("district"))
                        obj.District = GetJObjectValue_String(jObject["district"]);
                    
                    if (jObject.ContainsKey("continent.name"))
                        obj.ContinentName = GetJObjectValue_String(jObject["continent.name"]);
                    
                    if (jObject.ContainsKey("continent.code"))
                        obj.ContinentCode = GetJObjectValue_String(jObject["continent.code"]);
                    
                    if (jObject.ContainsKey("continent.hemisphere"))
                        obj.ContinentHemisphere = GetJObjectValue_String(jObject["continent.hemisphere"]);
                    
                    if (jObject.ContainsKey("continent.translation"))
                        obj.ContinentTranslation = GetJObjectValue_String(jObject["continent.translation"]);
                    
                    if (jObject.ContainsKey("country.name"))
                        obj.CountryName2 = GetJObjectValue_String(jObject["country.name"]);
                    
                    if (jObject.ContainsKey("country.alpha3_code"))
                        obj.CountryCodeAlpha3 = GetJObjectValue_String(jObject["country.alpha3_code"]);
                    
                    if (jObject.ContainsKey("country.numeric_code"))
                        obj.CountryCodeNumeric = GetJObjectValue_String(jObject["country.numeric_code"]);
                    
                    if (jObject.ContainsKey("country.demonym"))
                        obj.CountryDemonym = GetJObjectValue_String(jObject["country.demonym"]);
                    
                    if (jObject.ContainsKey("country.flag"))
                        obj.CountryFlag = GetJObjectValue_String(jObject["country.flag"]);
                    
                    if (jObject.ContainsKey("country.capital"))
                        obj.CountryCapital = GetJObjectValue_String(jObject["country.capital"]);
                    
                    if (jObject.ContainsKey("country.total_area"))
                        obj.CountryTotalArea = GetJObjectValue_String(jObject["country.total_area"]);
                    
                    if (jObject.ContainsKey("country.population"))
                        obj.CountryPopulation = GetJObjectValue_String(jObject["country.population"]);
                    
                    if (jObject.ContainsKey("country.currency"))
                        obj.CountryCurrency = GetJObjectValue_String(jObject["country.currency"]);
                    
                    if (jObject.ContainsKey("country.language"))
                        obj.CountryLanguage = GetJObjectValue_String(jObject["country.language"]);
                    
                    if (jObject.ContainsKey("country.tld"))
                        obj.CountryTld = GetJObjectValue_String(jObject["country.tld"]);
                    
                    if (jObject.ContainsKey("country.translation"))
                        obj.CountryTranslation = GetJObjectValue_String(jObject["country.translation"]);
                    
                    if (jObject.ContainsKey("region.name"))
                        obj.RegionName2 = GetJObjectValue_String(jObject["region.name"]);
                    
                    if (jObject.ContainsKey("region.code"))
                        obj.RegionCode = GetJObjectValue_String(jObject["region.code"]);
                    
                    if (jObject.ContainsKey("region.translation"))
                        obj.RegionTranslation = GetJObjectValue_String(jObject["region.translation"]);
                    
                    if (jObject.ContainsKey("city.name"))
                        obj.CityName2 = GetJObjectValue_String(jObject["city.name"]);
                    
                    if (jObject.ContainsKey("city.translation"))
                        obj.CityTranslation = GetJObjectValue_String(jObject["city.translation"]);
                    
                    if (jObject.ContainsKey("time_zone_info.olson"))
                        obj.TimeZoneInfoOlson = GetJObjectValue_String(jObject["time_zone_info.olson"]);
                    
                    if (jObject.ContainsKey("time_zone_info.current_time"))
                        obj.TimeZoneInfoCurrentTime = GetJObjectValue_String(jObject["time_zone_info.current_time"]);
                    
                    if (jObject.ContainsKey("time_zone_info.gmt_offset"))
                        obj.TimeZoneInfoGmtOffset = GetJObjectValue_String(jObject["time_zone_info.gmt_offset"]);
                    
                    if (jObject.ContainsKey("time_zone_info.is_dst"))
                        obj.TimeZoneInfoIsDst = GetJObjectValue_String(jObject["time_zone_info.is_dst"]);
                    
                    if (jObject.ContainsKey("time_zone_info.sunrise"))
                        obj.TimeZoneInfoSunrise = GetJObjectValue_String(jObject["time_zone_info.sunrise"]);
                    
                    if (jObject.ContainsKey("time_zone_info.sunset"))
                        obj.TimeZoneInfoSunset = GetJObjectValue_String(jObject["time_zone_info.sunset"]);
                    
                    if (jObject.ContainsKey("geotargeting.metro"))
                        obj.GeoTargetingMetro = GetJObjectValue_String(jObject["geotargeting.metro"]);
                    
                    if (jObject.ContainsKey("is_proxy"))
                        obj.IsProxy = GetJObjectValue_String(jObject["is_proxy"]);
                    
                    if (jObject.ContainsKey("proxy.last_seen"))
                        obj.ProxyLastSeen = GetJObjectValue_String(jObject["proxy.last_seen"]);
                    
                    if (jObject.ContainsKey("proxy.proxy_type"))
                        obj.ProxyType = GetJObjectValue_String(jObject["proxy.proxy_type"]);
                    
                    if (jObject.ContainsKey("proxy.threat"))
                        obj.ProxyThreat = GetJObjectValue_String(jObject["proxy.threat"]);
                    
                    if (jObject.ContainsKey("proxy.provider"))
                        obj.ProxyProvider = GetJObjectValue_String(jObject["proxy.provider"]);
                    
                    if (jObject.ContainsKey("proxy.is_vpn"))
                        obj.ProxyIsVPN = GetJObjectValue_String(jObject["proxy.is_vpn"]);
                    
                    if (jObject.ContainsKey("proxy.is_tor"))
                        obj.ProxyIsTor = GetJObjectValue_String(jObject["proxy.is_tor"]);
                    
                    if (jObject.ContainsKey("proxy.is_data_center"))
                        obj.ProxyIsDataCenter = GetJObjectValue_String(jObject["proxy.is_data_center"]);
                    
                    if (jObject.ContainsKey("proxy.is_public_proxy"))
                        obj.ProxyIsPublic = GetJObjectValue_String(jObject["proxy.is_public_proxy"]);
                    
                    if (jObject.ContainsKey("proxy.is_web_proxy"))
                        obj.ProxyIsWeb = GetJObjectValue_String(jObject["proxy.is_web_proxy"]);
                    
                    if (jObject.ContainsKey("proxy.is_web_crawler"))
                        obj.ProxyIsCrawler = GetJObjectValue_String(jObject["proxy.is_web_crawler"]);
                    
                    if (jObject.ContainsKey("proxy.is_residential_proxy"))
                        obj.ProxyIsResidential = GetJObjectValue_String(jObject["proxy.is_residential_proxy"]);
                    
                    if (jObject.ContainsKey("proxy.is_spammer"))
                        obj.ProxyIsSpammer = GetJObjectValue_String(jObject["proxy.is_spammer"]);
                    
                    if (jObject.ContainsKey("proxy.is_scanner"))
                        obj.ProxyIsScanner = GetJObjectValue_String(jObject["proxy.is_scanner"]);
                    
                    if (jObject.ContainsKey("proxy.is_botnet"))
                        obj.ProxyIsBotnet = GetJObjectValue_String(jObject["proxy.is_botnet"]);

                    if (jObject.ContainsKey("error"))
                        obj.Error = GetJObjectValue_String(jObject["error"]);

                    if (jObject.ContainsKey("error_code"))
                        obj.ErrorCode = GetJObjectValue_String(jObject["error_code"]);

                    if (jObject.ContainsKey("error_message"))
                        obj.ErrorMessage = GetJObjectValue_String(jObject["error_message"]);
                }
            }
            catch (Exception) { }
        }
        private string GetJObjectValue_String(JToken obj)
        {
            return (obj == null || obj is null) ? "" : obj.ToString();
        }
        private float GetJObjectValue_Decimal(JToken obj)
        {
            float.TryParse(obj.ToString(), out float temp);
            return temp;
        }

        private void GetIPInfo_BinFile()
        {
            IP2LocationUtility ip2LocationUtility = new IP2LocationUtility();
            IP2LocationResult ip2LocationResult;

            try
            {
                bool boolIsLoaded = false;

                string strPath = txtPath.Text.Trim();
                if (strPath.Length > 0)
                    boolIsLoaded = ip2LocationUtility.Open(strPath, true);

                if (!boolIsLoaded)
                {
                    txtResult.Text = "Bin file cannot be loaded.";
                    return;
                }

                string strIPAddress = txtIP.Text.Trim();
                if (strIPAddress.Length > 0)
                {
                    ip2LocationResult = ip2LocationUtility.IPQuery(strIPAddress);
                    txtResult.Text = GetIPInfoText(ip2LocationResult);
                }
                else
                    txtResult.Text = "IP Address cannot be blank.";

            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
            }
            finally
            {
                ip2LocationUtility.Close();
            }
        }
        private string GetIPInfoText(IP2LocationResult ip2LocationResult)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                switch (ip2LocationResult.Status)
                {
                    case "EMPTY_IP_ADDRESS":
                        sb.AppendLine("IP Address cannot be blank.");
                        break;
                    case "INVALID_IP_ADDRESS":
                        sb.AppendLine("Invalid IP Address.");
                        break;
                    case "MISSING_FILE":
                        sb.AppendLine("Invalid Database Path.");
                        break;
                    default: // includes "OK"
                        sb.AppendLine("IP Address: " + ip2LocationResult.IPAddress);
                        sb.AppendLine("IP Number: " + ip2LocationResult.IPNumber);
                        sb.AppendLine("Country Code: " + ip2LocationResult.CountryCode);
                        sb.AppendLine("Country Name: " + ip2LocationResult.CountryName);
                        sb.AppendLine("Region Name: " + ip2LocationResult.RegionName);
                        sb.AppendLine("City Name: " + ip2LocationResult.CityName);
                        sb.AppendLine("Latitude: " + ip2LocationResult.Latitude);
                        sb.AppendLine("Longitude: " + ip2LocationResult.Longitude);
                        sb.AppendLine("Postal Code: " + ip2LocationResult.ZipCode);
                        sb.AppendLine("Time Zone: " + ip2LocationResult.TimeZone);
                        sb.AppendLine("Autonomous System Number (ASN): " + ip2LocationResult.AutonomousSystemNumber);
                        sb.AppendLine("Autonomous System Name (AS): " + ip2LocationResult.AutonomousSystemName);
                        sb.AppendLine("Internet Service Provider (ISP): " + ip2LocationResult.InternetServiceProvider);
                        sb.AppendLine("Domain Name: " + ip2LocationResult.DomainName);
                        sb.AppendLine("NetSpeed: " + ip2LocationResult.NetSpeed);
                        sb.AppendLine("IDD Code: " + ip2LocationResult.IDDCode);
                        sb.AppendLine("Area Code: " + ip2LocationResult.AreaCode);
                        sb.AppendLine("Weather Station Code: " + ip2LocationResult.WeatherStationCode);
                        sb.AppendLine("Weather Station Name: " + ip2LocationResult.WeatherStationName);
                        sb.AppendLine("MCC: " + ip2LocationResult.MCC);
                        sb.AppendLine("MNC: " + ip2LocationResult.MNC);
                        sb.AppendLine("Mobile Brand: " + ip2LocationResult.MobileBrand);
                        sb.AppendLine("Elevation: " + ip2LocationResult.Elevation);
                        sb.AppendLine("Usage Type: " + ip2LocationResult.UsageType);
                        sb.AppendLine("Address Type: " + ip2LocationResult.AddressType);
                        sb.AppendLine("Ads Category: " + ip2LocationResult.AdsCategory);
                        sb.AppendLine("Ads Category Name: " + ip2LocationResult.AdsCategoryName);
                        sb.AppendLine("District: " + ip2LocationResult.District);
                        sb.AppendLine("Continent Name: " + ip2LocationResult.ContinentName);
                        sb.AppendLine("Continent Code: " + ip2LocationResult.ContinentCode);
                        sb.AppendLine("Continent Hemisphere: " + ip2LocationResult.ContinentHemisphere);
                        sb.AppendLine("Continent Translation: " + ip2LocationResult.ContinentTranslation);
                        sb.AppendLine("Country Name 2: " + ip2LocationResult.CountryName2);
                        sb.AppendLine("Country Code Alpha3: " + ip2LocationResult.CountryCodeAlpha3);
                        sb.AppendLine("Country Code Numeric: " + ip2LocationResult.CountryCodeNumeric);
                        sb.AppendLine("Country Demonym: " + ip2LocationResult.CountryDemonym);
                        sb.AppendLine("Country Flag: " + ip2LocationResult.CountryFlag);
                        sb.AppendLine("Country Capital: " + ip2LocationResult.CountryCapital);
                        sb.AppendLine("Country Total Area: " + ip2LocationResult.CountryTotalArea);
                        sb.AppendLine("Country Population: " + ip2LocationResult.CountryPopulation);
                        sb.AppendLine("Country Currency: " + ip2LocationResult.CountryCurrency);
                        sb.AppendLine("Country Language: " + ip2LocationResult.CountryLanguage);
                        sb.AppendLine("Country Tld: " + ip2LocationResult.CountryTld);
                        sb.AppendLine("Country Translation: " + ip2LocationResult.CountryTranslation);
                        sb.AppendLine("Region Name 2: " + ip2LocationResult.RegionName2);
                        sb.AppendLine("Region Code: " + ip2LocationResult.RegionCode);
                        sb.AppendLine("Region Translation: " + ip2LocationResult.RegionTranslation);
                        sb.AppendLine("City Name 2: " + ip2LocationResult.CityName2);
                        sb.AppendLine("City Translation: " + ip2LocationResult.CityTranslation);
                        sb.AppendLine("Time Zone Info Olson: " + ip2LocationResult.TimeZoneInfoOlson);
                        sb.AppendLine("Time Zone Info Current Time: " + ip2LocationResult.TimeZoneInfoCurrentTime);
                        sb.AppendLine("Time Zone Info Gmt Offset: " + ip2LocationResult.TimeZoneInfoGmtOffset);
                        sb.AppendLine("Time Zone Info Is Dst: " + ip2LocationResult.TimeZoneInfoIsDst);
                        sb.AppendLine("Time Zone Info Sunrise: " + ip2LocationResult.TimeZoneInfoSunrise);
                        sb.AppendLine("Time Zone Info Sunset: " + ip2LocationResult.TimeZoneInfoSunset);
                        sb.AppendLine("Geo Targeting Metro: " + ip2LocationResult.GeoTargetingMetro);
                        sb.AppendLine("Is Proxy: " + ip2LocationResult.IsProxy);
                        sb.AppendLine("Proxy Last Seen: " + ip2LocationResult.ProxyLastSeen);
                        sb.AppendLine("Proxy Type: " + ip2LocationResult.ProxyType);
                        sb.AppendLine("Proxy Threat: " + ip2LocationResult.ProxyThreat);
                        sb.AppendLine("Proxy Provider: " + ip2LocationResult.ProxyProvider);
                        sb.AppendLine("Proxy Is VPN: " + ip2LocationResult.ProxyIsVPN);
                        sb.AppendLine("Proxy Is Tor: " + ip2LocationResult.ProxyIsTor);
                        sb.AppendLine("Proxy Is Data Center: " + ip2LocationResult.ProxyIsDataCenter);
                        sb.AppendLine("Proxy Is Public: " + ip2LocationResult.ProxyIsPublic);
                        sb.AppendLine("Proxy Is Web: " + ip2LocationResult.ProxyIsWeb);
                        sb.AppendLine("Proxy Is Crawler: " + ip2LocationResult.ProxyIsCrawler);
                        sb.AppendLine("Proxy Is Residential: " + ip2LocationResult.ProxyIsResidential);
                        sb.AppendLine("Proxy Is Spammer: " + ip2LocationResult.ProxyIsSpammer);
                        sb.AppendLine("Proxy Is Scanner: " + ip2LocationResult.ProxyIsScanner);
                        sb.AppendLine("Proxy Is Botnet: " + ip2LocationResult.ProxyIsBotnet);
                        sb.AppendLine("Error: " + ip2LocationResult.Error);
                        sb.AppendLine("Error Code: " + ip2LocationResult.ErrorCode);
                        sb.AppendLine("Error Message: " + ip2LocationResult.ErrorMessage);
                        sb.AppendLine("Status: " + ip2LocationResult.Status);
                        break;
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }

            return sb.ToString();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            string strExecPath = Path.GetDirectoryName(Application.ExecutablePath);
            txtPath.Text = strExecPath + @"\Data\IP2LOCATION-LITE-DB1.IPV6.BIN";
        }

        private void rbWebService_CheckedChanged(object sender, EventArgs e)
        {
            pnlWebService.Enabled = rbWebService.Checked;
        }

        private void rbBinFile_CheckedChanged(object sender, EventArgs e)
        {
            pnlBinFile.Enabled = rbBinFile.Checked;
        }

        private void lnk1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lnk1.Text);
        }
    }
}
