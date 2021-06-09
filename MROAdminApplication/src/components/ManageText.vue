<template>
  <div>
    <v-row style="margin-left: 1%; margin-right: 1%; height: 100%">
      <v-col md="9" sm="9">
        <v-card>
          <v-card-title> Filters </v-card-title>
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
              <v-col cols="2" md="2" sm="4">
                <v-select
                  class="manageTextDropDown"
                  v-model="selectScreen"
                  :items="screenData"
                  item-text="sWizardDescription"
                  item-value="nWizardID"
                  label="Select Screen"
                  @change="getGridData()"
                  dense
                >
                  <!-- <template slot="selection" slot-scope="data">
                  {{ data.item.nWizardID }} - {{ data.item.sWizardDescription }}
                </template> -->
                  <template slot="item" slot-scope="data">
                    {{ data.item.nWizardID }} -
                    {{ data.item.sWizardDescription }}
                  </template>
                </v-select>
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
            <v-row md="12" sm="12">
              <v-col cols="12" md="12" sm="12">
                <v-data-table
                  v-if="gridData != null"
                  :headers="headers"
                  :items="gridData"
                  :footer-props="{
                    'items-per-page-options': [100, 200, 500, 1000],
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
                      :style="'background-color:' + colorName(group)"
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
                        {{ groupName(group) }}
                      </v-btn>
                    </td>
                  </template>

                  <template v-slot:item.sText="props">
                    <v-edit-dialog                    
                  :return-value.sync="props.item.sText"                 
                      @save="editText(props.item)"
                    >
                      {{ props.item.sText }}
                      <template v-slot:input>
                        <v-text-field
                          v-model="props.item.sText"
                          label="Edit"
                          counter
                          maxlength="100"
                        ></v-text-field>
                      </template>
                    </v-edit-dialog>
                  
                  </template>
                  <template v-slot:item.bReset="props">
                    <v-icon
                      
                      :disabled="!props.item.bReset"
                      style="color: red"
                      @click="resetToPrevious(props.item)"
                    >
                      mdi-close</v-icon
                    >
                  </template>
                </v-data-table>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="3" md="3" sm="3" style="height: 100%">
        <div v-if="selectScreen != null" >
          <img :src=getImgUrl(selectScreen) class= image alt="Image Not Found" />
          
        </div>
      </v-col>
    </v-row>
    <v-dialog v-model="dialogLoader" persistent width="300">
      <v-card color="rgb(0, 91, 168)" dark>
        <v-card-text>
          Please stand by
          <v-progress-linear
            indeterminate
            color="white"
            class="mb-0"
          ></v-progress-linear>
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
          text: "Place",
          align: "start",
          value: "sPlace",
          width: "10%",
        },
        {
          text: "Type",
          value: "sTypeWithID",
          width: "10%",
        },
        {
          text: "Text",
          value: "sText",
          width: "70%",
        },
        {
          text: "Delete",
          value: "bReset",
          width: "10%",
        },
      ],
      selectFacility: 0,
      facilityData: [
        {
          sFacilityName: "-Select-",
          nFacilityID: 0,
        },
      ],
      selectScreen: null,
      screenData: [
        {
          sWizardName: "-Select-",
          nWizardID: 0,
        },
      ],
      selectOrganization: 0,
      orgData: [
        {
          sOrgName: "-Select-",
          nFacilityOrgID: 0,
          sLocationName: "-Select-",
          nFacilityLocationID: 0,
        },
      ],
      selectLocation: 0,
      locationData: [
        {
          sLocationName: "-Select-",
          nFacilityLocationID: 0,
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
      gridData: null
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
    getManageTextFilterParam() {
      var ManageTextFilterParam = {
        nFacilityID: this.selectFacility,
        nFacilityOrgID: this.selectOrganization,
        nFacilityLocationID: this.selectLocation,
        nWizardID: this.selectScreen,
        nLanguageID: this.selectLanguage,
      };
      return ManageTextFilterParam;
    },
    getGridData() {
      this.dialogLoader = true;
      var ManageTextFilterParam = this.getManageTextFilterParam();
      this.$http
        .post("ManageText/GetManageTextData/", ManageTextFilterParam)
        .then(
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
            (this.orgData = [{ sOrgName: "-Select-", nFacilityOrgID: 0 }]),
              (this.locationData = [
                { sLocationName: "-Select-", nFacilityLocationID: 0 },
              ]),
              (this.orgData = [
                ...this.orgData,
                ...JSON.parse(response.bodyText)["orgData"],
              ]);
            this.locationData = [
              ...this.locationData,
              ...JSON.parse(response.bodyText)["locationData"],
            ];
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
            this.locationData = [
              ...this.locationData,
              ...JSON.parse(response.bodyText)["locations"],
            ];
            this.getGridData();
            this.dialogLoader = false;
          },
          (response) => {
            // error callback
            this.orgData = response.body;
          }
        );
    },

    getImgUrl(selectScreen)
    {
     let defaultImage = null // just set default
     let path =require.context("../assets/images/", false, /\.png$/);
      try {
        return path("./" + selectScreen + ".png");
      } 
      catch (e) {
        return defaultImage
      }
    },
    
    colorName(sSort) {
      switch (sSort) {
        case 1:
          return "#CAD7B4";

        case 2:
          return "#DF9996";

        case 3:
          return "#FCF1AC";

        case "null":
          return "Others";
      }
    },

    groupName(sSort) {
      switch (sSort) {
        case 1:
          return "STATIC";

        case 2:
          return "DYNAMIC";

        case 3:
          return "COMMON";
      }
    },

    resetToPrevious(item) {
      const index = this.gridData.findIndex(
        (element) =>
          element.sTypeWithID == item.sTypeWithID &&
          element.nControlID == item.nControlID &&
          element.sLevel == item.sLevel &&
          element.nCommonControlID == item.nCommonControlID
      );
      var resetManageText = {
        manageText: item,
        manageTextFilterParam: this.getManageTextFilterParam(),
      };
      this.dialogLoader = true;
      this.$http
        .post("ManageText/ResetToPrevious", resetManageText)
        .then((response) => {
          if (response.ok == true) {
            var returnedManageText = JSON.parse(response.bodyText)[
              "manageText"
            ];
            this.gridData[index].sPlace = returnedManageText.sPlace;
            this.gridData[index].sTypeWithID = returnedManageText.sTypeWithID;
            this.gridData[index].sText = returnedManageText.sText;
            this.gridData[index].bReset = returnedManageText.bReset;
            this.gridData[index].sTextType = returnedManageText.sTextType;
            this.gridData[index].nControlID = returnedManageText.nControlID;
            this.gridData[index].nCommonControlID =
              returnedManageText.nCommonControlID;
            this.gridData[index].sLevel = returnedManageText.sLevel;
            this.gridData[index].sSort = returnedManageText.sSort;
            this.dialogLoader = false;
          }
        });
    },

    editText(item) {
        const index = this.gridData.findIndex(
        (element) =>
          element.sTypeWithID == item.sTypeWithID &&
          element.nControlID == item.nControlID &&
          element.sLevel == item.sLevel &&
          element.nCommonControlID == item.nCommonControlID
      );
      var editText = {
        manageText: item,
        manageTextFilterParam: this.getManageTextFilterParam(),
        nAdminUserID: this.$store.state.adminUserId
      };
      this.dialogLoader = true;
      this.$http.post("ManageText/EditGridData/", editText).then((response) => {
        if (response.ok == true) {
           var returnedManageText = JSON.parse(response.bodyText)[
              "manageText"
            ];
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
.manageTextDropDown {
  font-size: 0.8125rem;
}
</style>

 

