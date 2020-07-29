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
            <v-col>
              <label cols="12" offset-sm="2" sm="8">
              {{disclaimer03}}
              </label>
            </v-col>
            <v-col v-if="MRORelationshipParentLegalGuardian" cols="12" offset-sm="2" sm="8">
            <v-checkbox
              hide-details
              dark
              class="checkboxBorder"
              label="Parent/Legal Guardian"
              color="white"
              value="MRORelationshipParentLegalGuardian"
              v-model="option"
              @change="check('MRORelationshipParentLegalGuardian')"
            ></v-checkbox>
            </v-col>
            <v-col v-if="MRORelationshipLegalRepresentative"  cols="12" offset-sm="2" sm="8">
            <v-checkbox
              hide-details
              dark
              class="checkboxBorder"
              label="Legal Representative (Executor, Patient Rep., HCPOA, etc.)"
              color="white"
              value="MRORelationshipLegalRepresentative"
              v-model="option"
              @change="check('MRORelationshipLegalRepresentative')"
            ></v-checkbox>
            </v-col>
            <v-col v-if="MRORelationshipOther" cols="12" offset-sm="2" sm="8">
            <v-checkbox
              hide-details
              dark
              class="checkboxBorder"
              label="Other, Please Specify"
              color="white"
              value="MRORelationshipOther"
              v-model="option"
              @change="check('MRORelationshipOther')"
            ></v-checkbox>
            </v-col>
            <v-col  cols="12" offset-sm="3" sm="6">
            <v-text-field
              v-if="bShowOtherRelation"
              v-model="sOtherRelation"
              :error-messages="sOtherRelationErrors"
              label="Other Relation"
              required
              maxlength="50"
              @input="$v.sOtherRelation.$touch()"
              @blur="$v.sOtherRelation.$touch()"
            ></v-text-field>
            </v-col>
            <v-col v-if="MRORelationMultipleDocument"  cols="12" offset-sm="3" sm="6">
              <template>
              <v-file-input
                ref="file"
                @change="filesChange"
                v-model="files"
                placeholder="Upload your documents"
                label="File input"
                multiple
                prepend-icon="mdi-paperclip"
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
              </v-file-input>
            </template>
            </v-col>
            <v-col cols="12" offset-sm="3" sm="6">
            <v-btn
              v-if="sSelectedRelation=='MRORelationshipOther'"
              @click.prevent="continueAhead"
              :disabled="$v.$invalid"
              class="mr-4 next"
            >Continue</v-btn>
            <v-btn
              v-else
              @click.prevent="continueAhead"
              :disabled="$v.sRelativeFirstName.$invalid || $v.sRelativeLastName.$invalid || sSelectedRelation==''"
              class="mr-4 next"
            >Continue</v-btn>
          </v-col>
        </v-row>
      </form>
    </div>
    <div v-if="bAreYouPatient && disclaimer01!=null " class="disclaimer">{{this.disclaimer01}}</div>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_03",
  data() {
    return {
      sRelativeFirstName: '',
      sRelativeLastName: '',
      sOtherRelation: '',
      files:[],
      sRelativeFileArray:[],
      sSelectedRelation: '',
      option:[],
      bShowOtherRelation:false,

      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer01,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer02,
      disclaimer03: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer03,
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
    sOtherRelation: { required }
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
    sOtherRelationErrors() {
      const errors = [];
      if (!this.$v.sOtherRelation.$dirty) return errors;
      !this.$v.sOtherRelation.required &&
        errors.push("Required if you select Other option.");
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
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    // This will set bAreYouPatient status to false and set realtives variables
    setNotPatient() {
      this.sActiveBtn='No';
      this.$store.commit("requestermodule/bAreYouPatient", false);
    },
    continueAhead() {
      if(this.sSelectedRelation=='MRORelationshipParentLegalGuardian'){
        this.$store.commit("requestermodule/sSelectedRelationName","Parent/Legal Guardian");
      }
      if(this.sSelectedRelation=='MRORelationshipLegalRepresentative'){
        this.$store.commit("requestermodule/sSelectedRelationName","Legal Representative (Executor, Patient Rep., HCPOA, etc.)");
      }
      if(this.sSelectedRelation=='MRORelationshipOther'){
        this.$store.commit("requestermodule/sSelectedRelationName",this.sOtherRelation);
      }
      this.$store.commit("requestermodule/sRelativeFileArray", this.sRelativeFileArray);
      this.$store.commit("requestermodule/sRelativeFirstName", this.sRelativeFirstName);
      this.$store.commit("requestermodule/sRelativeLastName", this.sRelativeLastName);
      this.$store.commit("requestermodule/sSelectedRelation", this.sSelectedRelation);
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    filesChange(files){
      this.sRelativeFileArray=[];
      for( var i = 0; i < files.length; i++ ){        
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.sRelativeFileArray.push(reader.result);
        });
        reader.readAsDataURL(files[i]);
      }    
    },
    check(id) {
      this.option=[];
      this.option.push(id);
      this.sSelectedRelation = id;
      // this.sSelectedRelation.push(id);
        if (this.sSelectedRelation == "MRORelationshipOther") {
          this.bShowOtherRelation = true;
        }
        else{
          this.bShowOtherRelation=false;
          this.sOtherRelation='';
        }
    },
  }
};
</script>
