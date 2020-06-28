<template>
  <div class="center">
    <h1>
      When should this
      <br />Authorization expire?
    </h1>

    <template>
      <v-layout row wrap>
        <v-col v-if="MROAuthExpireDateAfterNMonths" cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label= "nAuthMonths+' months after signature date'"
            color="green"
            :value="1"
            v-model="nSelectedCheckBox"
            @change="check(1)"

          ></v-checkbox>
        </v-col>
        <v-col v-if="MROAuthExpireDateSpecificDate" cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="On specific date"
            color="green"
            :value="2"
            v-model="nSelectedCheckBox"
            @change="check(2)"
          ></v-checkbox>
          <div v-if="nSelectedCheckBox==2">
              <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field            
              :value="dSpecificFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="dSpecificErrors"
              clearable
              label="Specify Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="dSpecific = null"
              @input="$v.dSpecific.$touch()"
              @blur="$v.dSpecific.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="dSpecific" color="green lighten-1" header-color="primary" light @change="menu1 = false"></v-date-picker>
        </v-menu>
          </div>
        </v-col>
        
        <v-col v-if="MROAuthExpireDateEventOccurs" cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="When a specific event occur."
            color="green"
            :value="3"
            v-model="nSelectedCheckBox"
            @change="check(3)"
          ></v-checkbox>
          <div v-if="nSelectedCheckBox==3">
              <v-textarea counter v-model="sAuthSpecificEvent" rows="2" label="Specify Event Here"></v-textarea>
          </div>
        </v-col>
      </v-layout>
    </template>
    <div>
      <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
    </div>
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
      nSelectedCheckBox:[],
      dSpecific: null,//new Date().toISOString().substr(0, 10),
      menu1: false,
      dAuthExpire: '',
      sAuthSpecificEvent: '',

      //Show and Hide Fields Values
      MROAuthExpireDateAfterNMonths : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROAuthExpireDateAfterNMonths,
      MROAuthExpireDateSpecificDate : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROAuthExpireDateSpecificDate,
      MROAuthExpireDateEventOccurs : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROAuthExpireDateEventOccurs
    };
  },
  validations: {
    dSpecific: {
        required,
        minValue: value => value > new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
        switch(this.nSelectedCheckBox[0]) {
        case 1:
            var dt = new Date();
            dt.setMonth( dt.getMonth() + this.nAuthMonths );            
            this.dAuthExpire = dt.toISOString();
            console.log(this.dAuthExpire);
            break;
        case 2:
            this.dAuthExpire = this.dSpecific;          
            console.log(this.dAuthExpire);
            break;
        case 3:
            this.dAuthExpire = null;
            console.log(this.sAuthSpecificEvent);
            break;
        default:
            var dt1 = new Date();
            this.dAuthExpire = dt1.setMonth( dt1.getMonth() + this.nAuthMonths );  
            console.log(this.dAuthExpire);
        }        
        this.$store.commit("requestermodule/dAuthExpire", this.dAuthExpire);
        this.$store.commit("requestermodule/sAuthSpecificEvent", this.sAuthSpecificEvent);
        this.$store.state.ConfigModule.showBackBtn = true;  
        this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {

        this.nSelectedCheckBox = [];
        this.nSelectedCheckBox.push(id);
        console.log(this.nSelectedCheckBox[0]);
    }
  },
  computed: {
    dSpecificFormatted() {
      return this.dSpecific ? moment(this.dSpecific).format("MM-DD-YYYY") : "";
    },
    dSpecificErrors(){
      const errors = [];
      if (!this.$v.dSpecific.$dirty) return errors;
      !this.$v.dSpecific.minValue && errors.push("Invalid Date");
      !this.$v.dSpecific.required && errors.push("End Date is required");
      return errors;
    }
  }
};
</script>

<style scoped>

</style>