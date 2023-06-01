using ApiLibrary.Handlers;
using ApiLibreriaNeoris.Handlers;
using Infraestructure.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiLibrary
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

            #region Swagger 1/2

            SwaggerHandler.SwaggerConfig(services);

            #endregion Swagger 1/2

            #region Context SQL Server
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringSQLServer"));

            });
            #endregion

            #region Inyeccion de dependencia 
            DependencyInjectionHandler.DependencyInyectionConfig(services);
            #endregion

            #region Jwt Token Configuration 1/2
            IConfigurationSection tokenAppSetting = Configuration.GetSection("Tokens");
            JwtConfigurationHandler.ConfigureJwtAuthentication(services, tokenAppSetting);

            #endregion Jwt Configuration

            // Ignorar la referencia ciclica, necesario nuget de newtonsoft
            ////services.AddControllers().AddNewtonsoftJson(x =>
            //x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Jwt Token Configuration 2/2
            JwtConfigurationHandler.ConfigureUseAuthentication(app);
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            #region Swagger 2/2

            SwaggerHandler.UseSwagger(app);

            #endregion Swagger 2/2

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
