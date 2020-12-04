<template>
  <div class="center">
    <h1>Are you requesting records for yourself?</h1>
    <v-row>
      <div style="width:100%">
        <v-col cols="12" offset-sm="1" sm="10">
          <button
            :class="{active: sActiveBtn === 'Yes'}"
            @click.prevent="setPatient"
            class="wizardSelectionButton"
          >Yes, I want my medical records.</button>
        </v-col>
      </div>
      <div style="width:100%">
        <v-col cols="12" offset-sm="1" sm="10">
          <button 
            :class="{active: sActiveBtn === 'No'}"
            @click.prevent="setNotPatient" class="wizardSelectionButton">
            No, I am requesting medical records for someone else (child, dependent, decedent, etc.).
          </button>
        </v-col>
      </div>
    </v-row>
    <!-- Below html will render only if user select not patient option -->
    <div v-show="!bAreYouPatient">
      <form>
        <v-row>
          <v-col cols="12" offset-sm="2" sm="8">
              <label>
              {{disclaimer04}}
              </label>
          </v-col>
          <v-col cols="6" offset-sm="2" sm="4">
            <v-text-field 
              v-model="sRelativeFirstName"
              :error-messages="sRelativeFirstNameErrors"
              label="First Name"
              required
              maxlength="30"
              @input="$v.sRelativeFirstName.$touch()"
              @blur="$v.sRelativeFirstName.$touch()"
            ></v-text-field>
          </v-col>
            <v-col cols="6"  sm="4">
            <v-text-field 
              v-model="sRelativeLastName"
              :error-messages="sRelativeLastNameErrors"
              label="Last Name"
              required
              maxlength="30"
              @input="$v.sRelativeLastName.$touch()"
              @blur="$v.sRelativeLastName.$touch()"
            ></v-text-field>
            </v-col>
            <v-col cols="12" offset-sm="2" sm="8">
            <div v-if="!bAreYouPatient && disclaimer02!='' " class="disclaimer">{{this.disclaimer02}}</div>
            </v-col>
            <v-col cols="12" offset-sm="2" sm="8">
              <label>
              {{disclaimer03}}
              </label>
            </v-col>
            <template>
              <!-- Get all Patient Representative associated to facility and displayed as checkbox for selection-->
              <v-layout
                v-for="patientRepresentative in oPatientRepresentativeArray"
                :key="patientRepresentative.sNormalizedPatientRepresentativeName"
                row
                wrap
                style="width:100%"
              >
                <v-col cols="12" offset-sm="2" sm="8">
                  <v-checkbox
                    hide-details
                    dark
                    class="checkboxBorder"
                    :label="patientRepresentative.sPatientRepresentativeName"
                    color="white"
                    :value="patientRepresentative.sNormalizedPatientRepresentativeName"
                    v-model="sSelectedPatientRepresentatives"
                    @change="check(patientRepresentative)"
                  >
                  <!-- This for 'i' button to give disclaimers/info about option -->
                    <v-tooltip v-if="patientRepresentative.sFieldToolTip" slot="append" left>
                      <template v-slot:activator="{ on }">
                        <v-icon v-on="on" color="white" top>mdi-information</v-icon>
                      </template>
                      <v-col cols="12" sm="12">
                        <p style="width:200px; background-color:white;color:black">{{patientRepresentative.sFieldToolTip}}</p>
                      </v-col>
                    </v-tooltip>
                  </v-checkbox>
                </v-col>
              </v-layout>
              <!-- If requester selects other Patient Representative then free text box will appear to enter data -->
              <v-col cols="12" offset-sm="3" sm="6">
                <div v-if="this.bOther==true">
                  <v-text-field 
                  v-model="sOtherPatientRepresentatives" 
                  :error-messages="sOtherPatientRepresentativesErrors"
                  required
                  maxlength="50"
                  @input="$v.sOtherPatientRepresentatives.$touch()"
                  @blur="$v.sOtherPatientRepresentatives.$touch()"
                  label="Other Relation"
                  ></v-text-field>
                </div>
              </v-col>
            </template>
            <v-col v-if="MRORelationMultipleDocument"  cols="12" offset-sm="2" sm="8">
              <template>
              <v-file-input
                ref="file"
                multiple
                @change="filesChange"
                v-model="files"
                accept="image/*, application/pdf"
                placeholder="Upload your documents"
                label="File input"
                counter="3"
                show-size
                prepend-icon="mdi-paperclip"
                :error-messages="filesErrors"
                @input="$v.files.$touch()"
                @blur="$v.files.$touch()"
              >
                <template v-slot:selection="{ text }">
                  <v-chip
                    small
                    label
                    color="primary"
                  >
                    {{ text }}
                  </v-chip>
                </template>
                <v-tooltip slot="append" top>
                  <template v-slot:activator="{ on }">
                    <v-icon style="cursor:pointer" v-on="on" color="white" top>mdi-information</v-icon>
                  </template>
                  <span >You can upload upto 3 image or pdf files per 10 MB.</span>
                </v-tooltip>
              </v-file-input>
            </template>
            </v-col>
            <v-col cols="12" offset-sm="3" sm="6">
            <v-btn
              v-if="sSelectedPatientRepresentatives[0]=='MRORelationshipOther'"
              @click.prevent="continueAhead"
              :disabled="$v.$invalid"
              class="mr-4 next"
            >Continue</v-btn>
            <v-btn
              v-else
              @click.prevent="continueAhead"
              :disabled="$v.sRelativeFirstName.$invalid || $v.sRelativeLastName.$invalid || sSelectedPatientRepresentatives.length==0|| $v.files.$invalid "
              class="mr-4 next"
            >Continue</v-btn>
          </v-col>
        </v-row>
        <v-dialog
            v-model="dialog"
            max-width="290"
            light
          >
        <v-card>
          <v-card-text style="padding-top:20px">
            You have successfully uploaded the necessary supporting documents to verify your legal relationship to the patient.
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="green darken-1"
              text
              @click="dialog = false"
            >
              Ok
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      </form>
    </div>
    <div v-if="bAreYouPatient && disclaimer01!=null " class="disclaimer">{{this.disclaimer01}}</div>
     <!-- Unsupported Format -->
    <v-dialog v-model="unsupported" width="360px" light max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Select JPG/JPEG/PNG/PDF File Only</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="unsupported=false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
