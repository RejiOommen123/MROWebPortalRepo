<template>
  <div class="center">
    <h1>
      Your request was received!
    </h1>
    
    <div v-if="disclaimer!=''" class="disclaimer">{{disclaimer}}</div>
  
    <div class="form-group">     
      <v-col cols="12" offset-sm="2" sm="8">
    <button @click.prevent="requestAnotherRecord($event)"  class="wizardSelectionButton" value=true>Request another record</button>
    <button @click.prevent="requestAnotherRecord($event)"  class="wizardSelectionButton" value=false>I'm done</button>
    </v-col>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_22",
  data() {
    return {
       disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_24_disclaimer01
    };
  },
  created(){ 
    this.$vuetify.theme.dark = true
  },
  methods:{
      requestAnotherRecord($event){  
            this.$store.commit("requestermodule/bRequestAnotherRecord",$event.target.value);   
            if($event.target.value==="true")
            {
              location.reload();
            }
            else{    
              this.$store.commit("ConfigModule/mutateNextIndex");          
            }
      }
    },
};
</script>
