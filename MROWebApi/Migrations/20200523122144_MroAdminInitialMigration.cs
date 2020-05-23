using Microsoft.EntityFrameworkCore.Migrations;

namespace MROWebApi.Migrations
{
    public partial class MroAdminInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    AdminUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.AdminUserId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    CustomerAddress = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    FieldId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WizardId = table.Column<int>(nullable: false),
                    FieldName = table.Column<string>(nullable: true),
                    NormalizedFieldName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.FieldId);
                });

            migrationBuilder.CreateTable(
                name: "FieldCustomerMap",
                columns: table => new
                {
                    FieldCustomerMapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldCustomerMap", x => x.FieldCustomerMapId);
                });

            migrationBuilder.CreateTable(
                name: "Wizard",
                columns: table => new
                {
                    WizardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WizardName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizard", x => x.WizardId);
                });

            migrationBuilder.CreateTable(
                name: "WizardCustomerMap",
                columns: table => new
                {
                    WizardCustomerMapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WizardId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WizardCustomerMap", x => x.WizardCustomerMapId);
                });

            migrationBuilder.InsertData(
                table: "AdminUser",
                columns: new[] { "AdminUserId", "Email", "Password" },
                values: new object[] { 1, "admin@razor-tech.com", "admin" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "ActiveStatus", "ConfigFileUrl", "CustomerAddress", "CustomerName", "Description", "FTPPassword", "FTPUrl", "FTPUsername", "NumOfInstitution", "SMTPPassword", "SMTPUrl", "SMTPUsername" },
                values: new object[] { 1, true, "https://www.cleveland.com/data", "Cleveland", "Cleveland Clinic", "Info about Cleveland", "Cleveland@101", "ftp://ftp.cleveland.com/", "Cleveland101", "Cleveland Clinic,Cleveland Hospital", "Cleveland@101", "smtp.cleveland.com", "Cleveland101" });

            migrationBuilder.InsertData(
                table: "Field",
                columns: new[] { "FieldId", "FieldName", "NormalizedFieldName", "WizardId" },
                values: new object[,]
                {
                    { 11, "Birth Date", "BDay", 8 },
                    { 10, "Street Area", "StreetArea", 7 },
                    { 9, "Postal Code", "PostalCode", 6 },
                    { 8, "Is Patient Deceased", "IsPatientDeceased", 5 },
                    { 6, "Middle Initial", "MInitial", 5 },
                    { 7, "Last Name", "LName", 5 },
                    { 4, "Confirm Report", "ConfirmReport", 4 },
                    { 3, "Email Id", "EmailID", 4 },
                    { 2, "Are you Patient", "NotPatient", 3 },
                    { 1, "Selected Location", "SelectedLocation", 2 },
                    { 5, "First Name", "FName", 5 }
                });

            migrationBuilder.InsertData(
                table: "FieldCustomerMap",
                columns: new[] { "FieldCustomerMapId", "CustomerId", "FieldId", "IsEnable" },
                values: new object[,]
                {
                    { 8, 1, 8, true },
                    { 11, 1, 11, true },
                    { 10, 1, 10, true },
                    { 9, 1, 9, true },
                    { 6, 1, 6, true },
                    { 7, 1, 7, true },
                    { 4, 1, 4, true },
                    { 3, 1, 3, true },
                    { 2, 1, 2, true },
                    { 1, 1, 1, true },
                    { 5, 1, 5, true }
                });

            migrationBuilder.InsertData(
                table: "Wizard",
                columns: new[] { "WizardId", "WizardName" },
                values: new object[,]
                {
                    { 6, "Wizard-6" },
                    { 1, "Wizard-1" },
                    { 2, "Wizard-2" },
                    { 3, "Wizard-3" },
                    { 4, "Wizard-4" },
                    { 5, "Wizard-5" },
                    { 7, "Wizard-7" },
                    { 8, "Wizard-8" }
                });

            migrationBuilder.InsertData(
                table: "WizardCustomerMap",
                columns: new[] { "WizardCustomerMapId", "CustomerId", "IsEnable", "WizardId" },
                values: new object[,]
                {
                    { 6, 1, true, 6 },
                    { 5, 1, true, 5 },
                    { 4, 1, true, 4 },
                    { 7, 1, true, 7 },
                    { 2, 1, true, 2 },
                    { 1, 1, true, 1 },
                    { 3, 1, true, 3 },
                    { 8, 1, true, 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "FieldCustomerMap");

            migrationBuilder.DropTable(
                name: "Wizard");

            migrationBuilder.DropTable(
                name: "WizardCustomerMap");
        }
    }
}
