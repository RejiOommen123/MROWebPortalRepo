//requestermodule
const state = {
    // TODO: check requestor/requester
    nRequesterID: 0,
    nFacilityID:0,
    nLocationID:0,
    sSelectedLocation: "",
    bAreYouPatient: true,
    sRelativeFirstName: '',
    sRelativeLastName: '',
    sOtherRelation:'',
    sSelectedRelation: '',
    sRelativeFileArray:[],
    sPatientFirstName: '',
    sPatientLastName: '',
    sPatientMiddleName: '',
    sPreviousPatientFirstName: '',
    sPreviousPatientLastName: '',
    sPreviousPatientMiddleName: '',
    dtPatientDOB: null,
    sRequesterEmailId: '',
    bConfirmReport: false,
    sAddZipCode: '',
    sAddCity: '',
    sAddState: '',
    sAddStreetAddress: '',
    sAddApartment:'',
    dtRecordRangeStart: '',
    dtRecordRangeEnd: '',
    bDRMostRecentVisit:false,
    bRTManualSelection:false,
    sSelectedRecordTypes: [],
    sSelectedPrimaryReasons: [],
    sOtherReasons: '',
    sReleaseTo: '',
    sSelectedShipmentTypes: [],
    nSTFaxNo:0,
    sSTFaxCompAdd:'',
    sSTEmailId:'',
    sSTAddZipCode: '',
    sSTAddCity: '',
    sSTAddState: '',
    sSTAddStreetAddress:'',
    sSTAddApartment:'',
    sRecipientFirstName: '',
    sRecipientLastName: '',
    sRecipientMiddleName: '',
    sRecipientAddZipCode: '',
    sRecipientAddCity:'',
    sRecipientAddState: '',
    sRecipientAddStreetAddress: '',
    sRecipientAddApartment:'',

   

    selectedSensitiveInfo: [],
    dAuthExpire : null,
    sAuthSpecificEvent : '',
    bDeadlineStatus:false,
    dtDeadline :'',
    sAdditionalData:'',
    sPhoneNo:'',
    sIdentityIdName:'',
    sIdentityImage:'',
    sSignatureData:'',
    bRequestAnotherRecord:false,
    nFeedbackRating:0,
    sFeedbackComment:'',   
    sWizardName:''
}
const mutations = {
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
    bAreYouPatient(state, payload) {
        state.bAreYouPatient = payload;
    },
    sRelativeFirstName(state, payload) {
        state.sRelativeFirstName = payload;
    },
    sRelativeLastName(state, payload) {
        state.sRelativeLastName = payload;
    },
    sOtherRelation(state, payload) {
        state.sOtherRelation = payload;
    },
    sSelectedRelation(state, payload) {
        state.sSelectedRelation = payload;
    },
    sRelativeFileArray(state, payload){
        state.sRelativeFileArray = payload;
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
    sPreviousPatientFirstName(state, payload) {
        state.sPreviousPatientFirstName = payload;
    },
    sPreviousPatientLastName(state, payload) {
        state.sPreviousPatientLastName = payload;
    },
    sPreviousPatientMiddleName(state, payload) {
        state.sPreviousPatientMiddleName = payload;
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
    bDRMostRecentVisit(state, payload) {
        state.bDRMostRecentVisit = payload;
    },
    bRTManualSelection(state, payload) {
        state.bRTManualSelection = payload;
    },
    sSelectedRecordTypes(state, payload) {
        state.sSelectedRecordTypes = payload;
    },
    sSelectedPrimaryReasons(state, payload) {
        state.sSelectedPrimaryReasons = payload;
    },
    sOtherReasons(state, payload) {
        state.sOtherReasons = payload;
    },
    sReleaseTo(state, payload) {
        state.sReleaseTo = payload;
    },
    sSelectedShipmentTypes(state, payload) {
        state.sSelectedShipmentTypes = payload;
    },
    sSTEmailId(state, payload) {
        state.sSTEmailId = payload;
    },
    nSTFaxNo(state, payload) {
        state.nSTFaxNo = payload;
    },
    sSTFaxCompAdd(state, payload) {
        state.sSTFaxCompAdd = payload;
    },
    sSTAddZipCode(state, payload) {
        state.sAddZipCode = payload;
    },
    sSTAddCity(state, payload) {
        state.sAddCity = payload;
    },
    sSTAddState(state, payload) {
        state.sAddState = payload;
    },
    sSTAddStreetAddress(state, payload) {
        state.sAddStreetAddress = payload;
    },
    sStAddApartment(state, payload) {
        state.sStAddApartment = payload;
    },
    sRecipientFirstName(state, payload) {
        state.sRecipientFirstName = payload;
    },
    sRecipientMiddleName(state, payload) {
        state.sRecipientMiddleName = payload;
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

    



    selectedSensitiveInfo(state, payload) {
        state.selectedSensitiveInfo = payload;
    },
    dAuthExpire(state, payload) {
        state.dAuthExpire = payload;
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
    }
}
const actions = {}
const getter = {}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getter
}