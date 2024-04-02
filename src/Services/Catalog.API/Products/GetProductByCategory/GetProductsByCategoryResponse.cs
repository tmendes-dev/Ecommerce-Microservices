namespace Catalog.API.Products.GetProductByCategory;

/// <summary>
///     Represents the result of retrieving products by category.
/// </summary>
internal readonly record struct GetProductsByCategoryResponse
{
    /// <summary>
    ///     Gets or initializes the list of products matching the specified category.
    /// </summary>
    public IReadOnlyList<Product> Products { get; init; }
}