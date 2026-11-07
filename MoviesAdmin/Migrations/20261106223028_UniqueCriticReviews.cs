using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class UniqueCriticReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CriticReview_MovieId",
                table: "CriticReview");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CriticReview",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CriticReview_MovieId_CreatedBy",
                table: "CriticReview",
                columns: new[] { "MovieId", "CreatedBy" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CriticReview_MovieId_CreatedBy",
                table: "CriticReview");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "CriticReview",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_CriticReview_MovieId",
                table: "CriticReview",
                column: "MovieId");
        }
    }
}
