<template>
  <div>
    <div id="EditFacilityPageBox">
      <form @submit.prevent="onSubmit" class="editfacility-form">
        <!-- <div id="editFormDiv">
          <span id="fieldsMandate">
            <i>All the fields are mandatory</i>
          </span>
        </div> -->
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <div id="marginDiv1"></div>
            <label class="required" for="sFacilityName">Facility Name:</label>
            <div id ="marginDiv2"></div>
            <v-text-field
              type="text"
              id="sFacilityName"
              placeholder="Facility Name"
              v-model="facility.sFacilityName"
              @input="$v.facility.sFacilityName.$touch()"
              @blur="$v.facility.sFacilityName.$touch()"
              :error-messages="sFacilityNameErrors"
              solo
            ></v-text-field>
            <div id ="marginDiv3"></div>
            <label class="required" for="sDescription">Facility Description:</label>
            <v-text-field
              type="text"
              id="sDescription"
              placeholder="General Description"
              v-model="facility.sDescription"
              @input="$v.facility.sDescription.$touch()"
              @blur="$v.facility.sDescription.$touch()"
              :error-messages="sDescriptionErrors"
              solo
            ></v-text-field>
            <div id ="marginDiv4"></div>
            <label for="sSMTPUsername">
              SMTP Username:
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="copySMTPDetails"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"
                    >mdi-content-copy</v-icon>
                  </v-btn>
                </template>
                <span>Click to Copy SMTP Details</span>
              </v-tooltip>
            </label>
            <v-text-field
              type="text"
              id="sSMTPUsername"
              placeholder="Enter SMTP Username"
              v-model="facility.sSMTPUsername"
              @input="$v.facility.sSMTPUsername.$touch()"
              @blur="$v.facility.sSMTPUsername.$touch()"
              :error-messages="sSMTPUsernameErrors"
              solo
            ></v-text-field>
            <label  for="sSMTPPassword">SMTP Password:</label>
            <div id ="marginDiv5"></div>
            <v-text-field
              type="password"
              id="sSMTPPassword"
              placeholder="Enter SMTP Password"
              v-model="facility.sSMTPPassword"
              @input="$v.facility.sSMTPPassword.$touch()"
              @blur="$v.facility.sSMTPPassword.$touch()"
              :error-messages="sSMTPPasswordErrors"
              solo
            ></v-text-field>
            <label class="required" for="sSMTPUrl">SMTP URL:</label>
            <v-text-field
              type="text"
              id="sSMTPUrl"
              placeholder="Enter SMTP URL"
              v-model="facility.sSMTPUrl"
              @input="$v.facility.sSMTPUrl.$touch()"
              @blur="$v.facility.sSMTPUrl.$touch()"
              :error-messages="sSMTPUrlErrors"
              solo
            ></v-text-field>
            <label class="required" for="sOutboundEmail">Outbound Email:</label>
            <v-text-field
              type="text"
              id="sOutboundEmail"
              placeholder="Enter Outbound Email"
              v-model="facility.sOutboundEmail"
              @input="$v.facility.sOutboundEmail.$touch()"
              @blur="$v.facility.sOutboundEmail.$touch()"
              :error-messages="sOutboundEmailErrors"
              solo
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="5">
            <label class="required" for="sFTPUsername">
              FTP Username:
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="copyFTPDetails"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"
                    >mdi-content-copy</v-icon>
                  </v-btn>
                </template>
                <span>Click to Copy FTP Details</span>
              </v-tooltip>
            </label>
            <v-text-field
              type="text"
              id="sFTPUsername"
              placeholder="Enter FTP Username"
              v-model="facility.sFTPUsername"
              @input="$v.facility.sFTPUsername.$touch()"
              @blur="$v.facility.sFTPUsername.$touch()"
              :error-messages="sFTPUsernameErrors"
              solo
            ></v-text-field>
            <label class="required" for="sFTPPassword">FTP Password:</label>
            <v-text-field
              type="password"
              id="sFTPPassword"
              placeholder="Enter FTP Password"
              v-model="facility.sFTPPassword"
              @input="$v.facility.sFTPPassword.$touch()"
              @blur="$v.facility.sFTPPassword.$touch()"
              :error-messages="sFTPPasswordErrors"
              solo
            ></v-text-field>
            <label class="required" for="sFTPUrl">FTP URL:</label>
            <div id ="marginDiv6"></div>
            <v-text-field
              type="text"
              id="sFTPUrl"
              placeholder="Enter FTP URL"
              v-model="facility.sFTPUrl"
              input="$v.facility.sFTPUrl.$touch()"
              @blur="$v.facility.sFTPUrl.$touch()"
              :error-messages="sFTPUrlErrors"
              solo
            ></v-text-field>
            <!-- Show GUID & -->
            <label for="sGUID">
              Guid for URL (Read Only):
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="copyGUID"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"
                    >mdi-content-copy</v-icon>
                  </v-btn>
                </template>
                <span>Click to Copy GUID</span>
              </v-tooltip>Button Code:
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="copyBtnCode"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"
                    >mdi-content-copy</v-icon>
                  </v-btn>
                </template>
                <span>Click to copy Button Code</span>
              </v-tooltip>
            </label>
            <!-- Show confirm email to requestor -->
            <v-text-field type="text" v-model="sGUID" :readonly="true" id="sGUID" solo></v-text-field>
            <label for="bRequestorEmailConfirm">Send confirmation email to Requestor ?</label>
            <v-switch
              inset
              flat
              color="rgb(0,91,168)"
              solo
              id="bRequestorEmailConfirm"
              v-model="facility.bRequestorEmailConfirm"
            ></v-switch>
            <div id="marginDiv7"></div>
            <!-- <label for="bRequestorEmailVerify">Send email verification for Requestor email address ?</label>
            <v-switch
              inset
              flat
              color="rgb(0,91,168)"
              solo
              id="bRequestorEmailVerify"
              v-model="facility.bRequestorEmailVerify"
            ></v-switch> -->
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn to="/facility" type="button" color="primary">Cancel</v-btn>
        </div>
      </form>
    </div>
    <!-- Dialog Alert for Same name facility -->
    <v-dialog v-model="sameNameAlert" width="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Facility with Same Name Exists</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="sameNameAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Loader to indicate Admin Module is getting ready -->
        <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="rgb(0, 91, 168)" flat dark max-height="500">
            <v-card-text>Please stand by<v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import {
  required,
  minLength,
  maxLength,
  email
} from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    facility: {
      sFacilityName: {
        required,
        maxLength: maxLength(40),
        minLength: minLength(2)
      },
      sDescription: {
        required,
        maxLength: maxLength(150),
        minLength: minLength(2)
      },
      sSMTPUsername: {
        maxLength: maxLength(100)
      },
      sSMTPPassword: {
        //maxLength: maxLength(20),
        minLength: minLength(5)
      },
      sSMTPUrl: {
        required,
        maxLength: maxLength(200)
      },
      sFTPUsername: {
        required,
        maxLength: maxLength(100)
      },
      sFTPPassword: {
        required,
        //maxLength: maxLength(1000),
        minLength: minLength(5)
      },
      sFTPUrl: {
        required,
        maxLength: maxLength(200)
      },
      sOutboundEmail: { required, email }
    }
  },
  computed: {
    sFacilityNameErrors() {
      const errors = [];
      if (!this.$v.facility.sFacilityName.$dirty) return errors;
      !this.$v.facility.sFacilityName.minLength &&
        errors.push("Facility Name must be at least 2 characters long");
      !this.$v.facility.sFacilityName.maxLength &&
        errors.push("Facility Name must be at most 40 characters long");
      !this.$v.facility.sFacilityName.required &&
        errors.push("Facility Name is required.");
      return errors;
    },
    sDescriptionErrors() {
      const errors = [];
      if (!this.$v.facility.sDescription.$dirty) return errors;
      !this.$v.facility.sDescription.minLength &&
        errors.push("Facility Description must be at least 2 characters long");
      !this.$v.facility.sDescription.maxLength &&
        errors.push("Facility Description must be at most 150 characters long");
      !this.$v.facility.sDescription.required &&
        errors.push("Facility Description is required.");
      return errors;
    },
    sSMTPUsernameErrors() {
      const errors = [];
      if (!this.$v.facility.sSMTPUsername.$dirty) return errors;
      !this.$v.facility.sSMTPUsername.maxLength &&
        errors.push("SMTP Username must be at most 100 characters long");
      return errors;
    },
    sSMTPPasswordErrors() {
      const errors = [];
      if (!this.$v.facility.sSMTPPassword.$dirty) return errors;
      !this.$v.facility.sSMTPPassword.minLength &&
        errors.push("SMTP Password must be at least 5 characters long");
      // !this.$v.facility.sSMTPPassword.maxLength &&
      //   errors.push("SMTP Password must be at most 20 characters long");
      return errors;
    },
    sSMTPUrlErrors() {
      const errors = [];
      if (!this.$v.facility.sSMTPUrl.$dirty) return errors;
      !this.$v.facility.sSMTPUrl.maxLength &&
        errors.push("SMTP URL must be at most 200 characters long");
      !this.$v.facility.sSMTPUrl.required &&
        errors.push("SMTP URL is required.");
      return errors;
    },
    sFTPUsernameErrors() {
      const errors = [];
      if (!this.$v.facility.sFTPUsername.$dirty) return errors;
      !this.$v.facility.sFTPUsername.maxLength &&
        errors.push("FTP Username must be at most 100 characters long");
      !this.$v.facility.sFTPUsername.required &&
        errors.push("FTP Username is required.");
      return errors;
    },
    sFTPPasswordErrors() {
      const errors = [];
      if (!this.$v.facility.sFTPPassword.$dirty) return errors;
      !this.$v.facility.sFTPPassword.minLength &&
        errors.push("FTP Password must be at least 5 characters long");
      // !this.$v.facility.sFTPPassword.maxLength &&
      //   errors.push("FTP Password must be at most 1000 characters long");
      !this.$v.facility.sFTPPassword.required &&
        errors.push("FTP Password is required.");
      return errors;
    },
    sFTPUrlErrors() {
      const errors = [];
      if (!this.$v.facility.sFTPUrl.$dirty) return errors;
      !this.$v.facility.sFTPUrl.maxLength &&
        errors.push("FTP Url must be at most 200 characters long");
      !this.$v.facility.sFTPUrl.required && errors.push("FTP URL is required.");
      return errors;
    },
    sOutboundEmailErrors() {
      const errors = [];
      if (!this.$v.facility.sOutboundEmail.$dirty) return errors;
      !this.$v.facility.sOutboundEmail.email &&
        errors.push("Please provide a proper Email ID");
      !this.$v.facility.sOutboundEmail.required &&
        errors.push("Email is required.");
      return errors;
    }
  },
  name: "EditFacility",
  data() {
    return {
      dialogLoader:false,
      sameNameAlert: false,
      sBtnCode:'',
      sGUID: "",
      facility: {
        nFacilityID: 0,
        sFacilityName: "",
        sDescription: "",
        sSMTPUsername: "",
        sSMTPPassword: "",
        sSMTPUrl: "",
        sFTPUsername: "",
        sFTPPassword: "",
        sFTPUrl: "",
        sOutboundEmail: "",
        bActiveStatus: true,
        bRequestorEmailConfirm: false,
        bRequestorEmailVerify:false,
        nCreatedAdminUserID: this.$store.state.adminUserId,
        nUpdatedAdminUserID: this.$store.state.adminUserId
      }
    };
  },
  mounted() {
    this.dialogLoader = true;
    // API call to get Facility
    this.$http.get("facility/GetHTMLButtonCode/sFacilityID="+this.$route.params.id).then(
      response=>{if(response.ok==true){
        this.sBtnCode = response.bodyText;
        //console.log(response.bodyText);
      }},
      error=>{console.log(error);}
    );
    this.$http.get("facility/GetFacility/sFacilityID=" + this.$route.params.id+"&sAdminUserID="+this.$store.state.adminUserId).then(
      response => {
        // get body data
        this.facility = JSON.parse(response.bodyText);
      },
      error => {
        // error callback
        this.gridData = error.body;
      }
    );
    //Get GUID for Facility
    this.$http.get("facility/GetFacilityGUID/" + this.$route.params.id).then(
      response => {
        this.sGUID = response.bodyText;
      },
      error => {
        // error callback
        console.log(error);
      }
    );
    this.dialogLoader = false;
  },
  methods: {
    copySMTPDetails() {
      if(navigator.clipboard != undefined){//Chrome
        navigator.clipboard.writeText(
        this.facility.sSMTPUrl +
          "\n" +
          this.facility.sSMTPUsername +
          "\n" 
      );
      }
    },
    copyFTPDetails() {
      if(navigator.clipboard != undefined){//Chrome
        navigator.clipboard.writeText(
        this.facility.sFTPUrl +
          "\n" +
          this.facility.sFTPUsername +
          "\n"
      );
      }
    },
    copyGUID() {
      if(navigator.clipboard != undefined){//Chrome
      navigator.clipboard.writeText(this.sGUID);}
    },
    copyBtnCode(){
       if(navigator.clipboard != undefined){//Chrome
          navigator.clipboard.writeText(this.sBtnCode);
       }
    },
    // API call to post facility (Edit Facility)
    onSubmit() {
      this.dialogLoader = true;
      this.$http
        .post(
          "facility/EditFacility/" + this.facility.nFacilityID,
          this.facility
        )
        .then(response => {
          if (response.ok == true) {
            this.dialogLoader = false;
            this.$router.push("/Facility");
}
        },
        error => {
          // Error Callback
          if (
            error.status == 400 
          ) {
            this.dialogLoader =false;
            this.sameNameAlert = true;
          }
          console.log(error.body);
        }
        );
    }
  }
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditFacilityPageBox{
    margin:0 0em
  }
  h1 {
    font-size: 14px;
  }
}
button{
  margin-right: 1em;
}
#marginDiv1{margin-bottom:0.3125em}
#marginDiv2{margin-bottom:0.5625em}
#marginDiv3{margin-bottom:-0.0625em}
#marginDiv4{margin-bottom:-0.5625em}
#marginDiv5{margin-bottom:1em}
#marginDiv6{margin-bottom:0.3125em

}
#marginDiv7{margin-bottom:1.25em}
.submit {
  text-align: center;
}

.editfacility-form {
  margin: 0.625em auto;
  border: 0.0625em solid #eee;
  padding: 0.9375em;
  box-shadow: 0 0.0625em 0.0625em #ccc;
  font-size: 0.9375em;
}

.input label {
  display: block;
  color: #4e4e4e;
  margin-bottom: 0.375em;
}

.submit button {
  font: inherit;
  cursor: pointer;
}

.submit button[disabled],
.submit button[disabled]:hover,
.submit button[disabled]:active {
  border: 0.078125em solid #ccc;
  background-color: transparent;
  color: #ccc;
  cursor: not-allowed;
}

i {
  color: rgb(40, 40, 40);
}
label {
  margin-left: -0.0625em;
}
.required:after {
  content: " *";
  color: red;
}
</style>