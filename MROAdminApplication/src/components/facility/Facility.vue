<template>
  <div id="FacilitiesPageBox">
    <!-- Add Facility Button which will redirect to Add Facility Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="12" md="6">
        <v-btn
          depressed
          small
          class="mx-2"
          fab
          dark
          color="rgb(0, 91, 168)"
          id="addFacility"
          to="/AddFacility"
        >
          <v-icon>mdi-plus</v-icon>
        </v-btn>
        <span id="addFacBtn">Add Facility</span>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Facility List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Facilities List
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Facility List DataTable  -->
      <v-data-table
        :headers="headers"
        :items="gridData"
        :search="search"
        :footer-props="{
          'items-per-page-options': [10,25,50,100]
        }"
        :items-per-page="5"
        class="body-1"
        fixed-header
        height="60vh"
      >
        <!-- Facility Location Count Template -->
        <template v-slot:item.nFacLocCount="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                v-on="on"
                id="facilityLocationCount"
                :to="'/Locations/'+item.facilities.nFacilityID"
                color="rgb(0, 91, 168)"
              >{{item.nFacLocCount}}</router-link>
            </template>
            <span>Manage Locations</span>
          </v-tooltip>
        </template>
        <!-- Facility Patient Form Count Template -->
        <template v-slot:item.Fields="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditFacilityData/'+item.facilities.nFacilityID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>assignment</v-icon>
              </router-link>
            </template>
            <span>Edit Form</span>
          </v-tooltip>
        </template>
        <!-- Facility Active Status Template -->
        <template v-slot:item.bActiveStatus="{ item }">
          <v-switch
            color="#1AA260"
            @click="deleteItem(item.facilities.nFacilityID,item.facilities.sFacilityName)"
            v-model="item.facilities.bActiveStatus"
          ></v-switch>
        </template>
        <!-- Facility List Actions (Edit Facility, Facility Locations)  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditFacility/'+item.facilities.nFacilityID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Facility</span>
          </v-tooltip>
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                id="facilityLocations"
                :to="'/Locations/'+item.facilities.nFacilityID"
              >
                <v-icon v-on="on" medium>mdi-location_on</v-icon>
              </router-link>
            </template>
            <span>Facility Locations</span>
          </v-tooltip>
        </template>
      </v-data-table>
      <!-- End Facility List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Active Status of Facility  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />change the active status?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
          <v-btn color="red darken-1" text @click="deleteFacility(editedItem.nFacilityID)">Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog Alert for 0 Locations -->
    <v-dialog v-model="facilityAlert" max-width="360" width="350">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>To Active Facility atleast 1 Location must be Present</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="facilityAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
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
      facilityAlert: false,
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "facilities.sFacilityName",
          width: "20%" 
        },
        { text: "Description", value: "facilities.sDescription", width: "40%" },
        { text: "Locations", value: "nFacLocCount", align: "center", width: "10%"},
        {
          text: "Edit Fields/Disclaimers",
          value: "Fields",
          sortable: false,
          align: "center"
        },
        { text: "Active", value: "bActiveStatus"},
        { text: "Edit", value: "actions", sortable: false}
      ],
      gridData: this.getGridData(),
      editedItem: {
        nFacilityID: 0,
        sFacilityName: ""
      }
    };
  },
  methods: {
    // API Call to Get all Facilities
    getGridData() {
      this.dialogLoader = true;
      this.$http.get("facility/GetFacilities").then(
        response => {
          this.gridData = JSON.parse(response.bodyText);
          this.dialogLoader = false;
        },
        response => {
          // Error Callback
          this.gridData = response.body;
        }
      );
    },
    //Method to pass nFacilityID & sFacilityName to dialog box
    deleteItem(nFacilityID, sFacilityName) {
      this.editedItem.nFacilityID = nFacilityID;
      this.editedItem.sFacilityName = sFacilityName;
      this.dialog = true;
    },
    //On Agree in dialog box API Call to Toggle Active Status of Facility
    deleteFacility(id) {
      this.dialogLoader =true;
      var combinedObj = {
        nFacilityID: id,
        nAdminUserID: this.$store.state.adminUserId 
      };
      this.dialog = false;
      this.$http.post("facility/ToggleFacility/", combinedObj).then(response => {
        if (response.ok == true) 
        {
          this.dialogLoader =false;
          if (response.body == "Cannot Activate Facility, Location Count = 0") 
          {
            this.dialogLoader =false;
            this.facilityAlert = true;
          } else 
          {
            this.dialogLoader =false;
            this.$router.go();
          }
        }
      });
    }
  }
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #FacilitiesPageBox {
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
</style>