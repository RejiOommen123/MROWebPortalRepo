<template>
  <div id="RecordTypePageBox">
    <!-- Add Record Type Button which will redirect to Add Record Type Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="12" md="6">
        <v-btn
          depressed
          small
          class="mx-2"
          fab
          dark
          color="rgb(0, 91, 168)"
          id="addRecordType"
          to="/RecordType/AddRecordType"
        >
          <v-icon>mdi-plus</v-icon>
        </v-btn>
        <span id="addRecBtn">Add Record Type</span>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Record Type List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Record Type List
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Record Type List DataTable  -->
      <v-data-table
        :headers="headers"
        :items="gridData"
        :search="search"
        :footer-props="{
          'items-per-page-options': [5,10]
        }"
        :items-per-page="5"
        class="body-1"
      >
        <!-- Record Type List Actions (Edit Record Type, Facility Locations)  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/RecordType/EditRecordType/'+item.nRecordTypeID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Record Type</span>
          </v-tooltip>
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn icon @click="deleteItem(item.nRecordTypeID, item.sRecordTypeName)">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-delete</v-icon>
              </v-btn>
            </template>
            <span>Delete Record Type</span>
          </v-tooltip>
          <!-- <v-tooltip top>
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
          </v-tooltip> -->
        </template>
      </v-data-table>
      <!-- End Record Type List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Active Status of Facility  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />delete?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="deleteRecordType(editedItem.nRecordTypeID)">Yes</v-btn>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
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
          value: "sRecordTypeName",
          width: "20%" 
        },
        { text: "Tooltip", value: "sFieldToolTip", width: "40%" },
        { text: "Normalized Name", value: "sNormalizedRecordTypeName", width: "30%"},
        { text: "Edit", value: "actions", align:"center", sortable: false}
      ],
      gridData: this.getGridData(),
      editedItem: {
        nRecordTypeID: 0,
        sRecordTypeName: ""
      }
    };
  },
  methods: {
    // API Call to Get all Record Types
    getGridData() {
      this.dialogLoader = true;
      this.$http.get("master/GetRecordTypes").then(
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
    deleteItem(nRecordTypeID, sRecordTypeName) {
      this.editedItem.nRecordTypeID = nRecordTypeID;
      this.editedItem.sRecordTypeName = sRecordTypeName;
      this.dialog = true;
    },
    //On Agree in dialog box API Call to Toggle Active Status of Facility
    deleteRecordType(id) {
      this.dialogLoader =true;
      var combinedObj = {
        nRecordTypeID: id,
        nAdminUserID: this.$store.state.adminUserId 
      };
      this.dialog = false;
      this.$http.post("master/DeleteRecordType/", combinedObj).then(response => {
        if (response.ok == true) 
        {
          this.dialogLoader =false;
          if (response.body == "Cannot Delete Record Type") 
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
  #RecordTypePageBox {
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