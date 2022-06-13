using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Services;
using Snacker.Infrastructure.Context;
using Snacker.Infrastructure.Repository;
using System.Text;

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

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JWT:Secret"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IBaseRepository<Address>, BaseRepository<Address>>();
            services.AddScoped<IBaseService<Address>, BaseService<Address>>();

            services.AddScoped<IBaseRepository<Restaurant>, RestaurantRepository>();
            services.AddScoped<IBaseService<Restaurant>, RestaurantService>();

            services.AddScoped<IBaseRepository<RestaurantCategory>, BaseRepository<RestaurantCategory>>();
            services.AddScoped<IBaseService<RestaurantCategory>, BaseService<RestaurantCategory>>();

            services.AddScoped<IBaseRepository<Person>, BaseRepository<Person>>();
            services.AddScoped<IBaseService<Person>, BaseService<Person>>();

            services.AddScoped<IBaseRepository<Table>, BaseRepository<Table>>();
            services.AddScoped<IBaseService<Table>, BaseService<Table>>();

            services.AddScoped<IBaseRepository<ProductCategory>, BaseRepository<ProductCategory>>();
            services.AddScoped<IBaseService<ProductCategory>, BaseService<ProductCategory>>();

            services.AddScoped<IBaseRepository<UserType>, BaseRepository<UserType>>();
            services.AddScoped<IBaseService<UserType>, BaseService<UserType>>();

            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseService<User>, UserService>();

            services.AddScoped<IBaseRepository<Product>, ProductRepository>();
            services.AddScoped<IBaseService<Product>, ProductService>();

            services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
            services.AddScoped<IBaseService<Order>, BaseService<Order>>();

            services.AddScoped<IBaseRepository<OrderHasProduct>, BaseRepository<OrderHasProduct>>();
            services.AddScoped<IBaseService<OrderHasProduct>, BaseService<OrderHasProduct>>();

            services.AddScoped<IBaseRepository<Bill>, BaseRepository<Bill>>();
            services.AddScoped<IBaseService<Bill>, BaseService<Bill>>();

            services.AddScoped<IBaseRepository<OrderStatus>, BaseRepository<OrderStatus>>();
            services.AddScoped<IBaseService<OrderStatus>, BaseService<OrderStatus>>();

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBillRepository, BillRepository>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ITableRepository, TableRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Snacker.API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
