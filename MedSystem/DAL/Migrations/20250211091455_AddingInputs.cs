using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingInputs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "Diseases",
       columns: new[] { "Id", "Name" },
       values: new object[,]
       {
            { 1, "Influenza" },
            { 2, "Diabetes" },
            { 3, "Hypertension" },
            { 4, "Asthma" },
            { 5, "Pneumonia" },
            { 6, "Arthritis" },
            { 7, "Migraine" },
            { 8, "Tuberculosis" },
            { 9, "Chronic Kidney Disease" },
            { 10, "Coronary Artery Disease" }
       });
            migrationBuilder.InsertData(
            table: "Drugs",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Paracetamol" },
                { 2, "Ibuprofen" },
                { 3, "Aspirin" },
                { 4, "Metformin" },
                { 5, "Atorvastatin" },
                { 6, "Omeprazole" },
                { 7, "Amoxicillin" },
                { 8, "Losartan" },
                { 9, "Levothyroxine" },
                { 10, "Salbutamol" }
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.DeleteData(
                 table: "Drugs",
                 keyColumn: "Id",
                 keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
             );

            migrationBuilder.DeleteData(
                table: "Diseases",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            );
        }
    }
}
