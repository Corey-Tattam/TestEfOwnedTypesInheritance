using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAddress_Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaveatStringFieldOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaveatStringFieldTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaveatStringFieldThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaveatDateFieldOne = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CaveatDateFieldTwo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ForwardingAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardingAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardingAddressSuburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForwardingAddressState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementStringFieldOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementStringFieldTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementStringFieldThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementDateFieldOne = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SettlementDateFieldTwo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerStringFieldOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerStringFieldTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerStringFieldThree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumerDateFieldOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumerDateFieldTwo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOrganisation = table.Column<bool>(type: "bit", nullable: false),
                    IndividualName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyType = table.Column<string>(type: "char(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DocumentStringFieldOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStringFieldTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStringFieldThree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDateFieldOne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDateFieldTwo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrderId",
                table: "Consumers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OrderId",
                table: "Documents",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
