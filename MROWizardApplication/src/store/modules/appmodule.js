//appmodule
const state = {
    QID:[1,2,3,4,5,6,7,8,9],
    askPatientDeceased:false,
    selectedPageNumber:1,
    selectedPage: 'page-1',
    showBackBtn:false,
    selectedLocation:"",
    notPatient:false,
    rname:'',
    relationToPatient:'',
    emailID:'',
    confirmEmailID:'',
    confirmReport:false,
    fname:'',
    lname:'',
    minitial:'',
    isPatientDeceased:false,
    postalCode:'',
    streetArea:'',
    bDay: '',
    imgdata: ''
}
const mutations = {
    mutateaskPatientDeceased(state,payload){
        state.askPatientDeceased = payload
    },
    mutatepageNumerical(state,payload){
        state.selectedPageNumber = payload
    },
    mutateCurrentPage(state,payload){
        state.selectedPage = payload;
    },
    mutateSelectedLocation(state,payload){
        state.selectedLocation = payload;
    },
    mutateNotPatient(state,payload){
        state.notPatient = payload;
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
    mutatername(state,payload){
        state.rname = payload;
    },
    mutaterelationToPatient(state,payload){
        state.relationToPatient = payload;
    },
    mutatefname(state,payload){
        state.fname = payload;
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
    mutatepostalCode(state,payload){
        state.postalCode = payload;
    },
    mutatestreetArea(state,payload){
        state.streetArea = payload;
    },
    mutatebDay(state,payload){
        state.bDay = payload;
    },
    mutatesignature(state, payload) {
        state.imgdata = payload;
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