using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MROWebApi.Migrations
{
    public partial class AddedCommonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WizardCustomerMap");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "FieldCustomerMap",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FieldCustomerMap",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "FieldCustomerMap",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FieldCustomerMap",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WizardId",
                table: "FieldCustomerMap",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AdminUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AdminUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "AdminUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AdminUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdminUser",
                keyColumn: "AdminUserId",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 413, DateTimeKind.Local).AddTicks(5293), 1, new DateTime(2020, 5, 25, 19, 5, 33, 414, DateTimeKind.Local).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(2853), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(2898) });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7441), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7458), 2 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7527), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7529), 3 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7531), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7533), 4 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 4,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7535), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7536), 4 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 5,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7538), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7539), 5 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 6,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7541), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7542), 5 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 7,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7544), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7546), 5 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 8,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7548), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7549), 5 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 9,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7551), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7552), 6 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 10,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7554), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7555), 7 });

            migrationBuilder.UpdateData(
                table: "FieldCustomerMap",
                keyColumn: "FieldCustomerMapId",
                keyValue: 11,
                columns: new[] { "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[] { 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7557), 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7559), 8 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FieldCustomerMap");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FieldCustomerMap");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FieldCustomerMap");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FieldCustomerMap");

            migrationBuilder.DropColumn(
                name: "WizardId",
                table: "FieldCustomerMap");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AdminUser");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AdminUser");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AdminUser");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AdminUser");

            migrationBuilder.CreateTable(
                name: "WizardCustomerMap",
                columns: table => new
                {
                    WizardCustomerMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    WizardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizardCustomerMap", x => x.WizardCustomerMapId);
                });

            migrationBuilder.InsertData(
                table: "WizardCustomerMap",
                columns: new[] { "WizardCustomerMapId", "CustomerId", "IsEnable", "WizardId" },
                values: new object[,]
                {
                    { 1, 1, true, 1 },
                    { 2, 1, true, 2 },
                    { 3, 1, true, 3 },
                    { 4, 1, true, 4 },
                    { 5, 1, true, 5 },
                    { 6, 1, true, 6 },
                    { 7, 1, true, 7 },
                    { 8, 1, true, 8 }
                });
        }
    }
}
