<template>
  <div>
    <div class="pageBody">
      <div class="pageContent">
    <form> 
  

     
     
        <v-radio-group
          v-model="sSelectedReleaseTo"
        >
          <v-layout
            v-for="releaseTo in oReleaseToArray"
            :key="releaseTo.sNormalizedReleaseTo"
            row
            wrap
          >
      <v-col cols="12" offset-sm="2" sm="8">
         <v-radio
          class="checkboxBorder"
          color="customText"
          :label="releaseTo.sReleaseTo"
          :value="releaseTo.sNormalizedReleaseTo" 
          @change="check(releaseTo)"
          >
          </v-radio>

          <!-------------------- Mobile View ---------------------------->
               
        <div v-if="sSelectedReleaseTo==releaseTo.sNormalizedReleaseTo && $vuetify.breakpoint.xs">
          <v-row>
         <v-col style="padding-bottom:0px;padding-top:0px" cols="6">
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

        <v-col style="padding-bottom:0px;padding-top:0px" cols="6">
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
          </v-row>
        </div>

        <!-------------------- End of Mobile View ---------------------------->
          </v-col> 
       </v-layout>  
         
		</v-radio-group> 
    
    
    <!------------------------ Non Mobile View ---------------------------->
    <div v-if="sSelectedReleaseTo!= '' && !$vuetify.breakpoint.xs">
      <v-row>
         <v-col sm="4">
          <label class="inputLabel required" for="id_sRecipientFirstName">First Name</label>
          <v-text-field
            solo
            dense
            placeholder="First Name"
            id="id_sRecipientFirstName"
            v-model="sRecipientFirstName"
            :error-messages="sRecipientFirstNameError"
            required
            maxlength="30"
            @input="$v.sRecipientFirstName.$touch()"
            @blur="$v.sRecipientFirstName.$touch()"
          ></v-text-field>
          
        </v-col>

        <v-col sm="4">
          <label class="inputLabel required" for="id_sRecipientLastName">Last Name</label>
          <v-text-field
            solo
            dense
            placeholder="Last Name"
            id="id_sRecipientLastName"
            v-model="sRecipientLastName"
            :error-messages="sRecipientLastNameError"
            required
            maxlength="30"
            @input="$v.sRecipientLastName.$touch()"
            @blur="$v.sRecipientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col sm="4">
          <label class="inputLabel" for="id_sRecipientOrganizationName">Organization Name</label>
          <v-text-field
            solo
            dense
            placeholder="Organization Name"
            id="id_sRecipientOrganizationName"
            v-model="sRecipientOrganizationName"
            maxlength="50"
          ></v-text-field>
        </v-col>
        <!-- TODO: v-if="MROAddApartment" -->
        
        <v-col sm="6">
          <label class="inputLabel required" for="id_sRecipientAddStreetAddress">Address</label>
          <v-text-field
            solo
            dense
            placeholder="Address"
            id="id_sRecipientOrganizationName"
            v-model="sRecipientAddStreetAddress"
            :error-messages="streetErrors"
            required
            maxlength="50"
            @input="$v.sRecipientAddStreetAddress.$touch()"
            @blur="$v.sRecipientAddStreetAddress.$touch()"
          ></v-text-field>
        </v-col>
         <v-col v-if="MRORecipientAddApartment" sm="6">
           <label class="inputLabel" for="id_sRecipientAddApartment">Address 2</label>
          <v-text-field
            solo
            dense
            placeholder="Address 2"
            id="id_sRecipientAddApartment"
            v-model="sRecipientAddApartment"
            maxlength="50"
          ></v-text-field>
        </v-col>
        <v-col sm="4">
          <label class="inputLabel required" for="id_sRecipientAddCity">City</label>
          <v-text-field
            solo
            dense
            placeholder="City"
            id="id_sRecipientAddCity"
            v-model="sRecipientAddCity"
            :error-messages="cityErrors"
            required
            maxlength="20"
            @input="$v.sRecipientAddCity.$touch()"
            @blur="$v.sRecipientAddCity.$touch()"
          ></v-text-field>
        </v-col>
        <v-col sm="4">
          <label class="inputLabel required" for="id_sRecipientAddState">State</label>
          <v-text-field
            solo
            dense
            placeholder="State"
            id="id_sRecipientAddState"
            v-model="sRecipientAddState"
            :error-messages="stateErrors"
            required
            maxlength="2"
            @input="sRecipientAddStateToUpper"
            @blur="$v.sRecipientAddState.$touch()"
          ></v-text-field>
        </v-col>
        <v-col sm="4">
          <label class="inputLabel required" for="id_sRecipientAddZipCode">Zip Code</label>
          <v-text-field
            solo
            dense
            placeholder="Zip Code"
            id="id_sRecipientAddZipCode"
            type="tel"
            v-model="sRecipientAddZipCode"
            :error-messages="sRecipientAddZipCodeErrors"
            required
            @input="$v.sRecipientAddZipCode.$touch()"
            @blur="$v.sRecipientAddZipCode.$touch()"
            maxlength="10"
          ></v-text-field>
        </v-col>
        <v-col sm="12">
          <div class="disclaimer">{{disclaimer}}</div>
        </v-col>
      </v-row>
        </div>
        <!------------------------ End of Non Mobile View ---------------------------->
    
     </form> 
      
      </div>
        <Footer v-if="$vuetify.breakpoint.xs" />
    </div>
    <div :class="$vuetify.breakpoint.xs ? 'buttonBlockMobile' : 'buttonBlock'">
      <v-btn  @click.once="nextPage" :key="buttonKey" :disabled="$v.$invalid" class="smallBlackBtn">Continue</v-btn>
    </div>
     <Footer v-if="!$vuetify.breakpoint.xs" />
  </div>
