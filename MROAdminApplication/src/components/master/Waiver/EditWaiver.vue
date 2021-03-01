<template>
  <div>
    <div id="EditWaiverPageBox">
      <form @submit.prevent="onSubmit" class="editWaiver-form">
        <v-row>
          <v-col cols="12" offset-md="1" md="5">
            <label class="required" for="sWaiverName">Waiver Name:</label>
            <v-text-field
              type="text"
              id="sWaiverName"
              placeholder="Waiver Name"
              v-model="waiver.sWaiverName"
              @input="$v.waiver.sWaivereName.$touch()"
              @blur="$v.waiver.sWaiverName.$touch()"
              :error-messages="sWaiverNameErrors"
              solo
              counter
              maxlength="100"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="5">
            <label for="sFieldToolTip">Tooltip:</label>
            <v-text-field
              type="text"
              id="sFieldToolTip"
              placeholder="Enter Tooltip"
              v-model="waiver.sFieldToolTip"
              solo
              counter
              maxlength="500"
            ></v-text-field>
          </v-col>
        </v-row>
        <div class="submit">
          <v-btn type="submit" color="primary" :disabled="$v.waiver.$invalid">Save</v-btn>
          <v-btn to="/Master/Waiver" type="button" color="primary">Cancel</v-btn>
        </div>
         <v-row>
          <v-col cols="12" offset-md="1" md="10">
            {{noteMessage}}
          </v-col>
        </v-row> 
      </form>
    </div>
    <!-- Dialog Alert for errors Waiver -->
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
    waiver: {
      sWaiverName: {
        required,
        maxLength: maxLength(100),
        minLength: minLength(2),
      },
    },
  },
  computed: {
    sWaiverNameErrors() {
      const errors = [];
      if (!this.$v.waiver.sWaiverName.$dirty) return errors;
      !this.$v.waiver.sWaiverName.minLength &&
        errors.push("Waiver Name must be at least 2 characters long");
      !this.$v.waiver.sWaiverName.maxLength &&
        errors.push("Waiver Name must be at most 100 characters long");
      !this.$v.waiver.sWaiverName.required &&
        errors.push("Waiver Name is required.");
      return errors;
    },
  },
  name: "AddWaiver",
  data() {
    return {
      dialogLoader: false,
      errorAlert: false,
      errorMessage: "",
      waiver: {
        nWaiverID: 0,
        sWaiverName: "",
        sNormalizedWaiverName: "",
        sFieldToolTip: "",
        nWizardID: 0,
        nUpdatedAdminUserID: this.$store.state.adminUserId,
      },
      noteMessage:"Note :- Edit will only edit in the master table. However, changes in facility will have to be made manually by the authority."
    };
  },
  mounted() {
    this.dialogLoader = true;
    // API call to get Waiver
    this.$http
      .get(
        "Master/GetWaiver/sWaiverID=" +
          this.$route.params.id +
          "&sAdminUserID=" +
          this.$store.state.adminUserId
      )
      .then(
        (response) => {
          // get body data
          this.dialogLoader = false;
          this.waiver = JSON.parse(response.bodyText);
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
  methods: {
    onSubmit() {
      // API Call to edit Waiver
      this.waiver.nUpdatedAdminUserID = this.$store.state.adminUserId;
      this.dialogLoader = true;
      this.$http.post("Master/EditWaiver", this.waiver).then(
        (response) => {
          if (response.ok == true) {
            this.dialogLoader = false;
            //if reponse ok then redirect to Waiver List Page
            this.$router.push("/Master/Waiver");
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
  #EditWaiverPageBox {
    margin: 0 0px;
  }
  h1 {
    font-size: 14px;
  }
}
.submit {
  text-align: center;
}

.editWaiver-form {
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