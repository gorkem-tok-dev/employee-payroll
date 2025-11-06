using EmployeePayroll.Api.Data;
using EmployeePayroll.Api.Data.Interfaces;
using EmployeePayroll.Api.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Payroll API",
        Description = "A clean Dapper-based Payroll API developed by Görkem Tok",
        Contact = new OpenApiContact
        {
            Name = "Görkem Tok",
            Email = "cout.gorkem.tok@gmail.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);

    c.EnableAnnotations();
});

// Dependency Injection
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IOvertimeEntriesRepository, OvertimeEntriesRepository>();
builder.Services.AddScoped<IWorkEntriesRepository, WorkEntriesRepository>();
builder.Services.AddScoped<IPayrollsRepository, PayrollsRepository>();

// FluentValidation configuration
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Payroll API v1");
    opt.RoutePrefix = "api/docs"; 
});

// 404 hatalarında Swaggera yönlendirme
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 &&
        !context.Request.Path.Value!.StartsWith("/api/docs"))
    {
        context.Response.Redirect("/api/docs");
    }
});

// Global exception middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
