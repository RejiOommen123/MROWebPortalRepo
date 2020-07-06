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
            dark
            class="checkboxBorder"
            label= "Drivers License"
            color="#e84700"
            value="MRODrivingLicID"
            v-model="nSelectedCheckBox"
            @change="check('MRODrivingLicID')"

          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="Other Government Photo Id"
            color="#e84700"
            value="MROOtherGovID"
            v-model="nSelectedCheckBox"
            @change="check('MROOtherGovID')"
          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="1" sm="10">
          <div class="disclaimer">{{disclaimer}}</div>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_19",
  data() {
    return {
      nSelectedCheckBox:[],
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_19_disclaimer01,
    };
  },
  methods: {
    nextPage() {
      
      this.$store.commit("requestermodule/sIdentityIdName", this.nSelectedCheckBox[0]);
       
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
    check(id) {
        this.nSelectedCheckBox = [];
        this.nSelectedCheckBox.push(id);
    }
  }
};
</script>
