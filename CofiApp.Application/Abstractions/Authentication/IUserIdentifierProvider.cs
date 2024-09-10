namespace CofiApp.Application.Abstractions.Authentication
{
    public interface IUserIdentifierProvider
    {
        Guid UserId { get; }
    }
}
