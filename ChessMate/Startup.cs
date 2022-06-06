using ChessMate.Application.Extensions;
using ChessMate.Infrastructure;
using ChessMate.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ChessMate
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration["ConnectionStrings:Main"];

            services.InitializeDatabase(connString);

            services.InitializeRepositories();

            services.RegisterManagers();

            services.RegisterValidators();

            services.AddControllers();

            services.AddLogging();

            services.AddSwaggerGen(swaggerOption =>
            {
                swaggerOption.EnableAnnotations();

                swaggerOption.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "ChessMate API", 
                    Version = "v1",
                    Description = "API was developed as solution for ECCO test excercise"});

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                swaggerOption.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ChessMateDbContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChessMate v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
