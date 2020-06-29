<template>
  <div class="center">
    <h1>
      Your request was received!
    </h1>
    
    <div class="disclaimer">{{disclaimer}}</div>
  
    <div class="form-group btn-group-vertical">     
    <button @click.prevent="requestAnotherRecord(true)"  class="btn btn-success btn-lg locationButton" value=true>Request another record</button>
    <button @click.prevent="requestAnotherRecord(false)"  class="btn btn-success btn-lg locationButton" value=false>I'm done</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_22",
  data() {
    return {
       disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_22_disclaimer01
    };
  },
  created(){ 
    this.$vuetify.theme.dark = true
  },
  methods:{
      requestAnotherRecord(status){  
            this.$store.commit("requestermodule/bRequestAnotherRecord",status);   
            if(status==true);
            {
              location.reload();
            }
            console.log(status);                
            this.$store.commit("ConfigModule/mutateNextIndex");          
      }
    },
};
</script>

<style scoped>

</style>