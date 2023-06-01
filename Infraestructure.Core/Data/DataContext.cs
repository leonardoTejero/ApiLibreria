using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.Library;
using Infraestructure.Entity.Model.Master;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Core.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //Establecer la tablas que se van a crear en la migracion, (puede tener otro nombre)
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<RolEntity> RolEntity { get; set; }
        public DbSet<RolUserEntity> RolUserEntity { get; set; }
        public DbSet<PermissionEntity> PermissionEntity { get; set; }
        public DbSet<RolPermissionEntity> RolPermissionEntity { get; set; }
        public DbSet<TypePermissionEntity> TypePermissionEntity { get; set; }
        public DbSet<StateEntity> StateEntity { get; set; }
        public DbSet<TypeStateEntity> TypeStateEntity { get; set; }


        public DbSet<AuthorEntity> AuthorEntity  { get; set; }
        public DbSet<BookEntity> BookEntity { get; set; }
        public DbSet<AuthorBookEntity> AuthorBookEntity { get; set; }
        public DbSet<EditorialEntity> EditorialEntity { get; set; }


        //Crear las configuaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Establecer el email como campo unico
            modelBuilder.Entity<UserEntity>()
               .HasIndex(b => b.Email)
               .IsUnique();

            // Evitar que a estas entidades se les asigne un id automatico, porque se lo establecemos en la data semilla con los enums
            modelBuilder.Entity<TypeStateEntity>().Property(t => t.IdTypeState).ValueGeneratedNever();
            modelBuilder.Entity<TypePermissionEntity>().Property(t => t.IdTypePermission).ValueGeneratedNever();
            modelBuilder.Entity<StateEntity>().Property(t => t.IdState).ValueGeneratedNever();
            modelBuilder.Entity<RolEntity>().Property(t => t.IdRol).ValueGeneratedNever();
            modelBuilder.Entity<PermissionEntity>().Property(t => t.IdPermission).ValueGeneratedNever();
        }
    }
}
