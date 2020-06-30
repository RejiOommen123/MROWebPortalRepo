<template>
  <div class="center">
    <h1 v-if="bAreYouPatient">What's your Email Address?</h1>
    <h1 v-else>What is patient's Email Address?</h1>
    <p>We'll email you a confirmation of your request when you're finished</p>
    <div>
      <v-row>
        <v-col v-if="MROPEmailId" cols="12" offset-sm="3" sm="6">
          <form>
            <label for="sPatientEmailId" class="control-label">Email</label>
            <v-text-field
              v-model="sPatientEmailId"
              :error-messages="emailErrors"
              required
              @input="$v.sPatientEmailId.$touch()"
              @blur="$v.sPatientEmailId.$touch()"
            ></v-text-field>
            <label for="sConfirmEmailId" class="control-label">Confirm Email</label>
            <v-text-field
              @paste.prevent
              v-model="sConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              @input="$v.sConfirmEmailId.$touch()"
              @blur="$v.sConfirmEmailId.$touch()"
            ></v-text-field>
            <v-checkbox
              v-if="MROConfirmReport"
              v-model="bConfirmReport"
              color="#53b958"
              label="Please email me a copy of my completed request form"
            ></v-checkbox>
            <v-btn
              v-if="showVerifyBlock!=true && bConfirmReport!=true"
              class="mr-4"
              @click.prevent="nextPage"
              
              color="success"
            >Next</v-btn>
          </form>
          <form>
            <div v-if="bConfirmReport==true">
              <p>Click on "Send Email" for email verification.</p>
              <v-btn @click="sendEmail" :disabled="this.isDisable" color="success">Send Email Verification</v-btn>
            </div>

            <div v-if="showVerifyBlock==true">
              <v-text-field
                :error-messages="sVerifyError"
                @input="$v.sVerify.$touch()"
                @blur="$v.sVerify.$touch()"
                v-model="sVerify"
                label="Enter OTP"
                required
              ></v-text-field>

              <v-btn @click.prevent="sendEmail" color="success">Resend OTP</v-btn>

              <v-btn
                @click.prevent="verifyCode"
                :disabled="$v.$invalid"
                style="margin-left:10px"
                color="success"
              >Verify</v-btn>
            </div>
          </form>
        </v-col>
      </v-row>
    </div>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import {
  required,
  email,
  sameAs,
  maxLength,
  minLength
} from "vuelidate/lib/validators";
export default {
  name: "WizardPage_06",
  data() {
    return {
      sPatientEmailId: this.$store.state.requestermodule.sPatientEmailId,
      sConfirmEmailId: this.$store.state.requestermodule.sConfirmEmailId,
      bConfirmReport: this.$store.state.requestermodule.bConfirmReport,
      showVerifyBlock: false,
      sVerify: "",
      sResponseKey: "",
      isDisable: false,
      verified:false,

      //Show and Hide Fields Values
      MROPEmailId: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROPEmailId,
      MROConfirmReport: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROConfirmReport
    };
  },
  mixins: [validationMixin],
  validations: {
    sPatientEmailId: {
      required,
      email
    },
    sConfirmEmailId: {
      required,
      email,
      sameAsemailID: sameAs("sPatientEmailId")
    },
    sVerify: { required, maxLength: maxLength(4), minLength: minLength(4) }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    emailErrors() {
      const errors = [];
      if (!this.$v.sPatientEmailId.$dirty) return errors;
      !this.$v.sPatientEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sPatientEmailId.required && errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.sConfirmEmailId.$dirty) return errors;
      !this.$v.sConfirmEmailId.sameAsemailID &&
        errors.push("Please enter correct e-mail");
      !this.$v.sConfirmEmailId.email && errors.push("Must be valid e-mail");
      !this.$v.sConfirmEmailId.required && errors.push("E-mail is required");
      return errors;
    },
    sVerifyError() {
      const errors = [];
      if (!this.$v.sVerify.$dirty) return errors;
      !this.$v.sVerify.maxLength && errors.push("Enter 4 digit OTP");
      !this.$v.sVerify.minLength && errors.push("Enter 4 digit OTP");
      !this.$v.sVerify.required && errors.push("OTP required");
      return errors;
    }
  },
  methods: {
    sendEmail() {
      this.$store.commit(
        "requestermodule/sPatientEmailId",
        this.sPatientEmailId
      );
      this.$store.commit(
        "requestermodule/sConfirmEmailId",
        this.sConfirmEmailId
      );
      this.isDisable = true;
      this.showVerifyBlock = true;
      var emailConfirm={
        nFacilityID :this.$store.state.requestermodule.nFacilityID,
        sPatientEmailId :this.$store.state.requestermodule.sPatientEmailId,
        sPatientFirstName :this.$store.state.requestermodule.sPatientFirstName,
        sPatientLastName :this.$store.state.requestermodule.sPatientLastName,
      }
      this.$http
        .post("Wizards/VerfiyRequestorEmail/", emailConfirm)
        .then(response => {
          if (response.body) {
            this.sResponseKey = response.body;
          }
        });
    },
    verifyCode() {
      if (this.sResponseKey == this.sVerify) {
        this.nextPage();
      } else {
        alert("Invalid Key");
      }
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/sPatientEmailId",
        this.sPatientEmailId
      );
      this.$store.commit(
        "requestermodule/sConfirmEmailId",
        this.sConfirmEmailId
      );
      this.$store.commit("requestermodule/bConfirmReport", this.bConfirmReport);
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