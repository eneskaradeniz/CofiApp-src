using CofiApp.Domain.Core.Primitives;

namespace CofiApp.Api.Contracts
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;
        public IReadOnlyCollection<Error> Errors { get; set; }
    }
}
