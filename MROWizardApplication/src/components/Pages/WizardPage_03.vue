<template>
  <div class="center">
    <div class="form-group">
      <h1>Are you requesting records for yourself?</h1>
      <div class="form-group btn-group-vertical" role="toolbar">
        <button @click.prevent="setPatient" class="btn btn-success btn-md locationButton " >Yes, I want my medical records.</button>
        <br />
        <button @click.prevent="setNotPatient" class="btn btn-success btn-md locationButton" >No, I am requesting records for someone else <br/>(Child, Dependent, Decedent).</button>
      </div>
      <div v-show="!bAreYouPatient">
        <form>
          <v-row>
          <v-col cols="6" offset-sm="3" sm="6">
          <v-text-field
            v-model="sRelativeName"
            :error-messages="sRelativeName"
            label="Relative Full Name"
            required
            @input="$v.sRelativeName.$touch()"
            @blur="$v.sRelativeName.$touch()"
          ></v-text-field>
          <v-text-field
            v-model="sRelationToPatient"
            :error-messages="sRelationToPatientErrors"
            label="Relation With Patient"
            required
            @input="$v.sRelationToPatient.$touch()"
            @blur="$v.sRelationToPatient.$touch()"
          ></v-text-field>
          <v-btn @click.prevent="continueAhead" :disabled="$v.$invalid" class="mr-4" color="success" >Continue</v-btn>
          </v-col>
          </v-row>
        </form>
      </div>
      <div v-if="bAreYouPatient" class="disclaimer">{{this.disclaimer01}}</div>
      <div v-else class="disclaimer">{{this.disclaimer02}}</div>
    </div>
    
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_03",
  data() {
    return {
      sRelativeName: this.$store.state.requestermodule.sRelativeName,
      sRelationToPatient: this.$store.state.requestermodule.sRelationToPatient,
      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_03_disclaimer02,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_03_disclaimer03
    };
  },
  mixins: [validationMixin],
  validations: {
    sRelativeName: { required },
    sRelationToPatient: { required }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    sRelativeNameErrors() {
      const errors = [];
      if (!this.$v. sRelativeName.$dirty) return errors;
      !this.$v. sRelativeName.required && errors.push("Name is required.");
      return errors;
    },
    sRelationToPatientErrors() {
      const errors = [];
      if (!this.$v.sRelationToPatient.$dirty) return errors;
      !this.$v.sRelationToPatient.required && errors.push("Name is required.");
      return errors;
    }
  },
  methods: {
    setPatient() {
      this.$store.state.requestermodule.isPatientDeceased = false;
      this.$store.commit("requestermodule/mutateaskPatientDeceased", false);
      this.$store.commit("requestermodule/bAreYouPatient", true);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 4);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-4");
          this.$store.commit("ConfigModule/mutateNextIndex");
    },
    setNotPatient() {
      this.$store.commit("requestermodule/bAreYouPatient", false);
    },
    continueAhead() {
      this.$store.state.requestermodule.isPatientDeceased = false;
      this.$store.commit("requestermodule/mutateaskPatientDeceased", true);

      this.$store.commit("requestermodule/sRelativeName", this.sRelativeName);
      this.$store.commit("requestermodule/sRelationToPatient",this.sRelationToPatient);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 4);
      
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-4");
          this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>

<style scoped>
/* .center {
  text-align: center;
}
.disclaimer{
    font-size: 14px;
    text-align: center;
    color: #ababab;
} */
/* .invalid label {
  color: red;
}

.invalid input {
  border-bottom: 1px solid red;
}

.invalid span {
  color: red;
}
input {
  background: transparent;
  border: none;
  border-bottom: 1px solid #000000;
}
input:focus {
  outline: none;
} */
</style>