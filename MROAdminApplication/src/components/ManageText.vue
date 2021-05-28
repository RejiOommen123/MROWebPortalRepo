<template>
  <div>
    <v-row  style="margin-left: 1%;margin-right: 1%;height: 100%;">
      <v-col md="9" sm="9">
        <v-card>
          <v-card-title>
            Filters
          </v-card-title>
          <v-card-text>
          <v-row>
            <v-col cols="2" sm="4" md="2">
              <v-select
                class="manageTextDropDown"
                v-model="selectFacility"
                :items="facilityData"
                item-text="sFacilityName"
                item-value="nFacilityID"
                label="Select Facility"
                @change="getOrgNlocationData()"
                dense
              ></v-select>
            </v-col>
            <v-col cols="2" sm="4" md="2">
              <v-select
                class="manageTextDropDown"
                :disabled="selectFacility == 0"
                v-model="selectOrganization"
                :items="orgData"
                item-text="sOrgName"
                item-value="nFacilityOrgID"
                label="Select Organization"
                @change="getLocationData()"
                dense
              ></v-select>
            </v-col>
            <v-col cols="2" sm="4" md="2">
              <v-select
                class="manageTextDropDown"
                :disabled="selectFacility == 0"
                v-model="selectLocation"
                :items="locationData"
                item-text="sLocationName"
                item-value="nFacilityLocationID"
                label="Select Location"
                 @change="getGridData()"
                dense
              ></v-select>
            </v-col>
          <!-- </v-row>
          <v-row  md="6" sm="6"> -->
            <v-col cols="2" md="2" sm="4" >
              <v-select
                class="manageTextDropDown"
                v-model="selectScreen"
                :items="screenData"
                item-text="sWizardDescription"
                item-value="nWizardID"
                label="Select Screen"
                dense
              ></v-select>
            </v-col>
            <v-col cols="2" md="2" sm="4">
              <v-select
                class="manageTextDropDown"
                v-model="selectLanguage"
                :items="languageData"
                item-text="text"
                item-value="value"
                label="Select Language"
                dense
              ></v-select>
            </v-col>
          </v-row>
          <v-row  md="12" sm="12">
            <v-col cols="12" md="12" sm="12">
              <v-data-table
                v-if="gridData != null"
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
                dense
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

                <template v-slot:item.sText="props">
                  <v-edit-dialog
                    v-if="props.item.sTableName != 'gridData'"
                    :return-value.sync="props.item.sText"
                    @save="pushToArray(props.item)"
                  >
                    {{ props.item.sText }}
                    <template v-slot:input>
                      <v-text-field
                        v-model="props.item.sText"
                        label="Edit"
                        counter
                        :maxlength="(props.item.gridData = 100)"
                      ></v-text-field>
                    </template>
                  </v-edit-dialog>
                  <label v-else>{{ props.item.sText }}</label>
                </template>
                <template v-slot:item.bReset="props" >
                  <v-icon :disabled="props.item.sPlace=='G__'" style="color: red" @click="resetToPrevious(props.item)"> mdi-close</v-icon>
                </template>
              </v-data-table>
            </v-col>
          </v-row>
          </v-card-text>
        </v-card>
      </v-col>
      <!-- <v-divider vertical style="margin-left: 5%"> </v-divider> -->
      <v-col cols="3" md="3" sm="3" style="height: 100%;">
        <div v-if="selectScreen != null">
          <img :src="getImgUrl(selectScreen)" class="image" />
        </div>
      </v-col>
    </v-row>
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
      selectFacility: 0,
      facilityData: [
        {
          sFacilityName: "-Select-",
          nFacilityID: null,
        },
      ],
      selectScreen: 0,
      screenData: [
        {
          sWizardName: "-Select-",
          nWizardID: null,
        },
      ],
      selectOrganization: 0,
      orgData: [
        {
          sOrgName: "-Select-",
          nFacilityOrgID: null,
          sLocationName: "-Select-",
          nFacilityLocationID: null,
        },
      ],
      selectLocation: 0,
      locationData: [
        {
          sLocationName: "-Select-",
          nFacilityLocationID: null,
        },
      ],
      selectLanguage: 1,
      languageData: [
        {
          text: "English",
          value: 1,
        },
      ],
      updatedArray: [],
      gridData: null,
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
        if (response.ok == true) {
        this.facilityData = [
          ...this.facilityData,
          ...JSON.parse(response.bodyText)["facilities"],
        ];
        this.screenData = JSON.parse(response.bodyText)["wizard"];
        if (this.screenData != null) {
          this.selectScreen = this.screenData[0].nWizardID;
        }
        this.getGridData();
        this.dialogLoader = false;
        }
      },

      (response) => {
        // Error Callback
        this.facilityData = response.body;
      }
    );   
  },

  methods: {
    getManageTextFilterParam(){
      var ManageTextFilterParam =
      {
          nFacilityID: this.selectFacility,
          nFacilityOrgID: this.selectOrganization,
          nFacilityLocationID: this.selectLocation,
          nWizardID: this.selectScreen,
          nLanguageID: this.selectLanguage
      };
      return ManageTextFilterParam;
    },
    getGridData(){
      this.dialogLoader = true;
     var ManageTextFilterParam = this.getManageTextFilterParam();
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
      this.selectLocation = 0;
      this.selectOrganization = 0;
      this.nFacilityID = parseInt(this.selectFacility);
      this.$http
        .get("ManageText/GetOrgNlocationData/sFacilityID=" + this.nFacilityID)
        .then(
          (response) => {
            // get body data
            this.orgData = JSON.parse(response.bodyText)["orgData"];
            this.locationData = JSON.parse(response.bodyText)["locationData"];
            this.getGridData();
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
      this.selectLocation = 0;
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
            this.getGridData();
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

    resetToPrevious(item){
      const index = this.gridData.findIndex(
        (element) => element.sTextType == item.sTextType && element.nControlID == item.nControlID && element.sLevel == item.sLevel
      );
      var resetManageText = {
        manageText : item,
        manageTextFilterParam: this.getManageTextFilterParam()
      };
       this.dialogLoader = true;
       this.$http
          .post("ManageText/ResetToPrevious",resetManageText)
          .then((response) => {
            if (response.ok == true) {
              var returnedManageText = JSON.parse(response.bodyText)["manageText"];
              this.gridData[index].sPlace = returnedManageText.sPlace;
              this.gridData[index].sTypeWithID = returnedManageText.sTypeWithID;
              this.gridData[index].sText = returnedManageText.sText;
              this.gridData[index].bReset = returnedManageText.bReset;
              this.gridData[index].sTextType = returnedManageText.sTextType;
              this.gridData[index].nControlID = returnedManageText.nControlID;
              this.gridData[index].nCommonControlID = returnedManageText.nCommonControlID;
              this.gridData[index].sLevel = returnedManageText.sLevel;
              this.gridData[index].sSort = returnedManageText.sSort;
              this.dialogLoader = false;
            }
          });
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
               this.getGridData();
              this.updatedArray = [];
              this.dialogLoader = false;
            }
          });
      },
  },
};
</script>

<style scoped>
.image {
  height: 100%;
  width: 100%;
  float: middle;
}
.GroupHeader {
  text-align: center;
}
.manageTextDropDown{
  font-size: 0.8125rem;
}
</style>

 

