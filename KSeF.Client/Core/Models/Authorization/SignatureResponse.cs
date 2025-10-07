using KSeF.Client.Core.Models.Authorization;

namespace KSeF.Client.Core.Models;


public class SignatureResponse
{
    /// <summary>
    /// Numer referencyjny.
    /// </summary>
    public string ReferenceNumber { get; set; }

    /// <summary>
    /// Token uwierzytelniający.
    /// </summary>
    public OperationToken AuthenticationToken { get; set; }

}

