<template>
<div class="center">
    <div><h1>Is there any deadline for request?</h1></div>
<div class="form-group btn-group-vertical">     
    <button @click.prevent="setDeadlineStatus($event)"  class="btn btn-success btn-lg locationButton" value=true>Yes, I have a deadline.</button>
    <button @click.prevent="setDeadlineStatus($event)"  class="btn btn-success btn-lg locationButton" value=false>No, just as soon as possible.</button>
</div>
 <div class="disclaimer">{{disclaimer}}</div>
</div>    
</template>
<script>
export default {
    name:"WizardPage_11",
    data(){
      return{
      }
    },
    methods:{
      setDeadlineStatus($event){           
            this.$store.commit("ConfigModule/mutateNextIndex");
            this.$store.commit("requestermodule/mutatedeadlineStatus",$event.target.value);   
            console.log($event.target.value);       
      }
    },
    computed:{
        disclaimer(){
            return this.$store.state.ConfigModule.wp15_disclaimer;
        }       
    }
}
</script>
<style scoped>

</style>