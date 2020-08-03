using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PayCompute.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNo = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    DateJoined = table.Column<DateTime>(nullable: false),
                    Designation = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NationalInsuranceNo = table.Column<string>(maxLength: 50, nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    StudentLoan = table.Column<int>(nullable: false),
                    UnionMember = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 150, nullable: false),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxYears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearOfTax = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    NiNo = table.Column<string>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: false),
                    PayMonth = table.Column<string>(nullable: true),
                    TaxYearId = table.Column<int>(nullable: false),
                    TaxCode = table.Column<string>(nullable: true),
                    HourlyRate = table.Column<decimal>(type: "money", nullable: false),
                    HoursWorked = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractualHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractualEarnings = table.Column<decimal>(type: "money", nullable: false),
                    OvertimeEarnings = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    NIC = table.Column<decimal>(type: "money", nullable: false),
                    UnionFee = table.Column<decimal>(type: "money", nullable: true),
                    SLC = table.Column<decimal>(type: "money", nullable: true),
                    TotalEarnings = table.Column<decimal>(type: "money", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "money", nullable: false),
                    NetPayment = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_TaxYears_TaxYearId",
                        column: x => x.TaxYearId,
                        principalTable: "TaxYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_EmployeeId",
                table: "PaymentRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_TaxYearId",
                table: "PaymentRecords",
                column: "TaxYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRecords");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "TaxYears");
        }
    }
}
