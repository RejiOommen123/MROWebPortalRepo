<template>
  <div class="center">
    <div>
      <h1>Is there any deadline for request?</h1>
    </div>
    <v-row>
       <div style="width:100%">
        <v-col name="Yes" cols="12" offset-sm="2" sm="8">
          <button
            @click.prevent="setDeadlineStatus($event)"
            class="locationButton"
            value="true"
          >Yes, I have a deadline.</button>
        </v-col>
        <v-col name="No" cols="12" offset-sm="2" sm="8">
          <button
            @click.prevent="setDeadlineStatus($event)"
            class="locationButton"
            value="false"
          >No, just as soon as possible.</button>
        </v-col>
      </div>
      <!-- <div class="disclaimer">{{disclaimer}}</div> -->
    </v-row>
  </div>
</template>
<script>
import moment from "moment";
export default {
  name: "WizardPage_15",
  data() {
    return {
      // disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_15_disclaimer01,
      //Show and Hide Fields Values
      // MRORequestDeadline : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MRORequestDeadline,
    };
  },
  methods: {
    setDeadlineStatus($event) {
      this.$store.commit(
        "requestermodule/bDeadlineStatus",
        $event.target.value
      );
      this.$store.commit(
        "requestermodule/dtDeadline",
        moment()
          .add(1, "days")
          .toISOString()
          .substr(0, 10)
      );
      this.$store.commit("ConfigModule/bDeadlineStatus", $event.target.value);
      this.$store.commit("ConfigModule/mutateNextIndex");
      console.log($event.target.value);
    }
  }
};
</script>
<style scoped>
</style>