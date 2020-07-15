<template>
  <div class="center">
    <!-- <div class="disclaimer">{{disclaimer}}</div> -->
    <h1>Please tell us how we can make this tool better.</h1>
    <div class="form-group ">
      <div>
      <v-rating v-if="MROFeedbackRating" v-model="nFeedbackRating" background-color="green lighten-3" color="green" large></v-rating>
      </div>
      <div>
        <v-textarea
        v-if="MROFeedbackComment"
          filled
          width:100
          rows="4"
          row-height="30"
          shaped
          counter
          v-model="sFeedbackComment"
          label="Additional Details"
        ></v-textarea>
      </div>
    </div>
    <div>
      <v-btn @click.prevent="nextPage" class="next">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_23",
  data() {
    return {
      nFeedbackRating: 4,
      sFeedbackComment: "",
      // disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_23_disclaimer01,
      //Show and Hide Fields Values
      MROFeedbackRating : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackRating,
      MROFeedbackComment : this.$store.state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackComment
    };
  },
  methods: {
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
