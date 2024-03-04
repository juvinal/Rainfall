using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Rainfall.Api.Extensions;
using Rainfall.Api.Middlewares;
using Rainfall.Core;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Rainfall Api", // Set the desired title here
        Description = "An API that provides rainfall reading data",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new Uri("https://www.sorted.com"),
        },
    });

    opt.AddServer(new OpenApiServer { Url = "https://localhost:3000/", Description = "Rainfall Api" });
});
builder.Services.AddRainfallReportService(builder.Configuration);
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(
    typeof(DomainMarker).Assembly,
    typeof(Program).Assembly
    ));
builder.Services.AddMemoryCache();
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseHsts();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
