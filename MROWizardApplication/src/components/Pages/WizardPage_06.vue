<template>
  <div class="center">
    <h1 v-if="bAreYouPatient">What's your Email Id ?</h1>
    <h1 v-else>What is patient's Email Id ?</h1>
    <p>We'll email you a confirmation of your request when you're finished</p>
    <div>
      <form>
        <v-row>
          <v-col v-if="MROPEmailId" cols="12" offset-sm="3" sm="6">
            <label for="sPatientEmailId" class="control-label">Email ID</label>
            <v-text-field
              v-model="sPatientEmailId"
              :error-messages="emailErrors"
              required
              @input="$v.sPatientEmailId.$touch()"
              @blur="$v.sPatientEmailId.$touch()"
            ></v-text-field>
            <label for="bConfirmEmailId" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="bConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              @input="$v.bConfirmEmailId.$touch()"
              @blur="$v.bConfirmEmailId.$touch()"
            ></v-text-field>
            <v-checkbox
              v-if="MROConfirmReport"
              v-model="bConfirmReport"
              label="Please email me a copy of my completed request form"
            ></v-checkbox>
            <v-btn
              class="mr-4"
              @click.prevent="nextPage"
              :disabled="$v.$invalid"
              color="success"
            >Next</v-btn>           
          </v-col>
        </v-row>
      </form>
    </div>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, email, sameAs } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_04",
  data() {
    return {
      sPatientEmailId: this.$store.state.requestermodule.sPatientEmailId,
      bConfirmEmailId: this.$store.state.requestermodule.bConfirmEmailId,
      bConfirmReport: this.$store.state.requestermodule.bConfirmReport,

      
      //Show and Hide Fields Values
      MROPEmailId : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROPEmailId,
      MROConfirmReport : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROConfirmReport
    };
  },
  mixins: [validationMixin],
  validations: {
    sPatientEmailId: {
      required,
      email
    },
    bConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs('sPatientEmailId')
    }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },   
    emailErrors() {
      const errors = [];
      if (!this.$v.sPatientEmailId.$dirty) return errors;
      !this.$v.sPatientEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sPatientEmailId.required && errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.bConfirmEmailId.$dirty) return errors;
      !this.$v.bConfirmEmailId.sameAsemailID && errors.push("Please enter correct e-mail");
      !this.$v.bConfirmEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.bConfirmEmailId.required && errors.push("E-mail is required");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sPatientEmailId", this.sPatientEmailId);
      this.$store.commit("requestermodule/bConfirmEmailId", this.bConfirmEmailId);
      this.$store.commit("requestermodule/bConfirmReport", this.bConfirmReport);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 5);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-5");
       this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
/* .center {
  text-align: center;
} */
</style>