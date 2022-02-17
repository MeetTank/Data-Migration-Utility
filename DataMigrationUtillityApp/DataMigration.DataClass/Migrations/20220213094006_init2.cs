using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMigration.DataClass.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTable",
                table: "DestinationTable");

            migrationBuilder.DropIndex(
                name: "IX_DestinationTable_SourceTable",
                table: "DestinationTable");

            migrationBuilder.DropColumn(
                name: "SourceTable",
                table: "DestinationTable");

            migrationBuilder.AddColumn<int>(
                name: "SourceTableId",
                table: "DestinationTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceTableId",
                table: "DestinationTable");

            migrationBuilder.AddColumn<int>(
                name: "SourceTable",
                table: "DestinationTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTable_SourceTable",
                table: "DestinationTable",
                column: "SourceTable");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationTable_SourceTable_SourceTable",
                table: "DestinationTable",
                column: "SourceTable",
                principalTable: "SourceTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
