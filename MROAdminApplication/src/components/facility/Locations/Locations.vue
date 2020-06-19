<template>
  <div id="demo">
    <!-- Add Location Button which will redirect to AddLocation Page -->
        <v-row no-gutters>
          <v-col cols="6" sm="2" md="6">
            <div class="my-2">
              <!-- <v-btn id="addlocation" color="primary" :to="'/AddLocation/'+this.$route.params.id">Add Location</v-btn> -->
              <v-btn depressed color='rgb(0, 91, 168)' id="addlocation" small class="mx-2" fab dark :to="'/AddLocation/'+this.$route.params.id">
              <v-icon box-shadow:none>mdi-plus</v-icon></v-btn><span id="AddLoc" style="font-size:24px">Add Location</span>
            </div>
          </v-col>
        </v-row>


        <!-- <v-row no-gutters>
          <v-col cols="6" sm="2" md="6">
               <v-btn small class="mx-2" fab dark color='rgb(0, 91, 168)' id="addfacility" to="/AddFacility">
                  <v-icon>mdi-plus</v-icon> 
              </v-btn><span id="AddFac" style="font-size:24px">Add Facility</span>
            
          </v-col>
        </v-row> -->

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
      <v-data-table :headers="headers" :items="gridData" :search="search" class="body-1" :footer-props="{
    'items-per-page-options': [5,10]
  }"
  :items-per-page="5">






        <!-- Location List Actions (Edit & Delete)  -->
        <template v-slot:item.actions="{ item }">
<v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditLocation/'+item.nFacilityLocationID">             
                <v-icon color='rgb(0, 91, 168)'  v-on="on" medium class="mr-2">mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Location</span>
          </v-tooltip>

             
        </template>

<template v-slot:item.bLocationActiveStatus="{ item }">
  <!-- <v-tooltip top>
            <template v-slot:activator="{ on }">              
                <v-icon color='red' v-on="on" medium @click="deleteItem(item.nFacilityLocationID,item.sLocationName)">mdi-delete</v-icon>            
            </template>
            <span>Delete Location</span>
          </v-tooltip> -->
      <v-switch v-model="item.bLocationActiveStatus" color='#1AA260' @click="deleteItem(item.nFacilityLocationID,item.sLocationName)"></v-switch>

  </template>
      </v-data-table>
       <!-- End Location List DataTable  -->
    </v-card>

    <!-- Dialog box for delete Location  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">Are you sure you want to <br/>change the active status?</v-card-title>

        <!-- <v-card-text class="red--text">Clicking Agree will Permanently Delete the Location</v-card-text>
        <v-card-text class="blue--text">Clicking Disagree will close the Modal</v-card-text> -->
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
          <v-btn color="red darken-1" text @click="deleteLocation(editedItem.nFacilityLocationID)">Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog
      v-model="locationAlert"
      max-width="360"
      width="350"
    >
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>
          Provide Valid Authorization Document to Activate Location
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="locationAlert = false"
          >Ok
          </v-btn>
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
      locationAlert:false,
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "sLocationName",
          width:'20%'
        },
        { text: "Address", value: "sLocationAddress",width:'50%' },
        { text: "Code", value: "sLocationCode",align:'center',width:'15%'},
        { text: "Active", value: "bLocationActiveStatus"},
        { text: "Edit", value: "actions", sortable: false,align:'center' }
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
      this.$http.get("FacilityLocations/GetFacilityLocationByFacilityID/"+this.$route.params.id).then(
        response => {
          // get body data
          //this.gridData = JSON.parse(response.bodyText);
    //console.log(JSON.parse(response.bodyText)["locations"]);

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
        .post("FacilityLocations/DeleteFacilityLocation/", id)
        .then(response => {
          if (response.ok == true) {
            if(response.body=="Provide Valid Authorization PDF"){
              this.locationAlert = true
          }
          else
            this.$router.go();
          }
        });
    },
    getStatus (status) {
        if(status) return 'Active'
        else return 'Inactive'
      },
      getColor (status) {
        if(status) return 'green'
        else return 'red'
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
#AddLoc{
  font-size:24px
}
#addlocation{
  margin-top:15px
}
</style>