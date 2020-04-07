using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacyNetwork.Infrastructure.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "firm",
                columns: table => new
                {
                    firm_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firm_name = table.Column<string>(unicode: false, maxLength: 75, nullable: false),
                    firm_address = table.Column<string>(unicode: false, maxLength: 75, nullable: false),
                    firm_contact = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    firm_markup = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firm", x => x.firm_id);
                });

            migrationBuilder.CreateTable(
                name: "pharmacy",
                columns: table => new
                {
                    pharm_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pharm_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    pharm_address = table.Column<string>(unicode: false, maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacies", x => x.pharm_id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    categ_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categ_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    categ_markup = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_categories", x => x.categ_id);
                });

            migrationBuilder.CreateTable(
                name: "income",
                columns: table => new
                {
                    income_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pharm_id = table.Column<int>(nullable: false),
                    income_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_income", x => x.income_id);
                    table.ForeignKey(
                        name: "FK_income_pharmacy",
                        column: x => x.pharm_id,
                        principalTable: "pharmacy",
                        principalColumn: "pharm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    purch_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pharm_id = table.Column<int>(nullable: false),
                    purch_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    purch_amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    purch_discount_percent = table.Column<decimal>(type: "decimal(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => x.purch_id);
                    table.ForeignKey(
                        name: "FK_purchase_pharmacy",
                        column: x => x.pharm_id,
                        principalTable: "pharmacy",
                        principalColumn: "pharm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medical_item",
                columns: table => new
                {
                    med_item_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firm_id = table.Column<int>(nullable: false),
                    categ_id = table.Column<int>(nullable: false),
                    med_item_name = table.Column<string>(unicode: false, maxLength: 60, nullable: false),
                    med_item_descrip = table.Column<string>(type: "text", nullable: true),
                    med_item_price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    med_item_price_markup = table.Column<decimal>(type: "decimal(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_items", x => x.med_item_id);
                    table.ForeignKey(
                        name: "FK_medical_item_product_category",
                        column: x => x.categ_id,
                        principalTable: "product_category",
                        principalColumn: "categ_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_medical_item_firm",
                        column: x => x.firm_id,
                        principalTable: "firm",
                        principalColumn: "firm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "check",
                columns: table => new
                {
                    med_item_id = table.Column<int>(nullable: false),
                    purch_id = table.Column<int>(nullable: false),
                    item_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checks", x => new { x.med_item_id, x.purch_id });
                    table.ForeignKey(
                        name: "FK_check_medical_item",
                        column: x => x.med_item_id,
                        principalTable: "medical_item",
                        principalColumn: "med_item_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_check_purchase",
                        column: x => x.purch_id,
                        principalTable: "purchase",
                        principalColumn: "purch_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "income_detail",
                columns: table => new
                {
                    med_item_id = table.Column<int>(nullable: false),
                    income_id = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incomes_detail", x => new { x.med_item_id, x.income_id });
                    table.ForeignKey(
                        name: "FK_income_detail_income",
                        column: x => x.income_id,
                        principalTable: "income",
                        principalColumn: "income_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_income_detail_medical_item",
                        column: x => x.med_item_id,
                        principalTable: "medical_item",
                        principalColumn: "med_item_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pharmacy_wharehouse",
                columns: table => new
                {
                    pharm_id = table.Column<int>(nullable: false),
                    med_item_id = table.Column<int>(nullable: false),
                    item_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacy_wharehouses", x => new { x.pharm_id, x.med_item_id });
                    table.ForeignKey(
                        name: "FK_pharmacy_wharehouse_medical_item",
                        column: x => x.med_item_id,
                        principalTable: "medical_item",
                        principalColumn: "med_item_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pharmacy_wharehouse_pharmacy",
                        column: x => x.pharm_id,
                        principalTable: "pharmacy",
                        principalColumn: "pharm_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reserved_med_item",
                columns: table => new
                {
                    reserved_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_start = table.Column<DateTime>(type: "datetime", nullable: false),
                    date_finish = table.Column<DateTime>(type: "datetime", nullable: false),
                    med_item_id = table.Column<int>(nullable: false),
                    pharm_id = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    telephone = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reserved_med_items", x => x.reserved_id);
                    table.ForeignKey(
                        name: "FK_reserved_med_item_pharmacy_wharehouse",
                        columns: x => new { x.pharm_id, x.med_item_id },
                        principalTable: "pharmacy_wharehouse",
                        principalColumns: new[] { "pharm_id", "med_item_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_check_purch_id",
                table: "check",
                column: "purch_id");

            migrationBuilder.CreateIndex(
                name: "IX_income_pharm_id",
                table: "income",
                column: "pharm_id");

            migrationBuilder.CreateIndex(
                name: "IX_income_detail_income_id",
                table: "income_detail",
                column: "income_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_item_categ_id",
                table: "medical_item",
                column: "categ_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_item_firm_id",
                table: "medical_item",
                column: "firm_id");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacy_wharehouse_med_item_id",
                table: "pharmacy_wharehouse",
                column: "med_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_pharm_id",
                table: "purchase",
                column: "pharm_id");

            migrationBuilder.CreateIndex(
                name: "IX_reserved_med_item_pharm_id_med_item_id",
                table: "reserved_med_item",
                columns: new[] { "pharm_id", "med_item_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "check");

            migrationBuilder.DropTable(
                name: "income_detail");

            migrationBuilder.DropTable(
                name: "reserved_med_item");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "income");

            migrationBuilder.DropTable(
                name: "pharmacy_wharehouse");

            migrationBuilder.DropTable(
                name: "medical_item");

            migrationBuilder.DropTable(
                name: "pharmacy");

            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "firm");
        }
    }
}
