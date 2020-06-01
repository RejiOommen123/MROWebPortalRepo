using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MROWebApi.Migrations
{
    public partial class CodeFirstTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lstPrimaryReasons",
                columns: table => new
                {
                    nPrimaryReasonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sPrimaryReasonName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lst_PrimaryReason", x => x.nPrimaryReasonID);
                });

            migrationBuilder.CreateTable(
                name: "lstRecordTypes",
                columns: table => new
                {
                    nRecordTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sRecordTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lst_RecordType", x => x.nRecordTypeID);
                });

            migrationBuilder.CreateTable(
                name: "lstSensitiveInfo",
                columns: table => new
                {
                    nSensitiveInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sSensitiveInfoName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lst_SensitiveInfor", x => x.nSensitiveInfoID);
                });

            migrationBuilder.CreateTable(
                name: "lstWizards",
                columns: table => new
                {
                    nWizardID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sWizardName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizard", x => x.nWizardID);
                });

            migrationBuilder.CreateTable(
                name: "tblROIFacilities",
                columns: table => new
                {
                    nROIFacilityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    sFacilityName = table.Column<string>(nullable: true),
                    sDescription = table.Column<string>(nullable: true),
                    sSMTPUsername = table.Column<string>(nullable: true),
                    sSMTPPassword = table.Column<string>(nullable: true),
                    sSMTPUrl = table.Column<string>(nullable: true),
                    sFTPUsername = table.Column<string>(nullable: true),
                    sFTPPassword = table.Column<string>(nullable: true),
                    sFTPUrl = table.Column<string>(nullable: true),
                    sOutboundEmail = table.Column<string>(nullable: true),
                    bActiveStatus = table.Column<bool>(nullable: false),
                    sConfigShowFields = table.Column<string>(nullable: false),
                    sConfigShowWizards = table.Column<string>(nullable: true),
                    sConfigFacilityLogo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.nROIFacilityID);
                });

            migrationBuilder.CreateTable(
                name: "lstFields",
                columns: table => new
                {
                    nFieldID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nWizardID = table.Column<int>(nullable: false),
                    sFieldName = table.Column<string>(nullable: true),
                    sNormalizedFieldName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.nFieldID);
                    table.ForeignKey(
                        name: "FK_Field_Wizard",
                        column: x => x.nWizardID,
                        principalTable: "lstWizards",
                        principalColumn: "nWizardID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnkROIFacilityPrimaryReasons",
                columns: table => new
                {
                    nPrimaryReasonID = table.Column<int>(nullable: false),
                    nROIFacilityID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityPrimaryReasonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sPrimaryReasonName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnkROIFacilityPrimaryReasons", x => new { x.nPrimaryReasonID, x.nROIFacilityID });
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityPrimaryReasons_nPrimaryReasonID",
                        column: x => x.nPrimaryReasonID,
                        principalTable: "lstPrimaryReasons",
                        principalColumn: "nPrimaryReasonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityPrimaryReasons_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnkROIFacilityRecordTypes",
                columns: table => new
                {
                    nROIFacilityID = table.Column<int>(nullable: false),
                    nRecordTypeID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityRecordTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sRecordTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnkROIFacilityRecordTypes", x => new { x.nROIFacilityID, x.nRecordTypeID });
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityRecordTypes_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityRecordTypes_nRecordTypeID",
                        column: x => x.nRecordTypeID,
                        principalTable: "lstRecordTypes",
                        principalColumn: "nRecordTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnkROIFacilitySensitiveInfo",
                columns: table => new
                {
                    nSensitiveInfoID = table.Column<int>(nullable: false),
                    nROIFacilityID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilitySensitiveInfo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sSensitiveInfoName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnkROIFacilitySensitiveInfo", x => new { x.nROIFacilityID, x.nSensitiveInfoID });
                    table.ForeignKey(
                        name: "FK_lnkROIFacilitySensitiveInfo_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_lnkROIFacilitySensitiveInfo_nSensitiveInfoID",
                        column: x => x.nSensitiveInfoID,
                        principalTable: "lstSensitiveInfo",
                        principalColumn: "nSensitiveInfoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblROILocations",
                columns: table => new
                {
                    nROIFacilityID = table.Column<int>(nullable: false),
                    nLocationID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    sLocationCode = table.Column<int>(nullable: true),
                    sLocationName = table.Column<string>(maxLength: 30, nullable: true),
                    sLocationAddress = table.Column<string>(fixedLength: true, maxLength: 50, nullable: true),
                    nPhoneNo = table.Column<int>(nullable: true),
                    nFaxNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => new { x.nROIFacilityID, x.nLocationID });
                    table.ForeignKey(
                        name: "FK_Location_Facility",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTempRequestors",
                columns: table => new
                {
                    nRequestorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityID = table.Column<int>(nullable: true),
                    bAreYouPatient = table.Column<bool>(nullable: true),
                    sRelativeName = table.Column<string>(maxLength: 30, nullable: true),
                    sRelationship = table.Column<string>(maxLength: 20, nullable: true),
                    sEmailId = table.Column<string>(maxLength: 30, nullable: true),
                    sPatientFirstName = table.Column<string>(maxLength: 20, nullable: true),
                    sPatientLastName = table.Column<string>(maxLength: 50, nullable: true),
                    sPatientMiddleInitial = table.Column<string>(maxLength: 50, nullable: true),
                    bIsPatientDeceased = table.Column<bool>(nullable: true),
                    sPatientZip = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    sPatientStreetAddress = table.Column<string>(maxLength: 20, nullable: true),
                    dtPatientDOB = table.Column<DateTime>(type: "date", nullable: true),
                    dtRecordTimeFrameStart = table.Column<DateTime>(type: "date", nullable: true),
                    dtRecordTimeFrameEnd = table.Column<DateTime>(type: "date", nullable: true),
                    sRecordType = table.Column<string>(nullable: true),
                    sRecordTypeOther = table.Column<string>(nullable: true),
                    sSensitiveData = table.Column<string>(nullable: true),
                    dtAuthExpireDate = table.Column<DateTime>(type: "date", nullable: true),
                    bRequestDeadline = table.Column<bool>(nullable: true),
                    dtRequestDeadlineDate = table.Column<DateTime>(type: "date", nullable: true),
                    sWhomReleaseRecord = table.Column<string>(maxLength: 30, nullable: true),
                    sWayOfSendRecord = table.Column<string>(maxLength: 30, nullable: true),
                    sWhomReleaseRecordName = table.Column<string>(maxLength: 30, nullable: true),
                    sWhomReleaseRecordZip = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    sWhomReleaseRecordStreetAdd = table.Column<string>(maxLength: 30, nullable: true),
                    sAdditionalData = table.Column<string>(nullable: true),
                    sPhoneNo = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    sPhotoIdentity = table.Column<string>(nullable: true),
                    sSignature = table.Column<string>(nullable: true),
                    sPDF = table.Column<string>(nullable: true),
                    bToolEasyToUse = table.Column<bool>(nullable: true),
                    sToolTextFeedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTempRequestors", x => x.nRequestorID);
                    table.ForeignKey(
                        name: "FK_tblTempRequestors_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnkROIFacilityFieldMaps",
                columns: table => new
                {
                    nROIFacilityID = table.Column<int>(nullable: false),
                    nFieldID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityFieldMapID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnkROIFacilityFieldMaps_1", x => new { x.nROIFacilityID, x.nFieldID });
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityFieldMaps_nFieldID",
                        column: x => x.nFieldID,
                        principalTable: "lstFields",
                        principalColumn: "nFieldID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityFieldMaps_tblROIFacilities",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblRequestors",
                columns: table => new
                {
                    nRequestorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityID = table.Column<int>(nullable: true),
                    nLocationID = table.Column<int>(nullable: true),
                    bAreYouPatient = table.Column<bool>(nullable: true),
                    sRelativeName = table.Column<string>(maxLength: 30, nullable: true),
                    sRelationship = table.Column<string>(maxLength: 20, nullable: true),
                    sEmailId = table.Column<string>(maxLength: 30, nullable: true),
                    sPatientFirstName = table.Column<string>(maxLength: 20, nullable: true),
                    sPatientLastName = table.Column<string>(maxLength: 50, nullable: true),
                    sPatientMiddleInitial = table.Column<string>(maxLength: 50, nullable: true),
                    bIsPatientDeceased = table.Column<bool>(nullable: true),
                    sPatientZip = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    sPatientStreetAddress = table.Column<string>(maxLength: 20, nullable: true),
                    dtPatientDOB = table.Column<DateTime>(type: "date", nullable: true),
                    dtRecordTimeFrameStart = table.Column<DateTime>(type: "date", nullable: true),
                    dtRecordTimeFrameEnd = table.Column<DateTime>(type: "date", nullable: true),
                    sRecordType = table.Column<string>(nullable: true),
                    sRecordTypeOther = table.Column<string>(nullable: true),
                    sSensitiveData = table.Column<string>(nullable: true),
                    dtAuthExpireDate = table.Column<DateTime>(type: "date", nullable: true),
                    bRequestDeadline = table.Column<bool>(nullable: true),
                    dtRequestDeadlineDate = table.Column<DateTime>(type: "date", nullable: true),
                    sWhomReleaseRecord = table.Column<string>(maxLength: 30, nullable: true),
                    sWayOfSendRecord = table.Column<string>(maxLength: 30, nullable: true),
                    sWhomReleaseRecordName = table.Column<string>(maxLength: 30, nullable: true),
                    sWhomReleaseRecordZip = table.Column<string>(unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    sWhomReleaseRecordStreetAdd = table.Column<string>(maxLength: 30, nullable: true),
                    sAdditionalData = table.Column<string>(nullable: true),
                    sPhoneNo = table.Column<string>(unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    sPhotoIdentity = table.Column<string>(nullable: true),
                    sSignature = table.Column<string>(nullable: true),
                    sPDF = table.Column<string>(nullable: true),
                    bToolEasyToUse = table.Column<bool>(nullable: true),
                    sToolTextFeedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRequestors", x => x.nRequestorID);
                    table.ForeignKey(
                        name: "FK_tblRequestors_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblRequestors_nLocationID",
                        columns: x => new { x.nROIFacilityID, x.nLocationID },
                        principalTable: "tblROILocations",
                        principalColumns: new[] { "nROIFacilityID", "nLocationID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "lstPrimaryReasons",
                columns: new[] { "nPrimaryReasonID", "sPrimaryReasonName" },
                values: new object[,]
                {
                    { 1, "Continued Care" },
                    { 2, "Patient Request" },
                    { 3, "Insurance" },
                    { 4, "Social Security Benifits/Claims" },
                    { 5, "OtherReason" },
                    { 6, "Continued Care" }
                });

            migrationBuilder.InsertData(
                table: "lstRecordTypes",
                columns: new[] { "nRecordTypeID", "sRecordTypeName" },
                values: new object[,]
                {
                    { 6, "Radiology Report" },
                    { 5, "Laboratory Report" },
                    { 4, "History and Physical" },
                    { 7, "Other" },
                    { 2, "Discharge Summary" },
                    { 1, "Abstract" },
                    { 3, "Operative Reports" }
                });

            migrationBuilder.InsertData(
                table: "lstSensitiveInfo",
                columns: new[] { "nSensitiveInfoID", "sSensitiveInfoName" },
                values: new object[,]
                {
                    { 1, "HIV Test Results" },
                    { 2, "Behavioural/Mental Health Records" },
                    { 3, "Substance Abuse Information" },
                    { 4, "Sexually Transmitted Dieases" }
                });

            migrationBuilder.InsertData(
                table: "lstWizards",
                columns: new[] { "nWizardID", "sWizardName" },
                values: new object[,]
                {
                    { 8, "Wizard-8" },
                    { 1, "Wizard-1" },
                    { 2, "Wizard-2" },
                    { 3, "Wizard-3" },
                    { 4, "Wizard-4" },
                    { 5, "Wizard-5" },
                    { 6, "Wizard-6" },
                    { 7, "Wizard-7" }
                });

            migrationBuilder.InsertData(
                table: "tblROIFacilities",
                columns: new[] { "nROIFacilityID", "bActiveStatus", "dtCreatedDate", "dtUpdatedDate", "sConfigFacilityLogo", "sConfigShowFields", "sConfigShowWizards", "sCreatedBy", "sDescription", "sFTPPassword", "sFTPUrl", "sFTPUsername", "sFacilityName", "sOutboundEmail", "sSMTPPassword", "sSMTPUrl", "sSMTPUsername", "sUpdatedBy" },
                values: new object[] { 1, true, new DateTime(2020, 6, 1, 17, 54, 45, 428, DateTimeKind.Local).AddTicks(6717), new DateTime(2020, 6, 1, 17, 54, 45, 429, DateTimeKind.Local).AddTicks(6990), "data:image/png;base64,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", "TestFields", "Test Wizards", 1, "Info about Cleveland", "Cleveland@101", "ftp://ftp.cleveland.com/", "Cleveland101", "Cleveland Clinic", "noreply@cleveland.com", "Cleveland@101", "smtp.cleveland.com", "Cleveland101", 1 });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityPrimaryReasons",
                columns: new[] { "nPrimaryReasonID", "nROIFacilityID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityPrimaryReasonID", "sCreatedBy", "sPrimaryReasonName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5118), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5119), 4, 1, "Social Security Benifits/Claims", 1 },
                    { 3, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5115), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5116), 3, 1, "Insurance", 1 },
                    { 2, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5111), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5113), 2, 1, "Patient Request", 1 },
                    { 1, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5043), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5051), 1, 1, "Continued Care", 1 },
                    { 5, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5121), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5122), 5, 1, "OtherReason", 1 },
                    { 6, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5124), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(5125), 6, 1, "Continued Care", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityRecordTypes",
                columns: new[] { "nROIFacilityID", "nRecordTypeID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityRecordTypeID", "sCreatedBy", "sRecordTypeName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 6, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9768), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9769), 6, 1, "Radiology Report", 1 },
                    { 1, 5, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9765), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9766), 5, 1, "Laboratory Report", 1 },
                    { 1, 3, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9759), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9760), 3, 1, "Operative Reports", 1 },
                    { 1, 2, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9756), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9757), 2, 1, "Discharge Summary", 1 },
                    { 1, 1, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9700), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9711), 1, 1, "Abstract", 1 },
                    { 1, 7, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9771), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9772), 7, 1, "Other", 1 },
                    { 1, 4, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9762), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(9763), 4, 1, "History and Physical", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilitySensitiveInfo",
                columns: new[] { "nROIFacilityID", "nSensitiveInfoID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilitySensitiveInfo", "sCreatedBy", "sSensitiveInfoName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4490), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4499), 1, 1, "HIV Test Results", 1 },
                    { 1, 2, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4559), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4561), 2, 1, "Behavioural/Mental Health Records", 1 },
                    { 1, 3, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4563), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4564), 3, 1, "Substance Abuse Information", 1 },
                    { 1, 4, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4566), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(4567), 4, 1, "Sexually Transmitted Dieases", 1 }
                });

            migrationBuilder.InsertData(
                table: "lstFields",
                columns: new[] { "nFieldID", "nWizardID", "sFieldName", "sNormalizedFieldName" },
                values: new object[,]
                {
                    { 1, 2, "Selected Location", "SelectedLocation" },
                    { 11, 8, "Birth Date", "BDay" },
                    { 10, 7, "Street Area", "StreetArea" },
                    { 9, 6, "Postal Code", "PostalCode" },
                    { 8, 5, "Is Patient Deceased", "IsPatientDeceased" },
                    { 7, 5, "Last Name", "LName" },
                    { 6, 5, "Middle Initial", "MInitial" },
                    { 5, 5, "First Name", "FName" },
                    { 4, 4, "Confirm Report", "ConfirmReport" },
                    { 3, 4, "Email Id", "EmailID" },
                    { 2, 3, "Are you Patient", "NotPatient" }
                });

            migrationBuilder.InsertData(
                table: "tblROILocations",
                columns: new[] { "nROIFacilityID", "nLocationID", "dtCreatedDate", "dtUpdatedDate", "nFaxNo", "nPhoneNo", "sCreatedBy", "sLocationAddress", "sLocationCode", "sLocationName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(9260), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(9268), 4026, 4026, 1, "Cleverland Clinic Address", 101, "Cleverland Clinic", 1 },
                    { 1, 2, new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(9351), new DateTime(2020, 6, 1, 17, 54, 45, 433, DateTimeKind.Local).AddTicks(9352), 4026, 4026, 1, "Cleverland Hospital Address", 102, "Cleverland Hospital", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityFieldMaps",
                columns: new[] { "nROIFacilityID", "nFieldID", "bShow", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityFieldMapID", "sCreatedBy", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(201), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(243), 1, 1, 1 },
                    { 1, 2, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(302), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(304), 2, 1, 1 },
                    { 1, 3, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(306), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(307), 3, 1, 1 },
                    { 1, 4, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(310), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(311), 4, 1, 1 },
                    { 1, 5, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(313), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(314), 5, 1, 1 },
                    { 1, 6, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(315), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(317), 6, 1, 1 },
                    { 1, 7, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(319), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(320), 7, 1, 1 },
                    { 1, 8, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(322), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(323), 8, 1, 1 },
                    { 1, 9, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(325), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(326), 9, 1, 1 },
                    { 1, 10, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(328), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(329), 10, 1, 1 },
                    { 1, 11, true, new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(331), new DateTime(2020, 6, 1, 17, 54, 45, 432, DateTimeKind.Local).AddTicks(332), 11, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_lnkROIFacilityFieldMaps_nFieldID",
                table: "lnkROIFacilityFieldMaps",
                column: "nFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_lnkROIFacilityPrimaryReasons_nROIFacilityID",
                table: "lnkROIFacilityPrimaryReasons",
                column: "nROIFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_lnkROIFacilityRecordTypes_nRecordTypeID",
                table: "lnkROIFacilityRecordTypes",
                column: "nRecordTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_lnkROIFacilitySensitiveInfo_nSensitiveInfoID",
                table: "lnkROIFacilitySensitiveInfo",
                column: "nSensitiveInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_lstFields_nWizardID",
                table: "lstFields",
                column: "nWizardID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRequestors_nROIFacilityID_nLocationID",
                table: "tblRequestors",
                columns: new[] { "nROIFacilityID", "nLocationID" });

            migrationBuilder.CreateIndex(
                name: "IX_tblTempRequestors_nROIFacilityID",
                table: "tblTempRequestors",
                column: "nROIFacilityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lnkROIFacilityFieldMaps");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityPrimaryReasons");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityRecordTypes");

            migrationBuilder.DropTable(
                name: "lnkROIFacilitySensitiveInfo");

            migrationBuilder.DropTable(
                name: "tblRequestors");

            migrationBuilder.DropTable(
                name: "tblTempRequestors");

            migrationBuilder.DropTable(
                name: "lstFields");

            migrationBuilder.DropTable(
                name: "lstPrimaryReasons");

            migrationBuilder.DropTable(
                name: "lstRecordTypes");

            migrationBuilder.DropTable(
                name: "lstSensitiveInfo");

            migrationBuilder.DropTable(
                name: "tblROILocations");

            migrationBuilder.DropTable(
                name: "lstWizards");

            migrationBuilder.DropTable(
                name: "tblROIFacilities");
        }
    }
}
