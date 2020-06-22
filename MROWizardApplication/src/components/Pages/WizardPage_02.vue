<template>
<div class="center">
    <div><h1>Great! Which location <br/>are you requesting records from? </h1></div>
<div class="form-group btn-group-vertical">
        <div v-for="location in locationArray" :key='location'>          
          <!-- <v-btn class="locationButton" @click.native="locationRequest($event)" :value=location depressed large color="success">{{location}}</v-btn> -->
          <button @click.prevent="locationRequest($event)"  class="btn btn-success btn-lg locationButton" :value=location>{{location}}</button>
        </div>
</div>
</div>    
</template>
<script>
export default {
    name:"WizardPage_02",
    data(){
      return{
        locationArray : this.$store.state.ConfigModule.wp02_locationArray
      }
    },
    methods:{
      locationRequest($event){
            // this.$store.commit("ConfigModule/mutatepageNumerical",3);
            // this.$store.commit("ConfigModule/mutateCurrentPage","page-3");
                this.$store.commit("ConfigModule/mutateNextIndex");
            this.$store.commit("requestermodule/mutateSelectedLocation",$event.target.value);          
      }
    }
}
</script>
<style scoped>
/* .center {
  text-align: center;
}
.locationButton{
  height: 87px;
  margin: 10px;  
  width: 250px;
} */
</style>