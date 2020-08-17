<template>
  <div class="center">
    <h1>Thank you for your feedback!</h1>
        <div class="disclaimer">{{disclaimer01}}</div>
        <div class="disclaimer">{{disclaimer02}}</div>
        <div class="disclaimer">{{disclaimer03}}</div>
    <div>
      <v-btn @click.prevent="closeWizard" class="next">Close</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_24",
  data() {
    return {
        disclaimer01 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_26_disclaimer01,
        disclaimer02 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_26_disclaimer02,
        disclaimer03 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_26_disclaimer03
    };
  },
  methods: {
    closeWizard() {  
      window.top.postMessage("hello", "*");
    }
  }
};
</script>

