<template>
  <div>
    <div class="pageBody">
      <div class="pageContent">
        <form>
          <v-radio-group v-model="sSelectedShipmentTypes">
            <!-- Get all Shipment Type associated to facility and displayed as checkbox for selection-->
            <v-layout
              v-for="shipmentType in oShipmentTypeArray"
              :key="shipmentType.sNormalizedShipmentTypeName"
              row
              wrap
            >
              <v-col cols="12"> 
                <v-radio
                  class="checkboxBorder"
                  color="black"
                  :label="shipmentType.sShipmentTypeName"
                  :value="shipmentType.sNormalizedShipmentTypeName"
                  @change="check(shipmentType)"
                >
                  <template v-slot:label>
                    <v-col cols="11" sm="11" style="padding: 0px">
                      {{ shipmentType.sShipmentTypeName }}
                    </v-col>
                    <v-col cols="1" sm="1" style="padding: 0px">
                      <v-tooltip
                        v-if="shipmentType.sFieldToolTip"
                        slot="append"
                        left
                      >
                        <template v-slot:activator="{ on }">
                          <v-icon v-on="on" color="black" top
                            >mdi-information</v-icon
                          >
                        </template>
                        <v-col cols="12" sm="12">
                          <p
                            style="
                              width: 200px;
                              background-color: white;
                              color: black;
                            "
                          >
                            {{ shipmentType.sFieldToolTip }}
                          </p>
                        </v-col>
                      </v-tooltip>
                    </v-col>
                  </template>
                </v-radio>

                <!-- MROSTEmail Block -->
                <div
                  v-if="
                    shipmentType.sNormalizedShipmentTypeName == 'MROSTEmail' &&
                    $vuetify.breakpoint.xs
                  "
                >
                  <v-col
                    v-if="
                      sSelectedShipmentTypes == 'MROSTEmail' &&
                      $vuetify.breakpoint.xs
                    "
                    slot="MROSTEmail"
                    cols="12"
                    sm="12"
                  >
                  <label class="inputLabel required" for="id_sSTEmailAddress">Email</label>
                    <v-text-field
                      placeholder="Email"
                      solo
                      dense
                      id="id_sSTEmailAddress"
                      v-model="sSTEmailAddress"
                      :error-messages="sSTEmailAddressErrors"
                      required
                      maxlength="70"
                      @input="$v.sSTEmailAddress.$touch()"
                      @blur="$v.sSTEmailAddress.$touch()"
                    ></v-text-field>
                    <label class="inputLabel required" for="id_sSTConfirmEmailId">Confirm Email</label>
                    <v-text-field
                      placeholder="Confirm Email"
                      id="id_sSTConfirmEmailId"
                      solo
                      dense
                      @paste.prevent
                      v-model="sSTConfirmEmailId"
                      :error-messages="sSTConfirmEmailIdErrors"
                      required
                      maxlength="70"
                      @input="$v.sSTConfirmEmailId.$touch()"
                      @blur="$v.sSTConfirmEmailId.$touch()"
                    ></v-text-field>
                  </v-col>
                </div>

                <!-- MROSTMail Block -->
                <div
                  v-if="
                    shipmentType.sNormalizedShipmentTypeName == 'MROSTMail' &&
                    $vuetify.breakpoint.xs
                  "
                >
                  <v-col
                    v-if="
                      sSelectedShipmentTypes == 'MROSTMail' &&
                      $vuetify.breakpoint.xs
                    "
                    slot="MROSTMail"
                    cols="12"
                    sm="12"
                  >
                    <v-row>
                      <v-col cols="12">
                        <label class="inputLabel required" for="id_sSTAddStreetAddress">Address</label>
                        <v-text-field
                          placeholder="Address"
                          solo
                          dense
                          id="id_sSTAddStreetAddress"
                          v-model="sSTAddStreetAddress"
                          :error-messages="streetErrors"
                          required
                          maxlength="50"
                          @input="$v.sSTAddStreetAddress.$touch()"
                          @blur="$v.sSTAddStreetAddress.$touch()"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="6">
                        <label class="inputLabel" for="id_sSTAddApartment">Address 2</label>
                        <v-text-field
                          placeholder="Address 2"
                          solo
                          dense
                          id="id_sSTAddApartment"
                          v-model="sSTAddApartment"
                          maxlength="50"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="6">
                         <label class="inputLabel required" for="id_sSTAddCity">City</label>
                        <v-text-field
                          placeholder="City"
                          solo
                          dense
                          id="id_sSTAddCity"
                          v-model="sSTAddCity"
                          :error-messages="cityErrors"
                          required
                          maxlength="20"
                          @input="$v.sSTAddCity.$touch()"
                          @blur="$v.sSTAddCity.$touch()"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="8">
                        <label class="inputLabel required" for="id_sSTAddState">State</label>
                        <v-text-field
                          placeholder="State"
                          solo
                          dense
                          id="id_sSTAddState"
                          v-model="sSTAddState"
                          :error-messages="stateErrors"
                          required
                          maxlength="2"
                          @input="sSTAddStateToUpper"
                          @blur="$v.sSTAddState.$touch()"
                        ></v-text-field>
                      </v-col>
                      <v-col cols="4">
                         <label class="inputLabel required" for="id_sSTAddZipCode">Zip Code</label>
                        <v-text-field
                          placeholder="Zip Code"
                          solo
                          dense
                          id="id_sSTAddZipCode"
                          type="tel"
                          v-model="sSTAddZipCode"
                          :error-messages="sSTAddZipCodeErrors"
                          required
                          @input="$v.sSTAddZipCode.$touch()"
                          @blur="$v.sSTAddZipCode.$touch()"
                          maxlength="10"
                        ></v-text-field>
                      </v-col>
                    </v-row>
                  </v-col>
                </div>

                <!-- MROSTPatientPickUp slot -->
                <div
                  v-if="
                    shipmentType.sNormalizedShipmentTypeName ==
                      'MROSTPatientPickUp' && $vuetify.breakpoint.xs
                  "
                >
                  <v-col
                    v-if="
                      sSelectedShipmentTypes == 'MROSTPatientPickUp' &&
                      $vuetify.breakpoint.xs
                    "
                    slot="MROSTPatientPickUp"
                    cols="12"
                    sm="12"
                  >
                    <!-- <div class="disclaimer">{{disclaimer}}</div> -->
                    <div v-if="pickUpInstruction != ''" class="disclaimer">
                      {{ pickUpInstruction }}
                    </div>
                  </v-col>
                </div>

                <!-- MROSTFax Block -->
                <div
                  v-if="
                    shipmentType.sNormalizedShipmentTypeName == 'MROSTFax' &&
                    $vuetify.breakpoint.xs
                  "
                >
                  <v-col
                    v-if="
                      sSelectedShipmentTypes == 'MROSTFax' &&
                      $vuetify.breakpoint.xs
                    "
                    slot="MROSTFax"
                    cols="12"
                    sm="12"
                  >
                    <label class="inputLabel required" for="id_sSTFaxNumber">Fax No</label>
                    <v-text-field
                      solo
                      dense
                      id="id_sSTFaxNumber"
                      placeholder="Fax No"
                      type="tel"
                      v-model="sSTFaxNumber"
                      :error-messages="sSTFaxNumberErrors"
                      required
                      maxlength="10"
                      @input="$v.sSTFaxNumber.$touch()"
                      @blur="$v.sSTFaxNumber.$touch()"
                    ></v-text-field>
                    <label class="inputLabel required" for="id_sSTConfirmFaxNumber">Confirm Fax No</label>
                    <v-text-field
                      placeholder="Confirm Fax No"
                      solo
                      dense
                      id="id_sSTConfirmFaxNumber"
                      type="tel"
                      v-model="sSTConfirmFaxNumber"
                      :error-messages="sSTConfirmFaxNumberErrors"
                      required
                      maxlength="10"
                      @input="$v.sSTConfirmFaxNumber.$touch()"
                      @blur="$v.sSTConfirmFaxNumber.$touch()"
                    ></v-text-field>
                  </v-col>
                </div>
              </v-col>
            </v-layout>
          </v-radio-group>
          <!-------------------- Non-Mobile View --------------------------------->
          <!-- MROSTEmail Block -->
          <div v-if="!$vuetify.breakpoint.xs">
            <v-row>
            <v-col
              v-if="sSelectedShipmentTypes == 'MROSTEmail'"
              slot="MROSTEmail"
              cols="12"
              sm="6"
            >
            <label class="inputLabel required" for="id_sSTEmailAddress">Email</label>
              <v-text-field
                solo
                dense
                placeholder="Email"
                id="id_sSTEmailAddress"
                v-model="sSTEmailAddress"
                :error-messages="sSTEmailAddressErrors"
                required
                maxlength="70"
                @input="$v.sSTEmailAddress.$touch()"
                @blur="$v.sSTEmailAddress.$touch()"
              ></v-text-field>
               </v-col>
              <v-col
              v-if="sSelectedShipmentTypes == 'MROSTEmail'"
              slot="MROSTEmail"
              cols="12"
              sm="6"
            >
             <label class="inputLabel required" for="id_sSTConfirmEmailId">Confirm Email</label>
              <v-text-field
                solo
                dense
                placeholder="Confirm Email"
                id="id_sSTConfirmEmailId"
                @paste.prevent
                v-model="sSTConfirmEmailId"
                :error-messages="sSTConfirmEmailIdErrors"
                required
                maxlength="70"
                @input="$v.sSTConfirmEmailId.$touch()"
                @blur="$v.sSTConfirmEmailId.$touch()"
              ></v-text-field>
           </v-col>
            </v-row>
          </div>

          <!-- MROSTMail Block -->
          <div v-if="!$vuetify.breakpoint.xs">
            <v-col
              v-if="sSelectedShipmentTypes == 'MROSTMail'"
              slot="MROSTMail"
              cols="12"
              sm="12"
            >
              <v-row>
                <v-col cols="12" sm="6">
                   <label class="inputLabel required" for="id_sSTAddStreetAddress">Address</label>
                  <v-text-field
                    placeholder="Address"
                    solo
                    dense
                    id="id_sSTAddStreetAddress"
                    v-model="sSTAddStreetAddress"
                    :error-messages="streetErrors"
                    required
                    maxlength="50"
                    @input="$v.sSTAddStreetAddress.$touch()"
                    @blur="$v.sSTAddStreetAddress.$touch()"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" sm="6">
                  <label class="inputLabel" for="id_sSTAddApartment">Address 2</label>
                  <v-text-field
                    placeholder="Address 2"
                    solo
                    dense
                    id="id_sSTAddApartment"
                    v-model="sSTAddApartment"
                    maxlength="50"
                  ></v-text-field>  
                </v-col>
                <v-col cols="4" sm="4">
                  <label class="inputLabel required" for="id_sSTAddCity">City</label>
                  <v-text-field
                    placeholder="City"
                    solo
                    dense
                    id="id_sSTAddCity"
                    v-model="sSTAddCity"
                    :error-messages="cityErrors"
                    required
                    maxlength="20"
                    @input="$v.sSTAddCity.$touch()"
                    @blur="$v.sSTAddCity.$touch()"
                  ></v-text-field>
                </v-col>
                <v-col cols="4" sm="4">
                  <label class="inputLabel required" for="id_sSTAddState">State</label>
                  <v-text-field
                    placeholder="State"
                    solo
                    dense
                    id="id_sSTAddState"
                    v-model="sSTAddState"
                    :error-messages="stateErrors"
                    required
                    maxlength="2"
                    @input="sSTAddStateToUpper"
                    @blur="$v.sSTAddState.$touch()"
                  ></v-text-field>
                </v-col>
                <v-col cols="4" sm="4">
                  <label class="inputLabel required" for="id_sSTAddZipCode">Zip Code</label>
                  <v-text-field
                    placeholder="Zip Code"
                    solo
                    dense
                    id="id_sSTAddZipCode"
                    type="tel"
                    v-model="sSTAddZipCode"
                    :error-messages="sSTAddZipCodeErrors"
                    required
                    @input="$v.sSTAddZipCode.$touch()"
                    @blur="$v.sSTAddZipCode.$touch()"
                    maxlength="10"
                  ></v-text-field>
                </v-col>
              </v-row>
            </v-col>
          </div>

          <!-- MROSTPatientPickUp slot -->
          <div v-if="!$vuetify.breakpoint.xs">
            <v-col
              v-if="sSelectedShipmentTypes == 'MROSTPatientPickUp'"
              slot="MROSTPatientPickUp"
              cols="12"
              sm="12"
            >
              <!-- <div class="disclaimer">{{disclaimer}}</div> -->
              <div v-if="pickUpInstruction != ''" class="disclaimer">
                {{ pickUpInstruction }}
              </div>
            </v-col>
          </div>

          <!-- MROSTFax Block -->
          <div v-if="!$vuetify.breakpoint.xs">
            <v-row>
            <v-col
              v-if="sSelectedShipmentTypes == 'MROSTFax'"
              slot="MROSTFax"
              cols="12"
              sm="6"
            >
            <label class="inputLabel required" for="id_sSTFaxNumber">Fax No</label>
              <v-text-field
                placeholder="Fax No"
                solo
                dense
                id="id_sSTFaxNumber"
                type="tel"
                v-model="sSTFaxNumber"
                :error-messages="sSTFaxNumberErrors"
                required
                maxlength="10"
                @input="$v.sSTFaxNumber.$touch()"
                @blur="$v.sSTFaxNumber.$touch()"
              ></v-text-field>
            </v-col>
             <v-col
              v-if="sSelectedShipmentTypes == 'MROSTFax'"
              slot="MROSTFax"
              cols="12"
              sm="6"
            >
            <label class="inputLabel required" for="id_sSTConfirmFaxNumber">Confirm Fax No</label>
              <v-text-field
                placeholder="Confirm Fax No"
                solo
                dense
                id="id_sSTConfirmFaxNumber"
                type="tel"
                v-model="sSTConfirmFaxNumber"
                :error-messages="sSTConfirmFaxNumberErrors"
                required
                maxlength="10"
                @input="$v.sSTConfirmFaxNumber.$touch()"
                @blur="$v.sSTConfirmFaxNumber.$touch()"
              ></v-text-field>
            </v-col>
            </v-row>
          </div>
          <!--------------------- Non-Mobile View End ------------------------------->
        </form>

        <Footer v-if="$vuetify.breakpoint.xs" />
      </div>
    </div>
    <div :class="$vuetify.breakpoint.xs ? 'buttonBlockMobile' : 'buttonBlock'">
      <div v-if="sSelectedShipmentTypes == 'MROSTEmail'">
        <v-btn
          :disabled="
            $v.sSTEmailAddress.$invalid || $v.sSTConfirmEmailId.$invalid
          "
          @click.once="nextPage"
          :key="buttonKey"
          class="smallBlackBtn"
          >Continue</v-btn
        >
      </div>
      <div
        v-if="
          sSelectedShipmentTypes == 'MROSTMail' &&
          $store.state.requestermodule.sReleaseTo == 'MROReleaseToMyself'
        "
      >
        <v-btn
          :disabled="
            $v.sSTAddStreetAddress.$invalid ||
            $v.sSTAddCity.$invalid ||
            $v.sSTAddState.$invalid ||
            $v.sSTAddZipCode.$invalid
          "
          @click.once="nextPage"
          :key="buttonKey"
          class="smallBlackBtn"
          >Continue</v-btn
        >
      </div>
      <div
        v-if="
          sSelectedShipmentTypes == 'MROSTMail' &&
          $store.state.requestermodule.sReleaseTo != 'MROReleaseToMyself'
        "
      >
        <v-btn
          :disabled="sSelectedShipmentTypes.length == 0"
          @click.once="nextPage"
          :key="buttonKey"
          class="smallBlackBtn"
          >Continue</v-btn
        >
      </div>
      <div v-if="sSelectedShipmentTypes == 'MROSTFax'">
        <v-btn
          :disabled="
            $v.sSTFaxNumber.$invalid || $v.sSTConfirmFaxNumber.$invalid
          "
          @click.once="nextPage"
          :key="buttonKey"
          class="smallBlackBtn"
          >Continue</v-btn
        >
      </div>
      <div
        v-if="
          sSelectedShipmentTypes != 'MROSTEmail' &&
          sSelectedShipmentTypes != 'MROSTMail' &&
          sSelectedShipmentTypes != 'MROSTFax'
        "
      >
        <v-btn
          :disabled="sSelectedShipmentTypes.length == 0"
          @click.once="nextPage"
          :key="buttonKey"
          class="smallBlackBtn"
          >Continue</v-btn
        >
      </div>
    </div>
    <Footer v-if="!$vuetify.breakpoint.xs" />
  </div>
