<template>
  <div class="center">
    <h1>What date do you need record(s) by?</h1>
    <v-row>
      <form>
        <!-- Date picker input to set deadline -->
        <v-col v-if="MRORequestDeadlineDate" cols="12" offset-sm="2" sm="6" md="8">
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
                @click:clear="dtDeadline  = null"
                @input="$v.dtDeadline .$touch()"
                @blur="$v.dtDeadline .$touch()"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="dtDeadline "
              color="green lighten-1"
              header-color="primary"
              light
              @change="menu1 = false"
            ></v-date-picker>
          </v-menu>
        </v-col>
        <v-spacer></v-spacer>

        <v-col cols="12" offset-sm="3" sm="6">
          <div>
            <v-btn @click.prevent="nextPage" :disabled="$v.$invalid" class="next">Next</v-btn>
          </div>
        </v-col>
      </form>
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
      dtDeadline: moment()
        .add(1, "days")
        .toISOString()
        .substr(0, 10),
      menu1: false,
      disclaimer: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_16_disclaimer01,

      //Show and Hide Fields Values
      MRORequestDeadlineDate: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORequestDeadlineDate
    };
  },
  //Date Validation
  validations: {
    dtDeadline: {
      required,
      minValue: value => value > new Date().toISOString()
    }
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/dtDeadline ", this.dtDeadline);

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
    dateFormatted() {
      return this.dtDeadline
        ? moment(this.dtDeadline).format("MM-DD-YYYY")
        : "";
    },
    //Date validation error message setter
    dateErrors() {
      const errors = [];
      if (!this.$v.dtDeadline.$dirty) return errors;
      !this.$v.dtDeadline.minValue && errors.push("Deadline date must be greater than today's date.");
      !this.$v.dtDeadline.required && errors.push("Date is required");
      return errors;
    }
  }
};
</script>
