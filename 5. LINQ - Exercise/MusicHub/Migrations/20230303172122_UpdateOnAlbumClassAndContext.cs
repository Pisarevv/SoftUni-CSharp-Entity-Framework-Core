using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicHub.Migrations
{
    public partial class UpdateOnAlbumClassAndContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Producers_ProducerId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerId",
                table: "Albums",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Producers_ProducerId",
                table: "Albums",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Producers_ProducerId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "ProducerId",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Producers_ProducerId",
                table: "Albums",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
