<template>
  <div class="center">
    <h1>
      What's the Primary Reason
      <br />for requesting records?
    </h1>

    <h6>(This is optional, but may help us better fulfill your request)</h6>

    <template>
      <v-layout v-for="primaryReason in oPrimaryReasonArray" :key="primaryReason.sNormalizedPrimaryReasonName" row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label="primaryReason.sPrimaryReasonName"
            color="green"
            :value="primaryReason.sNormalizedPrimaryReasonName"
            v-model="sSelectedPrimaryReasons"
            @change="checkOther(primaryReason.sNormalizedPrimaryReasonName)"
          ></v-checkbox>
        </v-col>
      </v-layout>
      <v-col cols="12" offset-sm="3" sm="6">
      <div v-if="this.bOther==true">
        <v-textarea v-model="sOtherReasons" counter label="Other Reason"></v-textarea>
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
      oPrimaryReasonArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oPrimaryReason,
      bOther: false,
      sSelectedPrimaryReasons: [],
      sOtherReasons: ''
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/sSelectedPrimaryReasons", this.sSelectedPrimaryReasons);
      this.$store.commit("requestermodule/sOtherReasons", this.sOtherReasons);
       this.$store.commit("ConfigModule/mutateNextIndex");
    },
    checkOther(prName) {
      if (prName == "MROOtherPrimaryReason") {
        this.bOther= !this.bOther;
      }
    }
  }
};
</script>

<style scoped>
/* .center {
  text-align: center;
}
.checkboxBorder {
  height: 40px;
  line-height: 40px;
  padding-left: 10px;
  font-size: 14px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: visible;
  border: 1px solid #f6efef;
  border-radius: 8px;
  -moz-border-radius: 8px;
  -webkit-border-radius: 8px;
  margin-top: 10px;
  position: relative;
}
.checkboxBorder:hover {
  background-color: #53b958;
} */
</style>