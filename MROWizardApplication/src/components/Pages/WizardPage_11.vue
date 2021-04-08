<template>
  <div class="center">
    <h1>By signing this request for medical records, I understand that:</h1>
    <template>
      <!-- Get all Waiver associated to facility and displayed as checkbox for selection-->
      <v-layout class="checkboxBorder waivers label" v-for="waiver in oWaiverArray" :key="waiver.sNormalizedWaiverName" row wrap>
        <v-col cols="1" sm="1">
          <v-checkbox
            class="vertical_center"
            hide-details
            dark
            v-model="sSelectedWaiver"            
            color="white"
            :value="waiver.sNormalizedWaiverName"
            :id="waiver.sNormalizedWaiverName"
          >                    
          </v-checkbox>
          </v-col>
          <v-col class="align_left" cols="10" sm="10">
            <label :for="waiver.sNormalizedWaiverName" class="vertical_center" v-html="waiver.sWaiverName"></label>
          </v-col>
          <v-col cols="1" sm="1">
           <v-tooltip class="vertical_center"  v-if="waiver.sFieldToolTip" slot="append" left>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p class="tooltip-text">{{waiver.sFieldToolTip}}</p>
                </v-col>
            </v-tooltip>
          </v-col>
      </v-layout>
    </template>
    <v-row>
    <v-col cols="4" offset="4" offset-sm="5" sm="2">
      <v-btn :disabled="!(sSelectedWaiver.length==oWaiverArray.length)" @click.once="nextPage" :key="buttonKey" class="next">Next</v-btn>
    </v-col>
    <!-- <v-col cols="6" sm="2">
      <v-btn @click.once="skipPage" class="next" :key="buttonKey">Skip</v-btn>
    </v-col> -->
    </v-row>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "WizardPage_11",
  activated(){
    this.buttonKey++;
    if(this.sSelectedStateWaiver.length == 0){
      this.sSelectedWaiver = [];
      this.$store.commit("requestermodule/bWaiverAccepted", false);
    }
  },
  computed:{
    ...mapState({
      oWaiverArray : state => state.ConfigModule.apiResponseDataByLocation.oWaivers,
      sSelectedStateWaiver : state => state.requestermodule.sSelectedWaiver,
    }),
  },
  data() {
    return {
      sSelectedWaiver: this.$store.state.requestermodule.sSelectedWaiver,
      buttonKey:1,
    };
  },
  methods: {
    skipPage(){
      this.sSelectedWaiver=[];
      this.nextPage();
    },
    nextPage() {
      this.$store.commit("requestermodule/sSelectedWaiver", this.sSelectedWaiver);
      this.$store.commit("requestermodule/bWaiverAccepted", true);
      
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
.vertical_center{
  margin-top:auto;
  margin-top:bottom;
}
.align_left{
  text-align: left;
}
.tooltip-text{
  width:200px; 
  background-color:white;
  color:black;
}
.waivers{
    padding-top: 0; 
    padding-bottom: 0;
    margin-bottom: 10px;
    line-height: 1.375rem;
}
.label{
  font-size: 16px;
}
</style>