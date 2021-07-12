<template>
  <div>
    <div class="pageBody">
      <div class="pageContent">
        <p class="titleQuestion required">
          What is the patient's full legal name and date of birth?
        </p>
        <v-form>
          <v-row>
            <!-- Patient's First Name -->
            <v-col cols="6" sm="4">
              <label class="inputLabel required" for="id_sPatientFirstName"
                >Patient's First Name</label
              >
              <v-text-field
                id="id_sPatientFirstName"
                placeholder="Enter Patient's First Name"
                solo
                dense
                v-model="sPatientFirstName"
                :error-messages="sPatientFirstNameError"
                required
                maxlength="30"
                @input="$v.sPatientFirstName.$touch()"
                @blur="$v.sPatientFirstName.$touch()"
              >
              </v-text-field>
            </v-col>
            <!-- Patient's Middle Name -->
            <v-col v-if="MROPatientMiddleName" cols="6" sm="4">
              <label class="inputLabel required" for="id_sPatientMiddleName"
                >Patient's Middle Name</label
              >
              <v-text-field
                id="id_sPatientMiddleName"
                placeholder="Enter Patient's Middle Name"
                solo
                dense
                v-model="sPatientMiddleName"
                maxlength="30"
              >
              </v-text-field>
            </v-col>
            <!-- Patient's Last Name -->
            <v-col cols="6" sm="4">
              <label class="inputLabel required" for="id_sPatientLastName"
                >Patient's Last Name</label
              >
              <v-text-field
                id="id_sPatientLastName"
                placeholder="Enter Patient's Last Name"
                solo
                dense
                v-model="sPatientLastName"
                :error-messages="sPatientLastNameError"
                required
                maxlength="30"
                @input="$v.sPatientLastName.$touch()"
                @blur="$v.sPatientLastName.$touch()"
              >
              </v-text-field>
            </v-col>
            <!-- Patient's Date of Birth -->
            <v-col cols="6" sm="6">
              <label class="inputLabel required" for="id_dtPatientDOB"
                >Patient's Date Of Birth</label
              >
              <v-menu
                v-model="patientDOBMenu"
                :close-on-content-click="false"
                @input="hideDatePicker = true"
                max-width="290"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    ref="inputRef"
                    id="id_dtPatientDOB"
                    transition="scale-transition"
                    offset-y
                    solo
                    dense
                    v-model="dateTextInput"
                    :placeholder="sDFMMDDYYYY"
                    :error-messages="dtPatientDOBErrors"
                    clearable
                    v-bind="attrs"
                    v-on="on"
                    @click:clear="
                      dtPatientDOB = null;
                      hideDatePicker = true;
                    "
                    @blur="$v.dtPatientDOB.$touch()"
                    @input="handleDateTextChange"
                    append-icon="mdi-calendar-range"
                    @click:append="
                      hideDatePicker = false;
                      patientDOBMenu = true;
                    "
                  >
                  </v-text-field>
                </template>
                <v-date-picker
                  ref="picker"
                  :max="new Date().toISOString().substr(0, 10)"
                  v-model="dtPatientDOB"
                  color="green lighten-1"
                  header-color="primary"
                  light
                  @change="handleDateChange"
                  :hidden="hideDatePicker"
                >
                </v-date-picker>
              </v-menu>
            </v-col>
            <!-- Added Extra Top P&M -->
            <v-col
              v-if="MROPatientDeceased && !bAreYouPatient"
              class="mt-3 pt-3"
              cols="12"
              sm="6"
            >
              <v-checkbox
                dense
                hide-details
                v-model="bPatientDeceased"
                color="customText"
                class="smallCheckboxWithBg"
              >
                <template v-slot:label>
                  <span class="smallCheckboxLabel">Patient is deceased</span>
                </template>
              </v-checkbox>
            </v-col>

            <!-- Upload supporting docs -->

            <v-col v-if="MRORelationMultipleDocument" cols="12" sm="12">
              <label class="inputLabel"
                >Upload supporting documents (LOA, Death Certificate, POA,
                etc.)</label
              >
              <Upload
                :noOfFilesLimit="3"
                :fileSizeLimit="10"
                message="Drag & drop or <a href='#'>Browse</a>"
                :multiple="true"
                @filesUploaded="filesChange"
              />
            </v-col>

            <v-col cols="12" sm="12">
              <p class="center">
                Rather Use a smartphone?{{ " " }}
                <a href="#" @click="bShowSessionTransfer()">Click HERE</a>
                to get a link
              </p>
            </v-col>
          </v-row>
        </v-form>

        <!-- Unsupported format Dialog -->
        <v-dialog v-model="unsupported" width="360px" light max-width="350px">
          <v-card>
            <v-card-title class="popupTitle">Info</v-card-title>
            <v-card-text class="popupBody"
              >Select JPG/JPEG/PNG/PDF File Only</v-card-text
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="green darken-1" text @click="unsupported = false"
                >Ok</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>

        <!-- Document required dialog -->
        <v-dialog
          v-model="DocReqDiaglog"
          persistent
          width="350px"
          light
          max-width="350px"
        >
          <v-card class="DocReqDiaglog">
            <v-card-title class="justify-center popupTitle" id="DocReqTitle">{{
              disclaimer05
            }}</v-card-title>
            <v-card-text id="DocReqBody" class="popupBody"
              ><p id="DocReqBodyPre" v-html="disclaimer06"></p
            ></v-card-text>

            <v-row id="DocReqActionRow">
              <v-col id="DocReqGoBackCol" cols="4" offset="4">
                <v-btn id="DocReqGoBackBtn" @click="DocReqDiaglog = false"
                  >Go Back</v-btn
                ><br />
              </v-col>
              <v-col id="DocReqContinueCol" cols="6" offset="3">
                <v-btn
                  id="DocReqContinueBtn"
                  color="#3f48cc"
                  text
                  @click="nextPage"
                  >Continue to next step</v-btn
                >
              </v-col>
            </v-row>
          </v-card>
        </v-dialog>

        <!-- Upload successful dialog-->
        <v-dialog v-model="dialog" max-width="290" light>
          <v-card>
            <v-card-text style="padding-top: 20px">
              You have successfully uploaded the necessary supporting documents
              to verify your legal relationship to the patient.
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="green darken-1" text @click="dialog = false">
                Ok
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <div v-if="!$vuetify.breakpoint.xs">
          <!-- Submit Button -->
          <v-col cols="12" offset-sm="3" sm="6" class="center">
            <v-btn
              class="smallBlackBtn"
              @click.once="docReqCheck"
              :key="buttonKey"
              :disabled="
                $v.sPatientFirstName.$invalid ||
                  $v.sPatientLastName.$invalid ||
                  $v.dtPatientDOB.$invalid
              "
              >Next</v-btn
            >
          </v-col>
        </div>
        <Footer v-if="$vuetify.breakpoint.xs" />
      </div>
    </div>
    <div v-if="$vuetify.breakpoint.xs" class="buttonBlockMobile">
      <v-col cols="12" offset-sm="3" sm="6" class="center">
        <v-btn
          class="smallBlackBtn"
          @click.once="docReqCheck"
          :key="buttonKey"
          :disabled="
            $v.sPatientFirstName.$invalid ||
              $v.sPatientLastName.$invalid ||
              $v.dtPatientDOB.$invalid
          "
          >Next</v-btn
        >
      </v-col>
    </div>
    <Footer v-if="!$vuetify.breakpoint.xs" />
  </div>
