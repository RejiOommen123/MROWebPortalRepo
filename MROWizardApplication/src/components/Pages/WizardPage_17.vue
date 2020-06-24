<template>
  <div class="center">
    <h1>You’re almost done!</h1>
    
    <div class="disclaimer">{{disclaimer}}</div>
    <div v-if="MROPatientAdditionalDetails">             
        <v-textarea filled          
          rows="4"
          row-height="30"
          shaped counter v-model="sAdditionalData" label="Additional Details"></v-textarea>    
    </div>
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

<style scoped>

</style>