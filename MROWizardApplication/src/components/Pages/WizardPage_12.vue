<template>
  <div class="center">
    <h1>
      How would you like us
      <br />to send you record(s)?
    </h1>

    <template>
      <form>
        <v-layout
          v-for="shipmentType in oShipmentTypeArray"
          :key="shipmentType.sNormalizedShipmentTypeName"
          row
          wrap
        >
          <v-col cols="12" offset-sm="3" sm="6">
            <v-checkbox
              dark
              class="checkboxBorder"
              :label="shipmentType.sShipmentTypeName"
              color="green"
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

            <slot :name="shipmentType.sNormalizedShipmentTypeName"></slot>
          </v-col>
        </v-layout>
        <!-- Slots Starts -->
        <template>
          <v-col
            v-if="sSelectedShipmentTypes[0]=='MROFax'"
            slot="MROFax"
            cols="12"
            offset-sm="2"
            sm="8"
          >
            <v-textarea v-model="sSTFaxCompAdd" counter label="Complete Address"></v-textarea>
          </v-col>
        </template>

        <template>
          <v-col
            v-if="sSelectedShipmentTypes[0]=='MROEmail'"
            slot="MROEmail"
            cols="12"
            offset-sm="2"
            sm="8"
          >
            <label for="sSTEmailId" class="control-label">Email ID</label>
            <v-text-field
              v-model="sSTEmailId"
              :error-messages="sSTEmailIdErrors"
              required
              @input="$v.sSTEmailId.$touch()"
              @blur="$v.sSTEmailId.$touch()"
            ></v-text-field>
            <label for="sSTConfirmEmailId" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="sSTConfirmEmailId"
              :error-messages="sSTConfirmEmailIdErrors"
              required
              @input="$v.sSTConfirmEmailId.$touch()"
              @blur="$v.sSTConfirmEmailId.$touch()"
            ></v-text-field>
          </v-col>
        </template>

        <template>
          <v-col
            v-if="sSelectedShipmentTypes[0]=='MROMailShipment'"
            slot="MROMailShipment"
            cols="12"
            offset-sm="2"
            sm="8"
          >
            <v-textarea v-model="sSTMailCompAdd" counter label="Complete Address"></v-textarea>
            <label for="sSTEmailId" class="control-label">Email ID</label>
            <v-text-field
              v-model="sSTEmailId"
              :error-messages="sSTEmailIdErrors"
              required
              @input="$v.sSTEmailId.$touch()"
              @blur="$v.sSTEmailId.$touch()"
            ></v-text-field>
            <label for="sSTConfirmEmailId" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="sSTConfirmEmailId"
              :error-messages="sSTConfirmEmailIdErrors"
              required
              @input="$v.sSTConfirmEmailId.$touch()"
              @blur="$v.sSTConfirmEmailId.$touch()"
            ></v-text-field>
          </v-col>
        </template>

        <template>
          <v-col
            v-if="sSelectedShipmentTypes[0]=='MROIn-Person'"
            slot="MROIn-Person"
            cols="12"
            offset-sm="2"
            sm="8"
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
        </template>

        <!-- Slots End -->
        <div>
          <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
        </div>
      </form>
    </template>
  </div>
  <!-- MROPatientPortal
MROEmail
MROMailShipment
MROIn-Person
  MROFax-->
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, email, sameAs } from "vuelidate/lib/validators";
import moment from "moment";
export default {
  name: "WizardPage_12",
  data() {
    return {
      //this.$store.state.ConfigModule.apiResponseDataByLocation.oPrimaryReason
      oShipmentTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation.oShipmentTypes,
      sSelectedShipmentTypes: [],
      sSTFaxCompAdd: "",
      sSTEmailId: this.$store.state.requestermodule.sPatientEmailId,
      sSTConfirmEmailId: this.$store.state.requestermodule.sConfirmEmailId,
      sSTMailCompAdd: "",
      sSTRecordFormat: "",
      dtSTPickUp: "",
      menu1: false

      //other: false
    };
  },
  mixins: [validationMixin],
  validations: {
    sSTEmailId: {
      required,
      email
    },
    sSTConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs("sSTEmailId")
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
      switch (this.sSelectedShipmentTypes[0]) {
        case "MROFax":
          this.$store.commit(
            "requestermodule/sSTFaxCompAdd",
            this.sSTFaxCompAdd
          );
          console.log(this.sSTFaxCompAdd);
          break;
        case "MROEmail":
          this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
          this.$store.commit(
            "requestermodule/sSTConfirmEmailId",
            this.sSTConfirmEmailId
          );
          console.log(this.dAuthExpire);
          break;
        case "MROMailShipment":
          this.$store.commit(
            "requestermodule/sSTMailCompAdd",
            this.sSTMailCompAdd
          );
          this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
          this.$store.commit(
            "requestermodule/sSTConfirmEmailId",
            this.sSTConfirmEmailId
          );
          console.log(this.dAuthExpire);
          break;
        case "MROIn-Person":
          this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
          this.$store.commit(
            "requestermodule/sSTConfirmEmailId",
            this.sSTConfirmEmailId
          );
          this.$store.commit("requestermodule/dtSTPickUp", this.dtSTPickUp);
          console.log(this.dAuthExpire);
          break;
      }
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {
      this.sSelectedShipmentTypes = [];
      this.sSelectedShipmentTypes.push(id);
      console.log(this.sSelectedShipmentTypes[0]);
    }
  }
};
</script>

<style scoped>
</style>