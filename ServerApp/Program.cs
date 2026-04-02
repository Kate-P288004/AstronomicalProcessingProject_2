using Microsoft.AspNetCore.Server.Kestrel.Core;
using ServerApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add gRPC
builder.Services.AddGrpc();

// Configure Kestrel to listen on localhost:5000 with HTTP/2
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

// Map gRPC service
app.MapGrpcService<AstroServiceImpl>();

// Optional root message
app.MapGet("/", () => "Astro gRPC server is running");

// Run app
app.Run();