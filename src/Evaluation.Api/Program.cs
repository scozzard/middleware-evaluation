using Evaluation.Api.ApiClient;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Evaluation.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add basic services.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add http client.
builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["MiddlewareBaseUrl"]);
});

// Configure swagger.
builder.Services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo {
            Version = "v1",
            Title = "Evaluation.Api",
            Description = "Api used for retrieving company data from Middleware NZ.",
            TermsOfService = new Uri("https://www.tolkien.co.uk/terms-of-use/"),
            Contact = new OpenApiContact()
            {
                Name = "Scott Walker",
                Email = "scot.walker@gmail.com",
                Url = new Uri("https://www.linkedin.com/in/scott-walker-65667824/")
            }
        });
        
        // Add xml comments to Swagger.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        opt.IncludeXmlComments(xmlPath);
    }
);

// Add Serilog
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

// Add custom middleware.
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();

// This is required to expose the implicitly defined Program class to the test project.
public partial class Program { }