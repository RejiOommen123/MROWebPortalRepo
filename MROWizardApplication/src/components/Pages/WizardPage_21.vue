<template>
  <div class="center">
    <h1>
      How would you like to
      <br />verify your identity?
    </h1>

    <template>
      <v-layout row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            label= "Drivers License"
            color="white"
            value="MRODLIdentity"
            v-model="nSelectedCheckBox"
            @change="check('MRODLIdentity')"

          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            label="Other Government Photo Id"
            color="white"
            value="MROOtherGovIdentity"
            v-model="nSelectedCheckBox"
            @change="check('MROOtherGovIdentity')"
          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="1" sm="10">
          <div class="disclaimer">{{disclaimer}}</div>
        </v-col>
      </v-layout>
    </template>
    <!-- <div>
      <v-btn :disabled="nSelectedCheckBox==''" @click.prevent="nextPage" class="next">Next</v-btn>
    </div> -->
    <v-row>
    <v-col cols="6" offset-sm="4" sm="2">
      <v-btn :disabled="nSelectedCheckBox==''" @click.prevent="nextPage" class="next">Next</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn  @click.prevent="skipPage" class="next">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "WizardPage_19",
  data() {
    return {
      nSelectedCheckBox:[],
      facilityForceCompliance: this.$store.state.ConfigModule
        .bForceCompliance,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_21_disclaimer01,
    };
  },
  methods: {
    skipPage(){
      this.nSelectedCheckBox = [];
      this.$store.commit("requestermodule/sIdentityIdName", '');   
      this.$store.commit("ConfigModule/bIdentitySkiped", true);
      if(!this.$store.state.requestermodule.bPhoneNoVerified && !this.$store.state.requestermodule.bEmailVerified){
        if(this.facilityForceCompliance)
        {
          this.$store.commit("requestermodule/bForceCompliance", false);
        }
      }
      this.continue();
    },
    nextPage() {
      this.$store.commit("ConfigModule/bIdentitySkiped", false);
      this.$store.commit("requestermodule/sIdentityIdName", this.nSelectedCheckBox[0]);    
      this.continue();
    },
    continue(){
       //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');

        this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {
        this.nSelectedCheckBox = [];
        this.nSelectedCheckBox.push(id);
    }
  }
};
</script>
