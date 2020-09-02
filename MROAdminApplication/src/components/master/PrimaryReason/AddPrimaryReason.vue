<template>
  <div>
    <div id="AddPrimaryReasonPageBox">
      <form @submit.prevent="onSubmit" class="addPrimaryReason-form">
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label class="required" for="sPrimaryReasonName">Primary Reason Name:</label>
            <v-text-field
              type="text"
              id="sPrimaryReasonName"
              placeholder="Primary Reason Name"
              v-model="primaryReason.sPrimaryReasonName"
              @input="$v.primaryReason.sPrimaryReasonName.$touch()"
              @blur="$v.primaryReason.sPrimaryReasonName.$touch()"
              :error-messages="sPrimaryReasonNameErrors"
              solo
              maxlength="100"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="5">
            <label for="sFieldToolTip">Tooltip:</label>
            <v-text-field
              type="text"
              id="sFieldToolTip"
              placeholder="Enter Tooltip"
              v-model="primaryReason.sFieldToolTip"
              solo
            ></v-text-field>
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="$v.primaryReason.$invalid">Save</v-btn>
          <v-btn to="/Master/PrimaryReason" type="button" color="primary">Cancel</v-btn>
        </div>
      </form>
    </div>
    <!-- Dialog Alert for errors Primary Reason -->
    <v-dialog v-model="errorAlert" width="350px" max-width="360px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>{{errorMessage}}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="errorAlert = false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <!-- Common Loader -->
    <v-dialog v-model="dialogLoader" persistent width="300" id="dialogLoader">
      <v-card color="rgb(0, 91, 168)" dark>
        <v-card-text>
          Please stand by
          <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, minLength, maxLength } from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],
  validations: {
    primaryReason: {
      sPrimaryReasonName: {
        required,
        maxLength: maxLength(100),
        minLength: minLength(2),
      },
    },
  },
  computed: {
    sPrimaryReasonNameErrors() {
      const errors = [];
      if (!this.$v.primaryReason.sPrimaryReasonName.$dirty) return errors;
      !this.$v.primaryReason.sPrimaryReasonName.minLength &&
        errors.push("Primary Reason Name must be at least 2 characters long");
      !this.$v.primaryReason.sPrimaryReasonName.maxLength &&
        errors.push("Primary Reason Name must be at most 100 characters long");
      !this.$v.primaryReason.sPrimaryReasonName.required &&
        errors.push("Primary Reason Name is required.");
      return errors;
    },
  },
  name: "AddPrimaryReason",
  data() {
    return {
      dialogLoader: false,
      errorAlert: false,
      errorMessage: "",
      primaryReason: {
        nPrimaryReasonID: 0,
        sPrimaryReasonName: "",
        sNormalizedPrimaryReasonName: "",
        sFieldToolTip: "",
        nWizardID: 0,
        nCreatedAdminUserID: this.$store.state.adminUserId,
        nUpdatedAdminUserID: this.$store.state.adminUserId,
      },
    };
  },
  methods: {
    onSubmit() {
      // API Call to add Primary Reason
      this.dialogLoader = true;
      this.$http.post("Master/AddPrimaryReason", this.primaryReason).then(
        (response) => {
          if (response.ok == true) {
            this.dialogLoader = false;
            //if reponse ok then redirect to Primary Reason List Page
            this.$router.push("/Master/PrimaryReason");
          }
        },
        (error) => {
          // Error Callback
          if (error.status == 400) {
            this.dialogLoader = false;
            this.errorMessage = error.body;
            this.errorAlert = true;
          }
        }
      );
    },
  },
};
</script>

<style scoped>
button,
a {
  margin-right: 1.25em;
}
@media screen and (max-width: 500px) {
  #AddPrimaryReasonPageBox {
    margin: 0 0px;
  }
  h1 {
    font-size: 14px;
  }
}
.submit {
  text-align: center;
}

.addPrimaryReason-form {
  margin: 0.625em auto;
  border: 0.0625em solid #eee;
  padding: 1.25em;
  box-shadow: 0 0.125em 0.1875em #ccc;
  font-size: 0.9375em;
}

.input label {
  display: block;
  color: #4e4e4e;
  margin-bottom: 0.375em;
}

.submit button {
  font: inherit;
  cursor: pointer;
}

.submit button[disabled],
.submit button[disabled]:hover,
.submit button[disabled]:active {
  border: 0.078125em solid #ccc;
  background-color: transparent;
  color: #ccc;
  cursor: not-allowed;
}
i {
  color: rgb(40, 40, 40);
}
label {
  margin-top: 0.25em;
  margin-left: -0.0625em;
}
.row {
  margin: 0.0625em;
}
.required:after {
  content: " *";
  color: red;
}
</style>