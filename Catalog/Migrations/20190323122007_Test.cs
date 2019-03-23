using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Address_FacilityAddressAddressId",
                table: "Facilities");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.RenameColumn(
                name: "FacilityAddressAddressId",
                table: "Facilities",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_FacilityAddressAddressId",
                table: "Facilities",
                newName: "IX_Facilities_AddressId");

            migrationBuilder.CreateTable(
                name: "FacilityAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    AppartmentNumber = table.Column<int>(nullable: false),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityAddress", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilityAddress_AddressId",
                table: "Facilities",
                column: "AddressId",
                principalTable: "FacilityAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilityAddress_AddressId",
                table: "Facilities");

            migrationBuilder.DropTable(
                name: "FacilityAddress");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Facilities",
                newName: "FacilityAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities",
                newName: "IX_Facilities_FacilityAddressAddressId");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppartmentNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Address_FacilityAddressAddressId",
                table: "Facilities",
                column: "FacilityAddressAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
