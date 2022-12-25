using Microsoft.OpenApi.Models;
using Infrastructure.Persistense;
using MediatR;
using FluentValidation.AspNetCore;
using Application.Drugstore.Queries.GetAllDrugstores;
using Application.Department.Queries.GetAllDepartments;
using Infrastructures.Persistance;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WebAPI.RabbitMQ;
namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection Services)
        {
            Services.AddPersistence(Configuration);
            Services.AddHostedService<RabbitMQListener>();
            Services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Drugstores.API",
                    Version = "v1"
                });

            });
            Services.AddHttpClient();
            Services.AddMediatR(typeof(GetAllDepartmentsQuery));
            Services.AddMediatR(typeof(GetAllDrugstoresQuery));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Drugstores.API v1"));
            //}

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate();
            }
        }
    }
}
