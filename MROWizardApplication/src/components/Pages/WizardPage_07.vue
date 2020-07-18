<template>
  <div class="center">
    <form>
      <h1>What is your mailing address?</h1>
      <v-row>
        <!-- TODO: v-if="MROAddApartment" -->
        <v-col cols="12" style="padding-bottom:0px" offset-sm="2" sm="8">
          <v-text-field
            v-model="sAddStreetAddress"
            :error-messages="streetErrors"
            label="STREET"
            required
            @input="$v.sAddStreetAddress.$touch()"
            @blur="$v.sAddStreetAddress.$touch()"
          ></v-text-field>
        </v-col>
         <v-col  style="padding-bottom:0px" v-if="MROAddApartment" cols="12" offset-sm="2" sm="8">
          <v-text-field
            v-model="sAddApartment"
            label="APARTMENT/BUILDING"
          ></v-text-field>
        </v-col>
        <v-col  style="padding-bottom:0px" offset-sm="1" cols="12" sm="3">
          <v-text-field
            v-model="sAddCity"
            :error-messages="cityErrors"
            label="CITY"
            required
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
            @input="$v.sAddState.$touch()"
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
            minlength="5"
            maxlength="5"
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
  minLength,
  numeric
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      sAddZipCode: '',
      sAddCity:'',
      sAddState: '',
      sAddStreetAddress: '',
      sAddApartment:'',

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
      maxLength: maxLength(5),
      minLength: minLength(5),
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
      !this.$v.sAddZipCode.maxLength && errors.push("ZipCode must be 5 digit");
      !this.$v.sAddZipCode.minLength && errors.push("ZipCode must be 5 digit");
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
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
      if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
      {
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      }
      //Partial Requester Data Save End
      console.log(JSON.stringify(this.$store.state.requestermodule));
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
