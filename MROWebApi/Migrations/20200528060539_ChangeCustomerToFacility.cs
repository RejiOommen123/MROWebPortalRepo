using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MROWebApi.Migrations
{
    public partial class ChangeCustomerToFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "FieldCustomerMap");

            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    FacilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    FacilityAddress = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NumOfInstitution = table.Column<string>(nullable: true),
                    SMTPUsername = table.Column<string>(nullable: true),
                    SMTPPassword = table.Column<string>(nullable: true),
                    SMTPUrl = table.Column<string>(nullable: true),
                    FTPUsername = table.Column<string>(nullable: true),
                    FTPPassword = table.Column<string>(nullable: true),
                    FTPUrl = table.Column<string>(nullable: true),
                    ConfigFileUrl = table.Column<string>(nullable: true),
                    ActiveStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "FieldFacilityMap",
                columns: table => new
                {
                    FieldFacilityMapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    FieldId = table.Column<int>(nullable: false),
                    WizardId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldFacilityMap", x => x.FieldFacilityMapId);
                });

            migrationBuilder.UpdateData(
                table: "AdminUser",
                keyColumn: "AdminUserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2020, 5, 28, 11, 35, 39, 141, DateTimeKind.Local).AddTicks(3526), new DateTime(2020, 5, 28, 11, 35, 39, 143, DateTimeKind.Local).AddTicks(3827) });

            migrationBuilder.InsertData(
                table: "Facility",
                columns: new[] { "FacilityId", "ActiveStatus", "ConfigFileUrl", "CreatedBy", "CreatedDate", "Description", "FTPPassword", "FTPUrl", "FTPUsername", "FacilityAddress", "FacilityName", "NumOfInstitution", "SMTPPassword", "SMTPUrl", "SMTPUsername", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, true, "https://www.cleveland.com/data", 1, new DateTime(2020, 5, 28, 11, 35, 39, 148, DateTimeKind.Local).AddTicks(9609), "Info about Cleveland", "Cleveland@101", "ftp://ftp.cleveland.com/", "Cleveland101", "Cleveland", "Cleveland Clinic", "Cleveland Clinic,Cleveland Hospital", "Cleveland@101", "smtp.cleveland.com", "Cleveland101", 1, new DateTime(2020, 5, 28, 11, 35, 39, 148, DateTimeKind.Local).AddTicks(9666) });

            migrationBuilder.InsertData(
                table: "FieldFacilityMap",
                columns: new[] { "FieldFacilityMapId", "CreatedBy", "CreatedDate", "FacilityId", "FieldId", "IsEnable", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6356), 1, 1, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6376), 2 },
                    { 2, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6497), 1, 2, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6500), 3 },
                    { 3, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6504), 1, 3, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6507), 4 },
                    { 4, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6510), 1, 4, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6512), 4 },
                    { 5, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6515), 1, 5, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6517), 5 },
                    { 6, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6521), 1, 6, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6523), 5 },
                    { 7, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6526), 1, 7, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6528), 5 },
                    { 8, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6531), 1, 8, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6533), 5 },
                    { 9, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6537), 1, 9, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6539), 6 },
                    { 10, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6542), 1, 10, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6545), 7 },
                    { 11, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6550), 1, 11, true, 1, new DateTime(2020, 5, 28, 11, 35, 39, 149, DateTimeKind.Local).AddTicks(6553), 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "FieldFacilityMap");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    ConfigFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTPUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FTPUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMTPUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FieldCustomerMap",
                columns: table => new
                {
                    FieldCustomerMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WizardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldCustomerMap", x => x.FieldCustomerMapId);
                });

            migrationBuilder.UpdateData(
                table: "AdminUser",
                keyColumn: "AdminUserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2020, 5, 25, 19, 5, 33, 413, DateTimeKind.Local).AddTicks(5293), new DateTime(2020, 5, 25, 19, 5, 33, 414, DateTimeKind.Local).AddTicks(6701) });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "ActiveStatus", "ConfigFileUrl", "CreatedBy", "CreatedDate", "CustomerAddress", "CustomerName", "Description", "FTPPassword", "FTPUrl", "FTPUsername", "NumOfInstitution", "SMTPPassword", "SMTPUrl", "SMTPUsername", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, true, "https://www.cleveland.com/data", 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(2853), "Cleveland", "Cleveland Clinic", "Info about Cleveland", "Cleveland@101", "ftp://ftp.cleveland.com/", "Cleveland101", "Cleveland Clinic,Cleveland Hospital", "Cleveland@101", "smtp.cleveland.com", "Cleveland101", 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(2898) });

            migrationBuilder.InsertData(
                table: "FieldCustomerMap",
                columns: new[] { "FieldCustomerMapId", "CreatedBy", "CreatedDate", "CustomerId", "FieldId", "IsEnable", "UpdatedBy", "UpdatedDate", "WizardId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7441), 1, 1, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7458), 2 },
                    { 2, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7527), 1, 2, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7529), 3 },
                    { 3, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7531), 1, 3, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7533), 4 },
                    { 4, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7535), 1, 4, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7536), 4 },
                    { 5, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7538), 1, 5, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7539), 5 },
                    { 6, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7541), 1, 6, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7542), 5 },
                    { 7, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7544), 1, 7, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7546), 5 },
                    { 8, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7548), 1, 8, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7549), 5 },
                    { 9, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7551), 1, 9, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7552), 6 },
                    { 10, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7554), 1, 10, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7555), 7 },
                    { 11, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7557), 1, 11, true, 1, new DateTime(2020, 5, 25, 19, 5, 33, 417, DateTimeKind.Local).AddTicks(7559), 8 }
                });
        }
    }
}
