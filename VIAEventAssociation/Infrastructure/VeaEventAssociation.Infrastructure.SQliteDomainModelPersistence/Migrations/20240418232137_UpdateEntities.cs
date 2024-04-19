using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_locationid",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_Eventid",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_parentId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Guests_parentId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Guests_parentId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "availability",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "parentId",
                table: "Request",
                newName: "eventId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_parentId",
                table: "Request",
                newName: "IX_Request_eventId");

            migrationBuilder.RenameColumn(
                name: "parentId",
                table: "Invitation",
                newName: "eventId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitation_parentId",
                table: "Invitation",
                newName: "IX_Invitation_eventId");

            migrationBuilder.RenameColumn(
                name: "Eventid",
                table: "Guests",
                newName: "eventId");

            migrationBuilder.RenameColumn(
                name: "parentId",
                table: "Guests",
                newName: "Eventid");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_Eventid",
                table: "Guests",
                newName: "IX_Guests_eventId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_parentId",
                table: "Guests",
                newName: "IX_Guests_Eventid");

            migrationBuilder.RenameColumn(
                name: "locationid",
                table: "Events",
                newName: "locationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_locationid",
                table: "Events",
                newName: "IX_Events_locationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_locationId",
                table: "Events",
                column: "locationId",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Invitation_Events_eventId",
                table: "Invitation",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Events_eventId",
                table: "Request",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_locationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_Eventid",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Events_eventId",
                table: "Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Events_eventId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Events_eventId",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "Request",
                newName: "parentId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_eventId",
                table: "Request",
                newName: "IX_Request_parentId");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "Invitation",
                newName: "parentId");

            migrationBuilder.RenameIndex(
                name: "IX_Invitation_eventId",
                table: "Invitation",
                newName: "IX_Invitation_parentId");

            migrationBuilder.RenameColumn(
                name: "eventId",
                table: "Guests",
                newName: "Eventid");

            migrationBuilder.RenameColumn(
                name: "Eventid",
                table: "Guests",
                newName: "parentId");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_eventId",
                table: "Guests",
                newName: "IX_Guests_Eventid");

            migrationBuilder.RenameIndex(
                name: "IX_Guests_Eventid",
                table: "Guests",
                newName: "IX_Guests_parentId");

            migrationBuilder.RenameColumn(
                name: "locationId",
                table: "Events",
                newName: "locationid");

            migrationBuilder.RenameIndex(
                name: "IX_Events_locationId",
                table: "Events",
                newName: "IX_Events_locationid");

            migrationBuilder.AddColumn<string>(
                name: "availability",
                table: "Locations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_locationid",
                table: "Events",
                column: "locationid",
                principalTable: "Locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_Eventid",
                table: "Guests",
                column: "Eventid",
                principalTable: "Events",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Events_parentId",
                table: "Guests",
                column: "parentId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Guests_parentId",
                table: "Invitation",
                column: "parentId",
                principalTable: "Guests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Guests_parentId",
                table: "Request",
                column: "parentId",
                principalTable: "Guests",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
