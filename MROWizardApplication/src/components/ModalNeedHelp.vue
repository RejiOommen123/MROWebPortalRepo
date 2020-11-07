<template>
  <div>
    <v-dialog v-model="dialog" light persistent content-class="needHelp">
      <v-card >
        <v-card-title >
          <v-btn style="font-size:36px;color:black"  class="wizardClose" icon dark @click="dialogConfirm=true" >
            <v-icon>mdi-close</v-icon>
          </v-btn>  
          <v-row>
            <v-col cols="12" sm="12">
            <h3>Your Details</h3>
            </v-col>
            <v-col cols="12" sm="12">
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
                maxlength="30"
                @input="$v.sName.$touch()"
                @blur="$v.sName.$touch()"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="4" >
              <label style="color:black" class="required" for="nPhoneNo">Phone Number:</label>
              <v-text-field
                solo
                v-model="nPhoneNo"
                :error-messages="nPhoneNoError"
                required
                maxlength="30"
                @input="$v.nPhoneNo.$touch()"
                @blur="$v.nPhoneNo.$touch()"
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
                style=" text-transform: none;"
                color="#30c4b0"
              >Send Message</v-btn>    
            </v-col>
          </v-row>
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
    sName: { required, maxLength: maxLength(30) },
    nPhoneNo: { required, maxLength: maxLength(10), minLength: minLength(10) },
    sEmail: { required, email},
  },
  data: function () {
    return {
        sName:'',
        nPhoneNo:'',
        sEmail:'',
        sMessage:'',
        dialog:true
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
    nPhoneNoError() {
      const errors = [];
      if (!this.$v.nPhoneNo.$dirty) return errors;
      !this.$v.nPhoneNo.maxLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.nPhoneNo.minLength && errors.push("Enter 10 digit mobile no.");
      !this.$v.nPhoneNo.required && errors.push("Mobile No Required");
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
  },
  methods: {
   
  }
};
</script>