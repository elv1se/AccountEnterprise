using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountEnterprise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OperationTypeId",
                table: "OperationTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OperationId",
                table: "Operations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("55102f50-c43a-4123-aba3-3d1fa9f69fec"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OperationTypeId",
                table: "OperationTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("44502076-deb4-4e5e-9dca-1e04f678fa3d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OperationId",
                table: "Operations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("1e8e2775-2a48-4629-92a2-3c18aebb241f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("a407368f-12f5-409f-91ac-578566eeb32b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("459284b0-f90d-48d5-9e4e-a10aa02e3a1d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("c90d2f38-5f2a-4574-ac76-00d3f31a772a"));
        }
    }
}
