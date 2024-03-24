namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Represents the response containing a list of products.
/// </summary>
internal readonly record struct GetProductsResponse
{
    /// <summary>
    /// Gets or initializes the list of products.
    /// </summary>
    public IReadOnlyList<Product> Products { get; init; }
}