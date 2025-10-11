using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Exemption
    {
        [XmlElement("P_19N")]
        public int P19N { get; set; }
    }
}
