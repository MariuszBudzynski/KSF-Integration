using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    [XmlRoot("Faktura", Namespace = "http://crd.gov.pl/wzor/2023/06/29/12648/")]
    public class Invoice
    {
        [XmlElement("Naglowek")]
        public Header Header { get; set; }

        [XmlElement("Podmiot1")]
        public Party Seller { get; set; }

        [XmlElement("Podmiot2")]
        public Party Buyer { get; set; }

        [XmlElement("Fa")]
        public InvoiceBody Body { get; set; }
    }
}
