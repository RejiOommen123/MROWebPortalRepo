<template>
  <div class="center">
    <div>
      <h1>Is there any deadline for request?</h1>
    </div>
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
      sActiveBtn:''
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
          .add(1, "days")
          .toISOString()
          .substr(0, 10)
      );
      this.$store.commit("ConfigModule/bDeadlineStatus", $event.target.value);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
