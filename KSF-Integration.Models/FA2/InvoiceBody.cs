using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class InvoiceBody
    {
        [XmlElement("KodWaluty")]
        public string CurrencyCode { get; set; }

        [XmlElement("P_1")]
        public DateTime InvoiceDate { get; set; }

        [XmlElement("P_1M")]
        public string PlaceOfIssue { get; set; }

        [XmlElement("P_2")]
        public string InvoiceNumber { get; set; }

        [XmlElement("P_6")]
        public DateTime SaleDate { get; set; }

        [XmlElement("P_13_1")]
        public decimal NetAmount { get; set; }

        [XmlElement("P_14_1")]
        public decimal VatAmount { get; set; }

        [XmlElement("P_15")]
        public decimal GrossAmount { get; set; }

        [XmlElement("Adnotacje")]
        public Annotations Annotations { get; set; }

        [XmlElement("RodzajFaktury")]
        public string InvoiceType { get; set; }

        [XmlElement("FaWiersz")]
        public List<InvoiceLine> Lines { get; set; }

        [XmlElement("Platnosc")]
        public Payment Payment { get; set; }
    }
}
