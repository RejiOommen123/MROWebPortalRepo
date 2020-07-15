<template>
  <div class="center">
    <form>
      <h1>Please provide recipient information.</h1>
      <v-row>
         <v-col  cols="12" offset-sm="1" sm="3" xs="3">
          <label for="sRecipientFirstName" class="control-label">FIRST NAME</label>
          <v-text-field
            v-model="sRecipientFirstName"
            :error-messages="sRecipientFirstNameError"
            required
            @input="$v.sRecipientFirstName.$touch()"
            @blur="$v.sRecipientFirstName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col  cols="12" sm="3" xs="3">
          <label for="sRecipientMiddleName" class="control-label">MIDDLE NAME</label>
          <v-text-field v-model="sRecipientMiddleName"></v-text-field>
        </v-col>
        <v-col cols="12" sm="3" xs="3">
          <label for="sRecipientLastName" class="control-label">LAST NAME</label>
          <v-text-field
            v-model="sRecipientLastName"
            :error-messages="sRecipientLastNameError"
            required
            @input="$v.sRecipientLastName.$touch()"
            @blur="$v.sRecipientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <v-divider></v-divider>
        <!-- TODO: v-if="MROAddApartment" -->
         <v-col cols="12" sm="6">
          <v-text-field
            v-model="sRecipientAddApartment"
            label="Apartment/Building"
          ></v-text-field>
        </v-col>
        <v-col  cols="12" sm="6">
          <v-text-field
            v-model="sRecipientAddStreetAddress"
            :error-messages="streetErrors"
            label="STREET"
            required
            @input="$v.sRecipientAddStreetAddress.$touch()"
            @blur="$v.sRecipientAddStreetAddress.$touch()"
          ></v-text-field>
        </v-col>
        <v-col offset-sm="1" cols="12" sm="3">
          <v-text-field
            v-model="sRecipientAddCity"
            :error-messages="cityErrors"
            label="CITY"
            required
            @input="$v.sRecipientAddCity.$touch()"
            @blur="$v.sRecipientAddCity.$touch()"
          ></v-text-field>
        </v-col>
        <v-col  offset-sm="1" cols="12"  sm="2">
          <v-text-field
            v-model="sRecipientAddState"
            :error-messages="stateErrors"
            label="STATE"
            required
            @input="$v.sRecipientAddState.$touch()"
            @blur="$v.sRecipientAddState.$touch()"
          ></v-text-field>
        </v-col>
        <v-col  offset-sm="1" cols="12" sm="3">
          <v-text-field
            type="tel"
            v-model="sRecipientAddZipCode"
            :error-messages="sRecipientAddZipCodeErrors"
            label="ZIP CODE"
            required
            @input="$v.sRecipientAddZipCode.$touch()"
            @blur="$v.sRecipientAddZipCode.$touch()"
            minlength="5"
            maxlength="5"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="2" sm="8">
          <div class="disclaimer">{{disclaimer}}</div>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-btn @click.prevent="nextPage" :disabled="$v.$invalid" class="next">Next</v-btn>
        </v-col>
      </v-row>
    </form>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import {
  required,
  maxLength,
  minLength,
  numeric
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      sRecipientFirstName: "",
      sRecipientLastName: "",
      sRecipientMiddleName: "",
      sRecipientAddZipCode: '',
      sRecipientAddCity:'',
      sRecipientAddState: '',
      sRecipientAddStreetAddress: '',
      sRecipientAddApartment:'',

      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_07_disclaimer01,

      //Show and Hide Fields Values
    //   MROAddZipCode: this.$store.state.ConfigModule.apiResponseDataByLocation
    //     .oFields.MROAddZipCode,
    //   MROAddCity: this.$store.state.ConfigModule.apiResponseDataByLocation
    //     .oFields.MROAddCity,
    //   MROAddState: this.$store.state.ConfigModule.apiResponseDataByLocation
    //     .oFields.MROAddState,
    //   MROAddStreetAddress: this.$store.state.ConfigModule
    //     .apiResponseDataByLocation.oFields.MROAddStreetAddress
    };
  },
  //Requester address validations
  mixins: [validationMixin],
  validations: {
    sRecipientFirstName: { required, maxLength: maxLength(15) },
    sRecipientLastName: { required, maxLength: maxLength(15) },
    sRecipientAddZipCode: {
      required,
      maxLength: maxLength(5),
      minLength: minLength(5),
      numeric
    },
    sRecipientAddCity: { required },
    sRecipientAddState: { required },
    sRecipientAddStreetAddress: { required }
  },
  computed: {
    sRecipientFirstNameError() {
      const errors = [];
      if (!this.$v.sRecipientFirstName.$dirty) return errors;
      !this.$v.sRecipientFirstName.maxLength &&
        errors.push("First Name must be at most 15 characters long");
      !this.$v.sRecipientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sRecipientLastNameError() {
      const errors = [];
      if (!this.$v.sRecipientLastName.$dirty) return errors;
      !this.$v.sRecipientLastName.maxLength &&
        errors.push("Last Name must be at most 15 characters long");
      !this.$v.sRecipientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    //Requester address validation error message setter
    sRecipientAddZipCodeErrors() {
      const errors = [];
      if (!this.$v.sRecipientAddZipCode.$dirty) return errors;
      !this.$v.sRecipientAddZipCode.maxLength && errors.push("ZipCode must be 5 digit");
      !this.$v.sRecipientAddZipCode.minLength && errors.push("ZipCode must be 5 digit");
      !this.$v.sRecipientAddZipCode.numeric && errors.push("ZipCode must me numeric.");
      !this.$v.sRecipientAddZipCode.required && errors.push("ZipCode is required.");
      return errors;
    },
    cityErrors() {
      const errors = [];
      if (!this.$v.sRecipientAddCity.$dirty) return errors;
      !this.$v.sRecipientAddCity.required && errors.push("City is required.");
      return errors;
    },
    stateErrors() {
      const errors = [];
      if (!this.$v.sRecipientAddState.$dirty) return errors;
      !this.$v.sRecipientAddState.required && errors.push("State is required.");
      return errors;
    },
    streetErrors() {
      const errors = [];
      if (!this.$v.sRecipientAddStreetAddress.$dirty) return errors;
      !this.$v.sRecipientAddStreetAddress.required &&
        errors.push("Street Address is required.");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sRecipientFirstName",this.sRecipientFirstName);
      this.$store.commit("requestermodule/sRecipientLastName",this.sRecipientLastName);
      this.$store.commit("requestermodule/sRecipientMiddleName",this.sRecipientMiddleName);
      this.$store.commit("requestermodule/sRecipientAddZipCode", this.sRecipientAddZipCode);
      this.$store.commit("requestermodule/sRecipientAddCity", this.sRecipientAddCity);
      this.$store.commit("requestermodule/sRecipientAddState", this.sRecipientAddState);
      this.$store.commit("requestermodule/sRecipientAddStreetAddress",this.sRecipientAddStreetAddress);
      this.$store.commit("requestermodule/sRecipientAddApartment",this.sRecipientAddApartment);

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
    }
  }
};
</script>
