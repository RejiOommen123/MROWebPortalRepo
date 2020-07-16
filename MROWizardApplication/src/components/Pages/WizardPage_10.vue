<template>
  <div class="center">
    <h1>Which types of records would like to request?</h1>
    
    <template>
       <!-- Get all record types associated to facility and displayed as checkbox for selection-->
      <v-layout v-for="recordType in RecordTypeArray" :key="recordType.sNormalizedRecordTypeName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox offset-sm="2" sm="8"
            hide-details
            dark
            v-model="sSelectedRecordTypes"
            class="checkboxBorder"
            :label="recordType.sRecordTypeName"
            color="#e84700"
            :value="recordType.sNormalizedRecordTypeName"
            wrap
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
            <v-tooltip  v-if="recordType.sFieldToolTip" slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px; background-color:white;color:black">{{recordType.sFieldToolTip}}</p>
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
  name: "WizardPage_10",
  data() {
    return {
      RecordTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oRecordTypes,
      sSelectedRecordTypes: []
    };
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sSelectedRecordTypes", this.sSelectedRecordTypes);

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
  background: white;
}
</style>