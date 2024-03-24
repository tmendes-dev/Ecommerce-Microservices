namespace Catalog.API.Products.UpdateProduct;
/// <summary>
/// Represents a request to update a product.
/// </summary>
internal sealed record UpdateProductRequest
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public required Guid Id { get; init; }
    /// <summary>
    /// The name of the product.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The categories to which the product belongs.
    /// </summary>
    public required List<string> Category { get; init; }
    /// <summary>
    /// The description of the product.
    /// </summary>
    public required string Description { get; init; }
    /// <summary>
    /// The image file of the product.
    /// </summary>
    public string ImageFile { get; init; } = default!;
    /// <summary>
    /// The price of the product.
    /// </summary>
    public required decimal Price { get; init; }
}
