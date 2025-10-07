using KSF_Integration.API.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace KSF_Integration.API.Services
{
    public class XadesSignService : IXadesSignService
    {
        public void SignAuthTokenRequest(string xmlPath, string outputPath, X509Certificate2 certificate)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlPath);

            // Create a SignedXml object
            var signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = certificate.GetRSAPrivateKey();

            // Create a reference to be signed
            var reference = new Reference();
            reference.Uri = "";
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

            // Add the reference to the SignedXml object
            signedXml.AddReference(reference);

            // Add key info (cert)
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));
            signedXml.KeyInfo = keyInfo;

            // Compute the signature
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save it
            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement?.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            xmlDoc.Save(outputPath);
        }
    }
}
