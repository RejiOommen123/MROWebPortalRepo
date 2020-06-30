<template>
  <v-app>
    <!-- App Starts here -->
    <!-- Content Loaded Under Vue Content based on Routing-->
    <v-content>
      <app-header />
      <router-view></router-view>
    </v-content>
    <!-- Fixed Footer for all Pages -->
    <v-footer padless>
      <v-col class="text-center" cols="12" id="footer">
        &copy;{{ new Date().getFullYear() }} — Powered by
        <strong>
          <a href="https://mrocorp.com/" target="_blank">MRO</a>
        </strong>
      </v-col>
    </v-footer>
  </v-app>
</template>

<script>
  import { AuthenticationContext } from 'vue-adal' 
import Header from "./components/header/header.vue";
export default {
  name: "app",
  components: {
    "app-header": Header
  },
  //Code to Remove Scroll Bar Both Life Cycles Events - mounted, destroyed
  mounted: function() {
    var adminUser = 
      {
        sUPN:AuthenticationContext.user.profile.upn,
        sName:AuthenticationContext.user.profile.name,
        sEmail:AuthenticationContext.user.userName
      }
      this.$http
        .post("auth/GetAdminUserID",adminUser)
        .then(response => {
          if (response.ok == true) {
             this.$store.commit("adminUserId", response.body);
          }
        });

    let elHtml = document.getElementsByTagName("html")[0];
    elHtml.style.overflowY = "auto";
  },
  destroyed: function() {
    let elHtml = document.getElementsByTagName("html")[0];
    elHtml.style.overflowY = null;
  }
};
</script>

<style>
body,
html {
  margin: 0;
  font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
  overflow-x: hidden;
}
#footer {
  margin-top: -25px;
}
</style>