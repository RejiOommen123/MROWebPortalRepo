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
            :class="{active: sActiveBtn === 'true'}" :key="buttonKey"
            @click.once="setDeadlineStatus($event)"
            class="wizardSelectionButton"
            value=true
          >Yes, I have a deadline.</button>
        </v-col>
        <v-col name="No" cols="12" offset-sm="2" sm="8">
          <button
            :class="{active: sActiveBtn === 'false'}" :key="buttonKey"
            @click.once="setDeadlineStatus($event)"
            class="wizardSelectionButton"
            value=false
          >No, just as soon as possible.</button>
        </v-col>
      </div>
    </v-row>
  </div>
</template>
<script>
import { mapState } from 'vuex';
import moment from "moment";
export default {
  name: "WizardPage_18",
   activated(){
    this.buttonKey++;
    },
  computed:{
    ...mapState({
      disclaimer : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_18_disclaimer01,
    }),
  },
  data() {
    return {
      sActiveBtn: this.$store.state.requestermodule.bDeadlineStatus,
      buttonKey:1,
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
      this.$store.dispatch('requestermodule/partialAddReq');

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
};
</script>
