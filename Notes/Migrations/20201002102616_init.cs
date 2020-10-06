using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "note_category",
                columns: table => new
                {
                    id_note_category = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note_category", x => x.id_note_category);
                });

            migrationBuilder.CreateTable(
                name: "user_type",
                columns: table => new
                {
                    id_user_type = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_type", x => x.id_user_type);
                });

            migrationBuilder.CreateTable(
                name: "note",
                columns: table => new
                {
                    id_note = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_note_category = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    header = table.Column<string>(maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_favorites = table.Column<bool>(nullable: false),
                    image_path = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.id_note);
                    table.ForeignKey(
                        name: "FK_note_note_category",
                        column: x => x.id_note_category,
                        principalTable: "note_category",
                        principalColumn: "id_note_category",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user_type = table.Column<int>(nullable: false, defaultValueSql: "((2))"),
                    login = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 255, nullable: false),
                    email = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_user_user_type",
                        column: x => x.id_user_type,
                        principalTable: "user_type",
                        principalColumn: "id_user_type",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_has_note",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false),
                    id_note = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_note", x => new { x.id_user, x.id_note });
                    table.ForeignKey(
                        name: "FK_user_has_note_note",
                        column: x => x.id_note,
                        principalTable: "note",
                        principalColumn: "id_note",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_has_note_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_note_id_note_category",
                table: "note",
                column: "id_note_category");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_user_type",
                table: "user",
                column: "id_user_type");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_note_id_note",
                table: "user_has_note",
                column: "id_note");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_has_note");

            migrationBuilder.DropTable(
                name: "note");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "note_category");

            migrationBuilder.DropTable(
                name: "user_type");
        }
    }
}
