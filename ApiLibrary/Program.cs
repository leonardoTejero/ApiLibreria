using Infraestructure.Core.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            // Crear data semilla comentar linea al ejecutar primera vez
            //RunSeeding(host);

            host.Run();
        }

        /// <summary>
        /// Ejecuta el proceso de siembra de datos en la base de datos.
        /// Utiliza un scope para obtener el servicio <see cref="SeedDb"/> y ejecutar la inicialización de datos.
        /// Este método debe ejecutarse solo la primera vez que se inicia la aplicación para poblar la base de datos con datos iniciales.
        /// </summary>
        /// <param name="host">Instancia del host de la aplicación que provee los servicios necesarios.</param>
        private static void RunSeeding(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.ExecSeedAsync().Wait();
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
