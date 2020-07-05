<template>
  <div id="dashboard">
    <!-- For Future Use -->
    <facility />
  </div>
</template>

<script>
//Facility is Imported & used as an element
import Facility from "../facility/Facility";

export default {
  data() {
    return {
      email: ""
    };
  },
  components: {
    facility: Facility
  },
  created() {}
};
</script>

<style scoped>
h1,
p {
    text-align: center;
}

p {
    color: red;
}
</style>