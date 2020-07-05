<template>
  <div class="center">
    <!-- <div>
      <h1>It only takes a few minutes to request your health records. Please have a copy of your Government or Photo ID ready.</h1>
    </div> -->
    <div>
      <p class="subHeadings">
        {{disclaimer01}}
      </p>
    </div>
    <div class="disclaimer">{{this.disclaimer02}}</div>
    <div>
      <v-btn @click.prevent="nextPage" x-large color="#e84700" id="letsGoBtn">Get Started</v-btn>
    </div>
    <div>
      <a
        href="#"
        id="pdfFormLink"
      >{{disclaimer03}}</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_01",
  data() {
    return {
      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer02,
      disclaimer03: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer03,
        //TODO: Correct all posiotions of disclaimers
      wizard_config: null
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
     
      this.$store.commit("ConfigModule/mutateNextIndex");

    }
  }
};
</script>
