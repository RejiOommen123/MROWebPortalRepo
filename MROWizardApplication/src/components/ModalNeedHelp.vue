<template>
  <div>
    <v-dialog v-model="dialog" light persistent >
      <v-card >
        <v-card-title style="color:white">
            <h2>Your Details</h2>    
            <h5>Let us know how to get back to you.</h5>
        </v-card-title>
        <v-card-text>
          <v-row>
            
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "NeedHelp",
  data: function () {
    return {
        facilityForceCompliance: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.facilityLogoandBackground[0]
        .bForceCompliance,
        bForceCompliance:this.$store.state.requestermodule.bForceCompliance,
        dialog: false,
        sActiveBtn:''
    };
  },
  mounted(){
    this.checkShowCompliance();
  },
  watch: {
    forceCompliance(newforceCompliance) {
      this.bForceCompliance = newforceCompliance;
      this.checkShowCompliance();
    },
  },
  computed: {
    forceCompliance() {
      return this.$store.state.requestermodule.bForceCompliance;
    },
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
            wizard='Wizard_20';      
        }
        if(option==='photo'){
            wizard='Wizard_21';  
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
};
</script>