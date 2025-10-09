using KSF_Integration.API.Services;
using KSF_Integration.Models;
using Microsoft.AspNetCore.Mvc;

namespace KSF_Integration.API.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class KsefStatusController : ControllerBase
    {
        private readonly KsefContextStorage _ksefContextStorage;

        public KsefStatusController(KsefContextStorage ksefContextStorage)
        {
            _ksefContextStorage = ksefContextStorage;
        }

        /// <summary>
        /// Returns current authentication status and token info.
        /// </summary>
        /// <response code="200">Returns authentication status information.</response>
        /// <response code="400">If an error occurs during processing.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthStatusResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStatus()
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

                return Ok(new AuthStatusResponse
                {
                    IsAuthenticated = true,
                    ValidUntil = _ksefContextStorage.ValidUntil,
                    Status = _ksefContextStorage.AuthStatus
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}