using CofiApp.Application.Abstractions.Common;
using CofiApp.Domain.Core.Primitives.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CofiApp.Application.Core.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
         where TResponse : Result
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly IDateTime _dateTime;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger, IDateTime dateTime)
        {
            _logger = logger;
            _dateTime = dateTime;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                _dateTime.UtcNow);

            var result = await next();

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Failed request {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error,
                    _dateTime.UtcNow);
            }

            _logger.LogInformation(
                    "Completed request {@RequestName}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    _dateTime.UtcNow);

            return result;
        }
    }
}
