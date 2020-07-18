<template>
  <div class="center">
    <h1>What is your email address?</h1>
    <p v-if="disclaimer01!=''">{{disclaimer01}}</p>
    <div>
      <v-row>
        <v-col  cols="12" offset-sm="2" sm="8">
          <form>
            <v-text-field
              label="EMAIL"
              v-model="emailValid.sRequesterEmailId"
              :error-messages="emailErrors"
              required
              :disabled="inputDisabled"
              @input="$v.emailValid.sRequesterEmailId.$touch()"
              @blur="$v.emailValid.sRequesterEmailId.$touch()"
            ></v-text-field>
            <v-text-field
            label="CONFIRM EMAIL"
              @paste.prevent
              v-model="emailValid.sConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              :disabled="inputDisabled"
              @input="$v.emailValid.sConfirmEmailId.$touch()"
              @blur="$v.emailValid.sConfirmEmailId.$touch()"
            ></v-text-field>
            <div v-if="bRequestorEmailConfirm==false">
               <v-btn
              :disabled="$v.emailValid.$invalid"
              class="mr-4 next"
              @click.prevent="nextPage"
              >Next</v-btn>
            </div>
            <div v-if="bRequestorEmailConfirm==true">
            <v-checkbox
              id="checkbox"
              v-model="bConfirmReport"
              :disabled="inputDisabled"
              @change="checked()"
              color="#e84700"
              :label="disclaimer02"
            ></v-checkbox>
            <div v-if="verified==false">
            <v-btn
              v-if="!showVerifyBlock || !bRequestorEmailVerify"
              :disabled="$v.emailValid.$invalid"
              class="mr-4 next"
              @click.prevent="nextPage"
            >Next</v-btn>
            </div>
            </div>
          </form>
          <form>
            <!-- if please email copy checkbox is check then below fields are visible -->
            <div v-if="showVerifyBlock && bRequestorEmailVerify">
              <p>Click on "Send Email" for email verification.</p>
               <v-alert
                    v-if="otpSentAlert"
                    dense
                    text
                    type="success"
                  >
                   Sending Email
                  </v-alert>
              <v-btn
                @click="sendEmail"
                :disabled="$v.emailValid.$invalid || emailSent==true"
                class="next"
              >Send Email Verification</v-btn>
              <div v-if="showVerifyInput==true">
                <v-col cols="12" offset-sm="3" sm="6">
                <v-text-field
                  :error-messages="sVerifyError"
                  @input="$v.sVerify.$touch()"
                  @blur="$v.sVerify.$touch()"
                  v-model="sVerify"
                  label="Enter OTP"
                  required
                ></v-text-field>
                </v-col>

                <v-btn @click.prevent="sendEmail" class="next">Resend OTP</v-btn>

                <v-btn
                  @click.prevent="verifyCode"
                  :disabled="$v.sVerify.$invalid"
                  style="margin-left:10px"
                  class="next"
                >Verify</v-btn>
              </div>
            </div>
            <div v-if="showSuccessBlock">
              <p class="disclaimer">Email Verification Successful.</p>
              <v-btn class="mr-4 next" @click.prevent="nextPage">Next</v-btn>
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
      emailValid: {
        sRequesterEmailId: this.$store.state.requestermodule.sRequesterEmailId,
        sConfirmEmailId: ""
      },
      bConfirmReport: this.$store.state.requestermodule.bConfirmReport,
      showVerifyInput: false,
      sVerify: "",
      sResponseKey: "",
      isDisable: false,
      verified: false,
      showVerifyBlock: false,
      showSuccessBlock: false,
      inputDisabled: false,
      emailSent:false,
      otpSentAlert:false,

      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer02,

      //Show and Hide Fields Values
      MROPEmailId: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROPEmailId,
      MROConfirmReport: this.$store.state.ConfigModule.apiResponseDataByLocation
        .oFields.MROConfirmReport,
      bRequestorEmailConfirm:this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .facilityLogoandBackground[0].bRequestorEmailConfirm,
      bRequestorEmailVerify:this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .facilityLogoandBackground[0].bRequestorEmailVerify,
    };
  },
  //Email on verify OTP validations
  mixins: [validationMixin],
  validations: {
    emailValid: {
      sRequesterEmailId: {
        required,
        email
      },
      sConfirmEmailId: {
        required,
        email,
        sameAsemailID: sameAs("sRequesterEmailId")
      }
    },
    sVerify: { required, maxLength: maxLength(4), minLength: minLength(4) }
  },
  computed: {
    // Email and verify OTP validations error message setter
    emailErrors() {
      const errors = [];
      if (!this.$v.emailValid.sRequesterEmailId.$dirty) return errors;
      !this.$v.emailValid.sRequesterEmailId.email &&
        errors.push("Must be valid e-mail");
      !this.$v.emailValid.sRequesterEmailId.required &&
        errors.push("E-mail is required");
      return errors;
    },
    confirmEmailErrors() {
      const errors = [];
      if (!this.$v.emailValid.sConfirmEmailId.$dirty) return errors;
      !this.$v.emailValid.sConfirmEmailId.sameAsemailID &&
        errors.push("Please enter correct e-mail");
      !this.$v.emailValid.sConfirmEmailId.email &&
        errors.push("Must be valid e-mail");
      !this.$v.emailValid.sConfirmEmailId.required &&
        errors.push("E-mail is required");
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
        "requestermodule/sRequesterEmailId",
        this.sRequesterEmailId
      );
      this.otpSentAlert=true;
      this.emailSent=true;
      this.isDisable = true;
      this.showVerifyInput = true;
      var emailConfirm = {
        nFacilityID: this.$store.state.requestermodule.nFacilityID,
        sRequesterEmailId: this.emailValid.sRequesterEmailId,
        sPatientFirstName: this.$store.state.requestermodule.sPatientFirstName,
        sPatientLastName: this.$store.state.requestermodule.sPatientLastName
      };
      // api to send mail and get opt in response
      this.$http
        // TODO: check requestor/requester
        .post("Wizards/VerfiyRequestorEmail/", emailConfirm)
        .then(response => {
          if (response.body) {
            this.sResponseKey = response.body;
          }
        });
        setTimeout(() => {
        this.otpSentAlert=false;
      }, 5000)
    },
    //Check response opt and entered opt matched or not
    verifyCode() {
      if (this.sResponseKey == this.sVerify) {
        this.showSuccessBlock = true;
        this.verified = true;
        this.inputDisabled = true;
        this.showVerifyBlock = false;
        // this.nextPage();
      } else {
        alert("Invalid Key");
      }
    },
    checked() {
      this.showVerifyBlock = !this.showVerifyBlock;
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/sRequesterEmailId",
        this.emailValid.sRequesterEmailId
      );
      this.$store.commit("requestermodule/bConfirmReport", this.bConfirmReport);

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
  }
};
</script>
<style scoped>
#checkbox label{
  font-size: 14px;
}
</style>