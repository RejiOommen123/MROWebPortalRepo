<template>
  <div class="center">
    <h1>What's Your Email ID ?</h1>
    <p>We'll email you a confirmation of your request when you're finished</p>
    <div>
      <form>
        <v-row>
          <v-col cols="12" offset-sm="3" sm="6">
            <label for="emailID" class="control-label">Email ID</label>
            <v-text-field
              v-model="emailID"
              :error-messages="emailErrors"
              required
              @input="$v.emailID.$touch()"
              @blur="$v.emailID.$touch()"
            ></v-text-field>
            <label for="confirmEmailID" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="confirmEmailID"
              :error-messages="confirmEmailErrors"
              required
              @input="$v.confirmEmailID.$touch()"
              @blur="$v.confirmEmailID.$touch()"
            ></v-text-field>
            <v-checkbox
              v-model="confirmReport"
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
      emailID: this.$store.state.requestermodule.emailID,
      confirmEmailID: this.$store.state.requestermodule.confirmEmailID,
      confirmReport: this.$store.state.requestermodule.confirmReport
    };
  },
  mixins: [validationMixin],
  validations: {
    emailID: {
      required,
      email
    },
    confirmEmailID: {
      required,
      email,
      sameAsemailID: sameAs('emailID')
    }
  },
  computed: {
    emailErrors() {
      const errors = [];
      if (!this.$v.emailID.$dirty) return errors;
      !this.$v.emailID.email && errors.push("Must be valid e-mail");
      !this.$v.emailID.required && errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.confirmEmailID.$dirty) return errors;
      !this.$v.confirmEmailID.sameAsemailID && errors.push("Please enter correct e-mail");
      !this.$v.confirmEmailID.email && errors.push("Must be valid e-mail");
      !this.$v.confirmEmailID.required && errors.push("E-mail is required");
      return errors;
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/mutateemailID", this.emailID);
      this.$store.commit("requestermodule/mutateconfirmEmailID", this.confirmEmailID);
      this.$store.commit("requestermodule/mutateconfirmReport", this.confirmReport);
      this.$store.commit("ConfigModule/mutatepageNumerical", 5);
      this.$store.commit("ConfigModule/mutateCurrentPage", "page-5");
    }
  }
};
</script>
<style scoped>
/* .center {
  text-align: center;
} */
</style>