using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.December2020.Web.Helpers;
using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.December2020.Domain.Mapping;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicatonProcess.December2020.Web
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
            services.AddScoped<IUtilities, Utilities>();
            services.AddMvc().AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddDbContext<AppDbContext>(opt =>
                 opt.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")), ServiceLifetime.Singleton);
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers();
            services.AddHttpClient();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.December2020.Web", Version = "v1" });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1"));
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
