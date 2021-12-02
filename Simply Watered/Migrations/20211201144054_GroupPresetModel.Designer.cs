﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simply_Watered.Data;

namespace Simply_Watered.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211201144054_GroupPresetModel")]
    partial class GroupPresetModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(50000);

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Simply_Watered.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Simply_Watered.Models.DeviceReadings", b =>
                {
                    b.Property<long>("ReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("NormalizedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReadingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<double?>("ReadingHumidity")
                        .HasColumnType("float");

                    b.Property<double?>("ReadingTemp")
                        .HasColumnType("float");

                    b.HasKey("ReadingId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceReadings");
                });

            modelBuilder.Entity("Simply_Watered.Models.DeviceTypes", b =>
                {
                    b.Property<long>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("TypeId");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("Simply_Watered.Models.Devices", b =>
                {
                    b.Property<long>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("IrrigScheduleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("RegionId")
                        .HasColumnType("bigint");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<long?>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceId");

                    b.HasIndex("RegionId");

                    b.HasIndex("TypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Simply_Watered.Models.DevicesSchedules", b =>
                {
                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<long>("ScheduleId")
                        .HasColumnType("bigint");

                    b.HasKey("DeviceId", "ScheduleId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("DevicesSchedules");
                });

            modelBuilder.Entity("Simply_Watered.Models.GroupPresets", b =>
                {
                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<long?>("IrrigModeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("((1))");

                    b.Property<double?>("MaxHumidity")
                        .HasColumnType("float");

                    b.Property<double?>("MinimalHumidity")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeviceId", "GroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("IrrigModeId");

                    b.ToTable("GroupPresets");
                });

            modelBuilder.Entity("Simply_Watered.Models.IrrigationModes", b =>
                {
                    b.Property<long>("IrrigModeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ModeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("IrrigModeId");

                    b.ToTable("IrrigationModes");
                });

            modelBuilder.Entity("Simply_Watered.Models.IrrigationSchedules", b =>
                {
                    b.Property<long>("IrrigScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IrrigScheduleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<DateTime>("ScheduleEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ScheduleStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IrrigScheduleId");

                    b.ToTable("IrrigationSchedules");
                });

            modelBuilder.Entity("Simply_Watered.Models.RegionGroups", b =>
                {
                    b.Property<long>("RegionGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("RegionGroupDescription")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("RegionGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("RegionGroups");
                });

            modelBuilder.Entity("Simply_Watered.Models.Regions", b =>
                {
                    b.Property<long>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegionDescription")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<long?>("RegionGroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegionName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("RegionId");

                    b.HasIndex("RegionGroupId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Simply_Watered.Models.ScheduleTimespans", b =>
                {
                    b.Property<long>("TimespanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<TimeSpan>("Finish")
                        .HasColumnType("time");

                    b.Property<long>("IrrigScheduleId")
                        .HasColumnType("bigint");

                    b.Property<TimeSpan>("Start")
                        .HasColumnType("time");

                    b.HasKey("TimespanId");

                    b.HasIndex("IrrigScheduleId");

                    b.ToTable("ScheduleTimespans");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Simply_Watered.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Simply_Watered.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simply_Watered.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Simply_Watered.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simply_Watered.Models.DeviceReadings", b =>
                {
                    b.HasOne("Simply_Watered.Models.Devices", "Device")
                        .WithMany("DeviceReadings")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("FK_DeviceReadings_Devices")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simply_Watered.Models.Devices", b =>
                {
                    b.HasOne("Simply_Watered.Models.Regions", "Region")
                        .WithMany("Devices")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Devices_Regions")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Simply_Watered.Models.DeviceTypes", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Devices_DeviceTypes")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Simply_Watered.Models.DevicesSchedules", b =>
                {
                    b.HasOne("Simply_Watered.Models.Devices", "Device")
                        .WithMany("DevicesSchedules")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("FK_DevicesSchedules_Devices")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simply_Watered.Models.IrrigationSchedules", "Schedule")
                        .WithMany("DevicesSchedules")
                        .HasForeignKey("ScheduleId")
                        .HasConstraintName("FK_DevicesSchedules_IrrigationSchedules")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simply_Watered.Models.GroupPresets", b =>
                {
                    b.HasOne("Simply_Watered.Models.Devices", "Device")
                        .WithMany("Presets")
                        .HasForeignKey("DeviceId")
                        .HasConstraintName("FK_RegionPresets_Devices")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simply_Watered.Models.RegionGroups", "RegionGroup")
                        .WithMany("Presets")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_RegionPresets_RegionGroups")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Simply_Watered.Models.IrrigationModes", "IrrigMode")
                        .WithMany("Presets")
                        .HasForeignKey("IrrigModeId")
                        .HasConstraintName("FK_Devices_IrrigationModes")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Simply_Watered.Models.RegionGroups", b =>
                {
                    b.HasOne("Simply_Watered.Models.ApplicationUser", "User")
                        .WithMany("RegionGroups")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_RegionGroups_Users")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Simply_Watered.Models.Regions", b =>
                {
                    b.HasOne("Simply_Watered.Models.RegionGroups", "RegionGroup")
                        .WithMany("Regions")
                        .HasForeignKey("RegionGroupId")
                        .HasConstraintName("FK_Regions_RegionGroups")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Simply_Watered.Models.ScheduleTimespans", b =>
                {
                    b.HasOne("Simply_Watered.Models.IrrigationSchedules", "IrrigSchedule")
                        .WithMany("ScheduleTimespans")
                        .HasForeignKey("IrrigScheduleId")
                        .HasConstraintName("FK_ScheduleTimespans_IrrigationSchedules")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
