<template>
  <div class="center">
    <div class="disclaimer">{{disclaimer}}</div>
    <h1>Please tell us how we can make this tool better?</h1>
    <div class="form-group ">
      <div>
      <v-rating v-model="rating" background-color="green lighten-3" color="green" large></v-rating>
      </div>
      <div>
        <v-textarea
          filled
          width:100
          rows="4"
          row-height="30"
          shaped
          counter
          v-model="feedback"
          label="Additional Details"
        ></v-textarea>
      </div>
    </div>
    <div>
      <v-btn @click.prevent="nextPage" color="success">Next</v-btn>
    </div>
  </div>
</template>

<script>
export default {
  name: "WizardPage_23",
  data() {
    return {
      rating: 4,
      feedback: ""
    };
  },
  methods: {
    nextPage() {
      this.$store.commit("requestermodule/mutaterating", this.rating);
      this.$store.commit("requestermodule/mutatefeedback", this.feedback);
      this.$store.commit("ConfigModule/mutateNextIndex");
    }
  },
  computed: {
    disclaimer() {
      return this.$store.state.ConfigModule.wp23_disclaimer;
    }
  }
};
</script>

<style scoped>
</style>