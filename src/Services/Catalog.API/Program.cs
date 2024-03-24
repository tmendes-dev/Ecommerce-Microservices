using BuildingBlocks.Behaviours;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
Assembly programAsm = typeof(Program).Assembly;

builder.Services.AddMarten(options => options.Connection(builder.Configuration.GetConnectionString("Database")!)).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(programAsm);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(programAsm);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
builder.Services.AddCarter(configurator: c =>
{
    c.WithModule<CreateProductEndpoint>();
    c.WithModule<GetProductsEndpoint>();
    c.WithModule<GetProductByIdEndpoint>();
    c.WithModule<GetProductsByCategoryEndpoint>();
    c.WithModule<UpdateProductEndpoint>();
    c.WithModule<DeleteProductEndpoint>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", info: new()
{
    Version = "v1",
    Title = "Catalog API",
    Description = "An ASP.NET Core Web API for managing products items"
}));
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();
app.MapCarter();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();