</template>

<script>
import { mapState } from "vuex";
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
import moment from "moment";
import Upload from "../Upload.vue";
import { oGlobalConstants } from "../../shared/GlobalConstants";
import Footer from "../Footer.vue";
const maxThree = (value) => {
  return value.length <= 3;
};
const maxSize = (value) => {
  if (!value) {
    return true;
  }
  let file = value;
  let status = true;
  file.forEach((element) => {
    if (element.size > 10485760) {
      status = false;
    }
  });
  return status;
};

export default {
  name: "WizardPage_04",
  components: { Upload, Footer },
  activated() {
    this.buttonKey++;
    if (this.bAreYouPatient || !this.MROPatientDeceased) {
      this.bPatientDeceased = false;

      this.$store.commit(
        "requestermodule/bPatientDeceased",
        this.bPatientDeceased
      );
    }
  },
  data() {
    return {
      sDFMMDDYYYY: oGlobalConstants.sDateFormatMMDDYYYY,
      sDFYYYYMMDD: oGlobalConstants.sDateFormatYYYYMMDD,
      dragging: false,
      dateTextInput: "",
      sPatientFirstName: this.$store.state.requestermodule.sPatientFirstName,
      sPatientMiddleName: this.$store.state.requestermodule.sPatientMiddleName,
      sPatientLastName: this.$store.state.requestermodule.sPatientLastName,
      // disclaimer02: "",
      bPatientDeceased: this.$store.state.requestermodule.bPatientDeceased,
      dtPatientDOB: this.$store.state.requestermodule.dtPatientDOB,
      hideDatePicker: true,
      patientDOBMenu: false,
      files:
        this.$store.state.requestermodule.sRelativeFileArray.length != 0
          ? this.getFilesFromState(
              this.$store.state.requestermodule.sRelativeFileArray,
              this.$store.state.requestermodule.sRelativeFileNameArray
            )
          : [],
      sRelativeFileNameArray: this.$store.state.requestermodule
        .sRelativeFileNameArray,
      sRelativeFileArray: this.$store.state.requestermodule.sRelativeFileArray,
      unsupported: false,
      DocReqDiaglog: false,
      dialog: false,
      buttonKey: 1,
    };
  },
  //Validations for fields
  mixins: [validationMixin],
  validations: {
    sPatientFirstName: { required, maxLength: maxLength(30) },
    sPatientLastName: { required, maxLength: maxLength(30) },
    sPatientPreviousFirstName: { required, maxLength: maxLength(30) },
    sPatientPreviousLastName: { required, maxLength: maxLength(30) },
    dtPatientDOB: {
      required,
      minValue: (value) => value < new Date().toISOString(),
    },
    files: { maxThree, maxSize },
    // sPatientMiddleName: { maxLength: maxLength(1) }
  },
  watch: {
    patientDOBMenu(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
    },
  },
  computed: {
    //Validation messages for fields
    sPatientFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientFirstName.$dirty) return errors;
      !this.$v.sPatientFirstName.maxLength &&
        errors.push("First Name must be at most 30 characters long");
      !this.$v.sPatientFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPatientLastNameError() {
      const errors = [];
      if (!this.$v.sPatientLastName.$dirty) return errors;
      !this.$v.sPatientLastName.maxLength &&
        errors.push("Last Name must be at most 30 characters long");
      !this.$v.sPatientLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    sPatientPreviousFirstNameError() {
      const errors = [];
      if (!this.$v.sPatientPreviousFirstName.$dirty) return errors;
      !this.$v.sPatientPreviousFirstName.maxLength &&
        errors.push("First Name must be at most 30 characters long");
      !this.$v.sPatientPreviousFirstName.required &&
        errors.push("First Name is required.");
      return errors;
    },
    sPatientPreviousLastNameError() {
      const errors = [];
      if (!this.$v.sPatientPreviousLastName.$dirty) return errors;
      !this.$v.sPatientPreviousLastName.maxLength &&
        errors.push("Last Name must be at most 30 characters long");
      !this.$v.sPatientPreviousLastName.required &&
        errors.push("Last Name is required.");
      return errors;
    },
    dtPatientDOBFormatted() {
      return this.dtPatientDOB
        ? moment(this.dtPatientDOB).format(this.sDFMMDDYYYY)
        : "";
    },
    dtPatientDOBErrors() {
      const errors = [];
      if (!this.$v.dtPatientDOB.$dirty) return errors;
      !this.$v.dtPatientDOB.required &&
        errors.push("Date of birth is required");
      !this.$v.dtPatientDOB.minValue &&
        errors.push("This date cannot be future date");
      return errors;
    },
    filesErrors() {
      const errors = [];
      if (!this.$v.files.$dirty) return errors;
      !this.$v.files.maxThree && errors.push("You can upload only 3 files");
      !this.$v.files.maxSize &&
        errors.push("One of the uploaded file is greater than 10 MB");
      return errors;
    },

    ...mapState({
      //Get value from state to show and hide fields
      MROPatientMiddleName: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROPatientMiddleName,
      MROPatientPreviousMiddleName: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROPatientPreviousMiddleName,
      MROPatientNameChanged: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROPatientNameChanged,
      bAreYouPatient: (state) => state.requestermodule.bAreYouPatient,
      Wizard_04_disclaimer01: (state) =>
        state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper
          .Wizard_04_disclaimer01,
      disclaimer05: (state) =>
        state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper
          .Wizard_03_disclaimer05,
      disclaimer06: (state) =>
        state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper
          .Wizard_03_disclaimer06,
      MROPatientDeceased: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields.MROPatientDeceased,
      MRORelationMultipleDocument: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORelationMultipleDocument,
    }),
  },
  methods: {
    handleDateChange() {
      this.patientDOBMenu = false;
      this.hideDatePicker = true;
      this.dateTextInput = moment(this.dtPatientDOB).format(this.sDFMMDDYYYY);
    },
    handleDateTextChange() {
      this.dtPatientDOB =
        moment(this.dateTextInput, this.sDFMMDDYYYY, true).format(
          this.sDFYYYYMMDD
        ) === "Invalid date"
          ? null
          : moment(this.dateTextInput, this.sDFMMDDYYYY, true).format(
              this.sDFYYYYMMDD
            );
    },
    checked() {
      if (!this.bPatientNameChanged) {
        this.sPatientPreviousFirstName = "";
        this.sPatientPreviousLastName = "";
        this.sPatientPreviousMiddleName = "";
      }
    },
    docReqCheck() {
      if (this.sRelativeFileArray.length == 0) {
        this.DocReqDiaglog = true;
        this.buttonKey++;
      } else {
        this.nextPage();
      }
    },

    nextPage() {
      this.$store.commit(
        "requestermodule/sPatientFirstName",
        this.sPatientFirstName
      );
      this.$store.commit(
        "requestermodule/sPatientLastName",
        this.sPatientLastName
      );
      this.$store.commit(
        "requestermodule/sPatientMiddleName",
        this.sPatientMiddleName
      );
      this.$store.commit("requestermodule/dtPatientDOB", this.dtPatientDOB);
      this.$store.commit(
        "requestermodule/sPatientPreviousFirstName",
        this.sPatientPreviousFirstName
      );
      this.$store.commit(
        "requestermodule/sPatientPreviousLastName",
        this.sPatientPreviousLastName
      );
      this.$store.commit(
        "requestermodule/sPatientPreviousMiddleName",
        this.sPatientPreviousMiddleName
      );
      this.$store.commit(
        "requestermodule/bPatientNameChanged",
        this.bPatientNameChanged
      );
      this.$store.commit(
        "requestermodule/bPatientDeceased",
        this.bPatientDeceased
      );

      this.$store.commit(
        "requestermodule/sRelativeFileArray",
        this.sRelativeFileArray
      );
      this.$store.commit(
        "requestermodule/sRelativeFileNameArray",
        this.sRelativeFileNameArray
      );

      //Partial Requester Data Save Start
      this.$store.dispatch("requestermodule/partialAddReq");
      // Mutate Next Index
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    bShowSessionTransfer() {
      this.$store.commit("ConfigModule/bShowSessionTransfer", true);
    },
    filesChange(payload) {
      let files = payload.files;
      let bRemoved = payload.bRemoved;
      this.sRelativeFileArray = [];
      this.sRelativeFileNameArray = [];
      var fileLength = files.length;
      for (var i = 0; i < files.length; i++) {
        var file_name_array = files[i].name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        this.sRelativeFileNameArray.push(
          file_name_array[file_name_array.length - 2]
        );
        if (
          file_extension == "jpg" ||
          file_extension == "png" ||
          file_extension == "jpeg" ||
          file_extension == "pdf"
        ) {
          const reader = new FileReader();
          reader.addEventListener("load", () => {
            this.sRelativeFileArray.push(reader.result);
          });
          reader.addEventListener("loadend", () => {
            if (this.sRelativeFileArray.length == fileLength) {
              var supportDocObj = {
                nRequesterID: this.$store.state.requestermodule.nRequesterID,
                nFacilityID: this.$store.state.requestermodule.nFacilityID,
                sRelativeFileArray: this.sRelativeFileArray,
                sRelativeFileNameArray: this.sRelativeFileNameArray,
                sWizardName: this.$store.state.ConfigModule.selectedWizard,
              };
              this.$http
                .post("requesters/UpdateSupportDocs/", supportDocObj)
                .then((response) => {
                  this.$store.commit(
                    "requestermodule/nRequesterID",
                    response.body
                  );
                });
            }
          });
          reader.readAsDataURL(files[i]);
        } else {
          this.files = [];
          this.unsupported = true;
          break;
        }
      }

      if (files.length > 0 && !this.$v.files.$invalid && !bRemoved) {
        this.dialog = true;
      }
    },
    getFilesFromState(sRelativeFileArray, sRelativeFileNameArray) {
      var returnFiles = [];
      sRelativeFileArray.forEach((element, index) => {
        var fileName = sRelativeFileNameArray[index];
        var mineType = this.base64MimeType(element);
        fetch(element)
          .then((res) => res.blob())
          .then((blob) => {
            returnFiles.push(new File([blob], fileName, { type: mineType }));
          });
      });
      return returnFiles;
    },
    base64MimeType(encoded) {
      var result = null;
      if (typeof encoded !== "string") {
        return result;
      }
      var mime = encoded.match(/data:([a-zA-Z0-9]+\/[a-zA-Z0-9-.+]+).*,.*/);
      if (mime && mime.length) {
        result = mime[1];
      }
      return result;
    },
  },
  // mounted() {
  //   this.disclaimer02 = this.disclaimer.replace("your", "patient's");
  // },
};
</script>

<style scoped>
.DocReqDiaglog {
  border: 2px solid #3f48cc;
}
#DocReqTitle {
  text-decoration: underline;
  font-size: 24px;
  color: #3f48cc;
}
#DocReqBody {
  padding-bottom: 0;
}
#DocReqBodyPre {
  color: #3f48cc;
}
#DocReqActionRow {
  width: 100%;
  margin: 0;
}
#DocReqGoBackBtn {
  background-color: #3f48cc;
  color: white;
  margin-top: 0;
  margin-bottom: 0;
  text-transform: none;
}
#DocReqContinueBtn {
  margin-top: 0;
  margin-bottom: 0;
  text-transform: none;
  padding: 0;
  text-decoration: underline;
}
#DocReqGoBackCol {
  margin-top: 0;
  margin-bottom: 0;
  padding-bottom: 0;
}
#DocReqContinueCol {
  margin-top: 0;
  margin-bottom: 0;
  padding-bottom: 0;
}
</style>
