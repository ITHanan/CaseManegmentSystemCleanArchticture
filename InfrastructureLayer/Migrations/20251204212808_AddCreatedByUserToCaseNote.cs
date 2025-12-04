using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByUserToCaseNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "CaseNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_CreatedByUserId",
                table: "CaseNotes",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseNotes_Users_CreatedByUserId",
                table: "CaseNotes",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseNotes_Users_CreatedByUserId",
                table: "CaseNotes");

            migrationBuilder.DropIndex(
                name: "IX_CaseNotes_CreatedByUserId",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CaseNotes");
        }
    }
}
