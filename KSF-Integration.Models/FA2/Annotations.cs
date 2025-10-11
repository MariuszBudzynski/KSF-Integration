using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Annotations
    {
        [XmlElement("P_16")]
        public int P16 { get; set; }

        [XmlElement("P_17")]
        public int P17 { get; set; }

        [XmlElement("P_18")]
        public int P18 { get; set; }

        [XmlElement("P_18A")]
        public int P18A { get; set; }

        [XmlElement("Zwolnienie")]
        public Exemption Exemption { get; set; }

        [XmlElement("NoweSrodkiTransportu")]
        public NewMeansOfTransport NewTransport { get; set; }

        [XmlElement("P_23")]
        public int P23 { get; set; }

        [XmlElement("PMarzy")]
        public Margin Margin { get; set; }
    }
}
