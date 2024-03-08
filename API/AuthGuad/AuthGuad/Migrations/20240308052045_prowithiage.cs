using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthGuad.Migrations
{
    public partial class prowithiage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PImage",
                table: "tblproducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PImage",
                table: "tblproducts");
        }
    }
}
