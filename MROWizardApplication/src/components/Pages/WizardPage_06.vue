<template>
  <div class="center">
      <h1>Please provide your email address to receive a confirmation of your request.</h1>
      <v-row>
        <v-col  cols="12" offset-sm="2" sm="8">
            <v-text-field
              label="EMAIL"
              v-model="emailValid.sRequesterEmailId"
              :error-messages="emailErrors"
              required
              maxlength="70"
              :disabled="inputDisabled"
              @input="sRequesterEmailIdToLower"
              @blur="$v.emailValid.sRequesterEmailId.$touch()"
            ></v-text-field>
            <v-text-field
            label="CONFIRM EMAIL"
              @paste.prevent
              v-model="emailValid.sConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              maxlength="70"
              :disabled="inputDisabled"
              @input="sConfirmEmailIdToLower"
              @blur="$v.emailValid.sConfirmEmailId.$touch()"
            ></v-text-field>
        </v-col>
        <v-col  cols="12" sm="12">
            <div v-if="disclaimer01!=''" class="disclaimer">{{disclaimer01}}</div>
            <div> 
            <v-checkbox          
              hide-details
              id="checkbox"
              v-model="bConfirmReport"
              :disabled="inputDisabled"
              color="white"
              :label="disclaimer02"
            ></v-checkbox>
            <div>
            <v-btn
              :disabled="$v.emailValid.$invalid"
              class="mr-4 next"
              @click.prevent="nextPage"
            >Next</v-btn>
            </div>
            </div>
          </v-col>
      </v-row>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import {
  required,
  email,
  sameAs
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      emailValid: {
        sRequesterEmailId: this.$store.state.requestermodule.sRequesterEmailId,
        sConfirmEmailId: ""
      },
      bConfirmReport: this.$store.state.requestermodule.bConfirmReport,
      inputDisabled: false,

      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer02,

      //Show and Hide Fields Values
      MROPEmailId: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROPEmailId,
      MROConfirmReport: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROConfirmReport
    };
  },
  //Email on verify OTP validations
  mixins: [validationMixin],
  validations: {
    emailValid: {
      sRequesterEmailId: {
        required,
        email
      },
      sConfirmEmailId: {
        required,
        email,
        sameAsemailID: sameAs("sRequesterEmailId")
      }
    }
  },
  computed: {
    // Email and verify OTP validations error message setter
    emailErrors() {
      const errors = [];
      if (!this.$v.emailValid.sRequesterEmailId.$dirty) return errors;
      !this.$v.emailValid.sRequesterEmailId.email &&
        errors.push("Must be valid e-mail");
      !this.$v.emailValid.sRequesterEmailId.required &&
        errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.emailValid.sConfirmEmailId.$dirty) return errors;
      !this.$v.emailValid.sConfirmEmailId.sameAsemailID &&
        errors.push("Please enter correct e-mail");
      !this.$v.emailValid.sConfirmEmailId.email &&
        errors.push("Must be valid e-mail");
      !this.$v.emailValid.sConfirmEmailId.required &&
        errors.push("E-mail is required");
      return errors;
    }
  },
  methods: {
    sRequesterEmailIdToLower(val) {
      this.emailValid.sRequesterEmailId = val.toLowerCase()
    },
    sConfirmEmailIdToLower(val) {
      this.emailValid.sConfirmEmailId = val.toLowerCase()
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/sRequesterEmailId",
        this.emailValid.sRequesterEmailId
      );
      this.$store.commit("requestermodule/bConfirmReport", this.bConfirmReport);

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
<style scoped>
#checkbox label{
  font-size: 14px;
}
</style>