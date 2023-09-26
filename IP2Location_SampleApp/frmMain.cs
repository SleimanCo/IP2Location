using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void btnGetIPInfoToTextBox_Click(object sender, EventArgs e)
        {
            IP2LocationUtility ip2LocationUtility = new IP2LocationUtility();
            IP2LocationResult ip2LocationResult;
            StringBuilder sb = new StringBuilder();

            try
            {
                string strPath = txtPath.Text.Trim();
                string strIPAddress = txtIP.Text.Trim();

                if (strPath == "")
                {
                    sb.AppendLine("Bin path cannot be blank.");
                    return;
                }

                if (strIPAddress == "")
                {
                    sb.AppendLine("IP Address cannot be blank.");
                    return;
                }

                bool loaded = ip2LocationUtility.Open(strPath, true);
                if (!loaded)
                {
                    sb.AppendLine("Bin file cannot be loaded.");
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

        private void btnGetIPInfoToConsole_Click(object sender, EventArgs e)
        {
            IP2LocationUtility ip2LocationUtility = new IP2LocationUtility();
            IP2LocationResult ip2LocationResult;

            try
            {
                string strPath = txtPath.Text.Trim();
                string strIPAddress = txtIP.Text.Trim();
                //string strPath = @"C:\Temp\IP2Location\IP2LOCATION-LITE-DB11.BIN";
                //string strIPAddress = "8.8.8.8";

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
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string strExecPath = Path.GetDirectoryName(Application.ExecutablePath);
            txtPath.Text = strExecPath + @"\Data\IP2LOCATION-LITE-DB1.IPV6.BIN";
        }
    }
}
