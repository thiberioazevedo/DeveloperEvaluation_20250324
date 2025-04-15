using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class Migration002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossValue",
                table: "CDBs");

            migrationBuilder.DropColumn(
                name: "NetValue",
                table: "CDBs");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "CDBs");

            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "CDBs");

            migrationBuilder.RenameColumn(
                name: "FinalValue",
                table: "MonthsCDBs",
                newName: "TaxPercentage");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossValue",
                table: "MonthsCDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetValue",
                table: "MonthsCDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "MonthsCDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossValue",
                table: "MonthsCDBs");

            migrationBuilder.DropColumn(
                name: "NetValue",
                table: "MonthsCDBs");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "MonthsCDBs");

            migrationBuilder.RenameColumn(
                name: "TaxPercentage",
                table: "MonthsCDBs",
                newName: "FinalValue");

            migrationBuilder.AddColumn<decimal>(
                name: "GrossValue",
                table: "CDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetValue",
                table: "CDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "CDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentage",
                table: "CDBs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
