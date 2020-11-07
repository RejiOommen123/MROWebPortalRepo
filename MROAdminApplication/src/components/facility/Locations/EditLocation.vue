<template>
  <div>
    <div id="EditLocationsPageBox">
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
            <label for="bForceCompliance">Apply force Compliance:</label>
            <v-btn-toggle
                v-model="location.bForceCompliance"
                mandatory
              >
                <v-btn :color="location.bForceCompliance==='true' ? 'green' : 'none'" value="true">
                  Yes
                </v-btn>
                <v-btn :color="location.bForceCompliance==='null' ? 'primary' : 'none'" value="null">
                  Default
                </v-btn>
                <v-btn :color="location.bForceCompliance==='false' ? 'red' : 'none'" value="false">
                  NO
                </v-btn>
            </v-btn-toggle>
          </v-col>
          <v-col cols="12" md="5">
            <label for="sAuthTemplate">Authorization Template:
               <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                  <v-btn icon :disabled="location.sAuthTemplate=='' || location.sAuthTemplate==null" v-bind="attrs" v-on="on" class="ma-0 pa-0">
                    <v-icon
                      @click="previewPDF(0)"
                      small
                      color="rgb(0,91,168)"
                      v-bind="attrs"
                      v-on="on"
                    >mdi-file</v-icon>
                  </v-btn>
                </template>
                <span>Preview PDF</span>
              </v-tooltip>
            </label>
            <v-file-input
            ref="sPDFFile"
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
              :placeholder="location.sConfigLogoName"
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
            <v-img :src="location.sConfigBackgroundData" width="20%"></v-img>
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
              :placeholder="location.sConfigBackgroundName"
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
                  v-model="location.nPrimaryTimeout"
                  @input="$v.location.nPrimaryTimeout.$touch()"
                  @blur="$v.location.nPrimaryTimeout.$touch()"
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
                  v-model="location.nSecondaryTimeout"
                  @input="$v.location.nSecondaryTimeout.$touch()"
                  @blur="$v.location.nSecondaryTimeout.$touch()"
                  :error-messages="nSecondaryTimeoutErrors"
                  solo
                ></v-text-field>
              </v-col>
            </v-row>      
          </v-col>          
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="this.$v.$invalid">Save</v-btn>
          <v-btn :to="'/locations/'+this.location.nFacilityID" type="button" color="primary">Cancel</v-btn>
        </div>
        <br />
      </form>
    </div>
    <v-dialog v-model="sameLocNameAlert" width="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>{{sameNameText}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="sameLocNameAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Dialog Alert for Auth Doc errors -->
    <v-dialog v-model="authDocErrors" width="425px" max-width="425px">
      <v-card>
        <v-card-title class="headline">PDF missing following fields</v-card-title>
        <v-card-text v-html="this.authDocErrorsText"></v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="authDocErrorsContinue()">Ok</v-btn>
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
         <!-- PDF Clearer -->
    <v-dialog v-model="PDFClearer" width="360px" max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Select PDF File Only</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="clearPDFField()">Ok</v-btn>
        </v-card-actions>
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
    <!-- PDF Preview Dialog -->
    <v-dialog
      v-model="previewPDFDialog"
      max-width="80%"
      content-class="pdfPreviewScroll"
    >
      <v-card>      
          
        <v-card-title class="headline justify-center">
          <v-btn
            color="green darken-1"
            text
            @click="previousPage()"
            :disabled="previewPDFPage==0"
          >
            Previous
          </v-btn>
          <v-spacer></v-spacer>
          <h3>Requester Sample Data</h3>
           <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="nextPage()"
            :disabled="previewPDFPage==4"
          >
            Next
          </v-btn>          
            <v-btn style="font-size:36px;color:black"  icon  @click="previewPDFDialog=false">
                      <v-icon>mdi-close</v-icon>
                    </v-btn> 
          </v-card-title>       

        <v-card-text class="scroll">
        <pdf          
          v-for="i in numPages"
          :key="i"
          :src="src"
          :page="i"
          class="pdfViewer"
        ></pdf>
        </v-card-text>

        <v-card-actions>        
          <v-spacer></v-spacer>
          Requester Sample Data - {{previewPDFPage+1}}/5
        
        </v-card-actions>
      </v-card>
    </v-dialog>


  </div>
