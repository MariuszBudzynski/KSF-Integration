using System.Security.Cryptography.X509Certificates;

namespace KSF_Integration.API.Services.Interfaces
{
    public interface ISignService
    {
        void SignAuthTokenRequest(string xmlPath, string outputPath, X509Certificate2 certificate);
    }
}