using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// Home endpoint
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Azure .NET API is running!",
        status = "Success"
    });
});

// Get all employees
app.MapGet("/api/employees", () =>
{
    var employees = new[]
    {
        new
        {
            Id = 1,
            Name = "Mahesh",
            Role = "Developer"
        },
        new
        {
            Id = 2,
            Name = "Ravi",
            Role = "Tester"
        },
        new
        {
            Id = 3,
            Name = "Suresh",
            Role = "DevOps Engineer"
        }
    };

    return Results.Ok(employees);
});

// Get employee by ID
app.MapGet("/api/employees/{id:int}", (int id) =>
{
    var employee = new
    {
        Id = id,
        Name = "Mahesh",
        Role = "Developer"
    };

    return Results.Ok(employee);
});

// Create employee
app.MapPost("/api/employees", (Employee employee) =>
{
    return Results.Created(
        $"/api/employees/{employee.Id}",
        employee
    );
});

// Health check
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        application = "AzureGitHubDemo"
    });
});

app.Run();


// Employee model
public record Employee(
    int Id,
    string Name,
    string Role
);
