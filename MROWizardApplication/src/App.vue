
<template>
  <v-app :style="selectedWizard=='Wizard_24'?  {backgroundColor:'white'} : {backgroundImage:`url(${this.backgroundImg})`}  ">
    <v-content>
      <v-row justify="center">
        <!-- Pop up wizard screen -->
        <v-dialog 
          id="mainDialog"
          class="wizardDialog"
          persistent
          v-model="dialog"
          scrollable
          :max-width="dialogMaxWidth"
          style="height:100%"
          :content-class="selectedWizard=='Wizard_24'?  'withOverflow' :  'withoutOverflow'"
        >
          <!-- Setting background color white if wizard screen is pdf else fetched background -->
          <v-card>           
           <v-card-title class="pa-4">
              <v-row  id="eXpressMobileHeader">
                <v-col cols="2" align="start" justify="center">
                  <v-btn
                      icon
                      v-if="showBackBtn"
                      @click.prevent="previousPage"
                      class="backAndClose"
                    >
                      <v-icon>mdi-chevron-left</v-icon> <span style="text-decoration: underline;">Back</span>
                    </v-btn> 
                </v-col>
                <v-col cols="8" align="center" justify="center" v-if="selectedWizard!='Wizard_24'">
                  <img
                    :src="this.logoImg"
                    alt="Facility Logo"
                    id="logoImg"
                  />
                </v-col>
                <v-col cols="2" align="end" justify="center">
                  <v-btn
                      icon
                      @click="dialogConfirm=true"
                      class="backAndClose"
                    >
                      <span style="text-decoration: underline;">Close</span> <v-icon>mdi-close</v-icon>
                    </v-btn>    
                </v-col>
              </v-row>
              <v-row style="width:100%">
                <v-col v-if="$vuetify.breakpoint.xs" class="pa-0" cols="12" align="end" justify="center">
                    <a href="#"
                      id="helpBtn"
                      @click="showNeedHelp()"
                    >
                      Need Help?
                    </a>
                    <v-tooltip slot="append" bottom>
                      <template v-slot:activator="{ on }">
                        <v-icon v-on="on" color="customDarkBlue" top>mdi-information</v-icon>
                      </template>
                      <v-col cols="12" sm="12">
                        <p style="width:250px; background-color:transparent">{{disclaimer03}}</p>
                      </v-col>
                    </v-tooltip>
                </v-col>
                <v-col class="py-0" cols="12">
                  <v-progress-linear rounded  color="customLightGrey"  height="20" :value="nProgressBar"><strong>{{ Math.ceil(nProgressBar) }}%</strong></v-progress-linear>
                </v-col>
                <v-col cols="12" md="9" align="left" justify="center" class="titleQuestion">
                  {{titleQuestion}}
                </v-col>
                <v-col v-if="!$vuetify.breakpoint.xs" class="px-0" cols="3" align="right" justify="center">
                    <a href="#"
                      id="helpBtn"
                      @click="showNeedHelp()"
                    >
                      Need Help?
                    </a>
                    <v-tooltip slot="append" bottom>
                      <template v-slot:activator="{ on }">
                        <v-icon v-on="on" color="customDarkBlue" top>mdi-information</v-icon>
                      </template>
                      <v-col cols="12" sm="12">
                        <p style="width:250px; background-color:transparent">{{disclaimer03}}</p>
                      </v-col>
                    </v-tooltip>
                </v-col>
              </v-row>
            </v-card-title>

            <v-card-text :style="{ height: dialogMaxHeight } " class="d-flex pa-0">
                  <ModalIdle/>
                  <ModalUnauthorized/>
                  <ModalNeedHelp/>
                  <SessionTransfer v-if="bShowSessionTransfer" />                  
                  <transition
                    appear
                    enter-active-class="animated fadeIn"
                    leave-active-class="animated fadeOut"
                    mode="out-in"
                  >
                    <!-- Dynamically loading wizard pages -->
                    <keep-alive>
                      <component class="center" :is="selectedWizard"></component>
                    </keep-alive>
                  </transition>
            </v-card-text>            
          </v-card>
        </v-dialog>
        <ErrorDialog/>
        <LoaderDialog/>
        <!-- Loader to indicate wizard is getting ready -->
        <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="primary" dark>
            <v-card-text>
              Please stand by
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- Confirm Close Wizard -->
        <v-dialog v-model="dialogConfirm" light persistent max-width="300">
          <v-card>
            <v-card-title class="headline wordBreakNormal">Are you sure you want to close this request?</v-card-title>
            <v-card-text class="wordBreakNormal" v-if="(selectedWizard !='Wizard_25') && (selectedWizard != 'Wizard_26') && (selectedWizard !='Wizard_27') ">Closing this request will clear all previously entered data.</v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="green darken-1" text @click="dialogConfirm = false">Cancel</v-btn>
              <v-btn color="green darken-1" text @click="dialogClose">Accept</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-row>
    </v-content>
    <br />
  </v-app>
