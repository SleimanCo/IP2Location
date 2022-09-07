// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IP2Location
{

    public sealed class ComponentWebService
    {
        private string _APIKey = "";
        private string _Package = "";
        private bool _UseSSL = true;
        private readonly Regex _RegexAPIKey = new Regex(@"^[\dA-Z]{10}$");
        private readonly Regex _RegexPackage = new Regex(@"^WS\d+$");
        private const string BASE_URL = "api.ip2location.com/v2/";

        // Description: Initialize
        public ComponentWebService()
        {
        }

        // Description: Set the API key and package for the queries
        public void Open(string APIKey, string Package, bool UseSSL = true)
        {
            _APIKey = APIKey;
            _Package = Package;
            _UseSSL = UseSSL;

            CheckParams();
        }

        // Description: Validate API key and package
        private void CheckParams()
        {
            if (!_RegexAPIKey.IsMatch(_APIKey) && _APIKey != "demo")
                throw new Exception("Invalid API key.");

            if (!_RegexPackage.IsMatch(_Package))
                throw new Exception("Invalid package name.");
        }

        // Description: Query web service to get location information by IP address and translations
        public JObject IPQuery(string IP, string Language = "en")
        {
            return IPQuery(IP, null, Language);
        }

        // Description: Query web service to get location information by IP address, addons and translations
        public JObject IPQuery(string IP, string[] AddOns, string Language = "en")
        {
            CheckParams(); // check here in case user haven't called Open yet

            string url;
            string protocol = _UseSSL ? "https" : "http";
            url = protocol + "://" + BASE_URL + "?key=" + _APIKey + "&package=" + _Package + "&ip=" + System.Net.WebUtility.UrlEncode(IP) + "&lang=" + System.Net.WebUtility.UrlEncode(Language);
            if (AddOns != null)
                url += "&addon=" + System.Net.WebUtility.UrlEncode(string.Join(",", AddOns));
            string rawjson = GetMethod(url);
            object results = JsonConvert.DeserializeObject<object>(rawjson);

            return (JObject)results;
        }

        // Description: Check web service credit balance
        public JObject GetCredit()
        {
            CheckParams(); // check here in case user haven't called Open yet

            string url;
            string protocol = _UseSSL ? "https" : "http";
            url = protocol + "://" + BASE_URL + "?key=" + _APIKey + "&check=true";
            string rawjson = GetMethod(url);
            object results = JsonConvert.DeserializeObject<object>(rawjson);

            return (JObject)results;
        }
        public string GetMethod(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string raw = reader.ReadToEnd();
                return raw;
            }
            else
            {
                return "Failed : HTTP error code :" + ((int)response.StatusCode).ToString();
            }
        }
        public string PostMethod(string url, string post)
        {
            UTF8Encoding encode = new UTF8Encoding();
            byte[] postdatabytes = encode.GetBytes(post);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postdatabytes.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(postdatabytes, 0, postdatabytes.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string raw = reader.ReadToEnd();
                return raw;
            }
            else
            {
                return "Failed : HTTP error code :" + ((int)response.StatusCode).ToString();
            }
        }
    }
}