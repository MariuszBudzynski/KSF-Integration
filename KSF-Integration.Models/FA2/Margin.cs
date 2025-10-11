using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Margin
    {
        [XmlElement("P_PMarzyN")]
        public int PPMarzyN { get; set; }
    }
}
