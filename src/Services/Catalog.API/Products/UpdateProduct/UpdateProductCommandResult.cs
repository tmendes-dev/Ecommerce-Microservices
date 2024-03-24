namespace Catalog.API.Products.UpdateProduct;

/// <summary>
/// Represents the result of updating a product.
/// </summary>
internal readonly record struct UpdateProductCommandResult
{
    /// <summary>
    /// Gets or initializes the updated product.
    /// </summary>
    public required Product Product { get; init; }
}