<template>
  <div class="center">
    <h1>Please specify an approximate date range for records.</h1>
    <form>
    <v-row>
      <!-- Start Date input -->
      <v-col cols="12" offset-sm="1" sm="3" md="4">
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dtRecordRangeStartFormatted"
              :error-messages="dtRecordRangeStartErrors"
              clearable
              label="START DATE"
              placeholder="MM-DD-YYYY"
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
      <!-- End Date input -->
      <v-col cols="12" offset-sm="2" sm="3" md="4">
        <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="dtRecordRangeEndFormatted"
              :error-messages="dtRecordRangeEndErrors"
              clearable
              label="END DATE"
               placeholder="MM-DD-YYYY"
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
          <v-btn  :disabled="$v.$invalid" @click.prevent="nextPage"  class="next">Next</v-btn>
        </div>
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
      dtRecordRangeStart: '',
      dtRecordRangeEnd: '',
      menu1: false,
      menu2: false,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_08_disclaimer01
    };
  },
  // Start and end date validations
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
      this.$store.commit("requestermodule/dtRecordRangeStart", this.dtRecordRangeStart);
      this.$store.commit("requestermodule/dtRecordRangeEnd", this.dtRecordRangeEnd);

      //Partial Requester Data Save Start
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
      if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
      {
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      }
      //Partial Requester Data Save End
    
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
     //Date Format setter
    dtRecordRangeStartFormatted() {
      return this.dtRecordRangeStart ? moment(this.dtRecordRangeStart).format("MM-DD-YYYY") : "";
    },
    dtRecordRangeEndFormatted() {
      return this.dtRecordRangeEnd ? moment(this.dtRecordRangeEnd).format("MM-DD-YYYY") : "";
    },
     //Start and End Date validation error message setter
    dtRecordRangeStartErrors(){
      const errors = [];
      if (!this.$v.dtRecordRangeStart.$dirty) return errors;
      !this.$v.dtRecordRangeStart.minValue && errors.push("This date cannot be future date");
      !this.$v.dtRecordRangeStart.required && errors.push("Start Date is required");
      return errors;
    },
     dtRecordRangeEndErrors(){
      const errors = [];
      if (!this.$v.dtRecordRangeEnd.$dirty) return errors;
      !this.$v.dtRecordRangeEnd.minValue && errors.push("This date cannot be future date");
      !this.$v.dtRecordRangeEnd.required && errors.push("End Date is required");
      !this.$v.dtRecordRangeEnd.afterStartDate && errors.push("End Date cannot be before Start Date.");
      return errors;
    }
  }
};
</script>
