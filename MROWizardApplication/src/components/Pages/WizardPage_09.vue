<template>
  <div class="center">
    <div>
      <h1>Which types of records would like to request?</h1>
    </div>
    <v-row>
      <!-- TODO : v-if="MROAuthExpireDateAfterNMonths" -->
      <v-col cols="12" offset-sm="2" sm="8">
        <v-checkbox
          hide-details
          dark
          class="checkboxBorder"
          label="I would like to request my medical records abstract."
          color="#e84700"
          value=1
          v-model="option"
          @change="checked(1)"
        >
        <v-tooltip  slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:350px; background-color:transparent">An abstract contains key documents about a visit to your healthcare provider. Choose this option if you need records to take to another healthcare facility/doctor or if needed for your own personal files. An abstract will include: ID (face) sheet, Discharge summary, History & Physical, Consultation reports, Operative reports, Pathology reports, Radiology reports, EKG/ECG reports, CT scan reports, Cardia related reports, and Lab summary.</p>
                </v-col>
            </v-tooltip>
        </v-checkbox>
      </v-col>
      <v-col cols="12" offset-sm="2" sm="8">
        <v-checkbox
          hide-details
          dark
          class="checkboxBorder"
          label="I would like to manually select which records I need."
          color="#e84700"
          value=2
          v-model="option"
          @change="checked(2)"
        >
        <v-tooltip  slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:350px; background-color:transparent">If you are looking for specific document types (not included in the Abstract above), you can choose the ‘manual option' to allow you to choose the record type(s) you need.</p>
                </v-col>
            </v-tooltip>
        </v-checkbox>
      </v-col>
      <v-col cols="12" offset-sm="5" sm="2">
        <v-btn class="next" @click="next">Next</v-btn>
      </v-col>
    </v-row>
  </div>
</template>
<script>
export default {
  name: "WizardPage_15",
  data() {
    return {
      bRTManualSelection : false,
      option:1
    };
  },
  methods: {
    next() {
      this.$store.commit(
        "ConfigModule/bRTManualSelection",
        this.bRTManualSelection
      );
      this.$store.commit(
        "requestermodule/bRTManualSelection",
        this.bRTManualSelection
      );

      //Partial Requester Data Save Start
      this.$store.commit(
        "requestermodule/sWizardName",
        this.$store.state.ConfigModule.selectedWizard
      );
      if (
        this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
          .wizardsSave[this.$store.state.ConfigModule.selectedWizard] == 1
      ) {
        this.$http
          .post("requesters/AddRequester/", this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    checked(status) {
      this.option = status;

      if(this.option==1){
        this.bRTManualSelection=false;
      }
      else{
        this.bRTManualSelection=true;
      }
    }
  }
};
</script>
