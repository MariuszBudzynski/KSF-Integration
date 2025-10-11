using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class InvoiceLine
    {
        [XmlElement("NrWierszaFa")]
        public int LineNumber { get; set; }

        [XmlElement("UU_ID")]
        public string UUID { get; set; }

        [XmlElement("P_7")]
        public string ItemName { get; set; }

        [XmlElement("P_8A")]
        public string Unit { get; set; }

        [XmlElement("P_8B")]
        public decimal Quantity { get; set; }

        [XmlElement("P_9A")]
        public decimal UnitPrice { get; set; }

        [XmlElement("P_11")]
        public decimal NetValue { get; set; }

        [XmlElement("P_12")]
        public int VatRate { get; set; }
    }
}
