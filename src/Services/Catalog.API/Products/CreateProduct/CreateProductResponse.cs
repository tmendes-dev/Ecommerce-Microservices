namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Represents a response after creating a product.
/// </summary>
internal readonly record struct CreateProductResponse
{
    /// <summary>
    /// The unique identifier of the created product.
    /// </summary>
    public required Guid Id { get; init; }
}