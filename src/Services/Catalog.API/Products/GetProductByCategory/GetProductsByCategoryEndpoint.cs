namespace Catalog.API.Products.GetProductByCategory;

/// <summary>
/// Represents an endpoint for retrieving products by category.
/// </summary>
internal class GetProductsByCategoryEndpoint : ICarterModule
{
    /// <summary>
    /// Adds routes for handling requests to retrieve products by category.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{category}", async (string category, ISender sender) =>
        {
            GetProductsByCategoryResult result = await sender.Send(new GetProductsByCategoryQuery() { Category = category });
            GetProductsByCategoryResponse response = result.Adapt<GetProductsByCategoryResponse>();
            return response.Products is null || !response.Products.Any() ? Results.NotFound(category) : Results.Ok(response);
        })
        .WithName("GetProductsByCategory")
        .WithSummary("Retrieve Products by Category")
        .WithDescription("Handles requests to retrieve products by their category.")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi();
    }
}