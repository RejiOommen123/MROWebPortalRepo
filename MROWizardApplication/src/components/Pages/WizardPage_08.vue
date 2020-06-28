<template>
  <div class="center">
    <h1>Specify an approximate*<br/>Date Range for records.</h1>
    <form>
    <v-row>
      <v-col cols="12" offset-sm="1" sm="3" md="4">
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dtRecordRangeStartFormatted"
              :error-messages="dtRecordRangeStartErrors"
              clearable
              label="Start Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dtRecordRangeStart = null"
              @input="$v.dtRecordRangeStart.$touch()"
              @blur="$v.dtRecordRangeStart.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dtRecordRangeStart" color="green lighten-1" header-color="primary" light @change="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>

      <v-col cols="12" offset-sm="2" sm="3" md="4">
        <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dtRecordRangeEndFormatted"
              :error-messages="dtRecordRangeEndErrors"
              clearable
              label="End Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dtRecordRangeEnd = null"
               @input="$v.dtRecordRangeEnd.$touch()"
              @blur="$v.dtRecordRangeEnd.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dtRecordRangeEnd" color="green lighten-1" header-color="primary" light @change="menu2 = false"></v-date-picker>
        </v-menu>
      </v-col>
      <br />
      <v-col cols="12" offset-sm="3" sm="6">
        <div>
          <v-btn  :disabled="$v.$invalid" @click.prevent="nextPage"  color="success">Next</v-btn>
        </div>
        <!-- :disabled="$v.$invalid" -->
      </v-col>
    </v-row>
     <div class="disclaimer">{{this.disclaimer}}</div>
    </form>
    </div>
    
</template>

<script>
import moment from "moment";
import { required} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_08",
  data() {
    return {
      dtRecordRangeStart: new Date().toISOString().substr(0, 10),
      dtRecordRangeEnd: new Date().toISOString().substr(0, 10),
      menu1: false,
      menu2: false,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_08_disclaimer01
    };
  },
  validations: {
    dtRecordRangeStart: {
        required,
        minValue: value => value < new Date().toISOString()
    },
    dtRecordRangeEnd: {
        required,
        minValue: value => value < new Date().toISOString(),
        afterStartDate: (value,vm) => new Date(vm.dtRecordRangeEnd).getTime() >= new Date(vm.dtRecordRangeStart).getTime()
    },
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/dtRecordRangeStart", this.dtRecordRangeEnd);
      this.$store.commit("requestermodule/dtRecordRangeEnd", this.dtRecordRangeEnd);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    dtRecordRangeStartFormatted() {
      return this.dtRecordRangeStart ? moment(this.dtRecordRangeStart).format("MM-DD-YYYY") : "";
    },
    dtRecordRangeEndFormatted() {
      return this.dtRecordRangeEnd ? moment(this.dtRecordRangeEnd).format("MM-DD-YYYY") : "";
    },
    dtRecordRangeStartErrors(){
      const errors = [];
      if (!this.$v.dtRecordRangeStart.$dirty) return errors;
      !this.$v.dtRecordRangeStart.minValue && errors.push("Invalid Date");
      !this.$v.dtRecordRangeStart.required && errors.push("Start Date is required");
      return errors;
    },
     dtRecordRangeEndErrors(){
      const errors = [];
      if (!this.$v.dtRecordRangeEnd.$dirty) return errors;
      !this.$v.dtRecordRangeEnd.minValue && errors.push("Invalid Date");
      !this.$v.dtRecordRangeEnd.required && errors.push("End Date is required");
      !this.$v.dtRecordRangeEnd.afterStartDate && errors.push("End date must be greater than start date.");
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