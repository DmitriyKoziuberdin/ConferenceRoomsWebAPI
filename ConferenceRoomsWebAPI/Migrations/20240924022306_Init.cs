using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConferenceRoomsWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "conference_rooms",
                columns: table => new
                {
                    id_room = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_room = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    base_price_per_hour = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conference_rooms", x => x.id_room);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id_booking = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_conference_room = table.Column<int>(type: "integer", nullable: false),
                    booking_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.id_booking);
                    table.ForeignKey(
                        name: "fk_bookings_conference_rooms_id_conference_room",
                        column: x => x.id_conference_room,
                        principalTable: "conference_rooms",
                        principalColumn: "id_room",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company_services",
                columns: table => new
                {
                    id_service = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_name = table.Column<string>(type: "text", nullable: false),
                    price_service = table.Column<decimal>(type: "numeric", nullable: false),
                    conference_rooms_id_room = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company_services", x => x.id_service);
                    table.ForeignKey(
                        name: "fk_company_services_conference_rooms_conference_rooms_id_room",
                        column: x => x.conference_rooms_id_room,
                        principalTable: "conference_rooms",
                        principalColumn: "id_room");
                });

            migrationBuilder.CreateTable(
                name: "booking_company_service",
                columns: table => new
                {
                    id_company_service = table.Column<int>(type: "integer", nullable: false),
                    id_booking = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_booking_company_service", x => new { x.id_company_service, x.id_booking });
                    table.ForeignKey(
                        name: "fk_booking_company_service_bookings_id_booking",
                        column: x => x.id_booking,
                        principalTable: "bookings",
                        principalColumn: "id_booking",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_booking_company_service_company_services_id_company_service",
                        column: x => x.id_company_service,
                        principalTable: "company_services",
                        principalColumn: "id_service",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_booking_company_service_id_booking",
                table: "booking_company_service",
                column: "id_booking");

            migrationBuilder.CreateIndex(
                name: "ix_bookings_id_conference_room",
                table: "bookings",
                column: "id_conference_room");

            migrationBuilder.CreateIndex(
                name: "ix_company_services_conference_rooms_id_room",
                table: "company_services",
                column: "conference_rooms_id_room");

            migrationBuilder.CreateIndex(
                name: "ix_conference_rooms_name_room",
                table: "conference_rooms",
                column: "name_room",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_company_service");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "company_services");

            migrationBuilder.DropTable(
                name: "conference_rooms");
        }
    }
}
