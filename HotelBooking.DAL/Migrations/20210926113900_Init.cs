using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBooking.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<double>(type: "float", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingExtras_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Country", "CreatedAt", "Name", "ThumbnailImage" },
                values: new object[,]
                {
                    { 1, "Sweden", new DateTime(2021, 9, 26, 13, 38, 59, 992, DateTimeKind.Local).AddTicks(7723), "Plaza Bay Hotel", "https://picsum.photos/500" },
                    { 2, "Sweden", new DateTime(2021, 9, 26, 13, 38, 59, 995, DateTimeKind.Local).AddTicks(3348), "Stadshotellet", "https://picsum.photos/500" },
                    { 3, "Sweden", new DateTime(2021, 9, 26, 13, 38, 59, 995, DateTimeKind.Local).AddTicks(3498), "Sunkstället", "https://picsum.photos/500" },
                    { 4, "Sweden", new DateTime(2021, 9, 26, 13, 38, 59, 995, DateTimeKind.Local).AddTicks(3509), "Stugstugan", "https://picsum.photos/500" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "CreatedAt", "MaxCapacity", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(4515), 1, 100, "Single Room" },
                    { 2, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(5067), 2, 150, "Double Room" },
                    { 3, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(5077), 3, 200, "Triple Room" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "HotelId", "Score" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(1860), null, 1, 2.0 },
                    { 2, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2355), null, 1, 2.0 },
                    { 3, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2365), null, 1, 1.0 },
                    { 4, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2369), null, 1, 1.0 },
                    { 5, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2372), null, 2, 0.0 },
                    { 6, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2375), null, 2, 0.0 },
                    { 7, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2379), null, 2, 2.0 },
                    { 8, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2382), null, 2, 1.0 },
                    { 9, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2385), null, 3, 2.0 },
                    { 10, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2388), null, 3, 5.0 },
                    { 11, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2392), null, 3, 2.0 },
                    { 12, new DateTime(2021, 9, 26, 13, 38, 59, 996, DateTimeKind.Local).AddTicks(2396), null, 3, 0.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingExtras_BookingId",
                table: "BookingExtras",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CustomerId",
                table: "Ratings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_HotelId",
                table: "Ratings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingExtras");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
