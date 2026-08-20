using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmRentalNET25.Migrations
{
    /// <inheritdoc />
    public partial class changedUserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_Users_UserId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Kunder");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kunder",
                table: "Kunder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_Kunder_UserId",
                table: "UsersMovies",
                column: "UserId",
                principalTable: "Kunder",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_Kunder_UserId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kunder",
                table: "Kunder");

            migrationBuilder.RenameTable(
                name: "Kunder",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_Users_UserId",
                table: "UsersMovies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
