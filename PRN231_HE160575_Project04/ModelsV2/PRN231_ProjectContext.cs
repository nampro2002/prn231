using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class PRN231_ProjectContext : DbContext
    {
        public PRN231_ProjectContext()
        {
        }

        public PRN231_ProjectContext(DbContextOptions<PRN231_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdministrativeRegion> AdministrativeRegions { get; set; } = null!;
        public virtual DbSet<AdministrativeUnit> AdministrativeUnits { get; set; } = null!;
        public virtual DbSet<BookingHistory> BookingHistories { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<House> Houses { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = PRN231_Project;uid=sa;pwd=123456;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdministrativeRegion>(entity =>
            {
                entity.Property(e => e.CodeName).HasColumnName("Code_name");

                entity.Property(e => e.CodeNameEn).HasColumnName("Code_name_en");

                entity.Property(e => e.NameEn).HasColumnName("Name_en");
            });

            modelBuilder.Entity<AdministrativeUnit>(entity =>
            {
                entity.Property(e => e.CodeName).HasColumnName("Code_name");

                entity.Property(e => e.CodeNameEn).HasColumnName("Code_name_en");

                entity.Property(e => e.FullName).HasColumnName("Full_name");

                entity.Property(e => e.FullNameEn).HasColumnName("Full_name_en");

                entity.Property(e => e.ShortName).HasColumnName("Short_name");

                entity.Property(e => e.ShortNameEn).HasColumnName("Short_name_en");
            });

            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.HasKey(e => e.BookingId)
                    .HasName("PK__BookingH__73951AED15F76F6A");

                entity.ToTable("BookingHistory");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Price).IsUnicode(false);

                entity.HasOne(d => d.House)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.HouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingHi__House__5CD6CB2B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookingHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookingHi__UserI__5DCAEF64");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("Administrative_unit_id");

                entity.Property(e => e.CodeName).HasColumnName("Code_name");

                entity.Property(e => e.FullName).HasColumnName("Full_name");

                entity.Property(e => e.FullNameEn).HasColumnName("Full_name_en");

                entity.Property(e => e.NameEn).HasColumnName("Name_en");

                entity.Property(e => e.ProvinceCode)
                    .HasMaxLength(20)
                    .HasColumnName("Province_code");

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.AdministrativeUnitId);

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceCode);
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.Property(e => e.DistrictCode).HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProvinceCode).HasMaxLength(20);

                entity.Property(e => e.WardCode).HasMaxLength(20);

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.DistrictCode);

                entity.HasOne(d => d.ProvinceCodeNavigation)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.ProvinceCode);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.WardCodeNavigation)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.WardCode);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.AdministrativeRegionId).HasColumnName("Administrative_region_id");

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("Administrative_unit_id");

                entity.Property(e => e.CodeName).HasColumnName("Code_name");

                entity.Property(e => e.FullName).HasColumnName("Full_name");

                entity.Property(e => e.FullNameEn).HasColumnName("Full_name_en");

                entity.Property(e => e.NameEn).HasColumnName("Name_en");

                entity.HasOne(d => d.AdministrativeRegion)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.AdministrativeRegionId);

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.AdministrativeUnitId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.IsActived)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsVerified)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.AdministrativeUnitId).HasColumnName("Administrative_unit_id");

                entity.Property(e => e.CodeName).HasColumnName("Code_name");

                entity.Property(e => e.DistrictCode)
                    .HasMaxLength(20)
                    .HasColumnName("District_code");

                entity.Property(e => e.FullName).HasColumnName("Full_name");

                entity.Property(e => e.FullNameEn).HasColumnName("Full_name_en");

                entity.Property(e => e.NameEn).HasColumnName("Name_en");

                entity.HasOne(d => d.AdministrativeUnit)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.AdministrativeUnitId);

                entity.HasOne(d => d.DistrictCodeNavigation)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictCode);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
