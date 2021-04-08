<template>
  <div>
    <v-dialog v-model="LoaderDialog.visible" persistent width="300">
      <v-card color="primary" dark>
        <v-card-text>
          {{LoaderDialog.title}}
          <v-progress-linear
            indeterminate
            color="white"
            class="mb-0"
          ></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "LoaderDialog",
  computed: {
    ...mapState({
      LoaderDialog: (state) => state.ConfigModule.LoaderDialog,
    }),
  }
};
</script>