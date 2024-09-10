namespace CofiApp.Application.Abstractions.Authentication
{
    public interface ITokenProvider
    {
        Task<string> CreateAccessTokenAsync(Guid userId);
        string CreateBase64Token();
    }
}
