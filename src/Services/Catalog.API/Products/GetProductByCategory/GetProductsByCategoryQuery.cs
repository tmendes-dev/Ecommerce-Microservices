namespace Catalog.API.Products.GetProductByCategory;

/// <summary>
///     Represents a query to retrieve products by category.
/// </summary>
internal readonly record struct GetProductsByCategoryQuery : IQuery<GetProductsByCategoryResult>
{
    /// <summary>
    ///     Gets or initializes the category for which products are to be retrieved.
    /// </summary>
    public required string Category { get; init; }
}