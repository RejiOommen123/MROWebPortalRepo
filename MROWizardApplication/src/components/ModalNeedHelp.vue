<template>
  <div>
    <v-dialog v-model="bShow" light persistent content-class="needHelp">
      <v-card >
        <v-card-title >
          <v-btn style="font-size:18px;color:black"  class="wizardClose" icon dark @click="closeShowNeedHelp()" >
            <v-icon>mdi-close</v-icon>
          </v-btn>  
          <v-row>
            <v-col cols="11" sm="11">
            <h3>Your Details</h3>
            </v-col>
            <v-col cols="11" sm="11">
            <h5>Let us know how to get back to you.</h5>
            </v-col>
          </v-row>
        </v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" sm="4" >
              <label style="color:black" class="required" for="sName">Name:</label>
              <v-text-field
                solo
                v-model="sName"
                :error-messages="sNameError"
                required
                maxlength="50"
                @input="$v.sName.$touch()"
                @blur="$v.sName.$touch()"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="4" >
              <label style="color:black" class="required" for="sPhoneNo">Phone Number:</label>
              <v-text-field
                maxlength="10"
                type="number"
                solo
                v-model="sPhoneNo"
                :error-messages="sPhoneNoError"
                required
                @input="$v.sPhoneNo.$touch()"
                @blur="$v.sPhoneNo.$touch()"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="4" >
              <label style="color:black" class="required" for="sEmail">Email Address:</label>
              <v-text-field
                solo
                v-model="sEmail"
                :error-messages="sEmailError"
                required
                maxlength="30"
                @input="$v.sEmail.$touch()"
                @blur="$v.sEmail.$touch()"
              ></v-text-field>
            </v-col>
            <v-col cols="12"  sm="12">         
              <label style="color:black" class="required" for="sMessage">Message:</label>    
              <v-textarea    
                solo 
                rows="3"
                row-height="30"
                maxlength="250"
                shaped counter v-model="sMessage"></v-textarea>    
            </v-col>
            <v-col cols="3" sm="3">             
              <v-btn  
                :disabled="$v.$invalid"
                style=" text-transform: none;"
                color="#30c4b0"
                @click="onSubmit()"
              >Send Message</v-btn>    
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
    <!-- Loader to indicate wizard is getting ready -->
        <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="primary" dark>
            <v-card-text>
              Please stand by
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
    <!-- Help mail send success -->
        <v-dialog v-model="dialogSuccess" persistent width="300">
          <v-card dark>
            <v-card-text>
              {{disclaimer04}}
              <v-btn  
                class="justify-center" 
                style=" text-transform: none;"
                color="#30c4b0"
                @click="dialogSuccess=false"
              >Close</v-btn>    
            </v-card-text>
          </v-card>
        </v-dialog> 
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required, maxLength, minLength, email } from "vuelidate/lib/validators";
export default {
  name: "NeedHelp",
  mixins: [validationMixin],
  validations: {
    sName: { required, maxLength: maxLength(50) },
    sPhoneNo: { required, maxLength: maxLength(10), minLength: minLength(10) },
    sEmail: { required, email},
    sMessage: { required }
  },
  data: function () {
    return {
        sName:'',
        sPhoneNo:'',
        sEmail:'',
        sMessage:'',
        dialog:true,
        dialogLoader:false,
        dialogSuccess:false,        
        disclaimer04: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
        .wizardHelper.Wizard_01_disclaimer04,
    };
  },
  computed: {
    sNameError() {
      const errors = [];
      if (!this.$v.sName.$dirty) return errors;
      !this.$v.sName.maxLength &&
        errors.push("Name must be at most 30 characters long");
      !this.$v.sName.required &&
        errors.push("Name is required.");
      return errors;
    },
    sPhoneNoError() {
      const errors = [];
      if (!this.$v.sPhoneNo.$dirty) return errors;
      !this.$v.sPhoneNo.maxLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.sPhoneNo.minLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.sPhoneNo.required && errors.push("Mobile No Required");
      return errors;
    },
    sEmailError() {
      const errors = [];
      if (!this.$v.sEmail.$dirty) return errors;
      !this.$v.sEmail.email &&
        errors.push("Must be valid e-mail");
      !this.$v.sEmail.required &&
        errors.push("E-mail is required");
      return errors;
    },
    sMessageError() {
      const errors = [];
      if (!this.$v.sMessage.$dirty) return errors;
      !this.$v.sMessage.required &&
        errors.push("Message is required");
      return errors;
    },
    bShow() {
      return this.$store.state.ConfigModule.bShowNeedHelp;
    }
  },
  methods: {
   closeShowNeedHelp(){
     this.$store.commit("ConfigModule/bShowNeedHelp",false);
   },
   onSubmit(){
     var combinedObj = {
        oRequester: this.$store.state.requestermodule,
        sName: this.sName,
        sPhoneNo: this.sPhoneNo,
        sEmail: this.sEmail,
        sMessage: this.sMessage
      };
      this.$http.post("wizards/SendNeedHelpEmail", combinedObj).then(
        response => {
          if (response.ok == true) {
            this.dialogLoader = false;
            this.dialogSuccess=false;
          }
        },
        error => {
          // Error Callback
          if (error.status == 400) {
            this.dialogLoader = false;
          }
        }
      );
     this.$store.commit("ConfigModule/bShowNeedHelp",false);
   }
  }
};
</script>