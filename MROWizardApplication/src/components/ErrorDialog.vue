<template>
  <div>
    <v-dialog v-model="ErrorDialog.visible" max-width="280" light persistent>
      <v-card>
        <v-card-title class="headline">{{ ErrorDialog.title }}</v-card-title>

        <v-card-text>
          <p>{{ ErrorDialog.body }}</p>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click.prevent="closeDialog">
            ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  name: "ErrorDialog",
  computed: {
    ...mapState({
      ErrorDialog: (state) => state.ConfigModule.ErrorDialog,
    }),
  },
  methods: {
    closeDialog: function () {
      var ErrorDialog = {
        visible: false,
      };
      this.$store.commit("ConfigModule/ErrorDialog", ErrorDialog);
    },
  },
};
</script>