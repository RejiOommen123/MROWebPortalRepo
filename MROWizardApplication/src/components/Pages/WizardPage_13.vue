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
                  <v-icon v-on="on" color="grey" top>mdi-information</v-icon>
                </template>
                <v-col cols="12" sm="12">
                  <p style="width:200px">{{shipmentType.sFieldToolTip}}</p>
                </v-col>
              </v-tooltip>
            </v-checkbox>

            <!-- MROEmail Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROEmail'">
              <v-col v-if="sSelectedShipmentTypes[0]=='MROEmail'" slot="MROEmail" cols="12" sm="12">
                <label for="sSTEmailId" class="control-label">Email</label>
                <v-text-field
                  v-model="sSTEmailId"
                  :error-messages="sSTEmailIdErrors"
                  required
                  @input="$v.sSTEmailId.$touch()"
                  @blur="$v.sSTEmailId.$touch()"
                ></v-text-field>
                <label for="sSTConfirmEmailId" class="control-label">Confirm Email</label>
                <v-text-field
                  @paste.prevent
                  v-model="sSTConfirmEmailId"
                  :error-messages="sSTConfirmEmailIdErrors"
                  required
                  @input="$v.sSTConfirmEmailId.$touch()"
                  @blur="$v.sSTConfirmEmailId.$touch()"
                ></v-text-field>
              </v-col>
            </div>

            <!-- MROMailShipment Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROMailShipment'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROMailShipment'"
                slot="MROMailShipment"
                cols="12"
                sm="12"
              >
                <v-textarea 
                v-model="sSTMailCompAdd"
                rows="2" 
                counter label="Complete Address"
                :error-messages="sSTMailCompAddErrors"
                @input="$v.sSTMailCompAdd.$touch()"
                @blur="$v.sSTMailCompAdd.$touch()"
                ></v-textarea>
                <label for="sSTEmailId" class="control-label">Email</label>
                <v-text-field
                  v-model="sSTEmailId"
                  :error-messages="sSTEmailIdErrors"
                  required
                  @input="$v.sSTEmailId.$touch()"
                  @blur="$v.sSTEmailId.$touch()"
                ></v-text-field>
                <label for="sSTConfirmEmailId" class="control-label">Confirm Email</label>
                <v-text-field
                  @paste.prevent
                  v-model="sSTConfirmEmailId"
                  :error-messages="sSTConfirmEmailIdErrors"
                  required
                  @input="$v.sSTConfirmEmailId.$touch()"
                  @blur="$v.sSTConfirmEmailId.$touch()"
                ></v-text-field>
              </v-col>
            </div>

            <!-- MROIn-Person slot -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROIn-Person'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROIn-Person'"
                slot="MROIn-Person"
                cols="12"
                sm="12"
              >
                <v-text-field
                  v-model="sSTRecordFormat"
                  :error-messages="sSTRecordFormatErrors"
                  required
                  label="Provide Format of Record"
                  placeholder="Paper,CD,etc"
                  @input="$v.sSTRecordFormat.$touch()"
                  @blur="$v.sSTRecordFormat.$touch()"
                ></v-text-field>
                <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      :value="dtSTPickUpFormatted"
                      placeholder="MM-DD-YYYY"
                      :error-messages="dtSTPickUpErrors"
                      clearable
                      label="Pick up date"
                      readonly
                      v-bind="attrs"
                      v-on="on"
                      @click:clear="dtSTPickUp = null"
                      @input="$v.dtSTPickUp.$touch()"
                      @blur="$v.dtSTPickUp.$touch()"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="dtSTPickUp"
                    color="green lighten-1"
                    header-color="primary"
                    light
                    @change="menu1 = false"
                  ></v-date-picker>
                </v-menu>
              </v-col>
            </div>

            <!-- MROFax Block -->
            <div v-if="shipmentType.sNormalizedShipmentTypeName=='MROFax'">
              <v-col
                v-if="sSelectedShipmentTypes[0]=='MROFax'"
                slot="MROFax"
                cols="12"
                sm="12"
              >
                <v-text-field
                  type="number"
                  v-model="nSTFaxNo"
                  :error-messages="nSTFaxNoErrors"
                  label="Fax No"
                  required
                  @input="$v.nSTFaxNo.$touch()"
                  @blur="$v.nSTFaxNo.$touch()"
                ></v-text-field>
                <v-textarea 
                v-model="sSTFaxCompAdd" 
                rows="2" 
                counter 
                label="Complete Address"
                :error-messages="sSTFaxCompAddErrors"
                @input="$v.sSTFaxCompAdd.$touch()"
                @blur="$v.sSTFaxCompAdd.$touch()"
                ></v-textarea>
              </v-col>
            </div>
          </v-col>
        </v-layout>
        <div v-if="sSelectedShipmentTypes[0]=='MROPatientPortal'">
          <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROEmail'">
          <v-btn :disabled="($v.sSTEmailId.$invalid || $v.sSTConfirmEmailId.$invalid)" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROMailShipment'">
          <v-btn :disabled="$v.sSTMailCompAdd.$invalid || $v.sSTEmailId.$invalid || $v.sSTConfirmEmailId.$invalid" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROIn-Person'">
          <v-btn :disabled="$v.dtSTPickUp.$invalid || $v.sSTRecordFormat$invalid" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
        <div v-if="sSelectedShipmentTypes[0]=='MROFax'">
          <v-btn :disabled="$v.nSTFaxNo.$invalid || $v.sSTFaxCompAdd.$invalid" @click.prevent="nextPage" class="next">Next</v-btn>
        </div>
      </form>
    </template>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, email, sameAs, numeric } from "vuelidate/lib/validators";
