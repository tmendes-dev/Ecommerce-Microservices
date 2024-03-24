namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Represents a command to delete a product.
/// </summary>
internal readonly record struct DeleteProductCommand : ICommand<DeleteProductResult>
{
    /// <summary>
    /// Gets or initializes the unique identifier of the product to be deleted.
    /// </summary>
    public required Guid Id { get; init; }
}