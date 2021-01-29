<template>
  <div class="center">
    <form>
      <h1>What is your mailing address?</h1>
      <v-row>
        <v-col cols="12" style="padding-bottom:0px" offset-sm="2" sm="8">
          <v-text-field
            v-model="sAddStreetAddress"
            :error-messages="streetErrors"
            label="STREET"
            maxlength="50"
            required
            @input="$v.sAddStreetAddress.$touch()"
            @blur="$v.sAddStreetAddress.$touch()"
          ></v-text-field>
        </v-col>
         <v-col  style="padding-bottom:0px" v-if="MROAddApartment" cols="12" offset-sm="2" sm="8">
          <v-text-field
            v-model="sAddApartment"
            label="APARTMENT/BUILDING"
            maxlength="50"
          ></v-text-field>
        </v-col>
        <v-col  style="padding-bottom:0px" offset-sm="1" cols="12" sm="3">
          <v-text-field
            v-model="sAddCity"
            :error-messages="cityErrors"
            label="CITY"
            required
            maxlength="20"
            @input="$v.sAddCity.$touch()"
            @blur="$v.sAddCity.$touch()"
          ></v-text-field>
        </v-col>
        <v-col  style="padding-bottom:0px" offset-sm="1" cols="12"  sm="2">
          <v-text-field
            v-model="sAddState"
            :error-messages="stateErrors"
            label="STATE"
            required
            maxlength="2"
            @input="sAddStateToUpper"
            @blur="$v.sAddState.$touch()"
          ></v-text-field>
        </v-col>
        <v-col  style="padding-bottom:0px" offset-sm="1" cols="12" sm="3">
          <v-text-field
            type="tel"
            v-model="sAddZipCode"
            :error-messages="sAddZipCodeErrors"
            label="ZIP CODE"
            required
            @input="$v.sAddZipCode.$touch()"
            @blur="$v.sAddZipCode.$touch()"
            maxlength="10"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="2" sm="8">
          <div v-if="disclaimer!=''" class="disclaimer">{{disclaimer}}</div>
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
  numeric
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      sAddZipCode : this.$store.state.requestermodule.sAddZipCode,
      sAddCity : this.$store.state.requestermodule.sAddCity,
      sAddState : this.$store.state.requestermodule.sAddState,
      sAddStreetAddress : this.$store.state.requestermodule.sAddStreetAddress,
      sAddApartment : this.$store.state.requestermodule.sAddApartment,

      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_07_disclaimer01,

      //Show and Hide Fields Values
      MROAddApartment: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROAddApartment,
    };
  },
  //Requester address validations
  mixins: [validationMixin],
  validations: {
    sAddZipCode: {
      required,
      maxLength: maxLength(10),
      numeric
    },
    sAddCity: { required },
    sAddState: { required },
    sAddStreetAddress: { required }
  },
  computed: {
    //Requester address validation error message setter
    sAddZipCodeErrors() {
      const errors = [];
      if (!this.$v.sAddZipCode.$dirty) return errors;
      !this.$v.sAddZipCode.maxLength && errors.push("ZipCode must be 10 digit");
      !this.$v.sAddZipCode.numeric && errors.push("ZipCode must me numeric.");
      !this.$v.sAddZipCode.required && errors.push("ZipCode is required.");
      return errors;
    },
    cityErrors() {
      const errors = [];
      if (!this.$v.sAddCity.$dirty) return errors;
      !this.$v.sAddCity.required && errors.push("City is required.");
      return errors;
    },
    stateErrors() {
      const errors = [];
      if (!this.$v.sAddState.$dirty) return errors;
      !this.$v.sAddState.required && errors.push("State is required.");
      return errors;
    },
    streetErrors() {
      const errors = [];
      if (!this.$v.sAddStreetAddress.$dirty) return errors;
      !this.$v.sAddStreetAddress.required &&
        errors.push("Street Address is required.");
      return errors;
    }
  },
  methods: {
    sAddStateToUpper(val) {
      this.sAddState = val.toUpperCase()
    },
    nextPage() {

      this.$store.commit("requestermodule/sAddZipCode", this.sAddZipCode);
      this.$store.commit("requestermodule/sAddCity", this.sAddCity);
      this.$store.commit("requestermodule/sAddState", this.sAddState);
      this.$store.commit(
        "requestermodule/sAddStreetAddress",
        this.sAddStreetAddress
      );
      this.$store.commit("requestermodule/sAddApartment",this.sAddApartment);

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');
      // console.log(JSON.stringify(this.$store.state.requestermodule));
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
