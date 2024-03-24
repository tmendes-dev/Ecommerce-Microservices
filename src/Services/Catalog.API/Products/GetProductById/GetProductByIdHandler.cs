namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Handles the retrieval of a product by its identifier.
/// </summary>
internal sealed class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    /// <summary>
    /// Handles the retrieval of a product based on the provided query.
    /// </summary>
    /// <param name="query">The query containing the product identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result containing the retrieved product.</returns>
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdHandler.Handle called with {@Query}", JsonSerializer.Serialize(query));
        Product? product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
        return new GetProductByIdResult() { Product = product };
    }
}
