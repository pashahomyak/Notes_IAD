using Microsoft.EntityFrameworkCore.Migrations;

namespace Notes.Migrations
{
    public partial class InitDBNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_has_note_category",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false),
                    id_note_category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_note_category", x => new { x.id_user, x.id_note_category });
                    table.ForeignKey(
                        name: "FK_user_has_note_category_note_category",
                        column: x => x.id_note_category,
                        principalTable: "note_category",
                        principalColumn: "id_note_category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_has_note_category_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_has_note_category_id_note_category",
                table: "user_has_note_category",
                column: "id_note_category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_has_note_category");
        }
    }
}
