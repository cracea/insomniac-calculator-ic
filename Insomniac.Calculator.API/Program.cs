using Mapster;
using Insomniac.Calculator.API.Middleware;
using Insomniac.Calculator.Data;
using Insomniac.Calculator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMapster();

builder.Services.AddCalculatorServices();
builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();

// Add global Exception handler
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.InitializeDatabase();

await app.RunAsync();

// Enable component tests
public partial class Program { }