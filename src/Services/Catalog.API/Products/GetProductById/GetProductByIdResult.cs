namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Represents the result of retrieving a product by its identifier.
/// </summary>
internal readonly record struct GetProductByIdResult
{
    /// <summary>
    /// Gets or initializes the retrieved product.
    /// </summary>
    public Product? Product { get; init; }
}