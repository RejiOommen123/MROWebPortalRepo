<template>
  <div class="center">
    <h1>Which types of<br/>records would you like?</h1>
    
    <template>
      <v-layout v-for="recordType in RecordTypeArray" :key="recordType" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            dark
            v-model="selectedRecordTypes"
            class="checkboxBorder"
            :label="recordType"
            color="green"
            :value="recordType"
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
      RecordTypeArray: this.$store.state.ConfigModule.wp10_RecordTypes,
      selectedRecordTypes: []
    };
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
      // this.$store.commit("requestermodule/mutatebDay", this.bDay);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 11);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-11");
       this.$store.commit("ConfigModule/mutateNextIndex");
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
v-checkbox :checked{
    background-color: #53b958;
} */
</style>