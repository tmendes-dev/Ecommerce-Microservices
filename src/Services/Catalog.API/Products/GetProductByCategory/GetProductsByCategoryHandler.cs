

namespace Catalog.API.Products.GetProductByCategory;
/// <summary>
/// Handles the retrieval of products by category.
/// </summary>
internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    /// <summary>
    /// Handles the retrieval of products based on the provided category query.
    /// </summary>
    /// <param name="query">The query containing the category for which products are to be retrieved.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result containing the retrieved products matching the category.</returns>
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsByCategoryHandler.Handle called with {@Query}", query);
        IReadOnlyList<Product> products = await session.Query<Product>()
                                            .Where(p => p.Category.IndexOf(query.Category, 0, (int)StringComparison.OrdinalIgnoreCase) != -1)
                                            .ToListAsync();

        return new() { Products = products };
    }
}