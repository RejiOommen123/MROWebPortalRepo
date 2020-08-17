<template>
  <div class="center">
    <h1>You’re almost done!</h1>
    <v-row>
    
    <v-col cols="12" offset-sm="1" sm="10">
    <div v-if="disclaimer01!=''" ><h6 style="color:white; padding-top:0">{{disclaimer01}}</h6></div>
    <div v-if="disclaimer02!=''" class="disclaimer">{{disclaimer02}}</div>
    </v-col>
    <v-col cols="12" offset-sm="2" sm="8" v-if="MROPatientAdditionalDetails">             
        <v-textarea    
          class="TextAreaBg"     
          rows="3"
          row-height="30"
          maxlength="250"
          shaped counter v-model="sAdditionalData" label="ADDITIONAL DETAILS"></v-textarea>    
    </v-col>
    </v-row>
    <!-- <div>
      <v-btn @click.prevent="nextPage" style="margin-top:0px; margin-bottom:0px;" class="next">Next</v-btn>
    </div> -->
     <v-row>
    <v-col cols="6" offset-sm="4" sm="2">
      <v-btn :disabled="sAdditionalData==''" @click.prevent="nextPage" style="margin-top:0px; margin-bottom:0px;" class="next">Next</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.prevent="skipPage" style="margin-top:0px; margin-bottom:0px;" class="next">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "WizardPage_10",
  data() {
    return {
      sAdditionalData:'',
      disclaimer01 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_19_disclaimer01,
      disclaimer02 : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_19_disclaimer02,

      //Show and Hide Fields Values
      MROPatientAdditionalDetails : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROPatientAdditionalDetails,
    };
  },
  methods: {
    skipPage(){
      this.sAdditionalData='';
      this.nextPage();
    },
    nextPage() {
      this.$store.commit("requestermodule/sAdditionalData", this.sAdditionalData);

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
