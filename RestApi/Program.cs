using RestApi.Data;
using RestApi.Middleware;
using RestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IDefaultProductProvider, DefaultProductProvider>();
builder.Services.AddSingleton<IProductManagementService, ProductManagementService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    var defaultFactory = options.InvalidModelStateResponseFactory;
    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .SelectMany(e => e.Value!.Errors.Select(x => $"{e.Key}: {x.ErrorMessage}"));

        logger.LogWarning("Walidacja nie powiod³a siê dla {Method} {Path}: {Errors}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            string.Join("; ", errors));

        return defaultFactory(context);
    };
});

var app = builder.Build();

app.UseExceptionHandler();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AngularClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
