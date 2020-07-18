<template>
  <div class="center">
    <h1>When should this expire?</h1>

    <template>
       <!--Auth expiration selection box-->
      <v-layout row wrap>
        <v-col v-if="MROAuthExpireDateAfterNMonths" cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            :label="nAuthMonths+' months after signature date'"
            color="#e84700"
            :value="1"
            v-model="nSelectedCheckBox"
            @change="check(1)"
          ></v-checkbox>
        </v-col>
        <v-col v-if="MROAuthExpireDateSpecificDate" cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            label="On specific date"
            color="#e84700"
            :value="2"
            v-model="nSelectedCheckBox"
            @change="check(2)"
          ></v-checkbox>
          <div v-if="nSelectedCheckBox==2">
            <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                  transition="scale-transition"
                  offset-y
                  :value="dSpecificFormatted"
                  placeholder="MM-DD-YYYY"
                  :error-messages="dSpecificErrors"
                  clearable
                  label="SPECIFY DATE"
                  readonly
                  v-bind="attrs"
                  v-on="on"
                  @click:clear="dSpecific = null"
                  @input="$v.dSpecific.$touch()"
                  @blur="$v.dSpecific.$touch()"
                ></v-text-field>
              </template>
              <v-date-picker
                ref="picker"
                :min="new Date().toISOString().substr(0, 10)"
                v-model="dSpecific"
                color="green lighten-1"
                header-color="primary"
                light
                @change="menu1 = false"
              ></v-date-picker>
            </v-menu>
          </div>
        </v-col>

        <v-col v-if="MROAuthExpireDateEventOccurs" cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            label="When a specific event occur."
            color="#e84700"
            :value="3"
            v-model="nSelectedCheckBox"
            @change="check(3)"
          ></v-checkbox>
          <div v-if="nSelectedCheckBox==3">
            <v-textarea counter v-model="sAuthSpecificEvent" rows="2" label="SPECIFY EVENT HERE"></v-textarea>
          </div>
        </v-col>
        <v-col cols="6" offset-sm="4" sm="2">
           <v-btn :disabled="nSelectedCheckBox[0]==null" @click.prevent="nextPage" class="next">Next</v-btn>
        </v-col>
        <v-col cols="6" sm="2">
          <v-btn @click.prevent="skipPage" class="next">Skip</v-btn>
        </v-col>
      </v-layout>
    </template>
  </div>
</template>

<script>
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_12",
  data() {
    return {
      nAuthMonths: this.$store.state.ConfigModule.nAuthExpirationMonths,
      nSelectedCheckBox: [],
      dSpecific: null,
      menu1: false,
      dtAuthExpire: "",
      sAuthSpecificEvent: "",

      //Show and Hide Fields Values
      MROAuthExpireDateAfterNMonths: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateAfterNMonths,
      MROAuthExpireDateSpecificDate: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateSpecificDate,
      MROAuthExpireDateEventOccurs: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateEventOccurs
    };
  },
   watch: {
      menu1 (val) {
        val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
      },
    },
  //Auth expiration validations
  validations: {
    dSpecific: {
      required,
      minValue: value => value > new Date().toISOString()
    }
  },
  methods: {
    nextPage() {
      //Switch based on selection and set state values
      switch (this.nSelectedCheckBox[0]) {
        case 1:
          var dt = new Date();
          dt.setMonth(dt.getMonth() + this.nAuthMonths);
          this.dtAuthExpire = dt.toISOString();
          break;
        case 2:
          this.dtAuthExpire = this.dSpecific;
          break;
        case 3:
          this.dtAuthExpire = null;
          break;
        default:
          var dt1 = new Date();
          this.dtAuthExpire = dt1.setMonth(dt1.getMonth() + this.nAuthMonths);
      }
      this.$store.commit("requestermodule/dtAuthExpire", this.dtAuthExpire);
      this.$store.commit(
        "requestermodule/sAuthSpecificEvent",
        this.sAuthSpecificEvent
      );

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
    },
    //Check for which checkbox is selected
    check(id) {
      this.nSelectedCheckBox = [];
      this.nSelectedCheckBox.push(id);
    },
    skipPage(){
      var dt = new Date();
      dt.setMonth(dt.getMonth() + this.nAuthMonths);  
      this.dtAuthExpire = dt.toISOString();
      this.$store.commit("requestermodule/dtAuthExpire", this.dtAuthExpire);
      this.nSelectedCheckBox = [];
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    //Change date format
    dSpecificFormatted() {
      return this.dSpecific ? moment(this.dSpecific).format("MM-DD-YYYY") : "";
    },
    //Date validation
    dSpecificErrors() {
      const errors = [];
      if (!this.$v.dSpecific.$dirty) return errors;
      !this.$v.dSpecific.minValue && errors.push("Date should be one day ahed from today.");
      !this.$v.dSpecific.required && errors.push("Date is required");
      return errors;
    }
  }
};
</script>
