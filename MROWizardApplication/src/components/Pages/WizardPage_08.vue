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
              :value="startDateFormatted"
              :error-messages="startDateErrors"
              clearable
              label="Start Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="startDate = null"
              @input="$v.startDate.$touch()"
              @blur="$v.startDate.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="startDate" @change="menu1 = false"></v-date-picker>
        </v-menu>
      </v-col>

      <v-col cols="12" offset-sm="2" sm="3" md="4">
        <v-menu v-model="menu2" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              :value="endDateFormatted"
              :error-messages="endDateErrors"
              clearable
              label="End Date"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="endDate = null"
               @input="$v.endDate.$touch()"
              @blur="$v.endDate.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="endDate" @change="menu2 = false"></v-date-picker>
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
      startDate: new Date().toISOString().substr(0, 10),
      endDate: new Date().toISOString().substr(0, 10),
      menu1: false,
      menu2: false,
      disclaimer : this.$store.state.ConfigModule.wp09_disclaimer
    };
  },
  validations: {
    startDate: {
        required,
        minValue: value => value < new Date().toISOString()
    },
    endDate: {
        required,
        minValue: value => value < new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;

       this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    startDateFormatted() {
      return this.startDate ? moment(this.startDate).format("MM-DD-YYYY") : "";
    },
    endDateFormatted() {
      return this.endDate ? moment(this.endDate).format("MM-DD-YYYY") : "";
    },
    startDateErrors(){
      const errors = [];
      if (!this.$v.startDate.$dirty) return errors;
      !this.$v.startDate.minValue && errors.push("Invalid Date");
      !this.$v.startDate.required && errors.push("Start Date is required");
      return errors;
    },
     endDateErrors(){
      const errors = [];
      if (!this.$v.endDate.$dirty) return errors;
      !this.$v.endDate.minValue && errors.push("Invalid Date");
      !this.$v.endDate.required && errors.push("End Date is required");
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