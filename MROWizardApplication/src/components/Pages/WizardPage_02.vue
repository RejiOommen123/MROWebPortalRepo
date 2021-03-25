<template>
  <div class="center">
    <div>
      <h1>
        Great! Which location
        <br />are you requesting records from?
      </h1>
    </div>
    <v-row>
      <!-- Get all location associated to facility and displayed as button for selection-->
      <div class="form-group" style="width:100%">
        <div
          v-for="location in locationArray"
          :key="location.sNormalizedLocationName"
          style="width:100%"
        >
          <v-col cols="12" offset-sm="2" sm="8">
            <button
              :class="{active: sActiveBtn === location.sNormalizedLocationName}"
              @click.once="locationRequest(location)" :key="buttonKey"
              class="wizardSelectionButton"
              :value="location.sNormalizedLocationName"
            >{{location.sLocationName}}</button>
          </v-col>
          <div v-if="location.sNormalizedLocationName=='MROLocationOther'">
            <v-col v-if="showOtherLoactionBox" offset="3" cols="6" offset-sm="3" sm="6">
              <v-text-field
                type="text"
                v-model="sSelectedLocationName"
                :error-messages="sSelectedLocationNameErrors"
                label="OTHER LOCATION NAME"
                required
                maxlength="50"
                @input="$v.sSelectedLocationName.$touch()"
                @blur="$v.sSelectedLocationName.$touch()"
              ></v-text-field>
              <v-btn
                :disabled="$v.sSelectedLocationName.$invalid"
                @click.once="next(location)" :key="buttonKey"
                class="next"
              >Next</v-btn>
            </v-col>
          </div>
        </div>
      </div>
    </v-row>
    <!-- Loader dialog -->
    <v-dialog v-model="dialogLoader" persistent width="300">
      <v-card color="primary" dark>
        <v-card-text>
          Please stand by
          <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_02",
   activated(){
    this.buttonKey++;
   },
   data() {
    return {
      locationArray: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.locationDetails,
      dialogLoader: false,
      sActiveBtn: "",
      showOtherLoactionBox: false,
      sSelectedLocationName: "",
       buttonKey:1,
    };
  },
  mixins: [validationMixin],
  validations: {
    sSelectedLocationName: {
      required
    },
  },
    computed: {
      //validations error message setter
      sSelectedLocationNameErrors() {
        const errors = [];
        if (!this.$v.sSelectedLocationName.$dirty) return errors;
        !this.$v.sSelectedLocationName.required &&
          errors.push("Other Location Name is required.");
        return errors;
      }
    },
    methods: {
      locationRequest(location) {
        // Using sActiveBtn variable to set background color to show button selection
        this.sActiveBtn = location.sNormalizedLocationName;

        //Getting back from next slide and then again selecting location. if selecting same location  then it
        //will not fetch wizard data based on location id as it already fetch previously
        if (location.sNormalizedLocationName == "MROLocationOther") {
          this.showOtherLoactionBox = true;
        } else {
          this.showOtherLoactionBox = false;
          this.$store.commit(
            "requestermodule/sSelectedLocationName",
            location.sLocationName
          );
          this.apiRequestByLocationId(location);
        }
      },
      next(location) {
        if (location.sNormalizedLocationName == "MROLocationOther") {
          this.$store.commit(
            "requestermodule/sSelectedLocationName",location.sLocationName+'-'+this.sSelectedLocationName
          );
        }
        else{
          this.$store.commit(
            "requestermodule/sSelectedLocationName",location.sLocationName
          );
        }
        this.apiRequestByLocationId(location);
      },
      apiRequestByLocationId(location) {
        this.dialogLoader = true;
        if (
          location.sNormalizedLocationName !=
          this.$store.state.requestermodule.sSelectedLocation
        ) {
          this.$http
            .get(
              "Wizards/GetWizardConfig/fID=" +
                location.nFacilityID +
                "&lID=" +
                location.nFacilityLocationID+
                "&rID=" +
                this.$store.state.requestermodule.nRequesterID +
                "&sLocationGUID=" +
                this.$store.state.ConfigModule.sLocationGUID
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
                  location.nFacilityID
                );
                this.$store.commit(
                  "requestermodule/nLocationID",
                  location.nFacilityLocationID
                );
                this.$store.commit(
                  "requestermodule/sSelectedLocation",
                  location.sNormalizedLocationName
                );
                this.$store.commit(
                  "ConfigModule/nAuthExpirationMonths",
                  location.nAuthExpirationMonths
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
                this.resetStateOnLocationChange();
                this.dialogLoader = false;
                
                //Partial Requester Data Save Start
                this.$store.dispatch('requestermodule/partialAddReq');
                this.$store.commit("ConfigModule/mutateNextIndex");
              }
            });
        } else {
          this.dialogLoader = false;
          //Partial Requester Data Save Start
          this.$store.dispatch('requestermodule/partialAddReq');
          this.$store.commit("ConfigModule/mutateNextIndex");
        }
      },
      resetStateOnLocationChange(){
        //Reset patient representative
        this.$store.commit("requestermodule/sSelectedRelationName", "");
        this.$store.commit("requestermodule/sSelectedRelation", "");

        //Reset record type
        this.$store.commit("requestermodule/sSelectedRecordTypes", []);
        this.$store.commit("requestermodule/sOtherRTText", "");

        //Reset sensitive info
        this.$store.commit("requestermodule/sSelectedSensitiveInfo", []);
        
        //Reset waivers
        this.$store.commit("requestermodule/sSelectedWaiver", []);
        this.$store.commit("requestermodule/bWaiverAccepted", false);

        //Reset primary reason
        this.$store.commit("requestermodule/sSelectedPrimaryReasons",[]);
        this.$store.commit("requestermodule/sSelectedPrimaryReasonsName", '');

        //Reset shipment type
        this.$store.commit("requestermodule/sSelectedShipmentTypes", []);       
        this.$store.commit("requestermodule/sSelectedShipmentTypesName", '');     
      }
    }
  
};
</script>
<style scoped>
</style>