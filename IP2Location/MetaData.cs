// ---------------------------------------------------------------------------
// Author       : SleimanCo
// URL          : https://www.sleimanco.com
// Email        : info@sleimanco.com
// ---------------------------------------------------------------------------

using System.Globalization;

namespace IP2Location
{
    internal class MetaData
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