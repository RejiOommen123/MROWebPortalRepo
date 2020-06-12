<template>
  <div id="demo">
    <!-- Add Location Button which will redirect to AddLocation Page -->
        <v-row no-gutters>
          <v-col cols="2" sm="2" md="3">
            <div class="my-2">
              <v-btn id="addlocation" color="primary" :to="'/AddLocation/'+this.$route.params.id">Add Location</v-btn>
            </div>
          </v-col>
        </v-row>

    <!-- Vuetify Card with Location List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Locations List For Facility - {{facilityName}}
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>

      <!-- Location List DataTable  -->
      <v-data-table :headers="headers" :items="gridData" :search="search" class="body-1">

        <!-- Location List Actions (Edit & Delete)  -->
        <template v-slot:item.actions="{ item }">

          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditLocation/'+item.nFacilityLocationID">             
                <v-icon  v-on="on" medium class="mr-2">mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Location</span>
          </v-tooltip>

          <v-tooltip top>
            <template v-slot:activator="{ on }">              
                <v-icon v-on="on" medium @click="deleteItem(item.nFacilityLocationID,item.sLocationName)">mdi-delete</v-icon>            
            </template>
            <span>Delete Location</span>
          </v-tooltip>   
        </template>

      </v-data-table>
       <!-- End Location List DataTable  -->
    </v-card>

    <!-- Dialog box for delete Location  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">Are you sure do you want to <br> delete '{{editedItem.sLocationName}}' <br> Location?</v-card-title>

        <!-- <v-card-text class="red--text">Clicking Agree will Permanently Delete the Location</v-card-text>
        <v-card-text class="blue--text">Clicking Disagree will close the Modal</v-card-text> -->
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
          <v-btn color="red darken-1" text @click="deleteLocation(editedItem.nFacilityLocationID)">Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialog: false,
      search: "",
      headers: [
        {
          text: "Location Name",
          align: "start",
          value: "sLocationName"
        },
        { text: "Location Code", value: "sLocationCode" },
        { text: "Location Address", value: "sLocationAddress" },
        { text: "Actions", value: "actions", sortable: false }
      ],
      gridData: this.getGridData(),
      facilityName:"",
      editedItem: {
        nFacilityLocationID:0,
        sLocationName: ''
      },
    };
  },
  methods: {
    // API to Get all Facilities
    getGridData() {
      this.$http.get("http://localhost:57364/api/FacilityLocations/GetFacilityLocationByFacilityID/"+this.$route.params.id).then(
        response => {
          // get body data
          this.gridData = JSON.parse(response.bodyText);
            this.gridData = JSON.parse(response.bodyText)["locations"];

          this.facilityName = JSON.parse(response.bodyText)["faciName"];

        },
        response => {
          // error callback
          this.gridData = response.body;
        }
      );
    },
    //Method to pass nLocationID & sLocationName to dialog box
     deleteItem(nFacilityLocationID,sLocationName) {
        this.editedItem.nFacilityLocationID=nFacilityLocationID;
        this.editedItem.sLocationName=sLocationName;
        this.dialog = true;
    },
    //On Agree in dialog box API to delete Location
    deleteLocation(id) {
      this.dialog = false;
      this.$http
        .post("http://localhost:57364/api/FacilityLocations/DeleteFacilityLocation/", id)
        .then(response => {
          if (response.ok == true) {
            this.$router.go();
          }
        });
    }
  }
};
</script>

<style scoped>
.mrorouterlink{
  text-decoration: none;
}
#addlocation{
  margin-bottom:20px ;
}
#editlocation{
    margin-right: 10px;
}
button {
  margin: 10px;
}
#demo {
  margin: 0 100px;
  padding: 20px;
}
#search {
  padding-bottom: 30px;
}
</style>