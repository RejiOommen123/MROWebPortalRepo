<template>
  <div class="center">
    <div class="form-group">
      <h5>Are you the Patient ?</h5>
      <div class="form-group btn-group-vertical" role="toolbar">

          <button @click.prevent="setPatient" class="form-control btn btn-success">Yes</button><br>


          <button @click.prevent="setNotPatient" class="form-control btn btn-success">No</button>

      </div>
      <div class="form-group" v-show="notPatient">
      <!--Relative Name-->
      <div class="control-group" :class="{invalid: $v.rname.$error}">
        <label for="rname" class="control-label">Your Full Name:</label>
        <div>
            <input
          class="controls"
          id="rname"
          placeholder="Full Name Please"
          v-model="rname"
          @input="$v.rname.$touch()"
        /></div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.rname.$error}"
          v-if="$v.rname.$error && !$v.rname.required"
        >Required Field</span>
      </div>
      <br>
      <!-- Relation With Patient -->
      <div class="control-group" :class="{invalid: $v.relationToPatient.$error}">
        <label for="relationToPatient" class="control-label">Relation With Patient:</label> 
        <div><input
          class="controls"
          id="relationToPatient"
          placeholder="Relation With Patient"
          v-model="relationToPatient"
          @input="$v.relationToPatient.$touch()"
        /></div>
        <span
          class="col-md-10 offset-md-2"
          :class="{invalid: $v.relationToPatient.$error}"
          v-if="$v.relationToPatient.$error && !$v.relationToPatient.required"
        >Required Field</span>
      </div>
      <br>
      <div><button @click.prevent="continueAhead" :disabled="$v.$invalid" class="btn btn-success btn-sm">Continue</button></div>
    </div>
    <div
      class="alert alert-info"
    >*If you're not the patient, you'll have the opportunity to supply additional documentation to verify that you're authorized to make this request.</div>
  </div>
    </div>
</template>

<script>
import { required } from "vuelidate/lib/validators";
export default {
  name: "ThirdPage",
  data() {
    return {
      rname: this.$store.state.appmodule.rname,
      relationToPatient: this.$store.state.appmodule.relationToPatient
    };
  },
  validations: {
    rname: { required },
    relationToPatient: { required }
  },
  computed: {
    notPatient() {
      return this.$store.state.appmodule.notPatient;
    }
  },
  methods: {
    setPatient() {
      this.$store.state.appmodule.isPatientDeceased = false;
      this.$store.commit("appmodule/mutateaskPatientDeceased",false);
      this.$store.commit("appmodule/mutateNotPatient", false);
      this.$store.commit("appmodule/mutatepageNumerical",4);
      this.$store.commit("appmodule/mutateCurrentPage", "page-4");
    },
    setNotPatient() {
      this.$store.commit("appmodule/mutateNotPatient", true);
    },
    continueAhead(){
      this.$store.state.appmodule.isPatientDeceased = false;
      this.$store.commit("appmodule/mutateaskPatientDeceased",true);

      this.$store.commit("appmodule/mutatername",this.rname);
      this.$store.commit("appmodule/mutaterelationToPatient",this.relationToPatient);
      this.$store.commit("appmodule/mutatepageNumerical",4);
      this.$store.commit("appmodule/mutateCurrentPage", "page-4");
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
input{
    background: transparent;
    border: none;
    border-bottom: 1px solid #000000;
}
input:focus{
    outline: none;
}
</style>