<template>
  <div class="center">
    <h1>Are you requesting records for yourself?</h1>
    <v-row>
      <div style="width:100%">
        <v-col cols="12" offset-sm="2" sm="8">
          <button
            @click.prevent="setPatient"
            class="releaseToButton"
          >Yes, I want my medical records.</button>
        </v-col>
      </div>
      <div style="width:100%">
        <v-col cols="12" offset-sm="2" sm="8">
          <button @click.prevent="setNotPatient" class="releaseToButton">
            No, I am requesting records
            <br />for someone else.
          </button>
        </v-col>
      </div>
    </v-row>
    <div v-show="!bAreYouPatient">
      <form>
        <v-row>
          <v-col cols="12" offset-sm="3" sm="6">
            <v-text-field
              v-model="sRelativeName"
              :error-messages="sRelativeNameErrors"
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
            <v-btn
              @click.prevent="continueAhead"
              :disabled="$v.$invalid"
              class="mr-4"
              color="success"
            >Continue</v-btn>
          </v-col>
        </v-row>
      </form>
    </div>
    <div v-if="bAreYouPatient" class="disclaimer">{{this.disclaimer01}}</div>
    <div v-else class="disclaimer">{{this.disclaimer02}}</div>
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
      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer02,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer03
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
      if (!this.$v.sRelativeName.$dirty) return errors;
      !this.$v.sRelativeName.required && errors.push("Name is required.");
      return errors;
    },
    sRelationToPatientErrors() {
      const errors = [];
      if (!this.$v.sRelationToPatient.$dirty) return errors;
      !this.$v.sRelationToPatient.required &&
        errors.push("Realtion to patient is required");
      return errors;
    }
  },
  methods: {
    setPatient() {
      this.$store.commit("requestermodule/bAreYouPatient", true);
      this.$store.commit("requestermodule/sRelativeName", "");
      this.$store.commit("requestermodule/sRelationToPatient", "");
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    setNotPatient() {
      this.$store.commit("requestermodule/bAreYouPatient", false);
      this.$store.commit("requestermodule/sRelativeName", this.sRelativeName);
      this.$store.commit(
        "requestermodule/sRelationToPatient",
        this.sRelationToPatient
      );
    },
    continueAhead() {
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>

<style scoped>
</style>