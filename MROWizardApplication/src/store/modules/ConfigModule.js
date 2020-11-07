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
            "sReleaseTo": "Myself",
            "sNormalizedReleaseTo": "MROReleaseToMyself"
        },
        {
            "sReleaseTo": "A family member/caregiver",
            "sNormalizedReleaseTo": "MROReleaseToFamilyCaregiver"
        },
        {
            "sReleaseTo": "Doctor",
            "sNormalizedReleaseTo": "MROReleaseToDoctor"
        },
        {
            "sReleaseTo": "A third party (Attorney, Insurance Co., Payer, etc.)",
            "sNormalizedReleaseTo": "MROReleaseToThirdParty"
        }
    ],
    nAuthExpirationMonths:0,
    bRTManualSelection:false,
    bDeadlineStatus:true,
    bIdentitySkiped:false,
    bShowRecipientPage:false,
    nTotalWizardPages:0,
    nProgressBar:0,
    nProgressBarIncrValue:0,
    nPrimaryTimeout:86400000,
    nSecondaryTimeout:600000,
    bReturnedForCompliance:false,
    bUnauthorized:false,
    bForceCompliance:null
    // oShipmentTypes:[{"sNormalizedShipmentTypeName":"MROPatientPortal","sShipmentTypeName":"Patient Portal","sFieldToolTip":"Please contact your healthcare provider to setup a patient portal if you do not have one already setup for guidance on how to do so."},{"sNormalizedShipmentTypeName":"MROEmail","sShipmentTypeName":"Email","sFieldToolTip":null},{"sNormalizedShipmentTypeName":"MROMailShipment","sShipmentTypeName":"Mail","sFieldToolTip":null},{"sNormalizedShipmentTypeName":"MROIn-Person","sShipmentTypeName":"In-Person","sFieldToolTip":null},{"sNormalizedShipmentTypeName":"MROFax","sShipmentTypeName":"Fax","sFieldToolTip":"Over certain number of pages will be sent by mail – paper or CD or specify only fax to providers, etc."}]
 
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
    mutateselectedWizard(state, payload) {
        state.selectedWizard = payload;
    },
    setProgressBarIncrValue(state, payload){
        state.nTotalWizardPages = payload.length-2;
        state.nProgressBarIncrValue=100/state.nTotalWizardPages;
    },
    mutateNextIndex(state) {
        state.nProgressBar+=state.nProgressBarIncrValue;
        state.showBackBtn = true;
        if((state.bDeadlineStatus=="false" && state.selectedWizard == 'Wizard_17') || (state.bRTManualSelection==false && state.selectedWizard == 'Wizard_09') || (state.bIdentitySkiped==true && state.selectedWizard == 'Wizard_21'))
        {
            state.wizardArrayIndex = state.wizardArrayIndex + 2;
            state.nProgressBar+=state.nProgressBarIncrValue * 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex + 1;
        }
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
        if (state.selectedWizard == 'Wizard_01' || state.selectedWizard == 'Wizard_23' || state.selectedWizard == 'Wizard_24' || state.selectedWizard == 'Wizard_25' || state.selectedWizard == 'Wizard_26') {
            state.showBackBtn = false;
        }
    },
    mutatePreviousIndex(state) {
        state.nProgressBar-=state.nProgressBarIncrValue;
        state.showBackBtn = true;
        if((state.bDeadlineStatus=="false" && state.selectedWizard == 'Wizard_19') || (state.bRTManualSelection==false && state.selectedWizard == 'Wizard_11') || (state.bIdentitySkiped==true && state.selectedWizard == 'Wizard_23'))
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
    bRTManualSelection(state,payload){
        state.bRTManualSelection = payload;
    },
    bShowRecipientPage(state,payload){
        state.bShowRecipientPage = payload;
    },
    bIdentitySkiped(state,payload){
        state.bIdentitySkiped = payload;
    },
    nPrimaryTimeout(state, payload) {
        state.nPrimaryTimeout = payload ;
    },
    nSecondaryTimeout(state, payload) {
        state.nSecondaryTimeout = payload;
    },  
    bReturnedForCompliance(state, payload) {
        state.bReturnedForCompliance = payload;
    },      
    bUnauthorized(state, payload) {
        state.bUnauthorized = payload;
    },
    bForceCompliance(state, payload) {
        state.bForceCompliance = payload;
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