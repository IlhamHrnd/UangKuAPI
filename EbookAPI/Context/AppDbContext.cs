using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UangKuAPI.BusinessObjects.Model;

namespace UangKuAPI.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AppStandardReferenceItem> AppStandardReferenceItems { get; set; }
        public DbSet<AppStandardReference> AppStandardReferences { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Subdistrict> Subdistricts { get; set; }
        public DbSet<PostalCodes> PostalCodes { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<GetSumTransaction> GetSumTransactions { get; set; }
        public DbSet<UserPicture> Picture { get; set; }
        public DbSet<AppParameter> Parameter { get; set; }
        public DbSet<UserReport> Report { get; set; }
        public DbSet<UserWishlist> UserWishlist { get; set; }
        public DbSet<UserWishlistPerCategory> UserWishlistPerCategories { get; set; }

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
            modelBuilder.Entity<Province>().ToTable("Provinces");
            modelBuilder.Entity<Cities>().ToTable("Cities");
            modelBuilder.Entity<District>().ToTable("Districts");
            modelBuilder.Entity<Subdistrict>().ToTable("Subdistricts");
            modelBuilder.Entity<PostalCodes>().ToTable("PostalCode");
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<UserPicture>().ToTable("UserPicture");
            modelBuilder.Entity<AppParameter>().ToTable("AppParameter");
            modelBuilder.Entity<UserReport>().ToTable("UserReport");
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.HasKey(t => t.TransNo);
            });
            modelBuilder.Entity<GetSumTransaction>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("Transaction");
            });
            modelBuilder.Entity<UserWishlist>(entity =>
            {
                entity.ToTable("UserWishlist");
                entity.HasKey(uw => uw.PersonID);
            });
            modelBuilder.Entity<UserWishlistPerCategory>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("UserWishlist");
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=103.185.53.118; Database=yolodevc_UangKu; User Id=yolodevc_API; Password=YoloAdmin@123",
                    new MariaDbServerVersion("10.6.15-MariaDB-cll-lve"));
            }
        }
    }
}
