<template>
  <div id="AuditReportPageBox">
    <v-card class="mb-5 pa-3">
      <v-row no-gutters>
        <v-col cols="12" sm="12" md="1" class="ml-4">
          <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field
                transition="scale-transition"
                offset-y
                :value="dtStartFormatted"
                :error-messages="dtStartErrors"
                clearable
                label="START DATE"
                placeholder="MM-DD-YYYY"
                readonly
                v-bind="attrs"
                v-on="on"
                @click:clear="dtStart = null"
                @input="$v.dtStart.$touch()"
                @blur="$v.dtStart.$touch()"
              ></v-text-field>
            </template>
            <v-date-picker
              ref="startpicker"
              v-model="dtStart"
              color="green lighten-1"
              header-color="primary"
              light
              @change="menu1 = false"
              :max="new Date().toISOString().substr(0, 10)"
            ></v-date-picker>
          </v-menu>
        </v-col>
        <!-- End Date input -->
        <v-col cols="12" sm="12" md="1" class="ml-4">
          <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field
                transition="scale-transition"
                offset-y
                :value="dtEndFormatted"
                :error-messages="dtEndErrors"
                clearable
                label="END DATE"
                placeholder="MM-DD-YYYY"
                readonly
                v-bind="attrs"
                v-on="on"
                @click:clear="dtEnd = null"
                @input="$v.dtEnd.$touch()"
                @blur="$v.dtEnd.$touch()"
              ></v-text-field>
            </template>
            <v-date-picker
              ref="endpicker"
              v-model="dtEnd"
              color="green lighten-1"
              header-color="primary"
              light
              @change="menu2 = false"
              :max="new Date().toISOString().substr(0, 10)"
            ></v-date-picker>
          </v-menu>
        </v-col>
        <v-col cols="12" sm="12" md="2" class="ml-4">
          <v-text-field
            label="Facility Name"
            v-model="sFacilityName"
            @input="$v.sFacilityName.$touch()"
            @blur="$v.sFacilityName.$touch()"
            :error-messages="sFacilityNameErrors"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="2" class="ml-4">
          <v-text-field 
          label="Location Name"
          v-model="sLocationName"
          @input="$v.sLocationName.$touch()"
          @blur="$v.sLocationName.$touch()"
          :error-messages="sLocationNameErrors"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="2" class="ml-4">
          <v-text-field 
          label="Admin Name"
          v-model="sAdminName"
          @input="$v.sAdminName.$touch()"
          @blur="$v.sAdminName.$touch()"
          :error-messages="sAdminNameErrors"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="2" class="ml-4">
          <v-btn color="primary" style="margin-top: 5%;">Filter</v-btn>
        </v-col>
      </v-row>
    </v-card>
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
          'items-per-page-options': [5,10]
        }"
        :items-per-page="5"
        class="body-1"
      >
        <!-- Primary Reason List Actions (Edit Primary Reason, Delete Primary Reason)  -->
        <template v-slot:item.actions="{ item }">
          <v-tooltip top>
            <template v-slot:activator="{ on }">
              <router-link
                class="mrorouterlink"
                :to="'/PrimaryReason/EditPrimaryReason/'+item.nPrimaryReasonID"
              >
                <v-icon color="rgb(0, 91, 168)" v-on="on" medium>mdi-pencil</v-icon>
              </router-link>
            </template>
            <span>Edit Primary Reason</span>
          </v-tooltip>
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
          <v-btn
            color="red darken-1"
            text
            @click="deletePrimaryReason(editedItem.nPrimaryReasonID)"
          >Yes</v-btn>
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
import moment from "moment";
import { required, maxLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      dtStart: moment()
        .add(-10, "days")
        .toISOString()
        .substr(0, 10),
      dtEnd: moment()
        .toISOString()
        .substr(0, 10),
      sFacilityName: "",
      sLocationName: "",
      sAdminName: "",
      menu1: false,
      menu2: false,
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
          text: "Edit/Delete",
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
  watch: {
    menu1(val) {
      val && setTimeout(() => (this.$refs.startpicker.activePicker = "YEAR"));
    },
    menu2(val) {
      val && setTimeout(() => (this.$refs.endpicker.activePicker = "YEAR"));
    },
  },
  // Start and end date validations
  validations: {
    dtStart: {
      required,
      minValue: (value) => value < new Date().toISOString(),
    },
    dtEnd: {
      required,
      minValue: (value) => value < new Date().toISOString(),
      afterStartDate: (value, vm) =>
        new Date(vm.dtEnd).getTime() >= new Date(vm.dtStart).getTime(),
    },
    sFacilityName: {
        maxLength: maxLength(40)
    },
    sLocationName: {
        maxLength: maxLength(100)
    },
    sAdminName: {
        maxLength: maxLength(50)
    },
  },
  computed: {
    //Date Format setter
    dtStartFormatted() {
      return this.dtStart ? moment(this.dtStart).format("MM-DD-YYYY") : "";
    },
    dtEndFormatted() {
      return this.dtEnd ? moment(this.dtEnd).format("MM-DD-YYYY") : "";
    },
    //Start and End Date validation error message setter
    dtStartErrors() {
      const errors = [];
      if (!this.$v.dtStart.$dirty) return errors;
      !this.$v.dtStart.required && errors.push("Start Date is required");
      !this.$v.dtStart.minValue &&
        errors.push("This date cannot be future date");
      return errors;
    },
    dtEndErrors() {
      const errors = [];
      if (!this.$v.dtEnd.$dirty) return errors;
      !this.$v.dtEnd.required && errors.push("End Date is required");
      !this.$v.dtEnd.minValue && errors.push("This date cannot be future date");
      !this.$v.dtEnd.afterStartDate &&
        errors.push("End Date cannot be before Start Date.");
      return errors;
    },
    sFacilityNameErrors() {
      const errors = [];
      if (!this.$v.sFacilityName.$dirty) return errors;
      !this.$v.sFacilityName.maxLength &&
        errors.push("Facility Name must be at most 40 characters long");
      return errors;
    },
    sLocationNameErrors() {
      const errors = [];
      if (!this.$v.sLocationName.$dirty) return errors;
      !this.$v.sLocationName.maxLength &&
        errors.push("Location Name must be at most 100 characters long");
      return errors;
    },
    sAdminNameErrors() {
      const errors = [];
      if (!this.$v.sAdminName.$dirty) return errors;
      !this.$v.sAdminName.maxLength &&
        errors.push("Admin Name must be at most 50 characters long");
      return errors;
    },
  },
  methods: {
    // API Call to Get all Primary Reasons
    getGridData() {
      var adminFilterParameter ={
        dtStart:this.dtStart,
        dtEnd:this.dtEnd,
        sFacilityName:this.sFacilityName,
        sLocationName:this.sLocationName,
        sAdminName:this.sAdminName
      };
      this.dialogLoader = true;
      this.$http.post("Report/GetAuditReport",adminFilterParameter).then(
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
    }
  },
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #AuditReportPageBox {
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