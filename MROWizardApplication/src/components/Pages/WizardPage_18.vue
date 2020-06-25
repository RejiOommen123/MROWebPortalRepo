<template>
  <div class="center">
    <div class="form-group">
      <h1>
        Let us send you a text
        <br />to verify your phone
      </h1>
      <form>
        <v-row>
          <v-col cols="12" offset-sm="3" sm="6">
            <v-text-field placeholder="+(XX) (XXX) XXX-XXXX" v-model="sPhoneNo" label="Enter Mobile No" required></v-text-field>
          </v-col>
          <v-col cols="12" offset-sm="1" sm="10">
            <p>
              NOTE: A verified phone number helps us trust that this
              <br />request is from a reliable source and allows us to follow-up
              <br />with any questions and/or updates.
            </p>
          </v-col>
          <v-col cols="12" offset-sm="3" sm="6">
            <v-btn @click.prevent="submit" class="ma-2" color="success">Send Verification Code</v-btn>
            <div v-show="bOtpSend">
            <div>
              <v-text-field v-model="sVerify" label="Enter 4 digit OTP" required></v-text-field>
              <v-btn @click.prevent="verifyCode" class="ma-2" color="success">Verify</v-btn>
              </div>
            </div>
          </v-col>
        </v-row>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_12",
  data() {
    return {
      bOtpSend: false,

      sPhoneNo: "",
      sApp_Key: "tu9ete3u9ocidovebefu",
      sApi_Key: "51bdcc70021d29097aedce2a39ecb2beaa379e1b",
      sVerify: "",
      service: "",
      subData: {}
    };
  },
  created(){ 
    this.$vuetify.theme.dark = true
  },
  methods: {
    submit() {
      this.bOtpSend = true;
      var obj = {};
      obj["phone"] = this.sPhoneNo;
      obj["api_key"] = this.sApi_Key;
      var formData = new FormData();
      formData.append("phone", this.sPhoneNo);
      formData.append("api_key", this.sApi_Key);
      var url = "https://api.ringcaptcha.com/" + this.sApp_Key + "/code/SMS";

      var self = this;
      console.log(self);
      this.$http
        .post(url, formData, {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
          }
        })
        .then(response => {
          self.subData = response;
        })
        .catch(err => {
          console.log(err);
        });
    },
    verifyCode() {
      var obj = {};
      obj["phone"] = this.sPhoneNo;
      obj["code"] = this.sVerify;
      var formData = new FormData();
      formData.append("phone", this.sPhoneNo);
      formData.append("code", this.sVerify);
      formData.append("api_key", this.sApi_Key);
      var url = "https://api.ringcaptcha.com/" + this.sApp_Key + "/verify";

      var self = this;
      console.log(self);
      this.$http
        .post(url, formData, {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"
          }
        })
        .then(response => {
          if(response.data.status=="SUCCESS"){
           this.$store.commit("requestermodule/sPhoneNo", this.sPhoneNo);
          this.$store.commit("ConfigModule/mutateNextIndex");
          }
          console.log(response.data.status);
        })
        .catch(err => {
          console.log(err);
        });
    }
  }
};
</script>

<style scoped>

</style>