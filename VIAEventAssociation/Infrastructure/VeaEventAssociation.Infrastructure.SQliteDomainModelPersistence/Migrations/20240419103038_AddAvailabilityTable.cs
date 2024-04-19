using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailabilityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_Eventid",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_eventId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Guests_Guestid",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Events_eventId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Guests_Guestid",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Guests_eventId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_Eventid",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Eventid",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "eventId",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "Request",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "Guestid",
                table: "Request",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_Guestid",
                table: "Request",
                newName: "IX_Request_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_eventId",
                table: "Request",
                newName: "IX_Request_EventId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Locations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "maxCapacity",
                table: "Locations",
                newName: "MaxCapacity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Locations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Guestid",
                table: "Invitation",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitation_Guestid",
                table: "Invitation",
                newName: "IX_Invitation_GuestId");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Guests",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Guests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "visibility",
                table: "Events",
                newName: "Visibility");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Events",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Events",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_date_time",
                table: "Events",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "max_guests",
                table: "Events",
                newName: "MaxGuests");

            migrationBuilder.RenameColumn(
                name: "end_date_time",
                table: "Events",
                newName: "EndDateTime");

            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "Request",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Request",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AvailabilityInterval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    locationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityInterval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailabilityInterval_Locations_locationId",
                        column: x => x.locationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventGuests",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuests", x => new { x.EventsId, x.GuestsId });
                    table.ForeignKey(
                        name: "FK_EventGuests_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventGuests_Guests_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_EventId1",
                table: "Request",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityInterval_locationId",
                table: "AvailabilityInterval",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuests_GuestsId",
                table: "EventGuests",
                column: "GuestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Guests_GuestId",
                table: "Invitation",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Events_EventId",
                table: "Request",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Events_EventId1",
                table: "Request",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Guests_GuestId",
                table: "Request",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Guests_GuestId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Events_EventId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Events_EventId1",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Guests_GuestId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "AvailabilityInterval");

            migrationBuilder.DropTable(
                name: "EventGuests");

            migrationBuilder.DropIndex(
                name: "IX_Request_EventId1",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Request",
                newName: "Guestid");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Request",
                newName: "eventId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_GuestId",
                table: "Request",
                newName: "IX_Request_Guestid");

            migrationBuilder.RenameIndex(
                name: "IX_Request_EventId",
                table: "Request",
                newName: "IX_Request_eventId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "Locations",
                newName: "maxCapacity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Invitation",
                newName: "Guestid");

            migrationBuilder.RenameIndex(
                name: "IX_Invitation_GuestId",
                table: "Invitation",
                newName: "IX_Invitation_Guestid");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Guests",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Guests",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Visibility",
                table: "Events",
                newName: "visibility");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Events",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Events",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Events",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Events",
                newName: "start_date_time");

            migrationBuilder.RenameColumn(
                name: "MaxGuests",
                table: "Events",
                newName: "max_guests");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Events",
                newName: "end_date_time");

            migrationBuilder.AddColumn<int>(
                name: "Eventid",
                table: "Guests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "eventId",
                table: "Guests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_eventId",
                table: "Guests",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Eventid",
                table: "Guests",
                column: "Eventid");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_Eventid",
                table: "Guests",
                column: "Eventid",
                principalTable: "Events",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_eventId",
                table: "Guests",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Guests_Guestid",
                table: "Invitation",
                column: "Guestid",
                principalTable: "Guests",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Events_eventId",
                table: "Request",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Guests_Guestid",
                table: "Request",
                column: "Guestid",
                principalTable: "Guests",
                principalColumn: "id");
        }
    }
}
