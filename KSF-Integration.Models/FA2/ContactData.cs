using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class ContactData
    {
        [XmlElement("Email")]
        public string Email { get; set; }

        [XmlElement("Telefon")]
        public string Phone { get; set; }
    }
}
