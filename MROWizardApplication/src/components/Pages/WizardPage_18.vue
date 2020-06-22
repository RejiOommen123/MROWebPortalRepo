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
            <v-text-field placeholder="+(XX) (XXX) XXX-XXXX" v-model="title" label="Enter Mobile No" required></v-text-field>
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
            <div v-show="otpSend">
            <div>
              <v-text-field v-model="verify" label="Enter 4 digit OTP" required></v-text-field>
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
      otpSend: false,

      title: "",
      app_key: "tu9ete3u9ocidovebefu",
      api_key: "51bdcc70021d29097aedce2a39ecb2beaa379e1b",
      verify: "",
      service: "",
      subData: {}
    };
  },
  created(){ 
    this.$vuetify.theme.dark = true
  },
  methods: {
    submit() {
      this.otpSend = true;
      var obj = {};
      obj["phone"] = this.title;
      obj["api_key"] = this.api_key;
      var formData = new FormData();
      formData.append("phone", this.title);
      formData.append("api_key", this.api_key);
      var url = "https://api.ringcaptcha.com/" + this.app_key + "/code/SMS";

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
      obj["phone"] = this.title;
      obj["code"] = this.verify;
      var formData = new FormData();
      formData.append("phone", this.title);
      formData.append("code", this.verify);
      formData.append("api_key", this.api_key);
      var url = "https://api.ringcaptcha.com/" + this.app_key + "/verify";

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

                  
          //       this.$store.commit("ConfigModule/mutatedialogMinWidth", "100%");
          // this.$store.commit("ConfigModule/mutatedialogMaxWidth", "100%");
          // this.$store.commit("ConfigModule/mutatedialogMaxHeight", "100%");
          //   this.$vuetify.theme.dark = false
          // this.$store.commit("ConfigModule/mutatepageNumerical", 13);
          // this.$store.commit("ConfigModule/mutateCurrentPage", "page-13");
             this.$store.commit("ConfigModule/mutateNextIndex");
          
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
/* .center {
  text-align: center;
}
.disclaimer{
    font-size: 14px;
    text-align: center;
    color: #ababab;
} */
/* .invalid label {
  color: red;
}

.invalid input {
  border-bottom: 1px solid red;
}

.invalid span {
  color: red;
}
input {
  background: transparent;
  border: none;
  border-bottom: 1px solid #000000;
}
input:focus {
  outline: none;
} */
</style>