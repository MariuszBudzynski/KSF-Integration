using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class FormCode
    {
        [XmlAttribute("kodSystemowy")]
        public string SystemCode { get; set; }

        [XmlAttribute("wersjaSchemy")]
        public string SchemaVersion { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
