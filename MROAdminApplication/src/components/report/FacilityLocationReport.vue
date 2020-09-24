<template>
  <div id="FacilityLocationReportPageBox">
    <v-card class="mb-5 pa-3">
      <v-card-title>
        Filters
      </v-card-title>
      <v-row no-gutters>
        <v-col cols="6" sm="12" md="2" class="pl-4">
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
        <v-col cols="6" sm="12" md="2" class="pl-4">
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
          label="Location Name"
          v-model="sLocationName"
          @input="$v.sLocationName.$touch()"
          @blur="$v.sLocationName.$touch()"
          :error-messages="sLocationNameErrors"
          maxlength="100"
          ></v-text-field>
        </v-col>
        <v-col cols="6" sm="12" md="2" class="pl-4">
          <v-btn v-if="dtStart!=null || dtEnd!=null" color="primary" style="margin-top: 5%;" :disabled="$v.$invalid" @click="getGridData()">Filter</v-btn>
          <v-btn v-else color="primary" style="margin-top: 5%;"  @click="getGridData()">Filter</v-btn>
        </v-col>
      </v-row>
    </v-card>
    <!-- Vuetify Card with Facility Location Report List Title and Search Text Box  -->
    <v-card>
      <v-card-title>
        Facility/Location Report -
        <v-btn class="ml-3" color="primary" small>
        <export-excel
            :data="gridData"
            :fields ="gridHeaders"
            type="xls"
            name="Facility_Location Report.xls"
        >
            Export Excel
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
      <!-- Facility/Location Report DataTable  -->
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
      >
      </v-data-table>
      <!-- End Facility/Location Report DataTable  -->
    </v-card>

    <!-- Dialog Alert for errors Facility/Location Report -->
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
import { maxLength } from "vuelidate/lib/validators";
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
      menu1: false,
      menu2: false,
      dialogLoader: false,
      dialog: false,
      errorAlert: false,
      errorMessage: "",
      search: "",
      headers: [
        {
          text: "Facility Name",
          align: "start",
          value: "sFacilityName",
          width: "150px",
        },
        { text: "Location Name", value: "sLocationName", width: "150px" },
        { text: "Active(Location)", value: "bLocationActiveStatus", width: "150px" },       
        { text: "ROI Location ID", value: "nROIFacilityID", width: "150px" },    
        { text: "Location Code", value: "sLocationCode", width: "150px" },
        { text: "Location Address", value: "sLocationAddress", width: "300px" },       
        { text: "Ph Number", value: "sPhoneNo", width: "150px"},    
        { text: "Fax Number", value: "sFaxNo", width: "150px"},   
        { text: "Authorization Template", value: "bAuthTemplate", width: "200px" },   
        { text: "Logo", value: "bLogo", width: "150px" },   
        { text: "Background", value: "bBackground", width: "150px" },   
        { text: "Active (Facility Active)", value: "bActiveStatus", width:"200px" },   
        { text: "Description", value: "sDescription", width: "300px" },   
        { text: "SMTP Username", value: "sSMTPUsername", width: "200px" },   
        { text: "SMTP Url", value: "sSMTPUrl", width: "200px" },  
        { text: "Outbound Email", value: "sOutboundEmail", width: "200px" },  
        { text: "sFTP Username", value: "sFTPUsername", width: "200px" },  
        { text: "sFTP URL", value: "sFTPUrl", width: "200px" },  
        { text: "GUID for URL", value: "sGuid", width: "400px" }
      ],
      gridData: undefined,
       gridHeaders :{
        'Facility Name':'sFacilityName',
        'Location Name': 'sLocationName',
        'Active(Location)':'bLocationActiveStatus',
        'ROI Location ID': 'nROIFacilityID',
        'Location Code':'sLocationCode',
        'Location Address': 'sLocationAddress',
        'Ph Number' : 'sPhoneNo',
        "Fax Number": 'sFaxNo', 
        'Authorization Template': 'bAuthTemplate',
        'Logo': 'bLogo', 
        'Background': 'bBackground' ,
        'Active (Facility Active)': 'bActiveStatus',
        'Description': 'sDescription' ,  
        'SMTP Username': 'sSMTPUsername',
        'SMTP Url': 'sSMTPUrl',
        'Outbound Email': 'sOutboundEmail',
        'sFTP Username': 'sFTPUsername',
        'sFTP URL': 'sFTPUrl',
        'GUID for URL': 'sGuid'
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
      minValue: (value) => value < new Date().toISOString(),
      endButNotStartDate :(value, vm) =>{
        if(vm.dtEnd!=null && vm.dtStart==null){
          return false;
        }
        else{
          return true;
        }
      }
    },
    dtEnd: {
      minValue: (value) => value < new Date().toISOString(),
      afterStartDate: (value, vm) =>
        new Date(vm.dtEnd).getTime() >= new Date(vm.dtStart).getTime(),
      startButNotEndDate :(value, vm) =>{
        if(vm.dtStart!=null && vm.dtEnd==null){
          return false;
        }
        else{
          return true;
        }
      }
    },
    sFacilityName: {
        maxLength: maxLength(40)
    },
    sLocationName: {
        maxLength: maxLength(100)
    }
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
      !this.$v.dtStart.endButNotStartDate &&
        errors.push("Start Date cannot be empty when End Date is provided.");
      return errors;
    },
    dtEndErrors() {
      const errors = [];
      if (!this.$v.dtEnd.$dirty) return errors;
      !this.$v.dtEnd.startButNotEndDate &&
        errors.push("End Date cannot be empty when Start Date is provided.");
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
    }
  },
  mounted(){
    this.getGridData();
  },
  methods: {
    // API Call to Get all Facility/Location Report
    getGridData() {
      var adminFilterParameter ={
        sStart:this.dtStart,
        sEnd:this.dtEnd,
        sFacilityName:this.sFacilityName,
        sLocationName:this.sLocationName
      };
      this.dialogLoader = true;
      this.$http.post("Report/GetFacilityLocationReport",adminFilterParameter).then(
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
  #FacilityLocationReportPageBox {
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