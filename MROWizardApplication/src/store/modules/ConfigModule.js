//requestermodule
const state = {

    dialogMaxWidth: '600px',
    dialogMinWidth: '600px',
    dialogMaxHeight: '620px',
    
    wizardLogo:'',
    wizardBackground:'',
    showBackBtn:false,
    wizardArrayIndex: 0,
    selectedWizard: "Wizard_01",
    apiResponseDataByFacilityGUID: '',
    apiResponseDataByLocation: '',
    oReleaseRequestTo: [{
            "sReleaseTo": "Me (The patient)",
            "sNormalizedReleaseTo": "MROReleaseToMe"
        },
        {
            "sReleaseTo": "A family member/caregiver",
            "sNormalizedReleaseTo": "MROReleaseToFamily/Caregiver"
        },
        {
            "sReleaseTo": "Provider/Doctor",
            "sNormalizedReleaseTo": "MROReleaseToProvider/Doctor"
        },
        {
            "sReleaseTo": "A third party (Attorney, Insurance Co., Payer, etc.)",
            "sNormalizedReleaseTo": "MROReleaseToThirdParty"
        }
    ],
    nAuthExpirationMonths:0,
    bDeadlineStatus:true,
    nTotalWizardPages:0,
    nProgressBar:0,
    nProgressBarIncrValue:0
 
}
const mutations = {

    wizardLogo(state, payload) {
        state.wizardLogo = payload;
    },
    wizardBackground(state, payload) {
        state.wizardBackground = payload;
    },
    mutatedialogMinWidth(state, payload) {
        state.dialogMinWidth = payload;
    },
    mutatedialogMaxWidth(state, payload) {
        state.dialogMaxWidth = payload;
    },
    mutatedialogMaxHeight(state, payload) {
        state.dialogMaxHeight = payload;
    },
    mutatewizardArrayIndex(state, payload) {
        state.wizardArrayIndex = payload;
    },
    setProgressBarIncrValue(state, payload){
        state.nTotalWizardPages = payload.length-2;
        state.nProgressBarIncrValue=100/state.nTotalWizardPages;
    },
    mutateNextIndex(state) {
        state.nProgressBar+=state.nProgressBarIncrValue;
        state.showBackBtn = true;
        if(state.bDeadlineStatus=="false" && state.selectedWizard == 'Wizard_15')
        {
            state.wizardArrayIndex = state.wizardArrayIndex + 2;
            state.nProgressBar+=state.nProgressBarIncrValue * 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex + 1;
        }
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
        if (state.selectedWizard == 'Wizard_01' || state.selectedWizard == 'Wizard_21' || state.selectedWizard == 'Wizard_22' || state.selectedWizard == 'Wizard_23' || state.selectedWizard == 'Wizard_24') {
            state.showBackBtn = false;
        }
    },
    mutatePreviousIndex(state) {
        state.nProgressBar-=state.nProgressBarIncrValue;
        state.showBackBtn = true;
        if(state.bDeadlineStatus=="false" && state.selectedWizard == 'Wizard_17')
        {
            state.wizardArrayIndex = state.wizardArrayIndex - 2;
            state.nProgressBar-=state.nProgressBarIncrValue * 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex - 1;
        }        
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
        if (state.selectedWizard == 'Wizard_01') {
            state.showBackBtn = false;
        }
    },
    apiResponseDataByFacilityGUID(state, payload) {
        state.apiResponseDataByFacilityGUID = payload;
    },
    apiResponseDataByLocation(state,payload){
        state.apiResponseDataByLocation =  payload;
    },
    nAuthExpirationMonths(state,payload){
        state.nAuthExpirationMonths =  payload;
    },
    bDeadlineStatus(state,payload){
        state.bDeadlineStatus = payload;
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