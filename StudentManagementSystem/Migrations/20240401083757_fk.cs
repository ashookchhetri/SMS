using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacuiltyId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacuiltyId",
                table: "Students",
                column: "FacuiltyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Facuilty_FacuiltyId",
                table: "Students",
                column: "FacuiltyId",
                principalTable: "Facuilty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Facuilty_FacuiltyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacuiltyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacuiltyId",
                table: "Students");
        }
    }
}
