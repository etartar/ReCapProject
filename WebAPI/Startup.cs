using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;

namespace WebAPI
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rent A Car Web Api", Version = "v1" });
            });

            #region [IoC Container]
            services.AddSingleton<IBrandService, BrandManager>();
            services.AddSingleton<IBrandDal, EfBrandDal>();

            services.AddSingleton<ICarService, CarManager>();
            services.AddSingleton<ICarDal, EfCarDal>();

            services.AddSingleton<IColorService, ColorManager>();
            services.AddSingleton<IColorDal, EfColorDal>();

            services.AddSingleton<ICustomerService, CustomerManager>();
            services.AddSingleton<ICustomerDal, EfCustomerDal>();

            services.AddSingleton<IRentalService, RentalManager>();
            services.AddSingleton<IRentalDal, EfRentalDal>();

            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IUserDal, EfUserDal>();
            #endregion

            #region [Database Context]
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
            {
                ConnectionString = Configuration.GetConnectionString("PostgreSqlDb"),
                Host = Configuration["PostgreSql:Host"],
                Database = Configuration["PostgreSql:Database"],
                Port = Convert.ToInt32(Configuration["PostgreSql:Port"]),
                Username = Configuration["PostgreSql:Username"],
                Password = Configuration["PostgreSql:Password"]
            };

            services.AddDbContext<CarRentalContext>(opt =>
            {
                opt.UseNpgsql(connectionStringBuilder.ConnectionString);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rent A Car Web Api v1"));
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
