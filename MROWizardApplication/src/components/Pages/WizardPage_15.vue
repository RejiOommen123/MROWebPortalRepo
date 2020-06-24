<template>
<div class="center">
    <div><h1>Is there any deadline for request?</h1></div>
<div v-if="MRORequestDeadline" class="form-group btn-group-vertical">     
    <button @click.prevent="setDeadlineStatus($event)"  class="btn btn-success btn-lg locationButton" value=true>Yes, I have a deadline.</button>
    <button @click.prevent="setDeadlineStatus($event)"  class="btn btn-success btn-lg locationButton" value=false>No, just as soon as possible.</button>
</div>
 <div class="disclaimer">{{disclaimer}}</div>
</div>    
</template>
<script>
export default {
    name:"WizardPage_15",
    data(){
      return{
        disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_15_disclaimer01,

        //Show and Hide Fields Values
        MRORequestDeadline : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MRORequestDeadline,
      }
    },
    methods:{
      setDeadlineStatus($event){           
            this.$store.commit("ConfigModule/mutateNextIndex");
            this.$store.commit("requestermodule/bDeadlineStatus",$event.target.value);   
            console.log($event.target.value);       
      }
    }
}
</script>
<style scoped>

</style>