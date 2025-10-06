namespace KSF_Integration.API.Services.Interfaces
{
    public interface IAuthTokenRequestBuilder
    {
        string GenerateAuthTokenRequestXml(string challenge, string nip);
        void SaveToFile(string xmlContent, string filePath);
    }
}