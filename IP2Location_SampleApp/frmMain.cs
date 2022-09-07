using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IP2Location;

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

        private void btnGetIPInfo_Click(object sender, EventArgs e)
        {
            IP2LocationUtility ip2LocationUtility = new IP2LocationUtility();
            IP2LocationResult ip2LocationResult;
            StringBuilder sb = new StringBuilder();

            try
            {
                string strPath = txtPath.Text.Trim();
                string strIPAddress = txtIP.Text.Trim();

                if (strPath != "" && strIPAddress != "")
                {
                    bool loaded = ip2LocationUtility.Open(strPath, true);
                    if (!loaded)
                    {
                        sb.AppendLine("Bin file can't be loaded.");
                        return;
                    }

                    ip2LocationResult = ip2LocationUtility.IPQuery(strIPAddress);

                    switch (ip2LocationResult.Status)
                    {
                        case "OK":
                            sb.AppendLine("IP Address: " + ip2LocationResult.IPAddress);
                            sb.AppendLine("IP Number: " + ip2LocationResult.IPNumber);
                            sb.AppendLine("Country Code: " + ip2LocationResult.CountryShort);
                            sb.AppendLine("Country Name: " + ip2LocationResult.CountryLong);
                            sb.AppendLine("Region: " + ip2LocationResult.Region);
                            sb.AppendLine("City: " + ip2LocationResult.City);
                            sb.AppendLine("Latitude: " + ip2LocationResult.Latitude);
                            sb.AppendLine("Longitude: " + ip2LocationResult.Longitude);
                            sb.AppendLine("Postal Code: " + ip2LocationResult.ZipCode);
                            sb.AppendLine("TimeZone: " + ip2LocationResult.TimeZone);
                            sb.AppendLine("ISP Name: " + ip2LocationResult.InternetServiceProvider);
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
                            sb.AppendLine("Category: " + ip2LocationResult.Category);
                            sb.AppendLine("Status: " + ip2LocationResult.Status);
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
                ip2LocationUtility.Close();
            }

            txtResult.Text = sb.ToString();
        }
    }
}
