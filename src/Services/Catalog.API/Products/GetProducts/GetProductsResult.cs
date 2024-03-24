namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Represents the result of retrieving products.
/// </summary>
internal readonly record struct GetProductsResult
{
    /// <summary>
    /// Gets or initializes the list of products.
    /// </summary>
    public required IReadOnlyList<Product> Products { get; init; }
}