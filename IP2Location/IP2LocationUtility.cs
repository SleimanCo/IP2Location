// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace IP2Location
{
    public sealed class IP2LocationUtility
    {
        private readonly object _LockLoadBIN = new object();
        private IP2LocationMetaData _MetaData = null;
        private MemoryMappedFile _MMF = null;
        private readonly int[,] _IndexArrayIPv4 = new int[65536, 2];
        private readonly int[,] _IndexArrayIPv6 = new int[65536, 2];
        private MemoryMappedViewAccessor _IPv4Accessor = null;
        private int _IPv4Offset = 0;
        private MemoryMappedViewAccessor _IPv6Accessor = null;
        private int _IPv6Offset = 0;
        private MemoryMappedViewAccessor _MapDataAccessor = null;
        private int _MapDataOffset = 0;
        private readonly Regex _OutlierCase1 = new Regex(@"^:(:[\dA-F]{1,4}){7}$", RegexOptions.IgnoreCase);
        private readonly Regex _OutlierCase2 = new Regex(@"^:(:[\dA-F]{1,4}){5}:(\d{1,3}\.){3}\d{1,3}$", RegexOptions.IgnoreCase);
        private readonly Regex _OutlierCase3 = new Regex(@"^\d+$");
        private readonly Regex _OutlierCase4 = new Regex(@"^([\dA-F]{1,4}:){6}(0\d+\.|.*?\.0\d+).*$");
        private readonly Regex _OutlierCase5 = new Regex(@"^(\d+\.){1,2}\d+$");
        private readonly Regex _IPv4MappedRegex = new Regex(@"^(.*:)((\d+\.){3}\d+)$");
        private readonly Regex _IPv4MappedRegex2 = new Regex(@"^.*((:[\dA-F]{1,4}){2})$");
        private readonly Regex _IPv4CompatibleRegex = new Regex(@"^::[\dA-F]{1,4}$", RegexOptions.IgnoreCase);
        private int _IPv4ColumnSize = 0;
        private int _IPv6ColumnSize = 0;
        private BigInteger _fromBI = new BigInteger(281470681743360L);
        private BigInteger _toBI = new BigInteger(281474976710655L);
        private BigInteger _FromBI2 = BigInteger.Parse("42545680458834377588178886921629466624");
        private BigInteger _ToBI2 = BigInteger.Parse("42550872755692912415807417417958686719");
        private BigInteger _FromBI3 = BigInteger.Parse("42540488161975842760550356425300246528");
        private BigInteger _ToBI3 = BigInteger.Parse("42540488241204005274814694018844196863");
        private BigInteger _DivBI = new BigInteger(4294967295L);

        private const long MAX_IPV4_RANGE = 4294967295L;
        private BigInteger MAX_IPV6_RANGE = BigInteger.Pow(2, 128) - 1;
        private const string MSG_OK = "OK";
        private const string MSG_INVALID_BIN = "Incorrect IP2Location BIN file format. Please make sure that you are using the latest IP2Location BIN file.";
        private const string MSG_NOT_SUPPORTED = "This method is not applicable for current IP2Location binary data file. Please upgrade your subscription package to install new data file.";

        private readonly byte[] COUNTRY_POSITION = new byte[] { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        private readonly byte[] REGION_POSITION = new byte[] { 0, 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        private readonly byte[] CITY_POSITION = new byte[] { 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
        private readonly byte[] ISP_POSITION = new byte[] { 0, 0, 3, 0, 5, 0, 7, 5, 7, 0, 8, 0, 9, 0, 9, 0, 9, 0, 9, 7, 9, 0, 9, 7, 9, 9 };
        private readonly byte[] LATITUDE_POSITION = new byte[] { 0, 0, 0, 0, 0, 5, 5, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        private readonly byte[] LONGITUDE_POSITION = new byte[] { 0, 0, 0, 0, 0, 6, 6, 0, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
        private readonly byte[] DOMAIN_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 6, 8, 0, 9, 0, 10, 0, 10, 0, 10, 0, 10, 8, 10, 0, 10, 8, 10, 10 };
        private readonly byte[] ZIPCODE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 0, 7, 7, 7, 0, 7, 0, 7, 7, 7, 0, 7, 7 };
        private readonly byte[] TIMEZONE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 7, 8, 8, 8, 7, 8, 0, 8, 8, 8, 0, 8, 8 };
        private readonly byte[] NETSPEED_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 11, 0, 11, 8, 11, 0, 11, 0, 11, 0, 11, 11 };
        private readonly byte[] IDDCODE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 12, 0, 12, 0, 12, 9, 12, 0, 12, 12 };
        private readonly byte[] AREACODE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 13, 0, 13, 0, 13, 10, 13, 0, 13, 13 };
        private readonly byte[] WEATHERSTATIONCODE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 14, 0, 14, 0, 14, 0, 14, 14 };
        private readonly byte[] WEATHERSTATIONNAME_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 15, 0, 15, 0, 15, 0, 15, 15 };
        private readonly byte[] MCC_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 16, 0, 16, 9, 16, 16 };
        private readonly byte[] MNC_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 17, 0, 17, 10, 17, 17 };
        private readonly byte[] MOBILEBRAND_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11, 18, 0, 18, 11, 18, 18 };
        private readonly byte[] ELEVATION_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11, 19, 0, 19, 19 };
        private readonly byte[] USAGETYPE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 20, 20 };
        private readonly byte[] ADDRESSTYPE_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21 };
        private readonly byte[] CATEGORY_POSITION = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 22 };

        private int COUNTRY_POSITION_OFFSET = 0;
        private int REGION_POSITION_OFFSET = 0;
        private int CITY_POSITION_OFFSET = 0;
        private int ISP_POSITION_OFFSET = 0;
        private int DOMAIN_POSITION_OFFSET = 0;
        private int ZIPCODE_POSITION_OFFSET = 0;
        private int LATITUDE_POSITION_OFFSET = 0;
        private int LONGITUDE_POSITION_OFFSET = 0;
        private int TIMEZONE_POSITION_OFFSET = 0;
        private int NETSPEED_POSITION_OFFSET = 0;
        private int IDDCODE_POSITION_OFFSET = 0;
        private int AREACODE_POSITION_OFFSET = 0;
        private int WEATHERSTATIONCODE_POSITION_OFFSET = 0;
        private int WEATHERSTATIONNAME_POSITION_OFFSET = 0;
        private int MCC_POSITION_OFFSET = 0;
        private int MNC_POSITION_OFFSET = 0;
        private int MOBILEBRAND_POSITION_OFFSET = 0;
        private int ELEVATION_POSITION_OFFSET = 0;
        private int USAGETYPE_POSITION_OFFSET = 0;
        private int ADDRESSTYPE_POSITION_OFFSET = 0;
        private int CATEGORY_POSITION_OFFSET = 0;

        private bool COUNTRY_ENABLED = false;
        private bool REGION_ENABLED = false;
        private bool CITY_ENABLED = false;
        private bool ISP_ENABLED = false;
        private bool DOMAIN_ENABLED = false;
        private bool ZIPCODE_ENABLED = false;
        private bool LATITUDE_ENABLED = false;
        private bool LONGITUDE_ENABLED = false;
        private bool TIMEZONE_ENABLED = false;
        private bool NETSPEED_ENABLED = false;
        private bool IDDCODE_ENABLED = false;
        private bool AREACODE_ENABLED = false;
        private bool WEATHERSTATIONCODE_ENABLED = false;
        private bool WEATHERSTATIONNAME_ENABLED = false;
        private bool MCC_ENABLED = false;
        private bool MNC_ENABLED = false;
        private bool MOBILEBRAND_ENABLED = false;
        private bool ELEVATION_ENABLED = false;
        private bool USAGETYPE_ENABLED = false;
        private bool ADDRESSTYPE_ENABLED = false;
        private bool CATEGORY_ENABLED = false;

        // Description: Gets or sets whether to use memory mapped file instead of filestream
        public bool UseMemoryMappedFile { get; set; } = false;

        // Description: Gets or sets the memory mapped file name
        public string MapFileName { get; set; } = "MyBIN";

        // Description: Returns the IP database version
        public string IPVersion
        {
            get
            {
                string returnval = "";
                if (_MetaData == null)
                {
                    if (LoadBIN())
                        returnval = _MetaData.IPVersion;
                }

                return returnval;
            }
        }

        // Description: Set/Get the value of IPv4+IPv6 database path
        public string IPDatabasePath { get; set; } = "";

        // Description: Set/Get the value of license key path (DEPRECATED)
        public string IPLicensePath { get; set; }

        // Description: Set the parameters and perform BIN pre-loading
        public void Open(string DBPath, bool UseMMF = false)
        {
            IPDatabasePath = DBPath;
            UseMemoryMappedFile = UseMMF;

            _ = LoadBIN();
        }

        // Description: Create memory mapped file
        private void CreateMemoryMappedFile()
        {
            // Using MyBIN instead of Global\MyBIN is coz the newer OSes don't grant permission to create global shared memory object
            // So new style is using localized memory object but using Global.asax.vb file to initialise and share the object
            if (_MMF == null)
            {
                try
                {
                    _MMF = MemoryMappedFile.OpenExisting(MapFileName, MemoryMappedFileRights.Read);
                }
                catch (Exception)
                {
                    try
                    {
                        _MMF = MemoryMappedFile.CreateFromFile(IPDatabasePath, FileMode.Open, MapFileName, new FileInfo(IPDatabasePath).Length, MemoryMappedFileAccess.Read);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            long len = new FileInfo(IPDatabasePath).Length;
                            _MMF = MemoryMappedFile.CreateNew(MapFileName, len, MemoryMappedFileAccess.ReadWrite);
                            using (MemoryMappedViewStream stream = _MMF.CreateViewStream())
                            {
                                using (BinaryWriter writer = new BinaryWriter(stream))
                                {
                                    using (FileStream fs = new FileStream(IPDatabasePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                    {
                                        byte[] buff = new byte[(int)(len + 1)];
                                        _ = fs.Read(buff, 0, buff.Length);
                                        writer.Write(buff, 0, buff.Length);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // this part onwards trying Linux specific stuff (no named map)
                            try
                            {
                                _MMF = MemoryMappedFile.OpenExisting(null, MemoryMappedFileRights.Read);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    _MMF = MemoryMappedFile.CreateFromFile(IPDatabasePath, FileMode.Open, null, new FileInfo(IPDatabasePath).Length, MemoryMappedFileAccess.Read);
                                }
                                catch (Exception)
                                {
                                    long len = new FileInfo(IPDatabasePath).Length;
                                    _MMF = MemoryMappedFile.CreateNew(null, len, MemoryMappedFileAccess.ReadWrite);
                                    using (MemoryMappedViewStream stream = _MMF.CreateViewStream())
                                    {
                                        using (BinaryWriter writer = new BinaryWriter(stream))
                                        {
                                            using (FileStream fs = new FileStream(IPDatabasePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                            {
                                                byte[] buff = new byte[(int)(len + 1)];
                                                _ = fs.Read(buff, 0, buff.Length);
                                                writer.Write(buff, 0, buff.Length);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Description: Destroy memory mapped file
        private void DestroyMemoryMappedFile()
        {
            if (_MMF != null)
            {
                _MMF.Dispose();
                _MMF = null;
            }
        }

        // Description: Create memory accessors
        private void CreateAccessors()
        {
            if (_IPv4Accessor == null)
            {
                int IPv4Bytes;
                IPv4Bytes = _IPv4ColumnSize * _MetaData.DBCount; // 4 bytes per column
                _IPv4Offset = _MetaData.BaseAddr - 1;
                _IPv4Accessor = _MMF.CreateViewAccessor(_IPv4Offset, IPv4Bytes, MemoryMappedFileAccess.Read); // assume MMF created
                _MapDataOffset = _IPv4Offset + IPv4Bytes;
            }

            if (!_MetaData.OldBIN && _IPv6Accessor == null)
            {
                int IPv6Bytes;
                IPv6Bytes = _IPv6ColumnSize * _MetaData.DBCountIPv6; // 4 bytes per column but IPFrom 16 bytes
                _IPv6Offset = _MetaData.BaseAddrIPv6 - 1;
                _IPv6Accessor = _MMF.CreateViewAccessor(_IPv6Offset, IPv6Bytes, MemoryMappedFileAccess.Read); // assume MMF created
                _MapDataOffset = _IPv6Offset + IPv6Bytes;
            }

            if (_MapDataAccessor == null)
            {
                _MapDataAccessor = _MMF.CreateViewAccessor(_MapDataOffset, 0L, MemoryMappedFileAccess.Read); // read from offset till EOF
            }
        }

        // Description: Destroy memory accessors
        private void DestroyAccessors()
        {
            if (_IPv4Accessor != null)
            {
                _IPv4Accessor.Dispose();
                _IPv4Accessor = null;
            }

            if (_IPv6Accessor != null)
            {
                _IPv6Accessor.Dispose();
                _IPv6Accessor = null;
            }

            if (_MapDataAccessor != null)
            {
                _MapDataAccessor.Dispose();
                _MapDataAccessor = null;
            }
        }

        // Description: Read BIN file into memory mapped file and create accessors
        public bool LoadBIN()
        {
            bool loadOK = false;
            lock (_LockLoadBIN)
            {
                if (!string.IsNullOrEmpty(IPDatabasePath))
                {
                    if (_MetaData == null)
                    {
                        using (FileStream _Filestream = new FileStream(IPDatabasePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            int len = 64; // 64-byte header
                            byte[] row = new byte[len];

                            _ = _Filestream.Seek(0L, SeekOrigin.Begin);
                            _ = _Filestream.Read(row, 0, len);

                            _MetaData = new IP2LocationMetaData();
                            {
                                ref IP2LocationMetaData withBlock = ref _MetaData;
                                withBlock.DBType = Read8FromHeader(ref row, 0);
                                withBlock.DBColumn = Read8FromHeader(ref row, 1);
                                withBlock.DBYear = Read8FromHeader(ref row, 2);
                                withBlock.DBMonth = Read8FromHeader(ref row, 3);
                                withBlock.DBDay = Read8FromHeader(ref row, 4);
                                withBlock.DBCount = Read32FromHeader(ref row, 5); // 4 bytes
                                withBlock.BaseAddr = Read32FromHeader(ref row, 9); // 4 bytes
                                withBlock.DBCountIPv6 = Read32FromHeader(ref row, 13); // 4 bytes
                                withBlock.BaseAddrIPv6 = Read32FromHeader(ref row, 17); // 4 bytes
                                withBlock.IndexBaseAddr = Read32FromHeader(ref row, 21); // 4 bytes
                                withBlock.IndexBaseAddrIPv6 = Read32FromHeader(ref row, 25); // 4 bytes
                                withBlock.ProductCode = Read8FromHeader(ref row, 29);
                                // below 2 fields just read for now, not being used yet
                                withBlock.ProductType = Read8FromHeader(ref row, 30);
                                withBlock.FileSize = Read32FromHeader(ref row, 31); // 4 bytes

                                // check if is correct BIN (should be 1 for IP2Location BIN file), also checking for zipped file (PK being the first 2 chars)
                                if ((withBlock.ProductCode != 1 && withBlock.DBYear >= 21) || (withBlock.DBType == 80 && withBlock.DBColumn == 75)) // only BINs from Jan 2021 onwards have this byte set
                                    throw new Exception(MSG_INVALID_BIN);

                                if (withBlock.IndexBaseAddr > 0)
                                    withBlock.Indexed = true;

                                if (withBlock.DBCountIPv6 == 0) // old style IPv4-only BIN file
                                {
                                    withBlock.OldBIN = true;
                                }
                                else if (withBlock.IndexBaseAddrIPv6 > 0)
                                {
                                    withBlock.IndexedIPv6 = true;
                                }

                                _IPv4ColumnSize = withBlock.DBColumn << 2; // 4 bytes each column
                                _IPv6ColumnSize = 16 + ((withBlock.DBColumn - 1) << 2); // 4 bytes each column, except IPFrom column which is 16 bytes

                                int dbt = withBlock.DBType;

                                COUNTRY_POSITION_OFFSET = COUNTRY_POSITION[dbt] != 0 ? (COUNTRY_POSITION[dbt] - 2) << 2 : 0;
                                REGION_POSITION_OFFSET = REGION_POSITION[dbt] != 0 ? (REGION_POSITION[dbt] - 2) << 2 : 0;
                                CITY_POSITION_OFFSET = CITY_POSITION[dbt] != 0 ? (CITY_POSITION[dbt] - 2) << 2 : 0;
                                ISP_POSITION_OFFSET = ISP_POSITION[dbt] != 0 ? (ISP_POSITION[dbt] - 2) << 2 : 0;
                                DOMAIN_POSITION_OFFSET = DOMAIN_POSITION[dbt] != 0 ? (DOMAIN_POSITION[dbt] - 2) << 2 : 0;
                                ZIPCODE_POSITION_OFFSET = ZIPCODE_POSITION[dbt] != 0 ? (ZIPCODE_POSITION[dbt] - 2) << 2 : 0;
                                LATITUDE_POSITION_OFFSET = LATITUDE_POSITION[dbt] != 0 ? (LATITUDE_POSITION[dbt] - 2) << 2 : 0;
                                LONGITUDE_POSITION_OFFSET = LONGITUDE_POSITION[dbt] != 0 ? (LONGITUDE_POSITION[dbt] - 2) << 2 : 0;
                                TIMEZONE_POSITION_OFFSET = TIMEZONE_POSITION[dbt] != 0 ? (TIMEZONE_POSITION[dbt] - 2) << 2 : 0;
                                NETSPEED_POSITION_OFFSET = NETSPEED_POSITION[dbt] != 0 ? (NETSPEED_POSITION[dbt] - 2) << 2 : 0;
                                IDDCODE_POSITION_OFFSET = IDDCODE_POSITION[dbt] != 0 ? (IDDCODE_POSITION[dbt] - 2) << 2 : 0;
                                AREACODE_POSITION_OFFSET = AREACODE_POSITION[dbt] != 0 ? (AREACODE_POSITION[dbt] - 2) << 2 : 0;
                                WEATHERSTATIONCODE_POSITION_OFFSET = WEATHERSTATIONCODE_POSITION[dbt] != 0 ? (WEATHERSTATIONCODE_POSITION[dbt] - 2) << 2 : 0;
                                WEATHERSTATIONNAME_POSITION_OFFSET = WEATHERSTATIONNAME_POSITION[dbt] != 0 ? (WEATHERSTATIONNAME_POSITION[dbt] - 2) << 2 : 0;
                                MCC_POSITION_OFFSET = MCC_POSITION[dbt] != 0 ? (MCC_POSITION[dbt] - 2) << 2 : 0;
                                MNC_POSITION_OFFSET = MNC_POSITION[dbt] != 0 ? (MNC_POSITION[dbt] - 2) << 2 : 0;
                                MOBILEBRAND_POSITION_OFFSET = MOBILEBRAND_POSITION[dbt] != 0 ? (MOBILEBRAND_POSITION[dbt] - 2) << 2 : 0;
                                ELEVATION_POSITION_OFFSET = ELEVATION_POSITION[dbt] != 0 ? (ELEVATION_POSITION[dbt] - 2) << 2 : 0;
                                USAGETYPE_POSITION_OFFSET = USAGETYPE_POSITION[dbt] != 0 ? (USAGETYPE_POSITION[dbt] - 2) << 2 : 0;
                                ADDRESSTYPE_POSITION_OFFSET = ADDRESSTYPE_POSITION[dbt] != 0 ? (ADDRESSTYPE_POSITION[dbt] - 2) << 2 : 0;
                                CATEGORY_POSITION_OFFSET = CATEGORY_POSITION[dbt] != 0 ? (CATEGORY_POSITION[dbt] - 2) << 2 : 0;

                                COUNTRY_ENABLED = COUNTRY_POSITION[dbt] != 0;
                                REGION_ENABLED = REGION_POSITION[dbt] != 0;
                                CITY_ENABLED = CITY_POSITION[dbt] != 0;
                                ISP_ENABLED = ISP_POSITION[dbt] != 0;
                                LATITUDE_ENABLED = LATITUDE_POSITION[dbt] != 0;
                                LONGITUDE_ENABLED = LONGITUDE_POSITION[dbt] != 0;
                                DOMAIN_ENABLED = DOMAIN_POSITION[dbt] != 0;
                                ZIPCODE_ENABLED = ZIPCODE_POSITION[dbt] != 0;
                                TIMEZONE_ENABLED = TIMEZONE_POSITION[dbt] != 0;
                                NETSPEED_ENABLED = NETSPEED_POSITION[dbt] != 0;
                                IDDCODE_ENABLED = IDDCODE_POSITION[dbt] != 0;
                                AREACODE_ENABLED = AREACODE_POSITION[dbt] != 0;
                                WEATHERSTATIONCODE_ENABLED = WEATHERSTATIONCODE_POSITION[dbt] != 0;
                                WEATHERSTATIONNAME_ENABLED = WEATHERSTATIONNAME_POSITION[dbt] != 0;
                                MCC_ENABLED = MCC_POSITION[dbt] != 0;
                                MNC_ENABLED = MNC_POSITION[dbt] != 0;
                                MOBILEBRAND_ENABLED = MOBILEBRAND_POSITION[dbt] != 0;
                                ELEVATION_ENABLED = ELEVATION_POSITION[dbt] != 0;
                                USAGETYPE_ENABLED = USAGETYPE_POSITION[dbt] != 0;
                                ADDRESSTYPE_ENABLED = ADDRESSTYPE_POSITION[dbt] != 0;
                                CATEGORY_ENABLED = CATEGORY_POSITION[dbt] != 0;

                                if (withBlock.Indexed)
                                {
                                    int readLen = _IndexArrayIPv4.GetLength(0);
                                    if (withBlock.IndexBaseAddrIPv6 > 0)
                                        readLen += _IndexArrayIPv6.GetLength(0);

                                    readLen *= 8; // 4 bytes for both From/To
                                    byte[] indexData = new byte[readLen];

                                    _ = _Filestream.Seek(withBlock.IndexBaseAddr - 1, SeekOrigin.Begin);
                                    _ = _Filestream.Read(indexData, 0, readLen);

                                    int pointer = 0;

                                    // read IPv4 index
                                    for (int x = _IndexArrayIPv4.GetLowerBound(0), loopTo = _IndexArrayIPv4.GetUpperBound(0); x <= loopTo; x++)
                                    {
                                        _IndexArrayIPv4[x, 0] = Read32FromHeader(ref indexData, pointer); // 4 bytes for from row
                                        _IndexArrayIPv4[x, 1] = Read32FromHeader(ref indexData, pointer + 4); // 4 bytes for to row
                                        pointer += 8;
                                    }

                                    if (withBlock.IndexedIPv6)
                                    {
                                        // read IPv6 index
                                        for (int x = _IndexArrayIPv6.GetLowerBound(0), loopTo1 = _IndexArrayIPv6.GetUpperBound(0); x <= loopTo1; x++)
                                        {
                                            _IndexArrayIPv6[x, 0] = Read32FromHeader(ref indexData, pointer); // 4 bytes for from row
                                            _IndexArrayIPv6[x, 1] = Read32FromHeader(ref indexData, pointer + 4); // 4 bytes for to row
                                            pointer += 8;
                                        }
                                    }
                                }
                            }
                        }

                        if (UseMemoryMappedFile)
                        {
                            CreateMemoryMappedFile();
                            CreateAccessors();
                        }

                        loadOK = true;
                    }
                }
            }

            return loadOK;
        }

        // Description: Make sure the component is registered (DEPRECATED)
        public bool IsRegistered()
        {
            return true;
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

        // Description: Query database to get location information by IP address
        public IP2LocationResult IPQuery(string myIPAddress)
        {
            IP2LocationResult obj = new IP2LocationResult();
            string strIP;
            int myIPType = 0;
            int myBaseAddr = 0;
            MemoryMappedViewAccessor myAccessor = null;
            FileStream myFilestream = null;

            long countrypos;
            long low = 0L;
            long high = 0L;
            long mid;
            BigInteger ipfrom;
            BigInteger ipto;
            BigInteger ipnum = 0;
            long indexaddr;
            BigInteger MAX_IP_RANGE = 0;
            long rowoffset;
            long rowoffset2;
            int myColumnSize = 0;
            bool overCapacity = false;
            byte[] fullRow = null;
            byte[] row;
            int firstCol = 4; // IP From is 4 bytes

            try
            {
                if (string.IsNullOrEmpty(myIPAddress) || myIPAddress == null)
                {
                    obj.Status = "EMPTY_IP_ADDRESS";
                    return obj;
                }

                strIP = VerifyIP(myIPAddress, ref myIPType, ref ipnum);
                if (strIP != "Invalid IP")
                {
                    myIPAddress = strIP;
                }
                else
                {
                    obj.Status = "INVALID_IP_ADDRESS";
                    return obj;
                }

                // Read BIN if haven't done so
                if (_MetaData == null)
                {
                    if (!LoadBIN()) // problems reading BIN
                    {
                        obj.Status = "MISSING_FILE";
                        return obj;
                    }
                }

                if (UseMemoryMappedFile)
                {
                    CreateMemoryMappedFile();
                    CreateAccessors();
                }
                else
                {
                    DestroyAccessors();
                    DestroyMemoryMappedFile();
                    myFilestream = new FileStream(IPDatabasePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                }

                switch (myIPType)
                {
                    case 4:
                        {
                            // IPv4
                            MAX_IP_RANGE = MAX_IPV4_RANGE;
                            high = _MetaData.DBCount;
                            if (UseMemoryMappedFile)
                            {
                                myAccessor = _IPv4Accessor;
                            }
                            else
                            {
                                myBaseAddr = _MetaData.BaseAddr;
                            }

                            myColumnSize = _IPv4ColumnSize;

                            if (_MetaData.Indexed)
                            {
                                indexaddr = (long)(ipnum >> 16);
                                low = _IndexArrayIPv4[(int)indexaddr, 0];
                                high = _IndexArrayIPv4[(int)indexaddr, 1];
                            }

                            break;
                        }
                    case 6:
                        {
                            // IPv6
                            firstCol = 16; // IPv6 is 16 bytes
                            if (_MetaData.OldBIN) // old IPv4-only BIN don't contain IPv6 data
                            {
                                obj.Status = "IPV6_NOT_SUPPORTED";
                                return obj;
                            }

                            MAX_IP_RANGE = MAX_IPV6_RANGE;
                            high = _MetaData.DBCountIPv6;
                            if (UseMemoryMappedFile)
                            {
                                myAccessor = _IPv6Accessor;
                            }
                            else
                            {
                                myBaseAddr = _MetaData.BaseAddrIPv6;
                            }

                            myColumnSize = _IPv6ColumnSize;

                            if (_MetaData.IndexedIPv6)
                            {
                                indexaddr = (long)(ipnum >> 112);
                                low = _IndexArrayIPv6[(int)indexaddr, 0];
                                high = _IndexArrayIPv6[(int)indexaddr, 1];
                            }

                            break;
                        }
                }

                if (ipnum >= MAX_IP_RANGE)
                    ipnum = MAX_IP_RANGE - 1;

                while (low <= high)
                {
                    mid = (int)Math.Round((low + high) / 2d);

                    rowoffset = myBaseAddr + (mid * myColumnSize);
                    rowoffset2 = rowoffset + myColumnSize;

                    if (UseMemoryMappedFile)
                    {
                        // only reading the IP From fields
                        overCapacity = rowoffset2 >= myAccessor.Capacity;
                        ipfrom = Read32or128(rowoffset, myIPType, ref myAccessor, ref myFilestream);
                        ipto = overCapacity ? BigInteger.Zero : Read32or128(rowoffset2, myIPType, ref myAccessor, ref myFilestream);
                    }
                    else
                    {
                        // reading IP From + whole row + next IP From
                        fullRow = ReadRow(rowoffset, (uint)(myColumnSize + firstCol), ref myAccessor, ref myFilestream);
                        ipfrom = Read32Or128Row(ref fullRow, 0, firstCol);
                        ipto = overCapacity ? BigInteger.Zero : Read32Or128Row(ref fullRow, myColumnSize, firstCol);
                    }

                    if (ipnum >= ipfrom && ipnum < ipto)
                    {
                        string country_short = MSG_NOT_SUPPORTED;
                        string country_long = MSG_NOT_SUPPORTED;
                        string region = MSG_NOT_SUPPORTED;
                        string city = MSG_NOT_SUPPORTED;
                        string isp = MSG_NOT_SUPPORTED;
                        float latitude = 0.0f;
                        float longitude = 0.0f;
                        string domain = MSG_NOT_SUPPORTED;
                        string zipcode = MSG_NOT_SUPPORTED;
                        string timezone = MSG_NOT_SUPPORTED;
                        string netspeed = MSG_NOT_SUPPORTED;
                        string iddcode = MSG_NOT_SUPPORTED;
                        string areacode = MSG_NOT_SUPPORTED;
                        string weatherstationcode = MSG_NOT_SUPPORTED;
                        string weatherstationname = MSG_NOT_SUPPORTED;
                        string mcc = MSG_NOT_SUPPORTED;
                        string mnc = MSG_NOT_SUPPORTED;
                        string mobilebrand = MSG_NOT_SUPPORTED;
                        float elevation = 0.0f;
                        string usagetype = MSG_NOT_SUPPORTED;
                        string addresstype = MSG_NOT_SUPPORTED;
                        string category = MSG_NOT_SUPPORTED;

                        int rowLen = myColumnSize - firstCol;

                        if (UseMemoryMappedFile)
                        {
                            row = ReadRow(rowoffset + firstCol, (uint)rowLen, ref myAccessor, ref myFilestream);
                        }
                        else
                        {
                            row = new byte[rowLen];
                            Array.Copy(fullRow, firstCol, row, 0, rowLen);
                        } // extract the actual row data

                        if (COUNTRY_ENABLED)
                        {
                            countrypos = (long)Read32FromRow(ref row, COUNTRY_POSITION_OFFSET);
                            country_short = ReadStr(countrypos, ref myFilestream);
                            country_long = ReadStr(countrypos + 3L, ref myFilestream);
                        }

                        if (REGION_ENABLED)
                            region = ReadStr((long)Read32FromRow(ref row, REGION_POSITION_OFFSET), ref myFilestream);
                        if (CITY_ENABLED)
                            city = ReadStr((long)Read32FromRow(ref row, CITY_POSITION_OFFSET), ref myFilestream);
                        if (ISP_ENABLED)
                            isp = ReadStr((long)Read32FromRow(ref row, ISP_POSITION_OFFSET), ref myFilestream);
                        if (DOMAIN_ENABLED)
                            domain = ReadStr((long)Read32FromRow(ref row, DOMAIN_POSITION_OFFSET), ref myFilestream);
                        if (ZIPCODE_ENABLED)
                            zipcode = ReadStr((long)Read32FromRow(ref row, ZIPCODE_POSITION_OFFSET), ref myFilestream);
                        if (LATITUDE_ENABLED)
                            latitude = (float)Math.Round(new decimal(ReadFloatFromRow(ref row, LATITUDE_POSITION_OFFSET)), 6);
                        if (LONGITUDE_ENABLED)
                            longitude = (float)Math.Round(new decimal(ReadFloatFromRow(ref row, LONGITUDE_POSITION_OFFSET)), 6);
                        if (TIMEZONE_ENABLED)
                            timezone = ReadStr((long)Read32FromRow(ref row, TIMEZONE_POSITION_OFFSET), ref myFilestream);
                        if (NETSPEED_ENABLED)
                            netspeed = ReadStr((long)Read32FromRow(ref row, NETSPEED_POSITION_OFFSET), ref myFilestream);
                        if (IDDCODE_ENABLED)
                            iddcode = ReadStr((long)Read32FromRow(ref row, IDDCODE_POSITION_OFFSET), ref myFilestream);
                        if (AREACODE_ENABLED)
                            areacode = ReadStr((long)Read32FromRow(ref row, AREACODE_POSITION_OFFSET), ref myFilestream);
                        if (WEATHERSTATIONCODE_ENABLED)
                            weatherstationcode = ReadStr((long)Read32FromRow(ref row, WEATHERSTATIONCODE_POSITION_OFFSET), ref myFilestream);
                        if (WEATHERSTATIONNAME_ENABLED)
                            weatherstationname = ReadStr((long)Read32FromRow(ref row, WEATHERSTATIONNAME_POSITION_OFFSET), ref myFilestream);
                        if (MCC_ENABLED)
                            mcc = ReadStr((long)Read32FromRow(ref row, MCC_POSITION_OFFSET), ref myFilestream);
                        if (MNC_ENABLED)
                            mnc = ReadStr((long)Read32FromRow(ref row, MNC_POSITION_OFFSET), ref myFilestream);
                        if (MOBILEBRAND_ENABLED)
                            mobilebrand = ReadStr((long)Read32FromRow(ref row, MOBILEBRAND_POSITION_OFFSET), ref myFilestream);
                        if (ELEVATION_ENABLED)
                            _ = float.TryParse(ReadStr((long)Read32FromRow(ref row, ELEVATION_POSITION_OFFSET), ref myFilestream), out elevation);
                        if (USAGETYPE_ENABLED)
                            usagetype = ReadStr((long)Read32FromRow(ref row, USAGETYPE_POSITION_OFFSET), ref myFilestream);
                        if (ADDRESSTYPE_ENABLED)
                            addresstype = ReadStr((long)Read32FromRow(ref row, ADDRESSTYPE_POSITION_OFFSET), ref myFilestream);
                        if (CATEGORY_ENABLED)
                            category = ReadStr((long)Read32FromRow(ref row, CATEGORY_POSITION_OFFSET), ref myFilestream);

                        obj.IPAddress = myIPAddress;
                        obj.IPNumber = ipnum.ToString();
                        obj.CountryShort = country_short;
                        obj.CountryLong = country_long;
                        obj.Region = region;
                        obj.City = city;
                        obj.InternetServiceProvider = isp;
                        obj.DomainName = domain;
                        obj.ZipCode = zipcode;
                        obj.NetSpeed = netspeed;
                        obj.IDDCode = iddcode;
                        obj.AreaCode = areacode;
                        obj.WeatherStationCode = weatherstationcode;
                        obj.WeatherStationName = weatherstationname;
                        obj.TimeZone = timezone;
                        obj.Latitude = latitude;
                        obj.Longitude = longitude;
                        obj.MCC = mcc;
                        obj.MNC = mnc;
                        obj.MobileBrand = mobilebrand;
                        obj.Elevation = elevation;
                        obj.UsageType = usagetype;
                        obj.AddressType = addresstype;
                        obj.Category = category;
                        obj.Status = MSG_OK;

                        return obj;
                    }
                    else if (ipnum < ipfrom)
                    {
                        high = mid - 1L;
                    }
                    else
                    {
                        low = mid + 1L;
                    }
                }

                obj.Status = "IP_ADDRESS_NOT_FOUND";
                return obj;
            }
            finally
            {
                if (myFilestream != null)
                {
                    myFilestream.Close();
                    myFilestream.Dispose();
                }
            }
        }

        // Read whole row into array of bytes
        private byte[] ReadRow(long _Pos, uint MyLen, ref MemoryMappedViewAccessor MyAccessor, ref FileStream MyFilestream)
        {
            byte[] row = new byte[(int)(MyLen - 1L + 1)];

            if (UseMemoryMappedFile)
            {
                _ = MyAccessor.ReadArray(_Pos, row, 0, (int)MyLen);
            }
            else
            {
                _ = MyFilestream.Seek(_Pos - 1L, SeekOrigin.Begin);
                _ = MyFilestream.Read(row, 0, (int)MyLen);
            }

            return row;
        }

        private BigInteger Read32Or128Row(ref byte[] row, int byteOffset, int len)
        {
            byte[] _Byte = new byte[len + 1]; // extra 1 zero byte at the end is for making the BigInteger unsigned
            Array.Copy(row, byteOffset, _Byte, 0, len);
            return new BigInteger(_Byte);
        }

        private BigInteger Read32or128(long _Pos, int _MyIPType, ref MemoryMappedViewAccessor MyAccessor, ref FileStream MyFilestream)
        {
            return _MyIPType == 4
                ? Read32(_Pos, ref MyAccessor, ref MyFilestream)
                : _MyIPType == 6 ? Read128(_Pos, ref MyAccessor, ref MyFilestream) : (BigInteger)0;
        }

        // Read 128 bits in the database
        private BigInteger Read128(long _Pos, ref MemoryMappedViewAccessor MyAccessor, ref FileStream MyFilestream)
        {
            BigInteger bigRetVal;

            if (UseMemoryMappedFile)
            {
                bigRetVal = MyAccessor.ReadUInt64(_Pos + 8L);
                bigRetVal <<= 64;
                bigRetVal += MyAccessor.ReadUInt64(_Pos);
            }
            else
            {
                byte[] _Byte = new byte[16]; // 16 bytes
                _ = MyFilestream.Seek(_Pos - 1L, SeekOrigin.Begin);
                _ = MyFilestream.Read(_Byte, 0, 16);
                bigRetVal = BitConverter.ToUInt64(_Byte, 8);
                bigRetVal <<= 64;
                bigRetVal += BitConverter.ToUInt64(_Byte, 0);
            }

            return bigRetVal;
        }

        // Read 8 bits in header
        private int Read8FromHeader(ref byte[] row, int byteOffset)
        {
            byte[] _Byte = new byte[1]; // 1 byte
            Array.Copy(row, byteOffset, _Byte, 0, 1);
            return _Byte[0];
        }

        // Read 32 bits in header
        private int Read32FromHeader(ref byte[] row, int byteOffset)
        {
            byte[] _Byte = new byte[4]; // 4 bytes
            Array.Copy(row, byteOffset, _Byte, 0, 4);
            return (int)BitConverter.ToUInt32(_Byte, 0);
        }

        // Read 32 bits in byte array
        private BigInteger Read32FromRow(ref byte[] row, int byteOffset)
        {
            byte[] _Byte = new byte[4]; // 4 bytes
            Array.Copy(row, byteOffset, _Byte, 0, 4);
            return BitConverter.ToUInt32(_Byte, 0);
        }

        // Read 32 bits in the database
        private BigInteger Read32(long _Pos, ref MemoryMappedViewAccessor MyAccessor, ref FileStream MyFilestream)
        {
            if (UseMemoryMappedFile)
            {
                return MyAccessor.ReadUInt32(_Pos);
            }
            else
            {
                byte[] _Byte = new byte[4]; // 4 bytes
                _ = MyFilestream.Seek(_Pos - 1L, SeekOrigin.Begin);
                _ = MyFilestream.Read(_Byte, 0, 4);
                return BitConverter.ToUInt32(_Byte, 0);
            }
        }

        // Read string in the database
        private string ReadStr(long _Pos, ref FileStream Myfilestream)
        {
            int _Size = 256; // max size of string field + 1 byte for the length
            byte[] _Data = new byte[_Size];

            if (UseMemoryMappedFile)
            {
                byte _Len;
                byte[] _Bytes;
                _Pos -= _MapDataOffset; // position stored in BIN file is for full file, not just the mapped data segment, so need to minus
                _ = _MapDataAccessor.ReadArray(_Pos, _Data, 0, _Size);
                _Len = _Data[0];
                _Bytes = new byte[_Len];
                Array.Copy(_Data, 1, _Bytes, 0, _Len);
                return Encoding.Default.GetString(_Bytes);
            }
            else
            {
                byte _Len;
                byte[] _Bytes;
                _ = Myfilestream.Seek(_Pos, SeekOrigin.Begin);
                _ = Myfilestream.Read(_Data, 0, _Size);
                _Len = _Data[0];
                _Bytes = new byte[_Len];
                Array.Copy(_Data, 1, _Bytes, 0, _Len);
                return Encoding.Default.GetString(_Bytes);
            }
        }

        // Read float number in byte array
        private float ReadFloatFromRow(ref byte[] row, int byteOffset)
        {
            byte[] _Byte = new byte[4];
            Array.Copy(row, byteOffset, _Byte, 0, 4);
            return BitConverter.ToSingle(_Byte, 0);
        }

        // Description: Initialize
        public IP2LocationUtility()
        {
        }

        // Description: Remove memory accessors
        ~IP2LocationUtility()
        {
            DestroyAccessors();
        }

        // Description: Destroy memory accessors & memory mapped file (only use in specific cases, otherwise don't use)
        public void Close()
        {
            try
            {
                _MetaData = null;
                DestroyAccessors();
                DestroyMemoryMappedFile();
            }
            catch (Exception)
            {
                // do nothing
            }
        }

        // Description: Validate the IP address input
        private string VerifyIP(string strParam, ref int strIPType, ref BigInteger ipnum)
        {
            try
            {
                string finalIP = "";

                // do checks for outlier cases here
                if (_OutlierCase1.IsMatch(strParam) || _OutlierCase2.IsMatch(strParam)) // good ip list outliers
                    strParam = "0000" + strParam.Substring(1);

                if (!_OutlierCase3.IsMatch(strParam) && !_OutlierCase4.IsMatch(strParam) && !_OutlierCase5.IsMatch(strParam) && IPAddress.TryParse(strParam, out IPAddress address))
                {
                    switch (address.AddressFamily)
                    {
                        case System.Net.Sockets.AddressFamily.InterNetwork:
                            {
                                strIPType = 4;
                                break;
                            }
                        case System.Net.Sockets.AddressFamily.InterNetworkV6:
                            {
                                strIPType = 6;
                                break;
                            }

                        default:
                            {
                                return "Invalid IP";
                            }
                    }

                    finalIP = address.ToString().ToUpper();

                    ipnum = IPNo(ref address);

                    if (strIPType == 6)
                    {
                        if (ipnum >= _fromBI && ipnum <= _toBI)
                        {
                            // ipv4-mapped ipv6 should treat as ipv4 and read ipv4 data section
                            strIPType = 4;
                            ipnum -= _fromBI;

                            // expand ipv4-mapped ipv6
                            if (_IPv4MappedRegex.IsMatch(finalIP))
                            {
                                string tmp = string.Join("", Enumerable.Repeat("0000:", 5).ToList());
                                finalIP = finalIP.Replace("::", tmp);
                            }
                            else if (_IPv4MappedRegex2.IsMatch(finalIP))
                            {
                                Match mymatch = _IPv4MappedRegex2.Match(finalIP);
                                int x = 0;

                                string tmp = mymatch.Groups[1].ToString();
                                string[] tmparr = tmp.Trim(':').Split(':');
                                int len = tmparr.Length - 1;
                                int loopTo = len;
                                for (x = 0; x <= loopTo; x++)
                                    tmparr[x] = tmparr[x].PadLeft(4, '0');
                                string myrear = string.Join("", tmparr);
                                byte[] bytes;

                                bytes = BitConverter.GetBytes(Convert.ToInt32("0x" + myrear, 16));
                                finalIP = finalIP.Replace(tmp, ":" + bytes[3] + "." + bytes[2] + "." + bytes[1] + "." + bytes[0]);
                                tmp = string.Join("", Enumerable.Repeat("0000:", 5).ToList());
                                finalIP = finalIP.Replace("::", tmp);
                            }
                        }
                        else if (ipnum >= _FromBI2 && ipnum <= _ToBI2)
                        {
                            // 6to4 so need to remap to ipv4
                            strIPType = 4;

                            ipnum >>= 80;
                            ipnum &= _DivBI; // get last 32 bits
                        }
                        else if (ipnum >= _FromBI3 && ipnum <= _ToBI3)
                        {
                            // Teredo so need to remap to ipv4
                            strIPType = 4;

                            ipnum = (-1) * ipnum;
                            ipnum &= _DivBI; // get last 32 bits
                        }
                        else if (ipnum <= MAX_IPV4_RANGE)
                        {
                            // ipv4-compatible ipv6 (DEPRECATED BUT STILL SUPPORTED BY .NET)
                            strIPType = 4;

                            if (_IPv4CompatibleRegex.IsMatch(finalIP))
                            {
                                byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(finalIP.Replace("::", "0x"), 16));
                                finalIP = "::" + bytes[3] + "." + bytes[2] + "." + bytes[1] + "." + bytes[0];
                            }
                            else if (finalIP == "::")
                            {
                                finalIP += "0.0.0.0";
                            }

                            string tmp = string.Join("", Enumerable.Repeat("0000:", 5).ToList());
                            finalIP = finalIP.Replace("::", tmp + "FFFF:");
                        }
                        else
                        {
                            // expand ipv6 normal
                            string[] myarr = Regex.Split(finalIP, "::");
                            int x = 0;
                            List<string> leftside = new List<string>();
                            leftside.AddRange(myarr[0].Split(':'));

                            if (myarr.Length > 1)
                            {
                                List<string> rightside = new List<string>();
                                rightside.AddRange(myarr[1].Split(':'));

                                List<string> midarr;
                                midarr = Enumerable.Repeat("0000", 8 - leftside.Count - rightside.Count).ToList();

                                rightside.InsertRange(0, midarr);
                                rightside.InsertRange(0, leftside);

                                int rlen = rightside.Count - 1;
                                int loopTo1 = rlen;
                                for (x = 0; x <= loopTo1; x++)
                                    rightside[x] = rightside[x].PadLeft(4, '0');

                                finalIP = string.Join(":", rightside);
                            }
                            else
                            {
                                int llen = leftside.Count - 1;
                                int loopTo2 = llen;
                                for (x = 0; x <= loopTo2; x++)
                                    leftside[x] = leftside[x].PadLeft(4, '0');

                                finalIP = string.Join(":", leftside);
                            }
                        }
                    }

                    return finalIP;
                }
                else
                {
                    return "Invalid IP";
                }
            }
            catch (Exception)
            {
                return "Invalid IP";
            }
        }

        // Description: Convert either IPv4 or IPv6 into big integer
        private BigInteger IPNo(ref IPAddress ipAddress)
        {
            try
            {
                byte[] addrBytes = ipAddress.GetAddressBytes();
                LittleEndian(ref addrBytes);

                BigInteger final;

                if (addrBytes.Length > 8)
                {
                    // IPv6
                    final = BitConverter.ToUInt64(addrBytes, 8);
                    final <<= 64;
                    final += BitConverter.ToUInt64(addrBytes, 0);
                }
                else
                {
                    // IPv4
                    final = BitConverter.ToUInt32(addrBytes, 0);
                }

                return final;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    internal class IP2LocationMetaData
    {
        public int BaseAddr { get; set; } = 0;
        public int DBCount { get; set; } = 0;
        public int DBColumn { get; set; } = 0;
        public int DBType { get; set; } = 0;
        public int DBDay { get; set; } = 1;
        public int DBMonth { get; set; } = 1;
        public int DBYear { get; set; } = 1;
        public int BaseAddrIPv6 { get; set; } = 0;
        public int DBCountIPv6 { get; set; } = 0;
        public string IPVersion =>
            DBYear.ToString(CultureInfo.CurrentCulture) + "." +
            DBMonth.ToString(CultureInfo.CurrentCulture) + "." +
            DBDay.ToString(CultureInfo.CurrentCulture);
        public bool OldBIN { get; set; } = false;
        public bool Indexed { get; set; } = false;
        public bool IndexedIPv6 { get; set; } = false;
        public int IndexBaseAddr { get; set; } = 0;
        public int IndexBaseAddrIPv6 { get; set; } = 0;
        public int ProductCode { get; set; } = 0;
        public int ProductType { get; set; } = 0;
        public int FileSize { get; set; } = 0;
    }
}