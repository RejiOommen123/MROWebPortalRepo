<template>
  <div class="center">
    <br />
    <br />
    <h1>Specify an approximate*<br/>Date Range for records.</h1>
    
    <v-row>
      <v-col cols="12" offset-sm="1" sm="3" md="4">
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dRecordRangeStartFormatted"
              :error-messages="dRecordRangeStartErrors"
              clearable
              label="Start Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dRecordRangeStart = null"
              @input="$v.dRecordRangeStart.$touch()"
              @blur="$v.dRecordRangeStart.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dRecordRangeStart" @change="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>

      <v-col cols="12" offset-sm="2" sm="3" md="4">
        <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dRecordRangeEndFormatted"
              :error-messages="dRecordRangeEndErrors"
              clearable
              label="End Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dRecordRangeEnd = null"
               @input="$v.dRecordRangeEnd.$touch()"
              @blur="$v.dRecordRangeEnd.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dRecordRangeEnd" @change="menu2 = false"></v-date-picker>
        </v-menu>
      </v-col>
      <br />
      <v-col cols="12" offset-sm="3" sm="6">
        <div>
          <v-btn  :disabled="$v.$invalid" @click.prevent="nextPage" color="success">Next</v-btn>
        </div>
        <!-- :disabled="$v.$invalid" -->
      </v-col>
    </v-row>
     <div class="disclaimer">{{this.disclaimer}}</div>
    </div>
</template>

<script>
import moment from "moment";
import { required} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_08",
  data() {
    return {
      dRecordRangeStart: new Date().toISOString().substr(0, 10),
      dRecordRangeEnd: new Date().toISOString().substr(0, 10),
      menu1: false,
      menu2: false,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_08_disclaimer01
    };
  },
  validations: {
    dRecordRangeStart: {
        required,
        minValue: value => value < new Date().toISOString()
    },
    dRecordRangeEnd: {
        required,
        minValue: value => value < new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/dRecordRangeStart", this.dRecordRangeEnd);
      this.$store.commit("requestermodule/dRecordRangeEnd", this.dRecordRangeEnd);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    dRecordRangeStartFormatted() {
      return this.dRecordRangeStart ? moment(this.dRecordRangeStart).format("MM-DD-YYYY") : "";
    },
    dRecordRangeEndFormatted() {
      return this.dRecordRangeEnd ? moment(this.dRecordRangeEnd).format("MM-DD-YYYY") : "";
    },
    dRecordRangeStartErrors(){
      const errors = [];
      if (!this.$v.dRecordRangeStart.$dirty) return errors;
      !this.$v.dRecordRangeStart.minValue && errors.push("Invalid Date");
      !this.$v.dRecordRangeStart.required && errors.push("Start Date is required");
      return errors;
    },
     dRecordRangeEndErrors(){
      const errors = [];
      if (!this.$v.dRecordRangeEnd.$dirty) return errors;
      !this.$v.dRecordRangeEnd.minValue && errors.push("Invalid Date");
      !this.$v.dRecordRangeEnd.required && errors.push("End Date is required");
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