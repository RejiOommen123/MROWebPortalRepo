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
          maxlength="500"
          counter
          v-model="sFeedbackComment"
          label="Additional Details"
        ></v-textarea>
      </div>
    </div>
     <v-row>
    <v-col cols="6" offset-sm="3" sm="3">
      <v-btn @click.once="nextPage" :key="buttonKey" class="next">Rate Us</v-btn>
    </v-col>
    <v-col cols="6" sm="2">
      <v-btn @click.once="skipPage" :key="buttonKey" class="next">Skip</v-btn>
    </v-col>
    </v-row>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  name: "WizardPage_23",
    activated(){
    this.buttonKey++;
   },
  data() {
    return {
      nFeedbackRating: 4,
      sFeedbackComment: "",
      buttonKey:1,
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
  },
  computed:{
    ...mapState({
      disclaimer : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_25_disclaimer01,

      //Show and Hide Fields Values
      MROFeedbackRating :state => state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackRating,
      MROFeedbackComment : state => state.ConfigModule.apiResponseDataByLocation.oFields.MROFeedbackComment,
    }),
  }
};
</script>
