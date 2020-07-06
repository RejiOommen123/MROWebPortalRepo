//requestermodule
const state = {
    // TODO: check requestor/requester
    nRequesterID: 0,
    nFacilityID:0,
    nLocationID:0,
    sSelectedLocation: "",
    bAreYouPatient: true,
    sRelativeName: '',
    sRelationToPatient: '',
    sRelativeFileArray:[],
    sPatientFirstName: '',
    sPatientLastName: '',
    bIsPatientMinor: false,
    sPatientMiddleInitial: '',
    bIsPatientDeceased: false,
    dtPatientDOB: null,
    sRequesterEmailId: '',
    bConfirmReport: false,
    sAddZipCode: '',
    sAddCity: '',
    sAddState: '',
    sAddStreetAddress: '',
    dtRecordRangeStart: '',
    dtRecordRangeEnd: '',
    sSelectedRecordTypes: [],
    sSelectedPrimaryReasons: [],
    sOtherReasons: '',
    sReleaseTo: '',
    sSelectedShipmentTypes: [],
    nSTFaxNo:0,
    sSTFaxCompAdd:'',
    sSTEmailId:'',
    sSTMailCompAdd:'',
    dtSTPickUp:'',
    sSTRecordFormat:'',

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
    sRelativeName(state, payload) {
        state.sRelativeName = payload;
    },
    sRelationToPatient(state, payload) {
        state.sRelationToPatient = payload;
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
    sPatientMiddleInitial(state, payload) {
        state.sPatientMiddleInitial = payload;
    },
    bIsPatientMinor(state, payload) {
        state.bIsPatientMinor = payload
    },
    bIsPatientDeceased(state, payload) {
        state.bIsPatientDeceased = payload
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
    dtRecordRangeStart(state, payload) {
        state.dtRecordRangeStart = payload;
    },
    dtRecordRangeEnd(state, payload) {
        state.dtRecordRangeEnd = payload;
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
    sSTMailCompAdd(state, payload) {
        state.sSTMailCompAdd = payload;
    },
    dtSTPickUp(state, payload) {
        state.dtSTPickUp = payload;
    },
    sSTRecordFormat(state, payload) {
        state.sSTRecordFormat = payload;
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