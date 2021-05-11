<template>
  <div>
    <div id="EditOrganizationsPageBox">
      <form @submit.prevent="onSubmit" class="editorganization-form">
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label class="required" for="sOrgName">Organization Name:</label>
            <v-text-field
              type="text"
              id="sOrgName"
              placeholder="Enter Organization Name"
              v-model="organization.sOrgName"
              @input="$v.organization.sOrgName.$touch()"
              @blur="$v.organization.sOrgName.$touch()"
              :error-messages="sOrgNameErrors"
              solo
            ></v-text-field>
            <label class="required" for="nROILocationID">ROI Location Id:</label>
            <v-text-field
              type="number"
              id="nROILocationID"
              placeholder="Enter Location Id"
              v-model="organization.nROILocationID"
              @input="$v.organization.nROILocationID.$touch()"
              @blur="$v.organization.nROILocationID.$touch()"
              :error-messages="nROILocationIDErrors"
              solo
              min="1"
            ></v-text-field>
            <label class="required" for="sLocationCode">Location Code:</label>
            <v-text-field
              type="text"
              id="sLocationCode"
              placeholder="Enter Location Code"
              v-model="organization.sLocationCode"
              @input="$v.organization.sLocationCode.$touch()"
              @blur="$v.organization.sLocationCode.$touch()"
              :error-messages="sLocationCodeErrors"
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
            <label for="sSupportEmail">Support Email:</label>
            <v-text-field
              type="text"
              id="sSupportEmail"
              placeholder="Enter Support Email"
              v-model="organization.sSupportEmail"
              @input="$v.organization.sSupportEmail.$touch()"
              @blur="$v.organization.sSupportEmail.$touch()"
              :error-messages="sSupportEmailErrors"
              solo
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="5">
            <label for="sConfigFacilityLogo">Logo Image:</label>
            <v-img :src="organization.sConfigLogoData" width="20%"></v-img>
            <br />
            <v-file-input
            ref="sLogoImage"
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
              :placeholder="organization.sConfigLogoName"
              accept="image/png, image/jpeg, image/bmp"
            >
              <v-tooltip slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="rgb(0, 91, 168)" top>mdi-information</v-icon>
                </template>
                <span>Please upload logo with height 50px/0.375em</span>
              </v-tooltip>
            </v-file-input>
            <label for="sConfigBackgroundImg">Background Image:</label>
            <v-img :src="organization.sConfigBackgroundData" width="20%"></v-img>
            <br />
            <v-file-input
            ref="sBGImage"
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
              :placeholder="organization.sConfigBackgroundName"
              accept="image/png, image/jpeg, image/bmp"
            >
              <v-tooltip slot="append" top>
                <template v-slot:activator="{ on }">
                  <v-icon v-on="on" color="rgb(0, 91, 168)" top>mdi-information</v-icon>
                </template>
                <span>Please upload logo with height 50px/0.375em</span>
              </v-tooltip>
            </v-file-input>      
            <v-row>
              <v-col cols="6" md="6">              
                <label  for="Primary Timeout">Primary Timeout:</label>
                <v-text-field
                  type="number"
                  id="nPrimaryTimeout"
                  placeholder="Primary Timeout (In Seconds)"
                  v-model="organization.nPrimaryTimeout"
                  @input="$v.organization.nPrimaryTimeout.$touch()"
                  @blur="$v.organization.nPrimaryTimeout.$touch()"
                  :error-messages="nPrimaryTimeoutErrors"
                  solo
                ></v-text-field>
              </v-col>
              <v-col cols="6" md="6">              
                <label for="Secondary Timeout">Secondary Timeout:</label>
                <v-text-field
                  type="number"
                  id="nSecondaryTimeout"
                  placeholder="Secondary Timeout (In Seconds)"
                  v-model="organization.nSecondaryTimeout"
                  @input="$v.organization.nSecondaryTimeout.$touch()"
                  @blur="$v.organization.nSecondaryTimeout.$touch()"
                  :error-messages="nSecondaryTimeoutErrors"
                  solo
                ></v-text-field>
              </v-col>
            </v-row>  
             <v-row> 
            <v-col cols="6" md="6"> 
              <v-btn :to="'/locations/'+this.organization.nFacilityID" type="submit" color="primary">View Location List</v-btn>
            </v-col>
            <v-col cols="6" md="6">
              <v-select
            v-model="value"
            :items="items"
            label="Please Select"
            chips
            multiple
            solo
          ></v-select>
            </v-col>  
            </v-row>     
          </v-col>          
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn :to="'/organizations/'+this.organization.nFacilityID" type="button" color="primary">Cancel</v-btn>
        </div>
        <br />
      </form>
    </div>
    <v-dialog v-model="sameOrgNameAlert" width="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>{{sameNameText}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="sameOrgNameAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Common Loader -->
        <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="rgb(0, 91, 168)" dark>
            <v-card-text>
              Please stand by
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
    <!-- Logo Clearer -->
    <v-dialog v-model="LogoClearer" width="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Select JPG/JPEG/BMP/PNG File Only</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="clearLogoField()">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Background Clearer -->
    <v-dialog v-model="BGClearer" width="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Select JPG/JPEG/BMP/PNG File Only</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="clearBGField()">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog Alert for Common errors  -->
    <v-dialog v-model="errorAlert" width="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Warning</v-card-title>
        <v-card-text>{{errorMessage}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="errorAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import MY_JSON from '../../../assets/json/data.json'
import { validationMixin } from "vuelidate";
import {
  required,
  minLength,
  maxLength,
  numeric,
  email
} from "vuelidate/lib/validators";
export default {
    //custom option named myJson
  myJson: MY_JSON,
  mixins: [validationMixin],
  validations: {
    organization: {
      sOrgName: {
        required,
        maxLength: maxLength(100),
        minLength: minLength(2)
      },
      nROILocationID: { required, numeric },
      sLocationCode: {
        required,
        maxLength: maxLength(8),
        minLength: minLength(2)
      },  
      nPrimaryTimeout: { numeric },
      nSecondaryTimeout: { numeric },
      sSupportEmail: { email, maxLength: maxLength(150) },
    }
  },
  computed: {
    sOrgNameErrors() {
      const errors = [];
      if (!this.$v.organization.sOrgName.$dirty) return errors;
      !this.$v.organization.sOrgName.minLength &&
        errors.push("Organization Name must be at least 2 characters long");
      !this.$v.organization.sOrgName.maxLength &&
        errors.push("Organization Name must be at most 100 characters long");
      !this.$v.organization.sOrgName.required &&
        errors.push("Organization Name is required.");
      return errors;
    },
    nROILocationIDErrors() {
      const errors = [];
      if (!this.$v.organization.nROILocationID.$dirty) return errors;
      !this.$v.organization.nROILocationID.numeric &&
        errors.push("Location Id Must be Numeric");
      !this.$v.organization.nROILocationID.required &&
        errors.push("Location Id is required.");
      return errors;
    },
    sLocationCodeErrors() {
      const errors = [];
      if (!this.$v.organization.sLocationCode.$dirty) return errors;
      !this.$v.organization.sLocationCode.minLength &&
        errors.push("Location Code must be at least 2 characters long");
      !this.$v.organization.sLocationCode.maxLength &&
        errors.push("Location Code must be at most 8 characters long");
      !this.$v.organization.sLocationCode.required &&
        errors.push("Location Code is required.");
      return errors;
    },
    nPrimaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.organization.nPrimaryTimeout.$dirty) return errors;
      !this.$v.organization.nPrimaryTimeout.numeric &&
        errors.push("Primary Timeout Must be Numeric");
      return errors;
    },
    nSecondaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.organization.nSecondaryTimeout.$dirty) return errors;
      !this.$v.organization.nSecondaryTimeout.numeric &&
        errors.push("Secondary Timeout Must be Numeric");
      return errors;
    },
    sSupportEmailErrors() {
      const errors = [];
      if (!this.$v.organization.sSupportEmail.$dirty) return errors;
       !this.$v.organization.sSupportEmail.maxLength &&
        errors.push("Support Email must be at most 150 characters long");
      !this.$v.organization.sSupportEmail.email &&
        errors.push("Please provide a proper Email ID");
      return errors;
    },
  },
  name: "EditOrganization",
  data() {
    return {
      sameNameText:'',
      sameOrgNameAlert: false,
      BGClearer:false,
      LogoClearer:false,
      PDFClearer: false,
      dialogLoader:false,
      authDocNotAdded:false,
      authDocErrorsText:'',
      authDocErrors:false,
      previewPDFDialog:false,
      previewPDFPage:0,
      src: undefined,
      numPages: undefined,
      sBtnCode:'',
      sGUID: '',
      errorAlert:false,
      errorMessage:'',
      organization: {
        nFacilityID: null,
        nFacilityOrgID: null,
        sLocationCode: "",
        sOrgName: "",
        sConfigLogoName: "",
        sConfigLogoData: "",
        sConfigBackgroundName: "",
        sConfigBackgroundData: "",
        nROILocationID: "",
        nPrimaryTimeout:"",
        nSecondaryTimeout:"",
        nCreatedAdminUserID: this.$store.state.adminUserId,
        nUpdatedAdminUserID: this.$store.state.adminUserId
      },
      nFacilityID:0,
      gridData_Loc: this.getGridData(),
      items: [],
      value: [],
    };
  },
  mounted() {
    // API to get single facility for Loading into the form
    this.dialogLoader =true;
    this.$http
      .get(
        "FacilityOrganizations/GetFacilityOrganizationSingle/sFacilityOrgID=" + this.$route.params.id+"&sAdminUserID="+this.$store.state.adminUserId
      )
      .then(
        response => {
          // get body data
          this.organization = JSON.parse(response.bodyText);
          this.nFacilityID = this.organization.nFacilityID;
          this.dialogLoader =false;    
        },
        response => {
          // error callback
          this.gridData = response.body;          
        }
      ); 
      
    //Get btn code for organization and guid
    this.$http.get("FacilityOrganizations/GetBtnCodeAndGUID/"+this.$route.params.id)
    .then(
      response=>{
        if(response.ok==true){        
        this.sGUID = response.body.sFacilityOrgURL;
        this.sBtnCode = response.body.sFacilityOrgButtonHTMLCode;
      }},
      error=>{        
        if (error.status == 400) {
            this.dialogLoader =false;
            this.errorMessage='Error - Something went wrong while getting GUID.';
            this.errorAlert=true;   
          }      
      }
    );  

    //Get btn code for organization and guid
    // this.$http
    //     .get(
    //       "FacilityLocations/GetFacilityLocationByFacilityIDForOrg/sFacilityID=" 
    //         + this.organization.nFacilityID+"&sAdminUserID="+this.$store.state.adminUserId + "&sFacilityOrgID="+this.$route.params.id
    //     )
    //     .then(
    //       response => {
    //         this.gridData_Loc = JSON.parse(response.bodyText)["locations"];
    //         this.dialogLoader = false;
    //         // const result = words.filter(word => word.length > 6);
    //         this.items = this.gridData_Loc.map(({ sLocationName }) => sLocationName);
    //         //this.value = this.gridData.map(({ sFacilityLocationID }) => sFacilityLocationID); 
    //       },
    //       response => {
    //         // error callback
    //         this.gridData_Loc = response.body;
    //       }
    //     );
    
  },
  methods: {
      getGridData() {
      //this.dialogLoader = true;
      // this.$http
      //   .get(
      //     "FacilityLocations/GetFacilityLocationByFacilityIDForOrg/sFacilityID=" 
      //       + parseInt(this.organization.nFacilityID)+"&sAdminUserID="+this.$store.state.adminUserId + "&sFacilityOrgID="+this.$route.params.id
      //   )
      //   .then(
      //     response => {
      //       this.gridData_Loc = JSON.parse(response.bodyText)["locations"];
      //       this.dialogLoader = false;
      //       // const result = words.filter(word => word.length > 6);
      //       this.items = this.gridData_Loc.map(({ sLocationName }) => sLocationName);
      //       //this.value = this.gridData.map(({ sFacilityLocationID }) => sFacilityLocationID); 
      //     },
      //     response => {
      //       // error callback
      //       this.gridData_Loc = response.body;
      //     }
      //   );
    },
    clearBGField() {
      this.BGClearer = false;
      this.organization.sConfigBackgroundName = "";
      this.organization.sConfigBackgroundData = "";
      this.$refs.sBGImage.clearableCallback();
    },
    clearLogoField() {
      this.LogoClearer = false;
      this.organization.sConfigLogoName = "";
      this.organization.sConfigLogoData = "";
      this.$refs.sLogoImage.clearableCallback();
    },
    onLogoFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "bmp"){
          const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.organization.sConfigLogoData = reader.result;
        });
        reader.readAsDataURL(file);
        this.organization.sConfigLogoName = file.name;
        }
        else {
          this.LogoClearer = true;
        }
      } else {
        this.organization.sConfigLogoName = "";
        this.organization.sConfigLogoData = "";
      }
    },
    onBackgroundFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "bmp"){
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.organization.sConfigBackgroundData = reader.result;
        });
        reader.readAsDataURL(file);
        this.organization.sConfigBackgroundName = file.name;
        }
        else{
          this.BGClearer=true;
        }
      } else {
        this.organization.sConfigBackgroundName = "";
        this.organization.sConfigBackgroundData = "";
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
    // API to Update organization
    onSubmit() {
      this.organization.nUpdatedAdminUserID=this.$store.state.adminUserId;
      this.dialogLoader =true;
      this.organization.nROILocationID = parseInt(this.organization.nROILocationID);
      this.organization.nFacilityOrgID = parseInt(
        this.organization.nFacilityOrgID
      );
      this.organization.nPrimaryTimeout= this.organization.nPrimaryTimeout==''? 0 :this.organization.nPrimaryTimeout;
      this.organization.nSecondaryTimeout= this.organization.nSecondaryTimeout==''? 0 :this.organization.nSecondaryTimeout; 
      this.$http
        .post(
          "FacilityOrganizations/EditFacilityOrganization/" +
            this.organization.nFacilityOrgID,
          this.organization
        )
        .then(
          response => {
          if (response.ok == true) {
            this.dialogLoader =false;
            if (response.bodyText != "") {
                this.dialogLoader = false;
                this.authDocErrorsText = response.bodyText;
                this.authDocErrors = true;
              } else {
                this.dialogLoader = false;
                this.$router.push(
                  "/organizations/" + parseInt(this.organization.nFacilityID)
                );
              }
          }
          
        },error => {
            // Error Callback
            if (
              error.status == 400
            ) {
              this.sameNameText =error.bodyText;
              this.dialogLoader = false;
               this.sameOrgNameAlert = true;
            }
          }
        );
    
    }

  }
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditOrganizationsPageBox {
    margin: 0 0em;
  }
  h1 {
    font-size: 14px;
  }
}
.submit {
  text-align: center;
}

.editorganization-form {
  border: 0.1875em solid #eee;
  box-shadow: 0 0.25em 0.375em #ccc;
}
.input label {
  display: block;
  color: #4e4e4e;
  color: #4e4e4e;
}

.submit button {
  font: inherit;
  cursor: pointer;
}

.submit button[disabled],
.submit button[disabled]:hover,
.submit button[disabled]:active {
  border: 0.0625em solid #ccc;
  background-color: transparent;
  color: #ccc;
  cursor: not-allowed;
}
.required:after {
  content: " *";
  color: red;
}
label {
  margin-top: 0.0625em;
}
.row {
  margin: 0.0625em;
}
button{
  margin-right: 1em;
}
.scroll{
  height: 80vh;
  overflow-y:auto;
}
</style>