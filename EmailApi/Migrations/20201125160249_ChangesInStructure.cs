using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailApi.Migrations
{
    public partial class ChangesInStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TemplateId",
                table: "EmailHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EmailHistory_TemplateId",
                table: "EmailHistory",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailHistory_Templates_TemplateId",
                table: "EmailHistory",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailHistory_Templates_TemplateId",
                table: "EmailHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmailHistory_TemplateId",
                table: "EmailHistory");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateId",
                table: "EmailHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
