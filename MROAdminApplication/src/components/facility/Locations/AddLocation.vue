<template>
  <div>
    <!-- <h1 style="text-align:center">Add Location</h1> -->
    <div class="addlocation-form">
      <form @submit.prevent="onSubmit">
        <div class="form-group row">
          <label class="col-md-4" for="sLocationName">Location Name:</label>
          <v-text-field
            type="text"
            id="sLocationName"
            placeholder="Enter Location Name"
            v-model="location.sLocationName"
            @input="$v.location.sLocationName.$touch()"
            @blur="$v.ocation.sLocationName.$touch()"
            :error-messages="sLocationNameErrors"
            solo
          ></v-text-field>
          <label class="col-md-4" for="nROILocationID">Location Id:</label>
          <v-text-field
            type="number"
            id="nROILocationID"
            placeholder="Enter Location Id"
            v-model="location.nROILocationID"
            @input="$v.location.nROILocationID.$touch()"
            @blur="$v.ocation.nROILocationID.$touch()"
            :error-messages="nROILocationIDErrors"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sLocationCode">Location Code:</label>
          <v-text-field
            type="text"
            id="sLocationCode"
            placeholder="Enter Location Code"
            v-model="location.sLocationCode"
            @input="$v.location.sLocationCode.$touch()"
            @blur="$v.ocation.sLocationCode.$touch()"
            :error-messages="sLocationCodeErrors"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sLocationAddress">Location Address:</label>
          <v-text-field
            type="text"
            id="sLocationAddress"
            placeholder="Enter Address"
            v-model="location.sLocationAddress"
            @input="$v.location.sLocationAddress.$touch()"
            @blur="$v.ocation.sLocationAddress.$touch()"
            :error-messages="sLocationAddressErrors"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sPhoneNo">Phone No:</label>
          <v-text-field
            type="text"
            id="nPhoneNo"
            placeholder="Enter Phone No"
            v-model="location.sPhoneNo"
            solo
            @input="$v.location.sPhoneNo.$touch()"
            @blur="$v.ocation.sPhoneNo.$touch()"
            :error-messages="sPhoneNoErrors"
          ></v-text-field>
          <label class="col-md-4" for="sFaxNo">Fax No:</label>
          <v-text-field
            type="text"
            id="nFaxNo"
            placeholder="Enter Fax No"
            v-model="location.sFaxNo"
            @input="$v.location.sFaxNo.$touch()"
            @blur="$v.ocation.sFaxNo.$touch()"
            :error-messages="sFaxNoErrors"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sAuthTemplate">Authorization Template:</label>
          <v-file-input
            class="col-md-2"
            label="Upload PDF"
            filled
            prepend-icon="mdi-pdf-box"
            accept=".pdf"
          ></v-file-input>
          <label class="col-md-4" for="sConfigFacilityLogo">Logo Image:</label>
          <v-file-input
            class="col-md-2"
            label="Logo Image"
            filled
            prepend-icon="mdi-camera"
            @change="onLogoFileChanged"
            accept="image/png, image/jpeg, image/bmp"
          ></v-file-input>
          <label class="col-md-4" for="sConfigBackgroundImg">Background Image:</label>
          <v-file-input
            class="col-md-2"
            label="Background Image"
            filled
            prepend-icon="mdi-camera"
            @change="onBackgroundFileChanged"
            accept="image/png, image/jpeg, image/bmp"
          ></v-file-input>
          <div class="col-md-6 offset-md-3 submit">
            <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
            <v-btn :to="'/locations/'+this.$route.params.id" type="submit" color="primary" >Cancel</v-btn>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { validationMixin } from 'vuelidate'
