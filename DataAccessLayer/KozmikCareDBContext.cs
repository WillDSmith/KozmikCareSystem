using BusinessObjectLayer;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class KozmikCareDbContext : DbContext
    {
        public KozmikCareDbContext() : base("KozmikChildCareSystemsDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KozmikCareDbContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>().Property(p => p.Email).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        //}
    }
}
