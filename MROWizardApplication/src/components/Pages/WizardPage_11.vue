<template>
  <div class="center">
    <div>
      <h1>
        To whom should we release
        <br />these medical records to?
      </h1>
    </div>
    <v-row>
      <div class="form-group">
        <div v-for="releaseTo in oReleaseToArray" :key="releaseTo.sNormalizedReleaseTo">
          <v-col cols="12" offset-sm="2" sm="10">
            <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
            <button @click.prevent="releaseRequestTo($event)" class="releaseToButton" :value="releaseTo.sNormalizedReleaseTo"
            >{{releaseTo.sReleaseTo}}</button>
          </v-col>
        </div>
      </div>
    </v-row>
  </div>
</template>
<script>
export default {
  name: "WizardPage_11",
  data() {
    return {
      oReleaseToArray: this.$store.state.ConfigModule.oReleaseRequestTo
    };
  },
  methods: {
    releaseRequestTo($event) {
      this.$store.commit("requestermodule/sReleaseTo", $event.target.value);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
</style>