import moment from "moment";
export default {
  name: "WizardPage_12",
  data() {
    return {
      oShipmentTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oShipmentTypes,
      // oShipmentTypeArray: this.$store.state.ConfigModule.oShipmentTypes,
      sSelectedShipmentTypes: [],
      // combine previously entered data by requester
      sSTFaxCompAdd:
        this.$store.state.requestermodule.sAddStreetAddress +
        ", " +
        this.$store.state.requestermodule.sAddCity +
        ", " +
        this.$store.state.requestermodule.sAddState +
        ", " +
        this.$store.state.requestermodule.sAddZipCode,
      nSTFaxNo: 0,
      sSTEmailId: this.$store.state.requestermodule.sRequesterEmailId,
      sSTConfirmEmailId: this.$store.state.requestermodule.sRequesterEmailId,
      // combine previously entered data by requester
      sSTMailCompAdd:
        this.$store.state.requestermodule.sAddStreetAddress +
        ", " +
        this.$store.state.requestermodule.sAddCity +
        ", " +
        this.$store.state.requestermodule.sAddState +
        ", " +
        this.$store.state.requestermodule.sAddZipCode,
      sSTRecordFormat: "",
      dtSTPickUp: "",
      menu1: false
    };
  },
  //Shipment type validations
  mixins: [validationMixin],
  validations: {
    nSTFaxNo: {
      required,
      numeric
    },
    sSTEmailId: {
      required,
      email
    },
    sSTConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs("sSTEmailId")
    },
    sSTMailCompAdd: {
      required
    },
    sSTFaxCompAdd: {
      required
    },
    sSTRecordFormat: {
      required
    },
    dtSTPickUp: {
      required,
      minValue: value => value > new Date().toISOString()
    }
  },
  computed: {
    //validations error message setter
    nSTFaxNoErrors() {
      const errors = [];
      if (!this.$v.nSTFaxNo.$dirty) return errors;
      !this.$v.nSTFaxNo.numeric && errors.push("Fax No must me numeric.");
      !this.$v.nSTFaxNo.required && errors.push("Fax No is required.");
      return errors;
    },
    sSTEmailIdErrors() {
      const errors = [];
      if (!this.$v.sSTEmailId.$dirty) return errors;
      !this.$v.sSTEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sSTEmailId.required && errors.push("E-mail is required");
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
    sSTMailCompAddErrors() {
      const errors = [];
      if (!this.$v.sSTMailCompAdd.$dirty) return errors;
      !this.$v.sSTMailCompAdd.required && errors.push("Mail address is required");
      return errors;
    },
   sSTFaxCompAddErrors() {
      const errors = [];
      if (!this.$v.sSTFaxCompAdd.$dirty) return errors;
      !this.$v.sSTFaxCompAdd.required && errors.push("Mail address is required");
      return errors;
    },
    sSTRecordFormatErrors() {
      const errors = [];
      if (!this.$v.sSTRecordFormat.$dirty) return errors;
      !this.$v.sSTRecordFormat.required &&
        errors.push("Record format type is required");
      return errors;
    },
    dtSTPickUpFormatted() {
      return this.dtSTPickUp
        ? moment(this.dtSTPickUp).format("MM-DD-YYYY")
        : "";
    },
    dtSTPickUpErrors() {
      const errors = [];
      if (!this.$v.dtSTPickUp.$dirty) return errors;
      !this.$v.dtSTPickUp.minValue && errors.push("Invalid Date");
      !this.$v.dtSTPickUp.required && errors.push("End Date is required");
      return errors;
    }
  },
  methods: {
    nextPage() {
      // Before switching shipment type reset state data for shipment type
        this.resetSTState();
      //Switch based on selection and set state data
      switch (this.sSelectedShipmentTypes[0]) {
        case "MROEmail":
          this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
          break;
        case "MROMailShipment":
          this.$store.commit(
            "requestermodule/sSTMailCompAdd",
            this.sSTMailCompAdd
          );
          this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
          break;
        case "MROIn-Person":
          this.$store.commit("requestermodule/sSTRecordFormat", this.sSTRecordFormat);
          this.$store.commit("requestermodule/dtSTPickUp", this.dtSTPickUp);
          break;
        case "MROFax":
          this.$store.commit("requestermodule/nSTFaxNo",this.nSTFaxNo);
          this.$store.commit("requestermodule/sSTFaxCompAdd",this.sSTFaxCompAdd);
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
      this.$store.commit("requestermodule/sSTEmailId", '');
      this.$store.commit("requestermodule/sSTMailCompAdd",'');
      this.$store.commit("requestermodule/sSTRecordFormat", '');
      this.$store.commit("requestermodule/dtSTPickUp", '');
      this.$store.commit("requestermodule/nSTFaxNo",0);
      this.$store.commit("requestermodule/sSTFaxCompAdd",'');
    }
  }
};
</script>
