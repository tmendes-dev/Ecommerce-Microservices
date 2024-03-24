namespace Catalog.API.Models;

/// <summary>
/// Represents a product in the catalog.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the categories to which the product belongs.
    /// </summary>
    public List<string> Category { get; set; } = [];

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the file path of the product's image.
    /// </summary>
    public required string ImageFile { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }
}
