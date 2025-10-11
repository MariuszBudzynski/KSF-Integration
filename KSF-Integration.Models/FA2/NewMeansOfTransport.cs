using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class NewMeansOfTransport
    {
        [XmlElement("P_22N")]
        public int P22N { get; set; }
    }
}
