<template>
  <div id="LocationsPageBox">
    <!-- Add Location Button which will redirect to Add Location Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="2" md="6">
        <div class="my-2">
          <v-btn
            depressed
            color="rgb(0, 91, 168)"
            id="addlocation"
            small
            class="mx-2"
            fab
            dark
            :to="'/AddLocation/'+this.$route.params.id"
          >
            <v-icon box-shadow:none>mdi-plus</v-icon>
          </v-btn>
          <span id="AddLoc">Add Location</span>
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
        group-by="sOrgName"
      >
           <!--Org Unit Location in Collapsed form -->
          <template v-slot:group.header="{ group, headers, toggle, isOpen }">

            <td :colspan="headers.length" style = "padding-left: 6%">
                <v-btn @click="toggle" small icon :ref="group" :data-open="isOpen">
                    <v-icon v-if="isOpen">mdi-chevron-up</v-icon>
                    <v-icon v-else>mdi-chevron-down</v-icon>
                    {{ group }}
                </v-btn>
            </td>
        </template>
        <!-- Location List Edit Location Template  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditLocation/'+item.nFacilityLocationID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Location</span>
          </v-tooltip>
        </template>

        <template v-slot:item.Fields="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link class="mrorouterlink" :to="'/EditLocationFields/'+$route.params.id+'/'+item.nFacilityLocationID">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>assignment</v-icon>
              </router-link>
            </template>
            <span>Edit Form</span>
          </v-tooltip>
        </template>
           <!-- Location List Toggle Facility Master location Template  -->
        <template v-slot:item.nFacilityMasterLocationID="{ item }">
          <v-switch
           
            v-model="item.nFacilityMasterLocationID"
            color="#1AA260"
            @click="FacilityMasterLocation(item.nFacilityMasterLocationID, item.nFacilityOrgID, item.nFacilityLocationID)"
            :disabled ="item.sOrgName== 'Unorganized'"
          ></v-switch>
        </template>

        <!-- Location List Toggle location Active Status Template  -->
        <template v-slot:item.bLocationActiveStatus="{ item }">
          <v-switch
            v-model="item.bLocationActiveStatus"
            color="#1AA260"
            @click="deleteItem(item.nFacilityLocationID,item.sLocationName)"
          ></v-switch>
        </template>

         <!-- Location List Toggle location Include in Facility Status Template  -->
        <template v-slot:item.bIncludeInFacilityLevel="{ item }">
          <div class="d-flex justify-center">
            <v-switch
              v-model="item.bIncludeInFacilityLevel"
              color="#1AA260"
              @click="toggleIncludeInFacilityLevel(item.nFacilityLocationID,item.sLocationName)"
              :disabled="!item.bLocationActiveStatus"
            ></v-switch>
          </div>
        </template>
      </v-data-table>
      <!-- End Location List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Location  -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />change the active status?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
          <v-btn
            color="red darken-1"
            text
            @click="deleteLocation(editedItem.nFacilityLocationID)"
          >Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
       <!-- Dialog box for Toggle Include In Facility Level  -->
    <v-dialog v-model="dialogIncludeInFacilityLevel" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />change the include in facility status?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="dialogIncludeInFacilityLevel = false">No</v-btn>
          <v-btn
            color="red darken-1"
            text
            @click="toggleIncludeInFacility(editedItem.nFacilityLocationID)"
          >Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog box for Toggle Facility Master Location -->
    <v-dialog v-model="dialogFacilityMasterLocation" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />Change Master Location
          <br /> Status?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="dialogFacilityMasterLocation = false">No</v-btn>
          <v-btn
            color="red darken-1"
            text
            @click="toggleFacilityMasterLocation(nFacilityMasterLocationID, nFacilityOrgID, nFacilityLocationID)"
          >Yes</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog box for Auth Doc (PDF) Validation  -->
    <v-dialog v-model="locationAlert" max-width="360px" width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Provide Valid Authorization Document to Activate Location</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="locationAlert = false">Ok</v-btn>
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
      locationAlert: false,
      search: "",
      OrganizationName: "",
      OrganizationID: 0,
      dialogIncludeInFacilityLevel: false,
      dialogFacilityMasterLocation: false,
      headers: [

        {
          text: "Name",
          align: "start",
          value: "sLocationName",
          width: "20%",
        },
        { text: "Address", value: "sLocationAddress", width: "40%" },
        { text: "Code", value: "sLocationCode", width: "10%" },
        {
          text: "Edit Fields",
          value: "Fields",
          sortable: false,
          align: "center"
        },
        {
        text: "Master Location",
        value: "nFacilityMasterLocationID",
        sortable: false,
        align:"center"

        },
        { text: "Active", value: "bLocationActiveStatus", sortable: false },
        { text: "Include In Facility Level", value: "bIncludeInFacilityLevel", sortable: false},
        { text: "Edit", value: "actions", sortable: false }
      ],
      gridData: this.getGridData(),
      facilityName: "",
      nFacilityMasterLocationID: 0,
      nFacilityLocationID: 0,
      nFacilityOrgID:0,
      editedItem: {
        nFacilityLocationID: 0,
        sLocationName: "",
      }
    };
  },
  methods: {
  
    // API Call to Get all Locations
    getGridData() {
      this.dialogLoader = true;
      this.$http
        .get(
          "FacilityLocations/GetFacilityLocationByFacilityID/sFacilityID="
            + this.$route.params.id+"&sAdminUserID="+this.$store.state.adminUserId
        )
        .then(
          response => {
            this.gridData = JSON.parse(response.bodyText)["locations"];
            this.gridData.forEach((element, index) => {
                    this.gridData[index].nFacilityMasterLocationID = element.nFacilityMasterLocationID == element.nFacilityLocationID;
                    this.gridData[index].sOrgName = element.sOrgName == null ? "Unorganized" : element.sOrgName;
            });
            this.facilityName = JSON.parse(response.bodyText)["faciName"];
            this.dialogLoader = false;
          },
          response => {
            // error callback
            this.gridData = response.body;
          }
        );
    },
    //Method to pass nFacilityLocationID & sLocationName to dialog box
    deleteItem(nFacilityLocationID, sLocationName) {
      this.editedItem.nFacilityLocationID = nFacilityLocationID;
      this.editedItem.sLocationName = sLocationName;
      this.dialog = true;
    },
    //Method to pass nFacilityMasterLocationID to dialogBox
    FacilityMasterLocation(nFacilityMasterLocationID, nFacilityOrgID, nFacilityLocationID)
    {
     this.nFacilityMasterLocationID = nFacilityMasterLocationID;    
     this.nFacilityLocationID = nFacilityLocationID;
     this.nFacilityOrgID = nFacilityOrgID; 
     this.dialogFacilityMasterLocation = true;
    },
   //Method to pass nFacilityLocationID & sLocationName to dialog box
    toggleIncludeInFacilityLevel(nFacilityLocationID, sLocationName)
    {
      //console.log("loc switch");
      this.editedItem.nFacilityLocationID = nFacilityLocationID;
      this.editedItem.sLocationName = sLocationName;
      this.dialogIncludeInFacilityLevel = true;
    },
    //On Agree in dialog box API call to Toggle Active status for Location
    deleteLocation(id) {
      this.dialogLoader =true;
      var combinedObj = {
        nfacilityLocationID: id,
        nAdminUserID: this.$store.state.adminUserId
      };
      this.dialog = false;
      this.$http
        .post("FacilityLocations/ToggleFacilityLocation/",combinedObj )
        .then(response => {
          if (response.ok == true)
          {
            this.dialogLoader =false;
            if (response.body == "Provide Valid Authorization PDF")
            {
              this.dialogLoader =false;
              this.locationAlert = true;
            }
            else
            {
              this.dialogLoader =false;
              this.$router.go();
            }
          }
        });
    },
 //On Agree in dialog box API call to Toggle Facility Master Location
    toggleFacilityMasterLocation(){
    this.dialogLoader = true;
    var Obj = {
        nAdminUserID: this.$store.state.adminUserId,
        nFacilityLocationID: this.nFacilityLocationID,      
        nFacilityOrgID: this.nFacilityOrgID,
        //nFacilityMasterLocationID: nFacilityMasterLocationID
        
      };
    this.dialog = false;
      this.$http
        .post("FacilityLocations/ToggleFacilityMasterLocation/",Obj )
        .then(response => {
          if (response.ok == true)
          {
            this.dialogLoader =false;
            this.$router.go();
            }         
        });
    },
    //On Agree in dialog box API call to Toggle Include in Facility status for Location
    toggleIncludeInFacility(id) {
      this.dialogLoader =true;
      var combinedObj = {
        nfacilityLocationID: id,
        nAdminUserID: this.$store.state.adminUserId
      };
      this.dialog = false;
      this.$http
        .post("FacilityLocations/ToggleFacilityLocationIncludeInFacility/",combinedObj )
        .then(response => {
          if (response.ok == true)
          {
            this.dialogLoader =false;
            if (response.body == "Provide Valid Authorization PDF")
            {
              this.dialogLoader =false;
              this.locationAlert = true;
            }
            else
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
  #LocationsPageBox {
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