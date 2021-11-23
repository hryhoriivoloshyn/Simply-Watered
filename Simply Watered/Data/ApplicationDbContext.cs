using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Simply_Watered.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simply_Watered.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }


        public virtual DbSet<DeviceReadings> DeviceReadings { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<IrrigationModes> IrrigationModes { get; set; }
        public virtual DbSet<IrrigationSchedules> IrrigationSchedules { get; set; }
        public virtual DbSet<RegionGroups> RegionGroups { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<ScheduleTimespans> ScheduleTimespans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-AB6K1OD;Initial Catalog=SimplyWatereddb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceReadings>(entity =>
            {
                entity.HasKey(e => e.ReadingId);

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceReadings)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_DeviceReadings_Devices");
            });

            modelBuilder.Entity<Devices>(entity =>
            {
                entity.HasKey(e => e.DeviceId);

                entity.Property(e => e.DeviceDescription).HasMaxLength(450);

                entity.Property(e => e.DeviceName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IrrigModeId).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IrrigMode)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.IrrigModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devices_IrrigationModes");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Devices_Regions");
            });

            modelBuilder.Entity<IrrigationModes>(entity =>
            {
                entity.HasKey(e => e.IrrigModeId);

                entity.Property(e => e.ModeName)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<IrrigationSchedules>(entity =>
            {
                entity.HasKey(e => e.IrrigScheduleId);

                entity.Property(e => e.IrrigScheduleName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.IrrigationSchedules)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_IrrigationSchedules_Devices");
            });

            modelBuilder.Entity<RegionGroups>(entity =>
            {
                entity.HasKey(e => e.RegionGroupId);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.RegionGroups)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_RegionGroups_Users");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.RegionGroupDescription).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.Property(e => e.RegionDescription).HasMaxLength(450);

                entity.Property(e => e.RegionName).HasMaxLength(450);

                entity.HasOne(d => d.RegionGroup)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.RegionGroupId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Regions_RegionGroups");
            });

            modelBuilder.Entity<ScheduleTimespans>(entity =>
            {
                entity.HasKey(e => e.TimespanId);

                entity.HasOne(d => d.IrrigSchedule)
                    .WithMany(p => p.ScheduleTimespans)
                    .HasForeignKey(d => d.IrrigScheduleId)
                    .HasConstraintName("FK_ScheduleTimespans_IrrigationSchedules");
            });

            
        }



    }
}
