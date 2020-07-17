<template>
  <div class="center">
    <div>
      <h1>Which types of records would like to request?</h1>
    </div>
    <v-row>
      <v-col cols="12" offset-sm="2" sm="8">
        <v-checkbox
          hide-details
          dark
          class="checkboxBorder"
          label="I would like to request my medical records abstract."
          color="#e84700"
          value=1
          v-model="option"
          @change="checked(1)"
        >
        <v-tooltip  slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:350px; background-color:transparent">{{disclaimer01}}</p>
                </v-col>
            </v-tooltip>
        </v-checkbox>
      </v-col>
      <v-col cols="12" offset-sm="2" sm="8">
        <v-checkbox
          hide-details
          dark
          class="checkboxBorder"
          label="I would like to manually select which records I need."
          color="#e84700"
          value=2
          v-model="option"
          @change="checked(2)"
        >
        <v-tooltip  slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:350px; background-color:transparent">{{disclaimer02}}</p>
                </v-col>
            </v-tooltip>
        </v-checkbox>
      </v-col>
      <v-col cols="12" offset-sm="5" sm="2">
        <v-btn  class="next" @click="next">Next</v-btn>
      </v-col>
    </v-row>
  </div>
</template>
<script>
export default {
  name: "WizardPage_15",
  data() {
    return {
      bRTManualSelection : false,
      option:1,

      disclaimer01 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_09_disclaimer01,
      disclaimer02 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_09_disclaimer02,
    };
  },
  methods: {
    next() {
      this.$store.commit(
        "ConfigModule/bRTManualSelection",
        this.bRTManualSelection
      );
      this.$store.commit(
        "requestermodule/bRTManualSelection",
        this.bRTManualSelection
      );

      //Partial Requester Data Save Start
      this.$store.commit(
        "requestermodule/sWizardName",
        this.$store.state.ConfigModule.selectedWizard
      );
      if (
        this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
          .wizardsSave[this.$store.state.ConfigModule.selectedWizard] == 1
      ) {
        this.$http
          .post("requesters/AddRequester/", this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    checked(status) {
      this.option = status;

      if(this.option==1){
        this.bRTManualSelection=false;
      }
      else{
        this.bRTManualSelection=true;
      }
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