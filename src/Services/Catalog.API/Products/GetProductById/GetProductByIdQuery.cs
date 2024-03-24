namespace Catalog.API.Products.GetProductById;

/// <summary>
/// Represents a query to retrieve a product by its identifier.
/// </summary>
public readonly record struct GetProductByIdQuery : IQuery<GetProductByIdResult>
{
    /// <summary>
    /// Gets or initializes the unique identifier of the product to retrieve.
    /// </summary>
    public required Guid ProductId { get; init; }
}