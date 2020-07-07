<template>
  <div class="center">
    <div class="control-group">
      <h1 v-if="bAreYouPatient">What is your full legal name?</h1>
      <h1 v-else>What is the patient’s full legal name?</h1>
    </div>
    <form>
      <v-row>
        <v-col v-if="MROPatientFirstName" cols="12" offset-sm="1" sm="3" xs="3">
          <label for="sPatientFirstName" class="control-label">FIRST NAME</label>
          <v-text-field
            v-model="sPatientFirstName"
            :error-messages="sPatientFirstNameError"
            required
            @input="$v.sPatientFirstName.$touch()"
            @blur="$v.sPatientFirstName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col v-if="MROPatientMiddleInitial" cols="12" sm="3" xs="3">
          <label for="sPatientMiddleInitial" class="control-label">MIDDLE INITIAL</label>
          <v-text-field
            style="max-width:50%; margin-right:auto;margin-left:auto"
            maxlength="1"
            v-model="sPatientMiddleInitial"
            :error-messages="sPatientMiddleInitialError"
            required
            @input="$v.sPatientMiddleInitial.$touch()"
            @blur="$v.sPatientMiddleInitial.$touch()"
          ></v-text-field>
        </v-col>
        <v-col v-if="MROPatientLastName" cols="12" sm="3" xs="3">
          <label for="sPatientLastName" class="control-label">LAST NAME</label>
          <v-text-field
            v-model="sPatientLastName"
            :error-messages="sPatientLastNameError"
            required
            @input="$v.sPatientLastName.$touch()"
            @blur="$v.sPatientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-btn class="mr-4 next" @click.prevent="nextPage" :disabled="$v.$invalid" >Next</v-btn>
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
      sPatientMiddleInitial: this.$store.state.requestermodule
        .sPatientMiddleInitial,


      //Show and Hide Fields Values fetch from store
      MROPatientFirstName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientFirstName,
      MROPatientMiddleInitial: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientMiddleInitial,
      MROPatientLastName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientLastName,
      MROIsPatientMinor: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROIsPatientMinor,
      MROIsPatientDeceased: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROIsPatientDeceased
    };
  },
  //Requester name validations
  mixins: [validationMixin],
  validations: {
    sPatientFirstName: { required, maxLength: maxLength(15) },
    sPatientLastName: { required, maxLength: maxLength(15) },
    sPatientMiddleInitial: { maxLength: maxLength(1) }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    disclaimer() {
      return this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_04_disclaimer01;
    },
    //Requester name validations error message setter
    sPatientFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientFirstName.$dirty) return errors;
      !this.$v.sPatientFirstName.maxLength &&
        errors.push("First Name must be at most 15 characters long");
      !this.$v.sPatientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPatientLastNameError() {
      const errors = [];
      if (!this.$v.sPatientLastName.$dirty) return errors;
      !this.$v.sPatientLastName.maxLength &&
        errors.push("Last Name must be at most 15 characters long");
      !this.$v.sPatientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    sPatientMiddleInitialError() {
      const errors = [];
      if (!this.$v.sPatientMiddleInitial.$dirty) return errors;
      !this.$v.sPatientMiddleInitial.maxLength &&
        errors.push("One character only.");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit(
        "requestermodule/sPatientFirstName",
        this.sPatientFirstName
      );
      this.$store.commit(
        "requestermodule/sPatientLastName",
        this.sPatientLastName
      );
      this.$store.commit(
        "requestermodule/sPatientMiddleInitial",
        this.sPatientMiddleInitial
      );
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
</style>
