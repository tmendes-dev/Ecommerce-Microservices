using BuildingBlocks.Behaviours;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var programAsm = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddHealthChecks().AddNpgSql(dbConnectionString);
builder.Services.AddMarten(options => options.Connection(dbConnectionString)).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(programAsm);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(programAsm);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddCarter(configurator: _ => { });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Basket API",
    Description = "An ASP.NET Core Web API for managing basket"
}));

var app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
            return;
        ProblemDetails problemDetails = new()
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };
        var logger = context.RequestServices.GetService<ILogger<Program>>()!;
        logger.LogError(exception, exception.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
app.MapCarter();
app.UseHealthChecks("/health", options: new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();