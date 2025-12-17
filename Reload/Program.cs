using Reload.Services;

var builder = WebApplication.CreateBuilder(args);

// Register MVC controllers
builder.Services.AddControllers();

// Register a service via Dependency Injection
// IMPORTANT: DI registration happens at startup and is NOT hot-reloadable

builder.Services.AddSingleton<ITimeService, TimeService>();
// HR-O8
//builder.Services.AddScoped<ITimeService, TimeService>();

var app = builder.Build();

// Configure routing middleware (startup-only logic)
app.UseRouting();

// Map controller endpoints
app.MapControllers();

// Start the application
app.Run();