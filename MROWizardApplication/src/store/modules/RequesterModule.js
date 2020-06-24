//requestermodule
const state = {    
    sSelectedLocation:"",
    bAreYouPatient:true,
    sRelativeName:'',
    sRelationToPatient:'',
    sPatientFirstName:'',
    sPatientLastName:'',
    bIsPatientMinor:false,
    sPatientMiddleInitial:'',
    bIsPatientDeceased:false,
    dPatientDOB: null,
    sPatientEmailId:'',
    bConfirmEmailId:'',
    bConfirmReport:false,  
    nAddZipCode:'',
    sAddCity:'',
    sAddState:'',
    sAddStreetAddress:'',
    dRecordRangeStart : '',
    dRecordRangeEnd : '',
    sSelectedRecordTypes: [],


 
    imgdata: '',
    releaseTo:'',
    authExpireDate : null,
    authSpecificEvent : '',
    deadlineStatus:false,
    deadlineDate:null,
    additionalData:'',
    identityId:'',
    requestAnotherRecord:false,
    rating:0,
    feedback:''
}
const mutations = {
    sSelectedLocation(state,payload){
        state.sSelectedLocation = payload;
    },
    bAreYouPatient(state,payload){
        state.bAreYouPatient = payload;
    },
    sRelativeName(state,payload){
        state.sRelativeName = payload;
    },
    sRelationToPatient(state,payload){
        state.sRelationToPatient = payload;
    },
    sPatientFirstName(state,payload){
        state.sPatientFirstName = payload;
    },
    sPatientLastName(state,payload){
        state.sPatientLastName = payload;
    },      
    sPatientMiddleInitial(state,payload){
        state.sPatientMiddleInitial = payload;
    },    
    bIsPatientMinor(state,payload){
        state.bIsPatientMinor = payload
    },
    bIsPatientDeceased(state,payload){
        state.bIsPatientDeceased = payload
    },    
    dPatientDOB(state,payload){
        state.bDay = payload;
    },
    sPatientEmailId(state,payload){
        state.sPatientEmailId = payload;
    },
    bConfirmEmailId(state,payload){
        state.bConfirmEmailId = payload;
    }, 
    bConfirmReport(state,payload){
        state.bConfirmReport = payload;
    },   
    nAddZipCode(state,payload){
        state.nAddZipCode = payload;
    },
    sAddCity(state,payload){
        state.sAddCity = payload;
    },
    sAddState(state,payload){
        state.sAddState = payload;
    },
    sAddStreetAddress(state,payload){
        state.sAddStreetAddress = payload;
    },
    dRecordRangeStart(state,payload){
        state.dRecordRangeStart = payload;
    },
    dRecordRangeEnd(state,payload){
        state.dRecordRangeEnd = payload;
    },
    sSelectedRecordTypes(state,payload){
        state.sSelectedRecordTypes = payload;
    },
    
    


   
 
    mutatesignature(state, payload) {
        state.imgdata = payload;
    },
    mutatereleaseTo(state, payload) {
        state.releaseTo = payload;
    },
    mutateauthExpireDate(state, payload) {
        state.authExpireDate = payload;
    },
    mutateauthSpecificEvent(state, payload) {
        state.authSpecificEvent = payload;
    },
    mutatedeadlineStatus(state, payload) {
        state.deadlineStatus = payload;
    },
    mutatedeadlineDate(state, payload) {
        state.deadlineDate = payload;
    },
    mutateadditionalData(state, payload) {
        state.additionalData = payload;
    },
    mutateidentityId(state, payload) {
        state.identityId = payload;
    },
    mutaterequestAnotherRecord(state, payload) {
        state.requestAnotherRecord = payload;
    }

}
const actions = {
}
const getter = {
}

export default
    {
        namespaced: true,
        state,
        mutations,
        actions,
        getter
    }