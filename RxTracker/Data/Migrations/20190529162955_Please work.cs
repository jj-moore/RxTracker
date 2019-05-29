using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RxTracker.Data.Migrations
{
    public partial class Pleasework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_AspNetUsers_IdentityUserId",
                table: "Prescription");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Prescription",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IdentityUserId",
                table: "Prescription",
                newName: "IX_Prescription_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFilled",
                table: "Transaction",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Transaction",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pharmacy",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Drug",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Doctor",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacy_UserId",
                table: "Pharmacy",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Drug_UserId",
                table: "Drug",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Drug_AspNetUsers_UserId",
                table: "Drug",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacy_AspNetUsers_UserId",
                table: "Pharmacy",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_AspNetUsers_UserId",
                table: "Prescription",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_AspNetUsers_UserId",
                table: "Doctor");

            migrationBuilder.DropForeignKey(
                name: "FK_Drug_AspNetUsers_UserId",
                table: "Drug");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacy_AspNetUsers_UserId",
                table: "Pharmacy");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_AspNetUsers_UserId",
                table: "Prescription");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacy_UserId",
                table: "Pharmacy");

            migrationBuilder.DropIndex(
                name: "IX_Drug_UserId",
                table: "Drug");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_UserId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Drug");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Prescription",
                newName: "IdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_UserId",
                table: "Prescription",
                newName: "IX_Prescription_IdentityUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFilled",
                table: "Transaction",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Transaction",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_AspNetUsers_IdentityUserId",
                table: "Prescription",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
