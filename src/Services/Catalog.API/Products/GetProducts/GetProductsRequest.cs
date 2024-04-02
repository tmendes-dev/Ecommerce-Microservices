namespace Catalog.API.Products.GetProducts;

/// <summary>
///     Represents the request structure for retrieving products.
/// </summary>
internal readonly record struct GetProductsRequest
{
    /// <summary>
    ///     Gets or initializes the page number for pagination.
    /// </summary>
    public required int PageNumber { get; init; }

    /// <summary>
    ///     Gets or initializes the page size for pagination.
    /// </summary>
    public required int PageSize { get; init; }
}