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
            @click.prevent="releaseRequestTo($event)" class="wizardSelectionButton"  :value="releaseTo.sNormalizedReleaseTo"
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
      oReleaseToArray: this.$store.state.ConfigModule.oReleaseRequestTo,
      sActiveBtn:''
    };
  },
  methods: {
    releaseRequestTo($event) {
      this.sActiveBtn=$event.target.value;
      this.$store.commit("requestermodule/sReleaseTo", $event.target.value);

      //Partial Requester Data Save Start
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
      if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
      {
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
