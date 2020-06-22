<template>
  <div class="center">
    <h1>
      How would you like us
      <br />to send you record(s)?
    </h1>

    <template>
      <v-layout v-for="shipmentType in shipmentTypeArray" :key="shipmentType" row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label="shipmentType"
            color="green"
            :value="shipmentType"
            @change="check(shipmentType)"
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
      shipmentTypeArray: this.$store.state.ConfigModule.wp12_ShipmentType,
      other: false
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;  
       this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(prName) {
      if (prName == "Other Reason") {
        this.other= !this.other;       
      }
    }
  }
};
</script>

<style scoped>

</style>