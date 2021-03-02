<template>
  <div class="center">
    <div>
      <h1>To whom should we release these medical records to?</h1>
    </div>
    <v-row>
      <div class="form-group">
         <!-- Get all Release to options and displayed as buttons for selection-->
        <div v-for="releaseTo in oReleaseToArray" :key="releaseTo.sNormalizedReleaseTo">
          <v-col cols="12" offset-sm="2" sm="10" style="padding-bottom:0px">
            <button
            :class="{active: sActiveBtn === releaseTo.sNormalizedReleaseTo}" 
            @click.once="releaseRequestTo(releaseTo)" :key="buttonKey" class="wizardSelectionButton"  :value="releaseTo"
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
    activated(){
    this.buttonKey++;
    },
  data() {
    return {
      oReleaseToArray: this.$store.state.ConfigModule.oReleaseRequestTo,
      sActiveBtn:'',
       buttonKey:1,
    };
  },
  methods: {
    releaseRequestTo(releaseTo) {
      this.sActiveBtn=releaseTo.sNormalizedReleaseTo;
      this.$store.commit("requestermodule/sReleaseTo", releaseTo.sNormalizedReleaseTo);
      this.$store.commit("requestermodule/sReleaseToName", releaseTo.sReleaseTo);
      

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