import { required, minLength,maxLength,numeric } from 'vuelidate/lib/validators'
export default {
  mixins: [validationMixin],
  validations: {
      location:{
          sLocationName: { required, maxLength: maxLength(25) },
          nROILocationID:{required,numeric},
          sLocationCode:{ required, maxLength: maxLength(20) },
          sLocationAddress:{ required,maxLength: maxLength(30) },
          sPhoneNo:{ required, maxLength: maxLength(10),minLength:minLength(10),numeric },
          sFaxNo:{required,maxLength: maxLength(10),minLength:minLength(10)},
      }
  },
  computed:
  {
    sLocationNameErrors(){
    const errors = [];
    if (!this.$v.location.sLocationName.$dirty) 
      return errors;
          // !this.$v.location.sLocationName.minLength && errors.push('Location Name must be at least 5 characters long')
          !this.$v.location.sLocationName.maxLength && errors.push('Location Name must be at most 25 characters long')
          !this.$v.location.sLocationName.required && errors.push('Location Name is required.')
          return errors;
    },
    nROILocationIDErrors(){
      const errors = [];
    if (!this.$v.location.nROILocationID.$dirty) 
      return errors;
          // !this.$v.location.sDescription.minLength && errors.push('Facility Description must be at least 5 characters long')
          !this.$v.location.nROILocationID.numeric && errors.push('Location Id Must be Numeric')
          !this.$v.location.nROILocationID.required && errors.push('Location Id is required.')
          return errors;
    },
    sLocationCodeErrors(){
      const errors = [];
    if (!this.$v.location.sLocationCode.$dirty) 
      return errors;
          // !this.$v.location.sLocationCode.minLength && errors.push('Location Code must be at least 5 characters long')
          !this.$v.location.sLocationCode.maxLength && errors.push('Location Code must be at most 20 characters long')
          !this.$v.location.sLocationCode.required && errors.push('Location Code is required.')
          return errors;
    },
    sLocationAddressErrors(){
      const errors = [];
      if (!this.$v.location.sLocationAddress.$dirty) 
      return errors
          // !this.$v.location.sLocationAddress.minLength && errors.push('Location Address must be at least 5 characters long')
          !this.$v.location.sLocationAddress.maxLength && errors.push('Location Address must be at most 30 characters long')
          !this.$v.location.sLocationAddress.required && errors.push('Location Address is required.')
          return errors;
    },
    sPhoneNoErrors(){
      const errors = [];

      if (!this.$v.location.sPhoneNo.$dirty) 
      return errors
          !this.$v.location.sPhoneNo.numeric && errors.push('Phone Number can only have numbers')
          !this.$v.location.sPhoneNo.minLength && errors.push('Phone Number cannot be less than 10 character')
          !this.$v.location.sPhoneNo.maxLength && errors.push('Phone Number cannot be greater than 10 character')
          !this.$v.location.sPhoneNo.required && errors.push('Phone Number is required.')
          return errors;
    },
    sFaxNoErrors(){
      const errors = [];
    if (!this.$v.location.sFaxNo.$dirty) 
      return errors;
          // !this.$v.location.sFaxNo.numeric && errors.push('Fax Number can only have numbers')
          !this.$v.location.sFaxNo.minLength && errors.push('Fax Number must be at least 10 characters long')
          !this.$v.location.sFaxNo.maxLength && errors.push('Fax Number must be at most 10 characters long')
          !this.$v.location.sFaxNo.required && errors.push('Fax No is required.')
          return errors;
    }
  },
  name: "AddLocation",
  data() {
    return {      
      location: {
        nFacilityID:parseInt(this.$route.params.id),
        nFacilityLocationID: 0,
        sLocationCode: "",
        sLocationAddress: "",
        sPhoneNo: null,
        sFaxNo: null,
        sConfigLogoName: "",
        sConfigLogoData: "",
        sConfigBackgroundName: "",
        sConfigBackgroundData: "",
        nROILocationID:"",
        sAuthTemplate:""
      }
    };
  },
  methods: {
    //         @change="onPDFFileChanged"

    // onLogoFileChanged(file) {
    //   if (file) {
    //     const reader = new FileReader();        
    //     reader.addEventListener("load",() => {
    //       this.location.sConfigLogoData=reader.result; //base64encoded string
    //     })
    //     reader.readAsDataURL(file);
    //     this.location.sConfigLogoName=file.name;
    //   }
    // },
    onPDFFileChanged(file) {
      if (file) {
        const reader = new FileReader();        
        reader.addEventListener("load",() => {
          this.location.sConfigLogoData=reader.result; //base64encoded string
        })
        reader.readAsDataURL(file);
        this.location.sConfigLogoName=file.name;
      }
    },
    onBackgroundFileChanged(file) {
      if (file) {
        const reader = new FileReader();        
        reader.addEventListener("load",() => {
          this.location.sConfigBackgroundData=reader.result; //base64encoded string
        })
        reader.readAsDataURL(file);
        this.location.sConfigLogoName=file.name;
      }
    },
    // API to add location
    onSubmit() {
        this.location.nROILocationID=parseInt( this.location.nROILocationID);
        console.log(this.location)
      this.$http
        .post(
          "http://localhost:57364/api/FacilityLocations/AddFacilityLocation/",
          this.location
        )
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/locations/"+parseInt(this.$route.params.id));
          }
        });
    }
  }
};
</script>

<style scoped>
* {
  margin: 10px;
}
.addlocation-form {
  width: 700px;
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
</style>