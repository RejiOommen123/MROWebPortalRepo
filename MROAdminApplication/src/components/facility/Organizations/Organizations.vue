<template>
  <div id="OrganizationsPageBox">
    <!-- Add Organization Button which will redirect to Add Organization Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="2" md="6">
        <div class="my-2">
          <v-btn
            depressed
            color="rgb(0, 91, 168)"
            id="addorganization"
            small
            class="mx-2"
            fab
            dark
            :to="'/AddOrganization/'+this.$route.params.id"
          >
            <v-icon box-shadow:none>mdi-plus</v-icon>
          </v-btn>
          <span id="AddOrg">Add Organization</span>
        </div>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Organization List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Organizations List For Facility - {{facilityName}}
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>

      <!-- Organization List DataTable  -->
      <v-data-table
        :headers="headers"
        :items="gridData"
        :search="search"
        class="body-1"
        :footer-props="{
        'items-per-page-options': [10,25,50,100]
      }"
        :items-per-page="5"
        fixed-header
        height="60vh"
      >
        <!-- Organization List Edit Organization Template  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditOrganization/'+item.nFacilityOrgID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Organization</span>
          </v-tooltip>
        </template>
      </v-data-table>
      <!-- End Organization List DataTable  -->
    </v-card>
    <!-- Common Loader -->
        <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="rgb(0, 91, 168)" dark>
            <v-card-text>
              Please stand by
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialogLoader:false,
      dialog: false,
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "sOrgName",
          width: "40%"
        },
        { text: "Code", value: "sLocationCode", width: "40%" },
        { text: "Edit", value: "actions", sortable: false }
      ],
      gridData: this.getGridData(),
      facilityName: "",
    };
  },
  methods: {
    // API Call to Get all Locations
    getGridData() {
      this.dialogLoader = true;
      this.$http
        .get(
          "FacilityOrganizations/GetFacilityOrganizationsByFacilityID/sFacilityID=" 
            + this.$route.params.id+"&sAdminUserID="+this.$store.state.adminUserId
        )
        .then(
          response => {
            this.gridData = JSON.parse(response.bodyText)["organizations"];
            this.facilityName = JSON.parse(response.bodyText)["faciName"];
            this.dialogLoader = false;
          },
          response => {
            // error callback
            this.gridData = response.body;
          }
        );
    }
  }
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #OrganizationsPageBox {
    margin: 0 0em;
  }
  h1 {
    font-size: 14px;
  }
  .v-data-table table tbody tr:nth-child(odd) {
    /* border-left: 4px solid white !important;
  border-collapse: collapse !important; */
    border-spacing: 10 10rem !important;
    padding-top: 10px !important;
  }

  .v-data-table table tbody tr:nth-child(even) {
    /* border-left: 4px solid white !important;
   border-collapse: collapse !important; */
    border-spacing: 10 10rem !important;
    padding-top: 10px !important;
  }
  .v-data-table table tbody tr:nth-child(even) td {
    border-left: 4px solid cyan !important;
  }
  .v-data-table table tbody tr:nth-child(odd) td {
    border-left: 4px solid deeppink !important;
  }
}
#search {
  padding-bottom: 1.875em;
}
</style>