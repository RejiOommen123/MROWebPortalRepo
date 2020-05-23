<template>
<div class="center">
    <div><h6>Great!!, from which location do you
want to request for records ?</h6></div><br><br>
<div class="form-group btn-group-vertical">
        <button @click.prevent="locationRequest($event)" class="btn btn-success btn-lg" value="Location 1">Location 1</button><br>
        <button @click.prevent="locationRequest($event)" class="btn btn-success btn-lg" value="Location 2">Location 2</button><br>
        <button @click.prevent="locationRequest($event)" class="btn btn-success btn-lg" value="Location 3">Location 3</button>

</div>
</div>    
</template>
<script>
export default {
    name:"SecondPage",
    methods:{
      locationRequest($event){
            this.$store.commit("appmodule/mutatepageNumerical",3);
            this.$store.commit("appmodule/mutateCurrentPage","page-3");
            this.$store.commit("appmodule/mutateSelectedLocation",$event.target.value);
      }
    }
}
</script>
<style scoped>
.center {
  text-align: center;
}
</style>