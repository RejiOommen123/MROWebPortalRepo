<template>
  <div class="center">
    <div>
      <h1>Request your health records.</h1>
    </div>
    <br />
    <div>
      <p class="subHeadings">
        This should only take a few minutes,
        <br />please have your photo ID ready.
      </p>
    </div>
    <br />
    <div class="disclaimer">{{this.disclaimer}}</div>
    <div>
      <v-btn @click.prevent="nextPage" x-large color="#e84700" class="letsGoBtn">Get Started</v-btn>
    </div>
    <div>
      <a
        style="cursor: pointer;"
        href="#"
        id="pdfFormLink"
      >Want to print, fax or mail in your request? Click here for our PDF form.</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_01",
  data() {
    return {
      disclaimer: this.$store.state.ConfigModule.wp01_disclaimer,
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
