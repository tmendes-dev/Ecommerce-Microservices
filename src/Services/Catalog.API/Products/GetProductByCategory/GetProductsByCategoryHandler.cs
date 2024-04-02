namespace Catalog.API.Products.GetProductByCategory;

/// <summary>
///     Handles the retrieval of products by category.
/// </summary>
internal sealed class GetProductsByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    /// <summary>
    ///     Handles the retrieval of products based on the provided category query.
    /// </summary>
    /// <param name="query">The query containing the category for which products are to be retrieved.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result containing the retrieved products matching the category.</returns>
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Product> products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult { Products = products };
    }
}