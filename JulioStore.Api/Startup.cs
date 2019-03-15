using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JulioStore.Domain.StoreContext.Handlers;
using JulioStore.Domain.StoreContext.Repositories;
using JulioStore.Domain.StoreContext.Services;
using JulioStore.Infra.Repositories;
using JulioStore.Infra.Services;
using JulioStore.Infra.StoreContext.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Elmah.Io.AspNetCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using JulioStore.shared;

namespace JulioStore.Api
{
    public class Startup
    {
        public static IConfiguration configuration {get; set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();


            services.AddApplicationInsightsTelemetry(configuration);
            services.AddMvc();
            services.AddResponseCompression();
            
            services.AddScoped<JulioStoreContext, JulioStoreContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();

            services.AddSwaggerGen(x=>{
                x.SwaggerDoc("v1", new Info{ Title = "Julio Store API", Version = "v1"});
            });

            Settings.ConnectionString = configuration["connectionString"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI( x=> {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Julio Store API - v1");
            });

            app.UseElmahIo("b7fad67c9e674654a1b190c2ab67dc2e", new Guid("4e0919e8-06ea-4a7a-9acc-d700cb6cddf4"));

        }
    }
}



