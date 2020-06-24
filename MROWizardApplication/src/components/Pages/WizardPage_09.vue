<template>
  <div class="center">
    <h1>Which types of<br/>records would you like?</h1>
    
    <template>
      <v-layout v-for="recordType in RecordTypeArray" :key="recordType.sNormalizedRecordTypeName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            dark
            v-model="sSelectedRecordTypes"
            class="checkboxBorder"
            :label="recordType.sRecordTypeName"
            color="green"
            :value="recordType.sNormalizedRecordTypeName"
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
  name: "WizardPage_10",
  data() {
    return {
      RecordTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oRecordTypes,
      sSelectedRecordTypes: []
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/sSelectedRecordTypes", this.sSelectedRecordTypes);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>

<style scoped>

</style>