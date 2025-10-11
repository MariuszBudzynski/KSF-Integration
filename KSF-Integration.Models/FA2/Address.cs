using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Address
    {
        [XmlElement("KodKraju")]
        public string CountryCode { get; set; }

        [XmlElement("AdresL1")]
        public string Line1 { get; set; }

        [XmlElement("AdresL2")]
        public string Line2 { get; set; }
    }
}
