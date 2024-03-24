namespace Catalog.API.Products.GetProducts;

/// <summary>
/// Represents an endpoint for retrieving a list of products.
/// </summary>
internal class GetProductsEndpoint : ICarterModule
{
    /// <summary>
    /// Adds routes for handling requests to retrieve a list of products.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            GetProductsResult result = await sender.Send(new GetProductsQuery());
            GetProductsResponse response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Handles requests to retrieve a list of products.")
        .WithOpenApi();
    }
}