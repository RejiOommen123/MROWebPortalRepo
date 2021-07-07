<template>
  <div>
      <!-- Get all location associated to facility and displayed as button for selection-->
      <div class="pageBody">
        <div class="pageContent">
        <div
          v-for="location in locationArray"
          :key="location.sNormalizedLocationName"
        >
        <div v-if="!isOrgURL || (isOrgURL && !bMasterLocationExist)">
          <v-col cols="12">
            <v-btn
              :class="{activeBtn: sActiveBtn === location.sNormalizedLocationName}"
              block
              outlined
              @click="locationSelect(location)" :key="buttonKey"
              class="smallWhiteBtn"
              :value="location.sNormalizedLocationName"
            >{{location.sLocationName}}</v-btn>
          </v-col>
          <div v-if="location.sNormalizedLocationName=='MROLocationOther'">
            <v-col v-if="showOtherLoactionBox" offset="3" cols="6" offset-sm="3" sm="6">
              <label class="inputLabel required" for="otherLocation">OTHER LOCATION NAME:</label>
              <v-text-field
                id="otherLocation"
                placeholder="Other location name"
                solo
                dense
                type="text"
                v-model="sSelectedLocationName"
                :error-messages="sSelectedLocationNameErrors"
                required
                maxlength="50"
                @input="$v.sSelectedLocationName.$touch()"
                @blur="$v.sSelectedLocationName.$touch()"
              ></v-text-field>           
            </v-col>
          </div>  
        </div> 
        <div v-if="isOrgURL && bMasterLocationExist">
          <v-col cols="12">
            <v-checkbox
              color="customText"
              hide-details
              class="checkboxBorder"
              :value="location.sNormalizedLocationName"
              v-model="sLocalSelectedLocation"
              @change="checkOther()"
            >
            <template v-slot:label>
              <span class="checkboxLabel">{{location.sLocationName}}</span>
            </template>
            </v-checkbox>

          </v-col>
          <div v-if="location.sNormalizedLocationName=='MROLocationOther'">
            <v-col v-if="showOtherLoactionBox" offset="2" cols="8" >
              <label class="inputLabel required" for="otherLocation">OTHER LOCATION NAME:</label>
              <v-text-field
                id="otherLocation"
                placeholder="Other location name"
                solo
                dense
                type="text"
                v-model="sSelectedLocationName"
                :error-messages="sSelectedLocationNameErrors"
                required
                maxlength="50"
                @input="$v.sSelectedLocationName.$touch()"
                @blur="$v.sSelectedLocationName.$touch()"
              ></v-text-field>
            </v-col>
          </div>              
        </div> 
        </div>
        <Footer  v-if="$vuetify.breakpoint.xs"/>
        </div>
      </div>
      <div :class="$vuetify.breakpoint.xs ? 'buttonBlockMobile' : 'buttonBlock'">
        <v-btn
          v-if="!isOrgURL || (isOrgURL && !bMasterLocationExist)"
            :disabled="(showOtherLoactionBox==true && $v.sSelectedLocationName.$invalid) || (showOtherLoactionBox==false && sActiveBtn=='')"
            @click.once="next()" :key="buttonKey"
            class="smallBlackBtn"
        >Next</v-btn>
        <v-btn v-if="isOrgURL && bMasterLocationExist" @click.once="nextMultiCheck()" :disabled="(showOtherLoactionBox==true && $v.sSelectedLocationName.$invalid) || sLocalSelectedLocation.length == 0" class="smallBlackBtn"  :key="buttonKey">Next</v-btn>
      </div>
      <Footer v-if="!$vuetify.breakpoint.xs"/>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
