namespace KSF_Integration.API.Services.Interfaces
{
    public interface IAuthChallengeService
    {
        Task<string> GetAuthChallengeAsync(HttpClient httpClient, IConfiguration configuration);
    }
}