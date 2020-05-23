<template>
  <div class="center">
    <h4>What's Your Email ID ?</h4>
    <p>We'll email you a confirmation of your request when you're finished</p>
    <div>
      <!-- Email ID -->
      <div class="control-group" :class="{invalid: $v.emailID.$error}">
        <label for="emailID" class="control-label">Email ID:</label>
        <div>
          <input
            class="controls"
            id="emailID"
            placeholder="Email ID Please"
            type="text"
            v-model="emailID"
            @input="$v.emailID.$touch()"
          />
        </div>

        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.emailID.$error}"
          v-if="$v.emailID.$error && !$v.emailID.required"
        >Required Field</span>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.emailID.$error}"
          v-if="$v.emailID.$error && !$v.emailID.email"
        >Enter a valid Email ID</span>
      </div>
<br>
      <!-- Confirm Email ID -->
      <div class="control-group" :class="{invalid: $v.confirmEmailID.$error}">
        <label for="confirmEmailID" class="control-label">Confirm Email ID:</label>
        <div>
          <input
            class="controls"
            id="confirmEmailID"
            placeholder="Confirm Your Email ID"
            type="text"
            v-model="confirmEmailID"
            @input="$v.confirmEmailID.$touch()"
          />
        </div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.confirmEmailID.$error}"
          v-if="$v.confirmEmailID.$error"
        >Email & Confirm Email Fields Should Match</span>
      </div>
      <br>
      <!--Confirm Email Report-->
      <div class="control-group">
        <label for="confirmReport">
          <input
            type="checkbox"
            id="confirmReport"
            value="Mail"
            v-model="confirmReport"
            class="controls"
          /> Please email me a copy of my completed request form
        </label>
      </div>
      <br>
      <div>
        <button @click.prevent="nextPage" :disabled="$v.$invalid" class="btn btn-success">Next</button>
    </div>
    </div>
  </div>
</template>
<script>
import { required, email, sameAs } from "vuelidate/lib/validators";
export default {
 name:"FourthPage",
  data() {
    return {
      emailID: this.$store.state.appmodule.emailID,
      confirmEmailID: this.$store.state.appmodule.confirmEmailID,
      confirmReport: this.$store.state.appmodule.confirmReport
    };
  },
  validations: {
    emailID: {
      required,
      email
    },
    confirmEmailID: { sameAs: sameAs("emailID") }
  },
  methods:{
    nextPage(){
      this.$store.commit("appmodule/mutateemailID",this.emailID);
      this.$store.commit("appmodule/mutateconfirmEmailID",this.confirmEmailID);
      this.$store.commit("appmodule/mutateconfirmReport",this.confirmReport);
      this.$store.commit("appmodule/mutatepageNumerical",5);
      this.$store.commit("appmodule/mutateCurrentPage", "page-5");
    }
  }
}
</script>
<style scoped>
.center {
  text-align: center;
}
.invalid label {
  color: red;
}

.invalid input {
  border-bottom: 1px solid red;
}

.invalid span {
  color: red;
}
input {
  background: transparent;
  border: none;
  border-bottom: 1px solid #000000;
}
input:focus {
  outline: none;
}
</style>