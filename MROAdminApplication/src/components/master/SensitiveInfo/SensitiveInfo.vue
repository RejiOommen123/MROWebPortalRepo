<template>
  <div id="SensitiveInfoPageBox">
    <!-- Add Sensitive Info Button which will redirect to Add Sensitive Info Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="12" md="6" >
        <v-btn
          depressed
          small
          class="mx-2 addButton"
          fab
          dark
          color="rgb(0, 91, 168)"
          to="/SensitiveInfo/AddSensitiveInfo"
        >
          <v-icon>mdi-plus</v-icon>
        </v-btn>
        <span id="addRecBtn">Add Sensitive Info</span>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Sensitive Info List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Sensitive Info List
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Sensitive Info List DataTable  -->
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
        <!-- Sensitive Info List Actions (Edit Sensitive Info, Delete Sensitive Info)  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                :to="'/SensitiveInfo/EditSensitiveInfo/'+item.nSensitiveInfoID"
              >
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Sensitive Info</span>
          </v-tooltip>
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn icon @click="deleteItem(item.nSensitiveInfoID, item.sSensitiveInfoName)">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-delete</v-icon>
              </v-btn>
            </template>
            <span>Delete Sensitive Info</span>
          </v-tooltip>
        </template>
      </v-data-table>
      <!-- End Sensitive Info List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Delete Sensitive Info Confirmation -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />delete?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="deleteSensitiveInfo(editedItem.nSensitiveInfoID)">Yes</v-btn>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog Alert for errors Sensitive Info -->
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
          value: "sSensitiveInfoName",
          width: "20%",
        },
        { text: "Tooltip", value: "sFieldToolTip", width: "40%" },
        {
          text: "Normalized Name",
          value: "sNormalizedSensitiveInfoName",
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
        nSensitiveInfoID: 0,
        sSensitiveInfoName: "",
      },
    };
  },
  methods: {
    // API Call to Get all Sensitive Info
    getGridData() {
      this.dialogLoader = true;
      this.$http.get("Master/GetSensitiveInfo").then(
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
    //Method to pass nSensitiveInfoID & sSensitiveInfoName to dialog box
    deleteItem(nSensitiveInfoID, sSensitiveInfoName) {
      this.editedItem.nSensitiveInfoID = nSensitiveInfoID;
      this.editedItem.sSensitiveInfoName = sSensitiveInfoName;
      this.dialog = true;
    },
    //On Agree in dialog box API Call to Delete Sensitive Info
    deleteSensitiveInfo(id) {
      this.dialogLoader = true;
      this.dialog = false;
      this.$http
        .get(
          "Master/DeleteSensitiveInfo/sSensitiveInfoID=" +
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
  #SensitiveInfoPageBox {
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