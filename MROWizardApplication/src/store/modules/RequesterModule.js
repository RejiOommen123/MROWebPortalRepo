﻿//requestermodule
const state = {
    nRequestorID: 0,
    nFacilityID:0,
    nLocationID:0,
    sSelectedLocation: "",
    bAreYouPatient: true,
    sRelativeName: '',
    sRelationToPatient: '',
    sPatientFirstName: '',
    sPatientLastName: '',
    bIsPatientMinor: false,
    sPatientMiddleInitial: '',
    bIsPatientDeceased: false,
    dtPatientDOB: null,
    sPatientEmailId: '',
    sConfirmEmailId: '',
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
    sSTFaxCompAdd:'',
    sSTEmailId:'',
    sSTConfirmEmailId:'',
    sSTMailCompAdd:'',
    dtSTPikeUp:'',

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
}
const mutations = {
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
    sPatientEmailId(state, payload) {
        state.sPatientEmailId = payload;
    },
    sConfirmEmailId(state, payload) {
        state.sConfirmEmailId = payload;
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
    sSTConfirmEmailId(state, payload) {
        state.sSTConfirmEmailId = payload;
    },
    sSTFaxCompAdd(state, payload) {
        state.sSTFaxCompAdd = payload;
    },
    sSTMailCompAdd(state, payload) {
        state.sSTMailCompAdd = payload;
    },
    dtSTPikeUp(state, payload) {
        state.dtSTPikeUp = payload;
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
    dtDeadline (state, payload) {
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