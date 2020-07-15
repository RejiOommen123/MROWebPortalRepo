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
        <v-col v-if="MROPatientMiddleName" cols="12" sm="3" xs="3">
          <label for="sPatientMiddleName" class="control-label">MIDDLE NAME</label>
          <v-text-field v-model="sPatientMiddleName"></v-text-field>
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
        <!-- bPatientNameChanged -->
        <v-col cols="12" offset-sm="2" sm="8">
          <v-checkbox
            v-model="bPatientNameChanged"
            color="#e84700"
            @change="checked()"
            label="If different from above, please provide full name given at time of visit (for records requested)"
          ></v-checkbox>
        </v-col>
        <!-- TODO:add show and hide for previous name after adding these fields in db -->
        <template v-if="bPatientNameChanged">
          <v-col v-if="MROPatientFirstName" cols="12" offset-sm="1" sm="3" xs="3">
            <label for="sPreviousPatientFirstName" class="control-label">FIRST NAME</label>
            <v-text-field
              v-model="sPreviousPatientFirstName"
              :error-messages="sPreviousPatientFirstNameError"
              required
              @input="$v.sPreviousPatientFirstName.$touch()"
              @blur="$v.sPreviousPatientFirstName.$touch()"
            ></v-text-field>
          </v-col>
          <v-col v-if="MROPatientMiddleName" cols="12" sm="3" xs="3">
            <label for="sPreviousPatientMiddleName" class="control-label">MIDDLE NAME</label>
            <v-text-field v-model="sPreviousPatientMiddleName"></v-text-field>
          </v-col>
          <v-col v-if="MROPatientLastName" cols="12" sm="3" xs="3">
            <label for="sPreviousPatientLastName" class="control-label">LAST NAME</label>
            <v-text-field
              v-model="sPreviousPatientLastName"
              :error-messages="sPreviousPatientLastNameError"
              required
              @input="$v.sPreviousPatientLastName.$touch()"
              @blur="$v.sPreviousPatientLastName.$touch()"
            ></v-text-field>
          </v-col>
        </template>
        <v-col v-if="bPatientNameChanged" cols="12" offset-sm="3" sm="6">
          <v-btn class="mr-4 next" @click.prevent="nextPage" :disabled="$v.$invalid">Next</v-btn>
        </v-col>
        <v-col v-else cols="12" offset-sm="3" sm="6">
          <v-btn class="mr-4 next" @click.prevent="nextPage" :disabled="$v.sPatientFirstName.$invalid  || $v.sPatientLastName.$invalid ">Next</v-btn>
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
      sPatientFirstName: "",
      sPatientLastName: "",
      sPatientMiddleName: "",
      sPreviousPatientFirstName: "",
      sPreviousPatientLastName: "",
      sPreviousPatientMiddleName: "",
      bPatientNameChanged: false,

      //Show and Hide Fields Values fetch from store
      MROPatientFirstName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientFirstName,
      MROPatientMiddleName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientMiddleName,
      MROPatientLastName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientLastName
    };
  },
  //Requester name validations
  mixins: [validationMixin],
  validations: {
    sPatientFirstName: { required, maxLength: maxLength(15) },
    sPatientLastName: { required, maxLength: maxLength(15) },
    sPreviousPatientFirstName: { required, maxLength: maxLength(15) },
    sPreviousPatientLastName: { required, maxLength: maxLength(15) }
    // sPatientMiddleName: { maxLength: maxLength(1) }
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
    sPreviousPatientFirstNameError() {
      const errors = [];
      if (!this.$v.sPreviousPatientFirstName.$dirty) return errors;
      !this.$v.sPreviousPatientFirstName.maxLength &&
        errors.push("First Name must be at most 15 characters long");
      !this.$v.sPreviousPatientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPreviousPatientLastNameError() {
      const errors = [];
      if (!this.$v.sPreviousPatientLastName.$dirty) return errors;
      !this.$v.sPreviousPatientLastName.maxLength &&
        errors.push("Last Name must be at most 15 characters long");
      !this.$v.sPreviousPatientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    }
    // sPatientMiddleNameError() {
    //    const errors = [];
    //   if (!this.$v.sPatientMiddleName.$dirty) return errors;
    //   !this.$v.sPatientMiddleName.maxLength &&
    //     errors.push("Middle Name must be at most 15 characters long");
    //   !this.$v.sPatientMiddleName.required &&
    //     errors.push("Middle Name is required.");
    //   return errors;
    // }
  },
  methods: {
    checked() {
      if (!this.bPatientNameChanged) {
        this.sPreviousPatientFirstName = "";
        this.sPreviousPatientLastName = "";
        this.sPreviousPatientMiddleName = "";
      }
    },
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
        "requestermodule/sPatientMiddleName",
        this.sPatientMiddleName
      );
      this.$store.commit(
        "requestermodule/sPreviousPatientFirstName",
        this.sPreviousPatientFirstName
      );
      this.$store.commit(
        "requestermodule/sPreviousPatientLastName",
        this.sPreviousPatientLastName
      );
      this.$store.commit(
        "requestermodule/sPreviousPatientMiddleName",
        this.sPreviousPatientMiddleName
      );
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style scoped>
</style>
