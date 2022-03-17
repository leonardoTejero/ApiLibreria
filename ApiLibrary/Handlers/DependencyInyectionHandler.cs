using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Domain.Services;
using MyLibrary.Domain.Services.Interface;
using MyVet.Domain.Services;
using MyVet.Domain.Services.Interface;

namespace ApiLibrary.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Repository await UnitofWork parameter ctor explicit
            services.AddScoped<UnitOfWork, UnitOfWork>();


            // Infrastructure
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<SeedDb>();

            //Domain
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IRolServices, RolServices>();
            services.AddTransient<IPermissionServices, PermissionServices>();

            services.AddTransient<IAuthorServices, AuthorServices>();
            services.AddTransient<IEditorialServices, EditorialServices>();
            services.AddTransient<IBookServices, BookServices>();
        }
    }
}
