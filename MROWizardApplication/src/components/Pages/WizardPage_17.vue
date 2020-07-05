<template>
  <div class="center">
    <h1>You’re almost done!</h1>
    <v-row>
    <div class="disclaimer">{{disclaimer01}}</div>
    <v-col cols="12" offset-sm="2" sm="8">
    <div class="disclaimer">{{disclaimer02}}</div>
    </v-col>
    <v-col cols="12" offset-sm="2" sm="8" v-if="MROPatientAdditionalDetails">             
        <v-textarea filled          
          rows="4"
          row-height="30"
          shaped counter v-model="sAdditionalData" label="Additional Details"></v-textarea>    
    </v-col>
    </v-row>
    <div>
      <v-btn @click.prevent="nextPage"  class="next">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_10",
  data() {
    return {
      sAdditionalData:'',
      disclaimer01 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_17_disclaimer01,
      disclaimer02 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_17_disclaimer02,

      //Show and Hide Fields Values
      MROPatientAdditionalDetails : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROPatientAdditionalDetails,
    };
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sAdditionalData", this.sAdditionalData);

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
