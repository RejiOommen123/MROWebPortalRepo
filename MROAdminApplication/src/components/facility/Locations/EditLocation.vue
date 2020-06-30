<template>
  <div>
    <div id="box">
      <form @submit.prevent="onSubmit" class="editlocation-form">
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label class="required" for="sLocationName">Location Name:</label>
            <v-text-field
              type="text"
              id="sLocationName"
              placeholder="Enter Location Name"
              v-model="location.sLocationName"
              @input="$v.location.sLocationName.$touch()"
              @blur="$v.location.sLocationName.$touch()"
              :error-messages="sLocationNameErrors"
              solo
            ></v-text-field>
            <label class="required" for="nROILocationID">ROI Location Id:</label>
            <v-text-field
              type="number"
              id="nROILocationID"
              placeholder="Enter Location Id"
              v-model="location.nROILocationID"
              @input="$v.location.nROILocationID.$touch()"
              @blur="$v.location.nROILocationID.$touch()"
              :error-messages="nROILocationIDErrors"
              solo
              min="1"
            ></v-text-field>
            <label class="required" for="sLocationCode">Location Code:</label>
            <v-text-field
              type="text"
              id="sLocationCode"
              placeholder="Enter Location Code"
              v-model="location.sLocationCode"
              @input="$v.location.sLocationCode.$touch()"
              @blur="$v.location.sLocationCode.$touch()"
              :error-messages="sLocationCodeErrors"
              solo
            ></v-text-field>
            <label class="required" for="sLocationAddress">Location Address:</label>
            <v-text-field
              type="text"
              id="sLocationAddress"
              placeholder="Enter Address"
              v-model="location.sLocationAddress"
              @input="$v.location.sLocationAddress.$touch()"
              @blur="$v.location.sLocationAddress.$touch()"
              :error-messages="sLocationAddressErrors"
              solo
            ></v-text-field>
            <label class="required" for="sPhoneNo">Phone No:</label>
            <v-text-field
              type="text"
              id="nPhoneNo"
              placeholder="Enter Phone No"
              v-model="location.sPhoneNo"
              @input="$v.location.sPhoneNo.$touch()"
              @blur="$v.location.sPhoneNo.$touch()"
              :error-messages="sPhoneNoErrors"
              solo
            ></v-text-field>
            <label for="sFaxNo">Fax No:</label>
            <v-text-field
              type="text"
              id="nFaxNo"
              placeholder="Enter Fax No"
              v-model="location.sFaxNo"
              @input="$v.location.sFaxNo.$touch()"
              @blur="$v.location.sFaxNo.$touch()"
              :error-messages="sFaxNoErrors"
              solo
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="5">
            <label for="sAuthTemplate">Authorization Template:</label>
            <v-file-input
              chips
              show-size
              dense
              hint="Upload Authorization Document"
              rounded
              label="Upload PDF"
              filled
              prepend-icon="mdi-file-pdf"
              accept=".pdf"
              @change="onPDFFileChanged"
              :placeholder="location.sAuthTemplateName"
            ></v-file-input>
            <label for="sConfigFacilityLogo">Logo Image:</label>
            <v-img :src="location.sConfigLogoData" width="20%"></v-img>
            <br />
            <v-file-input
              lazy-src
              chips
              show-size
              dense
              hint="Upload Logo Image"
              rounded
              label="Logo Image"
              filled
              prepend-icon="mdi-camera"
              @change="onLogoFileChanged"
              :placeholder="location.sConfigLogoName"
              accept="image/png, image/jpeg, image/bmp"
            >
              <v-tooltip slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="rgb(0, 91, 168)" top>mdi-information</v-icon>
                </template>
                <span>Please upload logo with height 50px</span>
              </v-tooltip>
            </v-file-input>
            <label for="sConfigBackgroundImg">Background Image:</label>
            <v-img :src="location.sConfigBackgroundData" width="20%"></v-img>
            <br />
            <v-file-input
              chips
              show-size
              dense
              hint="Upload Background Image"
              rounded
              class="col-md-2"
              label="Background Image"
              filled
              prepend-icon="mdi-camera"
              @change="onBackgroundFileChanged"
              :placeholder="location.sConfigBackgroundName"
              accept="image/png, image/jpeg, image/bmp"
            >
              <v-tooltip slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="rgb(0, 91, 168)" top>mdi-information</v-icon>
                </template>
                <span>Please upload logo with height 50px</span>
              </v-tooltip>
            </v-file-input>
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn :to="'/locations/'+this.location.nFacilityID" type="submit" color="primary">Cancel</v-btn>
        </div>
        <br />
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
  numeric
} from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    location: {
      sLocationName: {
        required,
        maxLength: maxLength(40),
        minLength: minLength(2)
      },
      nROILocationID: { required, numeric },
      sLocationCode: {
        required,
        maxLength: maxLength(4),
        minLength: minLength(4)
      },
      sLocationAddress: { required, maxLength: maxLength(1000) },
      sPhoneNo: {
        required,
        maxLength: maxLength(10),
        minLength: minLength(10),
        numeric
      },
      sFaxNo: { maxLength: maxLength(10), minLength: minLength(10), numeric }
    }
  },
  computed: {
    sLocationNameErrors() {
      const errors = [];
      if (!this.$v.location.sLocationName.$dirty) return errors;
      !this.$v.location.sLocationName.minLength &&
        errors.push("Location Name must be at least 2 characters long");
      !this.$v.location.sLocationName.maxLength &&
        errors.push("Location Name must be at most 40 characters long");
      !this.$v.location.sLocationName.required &&
        errors.push("Location Name is required.");
      return errors;
    },
    nROILocationIDErrors() {
      const errors = [];
      if (!this.$v.location.nROILocationID.$dirty) return errors;
      !this.$v.location.nROILocationID.numeric &&
        errors.push("Location Id Must be Numeric");
      !this.$v.location.nROILocationID.required &&
        errors.push("Location Id is required.");
      return errors;
    },
    sLocationCodeErrors() {
      const errors = [];
      if (!this.$v.location.sLocationCode.$dirty) return errors;
      !this.$v.location.sLocationCode.minLength &&
        errors.push("Location Code must be at least 4 characters long");
      !this.$v.location.sLocationCode.maxLength &&
        errors.push("Location Code must be at most 4 characters long");
      !this.$v.location.sLocationCode.required &&
        errors.push("Location Code is required.");
      return errors;
    },
    sLocationAddressErrors() {
      const errors = [];
      if (!this.$v.location.sLocationAddress.$dirty) return errors;
      !this.$v.location.sLocationAddress.maxLength &&
        errors.push("Location Address must be at most 1000 characters long");
      !this.$v.location.sLocationAddress.required &&
        errors.push("Location Address is required.");
      return errors;
    },
    sPhoneNoErrors() {
      const errors = [];

      if (!this.$v.location.sPhoneNo.$dirty) return errors;
      !this.$v.location.sPhoneNo.numeric &&
        errors.push("Phone Number can only have numbers");
      !this.$v.location.sPhoneNo.minLength &&
        errors.push("Phone Number cannot be less than 10 character");
      !this.$v.location.sPhoneNo.maxLength &&
        errors.push("Phone Number cannot be greater than 10 character");
      !this.$v.location.sPhoneNo.required &&
        errors.push("Phone Number is required.");
      return errors;
    },
    sFaxNoErrors() {
      const errors = [];
      if (!this.$v.location.sFaxNo.$dirty) return errors;
      !this.$v.location.sFaxNo.minLength &&
        errors.push("Fax Number must be at least 10 characters long");
      !this.$v.location.sFaxNo.maxLength &&
        errors.push("Fax Number must be at most 10 characters long");
      return errors;
    }
  },
  name: "EditLocation",
  data() {
    return {
      location: {
        nROIFacilityID: null,
        nFacilityID: null,
        nFacilityLocationID: null,
        sLocationCode: "",
        sLocationName: "",
        sLocationAddress: "",
        sPhoneNo: null,
        sFaxNo: null,
        sConfigLogoName: "",
        sConfigLogoData: "",
        sConfigBackgroundName: "",
        sConfigBackgroundData: "",
        sAuthTemplate: "",
        nROILocationID: "",
        sAuthTemplateName: ""
      }
    };
  },
  mounted() {
    // API to get single facility for Loading into the form
    this.$http
      .get(
        "FacilityLocations/GetFacilityLocationSingle/" + this.$route.params.id
      )
      .then(
        response => {
          // get body data
          this.location = JSON.parse(response.bodyText);
        },
        response => {
          // error callback
          this.gridData = response.body;
        }
      );
  },
  methods: {
    onPDFFileChanged(file) {
      if (file) {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sAuthTemplate = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sAuthTemplateName = file.name;
      }
    },
    onLogoFileChanged(file) {
      if (file) {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sConfigLogoData = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sConfigLogoName = file.name;
      }
    },
    onBackgroundFileChanged(file) {
      if (file) {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sConfigBackgroundData = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sConfigBackgroundName = file.name;
      }
    },
    // API to Update location
    onSubmit() {
      this.location.nROILocationID = parseInt(this.location.nROILocationID);
      this.location.nFacilityLocationID = parseInt(
        this.location.nFacilityLocationID
      );
      console.log(this.location);
      this.$http
        .post(
          "FacilityLocations/EditFacilityLocation/" +
            this.location.nFacilityLocationID,
          this.location
        )
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/Locations/" + this.location.nFacilityID);
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
  margin-left: 25px;
  margin-right: 25px;
  margin-bottom: 25px;
  margin-top: 15px;
}
button {
  margin-right: 25px;
}
.editlocation-form {
  border: 3px solid #eee;
  box-shadow: 0 4px 6px #ccc;
}
.input label {
  display: block;
  color: #4e4e4e;
}
.input.inline label {
  display: inline;
}
.input input {
  font: inherit;
  width: 100%;
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
.required:after {
  content: " *";
  color: red;
}
label {
  margin-top: 4px;
}
.row {
  margin: 1px;
}
</style>