using KSF_Integration.API.Services.Interfaces;
using KSF_Integration.API.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KSF_Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        //refactor everything after it works properly
        private readonly HttpClient _httpClient;
        private readonly IAuthChallengeService _authChallengeService;
        private readonly IAuthTokenRequestBuilder _authTokenRequestBuilder;
        private readonly IConfiguration _configuration;

        public CertificateController(
            IHttpClientFactory httpClientFactory,
            IAuthChallengeService authChallengeService,
            IAuthTokenRequestBuilder authTokenRequestBuilder,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("KsefClient");
            _authChallengeService = authChallengeService;
            _authTokenRequestBuilder = authTokenRequestBuilder;
            _configuration = configuration;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollCertificate()
        {
            //1) get Auth Challenge
            var authChallenge = await _authChallengeService.GetAuthChallengeAsync(_httpClient);
            var challenge = JsonSerializer.Deserialize<AuthChallenge>(
                authChallenge,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            //2) xml generation
            if (challenge == null) return BadRequest();

            var identifier = _configuration["Ksef:ContextIdentifier:Identifier"];
            var xml = _authTokenRequestBuilder.GenerateAuthTokenRequestXml(challenge.Challenge, identifier!);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Ksef", "AuthTokenRequest.xml");
            _authTokenRequestBuilder.SaveToFile(xml, filePath);

            // TODO: Replace with actual implementation logic
            return Ok(authChallenge);

        }

        private sealed record AuthChallenge(string Challenge, string Timestamp);
    }
}
