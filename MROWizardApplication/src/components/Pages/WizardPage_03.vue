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
            No, I am requesting medical records for someone else (Child, Dependent, Decedent).
          </button>
        </v-col>
      </div>
    </v-row>
    <!-- Below html will render only if user select not patient option -->
    <div v-show="!bAreYouPatient">
      <form>
        <v-row>
          <v-col cols="12" offset-sm="2" sm="8">
            <v-text-field
              v-model="sRelativeName"
              :error-messages="sRelativeNameErrors"
              label="Relative Full Name"
              required
              @input="$v.sRelativeName.$touch()"
              @blur="$v.sRelativeName.$touch()"
            ></v-text-field>
            <v-text-field
              v-model="sRelationToPatient"
              :error-messages="sRelationToPatientErrors"
              label="Please indicate your relationship to the patient."
              required
              @input="$v.sRelationToPatient.$touch()"
              @blur="$v.sRelationToPatient.$touch()"
            ></v-text-field>
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
            <v-btn
              @click.prevent="continueAhead"
              :disabled="$v.$invalid"
              class="mr-4 next"
            >Continue</v-btn>
          </v-col>
        </v-row>
      </form>
    </div>
    <div v-if="bAreYouPatient" class="disclaimer">{{this.disclaimer01}}</div>
    <div v-else class="disclaimer">{{this.disclaimer02}}</div>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
export default {
  name: "WizardPage_03",
  data() {
    return {
      sRelativeName: this.$store.state.requestermodule.sRelativeName,
      sRelationToPatient: this.$store.state.requestermodule.sRelationToPatient,
      files:[],
      sRelativeFileArray:[],

      disclaimer01: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer02,
      disclaimer02: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_03_disclaimer03,
      sActiveBtn:''
      //TODO: Fetch disclaimer03 for multiple dile upload
    };
  },
  //Relative name and realtion validations
  mixins: [validationMixin],
  validations: {
    sRelativeName: { required },
    sRelationToPatient: { required }
  },
  computed: {
    bAreYouPatient() {
      return this.$store.state.requestermodule.bAreYouPatient;
    },
    //Relative name and realtion validation error message setter
    sRelativeNameErrors() {
      const errors = [];
      if (!this.$v.sRelativeName.$dirty) return errors;
      !this.$v.sRelativeName.required && errors.push("Name is required.");
      return errors;
    },
    sRelationToPatientErrors() {
      const errors = [];
      if (!this.$v.sRelationToPatient.$dirty) return errors;
      !this.$v.sRelationToPatient.required &&
        errors.push("Realtion to patient is required");
      return errors;
    }
  },
  methods: {
    // This will set bAreYouPatient status to true and empty realtives variables
    setPatient() {
      this.sActiveBtn='Yes';
      this.$store.commit("requestermodule/bAreYouPatient", true);
      this.$store.commit("requestermodule/sRelativeName", "");
      this.$store.commit("requestermodule/sRelationToPatient", "");
      this.$store.commit("requestermodule/sRelativeFileArray", []);
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    // This will set bAreYouPatient status to false and set realtives variables
    setNotPatient() {
      this.sActiveBtn='No';
      this.$store.commit("requestermodule/bAreYouPatient", false);
    },
    continueAhead() {
      this.$store.commit("requestermodule/sRelativeFileArray", this.sRelativeFileArray);
      this.$store.commit("requestermodule/sRelativeName", this.sRelativeName);
      this.$store.commit(
        "requestermodule/sRelationToPatient",
        this.sRelationToPatient
      );
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
  }
};
</script>
