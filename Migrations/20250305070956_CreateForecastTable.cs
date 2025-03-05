using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RASCHET_HASHODOV.Migrations
{
    /// <inheritdoc />
    public partial class CreateForecastTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetRecommendations");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PredictedIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedTuitionFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedLivingExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedTransport = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedMiscellaneous = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedTotalExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PredictedSavings = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ForecastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForecastText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forecasts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forecasts_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_UserId",
                table: "Forecasts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Forecasts_UserId1",
                table: "Forecasts",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forecasts");

            migrationBuilder.CreateTable(
                name: "BudgetRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecommendationText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetRecommendations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlannedExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlannedIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetRecommendations_UserId",
                table: "BudgetRecommendations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");
        }
    }
}
