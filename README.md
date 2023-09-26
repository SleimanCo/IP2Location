# IP2Location
A utility to get the geo location info about an IP Address. 

IP2Location™ is a non-intrusive IP location lookup technology that retrieves geolocation information with no explicit permission required from users. All you need is your client’s IP address in IPv4 or IPv6 format.

## What Types of Geo Info to be Returned
The retured info will include some or all of these types: ISO3166 Country Code, Country Name, Region/State, City, Latitude and Longitude, ZIP/Postal Code, Time Zone, Connection Speed, ISP, Domain Name, IDD Country Code, Area Code, Weather Station Data, Mobile Network Codes (MNC), Mobile Country Codes (MCC), Mobile Carrier, Elevation, Usage Type, Address Type, and Advertising Category.


## How to use IP2Location
### 1. Download a BIN file
To use this utility, you may download a BIN data file from: 
* Free IP2Location IP Geolocation BIN Data: https://lite.ip2location.com
* Commercial IP2Location IP Geolocation BIN Data: https://www.ip2location.com/database/ip2location

Or you may use the included LITE BIN Data file in the "Data" folder in Sample App project.

As an alternative, this geolocation component can also call the IP2Location Web Service. This requires an API key. If you don't have an existing API key, you can subscribe for one at the below:
https://www.ip2location.io/pricing

### 2. Integrate this Class Library
It can be easily integrated into any .NET project by referencing the DLL file, or by adding the class library project into your solution and referencing it, or by simply copying the class files to your project.

### 3. Example C# Code
```cs
IP2LocationUtility ip2LocationUtility = new IP2LocationUtility();
IP2LocationResult ip2LocationResult;

try
{
    string strPath = @"C:\Temp\IP2Location\IP2LOCATION-LITE-DB11.BIN";
    string strIPAddress = "8.8.8.8";

    if (strPath == "")
    {
        Console.WriteLine("Bin path cannot be blank.");
        return;
    }

    if (strIPAddress == "")
    {
        Console.WriteLine("IP Address cannot be blank.");
        return;
    }

    bool loaded = ip2LocationUtility.Open(strPath, true);
    if (!loaded)
    {
        Console.WriteLine("Bin file cannot be loaded.");
        return;
    }

    ip2LocationResult = ip2LocationUtility.IPQuery(strIPAddress);
    switch (ip2LocationResult.Status)
    {
        case "OK":
            Console.WriteLine("IP Address: " + ip2LocationResult.IPAddress);
            Console.WriteLine("IP Number: " + ip2LocationResult.IPNumber);
            Console.WriteLine("Country Code: " + ip2LocationResult.CountryShort);
            Console.WriteLine("Country Name: " + ip2LocationResult.CountryLong);
            Console.WriteLine("Region: " + ip2LocationResult.Region);
            Console.WriteLine("City: " + ip2LocationResult.City);
            Console.WriteLine("Latitude: " + ip2LocationResult.Latitude);
            Console.WriteLine("Longitude: " + ip2LocationResult.Longitude);
            Console.WriteLine("Postal Code: " + ip2LocationResult.ZipCode);
            Console.WriteLine("TimeZone: " + ip2LocationResult.TimeZone);
            Console.WriteLine("ISP Name: " + ip2LocationResult.InternetServiceProvider);
            Console.WriteLine("Domain Name: " + ip2LocationResult.DomainName);
            Console.WriteLine("NetSpeed: " + ip2LocationResult.NetSpeed);
            Console.WriteLine("IDD Code: " + ip2LocationResult.IDDCode);
            Console.WriteLine("Area Code: " + ip2LocationResult.AreaCode);
            Console.WriteLine("Weather Station Code: " + ip2LocationResult.WeatherStationCode);
            Console.WriteLine("Weather Station Name: " + ip2LocationResult.WeatherStationName);
            Console.WriteLine("MCC: " + ip2LocationResult.MCC);
            Console.WriteLine("MNC: " + ip2LocationResult.MNC);
            Console.WriteLine("Mobile Brand: " + ip2LocationResult.MobileBrand);
            Console.WriteLine("Elevation: " + ip2LocationResult.Elevation);
            Console.WriteLine("Usage Type: " + ip2LocationResult.UsageType);
            Console.WriteLine("Address Type: " + ip2LocationResult.AddressType);
            Console.WriteLine("Category: " + ip2LocationResult.Category);
            Console.WriteLine("Status: " + ip2LocationResult.Status);
            break;
        case "EMPTY_IP_ADDRESS":
            Console.WriteLine("IP Address cannot be blank.");
            break;
        case "INVALID_IP_ADDRESS":
            Console.WriteLine("Invalid IP Address.");
            break;
        case "MISSING_FILE":
            Console.WriteLine("Invalid Database Path.");
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    ip2LocationUtility.Close();
}
```
