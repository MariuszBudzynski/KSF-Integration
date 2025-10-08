using KSF_Integration.API.Services;
using KSF_Integration.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSF_Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KsefAuthController : ControllerBase
    {
        private readonly IKsefAuthService _ksefAuthService;
        private readonly KsefContextStorage _ksefContextStorage;

        public KsefAuthController(IKsefAuthService ksefAuthService, KsefContextStorage ksefContextStorage)
        {
            _ksefAuthService = ksefAuthService;
            _ksefContextStorage = ksefContextStorage;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize()
        {
            try
            {
                await _ksefAuthService.AuthenticateAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                if (string.IsNullOrEmpty(_ksefContextStorage.AuthToken))
                {
                    return Ok(new
                    {
                        isAuthenticated = false,
                        message = "No authentication token stored."
                    });
                }

                return Ok(new
                {
                    isAuthenticated = true,
                    token = _ksefContextStorage.AuthToken,
                    validUntil = _ksefContextStorage.ValidUntil,
                    status = _ksefContextStorage.AuthStatus,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
    }
}