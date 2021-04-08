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
          counter v-model="sAdditionalData" label="ADDITIONAL DETAILS"></v-textarea>    
    </v-col>
    </v-row>
    <!-- <div>
      <v-btn @click.prevent="nextPage" style="margin-top:0px; margin-bottom:0px;" class="next">Next</v-btn>
    </div> -->
     <v-row>
    <v-col cols="6" offset-sm="4" sm="2">
      <v-btn :disabled="sAdditionalData==''" @click.once="nextPage" :key="buttonKey" style="margin-top:0px; margin-bottom:0px;" class="next">Next</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.once="skipPage" :key="buttonKey" style="margin-top:0px; margin-bottom:0px;" class="next">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "WizardPage_20",
  activated(){
   this.buttonKey++;
  },
  computed:{
    ...mapState({
      disclaimer01 : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_20_disclaimer01,
      disclaimer02 : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_20_disclaimer02,
      //Show and Hide Fields Values
      MROPatientAdditionalDetails : state => state.ConfigModule.apiResponseDataByLocation.oFields.MROPatientAdditionalDetails,
    }),
  },
  data() {
    return {
      sAdditionalData: this.$store.state.requestermodule.sAdditionalData,
      buttonKey:1,
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
      this.$store.dispatch('requestermodule/partialAddReq');

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
};
</script>
