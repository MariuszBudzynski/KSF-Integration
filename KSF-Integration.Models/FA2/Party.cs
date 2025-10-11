using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Party
    {
        [XmlElement("DaneIdentyfikacyjne")]
        public IdentificationData IdentificationData { get; set; }

        [XmlElement("Adres")]
        public Address Address { get; set; }

        [XmlElement("DaneKontaktowe")]
        public ContactData ContactData { get; set; }

        [XmlElement("NrKlienta")]
        public string CustomerNumber { get; set; }
    }
}
