<template>
  <div class="center">
    <h1>Is there any sensitive info you<br/>would also like to be included?</h1>
    <template>
      <v-layout v-for="sensitiveInfo in sensitiveInfoArray" :key="sensitiveInfo" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            dark
            v-model="selectedSensitiveInfo"
            class="checkboxBorder"
            :label="sensitiveInfo"
            color="green"
            :value="sensitiveInfo"
          ></v-checkbox>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" color="success">Submit</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_11",
  data() {
    return {
      sensitiveInfoArray: this.$store.state.ConfigModule.wp10_SensitiveInfo,
      selectedSensitiveInfo: []
    };
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
      // this.$store.commit("requestermodule/mutatebDay", this.bDay);
      this.$store.commit("ConfigModule/mutatepageNumerical", 12);
      this.$store.commit("ConfigModule/mutateCurrentPage", "page-12");
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
}
.checkboxBorder:checked{
    background-color: #53b958;
} */
</style>