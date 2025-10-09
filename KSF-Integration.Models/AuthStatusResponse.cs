namespace KSF_Integration.Models
{
    public class AuthStatusResponse
    {
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string? Status { get; set; }
    }
}
