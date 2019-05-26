using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxTracker.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Address", "Hospital", "Name" },
                values: new object[,]
                {
                    { 1, "East Ann Arbor", "University Hospital", "Bob Pharma" },
                    { 2, "Traverse City, MI", "VA Hospital", "Jane Cuts" },
                    { 3, "Hell, MI", null, "Dr. Strangelove" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName" },
                values: new object[,]
                {
                    { 1, null, "Pfizer", "Atorvastatin", "Lipitor" },
                    { 3, null, "Reckitt Benckiser", "Buprenorphine/naloxone", "Suboxone" },
                    { 4, null, "Pfizer", "Pregabalin", "Lyrica" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(1965, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mary@example.com", "Mary", "Smith", "5556567890" },
                    { 2, new DateTime(1950, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "mickey@example.com", "Mickey", "Mouse", "1237894560" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacy",
                columns: new[] { "PharmacyId", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Carpenter Rd, Ypsilanti", "Meijer" },
                    { 2, "Mail Order", "CVS Caremark" },
                    { 3, "Whittaker Rd, Ypsilanti, MI", "CVS" }
                });

            migrationBuilder.InsertData(
                table: "Drug",
                columns: new[] { "DrugId", "GenericForId", "Manufacturer", "Name", "TradeName" },
                values: new object[,]
                {
                    { 2, 1, null, "Atorvastatin", null },
                    { 5, 3, null, "Buprenorphine/naloxone", null }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "IdentityUserId", "PatientId", "Regimen" },
                values: new object[,]
                {
                    { 2, (byte)1, 3, "60mg", 4, "Tablet", null, 1, "Once daily" },
                    { 5, (byte)0, 1, "100mg", 1, "Capsule", null, 2, "Once daily" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "PrescriptionId", "Active", "DoctorId", "Dosage", "DrugId", "Form", "IdentityUserId", "PatientId", "Regimen" },
                values: new object[,]
                {
                    { 1, (byte)1, 1, "10mg/12.5mg", 2, "Sublingual Strip", null, 2, "Half strip, twice daily" },
                    { 4, (byte)1, 2, "100mg", 2, "Tablet", null, 1, "Once daily" },
                    { 3, (byte)1, 3, "15mg/20mg", 5, "Tablet", null, 1, "Two tablets daily, morning and evening" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "Cost", "DateFilled", "DiscountUsed", "InsuranceUsed", "PharmacyId", "PrescriptionId" },
                values: new object[,]
                {
                    { 5, 32.99m, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer's Coupon", "Molina", 1, 2 },
                    { 6, 30.65m, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer's Coupon", "Molina", 1, 2 },
                    { 4, 50m, new DateTime(2018, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "GoodRx", "Aetna", 1, 5 }
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pharmacy",
                keyColumn: "PharmacyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pharmacy",
                keyColumn: "PharmacyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pharmacy",
                keyColumn: "PharmacyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drug",
                keyColumn: "DrugId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drug",
                keyColumn: "DrugId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Drug",
                keyColumn: "DrugId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drug",
                keyColumn: "DrugId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drug",
                keyColumn: "DrugId",
                keyValue: 3);
        }
    }
}
