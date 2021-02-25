<template>
  <div class="center">
    <form>
      <h1>Please provide recipient information.</h1>
      <v-row>
         <v-col style="padding-bottom:0px;padding-top:0px" cols="6" offset-sm="2" sm="4">
          <v-text-field
            label="FIRST NAME"
            v-model="sRecipientFirstName"
            :error-messages="sRecipientFirstNameError"
            required
            maxlength="30"
            @input="$v.sRecipientFirstName.$touch()"
            @blur="$v.sRecipientFirstName.$touch()"
          ></v-text-field>
        </v-col>
        <!-- <v-col v-if="MRORecipientMiddleName"  cols="12" sm="3" xs="3">
          <v-text-field label="MIDDLE NAME" v-model="sRecipientMiddleName"></v-text-field>
        </v-col> -->
        <v-col style="padding-bottom:0px;padding-top:0px" cols="6" sm="4">
          <v-text-field
            label="LAST NAME"
            v-model="sRecipientLastName"
            :error-messages="sRecipientLastNameError"
            required
            maxlength="30"
            @input="$v.sRecipientLastName.$touch()"
            @blur="$v.sRecipientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col style="padding-bottom:0px;padding-top:0px" cols="12" offset-sm="3" sm="6">
          <v-text-field
            label="ORGANIZATION NAME"
            v-model="sRecipientOrganizationName"
            maxlength="50"
          ></v-text-field>
        </v-col>
        <!-- TODO: v-if="MROAddApartment" -->
        
        <v-col style="padding-bottom:0px;padding-top:0px" cols="12" offset-sm="2" sm="8">
          <v-text-field
            v-model="sRecipientAddStreetAddress"
            :error-messages="streetErrors"
            label="STREET"
            required
            maxlength="50"
            @input="$v.sRecipientAddStreetAddress.$touch()"
            @blur="$v.sRecipientAddStreetAddress.$touch()"
          ></v-text-field>
        </v-col>
         <v-col style="padding-bottom:0px;padding-top:0px" v-if="MRORecipientAddApartment" cols="12" offset-sm="2" sm="8">
          <v-text-field
            v-model="sRecipientAddApartment"
            label="APARTMENT/BUILDING"
            maxlength="50"
          ></v-text-field>
        </v-col>
        <v-col style="padding-bottom:0px;padding-top:0px" offset-sm="1" cols="12" sm="3">
          <v-text-field
            v-model="sRecipientAddCity"
            :error-messages="cityErrors"
            label="CITY"
            required
            maxlength="20"
            @input="$v.sRecipientAddCity.$touch()"
            @blur="$v.sRecipientAddCity.$touch()"
          ></v-text-field>
        </v-col>
        <v-col style="padding-bottom:0px;padding-top:0px"  offset-sm="1" cols="12"  sm="2">
          <v-text-field
            v-model="sRecipientAddState"
            :error-messages="stateErrors"
            label="STATE"
            required
            maxlength="2"
            @input="sRecipientAddStateToUpper"
            @blur="$v.sRecipientAddState.$touch()"
          ></v-text-field>
        </v-col>
        <v-col style="padding-bottom:0px;padding-top:0px" offset-sm="1" cols="12" sm="3">
          <v-text-field
            type="tel"
            v-model="sRecipientAddZipCode"
            :error-messages="sRecipientAddZipCodeErrors"
            label="ZIP CODE"
            required
            @input="$v.sRecipientAddZipCode.$touch()"
            @blur="$v.sRecipientAddZipCode.$touch()"
            maxlength="10"
          ></v-text-field>
        </v-col>
        <v-col style="padding-bottom:0px" cols="12" offset-sm="2" sm="8">
          <div class="disclaimer">{{disclaimer}}</div>
        </v-col>
        <v-col  style="padding-top:0px" cols="12" offset-sm="3" sm="6">
          <v-btn  @click.prevent="nextPage" :disabled="$v.$invalid" class="next">Next</v-btn>
        </v-col>
      </v-row>
    </form>
  </div>
