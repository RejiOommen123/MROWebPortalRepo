<template>
  <div id="demo">
    <!-- Add Facility Button which will redirect to AddFacility Page -->
        <v-row no-gutters>
          <v-col cols="6" sm="2" md="6">
            
              <!-- <v-btn id="addfacility" color="primary" to="/AddFacility">Add Facility</v-btn> -->
               <v-btn small class="mx-2" fab dark color='rgb(0, 91, 168)' id="addfacility" to="/AddFacility">
                  <v-icon>mdi-plus</v-icon> 
              </v-btn><span id="AddFac" style="font-size:24px">Add Facility</span>
            
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
      <v-data-table :headers="headers" :items="gridData" :search="search"
      class="body-1">
<template v-slot:item.nFacLocCount="{ item }">
<v-tooltip top>
            <template v-slot:activator="{ on }">   
               <router-link class="mrorouterlink" id="facilitylocation" :to="'/Locations/'+item.nFacilityID" color='rgb(0, 91, 168)' v-on="on">               
               {{item.nFacLocCount}}
                </router-link>            
    
                <!-- <v-btn :to="'/Locations/'+item.nFacilityID">{{item.nFacLocCount}}</v-btn> -->
            <!-- <v-btn :to="'/Locations/'+item.nFacilityID" v-on="on" color='rgb(0, 91, 168)'>
           {{item.nFacLocCount}}
            </v-btn> -->

            </template>
            <span>Manage Locations</span>
          </v-tooltip>
          </template>


<template v-slot:item.Fields="{ item }">

          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditFields/'+item.nFacilityID">             
                <v-icon color='rgb(0, 91, 168)' v-on="on" medium class="mr-2">assignment</v-icon>
                <!-- <v-img src="https://lh3.googleusercontent.com/proxy/2OQpbPrMGARzRvE92n73NNqOQIOTQ1R8iGPY17bOkNc-Kis_cEthPSttMw4975yUnOafbw44sOUffD42Yn2x3yrVqmd6YoQbqZQvpHT2kTM" v-on="on" medium class="mr-2" width=30 height=29></v-img> -->

              </router-link>
            </template>
            <span>Patient Form</span>
          </v-tooltip>

        </template>

<template v-slot:item.bActiveStatus="{ item }">
      <!-- <v-chip :color="getColor(item.bActiveStatus)">{{ getStatus(item.bActiveStatus) }}</v-chip> -->
      <v-switch color='#1AA260' style="padding-left:35px" @click="deleteItem(item.nFacilityID,item.sFacilityName)" v-model="item.bActiveStatus"></v-switch>
    </template>


        <!-- Facility List Actions (Edit,Delete,Location and ManageField)  -->
        <template v-slot:item.actions="{ item }">

          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditFacility/'+item.nFacilityID">             
                <v-icon color='rgb(0, 91, 168)' v-on="on" medium class="mr-2">mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Facility</span>
          </v-tooltip>

          <!-- <v-tooltip top>
            <template v-slot:activator="{ on }">              
                <v-icon v-on="on" medium @click="deleteItem(item.nFacilityID,item.sFacilityName)">mdi-delete</v-icon>            
            </template>
            <span>Delete Facility</span>
          </v-tooltip> -->
      
           <v-tooltip top>
            <template v-slot:activator="{ on }">   
               <router-link class="mrorouterlink" id="facilitylocation" :to="'/Locations/'+item.nFacilityID">               
                <v-icon v-on="on" medium >mdi-location_on</v-icon>   
                </router-link>             
            </template>
            <span>Facility Locations</span>
          </v-tooltip>
          
          <!-- <v-tooltip top>
            <template v-slot:activator="{ on }">       
              <router-link class="mrorouterlink" :to="'/EditFields/'+item.nFacilityID">          
                <v-icon v-on="on" medium >mdi-format-list-checks</v-icon> 
              </router-link>            
            </template>
            <span>Manage Fields</span>
          </v-tooltip>  -->

        </template>

      </v-data-table>
       <!-- End Facility List DataTable  -->
    </v-card>

    <!-- Dialog box for delete facility  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">Are you sure you want to <br>change the active status?</v-card-title>

        <!-- <v-card-text class="red--text">Clicking Agree will change Active Status to false.</v-card-text>
        <v-card-text class="blue--text">Clicking Disagree will close the Modal</v-card-text> -->
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>

          <v-btn color="red darken-1" text @click="deleteFacility(editedItem.nFacilityID)">Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      // facLocCount:null,
      dialog: false,
      search: "",
      headers: [
        {
          text: "Facility Name",
          align: "start",
          value: "sFacilityName"
        },
        { text: "Description", value: "sDescription" , width:'50%'},
        { text: "Locations", value: "nFacLocCount" ,align:'center'},
        { text: "Patient Form", value: "Fields" ,align:'center'},
        { text: "Active", value: "bActiveStatus" ,align:'center'},
        { text: "Edit", value: "actions", sortable: false ,align:'center'}
      ],
      gridData: this.getGridData(),
      editedItem: {
        nFacilityID:0,
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
          // this.facLocCount = JSON.parse(response.bodyText)['facLocCount'];
          this.gridData = JSON.parse(response.bodyText);
         

        },
        response => {
          // error callback
          this.gridData = response.body;
        }
      );
    },
    //Method to pass nROIFacilityID & sFacilityName to dialog box
     deleteItem(nFacilityID,sFacilityName) {
        this.editedItem.nFacilityID=nFacilityID;
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
#AddFac{
  font-size:24px
}
#addfacility{
  margin-top:15px
}
#addUnderline{
  text-decoration:underline
}
</style>