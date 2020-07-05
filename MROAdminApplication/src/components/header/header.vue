<template>
  <div>
    <v-app-bar color="#005ba8" dark>
      <span class="hidden-md-and-up">
        <v-menu transition="slide-x-transition" bottom right>
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="#005ba8" dark v-bind="attrs" v-on="on">
              <v-icon>mdi-menu</v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item v-for="(item, i) in menuItems" :key="i" :to="item.path">
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </span>
      <v-toolbar-title class="myUL hidden-xs-only hidden-sm-only">MRO Admin</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <h1 id="pageHeaderH1">{{this.pageHeader}}</h1>
      <v-spacer></v-spacer>
      <ul class="myUL">
        <li>
          <router-link
            to="/index"
            class="pageheaderLinksWhite hidden-xs-only hidden-sm-only"
          >Dashboard</router-link>
        </li>
        <li>
          <router-link
            to="/facility"
            class="pageheaderLinksWhite hidden-xs-only hidden-sm-only"
          >Manage Facilities</router-link>
        </li>
        <li>
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn color="#fba437" fab small dark v-bind="attrs" v-on="on">
                <v-icon>mdi-account-circle</v-icon>
              </v-btn>
            </template>
            <span>{{userName}}</span>
          </v-tooltip>
        </li>
        <!-- <li>
                <button @click="onLogout" class="logout">Logout</button>
        </li>-->
      </ul>
    </v-app-bar>
  </div>
</template>

<script>
import { AuthenticationContext } from "vue-adal";
export default {
  computed: {
    pageHeader() {
      return this.$store.state.pageheader;
    }
  },
  data() {
    return {
      sidebar: false,
      userName: AuthenticationContext.user.userName,
      menuItems: [
        { title: "Dashboard", path: "/index", icon: "mdi-home" },
        { title: "Manage Facilites", path: "/facility", icon: "mdi-briefcase" }
      ]
    };
  },
  methods: {
    onLogout() {
      //Call for logout
      // this.$store.commit("mutateIsSignIn", false);
      // this.$router.push('/')
    }
  }
};
</script>

<style scoped>
</style>