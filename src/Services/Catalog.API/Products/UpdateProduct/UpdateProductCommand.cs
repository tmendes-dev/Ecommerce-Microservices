namespace Catalog.API.Products.UpdateProduct;

/// <summary>
///     Represents a command to update a product.
/// </summary>
internal readonly record struct UpdateProductCommand : ICommand<UpdateProductCommandResult>
{
    /// <summary>
    ///     Gets or initializes the product information to be updated.
    /// </summary>
    public required Product Product { get; init; }
}