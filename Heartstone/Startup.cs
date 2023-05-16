using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;

namespace Heartstone
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Heartstone API", Version = "v1" });
            });

            // MongoDB configuration
            var mongoConnectionString = Configuration.GetConnectionString("MongoDB");
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);
                return new MongoClient(mongoClientSettings);
            });
            services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
                return mongoClient.GetDatabase("HeartstoneDB"); // Replace "HeartstoneDB" with the actual database name
            });

            // Other service configurations
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heartstone API v1");
                });
            }
            else
            {
                // Add production middleware configurations
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Other middleware configurations
        }
    }
}
