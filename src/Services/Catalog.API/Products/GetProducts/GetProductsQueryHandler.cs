﻿
namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Handles the retrieval of products based on the provided query.
/// </summary>
internal sealed class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    /// <summary>
    /// Handles the retrieval of products based on the provided query.
    /// </summary>
    /// <param name="query">The query containing parameters for retrieving products.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result containing the retrieved products.</returns>
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new() { Products = products };
    }
}
