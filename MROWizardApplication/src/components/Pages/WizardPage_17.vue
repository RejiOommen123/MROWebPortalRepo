<template>
  <div class="center">
    <div>
      <h1>Is there a deadline for this request?</h1>
    </div>
    <p v-if="disclaimer!=''" class="disclaimer">{{disclaimer}}</p>
    <v-row>
       <div style="width:100%">
        <v-col name="Yes" cols="12" offset-sm="2" sm="8">
          <button
            :class="{active: sActiveBtn === 'true'}"
            @click.prevent="setDeadlineStatus($event)"
            class="wizardSelectionButton"
            value=true
          >Yes, I have a deadline.</button>
        </v-col>
        <v-col name="No" cols="12" offset-sm="2" sm="8">
          <button
            :class="{active: sActiveBtn === 'false'}"
            @click.prevent="setDeadlineStatus($event)"
            class="wizardSelectionButton"
            value=false
          >No, just as soon as possible.</button>
        </v-col>
      </div>
    </v-row>
  </div>
</template>
<script>
import moment from "moment";
export default {
  name: "WizardPage_15",
  data() {
    return {
      sActiveBtn:'',
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_17_disclaimer01
    };
  },
  methods: {
    setDeadlineStatus($event) {
      this.sActiveBtn= $event.target.value;
      this.$store.commit(
        "requestermodule/bDeadlineStatus",
        $event.target.value
      );
      // set deadline to tomorrow date
      this.$store.commit(
        "requestermodule/dtDeadline",
        moment()
          .add(30, "days")
          .toISOString()
          .substr(0, 10)
      );
      this.$store.commit("ConfigModule/bDeadlineStatus", $event.target.value);

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
