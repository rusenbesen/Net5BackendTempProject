using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TempProject.API.Filters;
using TempProject.API.Middlewares;
using TempProject.Core.Repositories;
using TempProject.Core.Services;
using TempProject.Core.UnitOfWorks;
using TempProject.Repository;
using TempProject.Repository.Repositories;
using TempProject.Repository.UnitOfWorks;
using TempProject.Service.Mapping;
using TempProject.Service.Services;
using TempProject.Service.Validations;

namespace TempProject.API
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

            services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TempProject.API", Version = "v1" });
            });
            services.AddMemoryCache();
            services.AddScoped(typeof(NotFoundFilter<>));
            services.AddAutoMapper(typeof(MapProfile));
          
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseNpgsql(Configuration.GetConnectionString("NpgsqlConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TempProject.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseCustomException();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
