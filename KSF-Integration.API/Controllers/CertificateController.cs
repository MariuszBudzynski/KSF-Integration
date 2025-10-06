using KSF_Integration.API.Servises.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSF_Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        //refactor everything after
        private readonly HttpClient _httpClient;
        private readonly IAuthChallengeService _authChallengeService;

        public CertificateController(IHttpClientFactory httpClientFactory, IAuthChallengeService authChallengeService)
        {
            _httpClient = httpClientFactory.CreateClient("KsefClient");
            _authChallengeService = authChallengeService;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollCertificate()
        {
            //1) get Auth Challenge
            var authChallenge = await _authChallengeService.GetAuthChallengeAsync(_httpClient);

            // TODO: Replace with actual implementation logic
            return Ok(authChallenge);

        }
    }
}
