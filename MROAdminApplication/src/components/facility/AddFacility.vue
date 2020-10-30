<template>
  <div>
    <div id="AddFacilityPageBox">
      <form @submit.prevent="onSubmit" class="addfacility-form">
        <!-- <div id="addFormDiv">
          <span id="spanMandate">
            <i>All the fields are mandatory</i>
          </span>
        </div> -->
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label class="required" for="sFacilityName">Facility Name:</label>
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
            <label class="required" for="ROI Facility ID">ROI Facility Id:</label>
            <v-text-field
              type="number"
              id="nROIFacilityID"
              placeholder="Enter ROI Facility Id"
              v-model="facility.nROIFacilityID"
              @input="$v.facility.nROIFacilityID.$touch()"
              @blur="$v.facility.nROIFacilityID.$touch()"
              :error-messages="nROIFacilityIDErrors"
              solo
              min="1"
            ></v-text-field>
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
            <label for="sSMTPUsername">SMTP Username:   
              <v-dialog v-model="SMTPTestDialog" persistent max-width="400">
                <template v-slot:activator="{ on, attrs }">
                 <v-btn height="16px" icon v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="SMTPTestDialog=true"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"                      
                    >mdi-email</v-icon>
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="headline">Test SMTP</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12" sm="12">
                          <v-text-field 
                          label="Enter Email" 
                          v-model="sToTestEmail" 
                          required
                          @input="$v.sToTestEmail.$touch()"
                          @blur="$v.sToTestEmail.$touch()"
                          :error-messages="sToTestEmailErrors"
                          >
                          </v-text-field>
                        </v-col>              
                      </v-row>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="SMTPTestDialog = false">Close</v-btn>
                    <v-btn color="blue darken-1" :disabled="$v.sToTestEmail.$invalid" text @click="testSMTP()">Send</v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>  
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
            <label for="sSMTPPassword">SMTP Password:</label>
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
            <label class="required" for="sFTPUsername">FTP Username:</label>
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
            <v-text-field
              type="text"
              id="sFTPUrl"
              placeholder="Enter FTP URL"
              v-model="facility.sFTPUrl"
              @input="$v.facility.sFTPUrl.$touch()"
              @blur="$v.facility.sFTPUrl.$touch()"
              :error-messages="sFTPUrlErrors"
              solo
            ></v-text-field>
            <label class="required" for="sFTPUrl">Connection String:</label>
            <!-- <v-text-field
              type="text"
              id="sConnectionString"
              placeholder="Connection String"
              v-model="sConnectionString"
              @input="$v.sConnectionString.$touch()"
              @blur="$v.sConnectionString.$touch()"
              :error-messages="sConnectionStringErrors"
              solo
            ></v-text-field> -->
            <v-select id="nConnectionId" :rules="[(v) => !!v || 'Connection string is required']" required solo v-model="nConnectionId" :items="connectionStrings" item-text="sConnectionDisplayName" item-value="nConnectionID">
                
            </v-select>            
            <v-row>
              <v-col cols="6" md="6">    
                <label for="bRequestorEmailConfirm">Send confirmation email to Requestor ?</label>
                <v-switch
                  hide-details
                  inset
                  flat
                  color="rgb(0,91,168)"
                  solo
                  id="bRequestorEmailConfirm"
                  v-model="facility.bRequestorEmailConfirm"
                ></v-switch>
              </v-col>
              <v-col cols="6" md="6">  
                <label for="bRequestorEmailVerify">Send email verification to Requestor ?</label>
                <v-switch
                  hide-details
                  inset
                  flat
                  color="rgb(0,91,168)"
                  solo
                  id="bRequestorEmailVerify"
                  v-model="facility.bRequestorEmailVerify"
                ></v-switch>
              </v-col>
            </v-row>
            <v-row>
              <v-col cols="6" md="6">              
                <label class="required" for="Primary Timeout">Primary Timeout:</label>
                <v-text-field
                  type="number"
                  id="nPrimaryTimeout"
                  placeholder="Primary Timeout (In Seconds)"
                  v-model="facility.nPrimaryTimeout"
                  @input="$v.facility.nPrimaryTimeout.$touch()"
                  @blur="$v.facility.nPrimaryTimeout.$touch()"
                  :error-messages="nPrimaryTimeoutErrors"
                  solo
                  min="1"
                ></v-text-field>
              </v-col>
              <v-col cols="6" md="6">              
                <label class="required" for="Secondary Timeout">Secondary Timeout:</label>
                <v-text-field
                  type="number"
                  id="nSecondaryTimeout"
                  placeholder="Secondary Timeout (In Seconds)"
                  v-model="facility.nSecondaryTimeout"
                  @input="$v.facility.nSecondaryTimeout.$touch()"
                  @blur="$v.facility.nSecondaryTimeout.$touch()"
                  :error-messages="nSecondaryTimeoutErrors"
                  solo
                  min="1"
                ></v-text-field>
              </v-col>
            </v-row>
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn to="/facility" type="button" color="primary">Cancel</v-btn>
          <v-btn type="submit" color="primary" :disabled="$v.facility.$invalid || $v.nConnectionId.$invalid">Save</v-btn>
          <v-btn
            @click="goToLoc"
            type="button"
            :disabled="$v.facility.$invalid || $v.nConnectionId.$invalid"
            color="primary"
          >Save & Add Location</v-btn>
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
    <!-- Common Loader -->
        <v-dialog v-model="dialogLoader" persistent width="300" id="dialogLoader">
          <v-card color="rgb(0, 91, 168)" dark>
            <v-card-text>
              Please stand by
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
        <!-- SMTP Test Dialogs -->
        <v-dialog
          v-model="SMTPResponseDialog"
          max-width="400"
        >
        
        <v-card>
          <v-card-title class="headline">{{SMTPStatus}}</v-card-title>
          <v-card-text>
            {{SMTPResponseMsg}}
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="green darken-1"
              text
              @click="SMTPResponseDialog = false"
            >
              OK
            </v-btn>
          </v-card-actions>
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
  email,
  numeric
} from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    nConnectionId: {
      required
    },
    sToTestEmail:{
      required, email
    },
    facility: {
      sFacilityName: {
        required,
        maxLength: maxLength(40),
        minLength: minLength(2)
      },
      nROIFacilityID: { required, numeric },
      sDescription: {
        required,
        maxLength: maxLength(150),
        minLength: minLength(2)
      },
      sSMTPUsername: {
        maxLength: maxLength(100)
      },
      sSMTPPassword: {
        maxLength: maxLength(20)
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
        maxLength: maxLength(1000),
        minLength: minLength(5)
      },
      sFTPUrl: {
        required,
        maxLength: maxLength(200)
      },
      sOutboundEmail: { required, email },
      nPrimaryTimeout: { required, numeric },
      nSecondaryTimeout: { required, numeric },
    }
  },
  computed: {
    nConnectionIdErrors() {
      const errors = [];
      if (!this.$v.nConnectionId.$dirty) return errors;
      !this.$v.nConnectionId.required &&
        errors.push("Connection String is required.");
      return errors;
    },
    sToTestEmailErrors() {
      const errors = [];
      if (!this.$v.sToTestEmail.$dirty) return errors;
      !this.$v.sToTestEmail.email &&
        errors.push("Please provide a proper Email ID");
      !this.$v.sToTestEmail.required &&
        errors.push("Email is required.");
      return errors;
    },
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
    nROIFacilityIDErrors() {
      const errors = [];
      if (!this.$v.facility.nROIFacilityID.$dirty) return errors;
      !this.$v.facility.nROIFacilityID.numeric &&
        errors.push("Facility ROI Id Must be Numeric");
      !this.$v.facility.nROIFacilityID.required &&
        errors.push("Facility ROI Id is required.");
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
      !this.$v.facility.sSMTPPassword.maxLength &&
        errors.push("SMTP Password must be at most 20 characters long");
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
      !this.$v.facility.sFTPPassword.maxLength &&
        errors.push("FTP Password must be at most 1000 characters long");
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
    },
    nPrimaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.facility.nPrimaryTimeout.$dirty) return errors;
      !this.$v.facility.nPrimaryTimeout.numeric &&
        errors.push("Primary Timeout Must be Numeric");
      !this.$v.facility.nPrimaryTimeout.required &&
        errors.push("Primary Timeout is required.");
      return errors;
    },
    nSecondaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.facility.nSecondaryTimeout.$dirty) return errors;
      !this.$v.facility.nSecondaryTimeout.numeric &&
        errors.push("Secondary Timeout Must be Numeric");
      !this.$v.facility.nSecondaryTimeout.required &&
        errors.push("Secondary Timeout is required.");
      return errors;
    }
  },
  name: "AddFacility",
  data() {
    return {
      connectionStrings:[],
      dialogLoader:false,
      sameNameAlert: false,
      SMTPTestDialog:false,
      SMTPResponseDialog:false,
      SMTPStatus:'',
      SMTPResponseMsg:'',
      sToTestEmail:'',
      nConnectionId:null,
      facility: {
        nFacilityID: 0,
        nROIFacilityID: "",
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
        nPrimaryTimeout:0,
        nSecondaryTimeout:0,
        nCreatedAdminUserID: this.$store.state.adminUserId,
        nUpdatedAdminUserID: this.$store.state.adminUserId
      }
    };
  },
  mounted() {
    this.$http
      .get("Facility/GetMROConnectionString/")
      .then(resp => {
        if (resp.ok == true) {
          this.connectionStrings = resp.body;
        }
      });
  },
  methods: {
    // API Call to add facility
    goToLoc() {
      this.dialogLoader = true;
      this.facility.nROIFacilityID = parseInt(this.facility.nROIFacilityID);
      var combinedObj = {
        cFacility: this.facility,
        nConnectionId: this.nConnectionId
      };
      this.$http.post("facility/AddFacility", combinedObj).then(
        response => {
          if (response.ok == true) {
            this.dialogLoader =false;
            //if reponse ok then redirect to Add Location Page with Generated Facility ID
            this.$router.push("/AddLocation/" + response.body.nFacilityID);
          }
        },
        error => {
          // Error Callback
          if (error.status == 400) {
            this.dialogLoader = false;
            this.sameNameAlert = true;
          }
        }
      );
    },
    testSMTP(){
      // API Call to Test SMTP
      this.SMTPTestDialog = false
      this.dialogLoader =true;
      var testSMTPData= {
        sSMTPUsername: this.facility.sSMTPUsername,
        sSMTPPassword: this.facility.sSMTPPassword,
        sSMTPUrl: this.facility.sSMTPUrl,
        sOutboundEmail: this.facility.sOutboundEmail,
        sToTestEmail: this.sToTestEmail,
      }
      this.$http.post("facility/TestSMTPDetails", testSMTPData).then(
        response => {
          if (response.ok == true) {
            this.dialogLoader =false;    
            this.SMTPStatus='Info';
            this.SMTPResponseMsg='Test email sent successfully';
            this.SMTPResponseDialog=true;   
          }
        },
        error => {
          // Error Callback
          if (
            error.status == 400 
          ) {
            this.dialogLoader =false;
            this.SMTPStatus='Error - Something went wrong';
            this.SMTPResponseMsg=error.body;
            this.SMTPResponseDialog=true;   
          }
        }
      );
    },
    onSubmit() {
      // API Call to add facility
      this.dialogLoader =true;
      this.facility.nROIFacilityID = parseInt(this.facility.nROIFacilityID);
      var combinedObj = {
        cFacility: this.facility,
        nConnectionId: this.nConnectionId
      };
      this.$http.post("facility/AddFacility", combinedObj).then(
        response => {
          if (response.ok == true) {
            this.dialogLoader =false;
            //if reponse ok then redirect to Facility List Page
            this.$router.push("/facility");
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
        }
      );
    }
  }
};
</script>

<style scoped>
button,a{
  margin-right: 1.25em;
}
@media screen and (max-width: 500px) {
  #AddFacilityPageBox {
    margin: 0 0px;
  }
  h1 {
    font-size: 14px;
  }
}
.submit {
  text-align: center;
}

.addfacility-form {
  margin: 0.625em auto;
  border: 0.0625em solid #eee;
  padding: 1.25em;
  box-shadow: 0 0.125em 0.1875em #ccc;
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
  margin-top: 0.25em;
  margin-left: -0.0625em;
}
.row {
  margin: 0.0625em;
}
.required:after {
  content: " *";
  color: red;
}
</style>