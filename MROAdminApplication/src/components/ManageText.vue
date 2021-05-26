<template>
  <div>
    <v-row  style="margin-left: 3%;margin-right: 3%;height: 100%;">
      <v-col md="8" sm="8">
        <v-card>
          <v-row style="margin-left: 3%">
            <v-col cols="2" sm="4" md="2" style="margin-left: 2%">
              <v-select
                v-model="selectFacility"
                :items="facilityData"
                item-text="sFacilityName"
                item-value="nFacilityID"
                label="Select Facility"
                @change="getOrgNlocationData()"
              ></v-select>
            </v-col>
            <v-col cols="2" sm="4" md="2">
              <v-select
                :disabled="selectFacility == null"
                v-model="selectOrganization"
                :items="orgData"
                item-text="sOrgName"
                item-value="nFacilityOrgID"
                label="Select Organization"
                @change="getLocationData()"
              ></v-select>
            </v-col>
            <v-col cols="2" sm="4" md="2">
              <v-select
                :disabled="selectFacility == null"
                v-model="selectLocation"
                :items="locationData"
                item-text="sLocationName"
                item-value="nFacilityLocationID"
                label="Select Location"
              ></v-select>
            </v-col>
          <!-- </v-row>
          <v-row  md="6" sm="6"> -->
            <v-col cols="2" md="2" sm="4" >
              <v-select
                v-model="selectScreen"
                :items="screenData"
                item-text="sWizardDescription"
                item-value="nWizardID"
                label="Select Screen"
              ></v-select>
            </v-col>
            <v-col cols="2" md="2" sm="4">
              <v-select
                v-model="selectLanguage"
                :items="languageData"
                item-text="text"
                item-value="value"
                label="Select Language"
              ></v-select>
            </v-col>
          </v-row>
          <v-row  md="12" sm="12">
            <v-col cols="12" md="12" sm="12">
              <v-data-table
                :headers="headers"
                :items="gridData"
                :footer-props="{
                  'items-per-page-options': [10, 25, 50, 100],
                }"
                :items-per-page="5"
                class="body-1"
                fixed-header
                height="60vh"
                group-by="sSort"
              >
                <template
                  v-slot:group.header="{ group, headers, toggle, isOpen }"
                >
                  <td
                    :colspan="headers.length"
                    class="GroupHeader"
                    style="border: 1px solid black"
                  >
                    <v-btn
                      @click="toggle"
                      small
                      icon
                      :ref="group"
                      :data-open="isOpen"
                    >
                      <v-icon v-if="isOpen">mdi-chevron-up</v-icon>
                      <v-icon v-else>mdi-chevron-down</v-icon>
                      {{ group }}
                    </v-btn>
                  </td>
                </template>

                <template v-slot:item.stext="props">
                  <v-edit-dialog
                    v-if="props.item.sTableName != 'gridData'"
                    :return-value.sync="props.item.stext"
                    @save="pushToArray(props.item)"
                  >
                    {{ props.item.stext }}
                    <template v-slot:input>
                      <v-text-field
                        v-model="props.item.stext"
                        label="Edit"
                        counter
                        :maxlength="(props.item.gridData = 100)"
                      ></v-text-field>
                    </template>
                  </v-edit-dialog>
                  <label v-else>{{ props.item.stext }}</label>
                </template>
                <template v-slot:item.bReset="props" >
                  <v-icon :disabled="props.item.sPlace=='G__'" style="color: red" @click= "Delete()"> mdi-close</v-icon>
                </template>
              </v-data-table>
            </v-col>
          </v-row>
        </v-card>
      </v-col>
      <!-- <v-divider vertical style="margin-left: 5%"> </v-divider> -->
      <v-col cols="4" md="4" sm="12" style="height: 100%;">
        <div v-if="selectScreen != null">
          <img :src="getImgUrl(selectScreen)" class="image" />
        </div>
      </v-col>
    </v-row>
  </div>
</template>
  <script>
