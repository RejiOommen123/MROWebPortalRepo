<template>
  <div>
    <div class="pageBody">
      <div class="pageContent">
      <p v-if="disclaimer01!=''" class="subHeadings">
        {{disclaimer01}}
      </p>
      <div v-if="disclaimer02!=''" class="disclaimer">{{disclaimer02}}</div>
      <div class="disclaimer">If you do not have an email or physical address where you can receive your records, please contact this number {610}-994-7500, to reach the facility. They will provide other alternatives to request your records.</div>
      <div v-if="!$vuetify.breakpoint.xs">
        <v-btn @click.once="nextPage" :key="buttonKey" class="smallBlackBtn">Get Started</v-btn>
      </div>
      </div>      
      <Footer  v-if="$vuetify.breakpoint.xs"/>
    </div>
    <div v-if="$vuetify.breakpoint.xs" class="buttonBlockMobile">
      <v-btn @click.once="nextPage" :key="buttonKey" class="smallBlackBtn">Get Started</v-btn>
    </div>
    <Footer v-if="!$vuetify.breakpoint.xs"/>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Footer from '../Footer.vue';
export default {
  name: "WizardPage_01",
  components:{
    Footer
  },
  data() {
    return {
     buttonKey:1,
    };
  },
activated(){
    this.buttonKey++;
},
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;     
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
  },
  computed:{    
    ...mapState({
      disclaimer01: state => state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer01,
      disclaimer02: state => state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer02,
    }),
  }
};
</script>
