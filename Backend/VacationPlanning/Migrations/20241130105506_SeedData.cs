using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VacationPlanning.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "HolidayDate", "HolidayDescription" },
                values: new object[] { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новый год" });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "LeaveTypeName" },
                values: new object[,]
                {
                    { 1, "Ежегодный основной" },
                    { 2, "Ежегодный дополнительный" },
                    { 3, "Учебный" },
                    { 4, "Без сохранения зарплаты" }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "OrganizationName" },
                values: new object[] { 1, "Эн+ Диджитал" });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "PositionTitle" },
                values: new object[] { 1, "Разработчик" });

            migrationBuilder.InsertData(
                table: "Restrictions",
                columns: new[] { "Id", "RestrictionType" },
                values: new object[] { 1, "Законодательное" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentName", "OrganizationId" },
                values: new object[] { 1, "IT", 1 });

            migrationBuilder.InsertData(
                table: "DepartmentRestrictions",
                columns: new[] { "Id", "DepartmentId", "RestrictionId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "EmployeeName" },
                values: new object[] { 1, 1, "Иванов Иван Иванович" });

            migrationBuilder.InsertData(
                table: "LeaveDays",
                columns: new[] { "Id", "EmployeeId", "LeaveTypeId", "TotalDays" },
                values: new object[] { 1, 1, 1, 14 });

            migrationBuilder.InsertData(
                table: "ScheduledLeaves",
                columns: new[] { "Id", "EmployeeId", "EndDate", "StartDate" },
                values: new object[] { 1, 1, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentRestrictions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveDays",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ScheduledLeaves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restrictions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
