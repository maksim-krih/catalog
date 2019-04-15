using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Roleid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Roleid",
                        column: x => x.Roleid,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FacilityType = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    FacilityOwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facility_Users_FacilityOwnerId",
                        column: x => x.FacilityOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilityAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacilityId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    ZipCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityAddress_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacilityId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    FacilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photo_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FacilityId = table.Column<int>(nullable: false),
                    Open = table.Column<TimeSpan>(nullable: false),
                    Closed = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Facility_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Roleid" },
                values: new object[] { 1, "admin@gmail.com", "Admin", "1111", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Roleid" },
                values: new object[] { 2, "user@gmail.com", "User", "1111", 2 });

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "Id", "FacilityOwnerId", "FacilityType", "Name", "Phone", "Price", "Rating" },
                values: new object[] { 1, 2, "Bar", "Name 1", "012345678", 3.0, 3.2 });

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "Id", "FacilityOwnerId", "FacilityType", "Name", "Phone", "Price", "Rating" },
                values: new object[] { 2, 2, "Bar", "Name 2", "012345678", 3.0, 3.2 });

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "Id", "FacilityOwnerId", "FacilityType", "Name", "Phone", "Price", "Rating" },
                values: new object[] { 3, 2, "Bar", "Name 1", "012345678", 3.0, 3.2 });

            migrationBuilder.InsertData(
                table: "FacilityAddress",
                columns: new[] { "Id", "City", "Country", "FacilityId", "HouseNumber", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "City 1", "Country 1", 1, "1", "Street 1", 1 },
                    { 2, "City 2", "Country 2", 2, "2", "Street 2", 2 },
                    { 3, "City 3", "Country 3", 3, "3", "Street 3", 3 }
                });

            migrationBuilder.InsertData(
                table: "Feedback",
                columns: new[] { "Id", "Author", "Date", "FacilityId", "Message", "Rating" },
                values: new object[,]
                {
                    { 1, "Anonynous", new DateTime(2019, 4, 13, 23, 4, 52, 527, DateTimeKind.Local), 1, "Feedback message", 4 },
                    { 2, "Anonynous 2", new DateTime(2019, 4, 13, 23, 4, 52, 529, DateTimeKind.Local), 1, "Feedback message 2", 3 }
                });

            migrationBuilder.InsertData(
                table: "Schedule",
                columns: new[] { "Id", "Closed", "FacilityId", "Open" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 20, 0, 0, 0), 1, new TimeSpan(0, 8, 0, 0, 0) },
                    { 2, new TimeSpan(0, 20, 0, 0, 0), 2, new TimeSpan(0, 8, 0, 0, 0) },
                    { 3, new TimeSpan(0, 20, 0, 0, 0), 3, new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facility_FacilityOwnerId",
                table: "Facility",
                column: "FacilityOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityAddress_FacilityId",
                table: "FacilityAddress",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_FacilityId",
                table: "Feedback",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_FacilityId",
                table: "Photo",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_FacilityId",
                table: "Schedule",
                column: "FacilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roleid",
                table: "Users",
                column: "Roleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityAddress");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
