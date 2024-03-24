using MediatR;

namespace BuildingBlocks.CQRS;
/// <summary>
/// Represents a handler for queries without a response.
/// </summary>
public interface IQueryHandler
{
}

/// <summary>
/// Represents a handler for queries without a response.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
public interface IQueryHandler<in TQuery> : IQueryHandler<TQuery, Unit> where TQuery : IQuery<Unit>
{
}

/// <summary>
/// Represents a handler for queries with a response.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the query.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
