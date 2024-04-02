namespace Catalog.API.Products.DeleteProduct;

/// <summary>
///     Represents the result of a delete request.
/// </summary>
internal readonly record struct DeleteProductResponse
{
    /// <summary>
    ///     Gets or initializes a value indicating whether the delete operation was successful.
    /// </summary>
    public required bool Success { get; init; }
}