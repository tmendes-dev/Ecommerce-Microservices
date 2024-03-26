namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Represents a query to retrieve products.
/// </summary>
internal readonly record struct GetProductsQuery : IQuery<GetProductsResult>
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
}

