<template>
<div class="center">
<br>
      
      <!--Street Area-->
      <div class="control-group" :class="{invalid: $v.streetArea.$error}">
        <label for="streetArea" class="control-label"><h4>What is Patient's Street Area?</h4></label>
        <div>
            <input
            type="textarea"
          class="controls"
          id="streetArea"
          placeholder="Street Area Please"
          v-model="streetArea"
          @input="$v.streetArea.$touch()"
        /></div>
              <span
        class="col-md-10 offset-md-2"
        :class="{invalid: $v.streetArea.$error}"
        v-if="$v.streetArea.$error && !$v.streetArea.required"
      >Required Field</span>
      </div>
    <br>
    <div>
      <button @click.prevent="nextPage" :disabled="$v.$invalid" class="btn btn-success">Next</button>
    </div>

</div>  
</template>

<script>
import {
  required
} from "vuelidate/lib/validators";
export default {
 name:"SeventhPage",
    data(){
        return{
            streetArea:this.$store.state.appmodule.streetArea
        }
    },
    validations:{
        streetArea:{required}
    },
    methods:{
        nextPage(){
            //alert("Hello World");
            this.$store.commit("appmodule/mutatestreetArea",this.streetArea);
            this.$store.commit("appmodule/mutatepageNumerical",8);
            this.$store.commit("appmodule/mutateCurrentPage","page-8");
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