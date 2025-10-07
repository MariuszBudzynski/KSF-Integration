using KSF_Integration.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSF_Integration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateProcessService _certificateProcessService;
        public CertificateController(ICertificateProcessService certificateProcessService)
        {
            _certificateProcessService = certificateProcessService;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollCertificate()
        {
            try
            {
                await _certificateProcessService.ProcessCertificateAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
