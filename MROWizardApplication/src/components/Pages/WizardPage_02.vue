<template>
  <div class="center">
    <div>
      <h1>
        Great! Which location
        <br />are you requesting records from?
      </h1>
    </div>
    <div class="form-group btn-group-vertical">
      <div v-for="location in locationArray" :key="location">
        <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
        <button
          @click.prevent="locationRequest(location)"
          class="btn btn-success btn-lg locationButton"
          :value="location.sNormalizedLocationName"
        >{{location.sLocationName}}</button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "WizardPage_02",
  data() {
    return {
      locationArray: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.locationDetails
    };
  },
  methods: {
    locationRequest(location) {
      // this.$store.commit("ConfigModule/mutatepageNumerical",3);
      // this.$store.commit("ConfigModule/mutateCurrentPage","page-3");
      this.$http
        .get(
          "http://localhost:57364/api/Wizards/GetWizardConfig/fID=" +
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
              "requestermodule/sSelectedLocation",
              location.sNormalizedLocationName
            );
            this.$store.commit("ConfigModule/mutateNextIndex");
          }
        });
    }
  }
};
</script>
<style scoped>
/* .center {
  text-align: center;
}
.locationButton{
  height: 87px;
  margin: 10px;  
  width: 250px;
} */
</style>