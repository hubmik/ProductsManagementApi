using RestApi.Data;
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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AngularClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
