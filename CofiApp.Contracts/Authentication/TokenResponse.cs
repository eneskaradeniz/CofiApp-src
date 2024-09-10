namespace CofiApp.Contracts.Authentication
{
    public class TokenResponse
    {
        public TokenResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }
        public string RefreshToken { get; set; }
    }
}
