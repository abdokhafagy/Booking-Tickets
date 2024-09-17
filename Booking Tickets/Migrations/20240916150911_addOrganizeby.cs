using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Tickets.Migrations
{
    /// <inheritdoc />
    public partial class addOrganizeby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamImage2",
                table: "Events",
                newName: "SecondTeamName");

            migrationBuilder.RenameColumn(
                name: "TeamImage1",
                table: "Events",
                newName: "SecondTeamImage");

            migrationBuilder.RenameColumn(
                name: "Team2",
                table: "Events",
                newName: "FirstTeamName");

            migrationBuilder.RenameColumn(
                name: "Team1",
                table: "Events",
                newName: "FirstTeamImage");

            migrationBuilder.AddColumn<string>(
                name: "OrganizedBy",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 16, 15, 9, 5, 155, DateTimeKind.Utc).AddTicks(726));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizedBy",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "SecondTeamName",
                table: "Events",
                newName: "TeamImage2");

            migrationBuilder.RenameColumn(
                name: "SecondTeamImage",
                table: "Events",
                newName: "TeamImage1");

            migrationBuilder.RenameColumn(
                name: "FirstTeamName",
                table: "Events",
                newName: "Team2");

            migrationBuilder.RenameColumn(
                name: "FirstTeamImage",
                table: "Events",
                newName: "Team1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 7, 22, 54, 27, 399, DateTimeKind.Utc).AddTicks(4393));
        }
    }
}
