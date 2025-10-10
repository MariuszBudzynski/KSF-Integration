using KSeF.Client.Core.Models.Invoices;
using KsefApi.Model;
using KSF_Integration.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KSF_Integration.API.Controllers
{
    /// <summary>
    /// Handles operations related to invoices within the KSeF system.
    /// Includes sending invoices and checking their processing status.
    /// </summary>
    [ApiController]
    [Route("api/invoices")]
    public class KsefInvoicesController : ControllerBase
    {
        private readonly IKsefInvoiceService _ksefInvoiceService;

        public KsefInvoicesController(IKsefInvoiceService ksefInvoiceService)
        {
            _ksefInvoiceService = ksefInvoiceService;
        }

        /// <summary>
        /// Sends an invoice(FA2) to KSeF for processing.
        /// </summary>
        /// <returns>Returns success message or error details.</returns>
        [HttpPost("send/FA2")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendInvoiceFA2()
        {
            try
            {
                // TODO: Later add invoice JSON model as [FromBody] parameter.
                await _ksefInvoiceService.ProcessInvoice("FA2");

                return Ok(new
                {
                    message = "Invoice processed successfully."
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

        /// <summary>
        /// Sends an invoice(FA3) to KSeF for processing.
        /// </summary>
        /// <returns>Returns success message or error details.</returns>
        [HttpPost("send/FA3")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendInvoiceFA3()
        {
            try
            {
                // TODO: Later add invoice JSON model as [FromBody] parameter.
                await _ksefInvoiceService.ProcessInvoice("FA3");

                return Ok(new
                {
                    message = "Invoice processed successfully."
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

        /// <summary>
        /// Retrieves the processing status of a previously submitted invoice.
        /// </summary>
        /// <param name="referenceNumber">Unique reference number returned after invoice submission.</param>
        /// <returns>Invoice status or error details.</returns>
        [HttpGet("status/{referenceNumber}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInvoiceStatus(string referenceNumber)
        {
            try
            {
                // TODO: Replace with actual logic when implemented in service

                return Ok(new
                {
                    //referenceNumber,
                    //status
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