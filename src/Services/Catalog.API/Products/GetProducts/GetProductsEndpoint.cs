namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Represents an endpoint for retrieving a list of products.
/// </summary>
internal sealed class GetProductsEndpoint : ICarterModule
{
    /// <summary>
    /// Adds routes for handling requests to retrieve a list of products.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            GetProductsQuery query = request.Adapt<GetProductsQuery>();
            GetProductsResult result = await sender.Send(query);
            GetProductsResponse response = new() { Products = result.Products };
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Handles requests to retrieve a list of products.")
        .WithOpenApi();
    }
}