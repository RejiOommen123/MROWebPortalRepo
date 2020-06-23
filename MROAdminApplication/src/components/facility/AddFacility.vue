<template>
  <div>
    <div id="box">
      <form @submit.prevent="onSubmit" class="addfacility-form">
        <div style="margin-top:-15px;margin-left:-20px">
          <span style="font-size:14px">
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
      sSMTPUsername: { required },
      sSMTPPassword: {
        required,
        maxLength: maxLength(20),
        minLength: minLength(5)
      },
      sSMTPUrl: { required },
      sFTPUsername: { required },
      sFTPPassword: {
        required,
        maxLength: maxLength(20),
        minLength: minLength(5)
      },
      sFTPUrl: { required },
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
      // !this.$v.facility.sSMTPUsername.minLength && errors.push('SMTP Username must be at least 5 characters long')
      // !this.$v.facility.sSMTPUsername.maxLength && errors.push('SMTP Username must be at most 20 characters long')
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
      // !this.$v.facility.sSMTPUrl.url && errors.push('Please provide a proper URl')
      !this.$v.facility.sSMTPUrl.required &&
        errors.push("SMTP Url is required.");
      return errors;
    },
    sFTPUsernameErrors() {
      const errors = [];
      if (!this.$v.facility.sFTPUsername.$dirty) return errors;
      // !this.$v.facility.sSMTPUsername.minLength && errors.push('SMTP Username must be at least 5 characters long')
      // !this.$v.facility.sSMTPUsername.maxLength && errors.push('SMTP Username must be at most 20 characters long')
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
      // !this.$v.facility.sFTPUrl.url && errors.push('Please provide a proper URl')
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
        bActiveStatus: true
      }
    };
  },
  methods: {
    // API to add facility
    goToLoc() {
      // this.$v.$touch()
      this.$http
        .post("facility/AddFacility", this.facility)
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/locations/" + response.body.nFacilityID);
          }
        });
    },
    onSubmit() {
      // this.$v.$touch()
      this.$http
        .post("facility/AddFacility", this.facility)
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/facility");
          }
        });
    }
  }
};
</script>

<style scoped>
.submit {
  text-align: center;
}
#box {
  margin: 25px;
}
* {
  margin: 10px;
}
.addfacility-form {
  /* width: 1200px; */
  margin: 30px auto;
  border: 1px solid #eee;
  padding: 20px;
  box-shadow: 0 2px 3px #ccc;
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
  margin-left:-1px
}
.row{
margin:1px
}
</style>