using MediatR;

namespace BuildingBlocks.CQRS;
/// <summary>
/// Represents a query that returns a response.
/// </summary>
/// <typeparam name="TResponse">The type of response returned by the query.</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}