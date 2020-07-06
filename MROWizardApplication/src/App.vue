
<template>
  <v-app style="backgroundColor:transparent">
    <v-content>
      <v-row justify="center">
        <!-- Pop up wizard screen -->
        <v-dialog 
          class="wizardDialog"
          persistent
          v-model="dialog"
          scrollable
          :max-width="dialogMaxWidth"
          :height="dialogMaxHeight"
        >
          <!-- Setting background color white if wizard screen is pdf else fetched background -->
          <v-card
            id="bgImg"
            :style="selectedWizard=='Wizard_21'?  {backgroundColor:'white'} : {backgroundImage:`url(${this.backgroundImg})`}  "
          >
            <!-- Wizard top progress bar -->
            <v-progress-linear  color="#e84700"  height="5" :value="nProgressBar"></v-progress-linear>
            <!-- Wizard top close button -->
           

            <v-card-text :style="{ height: dialogMaxHeight}">
              <div>
                <div>
                  <div  v-if="showBackBtn">
                    <button type="button" @click.prevent="previousPage" style="color:#e0e0e0;">
                      <i style="font-size:36px" class="fa fa-angle-left"></i>
                    </button>                    
                  </div>
                  <div v-else>
                    <br/>
                    <br/>
                    </div>
                    <v-btn style="font-size:36px" class="wizardClose" icon dark @click="dialogConfirm=true">
                      <v-icon>mdi-close</v-icon>
                    </v-btn>      
                  <!-- Wizard logo image set here -->
                  <div v-if="selectedWizard!='Wizard_21'">
                    <img
                      :src="this.logoImg"
                      alt="Vue JS"
                      style="vertical-align:middle"
                      id="logoImg"
                    />
                  </div>
                  <transition
                    appear
                    enter-active-class="animated fadeIn"
                    leave-active-class="animated fadeOut"
                    mode="out-in"
                  >
                    <!-- Dynamically loading wizard pages -->
                    <keep-alive>
                      <component :is="selectedWizard"></component>
                    </keep-alive>
                  </transition>
                </div>
              </div>
            </v-card-text>
            <!-- Wizard Footer -->
            <v-footer padless id="footer" style="background-color:transparent">
              <v-col class="text-center" cols="12">
                 <span style="font-size:12px">Powered by</span>
                <a href="https://mrocorp.com/" target="_blank">
                  <img alt="Qries" src="./assets/images/MRO-Web.png" height="30px">
                </a>
                 <br />
                <span>For assistance call (610) 994-7500 Option 1 Mon-Thu 8:30am-8pm Fri 8:30am-6pm</span>              
              </v-col>
            </v-footer>
          </v-card>
        </v-dialog>
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
        <v-dialog v-model="dialogConfirm" light persistent max-width="280">
          <v-card>
            <v-card-title class="headline">Are you sure to close this request?</v-card-title>
            <v-card-text>Closing wizard will clear all data previously enter by you.</v-card-text>
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
export default {
  name: "App",
  data() {
    return {
      dialog: false,
      logoImg: this.$store.state.ConfigModule.wizardLogo,
      backgroundImg: this.$store.state.ConfigModule.wizardBackground,
      phoneNo: 0,
      dialogLoader: false,
      dialogConfirm: false,
    };
  },
  created() {
    this.dialogLoader = true;
    //Get GUID from url
    let urlParams = new URLSearchParams(window.location.search);
    let guid = urlParams.get("guid");

    //API Request get wizard config based on facility guid.
    this.$http
      .get("Wizards/GetFacilityDatafromFacilityGUID/" + guid)
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
          this.phoneNo = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_01_phoneFooter;
          //Check for number of locations in facility
          let locationLength = this.$store.state.ConfigModule
            .apiResponseDataByFacilityGUID.locationDetails.length;
          //disable loader and show wizard pop up screen
          this.dialogLoader = false;
          this.dialog = true;
          //API request to get wizard config data based on location id.
          if (locationLength == 1) {
            let singleLocation = this.$store.state.ConfigModule
              .apiResponseDataByFacilityGUID.locationDetails[0];

            this.$http
              .get(
                "Wizards/GetWizardConfig/fID=" +
                  singleLocation.nFacilityID +
                  "&lID=" +
                  singleLocation.nFacilityLocationID
              )
              .then(response => {
                console.log(response);
                var apiLocationResponse = response.body;
                if (response.body) {
                  this.$store.commit(
                    "ConfigModule/apiResponseDataByLocation",
                    apiLocationResponse
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
                }
              });
          }
          // else{
          // this.dialogLoader = false;
          //   alert("Request Wizard not working contact administrator.");
          // }
        }
      });
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
    }
  },
  methods: {
    previousPage() {
      this.$store.commit("ConfigModule/mutatePreviousIndex");
    },
    //To close wizard pop up window
    dialogClose() {
      this.dialogConfirm = false;
      this.dialog = false;
      window.top.postMessage("hello", "*");
    }
  },
  components: {
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
    Wizard_24: Wizard_24
  }
};

</script>
<style scoped>
@import "./assets/styles/RequestWizard.css";
</style>