using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

using DapperCRUDAngular.Abstraction.Services;
using DapperCRUDAngular.Abstraction.Repository;
using DapperCRUDAngular.InfraStructure.Repository;
using DapperCRUDAngular.Services;
using DapperCRUDAngular.Abstraction.Models;

namespace PostalCodeAPI
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
            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson(); ;
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddSwaggerGen(/*c=> { 
            c.}*/);
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });

            //services.AddTransient<IAppSettings, AppSettings>(); //DB Connection

            #region Common Service
            //services.AddTransient<IBaseRepository, BaseRepository>();
            //services.AddTransient<IBaseService, BaseService>();
            #endregion
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            #region Data Transformation           
            services.AddTransient<IEmployeeDetailsService, EmployeeDetailsService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IEmployeeDetailsRepository, EmployeeDetailsRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
    builder.WithOrigins("http://localhost:63889")
           .AllowAnyHeader()
           .AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
                
            });

            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
