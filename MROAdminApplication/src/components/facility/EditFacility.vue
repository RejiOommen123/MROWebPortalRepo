<template>
  <div>
    <div id="box">
      <form @submit.prevent="onSubmit" class="editfacility-form">
        <div id="editFormDiv">
          <span id="fieldsMandate">
            <i>All the fields are mandatory</i>
          </span>
        </div>
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label for="sFacilityName">Facility Name:</label>
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
            <label for="sDescription">Facility Description:</label>
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
            <label for="sSMTPUsername">
              SMTP Username:
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on">
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
            <label for="sSMTPUrl">SMTP URL:</label>
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
            <label class="col-md-4" for="sOutboundEmail">Outbound Email:</label>
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
            <label for="sFTPUsername">
              FTP Username:
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on">
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
            <label for="sFTPPassword">FTP Password:</label>
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
            <label class="col-md-4" for="sFTPUrl">FTP URL:</label>
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
            <!-- Show GUID -->
            <label class="col-md-4" for="sGUID">
              Guid for URL (Read Only):
              <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon v-bind="attrs" v-on="on">
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
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn to="/facility" type="submit" color="primary">Cancel</v-btn>
        </div>
      </form>
    </div>
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
        maxLength: maxLength(80),
        minLength: minLength(2)
      },
      sSMTPUsername: 
      { 
        required,
        maxLength: maxLength(100), 
      },
      sSMTPPassword: {
        required,
        maxLength: maxLength(20),
        minLength: minLength(5)
      },
      sSMTPUrl: 
      { 
        required,
        maxLength: maxLength(200),
      },
      sFTPUsername: 
      { 
        required,
        maxLength: maxLength(100),  
      },
      sFTPPassword: {
        required,
        maxLength: maxLength(20),
        minLength: minLength(5)
      },
      sFTPUrl: 
      { 
        required,
        maxLength: maxLength(200),
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
        errors.push("Facility Description must be at most 80 characters long");
      !this.$v.facility.sDescription.required &&
        errors.push("Facility Description is required.");
      return errors;
    },
    sSMTPUsernameErrors() {
      const errors = [];
      if (!this.$v.facility.sSMTPUsername.$dirty) return errors;
      !this.$v.facility.sSMTPUsername.maxLength &&
        errors.push("SMTP Username must be at most 100 characters long");
      !this.$v.facility.sSMTPUsername.required &&
        errors.push("SMTP Username is required.");
      return errors;
    },
    sSMTPPasswordErrors() {
      const errors = [];
      if (!this.$v.facility.sSMTPPassword.$dirty) return errors;
      !this.$v.facility.sSMTPPassword.minLength &&
        errors.push("SMTP Password must be at least 5 characters long");
      !this.$v.facility.sSMTPPassword.maxLength &&
        errors.push("SMTP Password must be at most 20 characters long");
      !this.$v.facility.sSMTPPassword.required &&
        errors.push("SMTP Password is required.");
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
        errors.push("FTP Password must be at most 20 characters long");
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
        bRequestorEmailConfirm: Boolean
      }
    };
  },
  mounted() {
    // API call to get Facility
    this.$http.get("facility/GetFacility/" + this.$route.params.id).then(
      response => {
        // get body data
        this.facility = JSON.parse(response.bodyText);
      },
      response => {
        // error callback
        this.gridData = response.body;
      }
    );
    //Get GUID for Facility
    this.$http.get("facility/GetFacilityGUID/" + this.$route.params.id).then(
      response => {
        this.sGUID = response.bodyText;
      },
      response => {
        // error callback
        console.log(response);
      }
    );
  },
  methods: {
    copySMTPDetails() {
      navigator.clipboard.writeText(
        this.facility.sSMTPUrl +
          "\n" +
          this.facility.sSMTPUsername +
          "\n" +
          this.facility.sSMTPPassword
      );
    },
    copyFTPDetails() {
      navigator.clipboard.writeText(
        this.facility.sFTPUrl +
          "\n" +
          this.facility.sFTPUsername +
          "\n" +
          this.facility.sFTPPassword
      );
    },
    copyGUID() {
      navigator.clipboard.writeText(this.sGUID);
    },
    // API call to post facility (Edit Facility)
    onSubmit() {
      this.$http
        .post(
          "facility/EditFacility/" + this.facility.nFacilityID,
          this.facility
        )
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/Facility");
          }
        });
    }
  }
};
</script>

<style scoped>
#fieldsMandate {
  font-size: 14px;
}
#editFormDiv {
  margin-top: -15px;
  margin-left: -20px;
}
.submit {
  text-align: center;
}
#box {
  margin: 10px;
}
* {
  margin: 4px;
}

.editfacility-form {
  margin: 10px auto;
  border: 1px solid #eee;
  padding: 20px;
  box-shadow: 0 2px 3px #ccc;
  font-size: 15px;
}

.input {
  margin: 10px auto;
}

.input label {
  display: block;
  color: #4e4e4e;
  margin-bottom: 6px;
}

.input.inline label {
  display: inline;
}

.input input {
  font: inherit;
  width: 100%;
  padding: 6px 12px;
  box-sizing: border-box;
  border: 1px solid #ccc;
}

.input.inline input {
  width: auto;
}

.input input:focus {
  outline: none;
  border: 1px solid #521751;
  background-color: #eee;
}

.input select {
  border: 1px solid #ccc;
  font: inherit;
}

.submit button {
  border: 1px solid #521751;
  color: #521751;
  padding: 10px 20px;
  font: inherit;
  cursor: pointer;
}

.submit button:hover,
.submit button:active {
  background-color: #521751;
  color: white;
}

.submit button[disabled],
.submit button[disabled]:hover,
.submit button[disabled]:active {
  border: 1px solid #ccc;
  background-color: transparent;
  color: #ccc;
  cursor: not-allowed;
}
i {
  color: rgb(40, 40, 40);
}
label {
  margin-top: 4px;
  margin-left: -1px;
}
.row {
  margin: 1px;
}
</style>