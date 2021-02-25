<template>
  <div class="center">
      <h1>Please provide your email address to receive a confirmation of your request.</h1>
      <v-row>
        <v-col  cols="12" offset-sm="2" sm="8">
            <v-text-field
              label="EMAIL"
              v-model="emailValid.sRequesterEmailId"
              :error-messages="emailErrors"
              required
              maxlength="70"
              :disabled="inputDisabled"
              @input="sRequesterEmailIdToLower"
              @blur="$v.emailValid.sRequesterEmailId.$touch()"
            ></v-text-field>
            <v-text-field
            label="CONFIRM EMAIL"
              @paste.prevent
              v-model="emailValid.sConfirmEmailId"
              :error-messages="confirmEmailErrors"
              required
              maxlength="70"
              :disabled="inputDisabled"
              @input="sConfirmEmailIdToLower"
              @blur="$v.emailValid.sConfirmEmailId.$touch()"
            ></v-text-field>
        </v-col>
        <v-col  cols="12" sm="12">
            <div v-if="disclaimer01!=''" class="disclaimer">{{disclaimer01}}</div>
            <div>
            <v-checkbox
              hide-details
              id="checkbox"
              v-model="bConfirmReport"
              color="white"
              :label="disclaimer02"
            ></v-checkbox>
            <div v-if="verified==false">
            <v-btn
              v-if="!showVerifyBlock || !(bRequestorEmailVerify || bReturnedForCompliance)"
              :disabled="$v.emailValid.$invalid"
              class="mr-4 next"
              @click.prevent="nextPage"
            >Next</v-btn>
            </div>
            </div>
          </v-col>
        <v-col  cols="12" offset-sm="2" sm="8">
          <form>
            <!-- if please email copy checkbox is check then below fields are visible -->
            <div v-show="(showVerifyBlock && bRequestorEmailVerify) || (showVerifyBlock && bReturnedForCompliance)">
              <p v-if="emailSent==false">We would like to verify your email address. Please enter the code sent to the email address you provided.</p>
              <p v-if="emailSent==false">Click on "Send Email" for email verification.</p>
               <v-alert
                    v-if="otpSentAlert"
                    dense
                    text
                    type="success"
                  >
                   Sending Email
                  </v-alert>
              <v-btn
                v-if="emailSent==false"
                @click="sendEmail"
                :disabled="$v.emailValid.$invalid || emailSent==true"
                class="next"
              >Send Email Verification</v-btn>
              <v-btn
                v-if="emailSent==false"
                @click.prevent="nextPage"
                :disabled="$v.emailValid.$invalid"
                style="margin-left:10px"
                class="next"
              >Skip</v-btn>
              <div v-if="showVerifyInput==true">
                <v-col cols="12" offset-sm="3" sm="6">
                <v-text-field
                  ref="otp"
                  :error-messages="sVerifyError"
                  @input="$v.sVerify.$touch()"
                  @blur="$v.sVerify.$touch()"
                  v-model="sVerify"
                  label="Enter Code"
                  required
                ></v-text-field>
                </v-col>
                <v-col cols="12" sm="12">
                <v-btn @click.prevent="sendEmail" :disabled="$v.emailValid.$invalid" class="next">Resend Code</v-btn>

                <v-btn
                  @click.prevent="verifyCode"
                  :disabled="$v.sVerify.$invalid || $v.emailValid.$invalid"
                  style="margin-left:10px"
                  class="next"
                >Verify</v-btn>                
                </v-col>
                <v-btn
                  @click.prevent="nextPage"
                  :disabled="$v.emailValid.$invalid"
                  style="margin-left:10px"
                  class="next"
                >Skip</v-btn>
                <!-- <v-col cols="12" offset-sm="3" sm="6">
                  <v-btn @click.prevent="nextPage" :disabled="$v.emailValid.$invalid" class="next">Skip</v-btn>
                </v-col> -->
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
</template>
<script>
import { mapState } from 'vuex';
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
      showVerifyBlock: true,
      showSuccessBlock: false,
      inputDisabled: false,
      emailSent:false,
      otpSentAlert:false,
      bReturnedForCompliance:false,
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
  activated(){
    this.bReturnedForCompliance=this.$store.state.ConfigModule.bReturnedForCompliance;
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
      !this.$v.sVerify.maxLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.minLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.required && errors.push("Verification Code required");
      return errors;
    },    
    ...mapState({
      facilityForceCompliance: state => state.ConfigModule
        .bForceCompliance,
      disclaimer01: state => state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer01,
      disclaimer02: state => state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_06_disclaimer02,

      //Show and Hide Fields Values
      MROPEmailId: state => state.ConfigModule.apiResponseDataByLocation
        .oFields.MROPEmailId,
      MROConfirmReport: state => state.ConfigModule.apiResponseDataByLocation
        .oFields.MROConfirmReport,
      bRequestorEmailVerify: state => state.ConfigModule.apiResponseDataByFacilityGUID
        .facilityLogoandBackground[0].bRequestorEmailVerify,
    }),
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
        sPatientLastName: this.$store.state.requestermodule.sPatientLastName,
        bAreYouPatient: this.$store.state.requestermodule.bAreYouPatient,
        sRelativeFirstName: this.$store.state.requestermodule.sRelativeFirstName,
        sRelativeLastName: this.$store.state.requestermodule.sRelativeLastName,
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
        this.$refs.otp.focus();
      }, 5000)
    },
    //Check response opt and entered opt matched or not
    verifyCode() {
      if (this.sResponseKey == this.sVerify) {
        this.showSuccessBlock = true;
        this.verified = true;
        this.inputDisabled = true;
        this.showVerifyBlock = false;
        if(this.facilityForceCompliance)
        {
          this.$store.commit("requestermodule/bForceCompliance", true);
        }
        // this.nextPage();
      } else {
        alert("Invalid Key");
      }
    },
    // checked() {
    //   this.showVerifyBlock = !this.showVerifyBlock;
    // },
    sRequesterEmailIdToLower(val) {
      this.emailValid.sRequesterEmailId = val.toLowerCase()
    },
    sConfirmEmailIdToLower(val) {
      this.emailValid.sConfirmEmailId = val.toLowerCase()
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/sRequesterEmailId",
        this.emailValid.sRequesterEmailId
      );
      this.$store.commit("requestermodule/bConfirmReport", this.bConfirmReport);
      this.$store.commit("requestermodule/bEmailVerified", this.verified);

      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');
      if(this.bReturnedForCompliance && this.verified)
      {
        var index,wizard='Wizard_23';
        this.$store.commit("ConfigModule/bReturnedForCompliance",false);
        index = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.oWizards.indexOf(wizard);
        this.$store.commit("ConfigModule/mutatewizardArrayIndex",index);
        this.$store.commit("ConfigModule/mutateselectedWizard",wizard);
      }
      else{
        this.$store.commit("ConfigModule/mutateNextIndex");
      }
    }
  }
};
</script>
<style scoped>
#checkbox label{
  font-size: 14px;
}
</style>