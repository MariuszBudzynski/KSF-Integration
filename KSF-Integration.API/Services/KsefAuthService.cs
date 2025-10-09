using KSeF.Client;
using KSeF.Client.Api.Builders.Auth;
using KSeF.Client.Api.Builders.X509Certificates;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.Core.Models.Authorization;
using KSF_Integration.API.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace KSF_Integration.API.Services
{
    public class KsefAuthService : IKsefAuthService
    {
        private readonly IKSeFClient _ksefClient;
        private readonly ISignatureService _signatureService;
        private readonly KsefContextStorage _ksefContextStorage;

        public KsefAuthService(
            IKSeFClient ksefClient,
            ISignatureService signatureService,
            KsefContextStorage ksefContextStorage)
        {
            _ksefClient = ksefClient;
            _signatureService = signatureService;
            _ksefContextStorage = ksefContextStorage;
        }

        /// <summary>
        /// Performs full authentication flow against KSeF API.
        /// </summary>
        public async Task AuthenticateAsync()
        {
            // Step 1: Request authentication challenge from KSeF
            var challengeResponse = await _ksefClient.GetAuthChallengeAsync();

            // Step 2: Build authentication request document
            var authTokenRequest = AuthTokenRequestBuilder
                .Create()
                .WithChallenge(challengeResponse.Challenge)
                .WithContext(ContextIdentifierType.Nip, "1234567890")
                .WithIdentifierType(SubjectIdentifierTypeEnum.CertificateSubject)
                .Build();

            // Step 3: Generate a self-signed certificate (used for signing)
            var userCertificate = GetPersonalCertificate("Jan", "Kowalski", "TINPL", "1234567890", "M B");

            // Step 4: Sign the authentication request XML document
            var xml = AuthTokenRequestSerializer.SerializeToXmlString(authTokenRequest);
            var signedXml = _signatureService.Sign(xml, userCertificate);

            // Step 5: Submit the signed authentication document to KSeF
            var authOperationInfo = await _ksefClient.SubmitXadesAuthRequestAsync(signedXml, verifyCertificateChain: false);

            // Step 6: Check the authorization status
            var status = await _ksefClient.GetAuthStatusAsync(
                authOperationInfo.ReferenceNumber,
                authOperationInfo.AuthenticationToken.Token);

            if (status.Status.Code != 200)
            {
                throw new InvalidOperationException("Authorization failed");
            }

            // Step 7: Retrieve the access and refresh tokens
            var tokenResponse = await _ksefClient.GetAccessTokenAsync(authOperationInfo.AuthenticationToken.Token);

            _ksefContextStorage.SetAuthData(
                authOperationInfo.AuthenticationToken.Token,
                status.Status.Description,
                authOperationInfo.AuthenticationToken.ValidUntil,
                tokenResponse);
        }

        /// <summary>
        /// Refreshes the existing access token using the stored refresh token.
        /// </summary>
        public async Task RefreshAcessToken()
        {
            var refreshToken = _ksefContextStorage.RefreshToken;

            if (refreshToken == null || string.IsNullOrEmpty(refreshToken.Token))
            {
                throw new InvalidOperationException("No refresh token available.");
            }

            var refreshedResponse = await _ksefClient.RefreshAccessTokenAsync(refreshToken.Token);

            _ksefContextStorage.RefreshAuthData(refreshedResponse);
        }

        /// <summary>
        /// Generates a self-signed certificate for authentication purposes.
        /// </summary>
        private static X509Certificate2 GetPersonalCertificate(
            string givenName,
            string surname,
            string serialNumberPrefix,
            string serialNumber,
            string commonName)
        {
            return SelfSignedCertificateForSignatureBuilder
                .Create()
                .WithGivenName(givenName)
                .WithSurname(surname)
                .WithSerialNumber($"{serialNumberPrefix}-{serialNumber}")
                .WithCommonName(commonName)
                .Build();
        }
    }
}