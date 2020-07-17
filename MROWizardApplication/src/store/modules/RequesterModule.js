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
    sPatientFirstName: '',
    sPatientLastName: '',
    sPatientMiddleName: '',
    sPatientPreviousFirstName: '',
    sPatientPreviousLastName: '',
    sPatientPreviousMiddleName: '',
    bPatientNameChanged:false,
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
    bRecordMostRecentVisit:false,
    bRTManualSelection:false,
    sSelectedRecordTypes: [],
    sSelectedPrimaryReasons: [],
    sSelectedPrimaryReasonsName: '',
    sReleaseTo: '',
    sSelectedShipmentTypes: [],
    nSTFaxNumber:0,
    sSTFaxCompAdd:'',
    sSTEmailAddress:'',
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

   

    sSelectedSensitiveInfo: [],
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
    bRecordMostRecentVisit(state, payload) {
        state.bRecordMostRecentVisit = payload;
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
    sSelectedPrimaryReasonsName(state, payload) {
        state.sSelectedPrimaryReasonsName = payload;
    },
    sReleaseTo(state, payload) {
        state.sReleaseTo = payload;
    },
    sSelectedShipmentTypes(state, payload) {
        state.sSelectedShipmentTypes = payload;
    },
    sSTEmailAddress(state, payload) {
        state.sSTEmailAddress = payload;
    },
    nSTFaxNumber(state, payload) {
        state.nSTFaxNumber = payload;
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

    



    sSelectedSensitiveInfo(state, payload) {
        state.sSelectedSensitiveInfo = payload;
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