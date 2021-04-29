<template>
  <div>
    <v-dialog v-model="bShow" light persistent content-class="sessionTransfer">
      <v-card>
        <v-card-title>
          <v-btn
            style="font-size: 18px; color: black"
            class="wizardClose"
            icon
            dark
            @click="closeShowSessionTransfer()"
          >
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <img
            :src="this.logoImg"
            alt="Vue JS"
            style="align: center; padding-top: 2%"
            id="logoImg"
          />
          <div class="center wordBreakNormal">
            <v-row>
              <v-col cols="12" sm="12">
                <b>
                  <p>
                    To complete this request on a different device, please enter
                 your phone number or email address below:
                  </p></b
                >
              </v-col>
            </v-row>
          </div>
        </v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" sm="12">
              <div>
                <v-row>
                  <v-col cols="11" sm="4">
                    <label
                      style="color: black; padding-top: 15%"
                      class="required"
                      for="sPhoneNo"
                      >Phone Number:</label
                    >
                  </v-col>
                  <v-col cols="3" sm="3">
                    <v-select
                      :disabled="disableInput"
                      id="phoneExt"
                      v-model="selectedCountry"
                      :items="countryCode"
                    ></v-select>
                  </v-col>
                  <v-col cols="7" sm="4">
                    <v-text-field
                     
                      type="tel"
                      maxlength="10"
                      placeholder="(XXX) XXX-XXXX"
                      v-model="sPhoneNo"
                      label="ENTER NUMBER"
                      required
                      :error-messages="sPhoneNoError"
                   
                      @input="PhoneInput()"
                     
                 
                      @blur="$v.sPhoneNo.$touch()"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="1" sm="1" style="padding-top: 5%">
                    <div>
                      <v-tooltip slot="append" left>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            :disabled="!bPhoneVerified"
                            v-on="on"
                            color="blue"
                            >mdi-checkbox-marked-circle</v-icon
                          >
                        </template>
                        <v-col cols="12" sm="12">
                          <p
                            style="width: 250px; background-color: transparent"
                          >
                            Mobile Number Verified Successfully
                          </p>
                        </v-col>
                      </v-tooltip>
                    </div>
                  </v-col>
                </v-row>

                <v-col cols="12" offset-sm="4" sm="6">
                  <div v-show="showSendVerify">
                    <v-btn
                      @click.prevent="submit"
                      :disabled="$v.sPhoneNo.$invalid || bPhoneVerified"
                      class="next"
                      >Send Code</v-btn
                    >
                  </div>
                </v-col>

                <div v-show="bOtpSend">
                  <v-row>
                    <v-col cols="8" offset-sm="4" sm="7">
                      
                      <v-text-field
                        :error-messages="sVerifyError"
                        @input="$v.sVerify.$touch()"
                        @blur="$v.sVerify.$touch()"
                        v-model="sVerify"
                        label="ENTER CODE"
                        required
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col offset-sm="4">
                      <v-btn
                        @click.prevent="submit"
                        :disabled="$v.sPhoneNo.$invalid || disableResend"
                        class="next"
                        >Resend Code</v-btn
                      >

                      <v-btn
                        @click.prevent="MobileVerifyCode"
                        :disabled="$v.sVerify.$invalid || $v.sPhoneNo.$invalid"
                        style="margin-left: 10px"
                        class="next"
                        >Verify</v-btn
                      >
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" offset-sm="4" sm="7">
                      <div v-if="timeStatement" id="hide">
                        Wait for {{ displayTime }} seconds to resend code.
                      </div>
                    </v-col>
                  </v-row>
                </div>
              </div>
              <div style="padding-top: 5%">
                <v-row>
                  <v-col cols="11" sm="4">
                    <label
                      style="color: black; padding-top: 15%"
                      class="required"
                      >Email Address:</label
                    >
                  </v-col>
                  <v-col cols="10" sm="7">
                    <v-text-field
                      label="ENTER EMAIL ADDRESS"
                      v-model="emailValid.sRequesterEmailId"
                      :error-messages="emailErrors"
                      required
                      maxlength="70"
                      @input="EmailInput()"
                      @blur="$v.emailValid.sRequesterEmailId.$touch()"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="1" sm="1" style="padding-top: 5%">
                    <div>
                      <v-tooltip slot="append" left>
                        <template v-slot:activator="{ on }">
                          <v-icon
                            :disabled="!bEmailVerified"
                            v-on="on"
                            color="blue"
                            >mdi-checkbox-marked-circle</v-icon
                          >
                        </template>
                        <v-col cols="12" sm="12">
                          <p
                            style="width: 250px; background-color: transparent"
                          >
                            Email Verified Successfully
                          </p>
                        </v-col>
                      </v-tooltip>
                    </div>
                  </v-col>
                </v-row>
                <v-col cols="12" offset-sm="4" sm="6">
                  <div v-show="emailSent">
                    <v-btn
                      @click.prevent="sendEmail"
                      :disabled="$v.emailValid.$invalid || bEmailVerified"
                      class="next"
                      >Send Code</v-btn
                    >
                  </div>
                </v-col>

                <div v-show="showVerifyInput">
                  <v-row>
                    <v-col cols="8" offset-sm="4" sm="7">
                      <v-text-field
                        ref="otp"
                        :error-messages="sVerifyEmailError"
                        @input="$v.sVerifyEmail.$touch()"
                        @blur="$v.sVerifyEmail.$touch()"
                        v-model="sVerifyEmail"
                        label="Enter Code"
                        required
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col offset-sm="4">
                      <v-btn
                        @click.prevent="sendEmail"
                        :disabled="$v.emailValid.$invalid || EmaildisableResend"
                        class="next"
                        >Resend Code</v-btn
                      >

                      <v-btn
                        @click.prevent="EmailVerifyCode"
                        :disabled="
                          $v.sVerifyEmail.$invalid || $v.emailValid.$invalid
                        "
                        style="margin-left: 10px"
                        class="next"
                        >Verify</v-btn
                      >
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" offset-sm="4" sm="7">
                      <div v-if="EmailtimeStatement" id="hide">
                        Wait for {{ EmaildisplayTime }} seconds to resend code.
                      </div>
                    </v-col>
                  </v-row>
                </div>
              </div>
              <div style="padding-top: 5%">
                <v-row>
                  <v-col cols="6" sm="4" offset-sm="2">
                    <a>
                      <v-btn block style="background-color: yellow" :disabled ="!bPhoneVerified && !bEmailVerified"
                       @click= SendLink() >Send Link
                      </v-btn></a
                    >
                  </v-col>
                  <v-col cols="6" sm="4">
                    <a>
                      <v-btn block style="background-color: yellow"
                       @click= GenerateQR() >Generate QR
                      </v-btn></a
                    >
                  </v-col>
                </v-row>
              </div>
              <v-col v-if="returnURL!=''" align="center" cols="8" offset="2">
              <vue-qrcode :value="returnURL" />
              <div v-if="timeStatement" id="hide">This session will expire in {{QRDisplayTime}} seconds.</div>
              </v-col>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
       <!-- Session Transfer success pop up -->
    <v-dialog v-model="successDialog" width="350px" persistent light max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text class="wordBreakNormal">Your current session is expired. A new session link should have been received on verified email / phone or by scanning QR code. Kindly click on the link to continue session on same request.</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="closeSession()">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
