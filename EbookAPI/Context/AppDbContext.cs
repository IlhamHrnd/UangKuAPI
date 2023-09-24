using EbookAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UangKuAPI.Model;

namespace EbookAPI.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AppStandardReferenceItem> AppStandardReferenceItems { get; set; }
        public DbSet<AppStandardReference> AppStandardReferences { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                    {
                        dbCreator.Create();
                    }
                    if (!dbCreator.HasTables())
                    {
                        dbCreator.CreateTables();
                    }
                }
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<AppStandardReferenceItem>().ToTable("AppStandardReferenceItem");
            modelBuilder.Entity<AppStandardReference>().ToTable("AppStandardReference");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=103.145.226.115; Database=yolodevc_UangKu; User Id=yolodevc_UangAdm; Password=[lLMZ(QMj]l{; Port=3306",
                    new MariaDbServerVersion("10.6.15-MariaDB-cll-lve"));
            }
        }
    }
}
