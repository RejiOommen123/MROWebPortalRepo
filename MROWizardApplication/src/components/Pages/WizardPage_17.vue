<template>
  <div class="center">
    <h1>When should this request expire?</h1>

    <template>
       <!--Auth expiration selection box-->
      <v-layout row wrap>
        <v-col v-if="MROAuthExpireDateAfterNMonths" cols="12" offset-sm="2" sm="8">
          <v-checkbox
            hide-details
            dark
            class="checkboxBorder"
            :label="nAuthMonths+' months after signature date'"
            color="white"
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
            color="white"
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
                :min="mindate"
                :max="maxdate"
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
            label="When a specific event occurs."
            color="white"
            :value="3"
            v-model="nSelectedCheckBox"
            @change="check(3)"
          ></v-checkbox>
          <div v-if="nSelectedCheckBox==3">
            <v-textarea class="TextAreaBg"  counter maxlength="100" v-model="sAuthSpecificEvent" rows="2" label="SPECIFY EVENT HERE"></v-textarea>
          </div>
        </v-col>
        <v-col v-if="nSelectedCheckBox[0]==1 || nSelectedCheckBox[0]==3 || nSelectedCheckBox[0]==null" cols="6" offset-sm="4" sm="2">
           <v-btn :disabled="nSelectedCheckBox[0]==null" @click.once="nextPage" :key="buttonKey" class="next">Next</v-btn>
        </v-col>
        <v-col v-if="nSelectedCheckBox[0]==2" cols="6" offset-sm="4" sm="2">
           <v-btn :disabled="$v.dSpecific.$invalid" @click.once="nextPage" :key="buttonKey" class="next">Next</v-btn>
        </v-col>
        <v-col cols="6" sm="2">
          <v-btn @click.once="skipPage" :key="buttonKey" class="next">Skip</v-btn>
        </v-col>
      </v-layout>
    </template>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_17",  
    activated(){
      this.buttonKey++;
      // var dt = new Date();
      // dt.setMonth(dt.getMonth() + 60);
      // this.maxdate = dt.toISOString();
      this.maxdate=moment()
          .add(5, "years")
          .toISOString()
          .substr(0, 10)

      // var dt2 =new Date();
      // dt2.setDate(dt2.getDate() + 1);
      // this.mindate = dt2.toISOString();

      this.mindate=moment()
          .add(1, "days")
          .toISOString()
          .substr(0, 10)
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
    },
    ...mapState({
      nAuthMonths : state => state.ConfigModule.nAuthExpirationMonths,
      //Show and Hide Fields Values
      MROAuthExpireDateAfterNMonths: state => state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateAfterNMonths,
      MROAuthExpireDateSpecificDate: state => state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateSpecificDate,
      MROAuthExpireDateEventOccurs: state => state.ConfigModule
        .apiResponseDataByLocation.oFields.MROAuthExpireDateEventOccurs
    }),
  },
  data() {
    return {
      nSelectedCheckBox: this.$store.state.ConfigModule.nSelectedAuthExpire,
      dSpecific: this.$store.state.ConfigModule.nSelectedAuthExpire[0] == 2 ? this.$store.state.requestermodule.dtAuthExpire : null,
      menu1: false,
      dtAuthExpire: this.$store.state.requestermodule.dtAuthExpire,
      sAuthSpecificEvent: this.$store.state.requestermodule.sAuthSpecificEvent,
      maxdate:'',
      mindate:'',
      buttonKey:1,
    };
  },
  methods: {
    nextPage() {
      //Switch based on selection and set state values
      switch (this.nSelectedCheckBox[0]) {
        case 1:
          this.dtAuthExpire = moment()
          .add(this.nAuthMonths, "months")
          .toISOString()
          .substr(0, 10)
          this.sAuthSpecificEvent='';
          break;
        case 2:
          this.dtAuthExpire = this.dSpecific;
          this.sAuthSpecificEvent='';
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
      this.$store.commit("ConfigModule/nSelectedAuthExpire",this.nSelectedCheckBox);
      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');

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
      this.$store.commit("ConfigModule/nSelectedAuthExpire",[]);
      this.$store.commit("requestermodule/dtAuthExpire", this.dtAuthExpire);
      this.$store.commit("requestermodule/sAuthSpecificEvent",'');
      this.nSelectedCheckBox = [];
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  //Auth expiration validations
  validations: {
    dSpecific: {
      required,
      minValue: value => value > new Date().toISOString()
    }
  },
  watch: {
      menu1 (val) {
        val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
      },
   },
};
</script>
