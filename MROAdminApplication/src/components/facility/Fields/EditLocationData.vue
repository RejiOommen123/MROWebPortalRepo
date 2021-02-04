<template>
  <div id="EditFacilityDataBox">
    <v-tabs v-model="tab" background-color="rgb(0, 91, 168)" dark>
      <v-tab>Edit Fields</v-tab>
      <v-tab>Edit Disclaimers</v-tab>
    </v-tabs>

    <v-tabs-items v-model="tab">
      <v-tab-item>
        <EditLocationFields></EditLocationFields>
      </v-tab-item>
      <v-tab-item>
        <EditLocationDisclaimers></EditLocationDisclaimers>
      </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
import EditLocationFields from "./EditLocationFields.vue";
import EditLocationDisclaimers from "./EditLocationDisclaimers.vue";
export default {
  components: {
    EditLocationFields,
    EditLocationDisclaimers,
  },
  data() {
    return {
      tab: null,
    };
  },
};
</script>

<style scoped>
@media screen and (max-width: 500px) {
  #EditFacilityDataBox {
    margin: 0 0em;
  }
}
.v-tab {
  text-transform: none !important;
}
</style>