using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMigration.DataClass.Migrations
{
    public partial class addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DestinationTable_SourceTableId",
                table: "DestinationTable",
                column: "SourceTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTableId",
                table: "DestinationTable",
                column: "SourceTableId",
                principalTable: "SourceTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTableId",
                table: "DestinationTable");

            migrationBuilder.DropIndex(
                name: "IX_DestinationTable_SourceTableId",
                table: "DestinationTable");
        }
    }
}
