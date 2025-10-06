using KSF_Integration.API.Services.Interfaces;
using KSF_Integration.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KSF_Integration.API.Services
{
    public class AuthTokenRequestBuilder : IAuthTokenRequestBuilder
    {
        public string GenerateAuthTokenRequestXml(string challenge, string nip)
        {
            var request = new AuthTokenRequest
            {
                Challenge = challenge,
                ContextIdentifier = new ContextIdentifier { Nip = nip },
                SubjectIdentifierType = "certificateSubject"
            };

            var xmlSerializer = new XmlSerializer(typeof(AuthTokenRequest));
            var settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(false),
                OmitXmlDeclaration = false
            };

            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            xmlSerializer.Serialize(xmlWriter, request);
            return stringWriter.ToString();
        }

        public void SaveToFile(string xmlContent, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, xmlContent, Encoding.UTF8);
        }
    }
}
