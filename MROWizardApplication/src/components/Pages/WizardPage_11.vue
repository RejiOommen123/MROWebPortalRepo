<template>
  <div class="center">
    <h1>Is there any sensitive information<br/>you would also like included?</h1>
    <template>
      <!-- Get all Sensitive Information associated to facility and displayed as checkbox for selection-->
      <v-layout v-for="sensitiveInfo in oSensitiveInfoArray" :key="sensitiveInfo.sNormalizedSensitiveInfoName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            v-model="sSelectedSensitiveInfo"
            class="checkboxBorder"
            :label="sensitiveInfo.sSensitiveInfoName"
            color="white"
            :value="sensitiveInfo.sNormalizedSensitiveInfoName"
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
          <!-- display info only if it exist else no i button -->
            <v-tooltip  v-if="sensitiveInfo.sFieldToolTip" slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px">{{sensitiveInfo.sFieldToolTip}}</p>
                </v-col>
            </v-tooltip>
          </v-checkbox>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_09",
  data() {
    return {
      oSensitiveInfoArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oSensitiveInfo,
      sSelectedSensitiveInfo: []
    };
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sSelectedSensitiveInfo", this.sSelectedSensitiveInfo);

      //Partial Requester Data Save Start
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
      if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
      {
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>