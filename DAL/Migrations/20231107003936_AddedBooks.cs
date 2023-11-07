using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RFID = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCopies = table.Column<int>(type: "int", nullable: false),
                    CurrentAvailableCopies = table.Column<int>(type: "int", nullable: false),
                    ReservationQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KeyWords = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPeopleAttended = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "ReaderCards",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderCards", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_ReaderCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBookings",
                columns: table => new
                {
                    BookBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustReturnInDays = table.Column<int>(type: "int", nullable: false),
                    ReturnedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBookings", x => x.BookBookingId);
                    table.ForeignKey(
                        name: "FK_BookBookings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationQueues",
                columns: table => new
                {
                    ReservationQueueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QueuePosition = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationQueues", x => x.ReservationQueueId);
                    table.ForeignKey(
                        name: "FK_ReservationQueues_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationQueues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ReaderCards",
                columns: new[] { "UserId", "Address", "BirthDate", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePicture" },
                values: new object[] { 1, "Street Ave. 15", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@mangahub.com", "Admin", "Admin", "0505050505", new byte[0] });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "RegistrationDate", "Role" },
                values: new object[] { new DateTime(2023, 11, 7, 0, 39, 36, 158, DateTimeKind.Utc).AddTicks(9240), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BookBookings_BookId",
                table: "BookBookings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookBookings_UserId",
                table: "BookBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationQueues_BookId",
                table: "ReservationQueues",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationQueues_UserId",
                table: "ReservationQueues",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBookings");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ReaderCards");

            migrationBuilder.DropTable(
                name: "ReservationQueues");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowConfidentialInformation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "UserId", "Avatar", "BirthDate", "Description", "Email", "FirstName", "LastName", "PhoneNumber", "ShowConfidentialInformation" },
                values: new object[] { 1, new byte[0], new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main administrator of the service", "admin@mangahub.com", "Admin", "Admin", "0505050505", false });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IsAdmin", "RegistrationDate" },
                values: new object[] { true, new DateTime(2023, 11, 6, 22, 5, 8, 747, DateTimeKind.Utc).AddTicks(7014) });
        }
    }
}
