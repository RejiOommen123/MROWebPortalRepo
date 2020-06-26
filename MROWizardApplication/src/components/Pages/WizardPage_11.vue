<template>
<div class="center">
    <div><h1>To whom should we release <br/>these medical records to? </h1></div>
<div class="form-group btn-group-vertical">
        <div v-for="releaseTo in oReleaseToArray" :key='releaseTo.sNormalizedReleaseTo'>          
          <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
          <button @click.prevent="releaseRequestTo($event)"  class="btn btn-success btn-lg locationButton" :value=releaseTo.sNormalizedReleaseTo>{{releaseTo.sReleaseTo}}</button>
        </div>
</div>
</div>    
</template>
<script>
export default {
    name:"WizardPage_11",
    data(){
      return{
        oReleaseToArray : this.$store.state.ConfigModule.oReleaseRequestTo,
      }
    },
    methods:{
      releaseRequestTo($event){  
            this.$store.commit("requestermodule/sReleaseTo",$event.target.value);
            this.$store.commit("ConfigModule/mutateNextIndex");
      }
    }
}
</script>
<style scoped>

</style>