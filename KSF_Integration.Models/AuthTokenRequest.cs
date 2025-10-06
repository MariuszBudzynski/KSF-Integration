using System.Xml.Serialization;

namespace KSF_Integration.Models
{
    [XmlRoot("AuthTokenRequest", Namespace = "http://ksef.mf.gov.pl/auth/token/2.0")]
    public class AuthTokenRequest
    {
        [XmlElement("Challenge")]
        public string Challenge { get; set; }

        [XmlElement("ContextIdentifier")]
        public ContextIdentifier ContextIdentifier { get; set; }

        [XmlElement("SubjectIdentifierType")]
        public string SubjectIdentifierType { get; set; } = "certificateSubject";
    }

    public class ContextIdentifier
    {
        [XmlElement("Nip")]
        public string Nip { get; set; }
    }
}
