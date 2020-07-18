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
              @click.prevent="locationRequest(location)"
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
                @input="$v.sSelectedLocationName.$touch()"
                @blur="$v.sSelectedLocationName.$touch()"
              ></v-text-field>
              <v-btn
                :disabled="$v.sSelectedLocationName.$invalid"
                @click.prevent="next(location)"
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
  data() {
    return {
      locationArray: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.locationDetails,
      dialogLoader: false,
      sActiveBtn: "",
      showOtherLoactionBox: false,
      sSelectedLocationName: ""
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
          this.sSelectedLocationName = "";
          this.$store.commit(
            "requestermodule/sSelectedLocationName",
            location.sLocationName
          );
          this.apiRequestByLocationId(location);
        }
      },
      next(location) {
        this.$store.commit(
          "requestermodule/sSelectedLocationName",
          this.sSelectedLocationName
        );
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
                location.nFacilityLocationID
            )
            .then(response => {
              var apiLocationResponse = response.body;
              if (response.body) {
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
                this.dialogLoader = false;
                this.$store.commit("ConfigModule/mutateNextIndex");
              }
            });
        } else {
          this.dialogLoader = false;
          this.$store.commit("ConfigModule/mutateNextIndex");
        }
      }
    }
  
};
</script>
<style scoped>
</style>