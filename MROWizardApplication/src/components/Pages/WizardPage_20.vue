<template>
  <div class="center">
    <div class="form-group">
      <h1>Let us send you a text to verify your phone number.</h1>
      <p v-if="disclaimer01!=''">{{disclaimer01}}</p>
      <form v-if="MRORequesterPhoneNumber">
        <v-row>
          <!-- Phone no input box -->
          <v-col cols="4" offset-sm="3" sm="2">
            <v-select :disabled="disableInput" id="phoneExt" v-model="selectedCountry" :items="countryCode"></v-select>
          </v-col>
          <!-- <v-col cols="1" style="margin-top:20px;" offset-sm="3" sm="1">
            <label class="input-group-addon">+91</label>
          </v-col>-->
          <v-col id="phoneNo" cols="7" sm="5">
            <v-text-field
              type="tel"
              maxlength="10"
              placeholder="(XXX) XXX-XXXX"
              v-model="sPhoneNo"
              label="ENTER MOBILE NO"
              required
              :disabled="disableInput"
              :error-messages="sPhoneNoError"
              @input="$v.sPhoneNo.$touch()"
              @blur="$v.sPhoneNo.$touch()"
            ></v-text-field>
          </v-col>
          <v-col cols="12" offset-sm="1" sm="10">
            <p v-if="disclaimer02!=''" class="disclaimer">{{disclaimer02}}</p>
          </v-col>
          <v-col cols="12" offset-sm="3" sm="6">
            <div v-show="showSendVerify">
              <v-btn
                @click.prevent="submit"
                :disabled="$v.sPhoneNo.$invalid"
                class="ma-2 next"
              >Send Verification Code</v-btn>
            </div>            
            <!-- Below fields will shown only after requester click on "Send Verification Code" button -->
            <div v-show="bOtpSend">
              <div>
                <v-text-field
                  :error-messages="sVerifyError"
                  @input="$v.sVerify.$touch()"
                  @blur="$v.sVerify.$touch()"
                  v-model="sVerify"
                  label="ENTER CODE"
                  required
                ></v-text-field>

                <v-btn @click.prevent="submit" :disabled="$v.sPhoneNo.$invalid" class="next">Resend Code</v-btn>

                <v-btn
                  @click.prevent="verifyCode"
                  :disabled="$v.sVerify.$invalid || $v.sPhoneNo.$invalid"
                  style="margin-left:10px"
                  class="next"
                >Verify</v-btn>
              </div>
            </div>
            <v-btn v-if="showSuccessBlock==false" @click.prevent="skipPage" :disabled="$v.sPhoneNo.$invalid" class="next">Skip</v-btn>
            <div v-if="showSuccessBlock">
              <p class="disclaimer">Mobile Verification Successful.</p>
              <v-btn class="mr-4 next" @click.prevent="nextPage">Next</v-btn>
            </div>
          </v-col>
        </v-row>
      </form>
    </div>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, maxLength, minLength } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_18",
  data() {
    return {
      isDisable: false,
      bOtpSend: false,
      showSuccessBlock: false,
      disableInput: false,
      showSendVerify: true,
      countryCode: ["+1", "+91"],
      selectedCountry: "+1",
      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_20_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_20_disclaimer02,

      MRORequesterPhoneNumber: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORequesterPhoneNumber,

      sPhoneNo: "",
      sApp_Key: process.env.VUE_APP_RINGCAPTCHA_APP,
      sApi_Key: process.env.VUE_APP_RINGCAPTCHA_API,
      sVerify: "",
      service: "",
      subData: {}
    };
  },
  // OTP and phono validations
  mixins: [validationMixin],
  validations: {
    sVerify: { required, maxLength: maxLength(4), minLength: minLength(4) },
    sPhoneNo: { required, maxLength: maxLength(10), minLength: minLength(10) }
  },
  created() {
    this.$vuetify.theme.dark = true;
  },
  computed: {
    //OTP and Phone no Validation message setter
    sPhoneNoError() {
      const errors = [];
      if (!this.$v.sPhoneNo.$dirty) return errors;
      !this.$v.sPhoneNo.maxLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.sPhoneNo.minLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.sPhoneNo.required && errors.push("Mobile No Required");
      return errors;
    },
    sVerifyError() {
      const errors = [];
      if (!this.$v.sVerify.$dirty) return errors;
      !this.$v.sVerify.maxLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.minLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.required && errors.push("Verification code required");
      return errors;
    }
  },
  methods: {
    //send otp on send verification no and resend button
    submit() {
      this.isDisable = true;
      this.bOtpSend = true;
      this.showSendVerify = false;
      var obj = {};
      obj["phone"] = this.selectedCountry + this.sPhoneNo;
      obj["api_key"] = this.sApi_Key;
      var formData = new FormData();
      formData.append("phone", this.selectedCountry + this.sPhoneNo);
      formData.append("api_key", this.sApi_Key);
      var url = "https://api.ringcaptcha.com/" + this.sApp_Key + "/code/SMS";

      var self = this;
      this.$http
        .post(url, formData, {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
          }
        })
        .then(response => {
          self.subData = response;
          // console.log(response.body);
        });
    },
    //Verify OPT entered by requester
    verifyCode() {
      var obj = {};
      obj["phone"] = this.selectedCountry + this.sPhoneNo;
      obj["code"] = this.sVerify;
      var formData = new FormData();
      formData.append("phone", this.selectedCountry + this.sPhoneNo);
      formData.append("code", this.sVerify);
      formData.append("api_key", this.sApi_Key);
      var url = "https://api.ringcaptcha.com/" + this.sApp_Key + "/verify";

      this.$http
        .post(url, formData, {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
          }
        })
        .then(response => {
          if (response.data.status == "SUCCESS") {
            this.bOtpSend = false;
            this.showSuccessBlock = true;
            this.disableInput = true;
          }
          if (response.data.status == "ERROR") {
            alert("Invalid verification code");
          }
        });
    },
    nextPage() {
      this.$store.commit("requestermodule/sPhoneNo", this.sPhoneNo);
      this.$store.commit(
        "requestermodule/bPhoneNoVerified",
        this.showSuccessBlock
      );

      //Partial Requester Data Save Start
      this.$store.commit(
        "requestermodule/sWizardName",
        this.$store.state.ConfigModule.selectedWizard
      );
      if (
        this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
          .wizardsSave[this.$store.state.ConfigModule.selectedWizard] == 1
      ) {
        this.$http
          .post("requesters/AddRequester/", this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
      }
      //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    skipPage() {
      this.nextPage();
    }
  }
};
</script>
