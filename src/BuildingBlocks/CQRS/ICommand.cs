using MediatR;

namespace BuildingBlocks.CQRS;
/// <summary>
/// Represents a command without a response.
/// </summary>
public interface ICommand : ICommand<Unit>
{
}

/// <summary>
/// Represents a command with a response.
/// </summary>
/// <typeparam name="TResponse">The type of response returned by the command.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
