<template>
  <div class="center">
    <div>
      <h1>
        Great! Which location
        <br />are you requesting records from?
      </h1>
    </div>
    <v-row>
      <div class="form-group" style="width:100%">
        <div v-for="location in locationArray" :key="location.sNormalizedLocationName" style="width:100%">
          <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
          <v-col cols="12" offset-sm="2" sm="8">
            <button
              @click.prevent="locationRequest(location)"
              class="locationButton"
              :value="location.sNormalizedLocationName"
            >{{location.sLocationName}}</button>
          </v-col>
        </div>
      </div>
    </v-row>
    <!-- Loader dialog -->
    <v-dialog v-model="dialogLoader" hide-overlay persistent width="300">
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
export default {
  name: "WizardPage_02",
  data() {
    return {
      locationArray: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.locationDetails,
        dialogLoader:false
    };
  },
  methods: {
    locationRequest(location) {
      this.dialogLoader=true;
      // this.$store.commit("ConfigModule/mutatepageNumerical",3);
      // this.$store.commit("ConfigModule/mutateCurrentPage","page-3");
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
            console.log(
              "apiResponseDataByLocation    " +
                this.$store.state.ConfigModule.apiResponseDataByLocation
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
            this.dialogLoader=false;
            this.$store.commit("ConfigModule/mutateNextIndex");
          }
        });
    }
  }
};
</script>
<style scoped>
</style>