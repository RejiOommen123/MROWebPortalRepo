<template>
  <div id="demo">
    <!-- Add Facility Button which will redirect to AddFacility Page -->
        <v-row no-gutters>
          <v-col cols="2" sm="2" md="3">
            <div class="my-2">
              <v-btn id="addfacility" color="primary" to="/AddFacility">Add Facility</v-btn>
            </div>
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
      <v-data-table :headers="headers" :items="gridData" :search="search">

        <!-- Facility List Actions (Edit,Delete,Location and ManageField)  -->
        <template v-slot:item.actions="{ item }">

          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditFacility/'+item.nROIFacilityID">             
                <v-icon v-on="on" medium class="mr-2">mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Facility</span>
          </v-tooltip>

          <v-tooltip top>
            <template v-slot:activator="{ on }">              
                <v-icon v-on="on" medium @click="deleteItem(item.nROIFacilityID,item.sFacilityName)">mdi-delete</v-icon>            
            </template>
            <span>Delete Facility</span>
          </v-tooltip>
      
           <v-tooltip top>
            <template v-slot:activator="{ on }">   
               <router-link class="mrorouterlink" id="facilitylocation" :to="'/Locations/'+item.nROIFacilityID">               
                <v-icon v-on="on" medium >mdi-hospital-marker</v-icon>   
                </router-link>             
            </template>
            <span>Facility Locations</span>
          </v-tooltip>
          
          <v-tooltip top>
            <template v-slot:activator="{ on }">       
              <router-link class="mrorouterlink" :to="'/EditFields/'+item.nROIFacilityID">          
                <v-icon v-on="on" medium >mdi-format-list-checks</v-icon> 
              </router-link>            
            </template>
            <span>Manage Fields</span>
          </v-tooltip> 

        </template>

      </v-data-table>
       <!-- End Facility List DataTable  -->
    </v-card>

    <!-- Dialog box for delete facility  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">Are you sure do you want to delete '{{editedItem.sFacilityName}}' Facility?</v-card-title>

        <v-card-text>Selecting Agree will change Active Status to false.</v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">Disagree</v-btn>

          <v-btn color="green darken-1" text @click="deleteFacility(editedItem.nROIFacilityID)">Agree</v-btn>
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
          text: "Facility Name",
          align: "start",
          value: "sFacilityName"
        },
        { text: "Description", value: "sDescription" },
        { text: "Active Status", value: "bActiveStatus" },
        { text: "Actions", value: "actions", sortable: false }
      ],
      gridData: this.getGridData(),
      editedItem: {
        nROIFacilityID:0,
        sFacilityName: ''
      },
    };
  },
  methods: {
    // API to Get all Facilities
    getGridData() {
      this.$http.get("http://localhost:57364/api/facility/GetFacility").then(
        response => {
          // get body data
          this.gridData = JSON.parse(response.bodyText);
        },
        response => {
          // error callback
          this.gridData = response.body;
        }
      );
    },
    //Method to pass nROIFacilityID & sFacilityName to dialog box
     deleteItem(nROIFacilityID,sFacilityName) {
        this.editedItem.nROIFacilityID=nROIFacilityID;
        this.editedItem.sFacilityName=sFacilityName;
        this.dialog = true;
    },
    //On Agree in dialog box API to delete Facility
    deleteFacility(id) {
      this.dialog = false;
      this.$http
        .post("http://localhost:57364/api/facility/DeleteFacility/", id)
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
#addfacility{
  margin-bottom:20px ;
}
#facilitylocation{
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