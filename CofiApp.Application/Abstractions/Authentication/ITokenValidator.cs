using CofiApp.Domain.Core.Primitives.Maybe;

namespace CofiApp.Application.Abstractions.Authentication
{
    public interface ITokenValidator
    {
        Maybe<Guid> ValidateEmailVerifyToken(string token);
    }
}
