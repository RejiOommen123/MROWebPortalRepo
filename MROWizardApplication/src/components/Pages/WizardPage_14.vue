<template>
  <div class="center">
    <h1>Is there any sensitive info you<br/>would also like to be included?</h1>
    <template>
      <v-layout v-for="sensitiveInfo in oSensitiveInfoArray" :key="sensitiveInfo.sNormalizedSensitiveInfoName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            dark
            v-model="selectedSensitiveInfo"
            class="checkboxBorder"
            :label="sensitiveInfo.sSensitiveInfoName"
            color="green"
            :value="sensitiveInfo.sNormalizedSensitiveInfoName"
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
  name: "WizardPage_14",
  data() {
    return {
      //this.$store.state.ConfigModule.apiResponseDataByLocation.oPrimaryReason
      oSensitiveInfoArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oSensitiveInfo,
      selectedSensitiveInfo: []
    };
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
          this.$store.commit("requestermodule/selectedSensitiveInfo", this.selectedSensitiveInfo);
          this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>

<style scoped>

</style>