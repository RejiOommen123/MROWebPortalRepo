<template>
  <div>
    <v-dialog
      v-model="isaut"
      max-width="280"
      light
      persistent
    >
      <v-card>
        <v-card-title class="headline">System Issue</v-card-title>

        <v-card-text>
          <p>{{disclaimer05}}</p>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click.prevent="logoutUser"
          >
            ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
    name: 'Unauthorized',
    data: function () {
        return {
            dialog:false,
            disclaimer05: this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
            .wizardHelper.Wizard_01_disclaimer05,
        }
    },
    computed: {
       isaut() {
        return this.$store.state.ConfigModule.bUnauthorized;
      }
    },
    methods: {
        logoutUser: function() {
            // Laravel            
            //this.dialog = false;
            this.$store.commit(
                  "ConfigModule/bUnauthorized",
                  false);
            location.reload();
        },
    }
}
</script>