<template>
  <div>
    <div id="box">
      <form @submit.prevent="onSubmit" class="addlocation-form">
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
            ></v-file-input>
            <label for="sConfigFacilityLogo">Logo Image:</label>
            <div v-show="location.sConfigLogoName!=''">
            <v-img :src="location.sConfigLogoData" width="20%"></v-img>
            </div>
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
            <div v-show="location.sConfigBackgroundName!=''">
            <v-img :src="location.sConfigBackgroundData" width="20%"></v-img>
            </div>
            <br />
            <v-file-input
              chips
              show-size
              dense
              hint="Upload Background Image"
              rounded
              label="Background Image"
              filled
              prepend-icon="mdi-camera"
              @change="onBackgroundFileChanged"
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
    <!-- Dialog Alert for Auth Doc Not added -->
    <v-dialog v-model="authDocNotAdded" width ="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Alert</v-card-title>
        <v-card-text>Authorization PDF not added, Location can't be activated</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="authDocNotAdded = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog Alert for Auth Doc errors -->
    <v-dialog v-model="authDocErrors" width ="425px" max-width="425px">
      <v-card>
        <v-card-title class="headline">PDF missing following fields</v-card-title>
        <v-card-text v-html="this.authDocErrorsText"></v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="authDocErrorsContinue()">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog Alert for Same name facility -->
    <v-dialog v-model="sameLocNameAlert" width ="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Location with Same Name Exists</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="sameLocNameAlert = false">Ok</v-btn>
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
      !this.$v.location.sFaxNo.numeric &&
        errors.push("Fax Number can only have numbers");
      !this.$v.location.sFaxNo.minLength &&
        errors.push("Fax Number must be at least 10 characters long");
      !this.$v.location.sFaxNo.maxLength &&
        errors.push("Fax Number must be at most 10 characters long");
      return errors;
    }
  },
  name: "AddLocation",
  data() {
    return {
      authDocNotAdded:false,
      authDocErrorsText:'',
      authDocErrors:false,
      sameLocNameAlert:false,
      location: {
        nFacilityID: parseInt(this.$route.params.id),
        nFacilityLocationID: 0,
        sLocationName: "",
        sLocationCode: "",
        sLocationAddress: "",
        sPhoneNo: null,
        sFaxNo: null,
        sConfigLogoName: "",
        sConfigLogoData: "",
        sConfigBackgroundName: "",
        sConfigBackgroundData: "",
        nROILocationID: "",
        sAuthTemplate: "",
        sAuthTemplateName: ""
      }
    };
  },
  mounted() {
    this.$http
      .get("FacilityLocations/GetROILocationID/" + this.$route.params.id)
      .then(resp => {
        if (resp.ok == true) {
          this.location.nROILocationID = resp.body;
          this.location.nROILocationID++;
        }
      });
  },
  methods: {
    authDocErrorsContinue(){
      this.authDocErrors = false;
      this.$router.push("/locations/" + parseInt(this.$route.params.id));
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
      else{
        this.location.sConfigLogoName='';
        this.location.sConfigLogoData='';
      }
    },
    onPDFFileChanged(file) {
      var file_name_array = file.name.split(".");
      var file_extension = file_name_array[file_name_array.length - 1];
      if(file_extension=="pdf")
      {
        if (file) {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sAuthTemplate = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sAuthTemplateName = file.name;
      }
      }
      else{
        alert("Please Select PDF File");
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
      else{
        this.location.sConfigBackgroundName='';
        this.location.sConfigBackgroundData='';
      }
    },
    // API Call to add Location
    onSubmit() {
      if(this.location.sAuthTemplate==""){
        this.authDocNotAdded=true;
      }
      this.location.nROILocationID = parseInt(this.location.nROILocationID);
      console.log(this.location);
      this.$http
        .post("FacilityLocations/AddFacilityLocation/", this.location)
        .then(response => {
          if (response.ok == true) {
            if(response.bodyText!=""){
              this.authDocErrorsText=response.bodyText;
              this.authDocErrors=true;
            }
            else{
              this.$router.push("/locations/" + parseInt(this.$route.params.id));
            }
          }
        },error => {
          // Error Callback
          if(error.status==400&&error.bodyText=="Cannot Add Location with Same Name"){
          this.sameLocNameAlert = true;
        }
        }
        );
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
.addlocation-form {
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
</style>