<template>
  <div class="center">
    <h1 v-if="bGivenFeedback">Thank you for your feedback!</h1>
    <h1 v-else>Thank you!</h1>
        <div class="disclaimer">{{disclaimer01}}</div>
        <div class="disclaimer">{{disclaimer02}}</div>
        <div class="disclaimer">{{disclaimer03}}</div>
    <div>
      <v-btn @click.once="closeWizard" :key="buttonKey" class="next">Close</v-btn>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "WizardPage_27",
  activated(){
    this.buttonKey++;
  },
  computed: {
    bGivenFeedback(){
      return this.$store.state.requestermodule.nFeedbackRating > 0 ? true : false;
    },
    ...mapState({
      disclaimer01 : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_27_disclaimer01,
      disclaimer02 : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_27_disclaimer02,
      disclaimer03 : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_27_disclaimer03
    }),
  },
  data() {
    return {
    buttonKey:1,
    };
  },
  methods: {
    closeWizard() {  
      window.top.postMessage("hello", "*");
    }
  },
};
</script>

