<template>
  <div class="center">
    <h1>
      When should this
      <br />Authorization expire?
    </h1>

    <template>
      <v-layout row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label= "authMonths+' months after signature date'"
            color="green"
            :value="1"
            v-model="selectedCheckBox"
            @change="check(1)"

          ></v-checkbox>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="On specific date"
            color="green"
            :value="2"
            v-model="selectedCheckBox"
            @change="check(2)"
          ></v-checkbox>
          <div v-if="selectedCheckBox==2">
              <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field            
              :value="specificDateFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="specificDateErrors"
              clearable
              label="Specify Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="specificDate = null"
              @input="$v.specificDate.$touch()"
              @blur="$v.specificDate.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="specificDate" @change="menu1 = false"></v-date-picker>
        </v-menu>
          </div>
        </v-col>
        
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            label="When a specific event occur."
            color="green"
            :value="3"
            v-model="selectedCheckBox"
            @change="check(3)"
          ></v-checkbox>
          <div v-if="selectedCheckBox==3">
              <v-textarea counter v-model="authSpecificEvent" label="Specify Event Here"></v-textarea>
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
      authMonths: this.$store.state.ConfigModule.wp13_authMonths,
      selectedCheckBox:[],
      other: false,
      specificDate: null,//new Date().toISOString().substr(0, 10),
      menu1: false,
      authExpireDate: '',
      authSpecificEvent: ''
    };
  },
  validations: {
    specificDate: {
        required,
        minValue: value => value < new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
        switch(this.selectedCheckBox[0]) {
        case 1:
            var dt = new Date();
            dt.setMonth( dt.getMonth() + this.authMonths );            
            this.authExpireDate = dt.toISOString();
            console.log(this.authExpireDate);
            break;
        case 2:
            this.authExpireDate = this.specificDate;          
            console.log(this.authExpireDate);
            break;
        case 3:
            this.authExpireDate = null;
            console.log(this.authSpecificEvent);
            break;
        default:
            var dt1 = new Date();
            this.authExpireDate = dt1.setMonth( dt1.getMonth() + this.authMonths );  
            console.log(this.authExpireDate);
        }        
        this.$store.commit("requestermodule/mutateauthExpireDate", this.authExpireDate);
        this.$store.commit("requestermodule/mutateauthSpecificEvent", this.authSpecificEvent);
        this.$store.state.ConfigModule.showBackBtn = true;  
        this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {

        this.selectedCheckBox = [];
        this.selectedCheckBox.push(id);
        console.log(this.selectedCheckBox[0]);
    }
  },
  computed: {
    specificDateFormatted() {
      return this.specificDate ? moment(this.specificDate).format("MM-DD-YYYY") : "";
    },
    specificDateErrors(){
      const errors = [];
      if (!this.$v.specificDate.$dirty) return errors;
      !this.$v.specificDate.minValue && errors.push("Invalid Date");
      !this.$v.specificDate.required && errors.push("End Date is required");
      return errors;
    }
  }
};
</script>

<style scoped>

</style>