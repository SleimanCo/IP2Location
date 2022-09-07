// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text.RegularExpressions;

namespace IP2Location
{
    public class IPTools
    {
        private const long MAX_IPV4_RANGE = 4294967295L;
        private BigInteger MAX_IPV6_RANGE = BigInteger.Pow(2, 128) - 1;

        // Description: Checks if IP address is IPv6
        public bool IsIPv4(string IP)
        {
            try
            {

                if (IPAddress.TryParse(IP, out IPAddress address))
                {
                    switch (address.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                            return true;
                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Description: Checks if IP address is IPv6
        public bool IsIPv6(string IP)
        {
            try
            {

                if (IPAddress.TryParse(IP, out IPAddress address))
                {
                    switch (address.AddressFamily)
                    {
                        case AddressFamily.InterNetworkV6:
                            return true;

                        default:
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Description: Reverse the bytes if system is little endian
        private void LittleEndian(ref byte[] byteArr)
        {
            if (BitConverter.IsLittleEndian)
            {
                List<byte> byteList = new List<byte>(byteArr);
                byteList.Reverse();
                byteArr = byteList.ToArray();
            }
        }

        // Description: Convert either IPv4 or IPv6 into big integer
        private BigInteger IPNo(ref IPAddress IP)
        {
            try
            {
                byte[] addrbytes = IP.GetAddressBytes();
                LittleEndian(ref addrbytes);

                BigInteger final;

                if (addrbytes.Length > 8)
                {
                    // IPv6
                    final = BitConverter.ToUInt64(addrbytes, 8);
                    final <<= 64;
                    final += BitConverter.ToUInt64(addrbytes, 0);
                }
                else
                {
                    // IPv4
                    final = BitConverter.ToUInt32(addrbytes, 0);
                }

                return final;
            }
            catch (Exception)
            {
                return default;
            }
        }

        // Description: Convert IPv4 into big integer
        public BigInteger IPv4ToDecimal(string IP)
        {
            if (IsIPv4(IP))
            {
                IPAddress argIP = IPAddress.Parse(IP);
                return IPNo(ref argIP);
            }
            else
            {
                return default;
            }
        }

        // Description: Convert IPv6 into big integer
        public BigInteger IPv6ToDecimal(string IP)
        {
            if (IsIPv6(IP))
            {
                IPAddress argIP = IPAddress.Parse(IP);
                return IPNo(ref argIP);
            }
            else
            {
                return default;
            }
        }

        private string NumToIPv4(BigInteger IPNum)
        {
            string result;
            byte[] arr;
            string[] str = new string[4];
            arr = IPNum.ToByteArray();
            int x;

            LittleEndian(ref arr);

            if (arr.Length > 4)
                arr = arr.Skip(1).ToArray(); // remove the 2's complement signed bit

            if (arr.Length < 4)
            {
                // need to insert missing bytes to the front of the array
                List<byte> list = arr.ToList();
                int loopTo = 4 - arr.Length;
                for (x = 1; x <= loopTo; x++)
                    list.Insert(0, 0);
                arr = list.ToArray();
            }

            int loopTo1 = arr.GetUpperBound(0);
            for (x = arr.GetLowerBound(0); x <= loopTo1; x++)
                str[x] = arr[x].ToString();
            result = string.Join(".", str);
            return result;
        }

        // Description: Convert big integer into IPv4
        public string DecimalToIPv4(BigInteger IPNum)
        {
            return IPNum < (BigInteger)0L || IPNum > (BigInteger)MAX_IPV4_RANGE ? null : NumToIPv4(IPNum);
        }

        private string NumToIPv6(BigInteger IPNum)
        {
            string result = "";
            byte[] arr;

            _ = new string[8];
            int x;
            arr = IPNum.ToByteArray();

            LittleEndian(ref arr);

            if (arr.Length > 16)
                arr = arr.Skip(1).ToArray(); // remove the 2's complement signed bit

            if (arr.Length < 16)
            {
                // need to insert missing bytes to the front of the array
                List<byte> list = arr.ToList();
                int loopTo = 16 - arr.Length;
                for (x = 1; x <= loopTo; x++)
                    list.Insert(0, 0);
                arr = list.ToArray();
            }

            int loopTo1 = arr.GetUpperBound(0);
            for (x = arr.GetLowerBound(0); x <= loopTo1; x++)
                result += arr[x].ToString("x2").PadLeft(2, '0');

            result = Regex.Replace(result, ".{4}", "$0:").TrimEnd(':');
            result = CompressIPv6(result);
            return result;
        }

        // Description: Convert big integer into IPv6
        public string DecimalToIPv6(BigInteger IPNum)
        {
            return IPNum < 0L || IPNum > MAX_IPV6_RANGE ? null : NumToIPv6(IPNum);
        }

        // Description: Convert IPv6 into compressed form
        public string CompressIPv6(string IP)
        {
            try
            {
                IPAddress address = null;
                if (IsIPv6(IP))
                {
                    address = IPAddress.Parse(IP);
                    return address.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Description: Convert IPv6 into expanded form
        public string ExpandIPv6(string IP)
        {
            try
            {
                IPAddress address = null;
                string result = "";
                if (IsIPv6(IP))
                {
                    address = IPAddress.Parse(IP);

                    byte[] addrBytes = address.GetAddressBytes();

                    int x;
                    int loopTo = addrBytes.GetUpperBound(0);
                    for (x = addrBytes.GetLowerBound(0); x <= loopTo; x++)
                        result += addrBytes[x].ToString("x2").PadLeft(2, '0');

                    result = Regex.Replace(result, ".{4}", "$0:").TrimEnd(':');

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Description: Convert IPv4 range into CIDR
        public List<string> IPv4ToCIDR(string IPFrom, string IPTo)
        {
            if (!IsIPv4(IPFrom) || !IsIPv4(IPTo))
                return null;

            long startip = (long)IPv4ToDecimal(IPFrom);
            long endip = (long)IPv4ToDecimal(IPTo);

            List<string> result = new List<string>();

            while (endip >= startip)
            {
                byte maxsize = 32;

                while (maxsize > 0)
                {
                    long mask = IPv4Mask(maxsize - 1);
                    long maskbase = startip & mask;

                    if (maskbase != startip)
                        break;

                    maxsize = (byte)(maxsize - 1);
                }

                double x = Math.Log(endip - startip + 1L) / Math.Log(2d);
                byte maxdiff = (byte)Math.Round(32d - Math.Floor(x));

                if (maxsize < maxdiff)
                    maxsize = maxdiff;

                string ip = DecimalToIPv4(startip);
                result.Add(ip + "/" + Convert.ToString(maxsize));
                startip = (long)Math.Round(startip + Math.Pow(2d, 32 - maxsize));
            }

            return result;
        }

        private long IPv4Mask(int s)
        {
            return (long)Math.Round(Math.Pow(2d, 32d) - Math.Pow(2d, 32 - s));
        }

        // Description: Convert IPv6 range into CIDR
        public List<string> IPv6ToCIDR(string IPFrom, string IPTo)
        {
            if (!IsIPv6(IPFrom) || !IsIPv6(IPTo))
                return null;

            string ipfrombin = IPToBinary(IPFrom);
            string iptobin = IPToBinary(IPTo);

            List<string> result = new List<string>();
            int networksize = 0;
            int shift;
            Dictionary<string, int> networks = new Dictionary<string, int>();
            int n;
            List<int> vals = new List<int>();

            if (string.Compare(ipfrombin, iptobin) == 0)
            {
                result.Add(IPFrom + "/128");
                return result;
            }

            if (string.Compare(ipfrombin, iptobin) > 0)
            {
                (iptobin, ipfrombin) = (ipfrombin, iptobin);
            }

            do
            {
                if (ipfrombin[ipfrombin.Length - 1].ToString() == "1")
                {
                    networks.Add(ipfrombin.Substring(networksize, 128 - networksize).PadRight(128, '0'), 128 - networksize);
                    n = ipfrombin.LastIndexOf("0");
                    ipfrombin = (n == 0 ? "" : ipfrombin.Substring(0, n)) + "1";
                    ipfrombin = ipfrombin.PadRight(128, '0');
                }

                if (iptobin[iptobin.Length - 1].ToString() == "0")
                {
                    networks.Add(iptobin.Substring(networksize, 128 - networksize).PadRight(128, '0'), 128 - networksize);
                    n = iptobin.LastIndexOf("1");
                    iptobin = (n == 0 ? "" : iptobin.Substring(0, n)) + "0";
                    iptobin = iptobin.PadRight(128, '1');
                }

                if (string.Compare(iptobin, ipfrombin) < 0)
                    continue;

                vals.Clear();
                vals.Add(ipfrombin.LastIndexOf("0"));
                vals.Add(iptobin.LastIndexOf("1"));
                shift = 128 - vals.Max();

                ipfrombin = ipfrombin.Substring(0, 128 - shift).PadLeft(128, '0');
                iptobin = iptobin.Substring(0, 128 - shift).PadLeft(128, '0');
                networksize += shift;

                if (string.Compare(ipfrombin, iptobin) == 0)
                {
                    networks.Add(ipfrombin.Substring(networksize, 128 - networksize).PadRight(128, '0'), 128 - networksize);
                    continue;
                }
            }

            while (string.Compare(ipfrombin, iptobin) < 0);

            // Get list of keys.
            List<string> keys = networks.Keys.ToList();

            // Sort the keys.
            keys.Sort();

            foreach (string ip in keys)
                result.Add(BinaryToIP(ip) + "/" + networks[ip]);

            return result;
        }

        // Description: Convert IPv6 into binary string representation
        private string IPToBinary(string IP)
        {
            if (IsIPv6(IP))
            {
                IPAddress address;
                address = IPAddress.Parse(IP);
                byte[] addrbytes = address.GetAddressBytes();

                int x;
                string result = "";

                int loopTo = addrbytes.GetUpperBound(0);
                for (x = addrbytes.GetLowerBound(0); x <= loopTo; x++)
                    result += Convert.ToString(addrbytes[x], 2).PadLeft(8, '0');
                return result;
            }
            else
            {
                return null;
            }
        }

        // Description: Convert binary string representation into IPv6
        private string BinaryToIP(string Binary)
        {
            if (!Regex.Match(Binary, "^([01]{8})+$").Success || Binary.Length != 128)
                return null;

            MatchCollection octets = Regex.Matches(Binary, "[01]{8}");
            string result;
            List<byte> list = new List<byte>();
            foreach (Match m in octets)
                list.Add(Convert.ToByte(m.ToString(), 2));
            IPAddress address = new IPAddress(list.ToArray());
            result = address.ToString();
            return result;
        }

        // Description: Convert CIDR into IPv4 range
        public (string IPStart, string IPEnd) CIDRToIPv4(string CIDR)
        {
            if (CIDR.IndexOf("/") == -1)
                return default;

            string ip;
            long prefix;
            string[] arr = CIDR.Split('/');
            string ipstart;
            string ipend;
            long ipstartlong;
            long ipendlong;
            long total;

            if (arr.Length != 2 || !IsIPv4(arr[0]) || !Regex.Match(arr[1], "^[0-9]{1,2}$").Success || Convert.ToInt64(arr[1]) > 32L)
                return default;

            ip = arr[0];
            prefix = Convert.ToInt64(arr[1]);

            ipstartlong = (long)IPv4ToDecimal(ip);
            ipstartlong &= -1 << (int)(32L - prefix);
            ipstart = DecimalToIPv4(ipstartlong);

            total = 1 << (int)(32L - prefix);

            ipendlong = ipstartlong + total - 1L;

            if (ipendlong > 4294967295L)
                ipendlong = 4294967295L;

            ipend = DecimalToIPv4(ipendlong);

            (string ipstart, string ipend) result = (ipstart, ipend);

            return result;
        }

        // Description: Convert CIDR into IPv6 range
        public (string IPStart, string IPEnd) CIDRToIPv6(string CIDR)
        {
            if (CIDR.IndexOf("/") == -1)
                return default;

            string ip;
            int prefix;
            string[] arr = CIDR.Split('/');

            if (arr.Length != 2 || !IsIPv6(arr[0]) || !Regex.Match(arr[1], "^[0-9]{1,3}$").Success || Convert.ToInt64(arr[1]) > 128L)
                return default;

            ip = arr[0];
            prefix = (int)Convert.ToInt64(arr[1]);

            string hexstartaddress = ExpandIPv6(ip).Replace(":", "");
            string hexlastaddress = hexstartaddress;

            int bits = 128 - prefix;
            int x;
            string y;
            int pos = 31;
            List<int> vals = new List<int>();
            while (bits > 0)
            {
                vals.Clear();
                vals.Add(4);
                vals.Add(bits);
                x = Convert.ToInt32(hexlastaddress.Substring(pos, 1), 16);
                y = ((long)x | (long)Math.Round(Math.Pow(2d, vals.Min()) - 1d)).ToString("x"); // single hex char

                hexlastaddress = hexlastaddress.Remove(pos, 1).Insert(pos, y);

                bits -= 4;
                pos -= 1;
            }

            hexstartaddress = Regex.Replace(hexstartaddress, ".{4}", "$0:").TrimEnd(':');
            hexlastaddress = Regex.Replace(hexlastaddress, ".{4}", "$0:").TrimEnd(':');

            return (hexstartaddress, hexlastaddress);
        }
    }
}