using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Services;
using Snacker.Infrastructure.Context;
using Snacker.Infrastructure.Repository;

namespace Snacker.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Snacker.API", Version = "v1" });
            });

            services.AddScoped<IBaseRepository<Address>, BaseRepository<Address>>();
            services.AddScoped<IBaseService<Address>, BaseService<Address>>();

            services.AddScoped<IBaseRepository<Restaurant>, BaseRepository<Restaurant>>();
            services.AddScoped<IBaseService<Restaurant>, BaseService<Restaurant>>();

            services.AddScoped<IBaseRepository<RestaurantCategory>, BaseRepository<RestaurantCategory>>();
            services.AddScoped<IBaseService<RestaurantCategory>, BaseService<RestaurantCategory>>();

            services.AddScoped<IBaseRepository<Person>, BaseRepository<Person>>();
            services.AddScoped<IBaseService<Person>, BaseService<Person>>();

            services.AddScoped<IBaseRepository<Table>, BaseRepository<Table>>();
            services.AddScoped<IBaseService<Table>, BaseService<Table>>();

            services.AddScoped<IBaseRepository<ProductCategory>, BaseRepository<ProductCategory>>();
            services.AddScoped<IBaseService<ProductCategory>, BaseService<ProductCategory>>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snacker.API v1"));
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
