namespace Catalog.API.Products.CreateProduct;

/// <summary>
///     Represents an endpoint for creating a new product.
/// </summary>
internal sealed class CreateProductEndpoint : ICarterModule
{
    /// <summary>
    ///     Adds routes for handling requests to create a new product.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                CreateProductCommand command = request.Adapt<CreateProductCommand>();
                CreateProductResult result = await sender.Send(command);
                CreateProductResponse response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/{ProductsConstants.EndpointPrefix}/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Handles requests to create a new product.")
            .WithOpenApi();
    }
}