</template>

<script>
import ModalIdle from "./components/ModalIdle";
import ModalUnauthorized from "./components/ModalUnauthorized";
import ModalNeedHelp from "./components/ModalNeedHelp";
import SessionTransfer from "./components/SessionTransfer";
import ErrorDialog from "./components/ErrorDialog";
import LoaderDialog from "./components/LoaderDialog";
import Wizard_01 from "./components/Pages/WizardPage_01";
import Wizard_02 from "./components/Pages/WizardPage_02";
import Wizard_03 from "./components/Pages/WizardPage_03";
import Wizard_04 from "./components/Pages/WizardPage_04";
import Wizard_05 from "./components/Pages/WizardPage_05";
import Wizard_06 from "./components/Pages/WizardPage_06";
import Wizard_07 from "./components/Pages/WizardPage_07";
import Wizard_08 from "./components/Pages/WizardPage_08";
import Wizard_09 from "./components/Pages/WizardPage_09";
import Wizard_10 from "./components/Pages/WizardPage_10";
import Wizard_11 from "./components/Pages/WizardPage_11";
import Wizard_12 from "./components/Pages/WizardPage_12";
import Wizard_13 from "./components/Pages/WizardPage_13";
import Wizard_14 from "./components/Pages/WizardPage_14";
import Wizard_15 from "./components/Pages/WizardPage_15";
import Wizard_16 from "./components/Pages/WizardPage_16";
import Wizard_17 from "./components/Pages/WizardPage_17";
import Wizard_18 from "./components/Pages/WizardPage_18";
import Wizard_19 from "./components/Pages/WizardPage_19";
import Wizard_20 from "./components/Pages/WizardPage_20";
import Wizard_21 from "./components/Pages/WizardPage_21";
import Wizard_22 from "./components/Pages/WizardPage_22";
import Wizard_23 from "./components/Pages/WizardPage_23";
import Wizard_24 from "./components/Pages/WizardPage_24";
import Wizard_25 from "./components/Pages/WizardPage_25";
import Wizard_26 from "./components/Pages/WizardPage_26";
import Wizard_27 from "./components/Pages/WizardPage_27";
export default {
  name: "App",
  data() {
    return {
      dialog: false,
      logoImg: this.$store.state.ConfigModule.wizardLogo,
      backgroundImg: this.$store.state.ConfigModule.wizardBackground,
      dialogLoader: false,
      dialogConfirm: false,
    };
  },
  created() {
    this.dialogLoader = true;
    //Get GUID from url
    let urlParams = new URLSearchParams(window.location.search);
    let guid = urlParams.get("guid");
    let locationguid = urlParams.get("locationguid");
    let orgguid = urlParams.get("orgguid");
    let stguid = urlParams.get("st");
    if(stguid==null){
    this.$store.commit(
      "ConfigModule/sLocationGUID",
      locationguid
    );
    this.$store.commit(
      "ConfigModule/sOrgGUID",
      orgguid
    );
     var guidParameters = {
        guid: guid,
        locationguid: locationguid,
        orgguid: orgguid
      };
    //API Request get wizard config based on facility guid and locationguid.
    this.$http
      .post("Wizards/GetFacilityDatafromFacilityGUID", guidParameters)
      .then(response => {
        var apiFacilityResponse = response.body;
        if (response.body!="") {
          this.$store.commit(
            "ConfigModule/apiResponseDataByFacilityGUID",
            apiFacilityResponse
          );
          this.$store.commit(
            "ConfigModule/wizardLogo",
            apiFacilityResponse.facilityLogoandBackground[0].sConfigLogoData
          );
          this.$store.commit(
            "ConfigModule/wizardBackground",
            apiFacilityResponse.facilityLogoandBackground[0]
              .sConfigBackgroundData
          );
          this.$store.commit(
            "ConfigModule/setProgressBarIncrValue",
            apiFacilityResponse.oWizards
          );
          this.$store.commit(
            "ConfigModule/nPrimaryTimeout",
            apiFacilityResponse.facilityLogoandBackground[0].nPrimaryTimeout
          );
          this.$store.commit(
            "ConfigModule/nSecondaryTimeout",
            apiFacilityResponse.facilityLogoandBackground[0].nSecondaryTimeout
          );
          this.$store.commit(
            "requestermodule/nRequesterID",
            apiFacilityResponse.requesterDetails[0].nRequestorID
          );
          this.$store.commit(
            "requestermodule/sGUID",
            apiFacilityResponse.requesterDetails[0].sGUID
          );
          this.$store.commit(
            "ConfigModule/bForceCompliance",
            apiFacilityResponse.facilityLogoandBackground[0].bForceCompliance
          );
          this.$store.commit(
            "requestermodule/nFacilityID",
            apiFacilityResponse.facilityLogoandBackground[0].nFacilityID
          );
          this.$store.commit(
            "ConfigModule/Wizard_01_phoneFooter",
            apiFacilityResponse.wizardHelper.Wizard_01_phoneFooter
          );
          this.$store.commit(
            "ConfigModule/Wizard_01_disclaimer03",
            apiFacilityResponse.wizardHelper.Wizard_01_disclaimer03
          );
          //Check for number of locations in facility
          let locationLength = this.$store.state.ConfigModule
            .apiResponseDataByFacilityGUID.locationDetails.length;
          //disable loader and show wizard pop up screen
          if(locationLength != 1){
            this.dialogLoader = false;
            this.dialog = true;
          }
          //API request to get wizard config data based on location id.
          if (locationLength == 1) {
            let singleLocation = this.$store.state.ConfigModule
              .apiResponseDataByFacilityGUID.locationDetails[0];
            this.$store.commit(
            "requestermodule/sSelectedLocationName",singleLocation.sLocationName
            );
              
            this.$http
              .get(
                "Wizards/GetWizardConfig/fID=" +
                  singleLocation.nFacilityID +
                  "&lID=" +
                  singleLocation.nFacilityLocationID +
                  "&rID=" +
                  this.$store.state.requestermodule.nRequesterID +
                  "&sLocationGUID=" +
                  this.$store.state.ConfigModule.sLocationGUID +
                  "&sOrgGUID="+
                  this.$store.state.ConfigModule.sOrgGUID +
                  "&bMultiSelected=false"
              )              
              .then(response => {
                var apiLocationResponse = response.body;
                if (response.body) {
                 
                  this.$store.commit("ConfigModule/oWizards",apiLocationResponse.oWizards);
                  this.$store.commit("ConfigModule/wizardsSave", apiLocationResponse.wizardsSave);
                  this.$store.commit("ConfigModule/wizardHelper", apiLocationResponse.oWizardHelper);
                  this.$store.commit(
                    "ConfigModule/Wizard_01_phoneFooter",
                    apiLocationResponse.oWizardHelper.Wizard_01_phoneFooter
                  );
                  this.$store.commit(
                    "ConfigModule/Wizard_01_disclaimer03",
                    apiLocationResponse.oWizardHelper.Wizard_01_disclaimer03
                  );
                  delete apiLocationResponse.oWizards;
                  delete apiLocationResponse.wizardsSave;
                  delete apiLocationResponse.oWizardHelper;
                  
                  this.$store.commit(
                    "ConfigModule/apiResponseDataByLocation",
                    apiLocationResponse
                  );
                  this.$store.commit(
                  "ConfigModule/wizardLogo",
                  apiLocationResponse.oLocations[0].sConfigLogoData
                  );
                  this.$store.commit(
                    "ConfigModule/wizardBackground",
                    apiLocationResponse.oLocations[0].sConfigBackgroundData
                  );   
                  this.$store.commit(
                    "requestermodule/nFacilityID",
                    singleLocation.nFacilityID
                  );
                  this.$store.commit(
                    "requestermodule/nLocationID",
                    singleLocation.nFacilityLocationID
                  );
                  this.$store.commit(
                    "requestermodule/sSelectedLocation",
                    singleLocation.sNormalizedLocationName
                  );
                  this.$store.commit(
                    "ConfigModule/nAuthExpirationMonths",
                    singleLocation.nAuthExpirationMonths
                  );
                  this.$store.commit(
                    "ConfigModule/nPrimaryTimeout",
                    apiLocationResponse.oLocations[0].nPrimaryTimeout
                  );
                  this.$store.commit(
                    "ConfigModule/nSecondaryTimeout",
                    apiLocationResponse.oLocations[0].nSecondaryTimeout
                  );
                  this.$store.commit(
                    "ConfigModule/bForceCompliance",
                    apiLocationResponse.oLocations[0].bForceCompliance
                  );
                  this.dialogLoader = false;
                  this.dialog = true;
                }
              });
          }          
          // else{
          // this.dialogLoader = false;
          //   alert("Request Wizard not working contact administrator.");
          // }
        }
      });
    }
    else{
        this.$http
          .get("requesters/GetSwitchedSession/st="+stguid)
          .then(response => {
            var data = JSON.parse(response.body.data);
          this.$store.commit(
            "requestermodule/completeState",
            data.requesterModule
          );
          this.$store.commit(
            "ConfigModule/completeState",
            data.configModule
          );
          this.$store.commit("ConfigModule/bShowSessionTransfer",false);
          var LoaderDialog = {
          visible : false,
          title : 'Please stand by'
          };
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          this.dialogLoader = false;
          this.dialog = true;
          },
          (err) => {            
          if(err.status == 400){
            var ErrorDialog = {
              visible : true,
              title : 'Error',
              body : ''
            }
            this.dialogLoader = false;

            if(err.body.statusCode == "File_Not_Found" || err.body.statusCode == "Link_Expired" || err.body.statusCode == "Invalid_Url")
            {
              ErrorDialog.body = 'Your current session is expired';     
            }
            else{
              ErrorDialog.body = 'Something went wrong. Please try again or contact us.';  
            }
            this.$store.commit(
                "ConfigModule/ErrorDialog",
                ErrorDialog
              );
          } 
          });
      }
  },
  //Watcher and computed property are set to check for update logo and background in store
  watch: {
    logo(newlogo) {
      this.logoImg = newlogo;
    },
    background(newBackground) {
      this.backgroundImg = newBackground;
    }
  },
  computed: {
    logo() {
      return this.$store.state.ConfigModule.wizardLogo;
    },
    background() {
      return this.$store.state.ConfigModule.wizardBackground;
    },
    phoneNo(){
      return this.$store.state.ConfigModule.Wizard_01_phoneFooter;
    },
    disclaimer03(){
      return this.$store.state.ConfigModule.Wizard_01_disclaimer03;
    },
    nProgressBar() {
      return this.$store.state.ConfigModule.nProgressBar;
    },
    selectedWizard() {
      return this.$store.state.ConfigModule.selectedWizard;
    },
    showBackBtn() {
      return this.$store.state.ConfigModule.showBackBtn;
    },
    dialogMaxWidth() {
      return this.$store.state.ConfigModule.dialogMaxWidth;
    },
    dialogMinWidth() {
      return this.$store.state.ConfigModule.dialogMinWidth;
    },
    dialogMaxHeight() {
      return this.$store.state.ConfigModule.dialogMaxHeight;
    },
    bShowSessionTransfer() {    
      return this.$store.state.ConfigModule.bShowSessionTransfer;
    },
    titleQuestion(){
      return this.$store.state.ConfigModule.titleQuestion;
    }
  },
  methods: {
    previousPage() {
      this.$store.commit("ConfigModule/mutatePreviousIndex");
    },
    showNeedHelp(){
      this.$store.commit("ConfigModule/bShowNeedHelp",true);
    },
    //To close wizard pop up window
    dialogClose() {
      this.dialogConfirm = false;
      this.dialog = false;
      window.top.postMessage("hello", "*");
    }
  },
  components: {
    ModalIdle,
    ModalUnauthorized,
    ModalNeedHelp,
    SessionTransfer,
    ErrorDialog,
    LoaderDialog,
    Wizard_01: Wizard_01,
    Wizard_02: Wizard_02,
    Wizard_03: Wizard_03,
    Wizard_04: Wizard_04,
    Wizard_05: Wizard_05,
    Wizard_06: Wizard_06,
    Wizard_07: Wizard_07,
    Wizard_08: Wizard_08,
    Wizard_09: Wizard_09,
    Wizard_10: Wizard_10,
    Wizard_11: Wizard_11,
    Wizard_12: Wizard_12,
    Wizard_13: Wizard_13,
    Wizard_14: Wizard_14,
    Wizard_15: Wizard_15,
    Wizard_16: Wizard_16,
    Wizard_17: Wizard_17,
    Wizard_18: Wizard_18,
    Wizard_19: Wizard_19,
    Wizard_20: Wizard_20,
    Wizard_21: Wizard_21,
    Wizard_22: Wizard_22,
    Wizard_23: Wizard_23,
    Wizard_24: Wizard_24,
    Wizard_25: Wizard_25,
    Wizard_26: Wizard_26,
    Wizard_27: Wizard_27
  }
};

</script>
<style scoped>
@import "./assets/styles/RequestWizard.css";
</style>