</template>
<script>
import { mapState } from "vuex";
import { validationMixin } from "vuelidate";
import { required, numeric, maxLength } from "vuelidate/lib/validators";
import Footer from '../Footer.vue';
export default {
  name: "WizardPage_05",
  components:{
    Footer
  },
  activated() {
    this.buttonKey++;
    this.$store.commit(
      "ConfigModule/titleQuestion",
      "To whom should we release these medical records to?"
    );
    if (
      this.$store.state.requestermodule.sRecipientFirstName != "" ||
      this.$store.state.requestermodule.sRecipientAddStreetAddress != ""
    ) {
      this.sRecipientFirstName = this.$store.state.requestermodule.sRecipientFirstName;
      this.sRecipientLastName = this.$store.state.requestermodule.sRecipientLastName;
      this.sRecipientOrganizationName = this.$store.state.requestermodule.sRecipientOrganizationName;
      this.sRecipientAddZipCode = this.$store.state.requestermodule.sRecipientAddZipCode;
      this.sRecipientAddCity = this.$store.state.requestermodule.sRecipientAddCity;
      this.sRecipientAddState = this.$store.state.requestermodule.sRecipientAddState;
      this.sRecipientAddStreetAddress = this.$store.state.requestermodule.sRecipientAddStreetAddress;
      this.sRecipientAddApartment = this.$store.state.requestermodule.sRecipientAddApartment;
    }
    // else
    // {
    //   if (this.$store.state.requestermodule.sReleaseTo == "MROReleaseToMyself")
    //   {
    //     if (this.$store.state.requestermodule.bAreYouPatient)
    //     {
    //       this.sRecipientFirstName = this.$store.state.requestermodule.sPatientFirstName;
    //       this.sRecipientLastName = this.$store.state.requestermodule.sPatientLastName;
    //     }
    //     else
    //     {
    //       this.sRecipientFirstName = this.$store.state.requestermodule.sRelativeFirstName;
    //       this.sRecipientLastName = this.$store.state.requestermodule.sRelativeLastName;
    //     }
    //     this.sRecipientAddZipCode = this.$store.state.requestermodule.sAddZipCode,
    //     this.sRecipientAddCity = this.$store.state.requestermodule.sAddCity,
    //     this.sRecipientAddState = this.$store.state.requestermodule.sAddState,
    //     this.sRecipientAddStreetAddress = this.$store.state.requestermodule.sAddStreetAddress,
    //     this.sRecipientAddApartment = this.$store.state.requestermodule.sAddApartment;
    //   }
    // }
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
      !this.$v.sRecipientAddZipCode.maxLength &&
        errors.push("ZipCode must be 10 digit");
      !this.$v.sRecipientAddZipCode.numeric &&
        errors.push("ZipCode must me numeric.");
      !this.$v.sRecipientAddZipCode.required &&
        errors.push("ZipCode is required.");
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
      disclaimer: (state) =>
        state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper
          .Wizard_15_disclaimer01,
      // Show and Hide Fields Values
      MRORecipientMiddleName: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORecipientMiddleName,
      MRORecipientAddApartment: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORecipientAddApartment,
    }),
  },
  data() {
    return {
      oReleaseToArray: this.$store.state.ConfigModule.oReleaseRequestTo,
      sActiveBtn: this.$store.state.requestermodule.sReleaseTo,
      sRecipientFirstName: "",
      sRecipientLastName: "",
      sRecipientOrganizationName: "",
      sRecipientAddZipCode: "",
      sRecipientAddCity: "",
      sRecipientAddState: "",
      sRecipientAddStreetAddress: "",
      sRecipientAddApartment: "",
      buttonKey: 1,
      sSelectedReleaseTo: this.$store.state.requestermodule.sReleaseTo,
      sSelectedReleaseToName: this.$store.state.requestermodule.sReleaseToName,
    };
  },
  methods: {
    releaseRequestTo(releaseTo) {
      this.sActiveBtn = releaseTo.sNormalizedReleaseTo;
      this.$store.commit(
        "requestermodule/sReleaseTo",
        releaseTo.sNormalizedReleaseTo
      );
      this.$store.commit(
        "requestermodule/sReleaseToName",
        releaseTo.sReleaseTo
      );

      // not sure to remove dispatch or not
      //Partial Requester Data Save Start
      this.$store.dispatch("requestermodule/partialAddReq");

      //this.$store.commit("ConfigModule/mutateNextIndex");
    },
    sRecipientAddStateToUpper(val) {
      this.sRecipientAddState = val.toUpperCase();
    },
    nextPage() {
      //this.sActiveBtn=releaseTo.sNormalizedReleaseTo;
      this.$store.commit("requestermodule/sReleaseTo", this.sSelectedReleaseTo);
      this.$store.commit(
        "requestermodule/sReleaseToName",
        this.sSelectedReleaseToName
      );

      this.$store.commit(
        "requestermodule/sRecipientFirstName",
        this.sRecipientFirstName
      );
      this.$store.commit(
        "requestermodule/sRecipientLastName",
        this.sRecipientLastName
      );
      this.$store.commit(
        "requestermodule/sRecipientOrganizationName",
        this.sRecipientOrganizationName
      );
      this.$store.commit(
        "requestermodule/sRecipientAddZipCode",
        this.sRecipientAddZipCode
      );
      this.$store.commit(
        "requestermodule/sRecipientAddCity",
        this.sRecipientAddCity
      );
      this.$store.commit(
        "requestermodule/sRecipientAddState",
        this.sRecipientAddState
      );
      this.$store.commit(
        "requestermodule/sRecipientAddStreetAddress",
        this.sRecipientAddStreetAddress
      );
      this.$store.commit(
        "requestermodule/sRecipientAddApartment",
        this.sRecipientAddApartment
      );

      //Partial Requester Data Save Start
      this.$store.dispatch("requestermodule/partialAddReq");

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(releaseTo) {
      this.sSelectedReleaseToName = releaseTo.sReleaseTo;
      this.sSelectedReleaseTo = releaseTo.sNormalizedReleaseTo;

      // if (this.$store.state.requestermodule.sRecipientFirstName != "" || this.$store.state.requestermodule.sRecipientAddStreetAddress != "")
      // {
      //   this.sRecipientFirstName = this.$store.state.requestermodule.sRecipientFirstName;
      //   this.sRecipientLastName = this.$store.state.requestermodule.sRecipientLastName;
      //   this.sRecipientOrganizationName = this.$store.state.requestermodule.sRecipientOrganizationName;
      //   this.sRecipientAddZipCode = this.$store.state.requestermodule.sRecipientAddZipCode;
      //   this.sRecipientAddCity = this.$store.state.requestermodule.sRecipientAddCity;
      //   this.sRecipientAddState = this.$store.state.requestermodule.sRecipientAddState;
      //   this.sRecipientAddStreetAddress = this.$store.state.requestermodule.sRecipientAddStreetAddress;
      //   this.sRecipientAddApartment = this.$store.state.requestermodule.sRecipientAddApartment;
      // }
      // else
      // {
      if (this.sSelectedReleaseTo == "MROReleaseToMyself") {
        if (this.$store.state.requestermodule.bAreYouPatient) {
          this.sRecipientFirstName = this.$store.state.requestermodule.sPatientFirstName;
          this.sRecipientLastName = this.$store.state.requestermodule.sPatientLastName;
        } else {
          this.sRecipientFirstName = this.$store.state.requestermodule.sRelativeFirstName;
          this.sRecipientLastName = this.$store.state.requestermodule.sRelativeLastName;
        }
        (this.sRecipientAddZipCode = this.$store.state.requestermodule.sAddZipCode),
          (this.sRecipientAddCity = this.$store.state.requestermodule.sAddCity),
          (this.sRecipientAddState = this.$store.state.requestermodule.sAddState),
          (this.sRecipientAddStreetAddress = this.$store.state.requestermodule.sAddStreetAddress),
          (this.sRecipientAddApartment = this.$store.state.requestermodule.sAddApartment);
      } else {
        this.sRecipientFirstName = "";
        this.sRecipientLastName = "";
        this.sRecipientOrganizationName = "";
        this.sRecipientAddZipCode = "";
        this.sRecipientAddCity = "";
        this.sRecipientAddState = "";
        this.sRecipientAddStreetAddress = "";
        this.sRecipientAddApartment = "";

        this.$v.sRecipientFirstName.$reset();
        this.$v.sRecipientLastName.$reset();
        this.$v.sRecipientOrganizationName.$reset();
        this.$v.sRecipientAddZipCode.$reset();
        this.$v.sRecipientAddCity.$reset();
        this.$v.sRecipientAddState.$reset();
        this.$v.sRecipientAddStreetAddress.$reset();
        this.$v.sRecipientAddApartment.$reset();
      }
      //}

      //  console.log(releaseTo);
      //this.sSelectedPrimaryReasons = [];
      //this.sSelectedPrimaryReasons.push(primaryReason.sNormalizedPrimaryReasonName);
      //this.sSelectedPrimaryReasonsName=primaryReason.sPrimaryReasonName;
      //if (this.sSelectedPrimaryReasons == "MROOtherPrimaryReason") {
      //this.bOther = true;
      //this.sSelectedPrimaryReasonsName=this.sOtherPrimaryReasons;
      //}
      //else{
      //this.bOther=false;
      //this.sOtherPrimaryReasons='';
      //}
    },
  },
  //Requester address validations
  mixins: [validationMixin],
  validations: {
    sRecipientFirstName: { required, maxLength: maxLength(30) },
    sRecipientLastName: { required, maxLength: maxLength(30) },
    sRecipientAddZipCode: {
      required,
      maxLength: maxLength(10),
      numeric,
    },
    sRecipientAddCity: { required },
    sRecipientAddState: { required },
    sRecipientAddStreetAddress: { required },
  },
};
</script>
