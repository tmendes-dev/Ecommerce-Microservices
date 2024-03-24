using BuildingBlocks.CQRS;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BuildingBlocks.Behaviours;
/// <summary>
/// Represents a pipeline behavior for request validation using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">The type of request being validated.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    /// <summary>
    /// Handles the request by validating it against the registered validators.
    /// </summary>
    /// <param name="request">The request to be validated.</param>
    /// <param name="next">The delegate representing the next step in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response returned by the handler.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);
        ValidationResult[] validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        IReadOnlyList<ValidationFailure> failures = validationResult.Where(v => v.Errors.Count != 0).SelectMany(r => r.Errors).ToList();
        if (failures.Count != 0)
            throw new ValidationException(failures);
        return await next();
    }
}
