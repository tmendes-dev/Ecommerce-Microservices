namespace Catalog.API.Products.UpdateProduct;

/// <summary>
/// Represents the response after updating a product.
/// </summary>
internal readonly record struct UpdateProductResponse
{
    /// <summary>
    /// Gets or initializes the updated product information.
    /// </summary>
    public required Product Product { get; init; }
}