</template>

<script>
import MY_JSON from '../../../assets/json/data.json'
import pdf from "vue-pdf";
import { validationMixin } from "vuelidate";
import {
  required,
  minLength,
  maxLength,
  numeric
} from "vuelidate/lib/validators";
export default {
    //custom option named myJson
  myJson: MY_JSON,
  components:{
    pdf
  },
  mixins: [validationMixin],
  validations: {
    location: {
      sLocationName: {
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
      sLocationAddress: { required, maxLength: maxLength(1000) },
      sPhoneNo: {
        required,
        maxLength: maxLength(10),
        minLength: minLength(10),
        numeric
      },
      sFaxNo: { maxLength: maxLength(10), minLength: minLength(10), numeric },      
      nPrimaryTimeout: { numeric },
      nSecondaryTimeout: { numeric },
    }
  },
  computed: {
    sLocationNameErrors() {
      const errors = [];
      if (!this.$v.location.sLocationName.$dirty) return errors;
      !this.$v.location.sLocationName.minLength &&
        errors.push("Location Name must be at least 2 characters long");
      !this.$v.location.sLocationName.maxLength &&
        errors.push("Location Name must be at most 100 characters long");
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
        errors.push("Location Code must be at least 2 characters long");
      !this.$v.location.sLocationCode.maxLength &&
        errors.push("Location Code must be at most 8 characters long");
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
    },
    nPrimaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.location.nPrimaryTimeout.$dirty) return errors;
      !this.$v.location.nPrimaryTimeout.numeric &&
        errors.push("Primary Timeout Must be Numeric");
      return errors;
    },
    nSecondaryTimeoutErrors() {
      const errors = [];
      if (!this.$v.location.nSecondaryTimeout.$dirty) return errors;
      !this.$v.location.nSecondaryTimeout.numeric &&
        errors.push("Secondary Timeout Must be Numeric");
      return errors;
    }
  },
  name: "EditLocation",
  data() {
    return {
      sameNameText:'',
      sameLocNameAlert: false,
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
        sAuthTemplateName: "",
        nPrimaryTimeout:"",
        nSecondaryTimeout:"",
        bForceCompliance:"null",
        nCreatedAdminUserID: this.$store.state.adminUserId,
        nUpdatedAdminUserID: this.$store.state.adminUserId
      }
    };
  },
  mounted() {
    // API to get single facility for Loading into the form
    this.dialogLoader =true;
    this.$http
      .get(
        "FacilityLocations/GetFacilityLocationSingle/sFacilitylocationID=" + this.$route.params.id+"&sAdminUserID="+this.$store.state.adminUserId
      )
      .then(
        response => {
          // get body data
          this.location = JSON.parse(response.bodyText);
          this.dialogLoader =false;    
          switch(this.location.bForceCompliance){
          case true:
            this.location.bForceCompliance="true";
            break;
          case false:
            this.location.bForceCompliance="false";
            break;
          case null:
            this.location.bForceCompliance="null";
            break;
        }
        },
        response => {
          // error callback
          this.gridData = response.body;          
        }
      ); 
      
    //Get btn code for location and guid
    this.$http.get("FacilityLocations/GetBtnCodeAndGUID/"+this.$route.params.id)
    .then(
      response=>{
        if(response.ok==true){        
        this.sGUID = response.body.sFacilityLocationURL;
        this.sBtnCode = response.body.sFacilityLocationButtonHTMLCode;
      }},
      error=>{        
        if (error.status == 400) {
            this.dialogLoader =false;
            this.errorMessage='Error - Something went wrong while getting GUID.';
            this.errorAlert=true;   
          }      
      }
    );  
    
  },
  methods: {
    clearBGField() {
      this.BGClearer = false;
      this.location.sConfigBackgroundName = "";
      this.location.sConfigBackgroundData = "";
      this.$refs.sBGImage.clearableCallback();
    },
    clearLogoField() {
      this.LogoClearer = false;
      this.location.sConfigLogoName = "";
      this.location.sConfigLogoData = "";
      this.$refs.sLogoImage.clearableCallback();
    },
    clearPDFField() {
      this.PDFClearer = false;
      this.location.sAuthTemplate = "";
      this.location.sAuthTemplateName = "";
      this.$refs.sPDFFile.clearableCallback();
    },
    authDocErrorsContinue() {
      this.authDocErrors = false;
      this.$router.push("/locations/" + parseInt(this.location.nFacilityID));
    },
    onLogoFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "bmp"){
          const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sConfigLogoData = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sConfigLogoName = file.name;
        }
        else {
          this.LogoClearer = true;
        }
      } else {
        this.location.sConfigLogoName = "";
        this.location.sConfigLogoData = "";
      }
    },
    onPDFFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if (file_extension == "pdf") {
          const reader = new FileReader();
          reader.addEventListener("load", () => {
            this.location.sAuthTemplate = reader.result;
          });
          reader.readAsDataURL(file);
          this.location.sAuthTemplateName = file.name;
        } else {
          this.PDFClearer = true;
        }
      } else {
        this.location.sAuthTemplate = "";
        this.location.sAuthTemplateName = "";
      }
    },
    onBackgroundFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "bmp"){
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.location.sConfigBackgroundData = reader.result;
        });
        reader.readAsDataURL(file);
        this.location.sConfigBackgroundName = file.name;
        }
        else{
          this.BGClearer=true;
        }
      } else {
        this.location.sConfigBackgroundName = "";
        this.location.sConfigBackgroundData = "";
      }
    },
     previousPage(){
      this.previewPDFPage=this.previewPDFPage-1;
      this.previewPDF(this.previewPDFPage);
    },
    nextPage(){
      this.previewPDFPage=this.previewPDFPage+1;
      this.previewPDF(this.previewPDFPage);
    },
    previewPDF(pageNo){
      this.previewPDFPage=pageNo;
      this.dialogLoader=true;
      var combinedObj = {
        oRequester: this.$options.myJson[pageNo],
        bIsTestRequest: true,
        sAuthTemplate: this.location.sAuthTemplate,
      };           
      this.$http
        .post("wizards/GeneratePDF/", 
        combinedObj,
        {
          responseType: "arraybuffer"
        })
        .then(response => {
          let blobFile = new Blob([response.data], { type: "application/pdf" });
          var fileURL = URL.createObjectURL(blobFile);
          // this.pdfURL = fileURL;
          this.src = pdf.createLoadingTask(fileURL);
          this.src.promise.then(pdf => {

              this.numPages = pdf.numPages;
              this.dialogLoader=false;
              this.previewPDFDialog=true;   
            });          
        });
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
    // API to Update location
    onSubmit() {
      this.location.nUpdatedAdminUserID=this.$store.state.adminUserId;
      this.dialogLoader =true;
      this.location.nROILocationID = parseInt(this.location.nROILocationID);
      this.location.nFacilityLocationID = parseInt(
        this.location.nFacilityLocationID
      );
      this.location.nPrimaryTimeout= this.location.nPrimaryTimeout==''? 0 :this.location.nPrimaryTimeout;
      this.location.nSecondaryTimeout= this.location.nSecondaryTimeout==''? 0 :this.location.nSecondaryTimeout; 
      switch(this.location.bForceCompliance){
        case "true":
          this.location.bForceCompliance=true;
          break;
        case "false":
          this.location.bForceCompliance=false;
          break;
        case "null":
          this.location.bForceCompliance=null;
          break;
      }
      this.$http
        .post(
          "FacilityLocations/EditFacilityLocation/" +
            this.location.nFacilityLocationID,
          this.location
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
                  "/locations/" + parseInt(this.location.nFacilityID)
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
               this.sameLocNameAlert = true;
            }
          }
        );
    
    }

  }
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditLocationsPageBox {
    margin: 0 0em;
  }
  h1 {
    font-size: 14px;
  }
}
.submit {
  text-align: center;
}

.editlocation-form {
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