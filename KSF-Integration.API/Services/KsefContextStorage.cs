namespace KSF_Integration.API.Services
{
    public class KsefContextStorage
    {
        public string? AuthToken { get; private set; }
        public DateTime? ValidUntil { get; private set; }
        public string? AuthStatus { get; private set; }

        public void SetAuthData(string token, string status, DateTime validUntil)
        {
            AuthToken = token;
            ValidUntil = validUntil;
            AuthStatus = status;
        }
    }
}