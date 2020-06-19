<template>
  <div class="center">
    <br />
    <br />
    <v-row>
    <v-col cols="12" offset-sm="2" sm="6" md="8">
       <h1>When was the Patient Born ?</h1>
        <h4>(MM/DD/YYYY)</h4>
      <v-menu v-model="menu1" :close-on-content-click="false" max-width="290">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field            
              :value="dateFormatted"
              placeholder="MM-DD-YYYY"
              :error-messages="dateErrors"
              clearable
              label="Date of Birth"
              readonly
              v-bind="attrs"
              v-on="on"
              @click:clear="date = null"
              @input="$v.date.$touch()"
              @blur="$v.date.$touch()"
            ></v-text-field>
          </template>
          <v-date-picker v-model="date" @change="menu1 = false"></v-date-picker>
        </v-menu>
    </v-col>
    <v-spacer></v-spacer>
    <br />
    <v-col cols="12" offset-sm="3" sm="6">
    <div>
      <v-btn @click.prevent="nextPage"  color="success">Submit</v-btn>
    </div>
    <!-- :disabled="$v.$invalid" -->
    </v-col>
  </v-row>
    
  </div>
</template>

<script>
import moment from "moment";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_08",
  data() {
    return {
     date: null,//new Date().toISOString().substr(0, 10),
      menu1: false,
    };
  },
  validations: {
    date: {
        required,
        minValue: value => value < new Date().toISOString()
    },
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.ConfigModule.showBackBtn = true;
      this.$store.commit("requestermodule/mutatebDay", this.bDay);
      this.$store.commit("ConfigModule/mutatepageNumerical", 8);
      this.$store.commit("ConfigModule/mutateCurrentPage", "page-8");
      
    }
  },
  computed: {
    dateFormatted() {
      return this.date ? moment(this.date).format("MM-DD-YYYY") : "";
    },
    dateErrors(){
      const errors = [];
      if (!this.$v.date.$dirty) return errors;
      !this.$v.date.minValue && errors.push("Invalid Date");
      !this.$v.date.required && errors.push("End Date is required");
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