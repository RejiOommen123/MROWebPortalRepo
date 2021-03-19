import Vue from 'vue';
//requestermodule
const state = {
    // TODO: check requestor/requester
    nRequesterID: 0,
    nFacilityID:0,
    nLocationID:0,
    sSelectedLocation: "",
    sSelectedLocationName:"",
    bAreYouPatient: true,
    sRelativeFirstName: '',
    sRelativeLastName: '',
    sSelectedRelation:'',
    sSelectedRelationName: '',
    sRelativeFileArray:[],
    sRelativeFileNameArray:[],
    sPatientFirstName: '',
    sPatientLastName: '',
    sPatientMiddleName: '',
    sPatientPreviousFirstName: '',
    sPatientPreviousLastName: '',
    sPatientPreviousMiddleName: '',
    bPatientNameChanged:false,
    bPatientDeceased:false,
    dtPatientDOB: null,
    sRequesterEmailId: '',
    bConfirmReport: false,
    bEmailVerified:false,
    sAddZipCode: '',
    sAddCity: '',
    sAddState: '',
    sAddStreetAddress: '',
    sAddApartment:'',
    dtRecordRangeStart: '',
    dtRecordRangeEnd: '',
    bRecordMostRecentVisit:false,
    bSpecifyVisit:false,
    sSpecifyVisitText:'',
    bRTManualSelection:false,
    sSelectedRecordTypes: [],
    sOtherRTText:'',
    sSelectedWaiver:[],
    bWaiverAccepted:false,
    sSelectedPrimaryReasons: [],
    sSelectedPrimaryReasonsName: '',
    sReleaseTo: '',
    sReleaseToName:'',
    sSelectedShipmentTypes: [],
    sSelectedShipmentTypesName:'',
    sSTFaxNumber:'',
    sSTFaxCompAdd:'',
    sSTEmailAddress:'',
    sSTAddZipCode: '',
    sSTAddCity: '',
    sSTAddState: '',
    sSTAddStreetAddress:'',
    sSTAddApartment:'',
    sRecipientFirstName: '',
    sRecipientLastName: '',
    sRecipientOrganizationName: '',
    sRecipientAddZipCode: '',
    sRecipientAddCity:'',
    sRecipientAddState: '',
    sRecipientAddStreetAddress: '',
    sRecipientAddApartment:'',

   

    sSelectedSensitiveInfo: [],
    dtAuthExpire : null,
    sAuthSpecificEvent : '',
    bDeadlineStatus:false,
    dtDeadline :'',
    sAdditionalData:'',
    sPhoneNo:'',
    bPhoneNoVerified:false,
    sIdentityIdName:'',
    sIdentityImage:'',
    sSignatureData:'',
    bRequestorFormSubmitted:false,
    bRequestAnotherRecord:false,
    nFeedbackRating:0,
    sFeedbackComment:'',   
    sWizardName:'',
    bForceCompliance:false,
    sGUID:''
}
const mutations = {
    completeState(state,requester){
        Object.assign(state, requester);
    },
    nRequesterID(state, payload) {
        state.nRequesterID = payload;
    },
    nFacilityID(state, payload) {
        state.nFacilityID = payload;
    },
    nLocationID(state, payload) {
        state.nLocationID = payload;
    },
    sSelectedLocation(state, payload) {
        state.sSelectedLocation = payload;
    },
    sSelectedLocationName(state, payload) {
        state.sSelectedLocationName = payload;
    },
    bAreYouPatient(state, payload) {
        state.bAreYouPatient = payload;
    },
    sRelativeFirstName(state, payload) {
        state.sRelativeFirstName = payload;
    },
    sRelativeLastName(state, payload) {
        state.sRelativeLastName = payload;
    },
    sSelectedRelation(state, payload) {
        state.sSelectedRelation = payload;
    },
    sSelectedRelationName(state, payload) {
        state.sSelectedRelationName = payload;
    },
    sRelativeFileArray(state, payload){
        state.sRelativeFileArray = payload;
    },
    sRelativeFileNameArray(state, payload){
        state.sRelativeFileNameArray = payload;
    },
    sPatientFirstName(state, payload) {
        state.sPatientFirstName = payload;
    },
    sPatientLastName(state, payload) {
        state.sPatientLastName = payload;
    },
    sPatientMiddleName(state, payload) {
        state.sPatientMiddleName = payload;
    },
    sPatientPreviousFirstName(state, payload) {
        state.sPatientPreviousFirstName = payload;
    },
    sPatientPreviousLastName(state, payload) {
        state.sPatientPreviousLastName = payload;
    },
    sPatientPreviousMiddleName(state, payload) {
        state.sPatientPreviousMiddleName = payload;
    },
    bPatientNameChanged(state, payload) {
        state.bPatientNameChanged = payload;
    },
    bPatientDeceased(state, payload) {
        state.bPatientDeceased = payload;
    },
    dtPatientDOB(state, payload) {
        state.dtPatientDOB = payload;
    },
    sRequesterEmailId(state, payload) {
        state.sRequesterEmailId = payload;
    },
    bConfirmReport(state, payload) {
        state.bConfirmReport = payload;
    },
    bEmailVerified(state, payload) {
        state.bEmailVerified = payload;
    },
    sAddZipCode(state, payload) {
        state.sAddZipCode = payload;
    },
    sAddCity(state, payload) {
        state.sAddCity = payload;
    },
    sAddState(state, payload) {
        state.sAddState = payload;
    },
    sAddStreetAddress(state, payload) {
        state.sAddStreetAddress = payload;
    },
    sAddApartment(state, payload) {
        state.sAddApartment = payload;
    },
    dtRecordRangeStart(state, payload) {
        state.dtRecordRangeStart = payload;
    },
    dtRecordRangeEnd(state, payload) {
        state.dtRecordRangeEnd = payload;
    },
    bRecordMostRecentVisit(state, payload) {
        state.bRecordMostRecentVisit = payload;
    },
    bSpecifyVisit(state, payload) {
        state.bSpecifyVisit = payload;
    },
    sSpecifyVisitText(state, payload) {
        state.sSpecifyVisitText = payload;
    },
    bRTManualSelection(state, payload) {
        state.bRTManualSelection = payload;
    },
    sSelectedRecordTypes(state, payload) {
        state.sSelectedRecordTypes = payload;
    },
    sOtherRTText(state, payload) {
        state.sOtherRTText = payload;
    },
    sSelectedWaiver(state, payload) {
        state.sSelectedWaiver = payload;
    },
    bWaiverAccepted(state, payload) {
        state.bWaiverAccepted = payload;
    },
    sSelectedPrimaryReasons(state, payload) {
        state.sSelectedPrimaryReasons = payload;
    },
    sSelectedPrimaryReasonsName(state, payload) {
        state.sSelectedPrimaryReasonsName = payload;
    },
    sReleaseTo(state, payload) {
        state.sReleaseTo = payload;
    },
    sReleaseToName(state, payload) {
        state.sReleaseToName = payload;
    },
    sSelectedShipmentTypes(state, payload) {
        state.sSelectedShipmentTypes = payload;
    },
    sSelectedShipmentTypesName(state, payload) {
        state.sSelectedShipmentTypesName = payload;
    },
    sSTEmailAddress(state, payload) {
        state.sSTEmailAddress = payload;
    },
    sSTFaxNumber(state, payload) {
        state.sSTFaxNumber = payload;
    },
    sSTFaxCompAdd(state, payload) {
        state.sSTFaxCompAdd = payload;
    },
    sSTAddZipCode(state, payload) {
        state.sSTAddZipCode = payload;
    },
    sSTAddCity(state, payload) {
        state.sSTAddCity = payload;
    },
    sSTAddState(state, payload) {
        state.sSTAddState = payload;
    },
    sSTAddStreetAddress(state, payload) {
        state.sSTAddStreetAddress = payload;
    },
    sSTAddApartment(state, payload) {
        state.sSTAddApartment = payload;
    },
    sRecipientFirstName(state, payload) {
        state.sRecipientFirstName = payload;
    },
    sRecipientOrganizationName(state, payload) {
        state.sRecipientOrganizationName = payload;
    },
    sRecipientLastName(state, payload) {
        state.sRecipientLastName = payload;
    },
    sRecipientAddApartment(state, payload) {
        state.sRecipientAddApartment = payload;
    },
    sRecipientAddStreetAddress(state, payload) {
        state.sRecipientAddStreetAddress = payload;
    },
    sRecipientAddCity(state, payload) {
        state.sRecipientAddCity = payload;
    },
    sRecipientAddState(state, payload) {
        state.sRecipientAddState = payload;
    },
    sRecipientAddZipCode(state, payload) {
        state.sRecipientAddZipCode = payload;
    },

    



    sSelectedSensitiveInfo(state, payload) {
        state.sSelectedSensitiveInfo = payload;
    },
    dtAuthExpire(state, payload) {
        state.dtAuthExpire = payload;
    },
    sAuthSpecificEvent(state, payload) {
        state.sAuthSpecificEvent = payload;
    },
    bDeadlineStatus(state, payload) {
        state.bDeadlineStatus = payload;
    },
    dtDeadline(state, payload) {
        state.dtDeadline  = payload;
    },
    sPhoneNo(state, payload) {
        state.sPhoneNo = payload;
    },
    bPhoneNoVerified(state, payload) {
        state.bPhoneNoVerified = payload;
    },
    sAdditionalData(state, payload) {
        state.sAdditionalData = payload;
    },
    sIdentityIdName(state, payload) {
        state.sIdentityIdName = payload;
    },
    sIdentityImage(state, payload) {
        state.sIdentityImage = payload;
    },
    sSignatureData(state, payload) {
        state.sSignatureData = payload;
    },
    bRequestorFormSubmitted(state, payload) {
        state.bRequestorFormSubmitted = payload;
    },
    bRequestAnotherRecord(state, payload) {
        state.requestAnotherRecord = payload;
    },
    nFeedbackRating(state, payload) {
        state.nFeedbackRating = payload;
    },
    sFeedbackComment(state, payload) {
        state.sFeedbackComment = payload;
    },
    sWizardName(state, payload) {
        state.sWizardName = payload;
    },
    bForceCompliance(state, payload) {
        state.bForceCompliance = payload;
    },
    sGUID(state, payload) {
        state.sGUID = payload;
    }
}
const actions = {
    async partialAddReq({ commit,rootState}) {
        commit("requestermodule/sWizardName", rootState.ConfigModule.selectedWizard,{ root: true });
        let requester=rootState.requestermodule;
        var requesterObj={
            bAreYouPatient: requester.bAreYouPatient,
            bConfirmReport: requester.bConfirmReport,
            bDeadlineStatus: requester.bDeadlineStatus,
            bEmailVerified: requester.bEmailVerified,
            bForceCompliance: requester.bForceCompliance,
            bPatientNameChanged: requester.bPatientNameChanged,
            bPatientDeceased: requester.bPatientDeceased,
            bPhoneNoVerified: requester.bPhoneNoVerified,
            bRTManualSelection: requester.bRTManualSelection,
            bRecordMostRecentVisit: requester.bRecordMostRecentVisit,
            bRequestAnotherRecord: requester.bRequestAnotherRecord,
            bRequestorFormSubmitted: requester.bRequestorFormSubmitted,
            bSpecifyVisit: requester.bSpecifyVisit,
            dtAuthExpire: requester.dtAuthExpire,
            dtDeadline: requester.dtDeadline,
            dtPatientDOB: requester.dtPatientDOB,
            dtRecordRangeEnd: requester.dtRecordRangeEnd,
            dtRecordRangeStart: requester.dtRecordRangeStart,
            nFacilityID: requester.nFacilityID,
            nFeedbackRating: requester.nFeedbackRating,
            nLocationID: requester.nLocationID,
            nRequesterID: requester.nRequesterID,
            sAddApartment: requester.sAddApartment,
            sAddCity: requester.sAddCity,
            sAddState: requester.sAddState,
            sAddStreetAddress: requester.sAddStreetAddress,
            sAddZipCode: requester.sAddZipCode,
            sAdditionalData: requester.sAdditionalData,
            sAuthSpecificEvent: requester.sAuthSpecificEvent,
            sFeedbackComment: requester.sFeedbackComment,
            sGUID: requester.sGUID,
            sIdentityIdName: requester.sIdentityIdName,
            sIdentityImage: '',
            sOtherRTText: requester.sOtherRTText,
            bWaiverAccepted:requester.bWaiverAccepted,
            sPatientFirstName: requester.sPatientFirstName,
            sPatientLastName: requester.sPatientLastName,
            sPatientMiddleName: requester.sPatientMiddleName,
            sPatientPreviousFirstName: requester.sPatientPreviousFirstName,
            sPatientPreviousLastName: requester.sPatientPreviousLastName,
            sPatientPreviousMiddleName: requester.sPatientPreviousMiddleName,
            sPhoneNo: requester.sPhoneNo,
            sRecipientAddApartment: requester.sRecipientAddApartment,
            sRecipientAddCity: requester.sRecipientAddCity,
            sRecipientAddState: requester.sRecipientAddState,
            sRecipientAddStreetAddress: requester.sRecipientAddStreetAddress,
            sRecipientAddZipCode: requester.sRecipientAddZipCode,
            sRecipientFirstName: requester.sRecipientFirstName,
            sRecipientLastName: requester.sRecipientLastName,
            sRecipientOrganizationName: requester.sRecipientOrganizationName,
            sRelativeFileArray: [],
            sRelativeFileNameArray: requester.sRelativeFileNameArray,
            sRelativeFirstName: requester.sRelativeFirstName,
            sRelativeLastName: requester.sRelativeLastName,
            sReleaseTo: requester.sReleaseTo,
            sReleaseToName: requester.sReleaseToName,
            sRequesterEmailId: requester.sRequesterEmailId,
            sSTAddApartment: requester.sSTAddApartment,
            sSTAddCity: requester.sSTAddCity,
            sSTAddState: requester.sSTAddState,
            sSTAddStreetAddress: requester.sSTAddStreetAddress,
            sSTAddZipCode: requester.sSTAddZipCode,
            sSTEmailAddress: requester.sSTEmailAddress,
            sSTFaxCompAdd: requester.sSTFaxCompAdd,
            sSTFaxNumber: requester.sSTFaxNumber,
            sSelectedLocation: requester.sSelectedLocation,
            sSelectedLocationName: requester.sSelectedLocationName,
            sSelectedPrimaryReasons: requester.sSelectedPrimaryReasons,
            sSelectedPrimaryReasonsName: requester.sSelectedPrimaryReasonsName,
            sSelectedRecordTypes: requester.sSelectedRecordTypes,
            sSelectedRelation: requester.sSelectedRelation,
            sSelectedRelationName: requester.sSelectedRelationName,
            sSelectedSensitiveInfo: requester.sSelectedSensitiveInfo,
            sSelectedShipmentTypes: requester.sSelectedShipmentTypes,
            sSelectedShipmentTypesName: requester.sSelectedShipmentTypesName,
            sSignatureData: requester.sSignatureData,
            sSpecifyVisitText: requester.sSpecifyVisitText,
            sWizardName: requester.sWizardName,
        }
        if(rootState.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[rootState.ConfigModule.selectedWizard]==1)
        {
           Vue.http.post("requesters/AddRequester/",requesterObj)
          .then(response => {
           commit("requestermodule/nRequesterID", response.body,{ root: true });
          });
        }
    }
}
const getter = {}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getter
}