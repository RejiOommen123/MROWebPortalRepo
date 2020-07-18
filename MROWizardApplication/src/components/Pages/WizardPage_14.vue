<template>
  <div class="center">
    <h1>How would you like us to send your record(s)?</h1>

    <template>
      <form>
        <!-- Get all Shipment Type associated to facility and displayed as checkbox for selection-->
        <v-layout
          v-for="shipmentType in oShipmentTypeArray"
          :key="shipmentType.sNormalizedShipmentTypeName"
          row
          wrap
        >
          <v-col cols="12" offset-sm="2" sm="8">
            <v-checkbox
              hide-details
              dark
              class="checkboxBorder"
              :label="shipmentType.sShipmentTypeName"
              color="#e84700"
              :value="shipmentType.sNormalizedShipmentTypeName"
              v-model="sSelectedShipmentTypes"
              @change="check(shipmentType.sNormalizedShipmentTypeName)"
            >
              <v-tooltip v-if="shipmentType.sFieldToolTip" slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px">{{shipmentType.sFieldToolTip}}</p>
                </v-col>
              </v-tooltip>
            </v-checkbox>

            <!-- MROSTEmail Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROSTEmail'">
              <v-col v-if="sSelectedShipmentTypes[0]=='MROSTEmail'" slot="MROSTEmail" cols="12" sm="12">
                <v-text-field
                  v-model="sSTEmailAddress"
                  :error-messages="sSTEmailAddressErrors"
                  label="EMAIL"
                  required
                  @input="$v.sSTEmailAddress.$touch()"
                  @blur="$v.sSTEmailAddress.$touch()"
                ></v-text-field>
                <v-text-field
                  @paste.prevent
                  v-model="sSTConfirmEmailId"
                  :error-messages="sSTConfirmEmailIdErrors"
                  label="CONFIRM EMAIL"
                  required
                  @input="$v.sSTConfirmEmailId.$touch()"
                  @blur="$v.sSTConfirmEmailId.$touch()"
                ></v-text-field>
              </v-col>
            </div>

            <!-- MROSTMail Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROSTMail'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROSTMail'"
                slot="MROSTMail"
                cols="12"
                sm="12"
              >
              <v-row>
              <v-col cols="12" sm="6">
              <v-text-field
                v-model="sSTAddStreetAddress"
                :error-messages="streetErrors"
                label="STREET"
                required
                @input="$v.sSTAddStreetAddress.$touch()"
                @blur="$v.sSTAddStreetAddress.$touch()"
              ></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field
                  v-model="sSTAddApartment"
                  label="Apartment/Building"
                ></v-text-field>
              </v-col>
              <v-col  cols="5" sm="5">
                <v-text-field
                  v-model="sSTAddCity"
                  :error-messages="cityErrors"
                  label="CITY"
                  required
                  @input="$v.sSTAddCity.$touch()"
                  @blur="$v.sSTAddCity.$touch()"
                ></v-text-field>
              </v-col>
              <v-col  cols="3"  sm="3">
                <v-text-field
                  v-model="sSTAddState"
                  :error-messages="stateErrors"
                  label="STATE"
                  required
                  @input="$v.sSTAddState.$touch()"
                  @blur="$v.sSTAddState.$touch()"
                ></v-text-field>
              </v-col>
              <v-col  cols="4" sm="4">
                <v-text-field
                  type="tel"
                  v-model="sSTAddZipCode"
                  :error-messages="sSTAddZipCodeErrors"
                  label="ZIP CODE"
                  required
                  @input="$v.sSTAddZipCode.$touch()"
                  @blur="$v.sSTAddZipCode.$touch()"
                  minlength="5"
                  maxlength="5"
                ></v-text-field>
              </v-col>
              </v-row>
              </v-col>
            </div>

            <!-- MROSTPatientPickUp slot -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROSTPatientPickUp'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROSTPatientPickUp'"
                slot="MROSTPatientPickUp"
                cols="12"
                sm="12"
              >
              <!-- <div class="disclaimer">{{disclaimer}}</div> -->
              <div v-if="pickUpInstruction!=''" class="disclaimer">{{pickUpInstruction}}</div>               
              </v-col>                
            </div>

            <!-- MROSTFax Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROSTFax'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROSTFax'"
                slot="MROSTFax"
                cols="12"
                sm="12"
              >
                <v-text-field
                  type="tel"
                  v-model="nSTFaxNumber"
                  :error-messages="nSTFaxNumberErrors"
                  label="FAX NO"
                  required
                  @input="$v.nSTFaxNumber.$touch()"
                  @blur="$v.nSTFaxNumber.$touch()"
                ></v-text-field>
                <v-text-field
                  type="tel"
                  v-model="nSTConfirmFaxNumber"
                  :error-messages="nSTConfirmFaxNumberErrors"
                  label="CONFIRM FAX NO"
                  required
                  @input="$v.nSTConfirmFaxNumber.$touch()"
                  @blur="$v.nSTConfirmFaxNumber.$touch()"
                ></v-text-field>
              </v-col>
            </div>
          </v-col>
        </v-layout>
        <div v-if="sSelectedShipmentTypes[0]=='MROSTPatientPortal'">
          <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROSTEmail'">
          <v-btn :disabled="($v.sSTEmailAddress.$invalid || $v.sSTConfirmEmailId.$invalid)" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROSTMail'">
          <v-btn :disabled="$v.sSTAddStreetAddress.$invalid || $v.sSTAddCity.$invalid || $v.sSTAddState.$invalid || $v.sSTAddZipCode.$invalid " @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROSTPatientPickUp'">
          <v-btn  @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROSTFax'">
          <v-btn :disabled="$v.nSTFaxNumber.$invalid || $v.nSTConfirmFaxNumber.$invalid" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
      </form>
    </template>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, email, sameAs, numeric ,maxLength,minLength} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_14",
  data() {
    return {
      oShipmentTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oShipmentTypes,
      // oShipmentTypeArray: this.$store.state.ConfigModule.oShipmentTypes,
      sSelectedShipmentTypes: [],
      // combine previously entered data by requester

      nSTFaxNumber: 0,
      nSTConfirmFaxNumber:0,
      sSTEmailAddress: this.$store.state.requestermodule.sRequesterEmailId,
      sSTConfirmEmailId: this.$store.state.requestermodule.sRequesterEmailId,
      // combine previously entered data by requester
      sSTAddZipCode: this.$store.state.requestermodule.sAddZipCode,
      sSTAddCity: this.$store.state.requestermodule.sAddCity,
      sSTAddState: this.$store.state.requestermodule.sAddState,
      sSTAddStreetAddress: this.$store.state.requestermodule.sAddStreetAddress,
      sSTAddApartment:this.$store.state.requestermodule.sAddApartment,
      menu1: false,

      // disclaimer:this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_14_disclaimer01,
      pickUpInstruction:this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.pickUpInstruction,
    };
  },
  //Shipment type validations
  mixins: [validationMixin],
  validations: {
    nSTFaxNumber: {
      required,
      numeric
    },
    nSTConfirmFaxNumber:{
      required,
      sameAsFaxNo: sameAs("nSTFaxNumber")
    },
    sSTEmailAddress: {
      required,
      email
    },
    sSTConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs("sSTEmailAddress")
    },
    sSTAddZipCode: {
      required,
      maxLength: maxLength(5),
      minLength: minLength(5),
      numeric
    },
    sSTAddCity: { required },
    sSTAddState: { required },
    sSTAddStreetAddress: { required },
  },
  computed: {
    //validations error message setter
    nSTFaxNumberErrors() {
      const errors = [];
      if (!this.$v.nSTFaxNumber.$dirty) return errors;
      !this.$v.nSTFaxNumber.numeric && errors.push("Fax No must me numeric.");
      !this.$v.nSTFaxNumber.required && errors.push("Fax No is required.");
      return errors;
    },
    nSTConfirmFaxNumberErrors() {
      const errors = [];
      if (!this.$v.nSTConfirmFaxNumber.$dirty) return errors;
      !this.$v.nSTConfirmFaxNumber.sameAsFaxNo &&
        errors.push("Please enter correct Fax No");
      !this.$v.nSTConfirmFaxNumber.numeric && errors.push("Fax No must me numeric.");
      !this.$v.nSTConfirmFaxNumber.required && errors.push("Fax No is required.");
      return errors;
    },
    sSTEmailAddressErrors() {
      const errors = [];
      if (!this.$v.sSTEmailAddress.$dirty) return errors;
      !this.$v.sSTEmailAddress.email && errors.push("Must be valid e-mail");
      !this.$v.sSTEmailAddress.required && errors.push("E-mail is required");
      return errors;
    },
    sSTConfirmEmailIdErrors() {
      const errors = [];
      if (!this.$v.sSTConfirmEmailId.$dirty) return errors;
      !this.$v.sSTConfirmEmailId.sameAsemailID &&
        errors.push("Please enter correct e-mail");
      !this.$v.sSTConfirmEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sSTConfirmEmailId.required && errors.push("E-mail is required");
      return errors;
    },
    sSTAddZipCodeErrors() {
      const errors = [];
      if (!this.$v.sSTAddZipCode.$dirty) return errors;
      !this.$v.sSTAddZipCode.maxLength && errors.push("ZipCode must be 5 digit");
      !this.$v.sSTAddZipCode.minLength && errors.push("ZipCode must be 5 digit");
      !this.$v.sSTAddZipCode.numeric && errors.push("ZipCode must me numeric.");
      !this.$v.sSTAddZipCode.required && errors.push("ZipCode is required.");
      return errors;
    },
    cityErrors() {
      const errors = [];
      if (!this.$v.sSTAddCity.$dirty) return errors;
      !this.$v.sSTAddCity.required && errors.push("City is required.");
      return errors;
    },
    stateErrors() {
      const errors = [];
      if (!this.$v.sSTAddState.$dirty) return errors;
      !this.$v.sSTAddState.required && errors.push("State is required.");
      return errors;
    },
    streetErrors() {
      const errors = [];
      if (!this.$v.sSTAddStreetAddress.$dirty) return errors;
      !this.$v.sSTAddStreetAddress.required &&
        errors.push("Street Address is required.");
      return errors;
    },
  },
  methods: {
    nextPage() {
      // Before switching shipment type reset state data for shipment type
        this.resetSTState();
      //Switch based on selection and set state data
      switch (this.sSelectedShipmentTypes[0]) {
        case "MROSTEmail":
          this.$store.commit("requestermodule/sSTEmailAddress", this.sSTEmailAddress);
          this.$store.commit("ConfigModule/bShowRecipientPage",true);
          break;
        case "MROSTMail":
          this.$store.commit("requestermodule/sSTAddZipCode", this.sSTAddZipCode);
          this.$store.commit("requestermodule/sSTAddCity", this.sSTAddCity);
          this.$store.commit("requestermodule/sSTAddState", this.sSTAddState);
          this.$store.commit(
            "requestermodule/sSTAddStreetAddress",
            this.sSTAddStreetAddress
          );
          this.$store.commit("requestermodule/sSTAddApartment",this.sSTAddApartment);
          break;
        case "MROSTFax":
          this.$store.commit("requestermodule/nSTFaxNumber",this.nSTFaxNumber);
          this.$store.commit("ConfigModule/bShowRecipientPage",true);
          break;
        default:
            this.$store.commit("ConfigModule/bShowRecipientPage",false);
          break;
      }

      this.$store.commit("requestermodule/sSelectedShipmentTypes", this.sSelectedShipmentTypes);       

      
      //Partial Requester Data Save Start
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
      if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
      {
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    //set variable based on selection
    check(id) {
      this.sSelectedShipmentTypes = [];
      this.sSelectedShipmentTypes.push(id);
    },
    resetSTState(){
      this.$store.commit("requestermodule/sSTEmailAddress", '');
      this.$store.commit("requestermodule/sSTAddZipCode", '');
      this.$store.commit("requestermodule/sSTAddCity", '');
      this.$store.commit("requestermodule/sSTAddState",'');
      this.$store.commit("requestermodule/sSTAddStreetAddress", '');
      this.$store.commit("requestermodule/sSTAddApartment",'');
      this.$store.commit("requestermodule/nSTFaxNumber",0);

      this.$store.commit("requestermodule/sRecipientFirstName",'');
      this.$store.commit("requestermodule/sRecipientLastName",'');
      // this.$store.commit("requestermodule/sRecipientMiddleName",'');
      this.$store.commit("requestermodule/sRecipientAddZipCode", '');
      this.$store.commit("requestermodule/sRecipientAddCity", '');
      this.$store.commit("requestermodule/sRecipientAddState", '');
      this.$store.commit("requestermodule/sRecipientAddStreetAddress",'');
      this.$store.commit("requestermodule/sRecipientAddApartment",'');
    }
  }
};
</script>
<style scoped>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>