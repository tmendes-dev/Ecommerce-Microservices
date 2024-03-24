using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours;
/// <summary>
/// Represents a behavior for logging requests and responses in the MediatR pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>
public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull,
    IRequest<TResponse> where TResponse : notnull
{
    /// <summary>
    /// Represents the maximum acceptable response time in seconds.
    /// </summary>
    private const int MAX_ACCEPTABLE_RESPONSE_TIME = 3;

    /// <summary>
    /// Handles the request by logging information before and after invoking the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="next">The delegate representing the next step in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response returned by the handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={Request}", typeof(TRequest).Name, typeof(TResponse).Name, request);

        Stopwatch timer = new();
        timer.Start();

        TResponse response = await next();

        timer.Stop();
        TimeSpan timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > MAX_ACCEPTABLE_RESPONSE_TIME)
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}", typeof(TRequest).Name, timeTaken.Seconds);

        logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, response);

        return response;
    }
}