export default {
  data() {
    return {
      dialogLoader: false,
      search: "",
      nFacilityID: "",
      nFacilityOrgID: "",
      nWizardID: "",
      headers: [
        {
          text: "sPlace",
          align: "start",
          value: "sPlace",
          width: "20%",
        },
        {
          text: "sTypeWithID",
          value: "sTypeWithID",
          width: "20%",
        },
        {
          text: "sText",
          value: "sText",
          width: "20%",
        },
        {
          text: "bReset",
          value: "bReset",
          width: "20%",
        },
      ],
      selectFacility: null,
      facilityData: [
        {
          sFacilityName: "-Select-",
          nFacilityID: null,
        },
      ],
      selectScreen: null,
      screenData: [
        {
          sWizardName: "-Select-",
          nWizardID: null,
        },
      ],
      selectOrganization: null,
      orgData: [
        {
          sOrgName: "-Select-",
          nFacilityOrgID: null,
          sLocationName: "-Select-",
          nFacilityLocationID: null,
        },
      ],
      selectLocation: null,
      locationData: [
        {
          sLocationName: "-Select-",
          nFacilityLocationID: null,
        },
      ],
      selectLanguage: {
        text: "English",
        value: 1,
      },
      languageData: [
        {
          text: "English",
          value: 1,
        },
      ],
      updatedArray: [],
      gridData: this.GetgridData(),
      // gridData: [
      //   {
      //     gfol: "",
      //     text: "",
      //     type: "",
      //     extra: "",
      //     button: "",
      //     groupBy: "",
      //   },
      // ],     
    };
  },
  mounted() {
    this.dialogLoader = true;
    this.$http.get("ManageText/GetFacilityNWizardData").then(
      (response) => {
        this.facilityData = [
          ...this.facilityData,
          ...JSON.parse(response.bodyText)["facilities"],
        ];
        this.screenData = JSON.parse(response.bodyText)["wizard"];
        if (this.screenData != null) {
          this.selectScreen = this.screenData[0].nWizardID;
        }
        this.dialogLoader = false;
      },

      (response) => {
        // Error Callback
        this.facilityData = response.body;
      }
    );   
  },

  methods: {
    GetgridData(){
      this.dialogLoader = true;
     var ManageTextFilterParam =
      {
      nFacilityID: this.nFacilityID,
      nFacilityOrgID: this.nFacilityOrgID,
      nFacilityLocationID: this.nFacilityLocationID,
      nWizardID: 3,
      nLanguageID: 1
   };

    this.$http.post("ManageText/GetManageTextData/", ManageTextFilterParam ).then(
      (response) => {
        this.gridData = JSON.parse(response.bodyText)["gridData"];      
        this.dialogLoader = false;
      },

      (response) => {
        // Error Callback
        this.gridData = response.body;
      }
    );
    },
    getOrgNlocationData() {
      this.dialogLoader = true;
      this.nFacilityID = parseInt(this.selectFacility);
      this.$http
        .get("ManageText/GetOrgNlocationData/sFacilityID=" + this.nFacilityID)
        .then(
          (response) => {
            // get body data
            this.orgData = JSON.parse(response.bodyText)["orgData"];
            this.locationData = JSON.parse(response.bodyText)["locations"];
            this.dialogLoader = false;
          },

          (response) => {
            // error callback
            this.orgData = response.body;
          }
        );
    },

    getLocationData() {
      this.dialogLoader = true;
      this.nFacilityID = parseInt(this.selectFacility);
      this.nFacilityOrgID = parseInt(this.selectOrganization);
      this.$http
        .get(
          "ManageText/GetLocationData/sFacilityID=" +
            this.nFacilityID +
            "&sFacilityOrgID=" +
            this.nFacilityOrgID
        )
        .then(
          (response) => {
            this.locationData = JSON.parse(response.bodyText)["locations"];
            this.dialogLoader = false;
          },

          (response) => {
            // error callback
            this.orgData = response.body;
          }
        );
    },

    getImgUrl(selectScreen) {
      var images = require.context("../assets/images/", false, /\.png$/);
      return images("./" + selectScreen + ".png");
    },

    pushToArray(obj) {
      const index = this.updatedArray.findIndex(
        (e) => e.sTableName === obj.sTableName
      );
      if (index === -1) {
        this.updatedArray.push(obj);
      } else {
        this.updatedArray[index] = obj;
      }
      this.onSubmit();
    },

    Delete(){
      //  this.dialogLoader = true;
      //  this.$http
      //     .post("ManageText/DeleteGridData", deletData)
      //     .then((response) => {
      //       if (response.ok == true) {
      //         this.gridData = [];
      //          this.GetgridData();
      //         this.dialogLoader = false;
      //       }
      //     });
    },

    onSubmit() {
      this.dialogLoader = true;
     var nAdminUserID = this.$store.state.adminUserId;
     var gridList = this.updatedArray.map(function (item) {
          item["nUpdatedAdminUserID"] = nAdminUserID;
          return item;
        });
        var editGrid = {
          gridData : gridList
        }
        this.$http
          .post("ManageText/EditGridData/", editGrid)
          .then((response) => {
            if (response.ok == true) {
              this.gridData = [];
               this.GetgridData();
              this.updatedArray = [];
              this.dialogLoader = false;
            }
          });
      },
  },
};
</script>

<style>
.image {
  height: 100%;
  width: 100%;
  float: middle;
}
.GroupHeader {
  text-align: center;
}
</style>

 

