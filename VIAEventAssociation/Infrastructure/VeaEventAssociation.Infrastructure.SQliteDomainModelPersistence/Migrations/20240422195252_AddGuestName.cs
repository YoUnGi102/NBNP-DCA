using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Guests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Guests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicURL",
                table: "Guests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ProfilePicURL",
                table: "Guests");
        }
    }
}
