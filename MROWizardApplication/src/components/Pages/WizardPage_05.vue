<template>
  <div class="center">
    <br />
    <br />
    <v-row>
      <v-col cols="12" offset-sm="2" sm="6" md="8">
        <h1>When was the Patient Born ?</h1>
        <h4>(MM/DD/YYYY)</h4>
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dPatientDOBFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="dPatientDOBErrors"
              clearable
              label="Date of Birth"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dPatientDOB = null"
              @input="$v.dPatientDOB.$touch()"
              @blur="$v.dPatientDOB.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dPatientDOB" @change="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>
      <v-spacer></v-spacer>
      <br />
      <v-col cols="12" offset-sm="3" sm="6">
        <div>
          <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_05",
  data() {
    return {
      dPatientDOB: this.$store.state.requestermodule.dPatientDOB,
      menu1: false,
    };
  },
  validations: {
    dPatientDOB: {
      required,
      minValue: value => value < new Date().toISOString()
    }
  },
  methods: {
    nextPage() {
      console.log(this.dPatientDOB);
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/dPatientDOB", this.dPatientDOB);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    dateFormatted() {
      return this.dPatientDOB ? moment(this.dPatientDOB).format("MM-DD-YYYY") : "";
    },
    dateErrors() {
      const errors = [];
      if (!this.$v.dPatientDOB.$dirty) return errors;
      !this.$v.dPatientDOB.minValue && errors.push("Invalid Date");
      !this.$v.dPatientDOB.required && errors.push("End Date is required");
      return errors;
    }
  }
};
</script>

<style scoped>
/* .center {
  text-align: center;
} */
</style>