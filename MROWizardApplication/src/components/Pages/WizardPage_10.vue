<template>
  <div class="center">
    <h1>
      What's the Primary Reason
      <br />for requesting records?
    </h1>

    <h6>(This is optional, but may help us better fulfill your request)</h6>

    <template>
      <!-- Get all Primary Reasons associated to facility and displayed as checkbox for selection-->
      <v-layout
        v-for="primaryReason in oPrimaryReasonArray"
        :key="primaryReason.sNormalizedPrimaryReasonName"
        row
        wrap
      >
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label="primaryReason.sPrimaryReasonName"
            color="green"
            :value="primaryReason.sNormalizedPrimaryReasonName"
            v-model="sSelectedPrimaryReasons"
            @change="checkOther(primaryReason.sNormalizedPrimaryReasonName)"
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
            <v-tooltip v-if="primaryReason.sFieldToolTip" slot="append" top>
              <template v-slot:activator="{ on }">
                <v-icon v-on="on" color="grey" top>mdi-information</v-icon>
              </template>
              <v-col cols="12" sm="12">
                <p style="width:200px">{{primaryReason.sFieldToolTip}}</p>
              </v-col>
            </v-tooltip>
          </v-checkbox>
        </v-col>
      </v-layout>
      <!-- If requestor selects other reason then free text box will appear to enter data -->
      <v-col cols="12" offset-sm="3" sm="6">
        <div v-if="this.bOther==true">
          <v-textarea v-model="sOtherReasons" rows="3" counter label="Other Reason"></v-textarea>
        </div>
      </v-col>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_08",
  data() {
    return {
      oPrimaryReasonArray: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oPrimaryReason,
      bOther: false,
      sSelectedPrimaryReasons: [],
      sOtherReasons: ""
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit(
        "requestermodule/sSelectedPrimaryReasons",
        this.sSelectedPrimaryReasons
      );
      this.$store.commit("requestermodule/sOtherReasons", this.sOtherReasons);
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    // to check if selected checkbox is other reason
    checkOther(prName) {
      if (prName == "MROOtherPrimaryReason") {
        this.bOther = !this.bOther;
      }
    }
  }
};
</script>
