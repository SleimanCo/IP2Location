using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IP2Location_SampleApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            IP2Location.IP2LocationUtility oIP2Location = new IP2Location.IP2LocationUtility();
            IP2Location.IP2LocationResult oIPResult;
            StringBuilder sb = new StringBuilder();

            try
            {
                string strIPAddress = txtIP.Text;

                if (strIPAddress.Trim() != "")
                {
                    //oIP2Location.Open(@"C:\CTemp\IP2Location\IP2LOCATION-LITE-DB11.BIN", true);
                    oIP2Location.Open(@"C:\CTemp\IP2Location\IP2LOCATION-LITE-DB11.IPV6.BIN", true);
                    oIPResult = oIP2Location.IPQuery(strIPAddress);
                    switch (oIPResult.Status)
                    {
                        case "OK":
                            sb.AppendLine("IP Address: " + oIPResult.IPAddress);
                            sb.AppendLine("IP Address: " + oIPResult.IPAddress);
                            sb.AppendLine("Country Code: " + oIPResult.CountryShort);
                            sb.AppendLine("Country Name: " + oIPResult.CountryLong);
                            sb.AppendLine("Region: " + oIPResult.Region);
                            sb.AppendLine("City: " + oIPResult.City);
                            sb.AppendLine("Latitude: " + oIPResult.Latitude);
                            sb.AppendLine("Longitude: " + oIPResult.Longitude);
                            sb.AppendLine("Postal Code: " + oIPResult.ZipCode);
                            sb.AppendLine("TimeZone: " + oIPResult.TimeZone);
                            sb.AppendLine("ISP Name: " + oIPResult.InternetServiceProvider);
                            sb.AppendLine("Domain Name: " + oIPResult.DomainName);
                            sb.AppendLine("NetSpeed: " + oIPResult.NetSpeed);
                            sb.AppendLine("IDD Code: " + oIPResult.IDDCode);
                            sb.AppendLine("Area Code: " + oIPResult.AreaCode);
                            sb.AppendLine("Weather Station Code: " + oIPResult.WeatherStationCode);
                            sb.AppendLine("Weather Station Name: " + oIPResult.WeatherStationName);
                            sb.AppendLine("MCC: " + oIPResult.MCC);
                            sb.AppendLine("MNC: " + oIPResult.MNC);
                            sb.AppendLine("Mobile Brand: " + oIPResult.MobileBrand);
                            sb.AppendLine("Elevation: " + oIPResult.Elevation);
                            sb.AppendLine("Usage Type: " + oIPResult.UsageType);
                            sb.AppendLine("Address Type: " + oIPResult.AddressType);
                            sb.AppendLine("Category: " + oIPResult.Category);
                            break;
                        case "EMPTY_IP_ADDRESS":
                            sb.AppendLine("IP Address cannot be blank.");
                            break;
                        case "INVALID_IP_ADDRESS":
                            sb.AppendLine("Invalid IP Address.");
                            break;
                        case "MISSING_FILE":
                            sb.AppendLine("Invalid Database Path.");
                            break;
                    }
                }
                else
                {
                    sb.AppendLine("IP Address cannot be blank.");
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }
            finally
            {
                oIP2Location.Close();
            }

            txtResult.Text = sb.ToString();
        }
    }
}
