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
            color="green"
            value="DriversLicense"
            v-model="selectedCheckBox"
            @change="check('DriversLicense')"

          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="Other Government Photo Id"
            color="green"
            value="OtherId"
            v-model="selectedCheckBox"
            @change="check('OtherId')"
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
  name: "WizardPage_19",
  data() {
    return {
      selectedCheckBox:[],
      menu1: false,
    };
  },
  methods: {
    nextPage() {
        this.$store.commit("requestermodule/mutateidentityId", this.selectedCheckBox[0]);
        this.$store.state.ConfigModule.showBackBtn = true;  
        this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {

        this.selectedCheckBox = [];
        this.selectedCheckBox.push(id);
        console.log(this.selectedCheckBox[0]);
    }
  }
};
</script>

<style scoped>

</style>