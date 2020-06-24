<template>
  <div class="center">
    <h1>
      How would you like us
      <br />to send you record(s)?
    </h1>

    <template>
      <v-layout v-for="shipmentType in oShipmentTypeArray" :key="shipmentType.sNormalizedShipmentTypeName" row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label="shipmentType.sShipmentTypeName"
            color="green"
            :value="shipmentType.sNormalizedShipmentTypeName"
            v-model="sSelectedShipmentTypes"
          ></v-checkbox>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_12",
  data() {
    return {
      //this.$store.state.ConfigModule.apiResponseDataByLocation.oPrimaryReason
      oShipmentTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oShipmentTypes,
      sSelectedShipmentTypes: []
      //other: false
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;  
      this.$store.commit("requestermodule/sSelectedShipmentTypes", this.sSelectedShipmentTypes);
       this.$store.commit("ConfigModule/mutateNextIndex");
    }
    // ,
    // check(prName) {
    //   if (prName == "Other Reason") {
    //     this.other= !this.other;       
    //   }
    // }
  }
};
</script>

<style scoped>

</style>