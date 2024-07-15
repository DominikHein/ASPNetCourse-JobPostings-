using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPNetCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedOwnerUsernameToJobPostingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "JobPostings");
        }
    }
}