</template>
<script>
import { mapState } from 'vuex';
import { validationMixin } from "vuelidate";
import {
  required,
  maxLength,
  numeric
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_14",
  mounted(){
    if(this.$store.state.requestermodule.sReleaseTo=='MROReleaseToMyself')
    {
      if(this.$store.state.requestermodule.bAreYouPatient)
      {
        this.sRecipientFirstName = this.$store.state.requestermodule.sPatientFirstName;
        this.sRecipientLastName=this.$store.state.requestermodule.sPatientLastName;
      }
      else
      {
        this.sRecipientFirstName = this.$store.state.requestermodule.sRelativeFirstName;
        this.sRecipientLastName=this.$store.state.requestermodule.sRelativeLastName;
      }
      this.sRecipientAddZipCode= this.$store.state.requestermodule.sAddZipCode,
      this.sRecipientAddCity=this.$store.state.requestermodule.sAddCity,
      this.sRecipientAddState= this.$store.state.requestermodule.sAddState,
      this.sRecipientAddStreetAddress =this.$store.state.requestermodule.sAddStreetAddress,
      this.sRecipientAddApartment = this.$store.state.requestermodule.sAddApartment;
    }
    else
    {
      this.sRecipientFirstName = '';
      this.sRecipientLastName='';
      this.sRecipientAddZipCode= '';
      this.sRecipientAddCity='';
      this.sRecipientAddState= '';
      this.sRecipientAddStreetAddress ='';
      this.sRecipientAddApartment = '';
    }
  },
  data() {
    return {
      sRecipientFirstName: "",
      sRecipientLastName: "",
      sRecipientOrganizationName: "",
      sRecipientAddZipCode: '',
      sRecipientAddCity:'',
      sRecipientAddState: '',
      sRecipientAddStreetAddress: '',
      sRecipientAddApartment:'',
    };
  },
  //Requester address validations
  mixins: [validationMixin],
  validations: {
    sRecipientFirstName: { required, maxLength: maxLength(30) },
    sRecipientLastName: { required, maxLength: maxLength(30) },
    sRecipientAddZipCode: {
      required,
      maxLength: maxLength(10),
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
        errors.push("First Name must be at most 30 characters long");
      !this.$v.sRecipientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sRecipientLastNameError() {
      const errors = [];
      if (!this.$v.sRecipientLastName.$dirty) return errors;
      !this.$v.sRecipientLastName.maxLength &&
        errors.push("Last Name must be at most 30 characters long");
      !this.$v.sRecipientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    //Requester address validation error message setter
    sRecipientAddZipCodeErrors() {
      const errors = [];
      if (!this.$v.sRecipientAddZipCode.$dirty) return errors;
      !this.$v.sRecipientAddZipCode.maxLength && errors.push("ZipCode must be 10 digit");
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
    },
    ...mapState({
      disclaimer : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_14_disclaimer01,
     // Show and Hide Fields Values
      MRORecipientMiddleName: state => state.ConfigModule
      .apiResponseDataByLocation.oFields.MRORecipientMiddleName,
      MRORecipientAddApartment: state => state.ConfigModule
      .apiResponseDataByLocation.oFields.MRORecipientAddApartment,
    }),
  },
  methods: {
    sRecipientAddStateToUpper(val) {
      this.sRecipientAddState = val.toUpperCase()
    },
    nextPage() {
      this.$store.commit("requestermodule/sRecipientFirstName",this.sRecipientFirstName);
      this.$store.commit("requestermodule/sRecipientLastName",this.sRecipientLastName);
      this.$store.commit("requestermodule/sRecipientOrganizationName",this.sRecipientOrganizationName);
      this.$store.commit("requestermodule/sRecipientAddZipCode", this.sRecipientAddZipCode);
      this.$store.commit("requestermodule/sRecipientAddCity", this.sRecipientAddCity);
      this.$store.commit("requestermodule/sRecipientAddState", this.sRecipientAddState);
      this.$store.commit("requestermodule/sRecipientAddStreetAddress",this.sRecipientAddStreetAddress);
      this.$store.commit("requestermodule/sRecipientAddApartment",this.sRecipientAddApartment);

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');
      
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
