<template>
  <div class="center">
    <div class="control-group">
      <h1 v-if="bAreYouPatient">What is your full legal name?</h1>
      <h1 v-else>What is the patient’s full legal name?</h1>
    </div>
    <form>
      <v-row>
        <v-col cols="12" sm="4" >
          <v-text-field
            label="FIRST NAME"
            v-model="sPatientFirstName"
            :error-messages="sPatientFirstNameError"
            required
            maxlength="30"
            @input="$v.sPatientFirstName.$touch()"
            @blur="$v.sPatientFirstName.$touch()"
          ></v-text-field>
        </v-col>
        <v-col v-if="MROPatientMiddleName" cols="12" sm="4" >
          <v-text-field label="MIDDLE NAME" maxlength="30" v-model="sPatientMiddleName"></v-text-field>
        </v-col>
        <v-col cols="12" sm="4" >
          <v-text-field
            label="LAST NAME"
            v-model="sPatientLastName"
            :error-messages="sPatientLastNameError"
            required
            maxlength="30"
            @input="$v.sPatientLastName.$touch()"
            @blur="$v.sPatientLastName.$touch()"
          ></v-text-field>
        </v-col>
        <!-- bPatientNameChanged -->
        <v-col v-if="MROPatientNameChanged" style="padding-top:0px;padding-bottom:0px;" cols="12" offset-sm="1" sm="10">
          <v-checkbox
            hide-details
            v-model="bPatientNameChanged"
            color="white"
            @change="checked()"
            label="Please check here if name was different at the time of visit (examples: maiden name, minor's name, alias)"
          ></v-checkbox>
        </v-col>
        <!-- TODO:add show and hide for previous name after adding these fields in db -->
        <template v-if="bPatientNameChanged">
          <v-col style="padding-top:0px;padding-bottom:0px;" cols="12" sm="4" >
            <v-text-field
              label="FIRST NAME"
              v-model="sPatientPreviousFirstName"
              :error-messages="sPatientPreviousFirstNameError"
              required
              maxlength="30"
              @input="$v.sPatientPreviousFirstName.$touch()"
              @blur="$v.sPatientPreviousFirstName.$touch()"
            ></v-text-field>
          </v-col>
          <v-col style="padding-top:0px;padding-bottom:0px;" v-if="MROPatientPreviousMiddleName" cols="12" sm="4" >
            <v-text-field label="MIDDLE NAME" maxlength="30" v-model="sPatientPreviousMiddleName"></v-text-field>
          </v-col>
          <v-col style="padding-top:0px;padding-bottom:0px;" cols="12" sm="4">
            <v-text-field
              label="LAST NAME"
              v-model="sPatientPreviousLastName"
              :error-messages="sPatientPreviousLastNameError"
              required
              maxlength="30"
              @input="$v.sPatientPreviousLastName.$touch()"
              @blur="$v.sPatientPreviousLastName.$touch()"
            ></v-text-field>
          </v-col>
        </template>
        <v-col v-if="bPatientNameChanged" cols="12" offset-sm="3" sm="6">
          <v-btn class="mr-4 next" @click.prevent="nextPage" :disabled="$v.$invalid">Next</v-btn>
        </v-col>
        <v-col v-else cols="12" offset-sm="3" sm="6">
          <v-btn class="mr-4 next" @click.prevent="nextPage" :disabled="$v.sPatientFirstName.$invalid  || $v.sPatientLastName.$invalid ">Next</v-btn>
        </v-col>
        <v-col v-if="disclaimer!='' && bPatientNameChanged==true" cols="12" sm="12">
        <div class="disclaimer">{{bAreYouPatient ? disclaimer : disclaimer02}}</div>
        </v-col>
      </v-row>
    </form>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_05",
  data() {
    return {
      sPatientFirstName: this.$store.state.requestermodule.sPatientFirstName,
      sPatientLastName: this.$store.state.requestermodule.sPatientLastName,
      sPatientMiddleName: this.$store.state.requestermodule.sPatientMiddleName,
      sPatientPreviousFirstName: this.$store.state.requestermodule.sPatientPreviousFirstName,
      sPatientPreviousLastName: this.$store.state.requestermodule.sPatientPreviousLastName,
      sPatientPreviousMiddleName: this.$store.state.requestermodule.sPatientPreviousMiddleName,
      disclaimer02:"",
      bPatientNameChanged: this.$store.state.requestermodule.bPatientNameChanged,

      //Show and Hide Fields Values fetch from store
      MROPatientMiddleName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientMiddleName,
      MROPatientPreviousMiddleName: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientPreviousMiddleName,
      MROPatientNameChanged: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MROPatientNameChanged
    };
  },
  //Requester name validations
  mixins: [validationMixin],
  validations: {
    sPatientFirstName: { required, maxLength: maxLength(30) },
    sPatientLastName: { required, maxLength: maxLength(30) },
    sPatientPreviousFirstName: { required, maxLength: maxLength(30) },
    sPatientPreviousLastName: { required, maxLength: maxLength(30) }
    // sPatientMiddleName: { maxLength: maxLength(1) }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    disclaimer() {
      return this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_04_disclaimer01;
    },
    //Requester name validations error message setter
    sPatientFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientFirstName.$dirty) return errors;
      !this.$v.sPatientFirstName.maxLength &&
        errors.push("First Name must be at most 30 characters long");
      !this.$v.sPatientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPatientLastNameError() {
      const errors = [];
      if (!this.$v.sPatientLastName.$dirty) return errors;
      !this.$v.sPatientLastName.maxLength &&
        errors.push("Last Name must be at most 30 characters long");
      !this.$v.sPatientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    sPatientPreviousFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientPreviousFirstName.$dirty) return errors;
      !this.$v.sPatientPreviousFirstName.maxLength &&
        errors.push("First Name must be at most 30 characters long");
      !this.$v.sPatientPreviousFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPatientPreviousLastNameError() {
      const errors = [];
      if (!this.$v.sPatientPreviousLastName.$dirty) return errors;
      !this.$v.sPatientPreviousLastName.maxLength &&
        errors.push("Last Name must be at most 30 characters long");
      !this.$v.sPatientPreviousLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    }
  },
  methods: {
    checked() {
      if (!this.bPatientNameChanged) {
        this.sPatientPreviousFirstName = "";
        this.sPatientPreviousLastName = "";
        this.sPatientPreviousMiddleName = "";
      }
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/sPatientFirstName",
        this.sPatientFirstName
      );
      this.$store.commit(
        "requestermodule/sPatientLastName",
        this.sPatientLastName
      );
      this.$store.commit(
        "requestermodule/sPatientMiddleName",
        this.sPatientMiddleName
      );
      this.$store.commit(
        "requestermodule/sPatientPreviousFirstName",
        this.sPatientPreviousFirstName
      );
      this.$store.commit(
        "requestermodule/sPatientPreviousLastName",
        this.sPatientPreviousLastName
      );
      this.$store.commit(
        "requestermodule/sPatientPreviousMiddleName",
        this.sPatientPreviousMiddleName
      );
      this.$store.commit(
        "requestermodule/bPatientNameChanged",
        this.bPatientNameChanged
      );

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');
        
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  mounted(){
    this.disclaimer02 = this.disclaimer.replace("your", "patient's");
  }
};
</script>
<style scoped>
</style>
