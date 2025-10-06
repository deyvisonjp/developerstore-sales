using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Sales""
                ALTER COLUMN ""BranchId"" TYPE uuid USING gen_random_uuid();
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Sales""
                ALTER COLUMN ""CustomerId"" TYPE uuid USING gen_random_uuid();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
