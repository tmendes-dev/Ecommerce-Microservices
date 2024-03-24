namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Represents the result of a delete command.
/// </summary>
internal readonly record struct DeleteProductResult
{
    /// <summary>
    /// Gets or initializes a value indicating whether the delete operation was successful.
    /// </summary>
    public required bool Success { get; init; }
}
