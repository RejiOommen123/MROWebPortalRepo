<template>
  <div>
    <v-app-bar color="#005ba8" dark>
      <span class="hidden-md-and-up">
        <v-menu transition="slide-x-transition" :close-on-content-click='closeMenu' bottom right>
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="#005ba8" dark v-bind="attrs" v-on="on">
              <v-icon>mdi-menu</v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item
              v-for="(item, i) in menuItems"
              :key="i"
              :to="item.path"
              @click="closeMenu=true"
            >
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item>
            <v-list-group>
              <template v-slot:activator>
                <v-list-item-content @click="closeMenu=false">
                  <v-list-item-title>Master</v-list-item-title>
                </v-list-item-content>
              </template>
              <v-list-item to="/Master/primaryreason"  @click="closeMenu=true"> 
                <v-list-item-title>Primary Reason</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/recordtype"  @click="closeMenu=true">
                <v-list-item-title>Record Type</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/sensitiveinfo"  @click="closeMenu=true">
                <v-list-item-title>Sensitive Info</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/shipmenttype"  @click="closeMenu=true">
                <v-list-item-title>Shipment Type</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/patientrepresentative"  @click="closeMenu=true">
                <v-list-item-title>Patient Representative</v-list-item-title>
              </v-list-item>
            </v-list-group>
            <v-list-group>
              <template v-slot:activator>
                <v-list-item-content @click="closeMenu=false">
                  <v-list-item-title>Reports</v-list-item-title>
                </v-list-item-content>
              </template>
              <v-list-item to="/Report/audit" @click="closeMenu=true">
                <v-list-item-title>Audit</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Report/facilitylocation" @click="closeMenu=true">
                <v-list-item-title>Facililty/Location</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Report/facilityconfiguration" @click="closeMenu=true">
                <v-list-item-title>Facililty Configuration</v-list-item-title>
              </v-list-item>
            </v-list-group>
          </v-list>
        </v-menu>
      </span>
      <v-toolbar-title class="myUL hidden-xs-only hidden-sm-only"
        >MRO Admin</v-toolbar-title
      >
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <h1 id="pageHeaderH1">{{ this.pageHeader }}</h1>
      <v-spacer></v-spacer>
      <ul class="myUL">
        <li>
          <router-link
            to="/facility"
            class="pageheaderLinksWhite hidden-xs-only hidden-sm-only"
            >Manage Facilities</router-link
          >
        </li>
        <li>
          <v-menu  open-on-hover open-on-click bottom offset-y>
            <template v-slot:activator="{ on, attrs }">
              <v-btn 
                id="masterButton"
                text
                class="pageheaderLinksWhite hidden-xs-only hidden-sm-only"
                v-bind="attrs"
                v-on="on"
                retain-focus-on-click
                style="margin-right:0px"
              >
                Master<v-icon>mdi-menu-down</v-icon>
              </v-btn>
            </template>
             <!-- <template v-slot:activator="{ on, attrs }">
              <v-btn 
                text
                dark
                v-bind="attrs"
                v-on="on"
              >
                Dropdown
              </v-btn>
            </template> -->
            <v-list>
              <v-list-item to="/Master/primaryreason">
                <v-list-item-title>Primary Reason</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/recordtype">
                <v-list-item-title>Record Type</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/sensitiveinfo">
                <v-list-item-title>Sensitive Info</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/shipmenttype">
                <v-list-item-title>Shipment Type</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Master/patientrepresentative">
                <v-list-item-title>Patient Representative</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </li>
        <li>
          <v-menu open-on-hover open-on-click bottom offset-y>
            <template v-slot:activator="{ on, attrs }">
              <v-btn 
                id="masterButton"
                text
                class="pageheaderLinksWhite hidden-xs-only hidden-sm-only"
                v-bind="attrs"
                v-on="on"
                retain-focus-on-click
              >
                Reports<v-icon>mdi-menu-down</v-icon>
              </v-btn>
            </template>

            <v-list>
              <v-list-item to="/Report/audit">
                <v-list-item-title>Audit</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Report/facilitylocation">
                <v-list-item-title>Facililty/Location</v-list-item-title>
              </v-list-item>
              <v-list-item to="/Report/facilityconfiguration">
                <v-list-item-title>Facililty Configuration</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </li>
        <li>
          <v-tooltip bottom>
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                color="#fba437"
                v-on:click="$adal.logout()"
                fab
                small
                dark
                v-bind="attrs"
                v-on="on"
              >
                <v-icon>mdi-account-circle</v-icon>
              </v-btn>
            </template>
            <span>{{ userName }}</span>
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
    },
  },
  data() {
    return {
      closeMenu: false,
      userName: AuthenticationContext.user.userName,
      menuItems: [
        { title: "Manage Facilites", path: "/facility", icon: "mdi-briefcase" },
      ],
    };
  },
  methods: {
    onLogout() {
      //Call for logout
      // this.$store.commit("mutateIsSignIn", false);
      // this.$router.push('/')
    },
  },
};
</script>

<style scoped>
.router-link-active {
  padding: 10px;
  background-color: #1976d2;
  color: white;
}
#masterButton {
  text-transform: none !important;
  font-size:16px;
  margin-left: 0px;
  font-weight:400;
  letter-spacing:0px;
}
</style>