const maxThree = (value) => value.length <= 3;
// const maxSize = (value) => (value.forEach(v => {
//   if(v.size>2000)
//     return false;
// }));
const maxSize = (value) =>  {
  if (!value) {
    return true;
  }
  let file = value;
  let status = true;
  file.forEach(element => {
  if(element.size > 10485760)
    status=false;
  });
  return status;
};
export default {
  name: "WizardPage_03",
  data() {
    return {
      sRelativeFirstName: '',
      sRelativeLastName: '',
      sOtherRelation: '',
      files:[],
      sRelativeFileNameArray:[],
      sRelativeFileArray:[],
      sSelectedRelation: '',
      option:[],
      bShowOtherRelation:false,
      dialog:false,
      unsupported:false,
      oPatientRepresentativeArray: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oPatientRepresentatives,
      bOther: false,
      sSelectedPatientRepresentatives: [],
      sOtherPatientRepresentatives: '',
      sSelectedPatientRepresentativesName:'',


      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer02,
      disclaimer03: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer03,
      disclaimer04: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer04,
      sActiveBtn:'',
      //TODO: Fetch disclaimer03 for multiple dile upload

      MRORelationshipParentLegalGuardian: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORelationshipParentLegalGuardian,
       MRORelationshipLegalRepresentative: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORelationshipLegalRepresentative,
      MRORelationshipOther: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORelationshipOther,
      MRORelationMultipleDocument: this.$store.state.ConfigModule
        .apiResponseDataByLocation.oFields.MRORelationMultipleDocument
    };
  },
  //Relative name and realtion validations
  mixins: [validationMixin],
  validations: {
    sRelativeFirstName: { required },
    sRelativeLastName: { required },
    sOtherPatientRepresentatives: { required },
    files: { maxThree ,maxSize }
    // ,maxSize
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    //Relative name and realtion validation error message setter
    sRelativeFirstNameErrors() {
      const errors = [];
      if (!this.$v.sRelativeFirstName.$dirty) return errors;
      !this.$v.sRelativeFirstName.required && errors.push("First Name is required.");
      return errors;
    },
    sRelativeLastNameErrors() {
      const errors = [];
      if (!this.$v.sRelativeLastName.$dirty) return errors;
      !this.$v.sRelativeLastName.required && errors.push("Last Name is required.");
      return errors;
    },
    sOtherPatientRepresentativesErrors() {
      const errors = [];
      if (!this.$v.sOtherPatientRepresentatives.$dirty) return errors;
      !this.$v.sOtherPatientRepresentatives.required &&
        errors.push("Required if you select Other option.");
      return errors;
    },
    filesErrors() {
      const errors = [];
      if (!this.$v.files.$dirty) return errors;
      !this.$v.files.maxThree && errors.push("You can upload only 3 files");
      !this.$v.files.maxSize && errors.push("One of the uploaded file is greater than 10 MB");
      return errors;
    }
  },
  methods: {
    // This will set bAreYouPatient status to true and empty realtives variables
    setPatient() {
      this.sActiveBtn='Yes';
      this.$store.commit("requestermodule/bAreYouPatient", true);
      this.$store.commit("requestermodule/sRelativeFirstName", "");
      this.$store.commit("requestermodule/sRelativeLastName", "");
      this.$store.commit("requestermodule/sSelectedRelationName", "");
      this.$store.commit("requestermodule/sSelectedRelation", "");
      this.$store.commit("requestermodule/sRelativeFileArray", []);
      this.$store.commit("requestermodule/sRelativeFileNameArray", []);
      
      //Partial Requester Data Save Start
        this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
        if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
        {
          this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
        }
        //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    // This will set bAreYouPatient status to false and set realtives variables
    setNotPatient() {
      this.sActiveBtn='No';
      this.$store.commit("requestermodule/bAreYouPatient", false);
    },
    continueAhead() {
      this.$store.commit("requestermodule/sSelectedRelation",this.sSelectedPatientRepresentatives);
        if (this.sSelectedPatientRepresentatives == "MRORelationshipOther") {
          this.sSelectedPatientRepresentativesName=this.sOtherPatientRepresentatives;
        }
      this.$store.commit("requestermodule/sSelectedRelationName", this.sSelectedPatientRepresentativesName);

      this.$store.commit("requestermodule/sRelativeFileArray", this.sRelativeFileArray);
      this.$store.commit("requestermodule/sRelativeFileNameArray", this.sRelativeFileNameArray);
      this.$store.commit("requestermodule/sRelativeFirstName", this.sRelativeFirstName);
      this.$store.commit("requestermodule/sRelativeLastName", this.sRelativeLastName);
      this.$store.commit("requestermodule/sSelectedRelation", this.sSelectedPatientRepresentatives[0]);

      //Partial Requester Data Save Start
        this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
        if(this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardsSave[this.$store.state.ConfigModule.selectedWizard]==1)
        {
          this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
        }
        //Partial Requester Data Save End

      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    filesChange(files){
      this.sRelativeFileArray=[];
      this.sRelativeFileNameArray=[];
      var counter=0;
      var fileLength=files.length;
      for( var i = 0; i < files.length; i++ ){        
        var file_name_array = files[i].name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "pdf"){
          const reader = new FileReader();
          reader.addEventListener("load", () => {
            this.sRelativeFileArray.push(reader.result);   
            counter++;       
          });
          reader.addEventListener("loadend", () => {
            if(counter==fileLength){
              var supportDocObj = {
                nRequesterID: this.$store.state.requestermodule.nRequesterID,
                nFacilityID: this.$store.state.requestermodule.nFacilityID,
                sRelativeFileArray: this.sRelativeFileArray,
                sRelativeFileNameArray: this.sRelativeFileNameArray,
                sWizardName: this.$store.state.ConfigModule.selectedWizard
              };
              this.$http.post("requesters/UpdateSupportDocs/",supportDocObj)
                .then(response => {
                  this.$store.commit("requestermodule/nRequesterID", response.body);
                });         
            }
          });
          reader.readAsDataURL(files[i]);
          this.sRelativeFileNameArray.push(this.files[i].name);
        }
        else{
          this.files=[];
          this.unsupported=true;          
          break;
        }
      }    
      if(this.files.length>0 && !this.$v.files.$invalid)
      {
        this.dialog=true;        
      }
    },
    // to check if selected checkbox is other reason
    check(patientRepresentative) {
        this.sSelectedPatientRepresentatives = [];
        this.sSelectedPatientRepresentatives.push(patientRepresentative.sNormalizedPatientRepresentativeName);
        this.sSelectedPatientRepresentativesName=patientRepresentative.sPatientRepresentativeName;
        if (this.sSelectedPatientRepresentatives == "MRORelationshipOther") {
          this.bOther = true;
          this.sSelectedPatientRepresentativesName=this.sOtherPatientRepresentatives;
        }
        else{
          this.bOther=false;
          this.sOtherPatientRepresentatives='';
        }
          
    }
  }
};
</script>
