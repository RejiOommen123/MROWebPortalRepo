<template>
  <div id="EditDisclaimersBox">
    <form @submit.prevent="onSubmit">
      <v-row>
        <v-col cols="12">
          <v-card>
            <v-card-title>
              Edit Disclaimers For Facility - {{facilityName}}
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
          text: "Name",
          align: "start",
          value: "sWizardHelperValue",
          width: "60%",
        },
        { text: "Wizard Name", value: "sWizardDescription", width: "40%" }
      ],
      gridData: this.getGridData(),
      updatedArray: [],
      facilityName: "",
    };
  },
  mounted() {},
  methods: {
    // API call to Get all Fields Maps
    getGridData() {
      this.dialogLoader = true;
      this.$http
        .get(
          "facilitydisclaimers/GetDisclaimersByFacilityID/nFacilityID=" +
            this.$route.params.id +
            "&nAdminUserID=" +
            this.$store.state.adminUserId
        )
        .then(
          (response) => {
            // get body data
            this.gridData = JSON.parse(response.bodyText)["disclaimers"];
            this.facilityName = JSON.parse(response.bodyText)["faciName"];
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
      this.$http
        .post("facilitydisclaimers/EditFacilityDisclaimers/", FacilityDisclaimersList)
        .then((response) => {
          if (response.ok == true) {
            this.dialogLoader = false;
            this.$router.push("/facility");
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