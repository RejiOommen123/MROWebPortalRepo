<template>
  <div class="center">
    <h1>Camera Demo</h1>
    <vc-cam ref="camera"
    width=100
    height=100></vc-cam>
    <v-btn @click="takeSnap">Snapshot</v-btn>
  </div>
</template>

<script>
import 'vue-cam'
export default {
  name: "WizardPage_14",
  data() {
    return {
     
    };
  },
  methods: {
    takeSnap(){
        let snapBase64 = this.$refs.camera.snapshot();
        this.$store.commit("ConfigModule/mutateNextIndex");
        console.log(snapBase64);
          
                this.$store.commit("ConfigModule/mutatedialogMinWidth", "100%");
          this.$store.commit("ConfigModule/mutatedialogMaxWidth", "100%");
          this.$store.commit("ConfigModule/mutatedialogMaxHeight", "100%");
            this.$vuetify.theme.dark = false
			
    }
  }
};
</script>

<style scoped>

</style>