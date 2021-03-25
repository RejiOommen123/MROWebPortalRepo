<template>
  <div id="FacilityConfigurationReportPageBox">
    <v-card class="mb-5 pa-3">
      <v-card-title>
        Filters
      </v-card-title>
      <v-row no-gutters>
        <v-col cols="6" sm="12" md="2" class="pl-4">
          <v-text-field
            label="Facility Name"
            v-model="sFacilityName"
            @input="$v.sFacilityName.$touch()"
            @blur="$v.sFacilityName.$touch()"
            :error-messages="sFacilityNameErrors"
            maxlength="40"
          ></v-text-field>
        </v-col>
        <v-col cols="6" sm="12" md="2" class="pl-4">
          <v-text-field 
          label="eXpress Name"
          v-model="sWizardName"
          @input="$v.sWizardName.$touch()"
          @blur="$v.sWizardName.$touch()"
          :error-messages="sWizardNameErrors"
          maxlength="50"
          ></v-text-field>
        </v-col>
        <v-col cols="6" sm="12" md="2" class="pl-4">
          <v-btn color="primary" style="margin-top: 5%;"  @click="getGridData()">Apply Filter</v-btn>
        </v-col>
      </v-row>
    </v-card>
    <!-- Vuetify Card with Facility Configuration Report List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Facility Configuration Report -
        <v-btn class="ml-3" color="primary" small>
        <export-excel
            :data="gridData"
            :fields ="gridHeaders"
            type="xls"
            name="FacilityConfiguration Report.xls"
        >
            Export To Excel
        </export-excel>
        </v-btn>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
      </v-card-title>
      <!-- Facility Configuration Report DataTable  -->
      <v-data-table
        :headers="headers"
        :items="gridData"
        :search="search"
        :footer-props="{
          'items-per-page-options': [10,25,50,100]
        }"
        :items-per-page="5"
        fixed-header
        class="body-1"        
        height="50vh"
        :page.sync="pageNo"
      >
      </v-data-table>
      <!-- End Facility Configuration Report DataTable  -->
    </v-card>

    <!-- Dialog Alert for errors Facility Configuration Report -->
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
import { maxLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      sFacilityName: "",
      sWizardName: "",
      pageNo:1,
      dialogLoader: false,
      dialog: false,
      errorAlert: false,
      errorMessage: "",
      search: "",
      headers: [
       {
          text: "Facility",
          align: "start",
          value: "sFacilityName",
          width: "150px",
        },
        { text: "eXpress Name", value: "sWizardName", width: "150px"},   
        { text: "eXpress Description", value: "sWizardDescription", width: "175px" },
        { text: "Value", value: "sFieldName", width: "300px" },       
        { text: "Type", value: "sType", width: "150px" },    
        { text: "Configurable in Admin Module", value: "bConfigurableAdmin", width: "250px" },
        { text: "Configurable in DB", value: "bConfigurableDB", width: "175px" },       
        { text: "In Use", value: "bInUse", width: "100px"}
      ],
      gridData: undefined,
       gridHeaders :{
        'Facility' : 'sFacilityName',
        'eXpress Name':'sWizardName',
        'eXpress Description': 'sWizardDescription',
        'Value':'sFieldName',
        'Type': 'sType',
        'Configurable in Admin Module':'bConfigurableAdmin',
        'Configurable in DB': 'bConfigurableDB',
        "In Use": 'bInUse'
      },
    };
  },
  validations: {
    sFacilityName: {
        maxLength: maxLength(40)
    },
    sWizardName: {
        maxLength: maxLength(50)
    }
  },
  computed: {
    sFacilityNameErrors() {
      const errors = [];
      if (!this.$v.sFacilityName.$dirty) return errors;
      !this.$v.sFacilityName.maxLength &&
        errors.push("Facility Name must be at most 40 characters long");
      return errors;
    },
    sWizardNameErrors() {
      const errors = [];
      if (!this.$v.sWizardName.$dirty) return errors;
      !this.$v.sWizardName.maxLength &&
        errors.push("Wizard Name must be at most 50 characters long");
      return errors;
    }
  },
  mounted(){
    this.getGridData();
  },
  methods: {
    // API Call to Get all Facility Configuration Report
    getGridData() {
      var adminFilterParameter ={
        sFacilityName:this.sFacilityName,
        sWizardName:this.sWizardName
      };
      this.dialogLoader = true;
      this.$http.post("Report/GetFacilityConfigurationReport",adminFilterParameter).then(
        (response) => {
          this.pageNo=1;
          this.search="";
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
  #FacilityConfigurationReportPageBox {
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