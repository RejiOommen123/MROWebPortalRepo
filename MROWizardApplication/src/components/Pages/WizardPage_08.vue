<template>
  <div class="center">
    <h1>Please specify an approximate date range for records.</h1>
    <form>
    <v-row>
      <!-- Start Date input -->
      <v-col v-if="MRORecordsDateRange" cols="12" offset-sm="1" sm="3" md="4">
        <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              transition="scale-transition"
              offset-y
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
              :disabled="bRecordMostRecentVisit || bSpecifyVisit"
            ></v-text-field>
          </template>
          <v-date-picker 
          ref="startpicker" 
          v-model="dtRecordRangeStart" 
          color="green lighten-1" 
          header-color="primary" 
          light @change="menu1 = false"
          :max="new Date().toISOString().substr(0, 10)"
          ></v-date-picker>
        </v-menu>
      </v-col>
      <!-- End Date input -->
      <v-col v-if="MRORecordsDateRange" cols="12" offset-sm="2" sm="3" md="4">
        <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              transition="scale-transition"
              offset-y
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
              :disabled="bRecordMostRecentVisit || bSpecifyVisit"
            ></v-text-field>
          </template>
          <v-date-picker 
          ref="endpicker" 
          v-model="dtRecordRangeEnd" 
          color="green lighten-1" 
          header-color="primary" 
          light 
          @change="menu2 = false"
          :max="new Date().toISOString().substr(0, 10)"
          ></v-date-picker>
        </v-menu>
      </v-col>
      <v-col v-if="MRORecordsMostRecentVisit" cols="12" offset-sm="2" sm="8" >
        <v-checkbox
          hide-details
          class="checkboxBorder"
          v-model="bRecordMostRecentVisit"
          color="white"
          label="Most Recent Visit."
          @change="CheckedMostRecentVisit()"
      ></v-checkbox>
      </v-col>
      <v-col v-if="MROSpecifyVisit" cols="12" offset-sm="2" sm="8" >
        <v-checkbox
          hide-details
          class="checkboxBorder"
          v-model="bSpecifyVisit"
          color="white"
          @change="CheckedSpecifyVisit()"
          label="Specify Visit/Injury/Event (ER, elbow surgery, car accident, etc.)"
      ></v-checkbox>
      <div v-if="bSpecifyVisit">
          <v-textarea  
          class="TextAreaBg"  
          v-model="sSpecifyVisitText" 
          maxlength="100" 
          rows="3" 
          counter 
          label="Specify Visit"
          :error-messages="sSpecifyVisitTextErrors"
          @input="$v.sSpecifyVisitText.$touch()"
          @blur="$v.sSpecifyVisitText.$touch()"
          ></v-textarea>
      </div>
      </v-col>
      <br />
      <v-col cols="12" offset-sm="3" sm="6" style="padding-top:0">
        <div>
          <v-btn v-if="bRecordMostRecentVisit"  @click.prevent="nextPage"  class="next">Next</v-btn>
          <v-btn v-if="bSpecifyVisit" :disabled="sSpecifyVisitText==''" @click.prevent="nextPage"  class="next">Next</v-btn>
          <v-btn v-if="!bRecordMostRecentVisit && !bSpecifyVisit" :disabled="$v.dtRecordRangeStart.$invalid || $v.dtRecordRangeEnd.$invalid" @click.prevent="nextPage"  class="next">Next</v-btn>
        </div>
      </v-col>
    </v-row>
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
      bRecordMostRecentVisit:false,
      bSpecifyVisit:false,
      sSpecifyVisitText:'',
      menu1: false,
      menu2: false,

      MRORecordsDateRange: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MRORecordsDateRange,
      MRORecordsMostRecentVisit: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MRORecordsMostRecentVisit,
      MROSpecifyVisit: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROSpecifyVisit,
    };
  },
   watch: {
      menu1 (val) {
        val && setTimeout(() => (this.$refs.startpicker.activePicker = 'YEAR'))
      },
      menu2 (val) {
        val && setTimeout(() => (this.$refs.endpicker.activePicker = 'YEAR'))
      },
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
    sSpecifyVisitText :{
        required
    },
  },
  methods: {
    CheckedMostRecentVisit(){
      if(this.bRecordMostRecentVisit)
      {
        this.$v.dtRecordRangeStart.$reset();
        this.$v.dtRecordRangeEnd.$reset();
        this.dtRecordRangeStart=null;
        this.dtRecordRangeEnd=null;
        this.bSpecifyVisit=false;
        this.sSpecifyVisitText="";
        this.$v.sSpecifyVisitText.$reset();
      }
    },
    CheckedSpecifyVisit() {
      if(this.bSpecifyVisit)
      {
        this.$v.dtRecordRangeStart.$reset();
        this.$v.dtRecordRangeEnd.$reset();
        this.dtRecordRangeStart=null;
        this.dtRecordRangeEnd=null;
        this.bRecordMostRecentVisit=false;
      }
      if (!this.bSpecifyVisit) {
        this.sSpecifyVisitText = "";
      }
    },   
    nextPage() {
      // if(this.bRecordMostRecentVisit){
      //   this.$store.commit("requestermodule/bRecordMostRecentVisit", this.bRecordMostRecentVisit);
      //   this.$store.commit("requestermodule/dtRecordRangeStart", '');
      //   this.$store.commit("requestermodule/dtRecordRangeEnd", '');
      // }
      // else{
      //   this.$store.commit("requestermodule/bRecordMostRecentVisit", this.bRecordMostRecentVisit);
      //   this.$store.commit("requestermodule/dtRecordRangeStart", this.dtRecordRangeStart);
      //   this.$store.commit("requestermodule/dtRecordRangeEnd", this.dtRecordRangeEnd);
      // }
      
      this.$store.commit("requestermodule/dtRecordRangeStart", this.dtRecordRangeStart);
      this.$store.commit("requestermodule/dtRecordRangeEnd", this.dtRecordRangeEnd);
      this.$store.commit("requestermodule/bRecordMostRecentVisit", this.bRecordMostRecentVisit);
      this.$store.commit("requestermodule/bSpecifyVisit", this.bSpecifyVisit);
      this.$store.commit("requestermodule/sSpecifyVisitText", this.sSpecifyVisitText);

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
      !this.$v.dtRecordRangeStart.required && errors.push("Start Date is required");
      !this.$v.dtRecordRangeStart.minValue && errors.push("This date cannot be future date");    
      return errors;
    },
     dtRecordRangeEndErrors(){
      const errors = [];
      if (!this.$v.dtRecordRangeEnd.$dirty) return errors;
       !this.$v.dtRecordRangeEnd.required && errors.push("End Date is required");
      !this.$v.dtRecordRangeEnd.minValue && errors.push("This date cannot be future date");
      !this.$v.dtRecordRangeEnd.afterStartDate && errors.push("End Date cannot be before Start Date.");
      return errors;
    },
     sSpecifyVisitTextErrors(){
      const errors = [];
      if (!this.$v.sSpecifyVisitText.$dirty) return errors;
      !this.$v.sSpecifyVisitText.required && errors.push("Specify visit is required.");
      return errors;
    }
  }
};
</script>
