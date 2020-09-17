<template>
  <div >
    <form @submit.prevent="onSubmit">
      <v-row>
        <v-col cols="12">
          <v-card>
            <v-card-title>
              Edit Fields For Facility - {{facilityName}}
              <v-spacer></v-spacer>
              <v-text-field
                v-model="search"
                append-icon="mdi-magnify"
                label="Search"
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>
            <!-- Facility List DataTable  -->
            <v-data-table
              :headers="headers"
              :items="gridData"
              :search="search"
              :footer-props="{
                'items-per-page-options': [5,10,20]
              }"
              :items-per-page="10"
            >
              <template v-slot:item.sFieldName="props">
                <v-edit-dialog
                  v-if="props.item.sTableName!='lnkFacilityFieldMaps'"
                  persistent
                  :return-value.sync="props.item.sFieldName"
                  @save="pushToArray(props.item)"
                >
                  {{ props.item.sFieldName }}
                  <template v-slot:input>
                    <v-text-field v-model="props.item.sFieldName" label="Edit" counter maxlength="100"></v-text-field>
                  </template>
                </v-edit-dialog>
                <label v-else>{{props.item.sFieldName}}</label>
              </template>
              <!-- Template for Field Order -->
              <template v-slot:item.nFieldOrder="props">
                <v-edit-dialog
                  v-if="props.item.sTableName!='lnkFacilityFieldMaps'"
                  persistent
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
          value: "sFieldName",
          width: "60%",
        },
        { text: "Order", value: "nFieldOrder", width: "20%" },
        { text: "Action", value: "actions", width: "20%", sortable: false },
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
          "facilityfieldmaps/GetFieldsByFacilityID/nFacilityID=" +
            this.$route.params.id +
            "&nAdminUserID=" +
            this.$store.state.adminUserId
        )
        .then(
          (response) => {
            // get body data
            this.gridData = JSON.parse(response.bodyText)["fields"];
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
      console.log(this.updatedArray);
      const index = this.updatedArray.findIndex(
        (e) => e.nFacilityFieldMapID === obj.nFacilityFieldMapID && e.sTableName === obj.sTableName
      );
      if (index === -1) {
        this.updatedArray.push(obj);
      } else {
        this.updatedArray[index] = obj;
      }
    },
    onSubmit() {
      console.log(this.gridData);
      this.dialogLoader = true;
      var nAdminUserID = this.$store.state.adminUserId;
      var FacilityFieldMapsList = this.updatedArray.map(function (item) {
        item["nUpdatedAdminUserID"] = nAdminUserID;
        return item;
      });
      this.$http
        .post("facilityfieldmaps/EditFacilityFields/", FacilityFieldMapsList)
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