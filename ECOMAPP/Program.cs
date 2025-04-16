//using System.Text.Json.Serialization;
//using ECOMAPP.DataLayer;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.Logging;
//using Razorpay.Api;
//using static ECOMAPP.DataLayer.DLOrder;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the DI container
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//        options.JsonSerializerOptions.MaxDepth = 64;
//    });

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", builder =>
//    {
//        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//    });
//});

//// Register RazorpayClient as Singleton
//builder.Services.AddSingleton<RazorpayClient>(serviceProvider =>
//{
//    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
//    string razorpayKey = configuration["Razorpay:Key"];
//    string razorpaySecret = configuration["Razorpay:Secret"];
//    return new RazorpayClient(razorpayKey, razorpaySecret);
//});

//builder.Services.AddSingleton<HttpClient>();

//builder.Services.AddSingleton<IMemoryCache>();
//builder.Services.AddScoped<DLOrder>();


//builder.Services.AddHttpClient();


//builder.Services.AddMemoryCache();


//builder.Services.AddLogging(logging => logging.AddConsole());


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddMemoryCache();

//var app = builder.Build();

//app.UseCors("AllowAllOrigins");

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseStaticFiles();
//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();

//app.Run();


using System.Text.Json.Serialization;
using ECOMAPP.DataLayer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Razorpay.Api;

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

// Register RazorpayClient as Singleton
builder.Services.AddSingleton<RazorpayClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string razorpayKey = configuration["Razorpay:Key"];
    string razorpaySecret = configuration["Razorpay:Secret"];
    return new RazorpayClient(razorpayKey, razorpaySecret);
});

// Register HttpClient as Singleton through IHttpClientFactory (Recommended for better management)
builder.Services.AddHttpClient();

// Register IMemoryCache as Singleton (Only once)
builder.Services.AddMemoryCache();

// Register DLOrder class with necessary dependencies
builder.Services.AddScoped<DLOrder>();

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
