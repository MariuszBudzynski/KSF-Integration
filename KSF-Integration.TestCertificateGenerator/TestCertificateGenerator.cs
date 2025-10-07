using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace KSF_Integration.TestCertificateGenerator
{
    public static class TestCertificateGenerator
    {
        public static void Generate()
        {
            var directoryPath = Path.Combine("..", "..", "..", "..", "KSF-Integration.API", "Data", "Ksef");
            var filePath = Path.Combine(directoryPath, "testcert.pfx");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Created directory: {directoryPath}");
            }

            var subjectName = new X500DistinguishedName("CN=KsefTestCert");
            using var rsa = RSA.Create(2048);

            var request = new CertificateRequest(subjectName, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            request.CertificateExtensions.Add(
                new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, critical: true)
            );

            var cert = request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1));

            var pfxBytes = cert.Export(X509ContentType.Pfx, "test123");
            File.WriteAllBytes(filePath, pfxBytes);

            Console.WriteLine($"Test cert generated at: {filePath}");
        }
    }
}
