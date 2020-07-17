<template>
  <div class="center">
    <h1>What is the primary reason for requesting records?</h1>
    <!-- TODO:Check for subheading -->
    <h6 v-if="disclaimer!=''">{{disclaimer}}</h6>

    <template>
      <!-- Get all Primary Reasons associated to facility and displayed as checkbox for selection-->
      <v-layout
        v-for="primaryReason in oPrimaryReasonArray"
        :key="primaryReason.sNormalizedPrimaryReasonName"
        row
        wrap
      >
        <v-col cols="12" offset-sm="1" sm="10">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            :label="primaryReason.sPrimaryReasonName"
            color="#e84700"
            :value="primaryReason.sNormalizedPrimaryReasonName"
            v-model="sSelectedPrimaryReasons"
            @change="check(primaryReason)"
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
            <v-tooltip v-if="primaryReason.sFieldToolTip" slot="append" top>
              <template v-slot:activator="{ on }">
                <v-icon v-on="on" color="white" top>mdi-information</v-icon>
              </template>
              <v-col cols="12" sm="12">
                <p style="width:200px">{{primaryReason.sFieldToolTip}}</p>
              </v-col>
            </v-tooltip>
          </v-checkbox>
        </v-col>
      </v-layout>
      <!-- If requester selects other reason then free text box will appear to enter data -->
      <v-col cols="12" offset-sm="3" sm="6">
        <div v-if="this.bOther==true">
          <v-textarea v-model="sOtherPrimaryReasons" rows="3" counter label="Other Reason"></v-textarea>
        </div>
      </v-col>
    </template>
    <v-row>
    <v-col cols="6" offset-sm="4" sm="2">
      <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.prevent="skipPage" class="next">Skip</v-btn>
    </v-col>
    </v-row>
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
      sOtherPrimaryReasons: '',
      sSelectedPrimaryReasonsName:'',

      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_12_disclaimer01
    };
  },
  methods: {
    nextPage() {
      this.$store.commit(
        "requestermodule/sSelectedPrimaryReasons",
        this.sSelectedPrimaryReasons
      );
        if (this.sSelectedPrimaryReasons == "MROOtherPrimaryReason") {
          this.sSelectedPrimaryReasonsName=this.sOtherPrimaryReasons;
        }
      this.$store.commit("requestermodule/sSelectedPrimaryReasonsName", this.sSelectedPrimaryReasonsName);

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
    },
    skipPage(){
      this.sSelectedPrimaryReasons = [];
      this.bOther=false;
      this.$store.commit("requestermodule/sSelectedPrimaryReasons",[]);
      this.$store.commit("requestermodule/sSelectedPrimaryReasonsName", '');
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    // to check if selected checkbox is other reason
    check(primaryReason) {
        this.sSelectedPrimaryReasons = [];
        this.sSelectedPrimaryReasons.push(primaryReason.sNormalizedPrimaryReasonName);
        this.sSelectedPrimaryReasonsName=primaryReason.sPrimaryReasonName;
        if (this.sSelectedPrimaryReasons == "MROOtherPrimaryReason") {
          this.bOther = true;
          this.sSelectedPrimaryReasonsName=this.sOtherPrimaryReasons;
        }
        else{
          this.bOther=false;
          this.sOtherPrimaryReasons='';
        }
          
    }
  }
};
</script>
<style scoped>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>