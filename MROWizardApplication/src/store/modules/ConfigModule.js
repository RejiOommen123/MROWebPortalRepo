//requestermodule
const state = {

    QID: [1, 2, 3, 4, 5, 6, 7, 8, 9],
    dialogMaxWidth: '600px',
    dialogMinWidth: '600px',
    dialogMaxHeight: '620px',
    // selectedPageNumber:14,
    // selectedPage: 'page-14',
  
    // wizardPagesArray:[1,2,3,4,5,6,7,8,9],
    // wp01_disclaimer:'Note: Please use a recent version of Chrome, Firefox, or Safari.',
    // wp02_locationArray : ["Cleverland Clinc","Cleverland Hospital","Cleverland Hospital2"],
    // wp03_disclaimer01:"*If you're not the patient, you'll have the opportunity to supply additional documentation to verify that you're authorized to make this request.",
    // wp03_disclaimer02:'Legal Relation',
    // wp04_disclaimer:'This should be the name given (i.e. maiden name, nickname, other) to the provider at the time of your visit.',
    // wp08_PrimaryReasons: ["Continued Care","Patient Request","Insurance","Attorney","Social Security Benifits/Claim", "Other Reason"],
    // wp09_disclaimer:"* Date range doesn't have to be exact.",
    // wp10_RecordTypes: ["Abstract- Disclaimer for Abstract","History & Physical","Progress Notes","S.O.A.P Notes (Nurse’s assessment notes)",
    // "Consultation Reports","Lab Results","Radiology Reports","Radiology Images","Wound Care Photos","Test Results",
    // "Physician Orders","Psychotherapy Notes (see item 2)","Discharge Summary/Instructions","EKG Reports","ECG Reports","Cardiology Reports",
    // "Catheterization Imaging","Rhythm Strips","Pathology Reports","Pathology Slides"],
    // wp10_SensitiveInfo : ['Behavioral/Mental Health Records','HIV Test Results','Substance Abuse Information','Sexually Transmitted Diease'],
    // wp11_releaseToArray : ["Me (The patient)","A family member/caregiver","Provider/Doctor","A third party (Attorney, Insurance Co., Payer, etc.)"],
    // wp12_ShipmentType: ["Fax","Patient Portal","Email","Mail","In-Person"],
    // wp13_authMonths:12,
    // wp15_disclaimer:"We will respond to your request within 30 days. If you need your records sooner, or if you have an upcoming appointment, please let us know.",
    // wp17_disclaimer:"(This is optional but may help us better fulfill your request.)",
    // wp22_disclaimer:"We will respond to your request within 30 days. If you entered a deadline by which you need your records, or if you have an upcoming appointment, your request will be prioritized accordingly.",
    // wp23_disclaimer:"We'd love your feedback",
    // wp24_disclaimer:"Your request is now complete. You may now close this window.",
    // // wizardArray:["page-1","page-2","page-3","page-4","page-5","page-6","page-7","page-8","page-9","page-10","page-11","page-12","page-13","page-14","page-15","page-16","page-17","page-18","page-19","page-20","page-21","page-22","page-23","page-24"],
    

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
    // mutatepageNumerical(state,payload){
    //     state.selectedPageNumber = payload
    // },
    // mutateCurrentPage(state,payload){
    //     state.selectedPage = payload;
    // },
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
        if (state.wizardArrayIndex == 19) {
            state.showBackBtn = false;
        }
        if(state.bDeadlineStatus==false)
        {
            state.wizardArrayIndex = state.wizardArrayIndex + 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex + 1;
        }
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
    },
    mutatePreviousIndex(state) {
        if (state.wizardArrayIndex == 1 && state.wizardArrayIndex == 21) {
            state.showBackBtn = false;
        }
        if(state.bDeadlineStatus==false)
        {
            state.wizardArrayIndex = state.wizardArrayIndex - 2;
        }
        else{
            state.wizardArrayIndex = state.wizardArrayIndex - 1;
        }        
        state.selectedWizard = state.apiResponseDataByFacilityGUID.oWizards[state.wizardArrayIndex];
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