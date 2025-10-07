namespace KSF_Integration.API.Servises.Interfaces
{
    public interface IAuthChallengeService
    {
        Task<string> GetAuthChallengeAsync(HttpClient httpClient, IConfiguration configuration);
    }
}