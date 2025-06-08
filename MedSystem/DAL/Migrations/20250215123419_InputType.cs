using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InputType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 1,
                column: "DoseType",
                value: "MG"
);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 2,
                column: "DoseType",
                value: "MG"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 3,
                column: "DoseType",
                value: "MG"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 4,
                column: "DoseType",
                value: "MG"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 5,
                column: "DoseType",
                value: "MG"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 6,
                column: "DoseType",
                value: "MG"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 7,
                column: "DoseType",
                value: "TAB"
            );

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 8,
                column: "DoseType",
                value: "MG"
            );
            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 9, 
                column: "DoseType",
                value: "MCG"  
);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "Id",
                keyValue: 10,  
                column: "DoseType",
                value: "MG"  
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "DoseType",
               table: "Drugs"
                );
        }
    }
}
