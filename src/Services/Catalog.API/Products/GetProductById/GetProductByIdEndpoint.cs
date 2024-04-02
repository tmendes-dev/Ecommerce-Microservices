namespace Catalog.API.Products.GetProductById;

/// <summary>
///     Represents an endpoint for retrieving product information by its unique identifier.
/// </summary>
internal sealed class GetProductByIdEndpoint : ICarterModule
{
    /// <summary>
    ///     Adds routes for handling requests to retrieve product information by ID.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery { Id = id });
                var response = result.Adapt<GetProductByIdResponse>();
                return response.Product is null ? Results.NotFound(id) : Results.Ok(response);
            })
            .WithName("GetProductById")
            .WithSummary("Retrieve Product by ID")
            .WithDescription("Handles requests to retrieve product information by its unique identifier.")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi();
    }
}