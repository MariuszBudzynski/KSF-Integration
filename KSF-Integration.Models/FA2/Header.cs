using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Header
    {
        [XmlElement("KodFormularza")]
        public FormCode FormCode { get; set; }

        [XmlElement("WariantFormularza")]
        public int FormVariant { get; set; }

        [XmlElement("DataWytworzeniaFa")]
        public DateTime CreationDate { get; set; }

        [XmlElement("SystemInfo")]
        public string SystemInfo { get; set; }
    }
}
