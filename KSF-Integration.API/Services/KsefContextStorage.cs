namespace KSF_Integration.API.Services
{
    public class KsefContextStorage
    {
        public string? AuthToken { get; private set; }
        public DateTime? ValidUntil { get; private set; }

        public void SetAuthData(string token, DateTime validUntil)
        {
            AuthToken = token;
            ValidUntil = validUntil;
        }
    }
}