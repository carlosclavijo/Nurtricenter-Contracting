using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contracting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrators",
                columns: table => new
                {
                    administratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrators", x => x.administratorId);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.patientId);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    contractId = table.Column<Guid>(type: "uuid", nullable: false),
                    administratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    patientId = table.Column<Guid>(type: "uuid", nullable: false),
                    transactionType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    transactionStatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    creationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    startDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    completedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    totalCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.contractId);
                    table.ForeignKey(
                        name: "FK_contracts_administrators_administratorId",
                        column: x => x.administratorId,
                        principalTable: "administrators",
                        principalColumn: "administratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contracts_patients_patientId",
                        column: x => x.patientId,
                        principalTable: "patients",
                        principalColumn: "patientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliverydays",
                columns: table => new
                {
                    deliveryDayId = table.Column<Guid>(type: "uuid", nullable: false),
                    contractId = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliverydays", x => x.deliveryDayId);
                    table.ForeignKey(
                        name: "FK_deliverydays_contracts_contractId",
                        column: x => x.contractId,
                        principalTable: "contracts",
                        principalColumn: "contractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contracts_administratorId",
                table: "contracts",
                column: "administratorId");

            migrationBuilder.CreateIndex(
                name: "IX_contracts_patientId",
                table: "contracts",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_deliverydays_contractId",
                table: "deliverydays",
                column: "contractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliverydays");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "administrators");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
