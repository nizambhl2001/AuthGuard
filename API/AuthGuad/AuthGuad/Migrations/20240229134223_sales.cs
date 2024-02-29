using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthGuad.Migrations
{
    public partial class sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creditlimit",
                table: "customers");

            migrationBuilder.RenameColumn(
                name: "TaxCode",
                table: "customers",
                newName: "ModifyUser");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "customers",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customers",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "customers");

            migrationBuilder.RenameColumn(
                name: "ModifyUser",
                table: "customers",
                newName: "TaxCode");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "customers",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Creditlimit",
                table: "customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
