using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PriorityList",
                columns: table => new
                {
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriorityName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityList", x => x.PriorityId);
                    table.ForeignKey(
                        name: "FK_PriorityList_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    TaskNotes = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Deadline = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DateEnded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Position = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_PriorityList_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "PriorityList",
                        principalColumn: "PriorityId");
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "PriorityList",
                columns: new[] { "PriorityId", "PriorityName", "UserId" },
                values: new object[,]
                {
                    { new Guid("0eb13bb3-a682-431b-ba2f-b702cd07142e"), "Important", null },
                    { new Guid("1dd9650e-b708-4ef5-8772-44853fb5c91e"), "Urgent", null },
                    { new Guid("f45713ab-23af-470d-a4ad-9accb2bafea8"), "Urgent & Important", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DateCreated", "Password", "Username" },
                values: new object[] { new Guid("db221a5a-db6e-473a-ba5e-088649b6f5cb"), new DateTimeOffset(new DateTime(2024, 8, 5, 6, 51, 3, 334, DateTimeKind.Unspecified).AddTicks(7120), new TimeSpan(0, 0, 0, 0, 0)), "adminPassword", "admin" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CreatedDate", "DateEnded", "Deadline", "Position", "PriorityId", "TaskName", "TaskNotes", "UserId" },
                values: new object[] { new Guid("c4383029-e9b2-42a8-b216-814b541490d2"), new DateTimeOffset(new DateTime(2024, 8, 5, 6, 51, 3, 334, DateTimeKind.Unspecified).AddTicks(7080), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 5, 6, 51, 3, 334, DateTimeKind.Unspecified).AddTicks(7080), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 5, 6, 51, 3, 334, DateTimeKind.Unspecified).AddTicks(7040), new TimeSpan(0, 0, 0, 0, 0)), 0, new Guid("1dd9650e-b708-4ef5-8772-44853fb5c91e"), "Sample Task", "This is a sample task. Feel free to delete anytime", null });

            migrationBuilder.CreateIndex(
                name: "IX_PriorityList_UserId",
                table: "PriorityList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "PriorityList");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
