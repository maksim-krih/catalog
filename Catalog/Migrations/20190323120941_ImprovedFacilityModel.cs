using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Migrations
{
    public partial class ImprovedFacilityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Address_AddressId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Facilities",
                newName: "FacilityAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_AddressId",
                table: "Facilities",
                newName: "IX_Facilities_FacilityAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Address_FacilityAddressAddressId",
                table: "Facilities",
                column: "FacilityAddressAddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Address_FacilityAddressAddressId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "FacilityAddressAddressId",
                table: "Facilities",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Facilities_FacilityAddressAddressId",
                table: "Facilities",
                newName: "IX_Facilities_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Address_AddressId",
                table: "Facilities",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
