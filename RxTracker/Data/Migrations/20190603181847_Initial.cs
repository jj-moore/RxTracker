using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxTracker.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

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
                    UserId = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
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
                values: new object[] { "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8", 0, "93c511c6-fd5f-4b3e-b4aa-a5602ec908e5", new DateTime(1972, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", false, "Jane", "Doe", true, null, "JANE@EXAMPLE.COM", "JANE@EXAMPLE.COM", "AQAAAAEAACcQAAAAEKcTyN1Z/Z+7Rp3oPfKhdJHrVDfX/xYEfNm8X2w3dOa7yM1Su9xEZhnF7ooHByTi/w==", "734-555-1234", false, "075b2c3b-4f10-4c91-a96c-3cfe4e8d6f50", false, "jane@example.com" });

            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Address", "Hospital", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "East Ann Arbor", "University Hospital", "Bob Pharma", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 2, "Traverse City, MI", "VA Hospital", "Jane Cuts", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 3, "Hell, MI", null, "Dr. Strangelove", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Pfizer", "Atorvastatin", "Lipitor", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 3, null, "Reckitt Benckiser", "Buprenorphine/naloxone", "Suboxone", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 4, null, "Pfizer", "Pregabalin", "Lyrica", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacy",
                columns: new[] { "PharmacyId", "Address", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Carpenter Rd, Ypsilanti", "Meijer", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 2, "Mail Order", "CVS Caremark", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 3, "Whittaker Rd, Ypsilanti, MI", "CVS", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName", "UserId" },
                values: new object[,]
                {
                    { 2, 1, null, "Atorvastatin", null, "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 5, 3, null, "Buprenorphine/naloxone", null, "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "Regimen", "UserId" },
                values: new object[,]
                {
                    { 5, (byte)0, 1, "100mg", 1, "Capsule", "Once daily", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 2, (byte)1, 3, "60mg", 4, "Tablet", "Once daily", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "Regimen", "UserId" },
                values: new object[,]
                {
                    { 1, (byte)1, 1, "10mg/12.5mg", 2, "Sublingual Strip", "Half strip, twice daily", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 4, (byte)1, 2, "100mg", 2, "Tablet", "Once daily", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" },
                    { 3, (byte)1, 3, "15mg/20mg", 5, "Tablet", "Two tablets daily, morning and evening", "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "Cost", "DateFilled", "DiscountUsed", "InsuranceUsed", "PharmacyId", "PrescriptionId" },
                values: new object[,]
                {
                    { 4, 50m, new DateTime(2018, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "GoodRx", "Aetna", 1, 5 },
                    { 5, 32.99m, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer's Coupon", "Molina", 1, 2 },
                    { 6, 30.65m, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer's Coupon", "Molina", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "Cost", "DateFilled", "DiscountUsed", "InsuranceUsed", "PharmacyId", "PrescriptionId" },
                values: new object[,]
                {
                    { 1, 20m, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 1 },
                    { 2, 25m, new DateTime(2019, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 3, 1 },
                    { 3, 20m, new DateTime(2019, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BCBS", 2, 1 },
                    { 9, 12.55m, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "GoodRx Gold", null, 2, 4 },
                    { 10, 9.82m, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 1, 4 },
                    { 7, 41.82m, new DateTime(2019, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 3, 3 },
                    { 8, 26.62m, new DateTime(2019, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Molina", 2, 3 }
                });

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
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Pharmacy");

            migrationBuilder.DropTable(
                name: "Prescription");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Drug");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e7cdd9e-601b-4b8d-9921-8de88f1e61e8");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
