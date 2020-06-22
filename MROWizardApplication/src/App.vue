
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
                <p>Call 123-456-7890 for assistance</p>
                <p>Powered by <a href="https://mrocorp.com/" target="_blank">MRO Corp</a>
                </p>
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
import WizardPage_01 from "./components/Pages/WizardPage_01";
import WizardPage_02 from "./components/Pages/WizardPage_02";
import WizardPage_03 from "./components/Pages/WizardPage_03";
import WizardPage_04 from "./components/Pages/WizardPage_04";
import WizardPage_05 from "./components/Pages/WizardPage_05";
import WizardPage_06 from "./components/Pages/WizardPage_06";
import WizardPage_07 from "./components/Pages/WizardPage_07";
import WizardPage_08 from "./components/Pages/WizardPage_08";
import WizardPage_09 from "./components/Pages/WizardPage_09";
import WizardPage_10 from "./components/Pages/WizardPage_10";
import WizardPage_11 from "./components/Pages/WizardPage_11";
import WizardPage_12 from "./components/Pages/WizardPage_12";
import WizardPage_13 from "./components/Pages/WizardPage_13";
import WizardPage_14 from "./components/Pages/WizardPage_14";
import WizardPage_15 from "./components/Pages/WizardPage_15";
import WizardPage_16 from "./components/Pages/WizardPage_16";
import WizardPage_17 from "./components/Pages/WizardPage_17";
import WizardPage_18 from "./components/Pages/WizardPage_18";
import WizardPage_19 from "./components/Pages/WizardPage_19";
import WizardPage_20 from "./components/Pages/WizardPage_20";
import WizardPage_21 from "./components/Pages/WizardPage_21";
import WizardPage_22 from "./components/Pages/WizardPage_22";
import WizardPage_23 from "./components/Pages/WizardPage_23";
import WizardPage_24 from "./components/Pages/WizardPage_24";
export default {
  name: "App",
  data() {
    return {
      dialog: true,
      logo: this.$store.state.ConfigModule.wizardLogo,
      backgroundImg: this.$store.state.ConfigModule.wizardBackground
    };
  },
  computed: {
    // selectedPage() {
    //   return this.$store.state.ConfigModule.selectedPage;
    // },
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
    "page-1": WizardPage_01,
    "page-2": WizardPage_02,
    "page-3": WizardPage_03,
    "page-4": WizardPage_04,
    "page-5": WizardPage_05,
    "page-6": WizardPage_06,
    "page-7": WizardPage_07,
    "page-8": WizardPage_08,
    "page-9": WizardPage_09,
    "page-10": WizardPage_10,
    "page-11": WizardPage_11,
    "page-12": WizardPage_12,
    "page-13": WizardPage_13,
    "page-14": WizardPage_14,
    "page-15": WizardPage_15,
    "page-16": WizardPage_16,
    "page-17": WizardPage_17,
    "page-18": WizardPage_18,
    "page-19": WizardPage_19,
    "page-20": WizardPage_20,
    "page-21": WizardPage_21,
    "page-22": WizardPage_22,
    "page-23": WizardPage_23,
    "page-24": WizardPage_24
  }
};
</script>
<style scoped>
@import "./assets/styles/RequestWizard.css";
/* #logoImg {
  display: block;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 20px;
  height: 70px;
  width: auto;
}
#bgImg{
  background-repeat: no-repeat;
  background-size: cover;
} */
</style>