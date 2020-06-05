<template>
  <div>
    <h1 style="text-align:center">Add Location</h1>
    <div class="addlocation-form">
      <form @submit.prevent="onSubmit">
        <div class="form-group row">
          <label class="col-md-4" for="sLocationName">Location Name:</label>
          <v-text-field
            type="text"
            id="sLocationName"
            placeholder="Enter Location Name"
            v-model="location.sLocationName"
            solo
          ></v-text-field>
          <label class="col-md-4" for="nLocationID">Location Id:</label>
          <v-text-field
            type="number"
            id="nLocationID"
            placeholder="Enter Location Id"
            v-model="location.nLocationID"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sLocationCode">Location Code:</label>
          <v-text-field
            type="text"
            id="sLocationCode"
            placeholder="Enter Location Code"
            v-model="location.sLocationCode"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sLocationAddress">Location Address:</label>
          <v-text-field
            type="text"
            id="sLocationAddress"
            placeholder="Enter Address"
            v-model="location.sLocationAddress"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sPhoneNo">Phone No:</label>
          <v-text-field
            type="text"
            id="nPhoneNo"
            placeholder="Enter Phone No"
            v-model="location.sPhoneNo"
            solo
          ></v-text-field>
          <label class="col-md-4" for="sFaxNo">Fax No:</label>
          <v-text-field
            type="text"
            id="nFaxNo"
            placeholder="Enter Fax No"
            v-model="location.sFaxNo"
            solo
          ></v-text-field>
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
            <v-btn type="submit" color="primary" >Save</v-btn>
            <v-btn :to="'/locations/'+this.$route.params.id" type="submit" color="primary" >Cancel</v-btn>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "AddLocation",
  data() {
    return {      
      location: {
        nROIFacilityID: parseInt(this.$route.params.id),
        nLocationID: null,
        sLocationCode: "",
        sLocationName: "",
        sLocationAddress: "",
        sPhoneNo: null,
        sFaxNo: null,
        sConfigLogoName: "",
        sConfigLogoData: "",
        sConfigBackgroundName: "",
        sConfigBackgroundData: ""
      }
    };
  },
  methods: {
    onLogoFileChanged(file) {
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
        this.location.nLocationID=parseInt( this.location.nLocationID);
        console.log(this.location)
      this.$http
        .post(
          "http://localhost:57364/api/FacilityLocation/AddFacilityLocation/",
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