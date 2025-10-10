using KSeF.Client;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.Core.Models.Invoices;
using KSeF.Client.Core.Models.Sessions;
using KSeF.Client.Core.Models.Sessions.OnlineSession;
using KSF_Integration.API.Services.Interfaces;
using System;

namespace KSF_Integration.API.Services
{
    /// <summary>
    /// Provides functionality for preparing, encrypting, and submitting invoices to the KSeF system.
    /// </summary>
    public class KsefInvoiceService : IKsefInvoiceService
    {
        private readonly IKSeFClient _ksefClient;
        private readonly ICryptographyService _cryptographyService;
        private readonly KsefContextStorage _ksefContextStorage;

        private const string DefaultSchemaVersion = "1-0E";
        private const string DefaultFormCodeValue = "FA";

        public KsefInvoiceService(
            IKSeFClient ksefClient,
            ICryptographyService cryptographyService,
            KsefContextStorage ksefContextStorage)
        {
            _ksefClient = ksefClient;
            _cryptographyService = cryptographyService;
            _ksefContextStorage = ksefContextStorage;
        }

        /// <summary>
        /// Processes and sends an invoice to the KSeF system.
        /// </summary>
        public async Task ProcessInvoice(string version)
        {
            var systemCode = version switch
            {
                "FA3" => SystemCodeEnum.FA3,
                _ => SystemCodeEnum.FA2
            };

            // Step 1: Generate symmetric key for encryption.
            // This key will be used to encrypt invoice data before sending it to KSeF.
            var encryptionData = _cryptographyService.GetEncryptionData();

            // Step 2: Open a new online session.
            // Change the invoice type (FA2 / FA3) if necessary.
            var openOnlineSessionResponse = await OpenSessionAsync(encryptionData, systemCode);

            // TODO: Step 3: Encrypt and upload invoice XML to KSeF.

            // Placeholder to keep method async until full implementation is added.
            await Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves the status of a previously submitted invoice from KSeF.
        /// </summary>
        /// <param name="referenceNumber">The unique reference number returned after invoice submission.</param>
        /// <returns>Status string representing the current processing state.</returns>
        public async Task<string> GetInvoiceStatus(string referenceNumber)
        {
            // TODO: Call _ksefClient.GetInvoiceStatusAsync(referenceNumber, accessToken)
            // using the access token stored in _ksefContextStorage.

            await Task.CompletedTask;
            return "Status check not yet implemented.";
        }

        /// <summary>
        /// Opens a new online session in KSeF for the given schema and encryption parameters.
        /// </summary>
        /// <param name="encryptionData">The symmetric encryption data used for secure communication.</param>
        /// <param name="systemCode">The system code (e.g., FA2 or FA3) defining the invoice schema version.</param>
        private async Task<OpenOnlineSessionResponse> OpenSessionAsync(EncryptionData encryptionData, SystemCodeEnum systemCode)
        {
            var accessToken = _ksefContextStorage.AccessToken;

            if (accessToken == null || string.IsNullOrEmpty(accessToken.Token))
                throw new InvalidOperationException("No access token available for KSeF session initialization.");

            var openOnlineSessionRequest = OpenOnlineSessionRequestBuilder
                .Create()
                .WithFormCode(
                    systemCode: SystemCodeHelper.GetValue(systemCode),
                    schemaVersion: DefaultSchemaVersion,
                    value: DefaultFormCodeValue)
                .WithEncryption(
                    encryptedSymmetricKey: encryptionData.EncryptionInfo.EncryptedSymmetricKey,
                    initializationVector: encryptionData.EncryptionInfo.InitializationVector)
                .Build();

            return await _ksefClient.OpenOnlineSessionAsync(openOnlineSessionRequest, accessToken.Token, CancellationToken.None);
        }
    }
}