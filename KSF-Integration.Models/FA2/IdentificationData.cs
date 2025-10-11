using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class IdentificationData
    {
        [XmlElement("NIP")]
        public string TaxId { get; set; }

        [XmlElement("Nazwa")]
        public string Name { get; set; }
    }
}
