using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameInvoicesLineToInvoiceLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesLine_Invoices_InvoiceId",
                table: "InvoicesLine");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicesLine",
                table: "InvoicesLine");

            migrationBuilder.RenameTable(
                name: "InvoicesLine",
                newName: "InvoiceLines");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicesLine_InvoiceId",
                table: "InvoiceLines",
                newName: "IX_InvoiceLines_InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "BeerId",
                table: "InvoiceLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceLines",
                table: "InvoiceLines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_BeerId",
                table: "InvoiceLines",
                column: "BeerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Beers_BeerId",
                table: "InvoiceLines",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Beers_BeerId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceLines",
                table: "InvoiceLines");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLines_BeerId",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "BeerId",
                table: "InvoiceLines");

            migrationBuilder.RenameTable(
                name: "InvoiceLines",
                newName: "InvoicesLine");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoicesLine",
                newName: "IX_InvoicesLine_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicesLine",
                table: "InvoicesLine",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TodoListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_TodoListId",
                table: "TodoItems",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesLine_Invoices_InvoiceId",
                table: "InvoicesLine",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
