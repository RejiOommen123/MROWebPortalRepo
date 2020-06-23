
<template>
  <v-app style="backgroundColor:transparent">
    <v-content>
      <v-row justify="center">
        <v-dialog v-model="dialog" scrollable :max-width="dialogMaxWidth" :height="dialogMaxHeight">
          <v-card
            id="bgImg"
            :style="selectedWizard=='page-21'?  {backgroundColor:'white'} : {backgroundImage:`url(${this.backgroundImg})`}  "
          >
            <v-card-text :style="{ height: dialogMaxHeight  }">
              <div>
                <div>
                  <div v-if="showBackBtn">
                    <button
                      type="button"
                      @click.prevent="doNothing"
                      style="font-size:36px color:#e0e0e0;"
                    >
                      <i style="font-size:36px" class="fa fa-angle-left"></i>
                    </button>
                  </div>
                  <div v-else>
                    <br />
                    <br />
                  </div>
                  <div v-if="selectedWizard!='page-21'">
                    <img
                      :src="this.logo"
                      height="70px"
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
                    <keep-alive>
                      <component :is="selectedWizard"></component>
                    </keep-alive>
                  </transition>
                  <div v-if="selectedWizard!='page-20'">
                    <!-- <div><p id="contact">Call 123-456-7890 for assistance</p></div>
                    <div id="poweredby">
                    <span>Powered by <a href="https://mrocorp.com/" target="_blank">MRO Corp</a></span>
                    </div>-->
                  </div>
                </div>
              </div>
            </v-card-text>

            <v-footer padless class="font-weight-medium" style="background-color:transparent">
              <v-col class="text-center" cols="12">
                <span>Call 123-456-7890 for assistance</span>
                <br />
                <span>
                  Powered by
                  <a href="https://mrocorp.com/" target="_blank">MRO Corp</a>
                </span>
              </v-col>
            </v-footer>
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
      dialog: true,
      logo: this.$store.state.ConfigModule.wizardLogo,
      backgroundImg: this.$store.state.ConfigModule.wizardBackground,
      phoneNo:0
    };
  },
  created() {
    let urlParams = new URLSearchParams(window.location.search);
    let guid = urlParams.get("guid");
    console.log(window.location.search);
    console.log("GUID Value:-" + guid);
    //API Request get wizard config based on facility id.
    this.$http
      .get(
        "http://localhost:57364/api/Wizards/GetFacilityDatafromFacilityGUID/" +
          guid
      )
      .then(response => {
        var apiFacilityResponse = response.body;
        if (response.body) {
          this.$store.commit("ConfigModule/apiResponseDataByFacilityGUID",apiFacilityResponse);
          this.logo = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.facilityLogoandBackground[0].sConfigLogoData;
          this.backgroundImg = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.facilityLogoandBackground[0].sConfigBackgroundData;
          this.phoneNo=this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_01_phoneFooter;
          console.log("This Logo" + this.logo);
          console.log("This BG" + this.backgroundImg);

          //Check for number of locations in facility
          let locationLength = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.locationDetails.length;

          //API request to get wizard config data based on location id.
          if (locationLength == 1) {
            console.log("Inside Mounted - " + locationLength);
            let singleLocation = this.$store.state.ConfigModule
              .apiResponseDataByFacilityGUID.locationDetails[0];
              // http://localhost:57364/api/Wizards/GetWizardConfig/fID=5&lID=2
            this.$http
              .get(
                "http://localhost:57364/api/Wizards/GetWizardConfig/fID=" +singleLocation.nFacilityID+"&lID="+singleLocation.nFacilityLocationID)
              .then(response => {
                var apiLocationResponse = response.body;
                if (response.body) {
                  this.$store.commit("ConfigModule/apiResponseDataByLocation",apiLocationResponse);
                  this.$store.commit("requestermodule/sSelectedLocation", singleLocation.sNormalizedLocationName);
                  console.log('apiResponseDataByLocation    '+this.$store.state.ConfigModule.apiResponseDataByLocation);
                }
              });
          }
        }
      });
  },
  computed: {
 
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
    // ,
    // facilityLogo(){
    //   return this.$store.state.ConfigModule.LogoAndBackgroundImageforFacility.facilityLogoandBackground.sConfigLogoData;
    // },
    // facilityBackground(){
    //   return this.$store.state.ConfigModule.LogoAndBackgroundImageforFacility.facilityLogoandBackground.sConfigBackgroundData;
    // }
  },
  methods: {
    doNothing() {
      // alert("Working");
      // var currPageAfterClickingBack = this.$store.state.ConfigModule
      //   .selectedPageNumber;
      // --currPageAfterClickingBack;
      // //if it becomes 1 reset v-show to false
      // if (currPageAfterClickingBack == 1) {
      //   this.$store.state.ConfigModule.showBackBtn = false;
      // }
      // if (currPageAfterClickingBack == 12) {
      //   this.$store.commit("ConfigModule/mutatedialogMinWidth", "600px");
      //   this.$store.commit("ConfigModule/mutatedialogMaxWidth", "600px");
      //   this.$store.commit("ConfigModule/mutatedialogMaxHeight", "653px");
      //   this.$vuetify.theme.dark = true;
      // }
      // this.$store.state.ConfigModule.selectedPage =
      //   "page-" + currPageAfterClickingBack;
      // this.$store.commit(
      //   "ConfigModule/mutatepageNumerical",
      //   currPageAfterClickingBack
      // );
      // this.$store.commit(
      //   "ConfigModule/mutateCurrentPage",
      //   this.$store.state.ConfigModule.selectedPage

      this.$store.commit("ConfigModule/mutatePreviousIndex");
    }
  },
  components: {
    "Wizard_01": Wizard_01,
    "Wizard_02": Wizard_02,
    "Wizard_03": Wizard_03,
    "Wizard_04": Wizard_04,
    "Wizard_05": Wizard_05,
    "Wizard_06": Wizard_06,
    "Wizard_07": Wizard_07,
    "Wizard_08": Wizard_08,
    "Wizard_09": Wizard_09,
    "Wizard_10": Wizard_10,
    "Wizard_11": Wizard_11,
    "Wizard_12": Wizard_12,
    "Wizard_13": Wizard_13,
    "Wizard_14": Wizard_14,
    "Wizard_15": Wizard_15,
    "Wizard_16": Wizard_16,
    "Wizard_17": Wizard_17,
    "Wizard_18": Wizard_18,
    "Wizard_19": Wizard_19,
    "Wizard_20": Wizard_20,
    "Wizard_21": Wizard_21,
    "Wizard_22": Wizard_22,
    "Wizard_23": Wizard_23,
    "Wizard_24": Wizard_24
  }
};
</script>
<style scoped>
@import "./assets/styles/RequestWizard.css";
</style>