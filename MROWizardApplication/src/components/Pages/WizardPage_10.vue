<template>
  <div class="center">
    <h1>Which types of records would like to request?</h1>
    
    <template>
       <!-- Get all record types associated to facility and displayed as checkbox for selection-->
      <v-layout v-for="recordType in RecordTypeArray" :key="recordType.sNormalizedRecordTypeName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox offset-sm="2" sm="8"
            hide-details
            dark
            v-model="sSelectedRecordTypes"
            class="checkboxBorder"
            :label="recordType.sRecordTypeName"
            color="white"
            :value="recordType.sNormalizedRecordTypeName"
            @change="checkOther()"
            wrap
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
            <v-tooltip  v-if="recordType.sFieldToolTip" slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px; background-color:white;color:black">{{recordType.sFieldToolTip}}</p>
                </v-col>
            </v-tooltip>
          </v-checkbox>

           <!-- TODO - Add normalized if -->
           <!-- Other Record Type-->
            <div v-if="recordType.sNormalizedRecordTypeName=='MROOtherRT'">
              <v-col
                v-if="otherExist"
                cols="12"
                sm="12"
              >
              <v-textarea class="TextAreaBg"  v-model="sOtherRTText" maxlength="100" rows="3" counter label="Enter Other Record Type"></v-textarea>
              </v-col>
            </div>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" :disabled="otherExist==true && sOtherRTText==''" class="next">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_10",
  data() {
    return {
      RecordTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oRecordTypes,
      sSelectedRecordTypes: [],
      sOtherRTText:'',
      otherExist:false
    };
  },
  methods: {
    checkOther(){
        if (this.sSelectedRecordTypes.includes("MROOtherRT")) {
         this.otherExist=true;
        }
        else{
          this.otherExist=false;
          this.sOtherRTText=''
        }
    },
    nextPage() {
      this.$store.commit("requestermodule/sSelectedRecordTypes", this.sSelectedRecordTypes);
      this.$store.commit("requestermodule/sOtherRTText", this.sOtherRTText);

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>