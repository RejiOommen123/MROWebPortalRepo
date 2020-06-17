USE [MRO_RequestWizard]
GO
SET IDENTITY_INSERT [dbo].[lstWizards] ON 
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (1, N'Wizard-1', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (2, N'Wizard-2', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (3, N'Wizard-3', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (4, N'Wizard-4', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (5, N'Wizard-5', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (6, N'Wizard-6', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (7, N'Wizard-7', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (8, N'Wizard-8', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (9, N'Wizard-9', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (10, N'Wizard-10', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (11, N'Wizard-11', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (12, N'Wizard-12', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (13, N'Wizard-13', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (14, N'Wizard-14', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [dtLastUpdate]) VALUES (15, N'Wizard-15', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizards] OFF
GO
SET IDENTITY_INSERT [dbo].[lstFields] ON 
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (1, 2, N'Facility Location Selections', N'SelectedLocation', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (2, 3, N'Are you the patient?', N'AreYouPatient?', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (3, 4, N'What is your\Patient''s email?', N'PEmailId', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (4, 4, N'Confirm to send Email Report checkbox', N'ConfirmReport', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (5, 5, N'Patient First Name', N'PFName', CAST(N'2020-06-08T21:57:10.037' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (6, 5, N'Patient Middle Initial', N'PMInitial', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (7, 5, N'Patient Last Name', N'PLName', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (8, 5, N'Is Patient Deceased', N'IsPatientDeceased', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (9, 6, N'What is the patient''s ZIP code?', N'ZipCode', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (10, 7, N'What is the patient''s street address?', N'StreetArea', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (11, 8, N'When was the patient born?', N'PBirthDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (12, 9, N'What''s the primary reason
for requesting records?', N'PrimaryReason', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (13, 10, N'Specify an approximate*
date range (optional)', N'TFDateRange', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (14, 11, N'Which types of
records would you like?', N'RecordType', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (15, 12, N'Is there any sensitive info you
would also like included?', N'SensitiveInfo', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (16, 13, N'When should this
authorization expire?', N'Authexpire', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate]) VALUES (17, 14, N'How should we send records ?', N'ShipmentType', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstFields] OFF
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] ON 
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate], [nWizardID]) VALUES (1, N'Continued Care', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 9)
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate], [nWizardID]) VALUES (2, N'Patient Request', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 9)
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate], [nWizardID]) VALUES (3, N'Insurance', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 9)
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate], [nWizardID]) VALUES (4, N'Social Security Benefit', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 9)
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [dtLastUpdate], [nWizardID]) VALUES (5, N'Other Reason', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 9)
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] OFF
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] ON 
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (1, N'Abstract', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (2, N'Discharge Summary', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (3, N'Operative Reports', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (4, N'History And Physical', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (5, N'Laboratory Reports', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (6, N'Radiology Report', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [dtLastUpdate], [nWizardID]) VALUES (7, N'Other', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 11)
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] ON 
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate], [nWizardID]) VALUES (1, N'HIV Test Results', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 12)
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate], [nWizardID]) VALUES (2, N'Behavioural/Mental Health Records', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 12)
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate], [nWizardID]) VALUES (3, N'Substance Abuse Information', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 12)
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [dtLastUpdate], [nWizardID]) VALUES (4, N'Sexually Transmitted Dieases', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 12)
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] ON 
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate], [nWizardID]) VALUES (1, N'Patient Portal', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 14)
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate], [nWizardID]) VALUES (2, N'Secure Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 14)
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate], [nWizardID]) VALUES (3, N'Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 14)
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [dtLastUpdate], [nWizardID]) VALUES (4, N'In-Person', CAST(N'2020-08-06T00:00:00.000' AS DateTime), 14)
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] OFF
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientName', N'MROPatientFullName', 1)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientDateOfBirth', N'MROPatientDOB', 2)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientTelephoneNo', N'MROPatientTelephoneNo', 3)
GO
INSERT [dbo].[lstValidateAuthorizationDoc] ([sFieldname], [sKeyword], [nFieldID]) VALUES (N'PatientAddress', N'MROPatientAddress', 4)
GO
