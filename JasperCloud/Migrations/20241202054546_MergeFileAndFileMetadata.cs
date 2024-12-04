using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JasperCloud.Migrations
{
    /// <inheritdoc />
    public partial class MergeFileAndFileMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_metadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_file",
                table: "file");

            migrationBuilder.DropColumn(
                name: "file_path",
                table: "file");

            migrationBuilder.RenameColumn(
                name: "file_id",
                table: "file",
                newName: "user_id");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "file",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "file",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "file_extension",
                table: "file",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "file_guid",
                table: "file",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "size",
                table: "file",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "upload_date",
                table: "file",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_file",
                table: "file",
                columns: new[] { "user_id", "file_name", "file_extension" });

            migrationBuilder.AddForeignKey(
                name: "FK_file_user_information_user_id",
                table: "file",
                column: "user_id",
                principalTable: "user_information",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_file_user_information_user_id",
                table: "file");

            migrationBuilder.DropPrimaryKey(
                name: "PK_file",
                table: "file");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "file");

            migrationBuilder.DropColumn(
                name: "file_extension",
                table: "file");

            migrationBuilder.DropColumn(
                name: "file_guid",
                table: "file");

            migrationBuilder.DropColumn(
                name: "size",
                table: "file");

            migrationBuilder.DropColumn(
                name: "upload_date",
                table: "file");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "file",
                newName: "file_id");

            migrationBuilder.AlterColumn<int>(
                name: "file_id",
                table: "file",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                table: "file",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_file",
                table: "file",
                column: "file_id");

            migrationBuilder.CreateTable(
                name: "file_metadata",
                columns: table => new
                {
                    file_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    file_extension = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_metadata", x => x.file_id);
                    table.ForeignKey(
                        name: "FK_file_metadata_file_file_id",
                        column: x => x.file_id,
                        principalTable: "file",
                        principalColumn: "file_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_file_metadata_user_information_user_id",
                        column: x => x.user_id,
                        principalTable: "user_information",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_metadata_user_id",
                table: "file_metadata",
                column: "user_id");
        }
    }
}
