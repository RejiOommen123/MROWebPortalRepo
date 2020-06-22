<template>
<div class="center">
    <div><h1>To whom should we release <br/>these medical records to? </h1></div>
<div class="form-group btn-group-vertical">
        <div v-for="releaseTo in releaseToArray" :key='releaseTo'>          
          <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
          <button @click.prevent="releaseRequestTo($event)"  class="btn btn-success btn-lg locationButton" :value=releaseTo>{{releaseTo}}</button>
        </div>
</div>
</div>    
</template>
<script>
export default {
    name:"WizardPage_11",
    data(){
      return{
        releaseToArray : this.$store.state.ConfigModule.wp11_releaseToArray
      }
    },
    methods:{
      releaseRequestTo($event){           
            this.$store.commit("ConfigModule/mutateNextIndex");
            this.$store.commit("requestermodule/mutatereleaseTo",$event.target.value);          
      }
    }
}
</script>
<style scoped>

</style>