using CinemaService.DataLayer;
using CinemaService.Web.Api;
using CinemaService.Web.Api.Library.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace CinemaService.IntegrationTests
{
    public static class WebApplicationFactoryExtension
    {
        public static HttpClient CreateHttpClientForTest<TStartup>(this WebApplicationFactory<TStartup> factory) where TStartup : class
        {
            HttpClient client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false, BaseAddress = new Uri("https://localhost") });
            return client;
        }

        public static WebApplicationFactory<TStartup> SetupForTests<TStartup>(this WebApplicationFactory<TStartup> factory,
                                                                              IEnumerable<KeyValuePair<string, string>> configOverrides,
                                                                              Action<IServiceCollection> configureTestServices) where TStartup : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, configBuilder) =>
                {
                    string projectDir = Directory.GetCurrentDirectory();
                    string configPath = Path.Combine(projectDir, "appsettings.IntegrationTests.json");
                    configBuilder.Sources.Clear();
                    configBuilder.AddJsonFile(configPath);
                });

                builder.ConfigureServices(configureTestServices);

                builder.ConfigureServices(services =>
                {
                    //services.TryAddTransient(sp => new Mock<ICinemaShowService>(MockBehavior.Strict).Object);
                    //services.TryAddTransient(sp => new Mock<ISeatService>(MockBehavior.Strict).Object);
                    //services.TryAddTransient(sp => new Mock<IBookingService>().Object);

                    //ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    //services.AddDbContext<ApplicationDbContext>(options =>
                    //{
                    //    options.UseInMemoryDatabase("IntegrationTestDb");
                    //    //options.UseInternalServiceProvider(serviceProvider);
                    //});

                    //ServiceProvider sp = services.BuildServiceProvider();

                    //using (IServiceScope scope = sp.CreateScope())
                    //{
                    //    IServiceProvider scopedServices = scope.ServiceProvider;
                    //    var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

                    //    dbContext.Database.EnsureCreated();
                    //}
                });

                builder.UseStartup<Startup>();
            });
        }
    }
}