<script>
import { mapState } from "vuex";
import { validationMixin } from "vuelidate";
import {
  required,
  maxLength,
  minLength,
  email,
  numeric,
} from "vuelidate/lib/validators";
import VueQrcode from 'vue-qrcode'
export default {
  mixins: [validationMixin],
  validations: {
     sVerify: { required, maxLength: maxLength(4), minLength: minLength(4) },
     sVerifyEmail: { required, maxLength: maxLength(4), minLength: minLength(4)},
     sPhoneNo: { required, maxLength: maxLength(10), minLength: minLength(10), numeric },
     emailValid: {
       sRequesterEmailId: { required, email },
     },
  },
  components: {
    VueQrcode,
  },
  data: function () {
    return {
      emailValid: {
        sRequesterEmailId: this.$store.state.ConfigModule.SessionTransferForm.sEmail != '' ? this.$store.state.ConfigModule.SessionTransferForm.sEmail : this.$store.state.requestermodule.sRequesterEmailId,
      },   
      sPhoneNo: this.$store.state.ConfigModule.SessionTransferForm.sPhone != '' ? this.$store.state.ConfigModule.SessionTransferForm.sPhone : this.$store.state.requestermodule.sPhoneNo,
      bEmailVerified: this.$store.state.ConfigModule.SessionTransferForm.sEmail != '' ? this.$store.state.ConfigModule.SessionTransferForm.bEmailVerified : this.$store.state.requestermodule.bEmailVerified,
      logoImg: this.$store.state.ConfigModule.wizardLogo,
     // sPhoneNo: "",
      countryCode: ["+1", "+91"],
      selectedCountry: this.$store.state.ConfigModule.SessionTransferForm.sPhoneExt != '' ? this.$store.state.ConfigModule.SessionTransferForm.sPhoneExt : "+1",
      showSendVerify: true,
      bOtpSend: false,
      time: 60,
      disableResend: true,
      timeStatement: true,
      displayTime: "",
      bPhoneVerified: this.$store.state.ConfigModule.SessionTransferForm.sPhone != '' ? this.$store.state.ConfigModule.SessionTransferForm.bPhoneVerified : this.$store.state.requestermodule.bPhoneNoVerified,
      disableInput: false,
      sApp_Key: process.env.VUE_APP_RINGCAPTCHA_APP,
      sApi_Key: process.env.VUE_APP_RINGCAPTCHA_API,
      sVerify: "",
      subData: {},
      showVerifyInput: false,
      sVerifyEmail: "",
      sResponseKey: "",  
      emailSent: true,
      otpSentAlert: false,
      Emailtime: 60,
      EmaildisableResend: true,
      EmailtimeStatement: true,
      EmaildisplayTime: "",
      returnURL: "",
      QRTimeStatement: true,
      QRDisplayTime: "",
      QRtime:300,
      successDialog: false,
      bSessionTransferred: false
    };
  },
  watch: {
    logo(newlogo) {
      this.logoImg = newlogo;
    },
    background(newBackground) {
      this.backgroundImg = newBackground;
    },
  },
  computed: {
    logo() {
      return this.$store.state.ConfigModule.wizardLogo;
    },
    sPhoneNoError() {
      const errors = [];
      if (!this.$v.sPhoneNo.$dirty) return errors;
      !this.$v.sPhoneNo.maxLength && errors.push("Enter 10 digit phone no");
      !this.$v.sPhoneNo.minLength && errors.push("Enter 10 digit phone no");
      !this.$v.sPhoneNo.numeric && errors.push("Must be Numeric");
      !this.$v.sPhoneNo.required && errors.push("Phone number is required");
      return errors;
      
    },
   

    sVerifyError() {
      const errors = [];
      if (!this.$v.sVerify.$dirty) return errors;
      !this.$v.sVerify.maxLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.minLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerify.required && errors.push("Verification code required");
      return errors;
    },

    emailErrors() {
      const errors = [];
      if (!this.$v.emailValid.sRequesterEmailId.$dirty) return errors;
      !this.$v.emailValid.sRequesterEmailId.email &&
        errors.push("Must be valid e-mail");
      !this.$v.emailValid.sRequesterEmailId.required &&
        errors.push("E-mail is required");
      return errors;
    },

    sVerifyEmailError() {
      const errors = [];
      if (!this.$v.sVerifyEmail.$dirty) return errors;
      !this.$v.sVerifyEmail.maxLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerifyEmail.minLength && errors.push("Enter 4 digit Code");
      !this.$v.sVerifyEmail.required &&
        errors.push("Verification Code required");
      return errors;
    },

    bShow() {
      return this.$store.state.ConfigModule.bShowSessionTransfer;
    },
    ...mapState({
      // Show and Hide Fields Values
      // MRORequesterPhoneNumber: (state) =>
      //   state.ConfigModule.apiResponseDataByLocation.oFields
      //     .MRORequesterPhoneNumber,

      // MROPEmailId: (state) =>
      //   state.ConfigModule.apiResponseDataByLocation.oFields.MROPEmailId,

      // bRequestorEmailVerify: (state) =>
      //   state.ConfigModule.apiResponseDataByFacilityGUID
      //     .facilityLogoandBackground[0].bRequestorEmailVerify,

        //   StatePhoneNo: (state) => 
        //   state.requestermodule.sPhoneNo,

        //   StateVerifiedPhoneNo: (state) =>
        //   state.requestermodule.bPhoneNoVerified,
         
        //  StateEmailId: (state) =>
        //  state.requestermodule.sRequesterEmailId,

        //  StateEmailIdVerified: (state) =>
        //  state.requestermodule.bEmailVerified,

    }),
  },
  methods: {
  PhoneInput(){ 
   this.$v.sPhoneNo.$touch();
   this.bPhoneVerified = false;
  },

  EmailInput(){
     this.sRequesterEmailIdToLower;
     this.bEmailVerified = false;
  },

    closeShowSessionTransfer() {
      this.SetSessionTransferForm();     
      if(this.bSessionTransferred){
        this.successDialog = true;
      }
      else{
        this.$store.commit("ConfigModule/bShowSessionTransfer", false);
      }       
    },
    //Verify OPT entered by requester

    submit() {
      this.bPhoneVerified = false;
      this.disableResend = true;
      this.timeStatement = false;
      
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
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
          },
        })
        .then((response) => {
          if (response.body) {
            self.subData = response;
            this.time = response.body.retry_in;
            if (response.body.status == "SUCCESS") {
              let timerId = setInterval(() => {
                this.timeStatement = true;
                this.time -= 1;
                var m = Math.floor((this.time % 3600) / 60);
                var s = Math.floor((this.time % 3600) % 60);
                this.displayTime =
                  ("0" + m).slice(-2) + ":" + ("0" + s).slice(-2);
                if (this.time == 0) {
                  clearInterval(timerId);
                  this.timeStatement = false;
                  this.disableResend = false;
                }
              }, 1000);
            } else {
              this.timeStatement = false;
              this.disableResend = false;
              if (response.body.message == "ERROR_INVALID_NUMBER") {
                alert("Invalid Mobile Number");
              }
            }
          }
        });
    },

    //Verify OPT entered by requester
    MobileVerifyCode() {
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
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
          },
        })
        .then((response) => {
          if (response.data.status == "SUCCESS") {
            this.bPhoneVerified = true;
            this.time = 1;
            this.bOtpSend = false;
            this.showSendVerify = true;
            this.sVerify = null;
          }
          if (response.data.status == "ERROR") {
            alert("Invalid verification code");
          }
        });
    },

    sendEmail() {
      this.EmaildisableResend = true;
      this.bEmailVerified = false;
      this.emailSent = false;
      this.Emailtime = 60;
      let timerId = setInterval(() => {
        this.EmailtimeStatement = true;
        this.Emailtime -= 1;
        var m = Math.floor((this.Emailtime % 3600) / 60);
        var s = Math.floor((this.Emailtime % 3600) % 60);
        this.EmaildisplayTime = ("0" + m).slice(-2) + ":" + ("0" + s).slice(-2);
        if (this.Emailtime == 0) {
          clearInterval(timerId);
          this.EmailtimeStatement = false;
          this.EmaildisableResend = false;
        }
      }, 1000);
      this.otpSentAlert = true;
      
     
      this.showVerifyInput = true;
      var emailConfirm = {
        nRequesterId : this.$store.state.requestermodule.nRequesterID,
        nFacilityId : this.$store.state.requestermodule.nFacilityID,
        nLocationId : this.$store.state.requestermodule.nLocationID,
        sEmailId: this.emailValid.sRequesterEmailId,
        sFirstName: this.$store.state.requestermodule.bAreYouPatient ? this.$store.state.requestermodule.sPatientFirstName : this.$store.state.requestermodule.sRelativeFirstName,
        sLastName: this.$store.state.requestermodule.bAreYouPatient ? this.$store.state.requestermodule.sPatientLastName : this.$store.state.requestermodule.sRelativeLastName,
      };
      // api to send mail and get opt in response
      this.$http
        // TODO: check requestor/requester
        .post("Wizards/VerfiyRequestorEmail/", emailConfirm)
        .then((response) => {
          if (response.body) {
            this.sResponseKey = response.body;
          }
        });
      setTimeout(() => {
        this.otpSentAlert = false;
        this.$refs.otp.focus();
      }, 5000);
    },

    //Check response opt and entered opt matched or not
    EmailVerifyCode() {
      if (this.sResponseKey == this.sVerifyEmail) {
        this.bEmailVerified = true; 
        this.Emailtime = 1;  
        this.emailSent = true; 
        this.showVerifyInput = false;
        this.sVerifyEmail = null;
      } else {
        alert("Invalid verification code");
      }
    },
   
    sRequesterEmailIdToLower(val) {
      this.emailValid.sRequesterEmailId = val.toLowerCase();
    },

      GenerateSTData(){        
        var LoaderDialog = {
          visible : true,
          title : 'Please stand by'
        };
        this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
      var CombineState = {
        requesterModule : this.$store.state.requestermodule,
        configModule : this.$store.state.ConfigModule
      };
      var SessionTransfer = {
        CompleteState : JSON.stringify(CombineState),
        nRequesterId : this.$store.state.requestermodule.nRequesterID,
        nFacilityId : this.$store.state.requestermodule.nFacilityID,
        nLocationId : this.$store.state.requestermodule.nLocationID,
        sEmailId: this.emailValid.sRequesterEmailId,
        sFirstName: this.$store.state.requestermodule.bAreYouPatient ? this.$store.state.requestermodule.sPatientFirstName : this.$store.state.requestermodule.sRelativeFirstName,
        sLastName: this.$store.state.requestermodule.bAreYouPatient ? this.$store.state.requestermodule.sPatientLastName : this.$store.state.requestermodule.sRelativeLastName,
        bSendEmail: this.bEmailVerified,
        sLocationName: this.$store.state.requestermodule.sSelectedLocationName
      };
      return SessionTransfer;
      },
      SendLink(){
        this.$store.commit("requestermodule/bSessionTransferred", true);
        this.bSessionTransferred = true;
        this.$store.dispatch('requestermodule/partialAddReq',true);
        var LoaderDialog = {
          visible : false,
        };
        this.SetSessionTransferForm();
        var SessionTransfer = this.GenerateSTData();        
      this.$http.post("requesters/SessionSwitch/",SessionTransfer)
        .then((response) => {
          if(this.bPhoneVerified){
            this.sendURLSMS(response.body.data);
          }
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          this.successDialog = true;
        },
        () => {            
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          var ErrorDialog = {
            visible : true,
            title : 'Error',
            body : 'Something went wrong. Please try again or contact us.'
          }
          this.$store.commit(
              "ConfigModule/ErrorDialog",
              ErrorDialog
            );
        
        });  
      },
      GenerateQR(){
      this.$store.commit("requestermodule/bSessionTransferred", true);
      this.bSessionTransferred = true;
      this.$store.dispatch('requestermodule/partialAddReq',true);
      var LoaderDialog = {
          visible : false,
        };
      this.SetSessionTransferForm();
      var SessionTransfer = this.GenerateSTData();      
      SessionTransfer.bSendEmail = false;
      this.$http.post("requesters/SessionSwitch/",SessionTransfer)
        .then((response) => {
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          this.returnURL = response.body.data;
          let timerId = setInterval(() => {
               this.QRTimeStatement = true;   
                this.QRtime -= 1;  
                var m = Math.floor(this.QRtime % 3600 / 60);
                var s = Math.floor(this.QRtime % 3600 % 60);
                this.QRDisplayTime = ('0' + m).slice(-2) + ":" + ('0' + s).slice(-2);                       
                if (this.QRtime == 0 ) {
                  clearInterval(timerId);
                  this.QRTimeStatement = false;
                  this.QRDisplayTime = false;    
                  this.successDialog = true;
                }
              }, 1000);
        },
        () => {            
          this.$store.commit("ConfigModule/LoaderDialog",LoaderDialog);
          var ErrorDialog = {
            visible : true,
            title : 'Error',
            body : 'Something went wrong. Please try again or contact us.'
          }
          this.$store.commit(
              "ConfigModule/ErrorDialog",
              ErrorDialog
            );
        
        });  
      },
      SetSessionTransferForm(){
        var SessionTransferForm = {
        sEmail: this.emailValid.sRequesterEmailId,
        bEmailVerified: this.bEmailVerified,
        sPhone: this.sPhoneNo,
        bPhoneVerified: this.bPhoneVerified,
        sPhoneExt: this.selectedCountry
      }
      this.$store.commit("ConfigModule/SessionTransferForm", SessionTransferForm);
      },
      sendURLSMS(url) {
      var appKey = process.env.VUE_APP_RINGCAPTCHA_APP;
      var apiKey = process.env.VUE_APP_RINGCAPTCHA_API;
      var facName = this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.facilityLogoandBackground[0].sFacilityName;
      var locName = this.$store.state.requestermodule.sSelectedLocationName;
        var formData = new FormData();
        var phoneNo = this.selectedCountry + this.sPhoneNo;
        formData.append("phone", phoneNo);
        formData.append("api_key", apiKey);
        formData.append("message", "To request records from (" + facName + " – " + locName + "), please follow this link: "+ url);
        var apiURL = "https://api.ringcaptcha.com/" + appKey + "/sms";
        var self = this;
        this.$http
          .post(apiURL, formData, {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
            }
          })
          .then(response => {
            if (response.body) {
              console.log(response.body);
            self.subData = response;
            }
          });
      },
      closeSession(){
        this.successDialog = false;
        this.$store.commit("ConfigModule/bShowSessionTransfer",false);
        window.top.postMessage("hello", "*");                 
        window.location.reload();
      }
  },
};
</script>
