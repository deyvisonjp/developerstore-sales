using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCustomerAndBranchIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Sales",
                type: "uuid",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "BranchId",
                table: "Sales",
                type: "uuid",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Sales",
                type: "integer",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "Sales",
                type: "integer",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldMaxLength: 50);
        }
    }
}
