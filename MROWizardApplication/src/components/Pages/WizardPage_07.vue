<template>
  <div class="left">
    <div class="pageBody">
      <div class="pageContent">
        <p class="titleQuestion">
          Please specify an approximate date range for records.
        </p>
        <v-form>
          <v-row>
            <!-- bPatientNameChanged -->
            <v-col v-if="MROPatientNameChanged" cols="12">
              <v-switch
                flat
                inset
                dense
                hide-details
                v-model="bPatientNameChanged"
                @change="checked()"
                color="customText"
              >
                <template v-slot:label>
                  <span class="subHeading"
                    >Please check here if name was different at the time of
                    visit (examples: maiden name, minor's name, alias)</span
                  >
                </template>
              </v-switch>
            </v-col>
            <template v-if="bPatientNameChanged">
              <!-- Patient's Previous First Name -->
              <v-col cols="6" sm="4">
                <label
                  class="inputLabel required"
                  for="id_sPatientPreviousFirstName"
                  >First Name</label
                >
                <v-text-field
                  placeholder="Enter First Name"
                  id="id_sPatientPreviousFirstName"
                  v-model="sPatientPreviousFirstName"
                  :error-messages="sPatientPreviousFirstNameError"
                  required
                  maxlength="30"
                  @input="$v.sPatientPreviousFirstName.$touch()"
                  @blur="$v.sPatientPreviousFirstName.$touch()"
                  solo
                  dense
                ></v-text-field>
              </v-col>
              <v-col v-if="MROPatientPreviousMiddleName" cols="6" sm="4">
                <!-- Patient's Previous Middle Name -->
                <label class="inputLabel" for="id_sPatientPreviousMiddleName"
                  >Middle Name</label
                >
                <v-text-field
                  placeholder="Enter Middle Name"
                  id="id_sPatientPreviousMiddleName"
                  maxlength="30"
                  v-model="sPatientPreviousMiddleName"
                  solo
                  dense
                ></v-text-field>
              </v-col>
              <v-col cols="6" sm="4">
                <!-- Patient's Previous Last Name -->
                <label
                  class="inputLabel required"
                  for="id_sPatientPreviousLastName"
                  >Last Name</label
                >
                <v-text-field
                  placeholder="Enter Last Name"
                  id="id_sPatientPreviousLastName"
                  v-model="sPatientPreviousLastName"
                  :error-messages="sPatientPreviousLastNameError"
                  required
                  maxlength="30"
                  @input="$v.sPatientPreviousLastName.$touch()"
                  @blur="$v.sPatientPreviousLastName.$touch()"
                  solo
                  dense
                ></v-text-field>
              </v-col>
            </template>

            <!-- Start Date input -->

            <v-col v-if="MRORecordsDateRange" cols="6" sm="6">
              <label class="inputLabel required" for="id_dtRecordRangeStart"
                >Start Date</label
              >
              <v-menu
                v-model="menu1"
                :close-on-content-click="false"
                @input="hideStartDatePicker = true"
                max-width="290"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="startDateTextInput"
                    ref="inputStartDateRef"
                    id="id_dtRecordRangeStart"
                    transition="scale-transition"
                    offset-y
                    :error-messages="dtRecordRangeStartErrors"
                    clearable
                    placeholder="MM-DD-YYYY"
                    v-bind="attrs"
                    v-on="on"
                    @click:clear="
                      dtRecordRangeStart = null;
                      hideStartDatePicker = true;
                    "
                    @blur="$v.dtRecordRangeStart.$touch()"
                    @input="handleStartDateTextChange"
                    append-icon="mdi-calendar-range"
                    @click:append="
                      hideStartDatePicker = false;
                      menu1 = true;
                    "
                    :disabled="bRecordMostRecentVisit || bSpecifyVisit"
                    solo
                    dense
                  ></v-text-field>
                </template>
                <v-date-picker
                  ref="startpicker"
                  v-model="dtRecordRangeStart"
                  color="green lighten-1"
                  header-color="primary"
                  light
                  @change="handleStartDateChange"
                  :max="new Date().toISOString().substr(0, 10)"
                  :hidden="hideStartDatePicker"
                ></v-date-picker>
              </v-menu>
            </v-col>

            <!-- End Date input -->

            <v-col v-if="MRORecordsDateRange" cols="6" sm="6">
              <label class="inputLabel required" for="id_dtRecordRangeEnd"
                >End Date</label
              >
              <v-menu
                v-model="menu2"
                :close-on-content-click="false"
                max-width="290"
                @input="hideEndDatePicker = true"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    ref="inputEndDateRef"
                    id="id_dtRecordRangeEnd"
                    v-model="endDateTextInput"
                    transition="scale-transition"
                    offset-y
                    :error-messages="dtRecordRangeEndErrors"
                    clearable
                    placeholder="MM-DD-YYYY"
                    v-bind="attrs"
                    v-on="on"
                    @click:clear="
                      dtRecordRangeEnd = null;
                      hideEndDatePicker = true;
                    "
                    @blur="$v.dtRecordRangeEnd.$touch()"
                    @input="handleEndDateTextChange"
                    :disabled="bRecordMostRecentVisit || bSpecifyVisit"
                    append-icon="mdi-calendar-range"
                    @click:append="
                      hideEndDatePicker = false;
                      menu2 = true;
                    "
                    solo
                    dense
                  ></v-text-field>
                </template>
                <v-date-picker
                  ref="endpicker"
                  v-model="dtRecordRangeEnd"
                  color="green lighten-1"
                  header-color="primary"
                  light
                  @change="handleEndDateChange"
                  :max="new Date().toISOString().substr(0, 10)"
                  :hidden="hideEndDatePicker"
                ></v-date-picker>
              </v-menu>
            </v-col>

            <!-- Recent Visit -->

            <v-col v-if="MRORecordsMostRecentVisit" cols="6">
              <v-checkbox
                hide-details
                dense
                color="customText"
                class="smallCheckboxWithBg"
                v-model="bRecordMostRecentVisit"
                @change="CheckedMostRecentVisit()"
              >
                <template v-slot:label>
                  <span class="smallCheckboxLabel required"
                    >Most Recent Visit</span
                  >
                </template>
              </v-checkbox>
            </v-col>
            <v-col v-if="MROSpecifyVisit" cols="6">
              <v-checkbox
                dense
                hide-details
                color="customText"
                class="smallCheckboxWithBg"
                v-model="bSpecifyVisit"
                @change="CheckedSpecifyVisit()"
                ><template v-slot:label>
                  <span class="smallCheckboxLabel">Specify Event</span>
                </template>
              </v-checkbox>
            </v-col>
            <!-- Specify visit Textarea -->
            <v-col v-if="bSpecifyVisit" cols="12">
              <label class="inputLabel" for="id_sSpecifyVisitText"
                >Specify Visit</label
              >
              <v-textarea
                id="id_sSpecifyVisitText"
                class="TextAreaBg"
                v-model="sSpecifyVisitText"
                maxlength="100"
                rows="3"
                counter
                placeholder="Specify visit here"
                type="text"
                :error-messages="sSpecifyVisitTextErrors"
                @input="$v.sSpecifyVisitText.$touch()"
                @blur="$v.sSpecifyVisitText.$touch()"
                solo
                dense
              ></v-textarea>
            </v-col>

            <br />

            <!-- New UI Part Addition -->

            <v-col cols="12">
              <p class="disclaimer">
                This is optional but may help us better fulfill your request.
              </p>
            </v-col>

            <v-col cols="12">
              <p class="titleQuestion">Is there a deadline for this request?</p>
            </v-col>
            <!-- Yes, I have a deadline -->
            <v-col name="Yes" cols="6">
              <v-btn
                outlined
                :class="{ active: sActiveBtn === 'true' }"
                :key="buttonKey"
                @click.prevent="setDeadlineStatus(true)"
                class="bigWhiteBtn"
                >Yes, I have a deadline
              </v-btn>
            </v-col>
            <!-- No, just as soon as possible -->
            <v-col name="No" cols="6">
              <v-btn
                outlined
                :class="{ active: sActiveBtn === 'false' }"
                :key="buttonKey"
                @click.prevent="setDeadlineStatus(false)"
                class="bigWhiteBtn"
                >No, just as soon as possible</v-btn
              >
            </v-col>

            <v-col v-if="MRORequestDeadlineDate && sActiveBtn">
              <v-col cols="12" sm="12">
                <p class="disclaimer">
                  We will do our best to meet your time requirement. Enter
                  atleast one day in the future.
                </p>
              </v-col>
              <v-col cols="12" sm="12" lg="6">
                <!-- Deadline Date -->
                <label class="inputLabel" for="id_dtDeadline"
                  >Select Date</label
                >
                <v-menu
                  v-model="menu4"
                  :close-on-content-click="false"
                  max-width="290"
                  @input="hideDeadlineDatePicker = true"
                >
                  <template v-slot:activator="{ on, attrs }">
                    <v-text-field
                      id="id_dtDeadline"
                      transition="scale-transition"
                      offset-y
                      placeholder="MM-DD-YYYY"
                      :error-messages="dateErrors"
                      clearable
                      v-bind="attrs"
                      v-on="on"
                      @click:clear="
                        dtDeadline = null;
                        hideDeadlineDatePicker = true;
                      "
                      @blur="$v.dtDeadline.$touch()"
                      @input="handleDeadlineDateTextChange"
                      append-icon="mdi-calendar-range"
                      @click:append="
                        hideDeadlineDatePicker = false;
                        menu4 = true;
                      "
                      solo
                      dense
                      v-model="deadlineDateTextInput"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    ref="pickerDeadline"
                    :min="mindateDeadline"
                    :max="maxdateDeadline"
                    v-model="dtDeadline"
                    color="green lighten-1"
                    header-color="primary"
                    light
                    @change="handleDeadlineDateChange"
                    :hidden="hideDeadlineDatePicker"
                  ></v-date-picker>
                </v-menu>
              </v-col>
            </v-col>

            <v-col cols="12">
              <p class="disclaimer">
                We will respond to your request within the required timeframe
                unless a deadline has been specififed. If you need your
                recordssooner, or if you have an upcoming appointment, please
                let us know.
              </p>
            </v-col>

            <v-col cols="12">
              <p class="titleQuestion">When should this request expire?</p>
            </v-col>
            <!-- Radio Group Added -->
            <v-radio-group v-model="radioGroup" row dense>
              <!-- Months after signature date -->
              <v-col v-if="MROAuthExpireDateAfterNMonths" cols="12" md="4">
                <v-radio
                  class="checkboxBorder"
                  color="customColor"
                  :value="1"
                  @change="check(1)"
                >
                  <template v-slot:label>
                    <span class="smallCheckboxLabel">{{
                      nAuthMonths + " months after signature date"
                    }}</span>
                  </template>
                </v-radio>
              </v-col>

              <!-- On Specific Date -->

              <v-col v-if="MROAuthExpireDateSpecificDate" cols="12" md="4">
                <v-radio
                  class="checkboxBorder"
                  color="customColor"
                  :value="2"
                  @change="check(2)"
                >
                  <template v-slot:label>
                    <span class="smallCheckboxLabel">On specific date</span>
                  </template>
                </v-radio>
              </v-col>

              <!-- When a specific event occurs -->

              <v-col v-if="MROAuthExpireDateEventOccurs" cols="12" md="4">
                <v-radio
                  class="checkboxBorder"
                  color="customColor"
                  :value="3"
                  @change="check(3)"
                >
                  <template v-slot:label>
                    <span class="smallCheckboxLabel"
                      >When a specific event occurs</span
                    >
                  </template>
                </v-radio>
              </v-col>
            </v-radio-group>

            <!-- Specific event textarea -->

            <v-col v-if="nSelectedCheckBox == 3" cols="12" sm="12">
              <label class="inputLabel" for="id_sAuthSpecificEvent"
                >Specify event here</label
              >
              <v-textarea
                id="id_sAuthSpecificEvent"
                type="text"
                class="TextAreaBg"
                counter
                maxlength="100"
                v-model="sAuthSpecificEvent"
                rows="2"
                placeholder="Specify event here"
                solo
                dense
                :error-messages="sAuthSpecificEventError"
                @input="$v.sAuthSpecificEvent.$touch()"
                @blur="$v.sAuthSpecificEvent.$touch()"
              ></v-textarea>
            </v-col>

            <!-- Specific Date -->

            <v-col v-if="nSelectedCheckBox == 2" cols="12" sm="12">
              <label class="inputLabel required" for="id_dSpecific"
                >Specify Date</label
              >
              <v-menu
                v-model="menu3"
                :close-on-content-click="false"
                max-width="290"
                @input="hideSpecifyDatePicker = true"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    id="id_dSpecific"
                    transition="scale-transition"
                    offset-y
                    solo
                    dense
                    v-model="specifyDateTextInput"
                    placeholder="MM-DD-YYYY"
                    :error-messages="dSpecificErrors"
                    clearable
                    v-bind="attrs"
                    v-on="on"
                    @click:clear="
                      dSpecific = null;
                      hideSpecifyDatePicker = true;
                    "
                    @blur="$v.dSpecific.$touch()"
                    @input="handleSpecifyDateTextChange"
                    append-icon="mdi-calendar-range"
                    @click:append="
                      hideSpecifyDatePicker = false;
                      menu3 = true;
                    "
                  ></v-text-field>
                </template>
                <v-date-picker
                  ref="picker"
                  :min="mindate"
                  :max="maxdate"
                  v-model="dSpecific"
                  color="green lighten-1"
                  header-color="primary"
                  light
                  @change="handleSpecifyDateChange"
                  :hidden="hideSpecifyDatePicker"
                ></v-date-picker>
              </v-menu>
            </v-col>

            <!-- Next Buttons Start -->

            <!-- Screen 8 -->

            <!-- if name changed -->
            <v-row v-if="!$vuetify.breakpoint.xs">
              <v-col cols="6" sm="6"
                ><v-btn
                  :disabled="getNextButtonDisabledStatus"
                  @click.prevent="nextPage"
                  :key="buttonKey"
                  class="smallBlackBtn"
                  >Next</v-btn
                ></v-col
              >
              <v-col cols="6" sm="6"
                ><v-btn
                  @click.once="skipPage"
                  :key="buttonKey"
                  class="smallWhiteBtn"
                  >Skip</v-btn
                ></v-col
              >
            </v-row>

            <!-- <v-btn
              v-if="bRecordMostRecentVisit"
              @click.once="nextPage"
              :key="buttonKey"
              class="next"
              >Next</v-btn 
            >-->
            <!-- <v-btn
              v-if="bSpecifyVisit"
              :disabled="sSpecifyVisitText == ''"
              @click.once="nextPage"
              :key="buttonKey"
              class="next"
              >Next</v-btn
            > -->
            <!-- <v-btn
              v-if="!bRecordMostRecentVisit && !bSpecifyVisit"
              :disabled="
                $v.dtRecordRangeStart.$invalid || $v.dtRecordRangeEnd.$invalid
              "
              @click.once="nextPage"
              :key="buttonKey"
              class="next"
              >Next</v-btn
            >  -->
            <!-- Screen 17 -->

            <!-- <v-col
              v-if="
                nSelectedCheckBox[0] == 1 ||
                  nSelectedCheckBox[0] == 3 ||
                  nSelectedCheckBox[0] == null
              "
              cols="6"
              offset-sm="4"
              sm="2"
            >
              <v-btn
                :disabled="nSelectedCheckBox[0] == null"
                @click.once="nextPage"
                :key="buttonKey"
                class="next"
                >Next</v-btn
              >
            </v-col>
            <v-col
              v-if="nSelectedCheckBox[0] == 2"
              cols="6"
              offset-sm="4"
              sm="2"
            >
              <v-btn
                :disabled="$v.dSpecific.$invalid"
                @click.once="nextPage"
                :key="buttonKey"
                class="next"
                >Next</v-btn
              >
            </v-col> -->

            <!-- Screen 19 -->
            <!-- <v-btn
              @click.once="nextPage"
              :disabled="$v.$invalid"
              :key="buttonKey"
              class="next"
              >Next</v-btn
            > -->

            <!-- Next Buttons End -->
            <!-- Skip Button Starts -->
            <!-- Screen 17 -->

            <!-- Skip BUtton Ends -->
            <!-- New UI Part Addition Ends here-->
          </v-row>
        </v-form>
        <Footer v-if="$vuetify.breakpoint.xs" />
      </div>
    </div>
    <div v-if="$vuetify.breakpoint.xs" class="buttonBlockMobile">
      <v-row>
        <v-col cols="12" md="6" sm="12"
          ><v-btn
            :disabled="getNextButtonDisabledStatus"
            @click.prevent="nextPage"
            :key="buttonKey"
            class="smallBlackBtn"
            >Next</v-btn
          ></v-col
        >
        <v-col cols="12" md="6" sm="12"
          ><v-btn @click.once="skipPage" :key="buttonKey" class="smallWhiteBtn"
            >Skip</v-btn
          ></v-col
        >
      </v-row>
    </div>
    <Footer v-if="!$vuetify.breakpoint.xs" />
  </div>
</template>

<script>
import { mapState } from "vuex";
import moment from "moment";
import { required, maxLength } from "vuelidate/lib/validators";
import Footer from "../Footer.vue";
export default {
  name: "WizardPage_08",
  components: {
    Footer,
  },
  activated() {
    this.buttonKey++;

    this.maxdate = moment()
      .add(5, "years")
      .toISOString()
      .substr(0, 10);

    this.mindate = moment()
      .add(1, "days")
      .toISOString()
      .substr(0, 10);

    this.mindateDeadline = moment()
      .add(1, "days")
      .toISOString()
      .substr(0, 10);
    this.maxdateDeadline = moment()
      .add(5, "years")
      .toISOString()
      .substr(0, 10);
  },
  data() {
    return {
      radioGroup: [],
      dtRecordRangeStart: this.$store.state.requestermodule.dtRecordRangeStart,
      dtRecordRangeEnd: this.$store.state.requestermodule.dtRecordRangeEnd,
      deadlineDateTextInput: "",
      startDateTextInput: "",
      endDateTextInput: "",
      specifyDateTextInput: "",
      hideSpecifyDatePicker: true,
      hideDeadlineDatePicker: true,
      hideStartDatePicker: true,
      hideEndDatePicker: true,
      bRecordMostRecentVisit: this.$store.state.requestermodule
        .bRecordMostRecentVisit,
      bSpecifyVisit: this.$store.state.requestermodule.bSpecifyVisit,
      sSpecifyVisitText: this.$store.state.requestermodule.sSpecifyVisitText,
      menu1: false,
      menu2: false,
      buttonKey: 1,
      bPatientNameChanged: this.$store.state.requestermodule
        .bPatientNameChanged,
      sPatientPreviousFirstName: this.$store.state.requestermodule
        .sPatientPreviousFirstName,
      sPatientPreviousLastName: this.$store.state.requestermodule
        .sPatientPreviousLastName,
      sPatientPreviousMiddleName: this.$store.state.requestermodule
        .sPatientPreviousMiddleName,
      sActiveBtn: this.$store.state.requestermodule.bDeadlineStatus,
      nSelectedCheckBox: this.$store.state.ConfigModule.nSelectedAuthExpire,
      dSpecific:
        this.$store.state.ConfigModule.nSelectedAuthExpire[0] == 2
          ? this.$store.state.requestermodule.dtAuthExpire
          : null,
      menu3: false,
      dtAuthExpire: this.$store.state.requestermodule.dtAuthExpire,
      sAuthSpecificEvent: this.$store.state.requestermodule.sAuthSpecificEvent,
      maxdate: "",
      mindate: "",
      dtDeadline:
        this.$store.state.requestermodule.dtDeadline == ""
          ? moment()
              .add(1, "days")
              .toISOString()
              .substr(0, 10)
          : this.$store.state.requestermodule.dtDeadline,
      menu4: false,
      mindateDeadline: "",
      maxdateDeadline: "",
    };
  },
  watch: {
    menu1(val) {
      val && setTimeout(() => (this.$refs.startpicker.activePicker = "YEAR"));
    },
    menu2(val) {
      val && setTimeout(() => (this.$refs.endpicker.activePicker = "YEAR"));
    },
    menu3(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
    },
    menu4(val) {
      val &&
        setTimeout(() => (this.$refs.pickerDeadline.activePicker = "YEAR"));
    },
  },
  // Start and end date validations
  validations: {
    dtRecordRangeStart: {
      required,
      minValue: (value) => value < new Date().toISOString(),
    },
    dtRecordRangeEnd: {
      required,
      minValue: (value) => value < new Date().toISOString(),
      afterStartDate: (value, vm) =>
        new Date(vm.dtRecordRangeEnd).getTime() >=
        new Date(vm.dtRecordRangeStart).getTime(),
    },
    sSpecifyVisitText: {
      required,
    },
    sPatientPreviousFirstName: { required, maxLength: maxLength(30) },
    sPatientPreviousLastName: { required, maxLength: maxLength(30) },
    sAuthSpecificEvent: { required, maxLength: maxLength(100) },
    dSpecific: {
      required,
      minValue: (value) => value > new Date().toISOString(),
    },
    dtDeadline: {
      required,
      minValue: (value) => value > new Date().toISOString(),
    },
  },
  methods: {
    getPatientValidation() {
      if (this.bPatientNameChanged) {
        // means invalid return true for diabled
        return (
          this.$v.sPatientPreviousFirstName.$invalid ||
          this.$v.sPatientPreviousLastName.$invalid
        );
      }
      return false;
    },
    getDateRangeAndVisitValidation() {
      if (!this.bRecordMostRecentVisit && !this.bSpecifyVisit) {
        console.log(
          "Date Range:" + this.$v.dtRecordRangeStart.$invalid ||
            this.$v.dtRecordRangeEnd.$invalid
        );
        return (
          this.$v.dtRecordRangeStart.$invalid ||
          this.$v.dtRecordRangeEnd.$invalid
        );
      } else if (this.bSpecifyVisit) {
        return this.$v.sSpecifyVisitText.$invalid;
      } else {
        // Recent Visit
        return false;
      }
    },
    getDeadlineValidation() {
      if (this.sActiveBtn) {
        return this.$v.dtDeadline.$invalid;
      }
      return false;
    },
    getRequestExpireValidation() {
      if (
        this.nSelectedCheckBox[0] === 1 ||
        // this.nSelectedCheckBox[0] === 3 ||
        this.nSelectedCheckBox[0] === null
      ) {
        return this.nSelectedCheckBox[0] === null;
      }
      if (this.nSelectedCheckBox[0] === 2) {
        return this.$v.dSpecific.$invalid;
      }
      if (this.nSelectedCheckBox[0] === 3) {
        return this.$v.sAuthSpecificEvent.$invalid;
      }
      return false;
    },
    check(id) {
      this.nSelectedCheckBox = [];
      this.nSelectedCheckBox.push(id);
      this.radioGroup = id;
    },
    setDeadlineStatus(bDeadlineStatus) {
      this.sActiveBtn = bDeadlineStatus;
      this.$store.commit("requestermodule/bDeadlineStatus", bDeadlineStatus);
      // set deadline to tomorrow date
      this.$store.commit(
        "requestermodule/dtDeadline",
        moment()
          .add(30, "days")
          .toISOString()
          .substr(0, 10)
      );
      this.$store.commit("ConfigModule/bDeadlineStatus", bDeadlineStatus);

      // //Partial Requester Data Save Start
      // this.$store.dispatch('requestermodule/partialAddReq');
      // this.$store.commit("ConfigModule/mutateNextIndex");
    },
    handleSpecifyDateChange() {
      this.menu3 = false;
      this.hideSpecifyDatePicker = true;
      this.specifyDateTextInput = moment(this.dSpecific).format("MM-DD-YYYY");
    },
    handleDeadlineDateChange() {
      this.menu4 = false;
      this.hideDeadlineDatePicker = true;
      this.deadlineDateTextInput = moment(this.dtDeadline).format("MM-DD-YYYY");
    },
    handleStartDateChange() {
      this.menu1 = false;
      this.hideStartDatePicker = true;
      this.startDateTextInput = moment(this.dtRecordRangeStart).format(
        "MM-DD-YYYY"
      );
    },
    handleEndDateChange() {
      this.menu2 = false;
      this.hideEndDatePicker = true;
      this.endDateTextInput = moment(this.dtRecordRangeEnd).format(
        "MM-DD-YYYY"
      );
    },
    handleSpecifyDateTextChange() {
      this.dSpecific =
        moment(this.specifyDateTextInput, "MM-DD-YYYY", true).format(
          "YYYY-MM-DD"
        ) === "Invalid date"
          ? null
          : moment(this.specifyDateTextInput, "MM-DD-YYYY", true).format(
              "YYYY-MM-DD"
            );
    },
    handleDeadlineDateTextChange() {
      this.dtDeadline =
        moment(this.deadlineDateTextInput, "MM-DD-YYYY", true).format(
          "YYYY-MM-DD"
        ) === "Invalid date"
          ? null
          : moment(this.deadlineDateTextInput, "MM-DD-YYYY", true).format(
              "YYYY-MM-DD"
            );
    },
    handleStartDateTextChange() {
      this.dtRecordRangeStart =
        moment(this.startDateTextInput, "MM-DD-YYYY", true).format(
          "YYYY-MM-DD"
        ) === "Invalid date"
          ? null
          : moment(this.startDateTextInput, "MM-DD-YYYY", true).format(
              "YYYY-MM-DD"
            );
    },
    handleEndDateTextChange() {
      this.dtRecordRangeEnd =
        moment(this.endDateTextInput, "MM-DD-YYYY", true).format(
          "YYYY-MM-DD"
        ) === "Invalid date"
          ? null
          : moment(this.endDateTextInput, "MM-DD-YYYY", true).format(
              "YYYY-MM-DD"
            );
    },
    checked() {
      if (!this.bPatientNameChanged) {
        this.sPatientPreviousFirstName = "";
        this.sPatientPreviousLastName = "";
        this.sPatientPreviousMiddleName = "";
      }
    },
    CheckedMostRecentVisit() {
      if (this.bRecordMostRecentVisit) {
        this.$v.dtRecordRangeStart.$reset();
        this.$v.dtRecordRangeEnd.$reset();
        this.dtRecordRangeStart = null;
        this.dtRecordRangeEnd = null;
        // this.dtRecordRangeStartFormatted = "";
        // this.hideStartDatePicker = true;
        this.bSpecifyVisit = false;
        this.sSpecifyVisitText = "";
        this.$v.sSpecifyVisitText.$reset();
      }
    },
    CheckedSpecifyVisit() {
      if (this.bSpecifyVisit) {
        this.$v.dtRecordRangeStart.$reset();
        this.$v.dtRecordRangeEnd.$reset();
        this.dtRecordRangeStart = null;
        this.dtRecordRangeEnd = null;
        this.bRecordMostRecentVisit = false;
      }
      if (!this.bSpecifyVisit) {
        this.sSpecifyVisitText = "";
      }
    },
    nextPage() {
      this.$store.commit(
        "requestermodule/dtRecordRangeStart",
        this.dtRecordRangeStart
      );
      this.$store.commit(
        "requestermodule/dtRecordRangeEnd",
        this.dtRecordRangeEnd
      );
      this.$store.commit(
        "requestermodule/bRecordMostRecentVisit",
        this.bRecordMostRecentVisit
      );
      this.$store.commit("requestermodule/bSpecifyVisit", this.bSpecifyVisit);
      this.$store.commit(
        "requestermodule/sSpecifyVisitText",
        this.sSpecifyVisitText
      );
      //Partial Requester Data Save Start
      // // this.$store.dispatch("requestermodule/partialAddReq");

      // // this.$store.commit("ConfigModule/mutateNextIndex");

      /*New Store Commits*/

      this.$store.commit(
        "requestermodule/sPatientPreviousFirstName",
        this.sPatientPreviousFirstName
      );

      this.$store.commit(
        "requestermodule/sPatientPreviousMiddleName",
        this.sPatientPreviousMiddleName
      );

      this.$store.commit(
        "requestermodule/sPatientPreviousLastName",
        this.sPatientPreviousLastName
      );

      //Switch based on selection and set state values
      switch (this.nSelectedCheckBox[0]) {
        case 1:
          this.dtAuthExpire = moment()
            .add(this.nAuthMonths, "months")
            .toISOString()
            .substr(0, 10);
          this.sAuthSpecificEvent = "";
          break;
        case 2:
          this.dtAuthExpire = this.dSpecific;
          this.sAuthSpecificEvent = "";
          break;
        case 3:
          this.dtAuthExpire = null;
          break;
        default:
          var dt1 = new Date();
          this.dtAuthExpire = dt1.setMonth(dt1.getMonth() + this.nAuthMonths);
      }

      this.$store.commit("requestermodule/dtAuthExpire", this.dtAuthExpire);
      this.$store.commit(
        "requestermodule/sAuthSpecificEvent",
        this.sAuthSpecificEvent
      );

      this.$store.commit(
        "ConfigModule/nSelectedAuthExpire",
        this.nSelectedCheckBox
      );

      this.$store.commit("requestermodule/dtDeadline", this.dtDeadline);

      /*New Store Commits*/

      //Partial Requester Data Save Start
      this.$store.dispatch("requestermodule/partialAddReq");
      this.$store.commit("ConfigModule/mutateNextIndex");
      //Partial Requester Data Save End
    },
    skipPage() {
      var dt = new Date();
      dt.setMonth(dt.getMonth() + this.nAuthMonths);
      this.dtAuthExpire = dt.toISOString();
      this.$store.commit("ConfigModule/nSelectedAuthExpire", []);
      this.$store.commit("requestermodule/dtAuthExpire", this.dtAuthExpire);
      this.$store.commit("requestermodule/sAuthSpecificEvent", "");
      this.nSelectedCheckBox = [];
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
  },
  computed: {
    getNextButtonDisabledStatus() {
      // console.log(this.getPatientValidation());
      // console.log(this.getDateRangeAndVisitValidation());
      // console.log(this.getDeadlineValidation());
      // console.log(this.getRequestExpireValidation());
      return (
        this.getPatientValidation() ||
        this.getDateRangeAndVisitValidation() ||
        this.getDeadlineValidation() ||
        this.getRequestExpireValidation()
      );
    },
    //Date Format setter
    dateFormatted() {
      return this.dtDeadline
        ? moment(this.dtDeadline).format("MM-DD-YYYY")
        : "";
    },
    //Date validation error message setter
    dateErrors() {
      const errors = [];
      if (!this.$v.dtDeadline.$dirty) return errors;
      !this.$v.dtDeadline.minValue &&
        errors.push("Deadline date must be greater than today's date.");
      !this.$v.dtDeadline.required && errors.push("Date is required");
      return errors;
    },
    //Change date format
    dSpecificFormatted() {
      return this.dSpecific ? moment(this.dSpecific).format("MM-DD-YYYY") : "";
    },
    //Date validation
    dSpecificErrors() {
      const errors = [];
      if (!this.$v.dSpecific.$dirty) return errors;
      !this.$v.dSpecific.minValue &&
        errors.push("Date should be one day ahed from today.");
      !this.$v.dSpecific.required && errors.push("Date is required");
      return errors;
    },
    //Date Format setter
    dtRecordRangeStartFormatted() {
      return this.dtRecordRangeStart
        ? moment(this.dtRecordRangeStart).format("MM-DD-YYYY")
        : "";
    },
    dtRecordRangeEndFormatted() {
      return this.dtRecordRangeEnd
        ? moment(this.dtRecordRangeEnd).format("MM-DD-YYYY")
        : "";
    },
    //Start and End Date validation error message setter
    dtRecordRangeStartErrors() {
      const errors = [];
      if (!this.$v.dtRecordRangeStart.$dirty) return errors;
      !this.$v.dtRecordRangeStart.required &&
        errors.push("Start Date is required");
      !this.$v.dtRecordRangeStart.minValue &&
        errors.push("This date cannot be future date");
      return errors;
    },
    dtRecordRangeEndErrors() {
      const errors = [];
      if (!this.$v.dtRecordRangeEnd.$dirty) return errors;
      !this.$v.dtRecordRangeEnd.required && errors.push("End Date is required");
      !this.$v.dtRecordRangeEnd.minValue &&
        errors.push("This date cannot be future date");
      !this.$v.dtRecordRangeEnd.afterStartDate &&
        errors.push("End Date cannot be before Start Date.");
      return errors;
    },
    sSpecifyVisitTextErrors() {
      const errors = [];
      if (!this.$v.sSpecifyVisitText.$dirty) return errors;
      !this.$v.sSpecifyVisitText.required &&
        errors.push("Specify visit is required.");
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
    sAuthSpecificEventError() {
      const errors = [];
      if (!this.$v.sAuthSpecificEvent.$dirty) return errors;
      !this.$v.sAuthSpecificEvent.maxLength &&
        errors.push("Specify event must be at most 100 characters long");
      !this.$v.sAuthSpecificEvent.required &&
        errors.push("Specify event is required.");
      return errors;
    },
    ...mapState({
      //Show and Hide Fields Values
      MRORecordsDateRange: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORecordsDateRange,
      MRORecordsMostRecentVisit: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORecordsMostRecentVisit,
      MROSpecifyVisit: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields.MROSpecifyVisit,
      MROPatientNameChanged: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROPatientNameChanged,
      MROPatientPreviousMiddleName: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROPatientPreviousMiddleName,
      // disclaimer : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_18_disclaimer01,
      nAuthMonths: (state) => state.ConfigModule.nAuthExpirationMonths,
      MROAuthExpireDateAfterNMonths: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROAuthExpireDateAfterNMonths,
      MROAuthExpireDateSpecificDate: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROAuthExpireDateSpecificDate,
      MROAuthExpireDateEventOccurs: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MROAuthExpireDateEventOccurs,
      MRORequestDeadlineDate: (state) =>
        state.ConfigModule.apiResponseDataByLocation.oFields
          .MRORequestDeadlineDate,
    }),
  },
};
</script>

<style scoped>
.buttonBlockMobile {
  /* display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  bottom: 0px; */
  height: 120px;
  /* background-color: white;
  width: 100%;
  z-index: 1;
  box-shadow: 0px -3px 6px #ebeef3; */
}
.buttonBlockMobile button {
  width: 90%;
}
.footerBlockMobile {
  /* display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  height: 70px;
  bottom: 20px; */
  padding-bottom: 120px;
}
.pageContent {
  padding-bottom: 190px;
}
</style>
