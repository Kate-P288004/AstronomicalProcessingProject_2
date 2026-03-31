using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerApp.Services;

// ============================================================
// Create WebApplication Builder
// Configures services required for the server
// ============================================================
var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Register gRPC services
// Enables communication between client and server
// ============================================================
builder.Services.AddGrpc();

// ============================================================
// Build the application
// ============================================================
var app = builder.Build();

// ============================================================
// Map gRPC Service
// Links AstroService implementation to incoming client requests
// ============================================================
app.MapGrpcService<AstroServiceImpl>();

// ============================================================
// Default endpoint
// Used for testing if server is running in browser
// ============================================================
app.MapGet("/", () => "Astronomical Processing Server is running...");

// ============================================================
// Run server
// Starts listening for client connections
// ============================================================
app.Run();