using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConnectHistoryCHeckupANdNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalHistoryId",
                table: "Checkups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Checkups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkups_MedicalHistoryId",
                table: "Checkups",
                column: "MedicalHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkups_MedicalHistories_MedicalHistoryId",
                table: "Checkups",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkups_MedicalHistories_MedicalHistoryId",
                table: "Checkups");

            migrationBuilder.DropIndex(
                name: "IX_Checkups_MedicalHistoryId",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Checkups");
        }
    }
}
