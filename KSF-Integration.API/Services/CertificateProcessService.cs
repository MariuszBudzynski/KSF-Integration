using KSeF.Client;
using KSeF.Client.Api.Builders.Auth;
using KSeF.Client.Api.Builders.X509Certificates;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.Core.Models.Authorization;
using KSF_Integration.API.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace KSF_Integration.API.Services
{
    public class CertificateProcessService : ICertificateProcessService
    {
        private readonly IKSeFClient _ksefClient;
        private readonly ISignatureService _signatureService;

        public CertificateProcessService(
          IKSeFClient ksefClient, ISignatureService signatureService)
        {
            _ksefClient = ksefClient;
            _signatureService = signatureService;
        }

        // refactor after
        public async Task ProcessCertificateAsync()
        {
            // Step 1: Request authentication challenge from KSeF
            var challengeResponse = await _ksefClient.GetAuthChallengeAsync();

            // Step 2: Generate the document 
            var authTokenRequest = AuthTokenRequestBuilder
           .Create()
           .WithChallenge(challengeResponse.Challenge)
           .WithContext(ContextIdentifierType.Nip, "1234567890")
           .WithIdentifierType(SubjectIdentifierTypeEnum.CertificateSubject)
           .Build();

            // Step 3: Generate the test certificate
            var ownerCertificate = GetPersonalCertificate("Jan", "Kowalski", "TINPL", "1234567890", "M B");

            // Step 4: Sign in the document
            var xml = AuthTokenRequestSerializer.SerializeToXmlString(authTokenRequest);
            var signedXml = _signatureService.Sign(xml, ownerCertificate);

            //Step 5: Send signed document
            var authOperationInfo = await _ksefClient.SubmitXadesAuthRequestAsync(signedXml, verifyCertificateChain: false);
        }

        private static X509Certificate2 GetPersonalCertificate(
        string givenName,
        string surname,
        string serialNumberPrefix,
        string serialNumber,
        string commonName)
        {
            X509Certificate2 certificate = SelfSignedCertificateForSignatureBuilder
                        .Create()
                        .WithGivenName(givenName)
                        .WithSurname(surname)
                        .WithSerialNumber($"{serialNumberPrefix}-{serialNumber}")
                        .WithCommonName(commonName)
                        .Build();
            return certificate;
        }
    }
}