<template>
  <div id="PatientRepresentativePageBox">
    <!-- Add Patient Representative Button which will redirect to Add Patient Representative Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="12" md="6" >
        <v-btn
          depressed
          small
          class="mx-2 addButton"
          fab
          dark
          color="rgb(0, 91, 168)"
          to="/PatientRepresentative/AddPatientRepresentative"
        >
          <v-icon>mdi-plus</v-icon>
        </v-btn>
        <span id="addRecBtn">Add Patient Representative</span>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Patient Representative List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Patient Representative List
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Patient Representative List DataTable  -->
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
        <!-- Patient Representative List Actions (Edit Patient Representative, Delete Patient Representative)  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                :to="'/PatientRepresentative/EditPatientRepresentative/'+item.nPatientRepresentativeID"
              >
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Patient Representative</span>
          </v-tooltip>
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn icon @click="deleteItem(item.nPatientRepresentativeID, item.sPatientRepresentativeName)">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-delete</v-icon>
              </v-btn>
            </template>
            <span>Delete Patient Representative</span>
          </v-tooltip>
        </template>
      </v-data-table>
      <!-- End Patient Representative List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Delete Patient Representative Confirmation -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />delete?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="deletePatientRepresentative(editedItem.nPatientRepresentativeID)">Yes</v-btn>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog Alert for errors Patient Representative -->
    <v-dialog v-model="errorAlert" width="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>{{errorMessage}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="errorAlert = false">Ok</v-btn>
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
      dialogLoader: false,
      dialog: false,
      errorAlert: false,
      errorMessage: "",
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "sPatientRepresentativeName",
          width: "20%",
        },
        { text: "Tooltip", value: "sFieldToolTip", width: "40%" },
        {
          text: "Normalized Name",
          value: "sNormalizedPatientRepresentativeName",
          width: "30%",
        },
        {
          text: "Edit/Delete",
          value: "actions",
          align: "center",
          sortable: false,
        },
      ],
      gridData: this.getGridData(),
      editedItem: {
        nPatientRepresentativeID: 0,
        sPatientRepresentativeName: "",
      },
    };
  },
  methods: {
    // API Call to Get all Patient Representatives
    getGridData() {
      this.dialogLoader = true;
      this.$http.get("Master/GetPatientRepresentatives").then(
        (response) => {
          this.gridData = JSON.parse(response.bodyText);
          this.dialogLoader = false;
        },
        (error) => {
          // Error Callback
          if (error.status == 400) {
            this.dialogLoader = false;
            this.errorMessage = error.body;
            this.errorAlert = true;
          }
        }
      );
    },
    //Method to pass nPatientRepresentativeID & sPatientRepresentativeName to dialog box
    deleteItem(nPatientRepresentativeID, sPatientRepresentativeName) {
      this.editedItem.nPatientRepresentativeID = nPatientRepresentativeID;
      this.editedItem.sPatientRepresentativeName = sPatientRepresentativeName;
      this.dialog = true;
    },
    //On Agree in dialog box API Call to Delete Patient Representative
    deletePatientRepresentative(id) {
      this.dialogLoader = true;
      this.dialog = false;
      this.$http
        .get(
          "Master/DeletePatientRepresentative/sPatientRepresentativeID=" +
            id +
            "&sAdminUserID=" +
            this.$store.state.adminUserId
        )
        .then(
          (response) => {
            if (response.ok == true) {
              this.dialogLoader = false;
              this.dialogLoader = false;
              this.$router.go();
            }
          },
          (error) => {
            // Error Callback
            if (error.status == 400) {
              this.dialogLoader = false;
              this.errorMessage = error.body;
              this.errorAlert = true;
            }
          }
        );
    },
  },
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #PatientRepresentativePageBox {
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