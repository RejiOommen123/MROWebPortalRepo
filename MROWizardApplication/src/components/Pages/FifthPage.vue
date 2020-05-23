<template>
  <div class="center">
    <div class="control-group">
      <h4>What is Patient's Full Name ?</h4>
    </div>
      <!--Relative Name-->
      <div class="control-group" :class="{invalid: $v.fname.$error}">
        <label for="fname" class="control-label">First Name:</label>
        <div>
            <input
          class="controls"
          id="fname"
          placeholder="First Name Please"
          v-model="fname"
          @input="$v.fname.$touch()"
        /></div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.fname.$error}"
          v-if="$v.fname.$error && !$v.fname.required"
        >Required Field</span>
      </div>
      <br>
      <!-- last Name -->
      <div class="control-group" :class="{invalid: $v.lname.$error}">
        <label for="lname" class="control-label">Last Name:</label>
        <div>
            <input
          class="controls"
          id="lname"
          placeholder="Last Name Please"
          v-model="lname"
          @input="$v.lname.$touch()"
        /></div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.lname.$error}"
          v-if="$v.lname.$error && !$v.lname.required"
        >Required Field</span>
      </div>
      <br>
    <!-- Middle Initial -->
    <div class="control-group" :class="{invalid: $v.minitial.$error}">
        <label for="minitial" class="control-label">Middle Inital:</label>
        <div>
            <input
          class="controls"
          id="minitial"
          placeholder="Initial Please"
          v-model="minitial"
          @input="$v.minitial.$touch()"
        /></div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.minitial.$error}"
          v-if="$v.minitial.$error && !$v.minitial.minLen"
        >Please Provide Inital Only</span>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.minitial.$error}"
          v-if="$v.minitial.$error && !$v.minitial.maxLen"
        >Please Provide Inital Only</span>
      </div>
    <br>
    <div v-show="askPatientDeceased">
      <!--Confirm Email Report-->
      <div class="control-group">
        <label for="isPatientDeceased">
          <input
            type="checkbox"
            id="isPatientDeceased"
            value="Mail"
            v-model="isPatientDeceased"
            class="controls"
          /> Please Check this box if Patient is Deceased
        </label>
      </div>
    </div> 
    <div>
      <button @click.prevent="nextPage" :disabled="$v.$invalid" class="btn btn-success">Next</button>
    </div>
  </div>
</template>
<script>
import {
  required,
  maxLength,
  minLength
} from "vuelidate/lib/validators";
export default {
 name:"FifthPage",
    data(){return {
        fname:this.$store.state.appmodule.fname,
        lname:this.$store.state.appmodule.lname,
        minitial:this.$store.state.appmodule.minitial,
        isPatientDeceased:this.$store.state.appmodule.isPatientDeceased
        
    }},
    computed:{
      askPatientDeceased(){
        return this.$store.state.appmodule.askPatientDeceased
      }
    },
    validations:{
        fname:{required},
        lname:{required},
        minitial:{minLen:minLength(1),maxLen:maxLength(1)}
    },
  methods: {
    nextPage() {
      this.$store.commit("appmodule/mutatefname",this.fname);
      this.$store.commit("appmodule/mutatelname",this.lname);
      this.$store.commit("appmodule/mutateminitial",this.minitial);
      this.$store.commit("appmodule/mutateisPatientDeceased",this.isPatientDeceased);
      this.$store.commit("appmodule/mutatepageNumerical",6);
      this.$store.commit("appmodule/mutateCurrentPage","page-6");
      
    }
  }
};
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
