using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MarketingMap.Models;

public partial class MarketingSupportContext : DbContext
{
    public MarketingSupportContext()
    {
    }

    public MarketingSupportContext(DbContextOptions<MarketingSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Marker> Markers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MarketingSupport;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LocationId }).HasName("PK_Locations");

            entity.ToTable("locations");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("location_id");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lng).HasColumnName("lng");
            entity.Property(e => e.LocationName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("location_name");
        });

        modelBuilder.Entity<Marker>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MarkerId }).HasName("PK_Markers");

            entity.ToTable("markers");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.MarkerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("marker_id");
            entity.Property(e => e.ContactId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("contact_id");
            entity.Property(e => e.ImageId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("image_id");
            entity.Property(e => e.Label)
                .HasColumnType("text")
                .HasColumnName("label");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lng).HasColumnName("lng");
            entity.Property(e => e.Title)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
