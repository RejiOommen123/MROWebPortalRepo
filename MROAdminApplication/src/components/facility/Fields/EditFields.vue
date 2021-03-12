<template>
  <div >
    <form @submit.prevent="onSubmit">
      <v-row>
        <v-col cols="12">
          <v-card>
            <v-card-title>
              <v-row>
                <v-col cols="12" md="6">
                  Edit Fields For Facility - {{titleName}}
                </v-col>
                <v-col cols="12" md="2">
                  <v-select 
                    label="Field Type:"
                    v-model="select" 
                    :items="items" 
                  ></v-select>  
                </v-col>
                <v-col cols="12" md="4"> 
                  <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="Search"
                    single-line
                    hide-details
                  ></v-text-field>               
                </v-col>
              </v-row>
            </v-card-title>
            <!-- Facility List DataTable  -->
            <v-data-table
              :headers="headers"
              :items="filteredItems"
              :search="search"
              :footer-props="{
                'items-per-page-options': [10,25,50,100]
              }"
              :items-per-page="10"
              fixed-header
              height="55vh"
            >
              <template v-slot:item.sFieldName="props">
                <v-edit-dialog
                  v-if="props.item.sTableName!='lnkFacilityFieldMaps'"
                  :return-value.sync="props.item.sFieldName"
                  @save="pushToArray(props.item)"
                >
                  {{ props.item.sFieldName }}
                  <template v-slot:input>
                    <v-text-field v-model="props.item.sFieldName" label="Edit" counter :maxlength="props.item.sTableName!='lnkFacilityWaiver' ? 100 : 500"></v-text-field>
                  </template>
                </v-edit-dialog>
                <label v-else>{{props.item.sFieldName}}</label>
              </template>
              <!-- Template for Field Order -->
              <template v-slot:item.nFieldOrder="props">
                <v-edit-dialog
                  v-if="props.item.sTableName!='lnkFacilityFieldMaps'"
                  :return-value.sync="props.item.nFieldOrder"
                  @save="pushToArray(props.item)"
                >
                  {{ props.item.nFieldOrder }}
                  <template v-slot:input>
                    <v-text-field
                      type="number"
                      min="1"
                      v-model.number="props.item.nFieldOrder"
                      v-show="props.item.nFieldOrder!=null"
                    ></v-text-field>
                  </template>
                </v-edit-dialog>
                <label id="nFieldOrderLabel" v-if="props.item.nFieldOrder==null">
                  <strong>-</strong>
                </label>
              </template>
              <template v-slot:item.actions="props">
                <v-checkbox
                  hide-details
                  style="margin-top:0px"
                  :return-value.sync="props.item.bShow"
                  @change="pushToArray(props.item)"
                  v-model="props.item.bShow"
                ></v-checkbox>
              </template>
            </v-data-table>
            <!-- End Facility List DataTable  -->
          </v-card>
        </v-col>
      </v-row>
      <div class="submit">
        <v-btn type="submit" :disabled="updatedArray.length==0" color="primary">Save</v-btn>
        <v-btn type="button" to="/facility" color="primary">Cancel</v-btn>
      </div>
      <br />
      <!-- Common Loader -->
      <v-dialog v-model="dialogLoader" persistent width="300">
        <v-card color="rgb(0, 91, 168)" dark>
          <v-card-text>
            Please stand by
            <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
          </v-card-text>
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
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialogLoader: false,
      search: "",
      headers: [
        {
          text: "Type",
          align: "start",
          value: "sType",
          width: "20%",
          searchable:true
        },
        {
          text: "Name",
          align: "start",
          value: "sFieldName",
          width: "50%",
        },
        { text: "Order", value: "nFieldOrder", width: "20%" },
        { text: "Action", value: "actions", width: "10%", sortable: false },
      ],
      gridData: this.getGridData(),
      updatedArray: [],
      titleName: "",
      errorAlert:false,
      errorMessage:'',
      select: "--Select--",
        items: [
          '--Select--',
          'Field',
          'Patient Representative',          
          'Primary Reason',
          'Record Type',
          'Sensitive Info',
          'Shipment Type',
          'Waiver'
        ],
    };
  },
  computed: {
    filteredItems() {
      if(this.select=='--Select--'){
        return this.gridData;
      }
      else{
      return this.gridData.filter((i) => {
        return i.sType === this.select;
      })
      }
    }
  },
  methods: {
    // API call to Get all Fields Maps
    getGridData() {
      this.dialogLoader = true;
      this.$http
        .get(
          "facilityfieldmaps/GetFieldsByFacilityID/nFacilityID=" +
            this.$route.params.id +
            "&nFacilityLocationID=0" +
            "&nAdminUserID=" +
            this.$store.state.adminUserId
        )
        .then(
          (response) => {
            // get body data
            this.gridData = JSON.parse(response.bodyText)["fields"];
            this.titleName = JSON.parse(response.bodyText)["titleName"];
            this.dialogLoader = false;
          },
          (error) => {
            // error callback
            this.gridData = error.body;
            this.dialogLoader = false;
          }
        );
    },
    pushToArray(obj) {
      const index = this.updatedArray.findIndex(
        (e) => e.nFacilityFieldMapID === obj.nFacilityFieldMapID && e.sTableName === obj.sTableName
      );
      if (index === -1) {
        this.updatedArray.push(obj);
      } else {
        this.updatedArray[index] = obj;
      }
    },
    checkPatientRepValid(){
      var status = (item) => item.sTableName==='lnkFacilityPatientRepresentatives'&&item.bShow===true;
      this.errorMessage='Atleast one patient representative option must be active.'
      this.errorAlert= !this.gridData.some(status);
      this.dialogLoader=false;
      return !this.errorAlert;
    },
    onSubmit() {
      this.dialogLoader = true;
      var nAdminUserID = this.$store.state.adminUserId;
      var PatientRepValid=this.checkPatientRepValid();    
      if(PatientRepValid!=false){
      var FacilityFieldMapsList = this.updatedArray.map(function (item) {
        item["nUpdatedAdminUserID"] = nAdminUserID;
        return item;
      });
      var editFields = {
          fieldFacilityMapsTable : FacilityFieldMapsList,
          nFacilityLocationID : 0
      }
      this.$http
        .post("facilityfieldmaps/EditFacilityFields/", editFields)
        .then((response) => {
          if (response.ok == true) {
            this.gridData = [];
            this.getGridData();
            this.updatedArray = [];
            this.dialogLoader = false;
          }
        });
      }
    },
  },
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditFieldsBox {
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
#nFieldOrderLabel {
  font-size: 1.125em;
}
#demo {
  margin: 1.25em;
}
button {
  margin-right: 1.25em;
}
.submit {
  text-align: center;
}
</style>