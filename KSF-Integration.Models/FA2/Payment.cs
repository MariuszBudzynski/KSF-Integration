using System.Xml.Serialization;

namespace KSF_Integration.Models.FA2
{
    public class Payment
    {
        [XmlElement("Zaplacono")]
        public int Paid { get; set; }

        [XmlElement("DataZaplaty")]
        public DateTime PaymentDate { get; set; }

        [XmlElement("FormaPlatnosci")]
        public int PaymentForm { get; set; }
    }
}