<template>
  <div class="center">
    <h1>Thank you for your feedback!</h1>
        <div class="disclaimer">{{disclaimer}}</div>
    <div>
      <v-btn @click.prevent="closeWizard" color="success">Close</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_24",
  data() {
    return {
        disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_24_disclaimer01,
    };
  },
  methods: {
    closeWizard() {
    //   parent.closeIFrame();
    location.reload();
    }
  }
};
</script>

<style scoped>
</style>