<template>
  <div class="center">
    <h1>Is there any sensitive information<br/>you would also like included?</h1>
    <template>
      <!-- Get all Sensitive Information associated to facility and displayed as checkbox for selection-->
      <v-layout v-for="sensitiveInfo in oSensitiveInfoArray" :key="sensitiveInfo.sNormalizedSensitiveInfoName" row wrap>
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            v-model="sSelectedSensitiveInfo"
            class="checkboxBorder"
            :label="sensitiveInfo.sSensitiveInfoName"
            color="white"
            :value="sensitiveInfo.sNormalizedSensitiveInfoName"
          >
          <!-- This for 'i' button to give disclaimers/info about option -->
          <!-- display info only if it exist else no i button -->
            <v-tooltip  v-if="sensitiveInfo.sFieldToolTip" slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px; background-color:white;color:black">{{sensitiveInfo.sFieldToolTip}}</p>
                </v-col>
            </v-tooltip>
          </v-checkbox>
        </v-col>
      </v-layout>
    </template>
    <!-- <div>
      <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
    </div> -->
    <v-row>
    <v-col cols="6" offset-sm="4" sm="2">
      <v-btn :disabled="sSelectedSensitiveInfo[0]==null" @click.once="nextPage" :key="buttonKey" class="next">Next</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.once="skipPage" class="next" :key="buttonKey">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "WizardPage_12",
  activated(){
    this.buttonKey++;
    if(this.sSelectedStateSensitiveInfo.length == 0){
      this.sSelectedSensitiveInfo = [];
    }
  },
  computed:{
    ...mapState({
      oSensitiveInfoArray : state => state.ConfigModule.apiResponseDataByLocation.oSensitiveInfo,
      sSelectedStateSensitiveInfo : state => state.requestermodule.sSelectedSensitiveInfo,
    }),
  },
  data() {
    return {
      sSelectedSensitiveInfo: [],
       buttonKey:1,
    };
  },
  methods: {
    skipPage(){
      this.sSelectedSensitiveInfo=[];
      this.nextPage();
    },
    nextPage() {
      this.$store.commit("requestermodule/sSelectedSensitiveInfo", this.sSelectedSensitiveInfo);

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');

      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
};
</script>
<style scoped>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>