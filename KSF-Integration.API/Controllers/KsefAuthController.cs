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

        public KsefAuthController(
            IKsefAuthService ksefAuthService,
            KsefContextStorage ksefContextStorage)
        {
            _ksefAuthService = ksefAuthService;
        }

        /// <summary>
        /// Performs authentication with KSeF and retrieves an access token.
        /// </summary>
        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize()
        {
            try
            {
                await _ksefAuthService.AuthenticateAsync();
                return Ok(new { message = "Authorization successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Refreshes the current access token using the stored refresh token.
        /// </summary>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                await _ksefAuthService.RefreshAcessToken();
                return Ok(new { message = "Access token refreshed successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}