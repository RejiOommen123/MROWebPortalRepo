//requestermodule
const state = {    
    sSelectedLocation:"",
    bAreYouPatient:true,
    sRelativeName:'',
    sRelationToPatient:'',
    sPatientFirstName:'',

    
    askPatientDeceased:false,     
    emailID:'',
    confirmEmailID:'',
    confirmReport:false,
   
    lname:'',
    minitial:'',
    isPatientDeceased:false,
    isPatientMinor:false,
    zipcode:'',
    cityName:'',
    stateName:'',
    streetAdd:'',
    bDay: '',
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
    sRelativeName(state,payload){
        state.sRelativeName = payload;
    },
    sRelationToPatient(state,payload){
        state.sRelationToPatient = payload;
    },
    sPatientFirstName(state,payload){
        state.sPatientFirstName = payload;
    },



    mutateaskPatientMinor(state,payload){
        state.askPatientMinor = payload
    },
    mutateaskPatientDeceased(state,payload){
        state.askPatientDeceased = payload
    },    
    bAreYouPatient(state,payload){
        state.bAreYouPatient = payload;
    },
    mutateemailID(state,payload){
        state.emailID = payload;
    },
    mutateconfirmEmailID(state,payload){
        state.confirmEmailID = payload;
    },
    mutateconfirmReport(state,payload){
        state.confirmReport = payload;
    },
   
    mutatelname(state,payload){
        state.lname = payload;
    },
    mutateminitial(state,payload){
        state.minitial = payload;
    },
    mutateisPatientDeceased(state,payload){
        state.isPatientDeceased = payload;
    },   
    mutatezipcode(state,payload){
        state.zipcode = payload;
    },
    mutatecityName(state,payload){
        state.cityName = payload;
    },
    mutatestateName(state,payload){
        state.zstateName = payload;
    },
    mutatestreetAdd(state,payload){
        state.streetAdd = payload;
    },
    mutatebDay(state,payload){
        state.bDay = payload;
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