<template>
<div class="center">
    <div class="control-group">
        <br>
      
      <!--Postal Code-->
      <div class="control-group" :class="{invalid: $v.postalCode.$error}">
        <label for="postalCode" class="control-label"><h4>What is Patient's Postal Code?</h4></label>
        <div>
            <input
            type="number"
          class="controls"
          id="postalCode"
          placeholder="Postal Code Please"
          v-model="postalCode"
          @input="$v.postalCode.$touch()"
        /></div>
              <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.postalCode.$error}"
        v-if="$v.postalCode.$error && !$v.postalCode.required"
      >Required Field</span>
      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.postalCode.$error}"
        v-if="$v.postalCode.$error && !$v.postalCode.numeric"
      >Numeric Field</span>

      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.postalCode.$error}"
        v-if="$v.postalCode.$error && !$v.postalCode.minL"
      >Postal Code Should be of 6 Charactersr</span>

      <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.postalCode.$error}"
        v-if="$v.postalCode.$error && !$v.postalCode.maxL"
      >Postal Code Should be of 6 Charactersr</span>
      </div>
    </div>
    <br>
    <div>
      <button @click.prevent="nextPage" :disabled="$v.$invalid" class="btn btn-success">Next</button>
    </div>  
</div>
</template>
<script>
import {
required,
  maxLength,
  minLength,
  numeric
} from "vuelidate/lib/validators";
export default {
 name:"SixthPage",
    data(){
        return{
            postalCode:this.$store.state.appmodule.postalCode
        }
    },
    validations:{
        postalCode:{required,maxL:maxLength(6),minL:minLength(6),numeric}
    },
    methods:{
        nextPage(){
            //alert("Hello World");
            this.$store.commit("appmodule/mutatepostalCode",this.postalCode);
            this.$store.commit("appmodule/mutatepageNumerical",7);
            this.$store.commit("appmodule/mutateCurrentPage","page-7");
        }
    }
}
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