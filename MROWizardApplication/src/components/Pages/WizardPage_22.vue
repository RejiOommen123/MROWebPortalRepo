<template>
  <div class="center">
    <h1>
      Your request was received!
    </h1>
    
    <div class="disclaimer">{{disclaimer}}</div>
  
    <div class="form-group btn-group-vertical">     
    <button @click.prevent="requestAnotherRecord($event)"  class="btn btn-success btn-lg locationButton" value=true>Request another record</button>
    <button @click.prevent="requestAnotherRecord($event)"  class="btn btn-success btn-lg locationButton" value=false>I'm done</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_22",
  data() {
    return {
    };
  },
  created(){ 
    this.$vuetify.theme.dark = true
  },
  methods:{
      requestAnotherRecord($event){  
            this.$store.commit("requestermodule/mutaterequestAnotherRecord",$event.target.value);   
            console.log($event.target.value);                
            this.$store.commit("ConfigModule/mutateNextIndex");          
      }
    },
    computed:{
        disclaimer(){
            return this.$store.state.ConfigModule.wp22_disclaimer;
        }       
    }
};
</script>

<style scoped>

</style>