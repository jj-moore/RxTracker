using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxTracker.Data.Migrations
{
    public partial class Removepatienttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_PatientId",
                table: "Prescription");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Prescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Prescription",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(maxLength: 80, nullable: false),
                    FirstName = table.Column<string>(maxLength: 80, nullable: false),
                    LastName = table.Column<string>(maxLength: 80, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, new DateTime(1965, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "mary@example.com", "Mary", "Smith", "5556567890" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 2, new DateTime(1950, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "mickey@example.com", "Mickey", "Mouse", "1237894560" });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 1,
                column: "PatientId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 2,
                column: "PatientId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 3,
                column: "PatientId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 4,
                column: "PatientId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "PrescriptionId",
                keyValue: 5,
                column: "PatientId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Prescription_PatientId",
                table: "Prescription",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_PatientId",
                table: "Prescription",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
