using KSF_Integration.API.Services.Interfaces;
using KSF_Integration.API.Servises.Interfaces;
using System.Text.Json;

namespace KSF_Integration.API.Services
{
    public class CertificateProcessService : ICertificateProcessService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthChallengeService _authChallengeService;
        private readonly IAuthTokenRequestBuilder _authTokenRequestBuilder;
        private readonly IConfiguration _configuration;

        public CertificateProcessService(
            IHttpClientFactory httpClientFactory,
            IAuthChallengeService authChallengeService,
            IAuthTokenRequestBuilder authTokenRequestBuilder,
            IConfiguration configuration
            )
        {
            _httpClient = httpClientFactory.CreateClient("KsefClient");
            _authChallengeService = authChallengeService;
            _authTokenRequestBuilder = authTokenRequestBuilder;
            _configuration = configuration;
        }

        public async Task ProcessCertificateAsync()
        {
            //1) get Auth Challenge
            var authChallenge = await _authChallengeService.GetAuthChallengeAsync(_httpClient, _configuration);
            var challenge = JsonSerializer.Deserialize<AuthChallenge>(
                authChallenge,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (challenge == null)
                throw new InvalidOperationException("Received data is null or does not match the expected format.");

            //2) xml generation
            var identifier = _configuration["Ksef:ContextIdentifier:Identifier"];
            var xml = _authTokenRequestBuilder.GenerateAuthTokenRequestXml(challenge.Challenge, identifier!);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Ksef", "AuthTokenRequest.xml");
            _authTokenRequestBuilder.SaveToFile(xml, filePath);

            // TODO: Replace with actual implementation logic
        }

        private sealed record AuthChallenge(string Challenge, string Timestamp);
    }
}
