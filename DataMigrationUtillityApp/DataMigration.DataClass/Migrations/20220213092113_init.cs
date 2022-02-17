﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataMigration.DataClass.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SourceTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNumber = table.Column<int>(type: "int", nullable: false),
                    SecondNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DestinationTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sum = table.Column<int>(type: "int", nullable: false),
                    SourceTable = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DestinationTable_SourceTable_SourceTable",
                        column: x => x.SourceTable,
                        principalTable: "SourceTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinationTable_SourceTable",
                table: "DestinationTable",
                column: "SourceTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationTable");

            migrationBuilder.DropTable(
                name: "SourceTable");
        }
    }
}
