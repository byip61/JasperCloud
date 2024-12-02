using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasperCloud.Migrations
{
    /// <inheritdoc />
    public partial class AIConsent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "upload_date",
                table: "file",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "ai_consent",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_consented = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_consent", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_ai_consent_user_information_user_id",
                        column: x => x.user_id,
                        principalTable: "user_information",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ai_consent");

            migrationBuilder.AlterColumn<DateTime>(
                name: "upload_date",
                table: "file",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
