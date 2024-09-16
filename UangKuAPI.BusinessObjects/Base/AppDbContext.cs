using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UangKuAPI.BusinessObjects.Models;

namespace UangKuAPI.BusinessObjects.Base;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        try
        {
            if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreator)
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

    public virtual DbSet<AppParameter> AppParameters { get; set; }

    public virtual DbSet<AppProgram> AppPrograms { get; set; }

    public virtual DbSet<AppStandardReference> AppStandardReferences { get; set; }

    public virtual DbSet<AppStandardReferenceItem> AppStandardReferenceItems { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<PostalCode> PostalCodes { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Subdistrict> Subdistricts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPicture> UserPictures { get; set; }

    public virtual DbSet<UserReport> UserReports { get; set; }

    public virtual DbSet<UserWishlist> UserWishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=103.185.53.118; Database=yolodevc_UangKu; User Id=yolodevc_API; Password=YoloAdmin@123",
                new MariaDbServerVersion("10.6.19-mariadb"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AppParameter>(entity =>
        {
            entity.HasKey(e => e.ParameterId).HasName("PRIMARY");

            entity.ToTable("AppParameter");

            entity.Property(e => e.ParameterId)
                .HasMaxLength(50)
                .HasColumnName("ParameterID");
            entity.Property(e => e.IsUsedBySystem).HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(50)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.ParameterName).HasMaxLength(200);
            entity.Property(e => e.ParameterValue).HasMaxLength(1000);
            entity.Property(e => e.Srcontrol)
                .HasMaxLength(50)
                .HasColumnName("SRControl");
        });

        modelBuilder.Entity<AppProgram>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PRIMARY");

            entity.ToTable("AppProgram");

            entity.Property(e => e.ProgramId)
                .HasMaxLength(30)
                .HasColumnName("ProgramID");
            entity.Property(e => e.IsProgram).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramAddAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramApprovalAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramDeleteAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramEditAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramPrintAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramUnApprovalAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramUnVoidAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramViewAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsProgramVoidAble).HasColumnType("bit(1)");
            entity.Property(e => e.IsUsedBySystem).HasColumnType("bit(1)");
            entity.Property(e => e.IsVisible).HasColumnType("bit(1)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(50)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(1000);
            entity.Property(e => e.ProgramName).HasMaxLength(30);
        });

        modelBuilder.Entity<AppStandardReference>(entity =>
        {
            entity.HasKey(e => e.StandardReferenceId).HasName("PRIMARY");

            entity.ToTable("AppStandardReference");

            entity.Property(e => e.StandardReferenceId)
                .HasMaxLength(30)
                .HasColumnName("StandardReferenceID");
            entity.Property(e => e.IsActive).HasColumnType("int(11)");
            entity.Property(e => e.IsUsedBySystem).HasColumnType("int(11)");
            entity.Property(e => e.ItemLength).HasColumnType("int(11)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(40)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.StandardReferenceName).HasMaxLength(200);
        });

        modelBuilder.Entity<AppStandardReferenceItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PRIMARY");

            entity.ToTable("AppStandardReferenceItem");

            entity.Property(e => e.ItemId)
                .HasMaxLength(50)
                .HasColumnName("ItemID");
            entity.Property(e => e.IsActive).HasColumnType("int(11)");
            entity.Property(e => e.IsUsedBySystem).HasColumnType("int(11)");
            entity.Property(e => e.ItemIcon).HasColumnType("mediumblob");
            entity.Property(e => e.ItemName).HasMaxLength(200);
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(40)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(1000);
            entity.Property(e => e.StandardReferenceId)
                .HasMaxLength(30)
                .HasColumnName("StandardReferenceID");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PRIMARY");

            entity.Property(e => e.CityId)
                .HasColumnType("int(11)")
                .HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(255);
            entity.Property(e => e.ProvId)
                .HasColumnType("int(11)")
                .HasColumnName("ProvID");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DisId).HasName("PRIMARY");

            entity.Property(e => e.DisId)
                .HasColumnType("int(11)")
                .HasColumnName("DisID");
            entity.Property(e => e.CityId)
                .HasColumnType("int(11)")
                .HasColumnName("CityID");
            entity.Property(e => e.DisName).HasMaxLength(255);
        });

        modelBuilder.Entity<PostalCode>(entity =>
        {
            entity.HasKey(e => e.PostalId).HasName("PRIMARY");

            entity.ToTable("PostalCode");

            entity.Property(e => e.PostalId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("PostalID");
            entity.Property(e => e.CityId)
                .HasColumnType("int(11)")
                .HasColumnName("CityID");
            entity.Property(e => e.DisId)
                .HasColumnType("int(11)")
                .HasColumnName("DisID");
            entity.Property(e => e.PostalCode1)
                .HasColumnType("int(11)")
                .HasColumnName("PostalCode");
            entity.Property(e => e.ProvId)
                .HasColumnType("int(11)")
                .HasColumnName("ProvID");
            entity.Property(e => e.SubdisId)
                .HasColumnType("int(11)")
                .HasColumnName("SubdisID");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("Profile");

            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(1000);
            entity.Property(e => e.District).HasMaxLength(1000);
            entity.Property(e => e.FirstName).HasMaxLength(1000);
            entity.Property(e => e.LastName).HasMaxLength(1000);
            entity.Property(e => e.LastUpdateByUser).HasMaxLength(50);
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.MiddleName).HasMaxLength(1000);
            entity.Property(e => e.Photo).HasColumnType("mediumblob");
            entity.Property(e => e.PlaceOfBirth).HasMaxLength(1000);
            entity.Property(e => e.PostalCode).HasColumnType("int(11)");
            entity.Property(e => e.Province).HasMaxLength(1000);
            entity.Property(e => e.Subdistrict).HasMaxLength(1000);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvId).HasName("PRIMARY");

            entity.Property(e => e.ProvId)
                .HasColumnType("int(11)")
                .HasColumnName("ProvID");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("LocationID");
            entity.Property(e => e.ProvName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)");
        });

        modelBuilder.Entity<Subdistrict>(entity =>
        {
            entity.HasKey(e => e.SubdisId).HasName("PRIMARY");

            entity.Property(e => e.SubdisId)
                .HasColumnType("int(11)")
                .HasColumnName("SubdisID");
            entity.Property(e => e.DisId)
                .HasColumnType("int(11)")
                .HasColumnName("DisID");
            entity.Property(e => e.SubdisName).HasMaxLength(255);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransNo).HasName("PRIMARY");

            entity.ToTable("Transaction");

            entity.Property(e => e.TransNo).HasMaxLength(50);
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.CreatedByUserId)
                .HasMaxLength(50)
                .HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(50)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.Photo).HasColumnType("mediumblob");
            entity.Property(e => e.SrtransItem)
                .HasMaxLength(20)
                .HasColumnName("SRTransItem");
            entity.Property(e => e.Srtransaction)
                .HasMaxLength(20)
                .HasColumnName("SRTransaction");
            entity.Property(e => e.TransType).HasMaxLength(15);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.Username).HasMaxLength(15);
            entity.Property(e => e.ActiveDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(1000);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateByUser).HasMaxLength(50);
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(1000);
            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.Sraccess)
                .HasMaxLength(10)
                .HasColumnName("SRAccess");
            entity.Property(e => e.Srsex)
                .HasMaxLength(10)
                .HasColumnName("SRSex");
            entity.Property(e => e.Srstatus)
                .HasMaxLength(10)
                .HasColumnName("SRStatus");
        });

        modelBuilder.Entity<UserPicture>(entity =>
        {
            entity.HasKey(e => e.PictureId).HasName("PRIMARY");

            entity.ToTable("UserPicture");

            entity.Property(e => e.PictureId)
                .HasMaxLength(50)
                .HasColumnName("PictureID");
            entity.Property(e => e.CreatedByUserId)
                .HasMaxLength(50)
                .HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnType("int(11)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(40)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.Picture).HasColumnType("mediumblob");
            entity.Property(e => e.PictureFormat).HasMaxLength(15);
            entity.Property(e => e.PictureName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserReport>(entity =>
        {
            entity.HasKey(e => e.ReportNo).HasName("PRIMARY");

            entity.ToTable("UserReport");

            entity.Property(e => e.ReportNo).HasMaxLength(50);
            entity.Property(e => e.ApprovedByUserId)
                .HasMaxLength(50)
                .HasColumnName("ApprovedByUserID");
            entity.Property(e => e.ApprovedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedByUserId)
                .HasMaxLength(50)
                .HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DateErrorOccured).HasColumnType("datetime");
            entity.Property(e => e.ErrorCronologic).HasMaxLength(150);
            entity.Property(e => e.IsApprove).HasColumnType("int(11)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(50)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.Picture).HasColumnType("mediumblob");
            entity.Property(e => e.SrerrorLocation)
                .HasMaxLength(20)
                .HasColumnName("SRErrorLocation");
            entity.Property(e => e.SrerrorPossibility)
                .HasMaxLength(20)
                .HasColumnName("SRErrorPossibility");
            entity.Property(e => e.SrreportStatus)
                .HasMaxLength(20)
                .HasColumnName("SRReportStatus");
            entity.Property(e => e.VoidByUserId)
                .HasMaxLength(50)
                .HasColumnName("VoidByUserID");
            entity.Property(e => e.VoidDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserWishlist>(entity =>
        {
            entity.HasKey(e => e.WishlistId).HasName("PRIMARY");

            entity.ToTable("UserWishlist");

            entity.Property(e => e.WishlistId)
                .HasMaxLength(50)
                .HasColumnName("WishlistID");
            entity.Property(e => e.CreatedByUserId)
                .HasMaxLength(50)
                .HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.IsComplete).HasColumnType("int(11)");
            entity.Property(e => e.LastUpdateByUserId)
                .HasMaxLength(50)
                .HasColumnName("LastUpdateByUserID");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.PersonId)
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductPicture).HasColumnType("mediumblob");
            entity.Property(e => e.ProductPrice).HasPrecision(10, 2);
            entity.Property(e => e.ProductQuantity).HasColumnType("int(11)");
            entity.Property(e => e.SrproductCategory)
                .HasMaxLength(20)
                .HasColumnName("SRProductCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