import Footer from '../Footer.vue';
export default {
  name: "WizardPage_02",
   activated(){
    this.buttonKey++;
    this.$store.commit(
            "ConfigModule/titleQuestion",
            "Which location(s) are you requesting records from? Please select all that apply."
          );
   },
    components:{
      Footer
    },
   data() {
    return {
      locationArray: this.$store.state.ConfigModule
        .apiResponseDataByFacilityGUID.locationDetails,
      sActiveBtn: this.$store.state.requestermodule.sSelectedLocation,
      showOtherLoactionBox: this.$store.state.requestermodule.sSelectedLocation == "MROLocationOther",
      sSelectedLocationName: this.$store.state.ConfigModule.otherLocationName,
      buttonKey:1,
      sLocalSelectedLocation: this.$store.state.requestermodule.sSelectedLocation!="" ? this.$store.state.requestermodule.sSelectedLocation.split(',') : [],
      isOrgURL: this.$store.state.ConfigModule.sOrgGUID != null,
      commaSeperatedNormalizedName: this.$store.state.requestermodule.sSelectedLocation,
      bMasterLocationExist: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.facilityLogoandBackground[0].bMasterLocationExist
    };
  },
  mixins: [validationMixin],
  validations: {
    sSelectedLocationName: {
      required
    },
  },
    computed: {
      //validations error message setter
      sSelectedLocationNameErrors() {
        const errors = [];
        if (!this.$v.sSelectedLocationName.$dirty) return errors;
        !this.$v.sSelectedLocationName.required &&
          errors.push("Other Location Name is required.");
        return errors;
      }
    },
    methods: {
      locationSelect(location) {
        // Using sActiveBtn variable to set background color to show button selection
        this.sActiveBtn = location.sNormalizedLocationName;

        //Getting back from next slide and then again selecting location. if selecting same location  then it
        //will not fetch wizard data based on location id as it already fetch previously
        if (location.sNormalizedLocationName == "MROLocationOther") {
          this.showOtherLoactionBox = true;
        } 
        else {
          this.showOtherLoactionBox = false;
        }
        this.$store.commit(
          "requestermodule/sSelectedLocationName",
          location.sLocationName
        );
        this.commaSeperatedNormalizedName = location.sNormalizedLocationName;
      },
      next() {
        var location = this.locationArray.find(x => x.sNormalizedLocationName == this.commaSeperatedNormalizedName);
        if (location.sNormalizedLocationName == "MROLocationOther") {
          this.$store.commit(
            "requestermodule/sSelectedLocationName",location.sLocationName+'-'+this.sSelectedLocationName
          );
          this.$store.commit(
            "ConfigModule/otherLocationName",
            this.sSelectedLocationName
          );
        }
        else{
          this.$store.commit(
            "requestermodule/sSelectedLocationName",location.sLocationName
          );
        }
        this.commaSeperatedNormalizedName = location.sNormalizedLocationName;
        this.apiRequestByLocationId(location);
      },
      nextMultiCheck(){
        if(this.sLocalSelectedLocation.length == 1)
        {
          var location1 = this.locationArray.find(x => x.sNormalizedLocationName == this.sLocalSelectedLocation[0]);
          this.next(location1);          
        }
        else{
          // Shallow copy array of objects
          let multipleLoc = this.locationArray.map(a => ({...a}));
          multipleLoc = multipleLoc.filter(item => this.sLocalSelectedLocation.includes(item.sNormalizedLocationName));
          var foundIndex = multipleLoc.findIndex(x => x.sNormalizedLocationName == "MROLocationOther");
          if(foundIndex != -1){                        
            multipleLoc[foundIndex].sLocationName = multipleLoc[foundIndex].sLocationName +'-'+ this.sSelectedLocationName;
            this.$store.commit(
              "ConfigModule/otherLocationName",
              this.sSelectedLocationName
            );
          }
          this.commaSeperatedNormalizedName = this.sLocalSelectedLocation.join();
          var commaSeperatedName = multipleLoc.map(item => item.sLocationName).join();
          this.$store.commit(
            "requestermodule/sSelectedLocationName",commaSeperatedName
          );
          var location2 = {            
            nFacilityID: this.locationArray[0].nFacilityID,
            nFacilityLocationID: 0,
          };
          this.apiRequestByLocationId(location2);
        }
      },
      apiRequestByLocationId(location) {
        var LoaderDialog = {
          visible : true,
          title : 'Please stand by'
        };
        this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
        if (
          location.sNormalizedLocationName !=
          this.$store.state.requestermodule.sSelectedLocation
        ) {
          var bMultiSelected = this.sLocalSelectedLocation.length > 1;
          this.$http
            .get(
              "Wizards/GetWizardConfig/fID=" +
                location.nFacilityID +
                "&lID=" +
                location.nFacilityLocationID+
                "&rID=" +
                this.$store.state.requestermodule.nRequesterID +
                "&sLocationGUID=" +
                this.$store.state.ConfigModule.sLocationGUID +
                "&sOrgGUID=" +
                this.$store.state.ConfigModule.sOrgGUID +
                "&bMultiSelected=" +
                bMultiSelected
            )
            .then(response => {
              var apiLocationResponse = response.body;
              if (response.body) {

                this.$store.commit("ConfigModule/oWizards",apiLocationResponse.oWizards);
                this.$store.commit("ConfigModule/wizardsSave", apiLocationResponse.wizardsSave);
                this.$store.commit("ConfigModule/wizardHelper", apiLocationResponse.oWizardHelper);
                this.$store.commit(
                  "ConfigModule/Wizard_01_phoneFooter",
                  apiLocationResponse.oWizardHelper.Wizard_01_phoneFooter
                );
                this.$store.commit(
                  "ConfigModule/Wizard_01_disclaimer03",
                  apiLocationResponse.oWizardHelper.Wizard_01_disclaimer03
                );
                delete apiLocationResponse.oWizards;
                delete apiLocationResponse.wizardsSave;
                delete apiLocationResponse.oWizardHelper;

                this.$store.commit(
                  "ConfigModule/apiResponseDataByLocation",
                  apiLocationResponse
                );
                this.$store.commit(
                  "ConfigModule/wizardLogo",
                  apiLocationResponse.oLocations[0].sConfigLogoData
                );
                this.$store.commit(
                  "ConfigModule/wizardBackground",
                  apiLocationResponse.oLocations[0].sConfigBackgroundData
                );                
                this.$store.commit(
                  "requestermodule/nFacilityID",
                  apiLocationResponse.oLocations[0].nFacilityID
                );
                this.$store.commit(
                  "requestermodule/nLocationID",
                  apiLocationResponse.oLocations[0].nFacilityLocationID
                );                
                this.$store.commit(
                  "requestermodule/sSelectedLocation",
                  this.commaSeperatedNormalizedName
                );
                this.$store.commit(
                  "ConfigModule/nAuthExpirationMonths",
                  apiLocationResponse.oLocations[0].nAuthExpirationMonths
                );
                this.$store.commit(
                  "ConfigModule/nPrimaryTimeout",
                  apiLocationResponse.oLocations[0].nPrimaryTimeout
                );
                this.$store.commit(
                  "ConfigModule/nSecondaryTimeout",
                  apiLocationResponse.oLocations[0].nSecondaryTimeout
                );
                this.$store.commit(
                  "ConfigModule/bForceCompliance",
                  apiLocationResponse.oLocations[0].bForceCompliance
                );
                this.resetStateOnLocationChange();
                LoaderDialog = {
                  visible : false,
                  title : 'Please stand by'
                };
                this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
                
                //Partial Requester Data Save Start
                this.$store.dispatch('requestermodule/partialAddReq');
                this.$store.commit("ConfigModule/mutateNextIndex");
              }
            });
        } else {
          LoaderDialog = {
            visible : false,
            title : 'Please stand by'
          };
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          //Partial Requester Data Save Start
          this.$store.dispatch('requestermodule/partialAddReq');
          this.$store.commit("ConfigModule/mutateNextIndex");
        }
      },
      resetStateOnLocationChange(){
        //Reset patient representative
        this.$store.commit("requestermodule/sSelectedRelationName", "");
        this.$store.commit("requestermodule/sSelectedRelation", "");

        //Reset record type
        this.$store.commit("requestermodule/sSelectedRecordTypes", []);
        this.$store.commit("requestermodule/sOtherRTText", "");

        //Reset sensitive info
        this.$store.commit("requestermodule/sSelectedSensitiveInfo", []);
        
        //Reset waivers
        this.$store.commit("requestermodule/sSelectedWaiver", []);
        this.$store.commit("requestermodule/bWaiverAccepted", false);

        //Reset primary reason
        this.$store.commit("requestermodule/sSelectedPrimaryReasons",[]);
        this.$store.commit("requestermodule/sSelectedPrimaryReasonsName", '');

        //Reset shipment type
        this.$store.commit("requestermodule/sSelectedShipmentTypes", '');       
        this.$store.commit("requestermodule/sSelectedShipmentTypesName", '');     
      },
      checkOther(){
          if (this.sLocalSelectedLocation.includes("MROLocationOther")) {
          this.showOtherLoactionBox=true;
          }
          else{
            this.showOtherLoactionBox=false;
            this.sSelectedLocationName=''
          }
      },
    },
  mounted(){
      this.showOtherLoactionBox = this.sLocalSelectedLocation.includes("MROLocationOther");
  }
  
};
</script>
<style scoped>
</style>