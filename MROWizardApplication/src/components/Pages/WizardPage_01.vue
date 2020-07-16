<template>
  <div class="center">
    <div>
      <p v-if="disclaimer01!=''" class="subHeadings">
        {{disclaimer01}}
      </p>
    </div>
    <div v-if="disclaimer02!=''" style="margin-bottom:10%; margin-top:10%" class="disclaimer">{{disclaimer02}}</div>
    <div>
      <v-btn style="margin-bottom:10%" @click.prevent="nextPage" x-large color="#e84700" id="letsGoBtn">Get Started</v-btn>
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
      wizard_config: null
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;     
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
  }
};
</script>
