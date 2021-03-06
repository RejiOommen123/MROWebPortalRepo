SET IDENTITY_INSERT [dbo].[lstWizards] ON 
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (1, N'Wizard-01', N'Introduction', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (2, N'Wizard-02', N'Locations', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (3, N'Wizard-03', N'Are You Patient', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (4, N'Wizard-04', N'Patient Full Name', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (5, N'Wizard-05', N'Patient DOB', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (6, N'Wizard-06', N'Patient Email', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (7, N'Wizard-07', N'Patient Address', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (8, N'Wizard-08', N'Date Range for requesting records', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (9, N'Wizard-09', N'Record Type', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (10, N'Wizard-10', N'Primary Reason', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (11, N'Wizard-11', N'To Whom should we release', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (12, N'Wizard-12', N'Shipment Type', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (13, N'Wizard-13', N'Authorization expire', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (14, N'Wizard-14', N'Sensitive Info', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (15, N'Wizard-15', N'Is there a deadline for this request', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (16, N'Wizard-16', N'Deadline Value', CAST(N'2020-06-21T19:06:25.137' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (17, N'Wizard-17', N'Additional details', CAST(N'2020-06-21T19:08:10.287' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (18, N'Wizard-18', N'Phone Verification', CAST(N'2020-06-21T19:08:23.620' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (19, N'Wizard-19', N'Photo Identification Document (Driving License)', CAST(N'2020-06-21T19:08:34.470' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (20, N'Wizard-20', N'Camera Option or File Upload', CAST(N'2020-06-21T19:09:00.637' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (21, N'Wizard-21', N'PDF Generation from Authorization Document and Digital Signature', CAST(N'2020-06-21T19:09:27.847' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (23, N'Wizard-22', N'Request Received Success Message', CAST(N'2020-06-21T19:10:24.823' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (24, N'Wizard-23', N'Rating and Feedback', CAST(N'2020-06-21T19:11:30.923' AS DateTime))
GO
INSERT [dbo].[lstWizards] ([nWizardID], [sWizardName], [sWizardDescription], [dtLastUpdate]) VALUES (25, N'Wizard-24', N'Thank you for feedback ', CAST(N'2020-06-21T19:11:32.997' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizards] OFF
GO
SET IDENTITY_INSERT [dbo].[lstFields] ON 
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (1, 2, N'Facility Location Selections', N'MROSelectedLocation', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (2, 3, N'Are you the patient?', N'MROAreYouPatient', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (3, 4, N'What is your\Patient''s email?', N'MROPEmailId', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (4, 4, N'Confirm to send Email Report checkbox', N'MROConfirmReport', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (5, 5, N'Patient First Name', N'MROPatientFirstName', CAST(N'2020-06-08T21:57:10.037' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (6, 5, N'Patient Middle Initial', N'MROPatientMiddleInitial', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (7, 5, N'Patient Last Name', N'MROPatientLastName', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (8, 5, N'Is Patient Deceased', N'MROIsPatientDeceased', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (9, 6, N'What is the patient''s ZIP code?', N'MROZipCode', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (10, 7, N'What is the patient''s street address?', N'MROStreetArea', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (11, 8, N'When was the patient born?', N'MROPBirthDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (12, 9, N'What''s the primary reason
for requesting records?', N'MROPrimaryReason', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (13, 10, N'Specify an approximate*
date range (optional)', N'MROTFDateRange', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (14, 11, N'Which types of
records would you like?', N'MRORecordType', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (15, 12, N'Is there any sensitive info you
would also like included?', N'MROSensitiveInfo', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (16, 13, N'When should this
authorization expire?', N'MROAuthExpireDate', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[lstFields] ([nFieldID], [nWizardID], [sFieldName], [sNormalizedFieldName], [dtLastUpdate], [sFieldToolTip]) VALUES (17, 14, N'How should we send records ?', N'MROShipmentType', CAST(N'2020-08-06T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[lstFields] OFF
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] ON 
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Continued Care', 10, N'MROContinuedCare', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Patient Request', 10, N'MROPatientRequest', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Insurance', 10, N'MROInsurance', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Attorney', 10, N'MROAttorney', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (5, N'Social Security Benefit', 10, N'MROSocialSecurityBenefit', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstPrimaryReasons] ([nPrimaryReasonID], [sPrimaryReasonName], [nWizardID], [sNormalizedPrimaryReasonName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Other Reason', 10, N'MROOtherPrimaryReason', NULL, CAST(N'2020-06-22T12:48:07.853' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstPrimaryReasons] OFF
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] ON 
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Abstract', 9, N'MROAbstract', N'State abstract per facility definition/template design.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'DRS - (Designated Record Set) ', 9, N'MRODRS DesignatedRecordSet)', N'any item, collection, or grouping of protected health information maintained, collected, used, or disseminated by a covered entity” that are used to make decisions about individuals health care or payment of claim.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'History & Physical ', 9, N'MROHistoryPhysical ', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Progress Notes ', 9, N'MROProgressNotes', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (5, N'S.O.A.P Notes (Nurse’s assessment notes)', 9, N'MROSOAPNotesNursesassessmentnotes', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Consultation Reports ', 9, N'MROConsultationReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (7, N'Lab Results ', 9, N'MROLabResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (8, N'Radiology Reports ', 9, N'MRORadiologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:39:56.217' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (9, N'Radiology Images ', 9, N'MRORadiologyImages', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:10.597' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (10, N'Wound Care Photos ', 9, N'MROWoundCarePhotos', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:24.900' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (11, N'Lab Results ', 9, N'MROLabResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:38.990' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (12, N'Test Results ', 9, N'MROTestResults', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:40:49.813' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (13, N'Physician Orders ', 9, N'MROPhysicianOrders', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:00.270' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (14, N'Psychotherapy Notes (see item 2) ', 9, N'MROPsychotherapyNotesseeitem2)', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:17.543' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (15, N'Discharge Summary/Instructions', 9, N'MRODischargeSummaryInstructions', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:33.550' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (16, N'EKG Reports ', 9, N'MROEKGReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:41:49.140' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (17, N'ECG Reports ', 9, N'MROECGReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:08.137' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (18, N'Cardiology Reports', 9, N'MROCardiologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:20.223' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (19, N'Catheterization Imaging ', 9, N'MROCatheterizationImaging', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:30.950' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (20, N'Rhythm Strips ', 9, N'MRORhythmStrips', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:41.513' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (21, N'Pathology Reports ', 9, N'MROPathologyReports', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:42:52.557' AS DateTime))
GO
INSERT [dbo].[lstRecordTypes] ([nRecordTypeID], [sRecordTypeName], [nWizardID], [sNormalizedRecordTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (22, N'Pathology Slides', 9, N'MROPathologySlides', N'AI- Kaitlin to schedule meeting to finalize list. Meeting scheduled for Monday, 6/15/2020 from 3-3:30 pm EST', CAST(N'2020-06-22T12:43:03.360' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstRecordTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] ON 
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'HIV Test Results', 14, N'MROHIVTestResults', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Behavioural/Mental Health Records', 14, N'MROBehavioural/MentalHealthRecords', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Substance Abuse Information', 14, N'MROSubstanceAbuseInformation', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstSensitiveInfo] ([nSensitiveInfoID], [sSensitiveInfoName], [nWizardID], [sNormalizedSensitiveInfoName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'Sexually Transmitted Dieases', 14, N'MROSexuallyTransmittedDieases', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstSensitiveInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[lstShipmentTypes] ON 
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (1, N'Patient Portal', 12, N'MROPatientPortal', N'Please contact your healthcare provider to setup a patient portal if you do not have one already setup for guidance on how to do so.', CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (2, N'Secure Email', 12, N'MROSecureEmail', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (3, N'Email', 12, N'MROEmailShipment', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (4, N'In-Person', 12, N'MROIn-Person', NULL, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[lstShipmentTypes] ([nShipmentTypeID], [sShipmentTypeName], [nWizardID], [sNormalizedShipmentTypeName], [sFieldToolTip], [dtLastUpdate]) VALUES (6, N'Fax', 12, N'MROFax', N'Over certain number of pages will be sent by mail – paper or CD or specify only fax to providers, etc.', CAST(N'2020-06-21T13:14:38.497' AS DateTime))
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
SET IDENTITY_INSERT [dbo].[lstWizardHelper] ON 
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (1, N'disclaimer01', N'Want to print, fax or mail in your request? Click here for our PDF form.', 1, CAST(N'2020-06-22T13:07:57.680' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (2, N'disclaimer02', N'Note: Please use a recent version of Chrome, Firefox, or Safari.', 1, CAST(N'2020-06-22T13:07:57.683' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (3, N'disclaimer03', N'It only takes a few minutes to request your records. <br />Please have a copy of your Government or Photo ID ready.', 1, CAST(N'2020-06-22T13:07:57.687' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (4, N'disclaimer04', N'Request your health records.', 1, CAST(N'2020-06-22T13:07:57.687' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (5, N'disclaimer05', N'Potential Charge may be incurred for the request of records', 1, CAST(N'2020-06-22T13:07:57.690' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (6, N'phoneFooter', N'1234567890', 1, CAST(N'2020-06-22T13:07:57.697' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (7, N'disclaimer01', N'Great! Which location are you requesting records from?', 2, CAST(N'2020-06-22T13:07:57.697' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (8, N'disclaimer01', N'Are you requesting records for yourself?', 3, CAST(N'2020-06-22T13:07:57.700' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (9, N'disclaimer02', N'*If you''re not the patient, you''ll have the opportunity to <br />supply additional documentation to verify that you''re <br />authorized to make this request.', 3, CAST(N'2020-06-22T13:07:57.703' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (10, N'disclaimer03', N'*legal* relationship required with examples such as Parent, Medical POA, Next of Kin etc.', 3, CAST(N'2020-06-22T13:07:57.707' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (11, N'disclaimer04', N'Yes, I want my medical records.', 3, CAST(N'2020-06-22T13:07:57.707' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (12, N'disclaimer05', N'No, I am requesting records for someone else (Child, Dependent, Decedent).', 3, CAST(N'2020-06-22T13:07:57.710' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (13, N'disclaimer06', N'If you are not the patient, you will have the opportunity to supply additional documentation to verify that you are authorized to make this request on their behalf.', 3, CAST(N'2020-06-22T13:07:57.713' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (14, N'disclaimer01', N'This should be the name given (i.e. maiden name, nickname, other) to the provider at the time of your visit.', 4, CAST(N'2020-06-22T13:07:57.717' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (15, N'disclaimer01', N'We will email you a confirmation of your request when you are finished', 7, CAST(N'2020-06-22T13:07:57.720' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (16, N'disclaimer02', N'must be US address', 7, CAST(N'2020-06-22T13:07:57.720' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (17, N'disclaimer01', N'Date range does not have to be exact.', 8, CAST(N'2020-06-22T13:07:57.723' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (18, N'disclaimer01', N'(This is optional but may help us better fulfill your request.)', 10, CAST(N'2020-06-22T13:07:57.723' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (19, N'disclaimer01', N'We will respond to your request within 30 days. If you need your records sooner, or if you have an upcoming appointment, please let us know.', 15, CAST(N'2020-06-22T13:07:57.727' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (20, N'disclaimer01', N'We will do our best to meet your time requirement. Enter date at least one day in the future.', 16, CAST(N'2020-06-22T13:07:57.727' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (21, N'disclaimer01', N'Please enter a valid mobile/cell phone number. We will use this phone number to verify your request with a text code.', 17, CAST(N'2020-06-22T13:07:57.730' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (22, N'disclaimer02', N'Standard messaging rates apply.', 17, CAST(N'2020-06-22T13:07:57.730' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (23, N'disclaimer01', N'Please enter a valid mobile/cell phone number. We will use this phone number to verify your request with a text code.', 18, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (24, N'disclaimer02', N'Note: You will need to take a photo of your selected ID. Please click "Allow" when the browser asks to use your camera on the next screen.', 18, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (25, N'disclaimer01', N'Click below to review your request form and sign your request. Please review all sections for accuracy, as any incorrect information may cause a delay in your records request delivery.', 20, CAST(N'2020-06-22T13:07:57.733' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (26, N'disclaimer01', N'We will respond to your request within 30 days. If you entered a deadline by which you need your records, or if you have an upcoming appointment, your request will be prioritized accordingly.', 22, CAST(N'2020-06-22T13:07:57.737' AS DateTime))
GO
INSERT [dbo].[lstWizardHelper] ([nWizardHelperID], [sWizardHelperType], [sWizardHelperValue], [nWizardID], [dtLastUpdate]) VALUES (27, N'disclaimer01', N'Your request is now complete. You may now close this window', 24, CAST(N'2020-06-22T13:07:57.737' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[lstWizardHelper] OFF
GO
