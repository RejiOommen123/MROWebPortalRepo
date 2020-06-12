<template>
  <v-app>
    <v-content>
    <app-header />
    <router-view></router-view>  
    </v-content>
   </v-app>
</template>

<script>
  import Header from './components/header/header.vue'
  export default {
    name: 'app',
    components: {
      'app-header': Header
    },
     mounted: function() {
    let elHtml = document.getElementsByTagName('html')[0]
    elHtml.style.overflowY = 'auto'
  },
  destroyed: function() {
    let elHtml = document.getElementsByTagName('html')[0]
    elHtml.style.overflowY = null
  },
  }
</script>

<style>
  body, html {
    margin: 0;
    font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
     
  }
  html { overflow-y: auto }
</style>