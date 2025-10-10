using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThunderPay.Database.Migrations.PaymentSagaDb
{
    /// <inheritdoc />
    public partial class AddTimeStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                schema: "saga",
                table: "eftSubmissionSagaState",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtUtc",
                schema: "saga",
                table: "eftSubmissionSagaState",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                schema: "saga",
                table: "eftSubmissionSagaState");

            migrationBuilder.DropColumn(
                name: "UpdatedAtUtc",
                schema: "saga",
                table: "eftSubmissionSagaState");
        }
    }
}
