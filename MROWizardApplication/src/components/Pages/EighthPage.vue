<template>
  <div class="center">
    <br />
    <br />
    <!--B Day-->
    <div class="control-group" :class="{invalid: $v.bDay.$error}">
      <label for="bDay" class="control-label">
        <h4>When was the Patient Born ?</h4>
        <h4>(MM/DD/YYYY)</h4>
      </label>
      <div>
        <input
          type="text"
          class="controls"
          id="bDay"
          placeholder="MM/DD/YYYY"
          v-model="bDay"
          @input="$v.bDay.$touch()"
        />
      </div>
      <!-- :class="{invalid: $v.bDay.$error}" -->
      <!-- @input="$v.bDay.$touch()" -->
      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.bDay.$error}"
        v-if="$v.bDay.$error && !$v.bDay.required"
      >Required Field</span>
      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.bDay.$error}"
        v-if="$v.bDay.$error && !$v.bDay.minLen"
      >Please Enter Birth date in Specified Format</span>
      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.bDay.$error}"
        v-if="$v.bDay.$error && !$v.bDay.maxLen"
      >Please Enter Birth date in Specified Format</span>
    </div>

    <br />
    <div>
      <button @click.prevent="nextPage" :disabled="$v.$invalid" class="btn btn-success">Submit</button>
    </div>
  </div>
</template>

<script>
import { required, maxLength, minLength } from "vuelidate/lib/validators";
export default {
  name: "EighthPage",
  data() {
    return {
      bDay: this.$store.state.appmodule.bDay
    };
  },
  validations: {
    bDay: {
      required,
      minLen: minLength(10),
      maxLen: maxLength(10)
    }
  },
  methods: {
    nextPage() {
      //alert("Hello World");
      this.$store.state.appmodule.showBackBtn = true;
      this.$store.commit("appmodule/mutatebDay", this.bDay);
      this.$store.commit("appmodule/mutatepageNumerical", 9);
      this.$store.commit("appmodule/mutateCurrentPage", "page-9");
      
    }
  }
};
</script>

<style scoped>
.center {
  text-align: center;
}
.invalid h4 {
  color: black;
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