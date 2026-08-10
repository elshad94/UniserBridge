using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project.API.Entities.Models;

namespace Project.API.DataAccess.Concrete.EntityFramework.Contexts;

public partial class ProjectAppDbContextBase : DbContext
{
    public ProjectAppDbContextBase()
    {
    }

    public ProjectAppDbContextBase(DbContextOptions<ProjectAppDbContextBase> options)
        : base(options)
    {
    }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageLang> MessageLangs { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.37.33; Database=DB_UniserBridge; User Id=sa;Password=Uniser!@#; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Languages", "OBJ");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
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

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.ToTable("RequestDetails", "OPR");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MethodName).IsUnicode(false);
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
