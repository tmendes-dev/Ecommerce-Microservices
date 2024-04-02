namespace Catalog.API.Products.CreateProduct;

/// <summary>
///     Represents the result of creating a product.
/// </summary>
internal readonly record struct CreateProductResult
{
    /// <summary>
    ///     The unique identifier of the created product.
    /// </summary>
    public required Guid Id { get; init; }
}