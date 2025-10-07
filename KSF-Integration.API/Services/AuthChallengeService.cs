using KSF_Integration.API.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace KSF_Integration.API.Services
{
    public class AuthChallengeService : IAuthChallengeService
    {
        public async Task<string> GetAuthChallengeAsync(HttpClient httpClient, IConfiguration configuration)
        {
            var type = configuration["Ksef:ContextIdentifier:Type"];
            var identifier = configuration["Ksef:ContextIdentifier:Identifier"];
            var userAgent = configuration["Ksef:Headers:UserAgent"];
            var acceptLanguage = configuration["Ksef:Headers:AcceptLanguage"];
            var cacheControl = configuration["Ksef:Headers:CacheControl"];

            var request = new HttpRequestMessage(HttpMethod.Post, "api/v2/auth/challenge");

            //fake data
            var payload = new
            {
                contextIdentifier = new
                {
                    type,
                    identifier
                }
            };

            var json = JsonSerializer.Serialize(payload);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.UserAgent.ParseAdd(userAgent);
            request.Headers.Add("Accept-Language", acceptLanguage);
            request.Headers.Add("Cache-Control", cacheControl);


            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
