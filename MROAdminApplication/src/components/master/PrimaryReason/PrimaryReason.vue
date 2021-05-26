<template>
  <div id="PrimaryReasonPageBox">
    <!-- Add Primary Reason Button which will redirect to Add Primary Reason Page -->
    <v-row no-gutters>
      <v-col cols="12" sm="12" md="6">
        <v-btn
          depressed
          small
          class="mx-2 addButton"
          fab
          dark
          color="rgb(0, 91, 168)"       
          to="/PrimaryReason/AddPrimaryReason"
        >
          <v-icon>mdi-plus</v-icon>
        </v-btn>
        <span id="addRecBtn">Add Primary Reason</span>
      </v-col>
    </v-row>
    <!-- Vuetify Card with Primary Reason List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Primary Reason List
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Primary Reason List DataTable  -->
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
        <!-- Primary Reason List Actions (Edit Primary Reason, Delete Primary Reason)  -->
        <template v-slot:item.actions="{ item }">
          <!-- <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                :to="'/PrimaryReason/EditPrimaryReason/'+item.nPrimaryReasonID"
              >
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Primary Reason</span>
          </v-tooltip> -->
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <v-btn icon @click="deleteItem(item.nPrimaryReasonID, item.sPrimaryReasonName)">
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-delete</v-icon>
              </v-btn>
            </template>
            <span>Delete Primary Reason</span>
          </v-tooltip>
        </template>
      </v-data-table>
      <!-- End Primary Reason List DataTable  -->
    </v-card>

    <!-- Dialog box for Toggle Delete Primary Reason Confirmation -->
    <v-dialog v-model="dialog" max-width="360">
      <v-card>
        <v-card-title class="headline">
          Are you sure you want to
          <br />delete?
        </v-card-title>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="red darken-1" text @click="deletePrimaryReason(editedItem.nPrimaryReasonID)">Yes</v-btn>
          <v-btn color="green darken-1" text @click="dialog = false">No</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Dialog Alert for errors Primary Reason -->
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
          value: "sPrimaryReasonName",
          width: "20%",
        },
        { text: "Tooltip", value: "sFieldToolTip", width: "40%" },
        {
          text: "Normalized Name",
          value: "sNormalizedPrimaryReasonName",
          width: "30%",
        },
        {
          text: "Delete",
          value: "actions",
          align: "center",
          sortable: false,
        },
      ],
      gridData: this.getGridData(),
      editedItem: {
        nPrimaryReasonID: 0,
        sPrimaryReasonName: "",
      },
    };
  },
  methods: {
    // API Call to Get all Primary Reasons
    getGridData() {
      this.dialogLoader = true;
      this.$http.get("Master/GetPrimaryReasons").then(
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
    //Method to pass nPrimaryReasonID & sPrimaryReasonName to dialog box
    deleteItem(nPrimaryReasonID, sPrimaryReasonName) {
      this.editedItem.nPrimaryReasonID = nPrimaryReasonID;
      this.editedItem.sPrimaryReasonName = sPrimaryReasonName;
      this.dialog = true;
    },
    //On Agree in dialog box API Call to Delete Primary Reason
    deletePrimaryReason(id) {
      this.dialogLoader = true;
      this.dialog = false;
      this.$http
        .get(
          "Master/DeletePrimaryReason/sPrimaryReasonID=" +
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
  #PrimaryReasonPageBox {
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