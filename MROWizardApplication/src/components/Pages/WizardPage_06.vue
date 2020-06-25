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
            <label for="sConfirmEmailId" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="sConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              @input="$v.sConfirmEmailId.$touch()"
              @blur="$v.sConfirmEmailId.$touch()"
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
  name: "WizardPage_06",
  data() {
    return {
      sPatientEmailId: this.$store.state.requestermodule.sPatientEmailId,
      sConfirmEmailId: this.$store.state.requestermodule.sConfirmEmailId,
      bConfirmReport: this.$store.state.requestermodule.bConfirmReport,

      
      //Show and Hide Fields Values
      MROPEmailId : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROPEmailId,
      MROConfirmReport : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROConfirmReport
    };
  },
  mixins: [validationMixin],
  validations: {
    sPatientEmailId: {
      required,
      email
    },
    sConfirmEmailId: {
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
      if (!this.$v.sConfirmEmailId.$dirty) return errors;
      !this.$v.sConfirmEmailId.sameAsemailID && errors.push("Please enter correct e-mail");
      !this.$v.sConfirmEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sConfirmEmailId.required && errors.push("E-mail is required");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sPatientEmailId", this.sPatientEmailId);
      this.$store.commit("requestermodule/sConfirmEmailId", this.sConfirmEmailId);
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