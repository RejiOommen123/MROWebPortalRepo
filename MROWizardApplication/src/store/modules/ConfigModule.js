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
    apiResponseDataByLocation1: {oShipmentTypes : [
        {
            "sNormalizedShipmentTypeName": "MROPatientPortal",
            "sShipmentTypeName": "Patient Portal",
            "sFieldToolTip": "Please contact your healthcare provider to setup a patient portal if you do not have one already setup for guidance on how to do so."
        },
        {
            "sNormalizedShipmentTypeName": "MROEmail",
            "sShipmentTypeName": "Email",
            "sFieldToolTip": null
        },
        {
            "sNormalizedShipmentTypeName": "MROMailShipment",
            "sShipmentTypeName": "Mail",
            "sFieldToolTip": null
        },
        {
            "sNormalizedShipmentTypeName": "MROIn-Person",
            "sShipmentTypeName": "In-Person",
            "sFieldToolTip": null
        },
        {
            "sNormalizedShipmentTypeName": "MROFax",
            "sShipmentTypeName": "Fax",
            "sFieldToolTip": "Over certain number of pages will be sent by mail – paper or CD or specify only fax to providers, etc."
        }
    ]},
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
    mutateNextIndex(state) {
        state.showBackBtn = true;
        if(state.bDeadlineStatus=="false")
        {
            state.wizardArrayIndex = state.wizardArrayIndex + 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex + 1;
        }
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
        if (state.selectedWizard == 'Wizard_21' || state.selectedWizard == 'Wizard_21') {
            state.showBackBtn = false;
        }
    },
    mutatePreviousIndex(state) {
     
        if(state.bDeadlineStatus=="false")
        {
            state.wizardArrayIndex = state.wizardArrayIndex - 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex - 1;
        }        
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
        if (state.selectedWizard == 'Wizard_01' || state.selectedWizard == 'Wizard_21') {
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