using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MROWebApi.Migrations
{
    public partial class removelocationrequestorfk : Migration
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
                name: "lstWayOfSendRecord",
                columns: table => new
                {
                    nWayOfSendRecordID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sWayOfSendRecordName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lst_WayOfSendRecord", x => x.nWayOfSendRecordID);
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
                    sConfigShowFields = table.Column<string>(nullable: true),
                    sConfigShowWizards = table.Column<string>(nullable: true)
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
                name: "lnkROIFacilityConnection",
                columns: table => new
                {
                    nROIFacilityConnectionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    sGUID = table.Column<string>(nullable: true),
                    nROIFacilityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROIFacilityConnectionID", x => x.nROIFacilityConnectionID);
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityConnection_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lnkROIFacilityLocations",
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
                    nFaxNo = table.Column<int>(nullable: true),
                    sConfigFacilityLogo = table.Column<string>(nullable: true),
                    sConfigBackgroundImg = table.Column<string>(nullable: true)
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
                    nROIFacilitySensitiveInfoID = table.Column<int>(nullable: false)
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
                name: "lnkROIFacilityWayOfSendRecord",
                columns: table => new
                {
                    nROIFacilityID = table.Column<int>(nullable: false),
                    nWayOfSendRecordID = table.Column<int>(nullable: false),
                    sCreatedBy = table.Column<int>(nullable: false),
                    dtCreatedDate = table.Column<DateTime>(nullable: false),
                    sUpdatedBy = table.Column<int>(nullable: false),
                    dtUpdatedDate = table.Column<DateTime>(nullable: false),
                    nROIFacilityWayOfSendRecordID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sWayOfSendRecordName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lnkROIFacilityWayOfSendRecord", x => new { x.nWayOfSendRecordID, x.nROIFacilityID });
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityWayOfSendRecord_nROIFacilityID",
                        column: x => x.nROIFacilityID,
                        principalTable: "tblROIFacilities",
                        principalColumn: "nROIFacilityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_lnkROIFacilityWayOfSendRecord_nWayOfSendRecordID",
                        column: x => x.nWayOfSendRecordID,
                        principalTable: "lstWayOfSendRecord",
                        principalColumn: "nWayOfSendRecordID",
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
                    { 1, "Abstract" },
                    { 2, "Discharge Summary" },
                    { 3, "Operative Reports" },
                    { 4, "History and Physical" },
                    { 5, "Laboratory Report" },
                    { 6, "Radiology Report" },
                    { 7, "Other" }
                });

            migrationBuilder.InsertData(
                table: "lstSensitiveInfo",
                columns: new[] { "nSensitiveInfoID", "sSensitiveInfoName" },
                values: new object[,]
                {
                    { 4, "Sexually Transmitted Dieases" },
                    { 3, "Substance Abuse Information" },
                    { 2, "Behavioural/Mental Health Records" },
                    { 1, "HIV Test Results" }
                });

            migrationBuilder.InsertData(
                table: "lstWayOfSendRecord",
                columns: new[] { "nWayOfSendRecordID", "sWayOfSendRecordName" },
                values: new object[,]
                {
                    { 1, "Patient Portal" },
                    { 2, "Secure Email" },
                    { 3, "Email" },
                    { 4, "In-Person" }
                });

            migrationBuilder.InsertData(
                table: "lstWizards",
                columns: new[] { "nWizardID", "sWizardName" },
                values: new object[,]
                {
                    { 1, "Wizard-1" },
                    { 2, "Wizard-2" },
                    { 3, "Wizard-3" },
                    { 4, "Wizard-4" },
                    { 5, "Wizard-5" },
                    { 6, "Wizard-6" },
                    { 7, "Wizard-7" },
                    { 8, "Wizard-8" }
                });

            migrationBuilder.InsertData(
                table: "tblROIFacilities",
                columns: new[] { "nROIFacilityID", "bActiveStatus", "dtCreatedDate", "dtUpdatedDate", "sConfigShowFields", "sConfigShowWizards", "sCreatedBy", "sDescription", "sFTPPassword", "sFTPUrl", "sFTPUsername", "sFacilityName", "sOutboundEmail", "sSMTPPassword", "sSMTPUrl", "sSMTPUsername", "sUpdatedBy" },
                values: new object[] { 1, true, new DateTime(2020, 6, 4, 15, 41, 24, 713, DateTimeKind.Local).AddTicks(9174), new DateTime(2020, 6, 4, 15, 41, 24, 714, DateTimeKind.Local).AddTicks(9569), "TestFields", "Test Wizards", 1, "Info about Cleveland", "Cleveland@101", "ftp://ftp.cleveland.com/", "Cleveland101", "Cleveland Clinic", "noreply@cleveland.com", "Cleveland@101", "smtp.cleveland.com", "Cleveland101", 1 });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityConnection",
                columns: new[] { "nROIFacilityConnectionID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityID", "sCreatedBy", "sGUID", "sUpdatedBy" },
                values: new object[] { 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(3055), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(3061), 1, 1, "Test GUID", 1 });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityLocations",
                columns: new[] { "nROIFacilityID", "nLocationID", "dtCreatedDate", "dtUpdatedDate", "nFaxNo", "nPhoneNo", "sConfigBackgroundImg", "sConfigFacilityLogo", "sCreatedBy", "sLocationAddress", "sLocationCode", "sLocationName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(6841), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(6842), 4026, 4026, null, null, 1, "Cleverland Hospital Address", 102, "Cleverland Hospital", 1 },
                    { 1, 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(6740), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(6746), 4026, 4026, null, null, 1, "Cleverland Clinic Address", 101, "Cleverland Clinic", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityPrimaryReasons",
                columns: new[] { "nPrimaryReasonID", "nROIFacilityID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityPrimaryReasonID", "sCreatedBy", "sPrimaryReasonName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6906), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6907), 4, 1, "Social Security Benifits/Claims", 1 },
                    { 6, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6912), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6913), 6, 1, "Continued Care", 1 },
                    { 5, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6909), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6910), 5, 1, "OtherReason", 1 },
                    { 3, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6903), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6904), 3, 1, "Insurance", 1 },
                    { 2, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6900), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6901), 2, 1, "Patient Request", 1 },
                    { 1, 1, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6834), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(6842), 1, 1, "Continued Care", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityRecordTypes",
                columns: new[] { "nROIFacilityID", "nRecordTypeID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityRecordTypeID", "sCreatedBy", "sRecordTypeName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1821), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1822), 3, 1, "Operative Reports", 1 },
                    { 1, 4, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1824), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1825), 4, 1, "History and Physical", 1 },
                    { 1, 5, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1827), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1828), 5, 1, "Laboratory Report", 1 },
                    { 1, 6, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1830), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1831), 6, 1, "Radiology Report", 1 },
                    { 1, 7, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1833), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1834), 7, 1, "Other", 1 },
                    { 1, 1, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1762), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1770), 1, 1, "Abstract", 1 },
                    { 1, 2, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1817), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(1819), 2, 1, "Discharge Summary", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilitySensitiveInfo",
                columns: new[] { "nROIFacilityID", "nSensitiveInfoID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilitySensitiveInfoID", "sCreatedBy", "sSensitiveInfoName", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6507), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6514), 1, 1, "HIV Test Results", 1 },
                    { 1, 2, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6559), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6560), 2, 1, "Behavioural/Mental Health Records", 1 },
                    { 1, 3, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6563), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6564), 3, 1, "Substance Abuse Information", 1 },
                    { 1, 4, new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6566), new DateTime(2020, 6, 4, 15, 41, 24, 718, DateTimeKind.Local).AddTicks(6567), 4, 1, "Sexually Transmitted Dieases", 1 }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityWayOfSendRecord",
                columns: new[] { "nWayOfSendRecordID", "nROIFacilityID", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityWayOfSendRecordID", "sCreatedBy", "sUpdatedBy", "sWayOfSendRecordName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1048), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1055), 1, 1, 1, "Patient Portal" },
                    { 2, 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1120), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1121), 2, 1, 1, "Secure Email" },
                    { 4, 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1127), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1128), 4, 1, 1, "In-Person" },
                    { 3, 1, new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1124), new DateTime(2020, 6, 4, 15, 41, 24, 719, DateTimeKind.Local).AddTicks(1125), 3, 1, 1, "Email" }
                });

            migrationBuilder.InsertData(
                table: "lstFields",
                columns: new[] { "nFieldID", "nWizardID", "sFieldName", "sNormalizedFieldName" },
                values: new object[,]
                {
                    { 11, 8, "Birth Date", "BDay" },
                    { 10, 7, "Street Area", "StreetArea" },
                    { 9, 6, "Postal Code", "PostalCode" },
                    { 8, 5, "Is Patient Deceased", "IsPatientDeceased" },
                    { 7, 5, "Last Name", "LName" },
                    { 6, 5, "Middle Initial", "MInitial" },
                    { 5, 5, "First Name", "FName" },
                    { 4, 4, "Confirm Report", "ConfirmReport" },
                    { 3, 4, "Email Id", "EmailID" },
                    { 2, 3, "Are you Patient", "NotPatient" },
                    { 1, 2, "Selected Location", "SelectedLocation" }
                });

            migrationBuilder.InsertData(
                table: "lnkROIFacilityFieldMaps",
                columns: new[] { "nROIFacilityID", "nFieldID", "bShow", "dtCreatedDate", "dtUpdatedDate", "nROIFacilityFieldMapID", "sCreatedBy", "sUpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1831), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1867), 1, 1, 1 },
                    { 1, 2, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1912), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1913), 2, 1, 1 },
                    { 1, 3, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1916), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1917), 3, 1, 1 },
                    { 1, 4, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1919), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1920), 4, 1, 1 },
                    { 1, 5, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1922), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1923), 5, 1, 1 },
                    { 1, 6, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1925), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1926), 6, 1, 1 },
                    { 1, 7, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1928), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(1929), 7, 1, 1 },
                    { 1, 8, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2028), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2030), 8, 1, 1 },
                    { 1, 9, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2032), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2033), 9, 1, 1 },
                    { 1, 10, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2037), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2038), 10, 1, 1 },
                    { 1, 11, true, new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2040), new DateTime(2020, 6, 4, 15, 41, 24, 717, DateTimeKind.Local).AddTicks(2041), 11, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_lnkROIFacilityConnection_nROIFacilityID",
                table: "lnkROIFacilityConnection",
                column: "nROIFacilityID");

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
                name: "IX_lnkROIFacilityWayOfSendRecord_nROIFacilityID",
                table: "lnkROIFacilityWayOfSendRecord",
                column: "nROIFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_lstFields_nWizardID",
                table: "lstFields",
                column: "nWizardID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRequestors_nROIFacilityID",
                table: "tblRequestors",
                column: "nROIFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_tblTempRequestors_nROIFacilityID",
                table: "tblTempRequestors",
                column: "nROIFacilityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lnkROIFacilityConnection");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityFieldMaps");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityLocations");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityPrimaryReasons");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityRecordTypes");

            migrationBuilder.DropTable(
                name: "lnkROIFacilitySensitiveInfo");

            migrationBuilder.DropTable(
                name: "lnkROIFacilityWayOfSendRecord");

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
                name: "lstWayOfSendRecord");

            migrationBuilder.DropTable(
                name: "tblROIFacilities");

            migrationBuilder.DropTable(
                name: "lstWizards");
        }
    }
}
