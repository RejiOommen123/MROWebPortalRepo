<template>
  <div class="center">
    <form>
    <v-row>
      <v-col cols="12" offset-sm="2" sm="6" md="8">
        <h1 v-if="bAreYouPatient">What is your date of birth?</h1>
        <h1 v-else>What is the patient’s date of birth?</h1>
        <p v-if="disclaimer!=''">{{disclaimer}}</p>
        <!-- date picker menu and date picker -->
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              transition="scale-transition"
              offset-y
              :value="dtPatientDOBFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="dtPatientDOBErrors"
              clearable
              label="DATE OF BIRTH"
              readonly
              v-bind="attrs"
              v-on="on"              
              @click:clear="dtPatientDOB = null"
              @input="$v.dtPatientDOB.$touch()"
              @blur="$v.dtPatientDOB.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker 
            ref="picker"
            :max="new Date().toISOString().substr(0, 10)"
            v-model="dtPatientDOB" 
            color="green lighten-1" 
            header-color="primary" 
            light @change="menu1 = false">
          </v-date-picker>
        </v-menu>
      </v-col>
      <v-spacer></v-spacer>
      <br />
      <v-col cols="12" offset-sm="3" sm="6">
        <div>
          <v-btn @click.prevent="nextPage" :disabled="$v.$invalid" class="next">Next</v-btn>
        </div>
      </v-col>
    </v-row>
    </form>
  </div>
</template>

<script>
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_05",
  data() {
    return {
      dtPatientDOB: this.$store.state.requestermodule.dtPatientDOB,
      menu1: false,
      disclaimer: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_05_disclaimer01,
    };
  },
  // Date Validation
  validations: {
    dtPatientDOB: {
      required,
      minValue: value => value < new Date().toISOString()
    }
  },
  watch: {
      menu1 (val) {
        val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
      },
    },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/dtPatientDOB", this.dtPatientDOB);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    bAreYouPatient(){
      return this.$store.state.requestermodule.bAreYouPatient;
    },
     //Date validations error message setter
    dtPatientDOBFormatted() {
      return this.dtPatientDOB ? moment(this.dtPatientDOB).format("MM-DD-YYYY") : "";
    },
    dtPatientDOBErrors() {
      const errors = [];
      if (!this.$v.dtPatientDOB.$dirty) return errors;
      !this.$v.dtPatientDOB.minValue && errors.push("This date cannot be future date");
      !this.$v.dtPatientDOB.required && errors.push("End Date is required");
      return errors;
    }
  }
};
</script>
