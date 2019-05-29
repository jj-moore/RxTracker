﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RxTracker.Data;

namespace RxTracker.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RxTracker.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<string>("Hospital")
                        .HasMaxLength(80);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("UserId");

                    b.HasKey("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctor");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            Address = "East Ann Arbor",
                            Hospital = "University Hospital",
                            Name = "Bob Pharma"
                        },
                        new
                        {
                            DoctorId = 2,
                            Address = "Traverse City, MI",
                            Hospital = "VA Hospital",
                            Name = "Jane Cuts"
                        },
                        new
                        {
                            DoctorId = 3,
                            Address = "Hell, MI",
                            Name = "Dr. Strangelove"
                        });
                });

            modelBuilder.Entity("RxTracker.Models.Drug", b =>
                {
                    b.Property<int>("DrugId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GenericForId");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(80);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("TradeName")
                        .HasMaxLength(80);

                    b.Property<string>("UserId");

                    b.HasKey("DrugId");

                    b.HasIndex("GenericForId");

                    b.HasIndex("UserId");

                    b.ToTable("Drug");

                    b.HasData(
                        new
                        {
                            DrugId = 1,
                            Manufacturer = "Pfizer",
                            Name = "Atorvastatin",
                            TradeName = "Lipitor"
                        },
                        new
                        {
                            DrugId = 2,
                            GenericForId = 1,
                            Name = "Atorvastatin"
                        },
                        new
                        {
                            DrugId = 3,
                            Manufacturer = "Reckitt Benckiser",
                            Name = "Buprenorphine/naloxone",
                            TradeName = "Suboxone"
                        },
                        new
                        {
                            DrugId = 4,
                            Manufacturer = "Pfizer",
                            Name = "Pregabalin",
                            TradeName = "Lyrica"
                        },
                        new
                        {
                            DrugId = 5,
                            GenericForId = 3,
                            Name = "Buprenorphine/naloxone"
                        });
                });

            modelBuilder.Entity("RxTracker.Models.MyUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("DOB");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
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

            modelBuilder.Entity("RxTracker.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(80);

                    b.HasKey("PatientId");

                    b.ToTable("Patient");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            DateOfBirth = new DateTime(1965, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mary@example.com",
                            FirstName = "Mary",
                            LastName = "Smith",
                            PhoneNumber = "5556567890"
                        },
                        new
                        {
                            PatientId = 2,
                            DateOfBirth = new DateTime(1950, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mickey@example.com",
                            FirstName = "Mickey",
                            LastName = "Mouse",
                            PhoneNumber = "1237894560"
                        });
                });

            modelBuilder.Entity("RxTracker.Models.Pharmacy", b =>
                {
                    b.Property<int>("PharmacyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("UserId");

                    b.HasKey("PharmacyId");

                    b.HasIndex("UserId");

                    b.ToTable("Pharmacy");

                    b.HasData(
                        new
                        {
                            PharmacyId = 1,
                            Address = "Carpenter Rd, Ypsilanti",
                            Name = "Meijer"
                        },
                        new
                        {
                            PharmacyId = 2,
                            Address = "Mail Order",
                            Name = "CVS Caremark"
                        },
                        new
                        {
                            PharmacyId = 3,
                            Address = "Whittaker Rd, Ypsilanti, MI",
                            Name = "CVS"
                        });
                });

            modelBuilder.Entity("RxTracker.Models.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Active")
                        .HasColumnType("tinyint");

                    b.Property<int>("DoctorId");

                    b.Property<string>("Dosage")
                        .HasMaxLength(80);

                    b.Property<int>("DrugId");

                    b.Property<string>("Form")
                        .HasMaxLength(80);

                    b.Property<int>("PatientId");

                    b.Property<string>("Regimen")
                        .HasMaxLength(80);

                    b.Property<string>("UserId");

                    b.HasKey("PrescriptionId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("DrugId");

                    b.HasIndex("PatientId");

                    b.HasIndex("UserId");

                    b.ToTable("Prescription");

                    b.HasData(
                        new
                        {
                            PrescriptionId = 1,
                            Active = (byte)1,
                            DoctorId = 1,
                            Dosage = "10mg/12.5mg",
                            DrugId = 2,
                            Form = "Sublingual Strip",
                            PatientId = 2,
                            Regimen = "Half strip, twice daily"
                        },
                        new
                        {
                            PrescriptionId = 2,
                            Active = (byte)1,
                            DoctorId = 3,
                            Dosage = "60mg",
                            DrugId = 4,
                            Form = "Tablet",
                            PatientId = 1,
                            Regimen = "Once daily"
                        },
                        new
                        {
                            PrescriptionId = 3,
                            Active = (byte)1,
                            DoctorId = 3,
                            Dosage = "15mg/20mg",
                            DrugId = 5,
                            Form = "Tablet",
                            PatientId = 1,
                            Regimen = "Two tablets daily, morning and evening"
                        },
                        new
                        {
                            PrescriptionId = 4,
                            Active = (byte)1,
                            DoctorId = 2,
                            Dosage = "100mg",
                            DrugId = 2,
                            Form = "Tablet",
                            PatientId = 1,
                            Regimen = "Once daily"
                        },
                        new
                        {
                            PrescriptionId = 5,
                            Active = (byte)0,
                            DoctorId = 1,
                            Dosage = "100mg",
                            DrugId = 1,
                            Form = "Capsule",
                            PatientId = 2,
                            Regimen = "Once daily"
                        });
                });

            modelBuilder.Entity("RxTracker.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Cost")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime?>("DateFilled")
                        .HasColumnType("datetime");

                    b.Property<string>("DiscountUsed")
                        .HasMaxLength(80);

                    b.Property<string>("InsuranceUsed")
                        .HasMaxLength(80);

                    b.Property<int>("PharmacyId");

                    b.Property<int>("PrescriptionId");

                    b.HasKey("TransactionId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("Transaction");

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            Cost = 20m,
                            DateFilled = new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "BCBS",
                            PharmacyId = 2,
                            PrescriptionId = 1
                        },
                        new
                        {
                            TransactionId = 2,
                            Cost = 25m,
                            DateFilled = new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "BCBS",
                            PharmacyId = 3,
                            PrescriptionId = 1
                        },
                        new
                        {
                            TransactionId = 3,
                            Cost = 20m,
                            DateFilled = new DateTime(2019, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "BCBS",
                            PharmacyId = 2,
                            PrescriptionId = 1
                        },
                        new
                        {
                            TransactionId = 4,
                            Cost = 50m,
                            DateFilled = new DateTime(2018, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountUsed = "GoodRx",
                            InsuranceUsed = "Aetna",
                            PharmacyId = 1,
                            PrescriptionId = 5
                        },
                        new
                        {
                            TransactionId = 5,
                            Cost = 32.99m,
                            DateFilled = new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountUsed = "Manufacturer's Coupon",
                            InsuranceUsed = "Molina",
                            PharmacyId = 1,
                            PrescriptionId = 2
                        },
                        new
                        {
                            TransactionId = 6,
                            Cost = 30.65m,
                            DateFilled = new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountUsed = "Manufacturer's Coupon",
                            InsuranceUsed = "Molina",
                            PharmacyId = 1,
                            PrescriptionId = 2
                        },
                        new
                        {
                            TransactionId = 7,
                            Cost = 41.82m,
                            DateFilled = new DateTime(2019, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "Molina",
                            PharmacyId = 3,
                            PrescriptionId = 3
                        },
                        new
                        {
                            TransactionId = 8,
                            Cost = 26.62m,
                            DateFilled = new DateTime(2019, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "Molina",
                            PharmacyId = 2,
                            PrescriptionId = 3
                        },
                        new
                        {
                            TransactionId = 9,
                            Cost = 12.55m,
                            DateFilled = new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DiscountUsed = "GoodRx Gold",
                            PharmacyId = 2,
                            PrescriptionId = 4
                        },
                        new
                        {
                            TransactionId = 10,
                            Cost = 9.82m,
                            DateFilled = new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            InsuranceUsed = "Molina",
                            PharmacyId = 1,
                            PrescriptionId = 4
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RxTracker.Models.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RxTracker.Models.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RxTracker.Models.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RxTracker.Models.MyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RxTracker.Models.Doctor", b =>
                {
                    b.HasOne("RxTracker.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RxTracker.Models.Drug", b =>
                {
                    b.HasOne("RxTracker.Models.Drug", "GenericFor")
                        .WithMany()
                        .HasForeignKey("GenericForId");

                    b.HasOne("RxTracker.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RxTracker.Models.Pharmacy", b =>
                {
                    b.HasOne("RxTracker.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RxTracker.Models.Prescription", b =>
                {
                    b.HasOne("RxTracker.Models.Doctor", "Doctor")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RxTracker.Models.Drug", "Drug")
                        .WithMany("Prescriptions")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RxTracker.Models.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RxTracker.Models.MyUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("RxTracker.Models.Transaction", b =>
                {
                    b.HasOne("RxTracker.Models.Pharmacy", "Pharmacy")
                        .WithMany("Transactions")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RxTracker.Models.Prescription", "Prescription")
                        .WithMany("Transactions")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
