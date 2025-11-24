using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MarketingManager.Models;

public partial class MarketingSupportContext : DbContext
{
    public MarketingSupportContext()
    {
    }

    public MarketingSupportContext(DbContextOptions<MarketingSupportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Marker> Markers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MarketingSupport;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ContactId }).HasName("PK_Contacts");

            entity.ToTable("contacts");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.ContactId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("contact_id");
            entity.Property(e => e.Comments)
                .HasColumnType("text")
                .HasColumnName("comments");
            entity.Property(e => e.CompanyAddress1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_address_1");
            entity.Property(e => e.CompanyAddress2)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_address_2");
            entity.Property(e => e.CompanyCity)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_city");
            entity.Property(e => e.CompanyCountry)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_country");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.CompanyPostalCode)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_postal_code");
            entity.Property(e => e.CompanyStateOrRegion)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("company_state_or_region");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("contact__email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("contact_phone");
            entity.Property(e => e.ContactWebSite)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("contact_web_site");
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("location_id");
            entity.Property(e => e.MarkerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("marker_id");
            entity.Property(e => e.PointOfContact)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("point_of_contact");
            entity.Property(e => e.Status)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("status");
        });

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
            entity.Property(e => e.LocationId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .HasColumnName("location_id");
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
