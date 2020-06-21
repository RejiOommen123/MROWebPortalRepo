<template>
  <div class="center">
    <div><h1>Request your health records.</h1></div>
    <br>
    <div><p class="subHeadings">This should only take a few minutes,<br/>please have your photo ID ready.</p></div>
    <br>
    <div class="disclaimer">{{this.disclaimer}}</div>
    <div>
        <v-btn @click.prevent="nextPage" x-large  color="success" class="letsGoBtn">Get Started</v-btn>
    </div>


    <br>
    <div><a style="cursor: pointer;" href="#" id="pdfFormLink">Want to print, fax or mail in your request? Click here for our PDF form.</a></div>
     <div><p id="contact">Call 123-456-7890 for assistance</p></div>
                    <div id="poweredby">
                    <span>Powered by <a href="https://mrocorp.com/" target="_blank">MRO Corp</a></span>
                    </div>


  </div>
</template>
<script>
export default {
    name:"WizardPage_01",
    data() {
      return{
         disclaimer : this.$store.state.ConfigModule.wp01_disclaimer
      }
    },
    methods:{
        nextPage(){
            this.$store.state.ConfigModule.showBackBtn = true;
            this.$store.commit("ConfigModule/mutatepageNumerical",2);
            this.$store.commit("ConfigModule/mutateCurrentPage","page-2");
        }
    },
    mounted(){
      
    //  var widget = new RingCaptcha.Widget('#xyz', {
    //       app: "APP_KEY",
    //       events: {
    //       // Add JavaScript Callbacks
    //       verified: function(event) {
    //         alert("Phone number verified!");
    //       }
    //   }
    // }).setup();
 
    }
};
</script>

<style scoped>
/* .center {
  text-align: center;
}
#poweredby {
  position: absolute;
  left: 0;
  right: 0;
  bottom: 10px;
} */
</style>