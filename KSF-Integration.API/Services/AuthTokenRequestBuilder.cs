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
                Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true), // ✅ BOM enabled
                OmitXmlDeclaration = false
            };

            using var stringWriter = new StringWriterWithEncoding(settings.Encoding);
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            xmlSerializer.Serialize(xmlWriter, request);
            return stringWriter.ToString();
        }

        public void SaveToFile(string xmlContent, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            File.WriteAllText(filePath, xmlContent, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)); // ✅ BOM enabled
        }
    }

    //Custom StringWriter that respects encoding
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding => _encoding;
    }
}