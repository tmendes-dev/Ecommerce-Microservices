namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Represents a command to create a product.
/// </summary>
internal record CreateProductCommand : ICommand<CreateProductResult>
{
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


