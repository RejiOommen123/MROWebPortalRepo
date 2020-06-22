<template>
  <div class="center">
    <h1>You’re almost done!</h1>
    
    <div class="disclaimer">{{disclaimer}}</div>
    <div>             
        <v-textarea filled          
          rows="4"
          row-height="30"
          shaped counter v-model="additionalData" label="Additional Details"></v-textarea>    
    </div>
    <div>
      <v-btn @click.prevent="nextPage"  color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_10",
  data() {
    return {
      additionalData:''
    };
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
        
        this.$store.commit("requestermodule/mutateadditionalData", this.additionalData);
       this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
    computed:{
        disclaimer(){
            return this.$store.state.ConfigModule.wp17_disclaimer;
        }      
    }
};
</script>

<style scoped>

</style>