<template>
  <div>
    <div id="box">
      <form @submit.prevent="onSubmit" class="addfacility-form">
        <div id="formDiv">
          <span id="spanMandate">
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
            <label for="sSMTPUsername">SMTP Username:</label>
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
            <label for="sOutboundEmail">Outbound Email:</label>
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
            <label for="sFTPUsername">FTP Username:</label>
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
            <label for="sFTPUrl">FTP URL:</label>
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
            <label for="sFTPUrl">Connection String:</label>
            <v-text-field
              type="text"
              id="sConnectionString"
              placeholder="Connection String"
              v-model="sConnectionString"
              @input="$v.sConnectionString.$touch()"
              @blur="$v.sConnectionString.$touch()"
              :error-messages="sConnectionStringErrors"
              solo
            ></v-text-field>
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
          <v-btn to="/facility" type="submit" color="primary">Cancel</v-btn>
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn
            @click="goToLoc"
            type="button"
            :disabled="this.$v.$invalid"
            color="primary"
          >Save & Add Location</v-btn>
        </div>
      </form>
    </div>
    <!-- Dialog Alert for Same name facility -->
    <v-dialog v-model="sameNameAlert" width ="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Facility with Same Name Exists</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="sameNameAlert = false">Ok</v-btn>
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
  email
} from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    sConnectionString: {
      required,
      maxLength: maxLength(1000)
    },
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
    sConnectionStringErrors() {
      const errors = [];
      if (!this.$v.sConnectionString.$dirty) return errors;
      !this.$v.sConnectionString.maxLength &&
        errors.push("Connection String must be at most 1000 characters long");
      !this.$v.sConnectionString.required &&
        errors.push("Connection String is required.");
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
      !this.$v.facility.sFTPUrl.required && errors.push("FTP Url is required.");
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
  name: "AddFacility",
  data() {
    return {
      sameNameAlert:false,
      sConnectionString: "",
      genFacId: "",
      facility: {
        nFacilityID: 0,
        nROIFacilityID: 0,
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
        bRequestorEmailConfirm: true
      }
    };
  },
  methods: {
    // API Call to add facility
    goToLoc() {
      var combinedObj = {
        cFacility: this.facility,
        sConnectionString: this.sConnectionString
      };
      this.$http.post("facility/AddFacility", combinedObj).then(response => {
        if (response.ok == true) {
          //if reponse ok then redirect to Add Location Page with Generated Facility ID
          this.$router.push("/AddLocation/" + response.body.nFacilityID);
        }
      },error => {
          // Error Callback
          if(error.status==400&&error.bodyText=="Cannot Add Facility with Same Name"){
          this.sameNameAlert = true;
        }
          console.log(error.body);
        }
      );
    },
    onSubmit() {
      // API Call to add facility
      var combinedObj = {
        cFacility: this.facility,
        sConnectionString: this.sConnectionString
      };
      this.$http.post("facility/AddFacility", combinedObj).then(response => {
        if (response.ok == true) {
          //if reponse ok then redirect to Facility List Page
          this.$router.push("/facility");
        }
      },error => {
          // Error Callback
          if(error.status==400&&error.bodyText=="Cannot Add Facility with Same Name"){
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
#spanMandate {
  font-size: 14px;
}
#formDiv {
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
  margin: 5px;
}
.addfacility-form {
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