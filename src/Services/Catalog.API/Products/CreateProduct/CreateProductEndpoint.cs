namespace Catalog.API.Products.CreateProduct;

internal class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>();
            CreateProductResult result = await sender.Send(command);
            CreateProductResponse response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/{ProductsConstants.ENDPOINT_PREFIX}/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("CreateProduct")
        .WithDescription("CreateProduct")
        .WithOpenApi();
    }
}
