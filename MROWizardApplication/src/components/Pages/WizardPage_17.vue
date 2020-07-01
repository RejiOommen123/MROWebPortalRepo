<template>
  <div class="center">
    <h1>You’re almost done!</h1>
    <v-row>
    <div class="disclaimer">{{disclaimer}}</div>
    <v-col cols="12" offset-sm="2" sm="8" v-if="MROPatientAdditionalDetails">             
        <v-textarea filled          
          rows="4"
          row-height="30"
          shaped counter v-model="sAdditionalData" label="Additional Details"></v-textarea>    
    </v-col>
    </v-row>
    <div>
      <v-btn @click.prevent="nextPage"  color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_10",
  data() {
    return {
      sAdditionalData:'',
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_17_disclaimer01,

      //Show and Hide Fields Values
      MROPatientAdditionalDetails : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROPatientAdditionalDetails,
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/sAdditionalData", this.sAdditionalData);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
