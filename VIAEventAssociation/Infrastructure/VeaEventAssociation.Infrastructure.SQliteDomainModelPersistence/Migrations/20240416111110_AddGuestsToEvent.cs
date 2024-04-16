using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeaEventAssociation.Infrastructure.SQliteDomainModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestsToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    maxCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    availability = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    start_date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    end_date_time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    max_guests = table.Column<int>(type: "INTEGER", nullable: false),
                    visibility = table.Column<int>(type: "INTEGER", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    locationid = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Locations_locationid",
                        column: x => x.locationid,
                        principalTable: "Locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    Eventid = table.Column<int>(type: "INTEGER", nullable: true),
                    parentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.id);
                    table.ForeignKey(
                        name: "FK_Guests_Events_Eventid",
                        column: x => x.Eventid,
                        principalTable: "Events",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Guests_Events_parentId",
                        column: x => x.parentId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guestid = table.Column<int>(type: "INTEGER", nullable: true),
                    parentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Guests_Guestid",
                        column: x => x.Guestid,
                        principalTable: "Guests",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Invitation_Guests_parentId",
                        column: x => x.parentId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Guestid = table.Column<int>(type: "INTEGER", nullable: true),
                    parentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Guests_Guestid",
                        column: x => x.Guestid,
                        principalTable: "Guests",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Request_Guests_parentId",
                        column: x => x.parentId,
                        principalTable: "Guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_locationid",
                table: "Events",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Eventid",
                table: "Guests",
                column: "Eventid");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_parentId",
                table: "Guests",
                column: "parentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_Guestid",
                table: "Invitation",
                column: "Guestid");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_parentId",
                table: "Invitation",
                column: "parentId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_Guestid",
                table: "Request",
                column: "Guestid");

            migrationBuilder.CreateIndex(
                name: "IX_Request_parentId",
                table: "Request",
                column: "parentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creators");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
