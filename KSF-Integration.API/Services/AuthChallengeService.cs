using KSF_Integration.API.Servises.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace KSF_Integration.API.Servises
{
    public class AuthChallengeService : IAuthChallengeService
    {
        private readonly IConfiguration _configuration;
        public AuthChallengeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetAuthChallengeAsync(HttpClient _httpClient)
        {
            var type = _configuration["Ksef:ContextIdentifier:Type"];
            var identifier = _configuration["Ksef:ContextIdentifier:Identifier"];
            var userAgent = _configuration["Ksef:Headers:UserAgent"];
            var acceptLanguage = _configuration["Ksef:Headers:AcceptLanguage"];
            var cacheControl = _configuration["Ksef:Headers:CacheControl"];

            var request = new HttpRequestMessage(HttpMethod.Post, "api/v2/auth/challenge");

            //fake data
            var payload = new
            {
                contextIdentifier = new
                {
                    type = type,
                    identifier = identifier
                }
            };

            var json = JsonSerializer.Serialize(payload);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.UserAgent.ParseAdd(userAgent);
            request.Headers.Add("Accept-Language", acceptLanguage);
            request.Headers.Add("Cache-Control", cacheControl);


            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