</template>

<script>
import { mapState } from "vuex";
import { validationMixin } from "vuelidate";
import {
  required,
  email,
  sameAs,
  numeric,
  maxLength,
} from "vuelidate/lib/validators";
import Footer from "../Footer.vue";
export default {
  name: "WizardPage_06",
  activated() {
    this.buttonKey++;
    this.$store.commit(
      "ConfigModule/titleQuestion",
      "How would you like us to send your record(s)?"
    );
    if (this.sSelectedStateShipmentTypes.length == 0) {
      this.sSelectedShipmentTypes = "";
      this.sSelectedShipmentTypesName = "";
    }
    this.sSTAddZipCode = this.$store.state.requestermodule.sRecipientAddZipCode;
    this.sSTAddCity = this.$store.state.requestermodule.sRecipientAddCity;
    this.sSTAddState = this.$store.state.requestermodule.sRecipientAddState;
    this.sSTAddStreetAddress = this.$store.state.requestermodule.sRecipientAddStreetAddress;
    this.sSTAddApartment = this.$store.state.requestermodule.sRecipientAddApartment;
    console.log(this.sSelectedShipmentTypes);
  },
  components: {
    Footer,
  },
  computed: {
    //validations error message setter
    sSTFaxNumberErrors() {
      const errors = [];
      if (!this.$v.sSTFaxNumber.$dirty) return errors;
      !this.$v.sSTFaxNumber.numeric && errors.push("Fax must be numeric.");
      !this.$v.sSTFaxNumber.required && errors.push("Fax No is required.");
      return errors;
    },
    sSTConfirmFaxNumberErrors() {
      const errors = [];
      if (!this.$v.sSTConfirmFaxNumber.$dirty) return errors;
      !this.$v.sSTConfirmFaxNumber.numeric &&
        errors.push("Confirm Fax must be numeric.");
      !this.$v.sSTConfirmFaxNumber.sameAsFaxNo &&
        errors.push("Please enter correct Fax No");
      !this.$v.sSTConfirmFaxNumber.required &&
        errors.push("Fax No is required.");
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
      !this.$v.sSTAddZipCode.maxLength &&
        errors.push("ZipCode must be 10 digit");
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
    ...mapState({
      oShipmentTypeArray: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oShipmentTypes,
      pickUpInstruction: (state) =>
        state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper
          .Wizard_16_pickUpInstruction,
      sSelectedStateShipmentTypes: (state) =>
        state.requestermodule.sSelectedShipmentTypes,
    }),
  },
  data() {
    return {
      sSelectedShipmentTypes: this.$store.state.requestermodule
        .sSelectedShipmentTypes,
      sSelectedShipmentTypesName: this.$store.state.requestermodule
        .sSelectedShipmentTypesName,
      // combine previously entered data by requester

      sSTFaxNumber: "",
      sSTConfirmFaxNumber: "",
      sSTEmailAddress: "",
      sSTConfirmEmailId: "",
      // combine previously entered data by requester
      sSTAddZipCode: "",
      sSTAddCity: "",
      sSTAddState: "",
      sSTAddStreetAddress: "",
      sSTAddApartment: "",
      buttonKey: 1,
    };
  },
  methods: {
    sSTAddStateToUpper(val) {
      this.sSTAddState = val.toUpperCase();
    },
    nextPage() {
      console.log("nxt" + this.sSelectedShipmentTypes);
      // Before switching shipment type reset state data for shipment type
      this.resetSTState();
      //Switch based on selection and set state data
      switch (this.sSelectedShipmentTypes) {
        case "MROSTEmail":
          this.$store.commit(
            "requestermodule/sSTEmailAddress",
            this.sSTEmailAddress
          );
          break;
        case "MROSTMail":
          this.$store.commit(
            "requestermodule/sSTAddApartment",
            this.sSTAddApartment
          );
          this.$store.commit(
            "requestermodule/sSTAddZipCode",
            this.sSTAddZipCode
          );
          this.$store.commit("requestermodule/sSTAddCity", this.sSTAddCity);
          this.$store.commit("requestermodule/sSTAddState", this.sSTAddState);
          this.$store.commit(
            "requestermodule/sSTAddStreetAddress",
            this.sSTAddStreetAddress
          );
          this.$store.commit(
            "requestermodule/sRecipientAddZipCode",
            this.sSTAddZipCode
          );
          this.$store.commit(
            "requestermodule/sRecipientAddCity",
            this.sSTAddCity
          );
          this.$store.commit(
            "requestermodule/sRecipientAddState",
            this.sSTAddState
          );
          this.$store.commit(
            "requestermodule/sRecipientAddStreetAddress",
            this.sSTAddStreetAddress
          );
          this.$store.commit(
            "requestermodule/sRecipientAddApartment",
            this.sSTAddApartment
          );

          //}
          break;
        case "MROSTFax":
          this.$store.commit("requestermodule/sSTFaxNumber", this.sSTFaxNumber);
          break;
      }

      this.$store.commit(
        "requestermodule/sSelectedShipmentTypes",
        this.sSelectedShipmentTypes
      );
      this.$store.commit(
        "requestermodule/sSelectedShipmentTypesName",
        this.sSelectedShipmentTypesName
      );

      //Partial Requester Data Save Start
      this.$store.dispatch("requestermodule/partialAddReq");

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    //set variable based on selection
    check(shipment) {
      console.log(this.sSelectedShipmentTypes);
      this.sSelectedShipmentTypes = shipment.sNormalizedShipmentTypeName;
      this.sSelectedShipmentTypesName = shipment.sShipmentTypeName;
    },
    resetSTState() {
      this.$store.commit("requestermodule/sSTEmailAddress", "");
      this.$store.commit("requestermodule/sSTAddApartment", "");
      this.$store.commit("requestermodule/sSTAddZipCode", "");
      this.$store.commit("requestermodule/sSTAddCity", "");
      this.$store.commit("requestermodule/sSTAddState", "");
      this.$store.commit("requestermodule/sSTAddStreetAddress", "");
      this.$store.commit("requestermodule/sSTFaxNumber", "");
    },
  },
  mounted() {
    if (
      this.$store.state.requestermodule.sSTEmailAddress != "" ||
      this.$store.state.requestermodule.sSTFaxNumber != ""
    ) {
      this.sSTEmailAddress = this.$store.state.requestermodule.sSTEmailAddress;
      this.sSTConfirmEmailId = this.$store.state.requestermodule.sSTEmailAddress;
      this.sSTFaxNumber = this.$store.state.requestermodule.sSTFaxNumber;
      this.sSTConfirmFaxNumber = this.$store.state.requestermodule.sSTFaxNumber;
    } else {
      this.sSTEmailAddress = this.$store.state.requestermodule.sRequesterEmailId;
      this.sSTConfirmEmailId = this.$store.state.requestermodule.sRequesterEmailId;
    }
  },
  //Shipment type validations
  mixins: [validationMixin],
  validations: {
    sSTFaxNumber: {
      required,
      numeric,
    },
    sSTConfirmFaxNumber: {
      required,
      numeric,
      sameAsFaxNo: sameAs("sSTFaxNumber"),
    },
    sSTEmailAddress: {
      required,
      email,
    },
    sSTConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs("sSTEmailAddress"),
    },
    sSTAddZipCode: {
      required,
      maxLength: maxLength(10),
      numeric,
    },
    sSTAddCity: { required },
    sSTAddState: { required },
    sSTAddStreetAddress: { required },
  },
};
</script>
<style scoped>
.v-tooltip__content {
  color: black;
  background: white;
}
</style>