<template>
  <div class="center">
    <div class="control-group">
      <h1 v-if="areYouPatient">What is your Full Name ?</h1>
      <h1 v-else>What is patient's Full Name ?</h1>
      
    </div>
    <form>
      <v-row>
        <v-col cols="12" offset-sm="1" sm="3">
          <label for="fname" class="control-label">First Name</label>
          <v-text-field
            v-model="fname"
            :error-messages="fNameError"
            required
            @input="$v.fname.$touch()"
            @blur="$v.fname.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
          <label for="minitial" class="control-label">Middle Initial</label>
          <v-text-field
            v-model="minitial"
            :error-messages="mInitialError"
            required
            @input="$v.minitial.$touch()"
            @blur="$v.minitial.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
          <label for="lname" class="control-label">Last Name</label>
          <v-text-field
            v-model="lname"
            :error-messages="lNameError"
            required
            @input="$v.lname.$touch()"
            @blur="$v.lname.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox v-model="isPatientMinor" label="Is the patient a minor?"></v-checkbox>
          <v-checkbox v-model="isPatientDeceased" label="Is the patient deceased?"></v-checkbox>
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
      fname: this.$store.state.requestermodule.fname,
      lname: this.$store.state.requestermodule.lname,
      minitial: this.$store.state.requestermodule.minitial,
      isPatientMinor: this.$store.state.requestermodule.isPatientMinor,
      isPatientDeceased: this.$store.state.requestermodule.isPatientDeceased
    };
  },
  mixins: [validationMixin],
  validations: {
    fname: { required, maxLength: maxLength(15) },
    lname: { required, maxLength: maxLength(15) },
    minitial: { required, maxLength: maxLength(1) }
  },
  computed: {
    areYouPatient() {
      return this.$store.state.requestermodule.areYouPatient;
    },   
    disclaimer() {
      return this.$store.state.ConfigModule.wp04_disclaimer;
    },
    askPatientDeceased() {
      return this.$store.state.requestermodule.askPatientDeceased;
    },
    fNameError() {
      const errors = [];
      if (!this.$v.fname.$dirty) return errors;
      !this.$v.fname.maxLength &&
        errors.push("First Name must be at most 15 characters long");
      !this.$v.fname.required && errors.push("First Name is required.");
      return errors;
    },
    lNameError() {
      const errors = [];
      if (!this.$v.lname.$dirty) return errors;
      !this.$v.lname.maxLength &&
        errors.push("Last Name must be at most 15 characters long");
      !this.$v.lname.required && errors.push("Last Name is required.");
      return errors;
    },
    mInitialError() {
      const errors = [];
      if (!this.$v.minitial.$dirty) return errors;
      !this.$v.minitial.maxLength &&
        errors.push("Middle Initial must be at most 1 characters long");
      !this.$v.minitial.required && errors.push("Middle Initial is required.");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/mutatefname", this.fname);
      this.$store.commit("requestermodule/mutatelname", this.lname);
      this.$store.commit("requestermodule/mutateminitial", this.minitial);
      this.$store.commit(
        "requestermodule/mutateisPatientDeceased",
        this.isPatientMinor
      );
      this.$store.commit(
        "requestermodule/mutateisPatientDeceased",
        this.isPatientDeceased
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
