<template>
  <div class="center">
    <h1>
      How would you like us
      <br />to send you record(s)?
    </h1>

    <template>
      <v-layout v-for="shipmentType in oShipmentTypeArray" :key="shipmentType.sNormalizedShipmentTypeName" row wrap>
        <v-col cols="12" offset-sm="3" sm="6">
          <v-checkbox
            dark
            class="checkboxBorder"
            :label="shipmentType.sShipmentTypeName"
            color="green"
            :value="shipmentType.sNormalizedShipmentTypeName"
            v-model="sSelectedShipmentTypes"
            @change="check(shipmentType.sNormalizedShipmentTypeName)"
          >
          <v-tooltip v-if="shipmentType.sFieldToolTip" slot="append" top>
            <template v-slot:activator="{ on }">
              <v-icon v-on="on" color='grey' top>
                  mdi-information
              </v-icon>
            </template>
            <v-col cols="12" sm="12">
             <p style="width:200px">{{shipmentType.sFieldToolTip}}</p>
             </v-col>
          </v-tooltip>
          </v-checkbox>
          <slot :name="shipmentType.sNormalizedShipmentTypeName"></slot>
        </v-col>
      </v-layout>
      <!-- Slots Starts -->
      <template >
        <v-col v-if="sSelectedShipmentTypes[0]=='MROFax'" slot="MROFax" cols="12" offset-sm="2" sm="8">
              <v-textarea v-model="sSTFaxCompAdd" counter label="Complete Address"></v-textarea>
        </v-col>
      </template>

      <template >
        <v-col v-if="sSelectedShipmentTypes[0]=='MROSecureEmail'" slot="MROEmail" cols="12" offset-sm="3" sm="6">
            <label for="sSTEmailId" class="control-label">Email ID</label>
            <v-text-field
              v-model="sSTEmailId"
              :error-messages="emailErrors"
              required
              @input="$v.sSTEmailId.$touch()"
              @blur="$v.sSTEmailId.$touch()"
            ></v-text-field>
            <label for="sSTConfirmEmailId" class="control-label">Confirm Email ID</label>
            <v-text-field
              @paste.prevent
              v-model="sSTConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              @input="$v.sSTConfirmEmailId.$touch()"
              @blur="$v.sSTConfirmEmailId.$touch()"
            ></v-text-field> 
        </v-col>

      </template>
      <!-- Slots End -->
    </template>
    <!-- MROPatientPortal
MROSecureEmail
MROEmailShipment
MROIn-Person
MROFax -->
    <div>
      <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, email, sameAs } from "vuelidate/lib/validators";
export default {
  
  name: "WizardPage_12",
  data() {
    return {
      //this.$store.state.ConfigModule.apiResponseDataByLocation.oPrimaryReason
      oShipmentTypeArray: this.$store.state.ConfigModule.apiResponseDataByLocation1.oShipmentTypes,
      sSelectedShipmentTypes: [],
      sSTFaxCompAdd:'',
      sSTEmailId: this.$store.state.requestermodule.sPatientEmailId,
      sSTConfirmEmailId: this.$store.state.requestermodule.sConfirmEmailId,

      //other: false
    };
  },
  mixins: [validationMixin],
  validations: {
    sSTEmailId: {
      required,
      email
    },
    sSTConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs('sSTEmailId')
    }
  },
  computed: {
    emailErrors() {
      const errors = [];
      if (!this.$v.sSTEmailId.$dirty) return errors;
      !this.$v.sSTEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sSTEmailId.required && errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.sSTConfirmEmailId.$dirty) return errors;
      !this.$v.sSTConfirmEmailId.sameAsemailID && errors.push("Please enter correct e-mail");
      !this.$v.sSTConfirmEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sSTConfirmEmailId.required && errors.push("E-mail is required");
      return errors;
    }
  },
  methods: {
    nextPage() {
        switch(this.sSelectedShipmentTypes[0]) {
        case 'MROFax':
            this.$store.commit("requestermodule/sSTFaxCompAdd", this.sSTFaxCompAdd);
            console.log(this.sSTFaxCompAdd);
            break;
        case 'MROSecureEmail':
            this.$store.commit("requestermodule/sSTEmailId", this.sSTEmailId);
            this.$store.commit("requestermodule/sSTConfirmEmailId", this.sSTConfirmEmailId);  
            console.log(this.dAuthExpire);
            break;
        default:
            console.log(this.dAuthExpire);
        }        
        this.$store.state.ConfigModule.showBackBtn = true;  
        this.$store.commit("ConfigModule/mutateNextIndex");
    },
    check(id) {
        this.sSelectedShipmentTypes = [];
        this.sSelectedShipmentTypes.push(id);
        console.log(this.sSelectedShipmentTypes[0]);
    }
    // ,
    // check(prName) {
    //   if (prName == "Other Reason") {
    //     this.other= !this.other;       
    //   }
    // }
  }
};
</script>

<style scoped>

</style>