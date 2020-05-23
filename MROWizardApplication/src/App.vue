
<template>
  <div>
    <div class="myImage">
      <div v-show="showBackBtn">
        <button type="button" @click.prevent="doNothing" class="btn btn-default">
          <i class="fa fa-angle-left" style="font-size:24px"></i>
        </button>
      </div>
      <img
        src="../src/assets/logo.png"
        height="125px"
        alt="Vue JS"
        style="vertical-align:middle"
        class="center-img"
      />
      <transition
        appear
        enter-active-class="animated fadeIn"
        leave-active-class="animated fadeOut"
        mode="out-in"
      >
        <keep-alive>
          <component :is="selectedPage"></component>
        </keep-alive>
      </transition>
    </div>
  </div>
</template>

<script>
import FirstPage from "./components/Pages/FirstPage";
import SecondPage from "./components/Pages/SecondPage";
import ThirdPage from "./components/Pages/ThirdPage";
import FourthPage from "./components/Pages/FourthPage";
import FifthPage from "./components/Pages/FifthPage.vue";
import SixthPage from "./components/Pages/SixthPage.vue";
import SeventhPage from "./components/Pages/SeventhPage.vue";
import EighthPage from "./components/Pages/EighthPage.vue";
import NinthPage from "./components/Pages/NinthPage.vue";

export default {
  name: "App",
  data(){
    return{
    }
  },
  computed: {
    selectedPage() {
      return this.$store.state.appmodule.selectedPage;
    },
    showBackBtn() {
      return this.$store.state.appmodule.showBackBtn;
    },
    
  },
  methods: {
    doNothing() {
      // alert("Working");
      var currPageAfterClickingBack = this.$store.state.appmodule.selectedPageNumber;
      --currPageAfterClickingBack;
      //if it becomes 1 reset v-show to false
      if(currPageAfterClickingBack==1){
        this.$store.state.appmodule.showBackBtn=false;
      }
      this.$store.state.appmodule.selectedPage ="page-"+currPageAfterClickingBack;
      this.$store.commit("appmodule/mutatepageNumerical", currPageAfterClickingBack);
      this.$store.commit("appmodule/mutateCurrentPage",this.$store.state.appmodule.selectedPage );
    }
  },
  components: {
    "page-1": FirstPage,
    "page-2": SecondPage,
    "page-3": ThirdPage,
    "page-4": FourthPage,
    "page-5": FifthPage,
    "page-6": SixthPage,
    "page-7": SeventhPage,
    "page-8": EighthPage,
    "page-9": NinthPage
  }
};
</script>
<style scoped>
.center-img {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: auto;
}
.myImage {
  background-image: url("./assets/healthcarebg.jpg");
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
