<template>
  <div id="EditDisclaimersBox">
    <form @submit.prevent="onSubmit">
      <v-row>
        <v-col cols="12">
          <v-card>
            <v-card-title>
              Edit Disclaimers For Location - {{ titleName }}
              <v-spacer></v-spacer>
              <v-text-field
                v-model="search"
                append-icon="mdi-magnify"
                label="Search"
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>
            <!-- Disclaimers List DataTable  -->
            <v-data-table
              :headers="headers"
              :items="gridData"
              :search="search"
              :footer-props="{
                'items-per-page-options': [10,25,50,100]
              }"
              :items-per-page="10"
              fixed-header
              height="55vh"
            >

              <template v-slot:item.bIsLocationLevel="{ item }">
                <div class="d-flex justify-center">
                  <v-switch
                    :disabled ="!item.bIsLocationLevel"
                    v-model="item.bIsLocationLevel"
                    color="#1AA260"
                    @click="setResetDialog(item.nFacilityWizardHelperID)"
                  ></v-switch>
                </div>
              </template>

              <template v-slot:item.sWizardHelperValue="props">
                <v-edit-dialog
                  :return-value.sync="props.item.sWizardHelperValue"
                  @save="pushToArray(props.item)"
                >
                  {{ props.item.sWizardHelperValue }}
                  <template v-slot:input>
                    <v-text-field v-model="props.item.sWizardHelperValue" label="Edit" counter maxlength="1000"></v-text-field>
                  </template>
                </v-edit-dialog>
              </template>
              <!-- Template for Field Order -->
              <!-- <template v-slot:item.nFieldOrder="props">
                  {{ props.item.nFieldOrder }}
              </template> -->
            </v-data-table>
            <!-- End Disclaimers List DataTable  -->
          </v-card>
        </v-col>
      </v-row>
      <div class="submit">
        <v-btn type="submit" :disabled="updatedArray.length==0" color="primary">Save</v-btn>
        <v-btn type="button" :to="'/Locations/'+$route.params.nFacilityID" color="primary">Cancel</v-btn>
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
      <v-dialog v-model="dialog" max-width="360">
        <v-card>
          <v-card-title class="headline">
            Are you sure you want to
            <br />reset to default?
          </v-card-title>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="green darken-1" text @click="dialog = false"
              >No</v-btn
            >
            <v-btn
              color="red darken-1"
              text
              @click="resetToDefault(resetItemId)"
              >Yes</v-btn
            >
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
      dialog: false,
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "sWizardHelperValue",
          width: "60%",
        },
        { text: "Wizard Name", value: "sWizardDescription", width: "25%" },
        { text: "Configured at Location", value: "bIsLocationLevel", align: "center"},
      ],
      gridData: this.getGridData(),
      updatedArray: [],
      titleName: "",
      resetItemId: 0
    };
  },
  mounted() {},
  methods: {
    // API call to Get all Fields Maps
    getGridData() {
      console.log("facility(disc) - "+this.$route.params.nFacilityID);
      console.log("location(disc) - "+this.$route.params.nFacilityLocationID);
      this.dialogLoader = true;
      this.$http
        .get(
          "facilitydisclaimers/GetDisclaimersByFacilityID/nFacilityID=" +
            this.$route.params.nFacilityID +
            "&nFacilityLocationID=" +
            this.$route.params.nFacilityLocationID+
            "&nAdminUserID=" +
            this.$store.state.adminUserId
        )
        .then(
          (response) => {
            // get body data
            this.gridData = JSON.parse(response.bodyText)["disclaimers"];
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
    setResetDialog(nFacilityWizardHelperID) {
      this.resetItemId = nFacilityWizardHelperID;
      this.dialog = true;
    },
    resetToDefault(id) {
      const index = this.gridData.findIndex(
        (element) => element.nFacilityWizardHelperID == id
      );
      this.updatedArray = this.updatedArray.filter(
        (element) => !(element.nFacilityWizardHelperID == id)
      );
      var itemToReset = this.gridData[index];
      itemToReset.nUpdatedAdminUserID = this.$store.state.adminUserId;
      //delete itemToReset.bIsLocationLevel;
      console.log(itemToReset);
      this.dialogLoader = true;
      this.dialog = false;
      this.$http
        .post("facilitydisclaimers/ResetToDefault/", itemToReset)
        .then((response) => {
          if (response.ok == true) {
            this.dialogLoader = false;
            var newObj = JSON.parse(response.bodyText);
            this.gridData[index].nFacilityWizardHelperID = newObj.nFacilityWizardHelperID;
            this.gridData[index].nFacilityID = newObj.nFacilityID;
            this.gridData[index].nFacilityLocationID = newObj.nFacilityLocationID;
            this.gridData[index].sWizardHelperValue = newObj.sWizardHelperValue;
            this.gridData[index].sWizardHelperType = newObj.sWizardHelperType;
            this.gridData[index].nWizardID = newObj.nWizardID;
            this.gridData[index].dtCreated = newObj.dtCreated;
            this.gridData[index].nCreatedAdminUserID = newObj.nCreatedAdminUserID;        
            this.gridData[index].nUpdatedAdminUserID = newObj.nUpdatedAdminUserID;
            this.gridData[index].dtLastUpdate = newObj.dtLastUpdate;
            this.gridData[index].bIsLocationLevel = false;     
          }
        });
    },
    pushToArray(obj) {
      const index = this.updatedArray.findIndex(
        (e) => e.nFacilityWizardHelperID === obj.nFacilityWizardHelperID
      );
      if (index === -1) {
        this.updatedArray.push(obj);
      } else {
        this.updatedArray[index] = obj;
      }
    },    
    onSubmit() {
      this.dialogLoader = true;
      var nAdminUserID = this.$store.state.adminUserId;
      var FacilityDisclaimersList = this.updatedArray.map(function (item) {
        item["nUpdatedAdminUserID"] = nAdminUserID;
        return item;
      });
      var editDisclaimers = {
          facilityDisclaimers : FacilityDisclaimersList,
          nFacilityLocationID : this.$route.params.nFacilityLocationID
        }
      this.$http
        .post("facilitydisclaimers/EditFacilityDisclaimers/", editDisclaimers)
        .then((response) => {
          if (response.ok == true) {

            this.gridData = [];
            this.getGridData();
            this.updatedArray = [];
            this.resetItemId = 0;

            this.dialogLoader = false;
          }
        });
    },
  },
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditDisclaimersBox {
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