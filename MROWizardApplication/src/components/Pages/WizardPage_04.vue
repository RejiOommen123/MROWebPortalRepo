<template>
  <div class="center">
    <div class="control-group">
      <h1 v-if="bAreYouPatient">What is your Full Name ?</h1>
      <h1 v-else>What is patient's Full Name ?</h1>
      
    </div>
    <form>
      <v-row>
        <v-col v-if="MROPatientFirstName" cols="12" offset-sm="1" sm="3">
          <label for="sPatientFirstName" class="control-label">First Name</label>
          <v-text-field
            v-model="sPatientFirstName"
            :error-messages="sPatientFirstNameError"
            required
            @input="$v.sPatientFirstName.$touch()"
            @blur="$v.sPatientFirstName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col v-if="MROPatientMiddleInitial" cols="12" sm="3">
          <label for="sPatientMiddleInitial" class="control-label">Middle Initial</label>
          <v-text-field
            v-model="sPatientMiddleInitial"
            :error-messages="sPatientMiddleInitialError"
            required
            @input="$v.sPatientMiddleInitial.$touch()"
            @blur="$v.sPatientMiddleInitial.$touch()"
          ></v-text-field>
        </v-col>
        <v-col v-if="sPatientLastName" cols="12" sm="3">
          <label for="sPatientLastName" class="control-label">Last Name</label>
          <v-text-field
            v-model="sPatientLastName"
            :error-messages="sPatientLastNameError"
            required
            @input="$v.sPatientLastName.$touch()"
            @blur="$v.sPatientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox v-if="MROIsPatientMinor" v-model="bIsPatientMinor" label="Is the patient a minor?"></v-checkbox>
          <v-checkbox v-if="MROIsPatientDeceased" v-model="bIsPatientDeceased" label="Is the patient deceased?"></v-checkbox>
          <v-btn class="mr-4" @click.prevent="nextPage" :disabled="$v.$invalid" color="success">Next</v-btn>
        </v-col>
        <div class="disclaimer">{{disclaimer}}</div>
      </v-row>
    </form>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_05",
  data() {
    return {
      sPatientFirstName: this.$store.state.requestermodule.sPatientFirstName,
      sPatientLastName: this.$store.state.requestermodule.sPatientLastName,
      sPatientMiddleInitial: this.$store.state.requestermodule.sPatientMiddleInitial,
      bIsPatientMinor: this.$store.state.requestermodule.bIsPatientMinor,
      bIsPatientDeceased: this.$store.state.requestermodule.bIsPatientDeceased,

      //Show and Hide Fields Values
      MROPatientFirstName : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROPatientFirstName,
      MROPatientMiddleInitial : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROPatientMiddleInitial,
      MROPatientLastName : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROPatientLastName,
      MROIsPatientMinor : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROIsPatientMinor,
      MROIsPatientDeceased : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oFields.MROIsPatientDeceased
    };
  },
  mixins: [validationMixin],
  validations: {
    sPatientFirstName: { required, maxLength: maxLength(15) },
    sPatientLastName: { required, maxLength: maxLength(15) },
    sPatientMiddleInitial: { required, maxLength: maxLength(1) }
  },
  computed: {  
    disclaimer() {      
      return this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_04_disclaimer01;
    },
    sPatientFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientFirstName.$dirty) return errors;
      !this.$v.sPatientFirstName.maxLength &&
        errors.push("First Name must be at most 15 characters long");
      !this.$v.sPatientFirstName.required && errors.push("First Name is required.");
      return errors;
    },
    sPatientLastNameError() {
      const errors = [];
      if (!this.$v.sPatientLastName.$dirty) return errors;
      !this.$v.sPatientLastName.maxLength &&
        errors.push("Last Name must be at most 15 characters long");
      !this.$v.sPatientLastName.required && errors.push("Last Name is required.");
      return errors;
    },
    sPatientMiddleInitialError() {
      const errors = [];
      if (!this.$v.sPatientMiddleInitial.$dirty) return errors;
      !this.$v.sPatientMiddleInitial.maxLength &&
        errors.push("Middle Initial must be at most 1 characters long");
      !this.$v.sPatientMiddleInitial.required && errors.push("Middle Initial is required.");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/sPatientFirstName", this.sPatientFirstName);
      this.$store.commit("requestermodule/sPatientLastName", this.sPatientLastName);
      this.$store.commit("requestermodule/sPatientMiddleInitial", this.sPatientMiddleInitial);
      this.$store.commit(
        "requestermodule/bIsPatientMinor",
        this.bIsPatientMinor
      );
      this.$store.commit(
        "requestermodule/bIsPatientDeceased",
        this.bIsPatientDeceased
      );
      // this.$store.commit("ConfigModule/mutatepageNumerical", 6);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-6");
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
