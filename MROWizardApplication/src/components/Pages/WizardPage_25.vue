<template>
  <div class="center">
    <!-- <div class="disclaimer">{{disclaimer}}</div> -->
    <h1>How would you rate this tool?</h1>
    <div class="form-group ">
      <div>
      <v-rating v-if="MROFeedbackRating" v-model="nFeedbackRating" background-color="green lighten-3" color="green" large></v-rating>
      </div>
      <div class="subHeadings" style="margin-top:20px">{{disclaimer}}</div>
      <div>
        <v-textarea
        class="TextAreaBg" 
        v-if="MROFeedbackComment"
          
          width:100
          rows="4"
          row-height="30"
          shaped
          maxlength="500"
          counter
          v-model="sFeedbackComment"
          label="Additional Details"
        ></v-textarea>
      </div>
    </div>
     <v-row>
    <v-col cols="6" offset-sm="3" sm="3">
      <v-btn @click.prevent="nextPage" class="next">Rate Us</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.prevent="skipPage" class="next">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "WizardPage_23",
  data() {
    return {
      nFeedbackRating: 4,
      sFeedbackComment: "",

      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_25_disclaimer01,

      //Show and Hide Fields Values
      MROFeedbackRating : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackRating,
      MROFeedbackComment : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackComment,
    };
  },
  methods: {
     skipPage(){
      this.nFeedbackRating=0;
      this.sFeedbackComment='';
      this.nextPage();
    },
    nextPage() {
      this.$store.commit("requestermodule/nFeedbackRating", this.nFeedbackRating);
      this.$store.commit("requestermodule/sFeedbackComment", this.sFeedbackComment);

      //Partial Requester Data Save Start
      this.$store.commit("requestermodule/sWizardName", this.$store.state.ConfigModule.selectedWizard);
        this.$http.post("requesters/AddRequester/",this.$store.state.requestermodule)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
        });
      //Partial Requester Data Save End
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
