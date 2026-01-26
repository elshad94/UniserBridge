using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project.Core.Entities.Models;

namespace Project.Core.DataAccess.Concrete.EntityFramework.Contexts;

public partial class ProjectAppDbContextBase : DbContext
{
    public ProjectAppDbContextBase()
    {
    }

    public ProjectAppDbContextBase(DbContextOptions<ProjectAppDbContextBase> options)
        : base(options)
    {
    }

    public virtual DbSet<CardLang> CardLangs { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FileUpload> FileUploads { get; set; }

    public virtual DbSet<FileUploadSetting> FileUploadSettings { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuInfo> MenuInfos { get; set; }

    public virtual DbSet<MenuLang> MenuLangs { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageLang> MessageLangs { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleLang> ModuleLangs { get; set; }

    public virtual DbSet<ObjectContent> ObjectContents { get; set; }

    public virtual DbSet<ObjectContentsLang> ObjectContentsLangs { get; set; }

    public virtual DbSet<OperationBlock> OperationBlocks { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<SpeCode> SpeCodes { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLoginHistory> UserLoginHistories { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserToken> UserTokens { get; set; }

    public virtual DbSet<VCity> VCities { get; set; }

    public virtual DbSet<VCountry> VCountries { get; set; }

    public virtual DbSet<VSpeCode> VSpeCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.37.33; Database=DB_UniserBridge; User Id=sa;Password=Uniser!@#; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardLang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CARDLANG");

            entity.ToTable("CardLang", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasMaxLength(250);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("Cities", "CRD");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries", "CRD");

            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<FileUpload>(entity =>
        {
            entity.ToTable("FileUploads", "OPR");

            entity.HasIndex(e => e.DownloadKey, "UK_FileUploads_DownloadKey").IsUnique();

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.DownloadKey).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ExistsOnTheServer).HasDefaultValueSql("((1))");
            entity.Property(e => e.FileName).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasComment("");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Table Name");
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<FileUploadSetting>(entity =>
        {
            entity.ToTable("FileUploadSettings", "OBJ");

            entity.Property(e => e.ContentType)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SizeInMegabyte).HasDefaultValueSql("((10))");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Languages", "OBJ");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_SYS_MENUS");

            entity.ToTable("Menus", "OBJ");

            entity.Property(e => e.Defination).HasMaxLength(50);
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Link)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuType)
                .HasDefaultValueSql("((0))")
                .HasComment("0-esas menu, 1-alt, 2-1in alt menusu");
            entity.Property(e => e.Orderby).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<MenuInfo>(entity =>
        {
            entity.ToTable("MenuInfo", "OBJ");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.KeyfieldName)
                .HasMaxLength(50)
                .HasComment("QueryString ile gelen parametr adi");
            entity.Property(e => e.PageName).HasMaxLength(350);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<MenuLang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MENUSLANG");

            entity.ToTable("MenuLangs", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Messages", "OBJ");

            entity.Property(e => e.Definition).HasMaxLength(250);
            entity.Property(e => e.Note).HasMaxLength(250);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<MessageLang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MessagesLang");

            entity.ToTable("MessageLangs", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Value).HasMaxLength(250);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MODULES");

            entity.ToTable("Modules", "OBJ");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<ModuleLang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ModulesLang");

            entity.ToTable("ModuleLangs", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<ObjectContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_SYS_OBJ");

            entity.ToTable("ObjectContents", "OBJ");

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PageName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ObjectContentsLang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_SYS_OBJLANG");

            entity.ToTable("ObjectContentsLang", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Value).HasMaxLength(250);
        });

        modelBuilder.Entity<OperationBlock>(entity =>
        {
            entity.ToTable("OperationBlock", "OBJ");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.ToTable("RequestDetails", "OPR");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_SYS_ROLES");

            entity.ToTable("Roles", "OBJ");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_SYS_ROLEMENUS");

            entity.ToTable("RoleMenus", "OBJ");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<SpeCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SPECODE");

            entity.ToTable("SpeCode", "OBJ");

            entity.HasIndex(e => new { e.Type, e.RefId }, "UK_SpeCode_TypeRefId").IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasMaxLength(200);
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SystemLog");

            entity.ToTable("SystemLogs", "LOG");

            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequestUrl)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERS");

            entity.ToTable("Users", "OBJ");

            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FinCode).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Phone1)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Phone2)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<UserLoginHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LoginHistories");

            entity.ToTable("UserLoginHistories", "OBJ");

            entity.Property(e => e.LoginDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_USERROLES");

            entity.ToTable("UserRoles", "OBJ");

            entity.Property(e => e.Expdate)
                .HasDefaultValueSql("('2099-01-01 00:00:00.000')")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RefreshTokens");

            entity.ToTable("UserTokens", "OBJ");

            entity.Property(e => e.AccessToken)
                .HasMaxLength(800)
                .IsUnicode(false);
            entity.Property(e => e.EndDate)
                .HasComment("AccessToken endate")
                .HasColumnType("datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(800)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Cities", "CRD");

            entity.Property(e => e.Lang).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(250);
        });

        modelBuilder.Entity<VCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Countries", "CRD");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<VSpeCode>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SpeCode", "OBJ");

            entity.Property(e => e.KeyType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Lang).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
