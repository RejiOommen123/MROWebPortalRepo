<template>
  <div>
    <v-dialog v-model="dialog" light persistent content-class="forceCompliance">
      <v-card color="#006bfa">
        <v-card-title primary-title class="justify-center" style="color:white"><h2><b>Identity Verification</b></h2></v-card-title>

        <v-card-text style="color:white;">
            <h5>In order to comply with  privacy requirements, please verify your identity by one of the following methods:</h5>
          <v-row>
            <div style="width: 100%">
              <v-col cols="12" offset-sm="1" sm="10">
                <button
                  :class="{ active: sActiveBtn === 'email' }"
                  @click="redirectForCompliance('email')"
                  class="wizardSelectionButton"
                >
                  E-Mail
                </button>
              </v-col>
            </div>
            <div style="width: 100%">
              <v-col cols="12" offset-sm="1" sm="10">
                <button
                  :class="{ active: sActiveBtn === 'phone' }"
                  @click="redirectForCompliance('phone')"
                  class="wizardSelectionButton"
                >
                  Phone Number Verification
                </button>
              </v-col>
            </div>
             <div style="width: 100%">
              <v-col cols="12" offset-sm="1" sm="10">
                <button
                  :class="{ active: sActiveBtn === 'photo' }"
                  @click="redirectForCompliance('photo')"
                  class="wizardSelectionButton"
                >
                  Photo ID Capture/Upload
                </button>
              </v-col>
            </div>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "ForceCompliance",
  data: function () {
    return {
        dialog: false,
        sActiveBtn:''
    };
  },
  mounted(){
    this.checkShowCompliance();
  },
  methods: {
    redirectForCompliance(option){
        this.sActiveBtn=option;
        var index;
        var wizard;
        if(option==='email'){
            wizard='Wizard_06';           
        }
        if(option==='phone'){      
            wizard='Wizard_21';      
        }
        if(option==='photo'){
            wizard='Wizard_22';  
        }
        this.$store.commit("ConfigModule/bReturnedForCompliance",true);
        index = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oWizards.indexOf(wizard);
        this.$store.commit("ConfigModule/mutatewizardArrayIndex",index);
        this.$store.commit("ConfigModule/mutateselectedWizard",wizard);
    },
    checkShowCompliance(){
        if(this.facilityForceCompliance && !this.bForceCompliance)
        {
            this.dialog=true;
        }
    } 
  },
  computed:{
    ...mapState({
      facilityForceCompliance: state => state.ConfigModule.bForceCompliance,
      bForceCompliance: state => state.requestermodule.bForceCompliance,
    }),
  }
};
</script>