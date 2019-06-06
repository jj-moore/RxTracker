using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Hospital = table.Column<string>(maxLength: 80, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drug",
                columns: table => new
                {
                    DrugId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    TradeName = table.Column<string>(maxLength: 80, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 80, nullable: true),
                    GenericForId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug", x => x.DrugId);
                    table.ForeignKey(
                        name: "FK_Drug_Drug_GenericForId",
                        column: x => x.GenericForId,
                        principalTable: "Drug",
                        principalColumn: "DrugId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drug_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.PharmacyId);
                    table.ForeignKey(
                        name: "FK_Pharmacy_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(nullable: false),
                    DrugId = table.Column<int>(nullable: false),
                    Active = table.Column<byte>(type: "tinyint", nullable: false),
                    Form = table.Column<string>(maxLength: 80, nullable: true),
                    Dosage = table.Column<string>(maxLength: 80, nullable: true),
                    Regimen = table.Column<string>(maxLength: 80, nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescription_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescription_Drug_DrugId",
                        column: x => x.DrugId,
                        principalTable: "Drug",
                        principalColumn: "DrugId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescription_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrescriptionId = table.Column<int>(nullable: false),
                    PharmacyId = table.Column<int>(nullable: false),
                    DateFilled = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    InsuranceUsed = table.Column<string>(maxLength: 80, nullable: true),
                    DiscountUsed = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "PharmacyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Prescription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescription",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DOB", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c0a85163-ea56-4558-b867-6c2e0fc0b0ce", 0, "afd7e1ee-b8b8-4511-bd35-86258377080c", new DateTime(1972, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", false, "Jane", "Doe", true, null, "JANE@EXAMPLE.COM", "JANE@EXAMPLE.COM", "AQAAAAEAACcQAAAAEO/KSAhUbDv0li7Zz8F8zT+MFgyjIGfAoYchDplrmEYCeISgk4kXRJmzAItbC4dwdw==", "734-555-1234", false, "53b6c7a3-17fb-4b85-8a9f-c9474246b38d", false, "jane@example.com" });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Address", "Hospital", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "East Ann Arbor", "University Hospital", "Bob Pharma", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 2, "Traverse City, MI", "VA Hospital", "Mary Cutz", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 3, "Hell, MI", null, "Dr. Feelgood", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 4, "Sirius Beta", null, "Douglas Adams", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName", "UserId" },
                values: new object[,]
                {
                    { 14, null, null, "Oxybutynin", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 13, null, null, "Trazodone", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 12, null, null, "Lisonopril HCL", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 10, null, null, "Fluoxetine", "Prozac", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 9, null, null, "Estradiol", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 6, null, null, "Methylphenidate", "Ritalin", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 4, null, "Pfizer", "Pregabalin", "Lyrica", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 3, null, "Reckitt Benckiser", "Buprenorphine/naloxone", "Suboxone", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 1, null, "Pfizer", "Atorvastatin", "Lipitor", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 8, null, null, "Beclomethasone", "QVAR", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacy",
                columns: new[] { "PharmacyId", "Address", "Name", "UserId" },
                values: new object[,]
                {
                    { 4, "Whittaker Rd, Ypsilanti, MI", "Kroger", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 1, "Carpenter Rd, Ypsilanti", "Meijer", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 2, "Mail Order", "CVS Caremark", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 3, "Whittaker Rd, Ypsilanti, MI", "CVS", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 5, "Ellwworth Rd, Pittsfield, MI", "Costco", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName", "UserId" },
                values: new object[,]
                {
                    { 2, 1, null, "Atorvastatin", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 5, 3, null, "Buprenorphine/naloxone", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 7, 6, null, "Methylphenidate", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 11, 10, null, "Fluoxetine", null, "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "Regimen", "UserId" },
                values: new object[,]
                {
                    { 1, (byte)0, 1, "25mg", 1, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 3, (byte)0, 3, "15mg/20mg", 3, "Sublingual Strip", "Half strip, twice daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 2, (byte)1, 3, "60mg", 4, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 6, (byte)0, 4, "60mcg", 8, "Inhaler", "Two puffs, twice daily or as needed", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 7, (byte)1, 2, "0.1mg", 9, "Patch", "Once weekly", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 13, (byte)1, 3, "100mg", 10, "Capsule", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 9, (byte)1, 1, "10-12.5mg", 12, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 10, (byte)1, 1, "100mg", 13, "Capsule", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "Regimen", "UserId" },
                values: new object[,]
                {
                    { 16, (byte)1, 1, "20mg", 2, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 12, (byte)1, 3, "100mg", 7, "Capsule", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 8, (byte)0, 3, "25mg", 11, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 15, (byte)1, 3, "10mg/15mg", 5, "Tablet", "Two tablets daily, morning and evening", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 14, (byte)1, 3, "50mg", 11, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" },
                    { 4, (byte)1, 2, "100mg", 2, "Tablet", "Once daily", "c0a85163-ea56-4558-b867-6c2e0fc0b0ce" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "Cost", "DateFilled", "DiscountUsed", "InsuranceUsed", "PharmacyId", "PrescriptionId" },
                values: new object[,]
                {
                    { 25, 84.99m, new DateTime(2019, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good Rx", null, 1, 7 },
                    { 24, 82.69m, new DateTime(2018, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Good Rx", null, 4, 7 },
                    { 23, 20.99m, new DateTime(2019, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "GoodRx", "Molina", 5, 6 },
                    { 22, 30.99m, new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 3, 6 },
                    { 21, 33.34m, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 3, 6 },
                    { 20, 32.33m, new DateTime(2018, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 3, 6 },
                    { 3, 20m, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 1 },
                    { 2, 20m, new DateTime(2019, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 1 },
                    { 13, 60m, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 2, 2 },
                    { 1, 20m, new DateTime(2018, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 1 },
                    { 11, 47.32m, new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 2 },
                    { 10, 45.08m, new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 2 },
                    { 7, 75.67m, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 3 },
                    { 6, 78.32m, new DateTime(2019, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 3 },
                    { 14, 58.50m, new DateTime(2019, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 2, 2 },
                    { 12, 47.32m, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "Cost", "DateFilled", "DiscountUsed", "InsuranceUsed", "PharmacyId", "PrescriptionId" },
                values: new object[,]
                {
                    { 4, 0m, new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free at Meijer", "None", 1, 16 },
                    { 5, 0m, new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free at Meijer", "None", 1, 16 },
                    { 8, 45.99m, new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 5, 15 },
                    { 9, 25m, new DateTime(2019, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer Coupon", "Molina", 5, 15 },
                    { 15, 3.55m, new DateTime(2018, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 12 },
                    { 16, 2.85m, new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 12 },
                    { 17, 3.80m, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 4, 12 },
                    { 18, 3.80m, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 4, 12 },
                    { 19, 25m, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 3, 12 },
                    { 26, 1.85m, new DateTime(2018, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 8 },
                    { 27, 1.99m, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 8 },
                    { 28, 2.55m, new DateTime(2019, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "BCBS", null, 4, 14 },
                    { 29, 2.55m, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 4, 14 },
                    { 30, 2.85m, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 4, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_GenericForId",
                table: "Drug",
                column: "GenericForId");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_UserId",
                table: "Drug",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_UserId",
                table: "Pharmacy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DoctorId",
                table: "Prescription",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_DrugId",
                table: "Prescription",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_UserId",
                table: "Prescription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PharmacyId",
                table: "Transaction",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PrescriptionId",
                table: "Transaction",
                column: "PrescriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Drug");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
