using System;
using System.Net.Http;
using Evaluation.Api.ApiClient;
using Evaluation.Api.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Evaluation.Api.Tests.Integration
{
    public class IntegrationTestsFixture
    {
        public HttpClient HttpClient { get; }

        public IntegrationTestsFixture()
        {
            var app = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Add basic services.
                        services.AddControllers();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddMediatR(typeof(GetCompanyQuery));

                        // Add http client.
                        services.AddHttpClient<IApiClient, Api.ApiClient.ApiClient>(client =>
                        {
                            client.BaseAddress = new Uri("https://raw.githubusercontent.com/MiddlewareNewZealand/evaluation-instructions/main/xml-api/");
                        });
                    });
                });

            HttpClient = app.CreateClient(new WebApplicationFactoryClientOptions());
        }
    }
}
