using KSeF.Client.Core.Models.Invoices;

namespace KSF_Integration.API.Services.Interfaces
{
    public interface IKsefInvoiceService
    {
        Task ProcessInvoice(string version);
    }
}