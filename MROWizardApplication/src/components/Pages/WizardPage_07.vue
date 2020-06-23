<template>
  <div class="center">
    <form>
       <h1 v-if="bAreYouPatient">What is your address?</h1>
       <h1 v-else>What is patient's address?</h1>
      <v-row>
       
        <v-col cols="12" offset-sm="2" sm="3">
          <v-text-field
            type="number"
            v-model="zipcode"
            :error-messages="zipcodeErrors"
            label="ZipCode"
            required
            @input="$v.zipcode.$touch()"
            @blur="$v.zipcode.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
          <v-text-field
            v-model="cityName"
            :error-messages="cityErrors"
            label="City"
            required
            @input="$v.cityName.$touch()"
            @blur="$v.cityName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
          <v-text-field
            v-model="stateName"
            :error-messages="stateErrors"
            label="State"
            required
            @input="$v.stateName.$touch()"
            @blur="$v.stateName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-text-field
            v-model="streetAdd"
            :error-messages="streetErrors"
            label="Street"
            required
            @input="$v.streetAdd.$touch()"
            @blur="$v.streetAdd.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-btn @click.prevent="nextPage" :disabled="$v.$invalid" color="success">Next</v-btn>
        </v-col>
      </v-row>
    </form>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import {
  required,
  maxLength,
  minLength,
  numeric
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      zipcode: this.$store.state.requestermodule.zipcode,
      cityName: this.$store.state.requestermodule.cityName,
      stateName: this.$store.state.requestermodule.stateName,
      streetAdd: this.$store.state.requestermodule.streetAdd
    };
  },
  mixins: [validationMixin],
  validations: {
    zipcode: {
      required,
      maxLength: maxLength(5),
      minLength: minLength(5),
      numeric
    },
    cityName: { required },
    stateName: { required },
    streetAdd: { required }
  },
  computed: {
     bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },  
    zipcodeErrors() {
      const errors = [];
      if (!this.$v.zipcode.$dirty) return errors;
      !this.$v.zipcode.maxLength && errors.push("Zipcode must be 5 digit");
      !this.$v.zipcode.minLength && errors.push("Zipcode must be 5 digit");
      !this.$v.zipcode.numeric && errors.push("Zipcode must me numeric.");
      !this.$v.zipcode.required && errors.push("Name is required.");
      return errors;
    },
    cityErrors() {
      const errors = [];
      if (!this.$v.cityName.$dirty) return errors;
      !this.$v.cityName.required && errors.push("City is required.");
      return errors;
    },
    stateErrors() {
      const errors = [];
      if (!this.$v.stateName.$dirty) return errors;
      !this.$v.stateName.required && errors.push("State is required.");
      return errors;
    },
    streetErrors() {
      const errors = [];
      if (!this.$v.streetAdd.$dirty) return errors;
      !this.$v.streetAdd.required && errors.push("Street Address is required.");
      return errors;
    }
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.commit("requestermodule/mutatezipcode", this.zipcode);
      this.$store.commit("requestermodule/mutatecityName", this.cityName);
      this.$store.commit("requestermodule/mutatestateName", this.stateName);
      this.$store.commit("requestermodule/mutatestreetAdd", this.streetAdd);
      // this.$store.commit("ConfigModule/mutatepageNumerical", 7);
      // this.$store.commit("ConfigModule/mutateCurrentPage", "page-7");
       this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>

<style scoped>
/* .center {
  text-align: center;
} */
</style>