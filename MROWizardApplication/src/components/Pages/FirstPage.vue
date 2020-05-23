<template>
  <div class="center">
    <div><h4>Request your health records.</h4></div>
    <br>
    <div><h5>This should only take a few minutes,</h5></div>
    <div><h5>please have your photo ID ready.</h5></div>
    <br>
    <div class="alert alert-info">Note: Please use a recent version of Chrome, Firefox, or Safari.</div>
    <div>
        <button @click.prevent="nextPage" class="btn btn-success">Let's Go!!</button>
    </div>
    <br>
    <div><a style="cursor: pointer;" href="#">Want to print, fax or mail in your request? Click here for our PDF form.</a></div>
  </div>
</template>
<script>
export default {
    name:"FirstPage",
    methods:{
        nextPage(){
            this.$store.state.appmodule.showBackBtn = true;
            this.$store.commit("appmodule/mutatepageNumerical",2);
            this.$store.commit("appmodule/mutateCurrentPage","page-2");
        }
    }
};
</script>

<style scoped>
.center {
  text-align: center;
}
</style>