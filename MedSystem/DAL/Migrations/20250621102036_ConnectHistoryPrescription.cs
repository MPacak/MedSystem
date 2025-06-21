using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConnectHistoryPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalHistoryId",
                table: "Prescriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicalHistoryId",
                table: "Prescriptions",
                column: "MedicalHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_MedicalHistories_MedicalHistoryId",
                table: "Prescriptions",
                column: "MedicalHistoryId",
                principalTable: "MedicalHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_MedicalHistories_MedicalHistoryId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicalHistoryId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "MedicalHistoryId",
                table: "Prescriptions");
        }
    }
}
