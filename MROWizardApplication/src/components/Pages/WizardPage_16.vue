<template>
  <div class="center">
    <br />
    <br />
    <v-row>
    <v-col v-if="MRORequestDeadlineDate" cols="12" offset-sm="2" sm="6" md="8">
       <h1>What date do you need your records by?</h1>
        <h4>(MM/DD/YYYY)</h4>
        <div class="disclaimer">{{disclaimer}}</div>
      <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field            
              :value="dateFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="dateErrors"
              clearable
              label="Select Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dDeadline = null"
              @input="$v.dDeadline.$touch()"
              @blur="$v.dDeadline.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dDeadline" @change="menu1 = false"></v-date-picker>
        </v-menu>
    </v-col>
    <v-spacer></v-spacer>
    <br />
    <v-col cols="12" offset-sm="3" sm="6">
    <div>
      <v-btn @click.prevent="nextPage"  color="success">Next</v-btn>
    </div>
    <!-- :disabled="$v.$invalid" -->
    </v-col>
  </v-row>
    
  </div>
</template>

<script>
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_16",
  data() {
    return {
      dDeadline: null,//new Date().toISOString().substr(0, 10),
      menu1: false,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_16_disclaimer01,

      //Show and Hide Fields Values
      MRORequestDeadlineDate : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MRORequestDeadlineDate,
    };
  },
  validations: {
    dDeadline: {
        required,
        minValue: value => value < new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/dDeadline", this.dDeadline);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    dateFormatted() {
      return this.dDeadline ? moment(this.dDeadline).format("MM-DD-YYYY") : "";
    },
    dateErrors(){
      const errors = [];
      if (!this.$v.dDeadline.$dirty) return errors;
      !this.$v.dDeadline.minValue && errors.push("Invalid Date");
      !this.$v.dDeadline.required && errors.push("End Date is required");
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