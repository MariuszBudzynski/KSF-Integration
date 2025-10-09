using KSeF.Client.Core.Models.Authorization;

namespace KSF_Integration.API.Services
{
    public class KsefContextStorage
    {
        public string? AuthToken { get; private set; }
        public DateTime? ValidUntil { get; private set; }
        public string? AuthStatus { get; private set; }
        public TokenInfo? AccessToken { get; private set; }
        public TokenInfo? RefreshToken { get; private set; }

        public void SetAuthData(
            string token,
            string status,
            DateTime validUntil,
            AuthOperationStatusResponse authOperationtoken)
        {
            AuthToken = token;
            ValidUntil = validUntil;
            AuthStatus = status;
            AccessToken = authOperationtoken.AccessToken;
            RefreshToken = authOperationtoken.RefreshToken;
        }

        public void RefreshAuthData(RefreshTokenResponse refreshedAccessTokenResponse)
        {
            AccessToken = refreshedAccessTokenResponse.AccessToken;
        }
    }
}