<template>
  <div class="center">
    <div class="form-group">
      <h1>Are you requesting records for yourself?</h1>
      <div class="form-group btn-group-vertical" role="toolbar">
        <button @click.prevent="setPatient" class="btn btn-success btn-md locationButton " >Yes, I want my medical records.</button>
        <br />
        <button @click.prevent="setNotPatient" class="btn btn-success btn-md locationButton" >No, I am requesting records for someone else <br/>(Child, Dependent, Decedent).</button>
      </div>
      <div v-show="!areYouPatient">
        <form>
          <v-row>
          <v-col cols="6" offset-sm="3" sm="6">
          <v-text-field
            v-model="rname"
            :error-messages="rnameErrors"
            label="Relative Full Name"
            required
            @input="$v.rname.$touch()"
            @blur="$v.rname.$touch()"
          ></v-text-field>
          <v-text-field
            v-model="relationToPatient"
            :error-messages="relationToPatientErrors"
            label="Relation With Patient"
            required
            @input="$v.relationToPatient.$touch()"
            @blur="$v.relationToPatient.$touch()"
          ></v-text-field>
          <v-btn @click.prevent="continueAhead" :disabled="$v.$invalid" class="mr-4" color="success" >Continue</v-btn>
          </v-col>
          </v-row>
        </form>
      </div>
      <div v-if="areYouPatient" class="disclaimer">{{this.disclaimer01}}</div>
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
      rname: this.$store.state.requestermodule.rname,
      relationToPatient: this.$store.state.requestermodule.relationToPatient,
      disclaimer01: this.$store.state.ConfigModule.wp03_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.wp03_disclaimer02
    };
  },
  mixins: [validationMixin],
  validations: {
    rname: { required },
    relationToPatient: { required }
  },
  computed: {
    areYouPatient() {
      return this.$store.state.requestermodule.areYouPatient;
    },
    rnameErrors() {
      const errors = [];
      if (!this.$v. rname.$dirty) return errors;
      !this.$v. rname.required && errors.push("Name is required.");
      return errors;
    },
    relationToPatientErrors() {
      const errors = [];
      if (!this.$v.relationToPatient.$dirty) return errors;
      !this.$v.relationToPatient.required && errors.push("Name is required.");
      return errors;
    }
  },
  methods: {
    setPatient() {
      this.$store.state.requestermodule.isPatientDeceased = false;
      this.$store.commit("requestermodule/mutateaskPatientDeceased", false);
      this.$store.commit("requestermodule/mutateareYouPatient", true);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 4);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-4");
          this.$store.commit("ConfigModule/mutateNextIndex");
    },
    setNotPatient() {
      this.$store.commit("requestermodule/mutateareYouPatient", false);
    },
    continueAhead() {
      this.$store.state.requestermodule.isPatientDeceased = false;
      this.$store.commit("requestermodule/mutateaskPatientDeceased", true);

      this.$store.commit("requestermodule/mutatername", this.rname);
      this.$store.commit("requestermodule/mutaterelationToPatient",this.relationToPatient);
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