using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById;

internal class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            GetProductByIdResult result = await sender.Send(new GetProductByIdQuery() { ProductId = id });
            GetProductByIdResponse response = result.Adapt<GetProductByIdResponse>();
            return response.Product is null ? Results.NotFound(id) : Results.Ok(response);
        })
        .WithName("GetProducById")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetProducById")
        .WithDescription("GetProducById")
        .WithOpenApi();
    }
}
