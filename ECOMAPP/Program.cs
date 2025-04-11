using System.Text.Json.Serialization;
using ECOMAPP.DataLayer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static ECOMAPP.DataLayer.DLOrder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Register dependencies for DLOrder
builder.Services.AddMemoryCache(); // Required for TokenService caching
builder.Services.AddSingleton<TokenService>(); // TokenService as singleton
builder.Services.AddScoped<DLOrder>();
builder.Services.AddLogging(logging => logging.AddConsole()); 

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();