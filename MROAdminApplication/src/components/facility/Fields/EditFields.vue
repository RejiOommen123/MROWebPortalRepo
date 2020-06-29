<template>
  <div id="demo">
    <v-row>
      <v-col cols="12">
        <form @submit.prevent="onSubmit">
          <v-card>
            <v-card-title style="padding-top:0px">
              Edit Fields For Facility - {{facilityName}}
              <v-spacer></v-spacer>
              <v-text-field
                v-model="search"
                append-icon="mdi-magnify"
                label="Search"
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>

            <!-- Facility List DataTable  -->
            <v-data-table
              :headers="headers"
              :items="gridData"
              :search="search"
              :footer-props="{
    'items-per-page-options': [5,10,20]
  }"
              :items-per-page="10"
            >
            <template v-slot:item.nFieldOrder="{ item }">

              <v-text-field
                type="number" min="1"
                v-model.number="item.nFieldOrder"
                v-show="item.nFieldOrder!=null" style="width: 4em"
              ></v-text-field>
              <label v-if="item.nFieldOrder==null" style="font-size:'18px'"><strong>-</strong></label>
              </template>
              <!-- Facility List Actions (Edit,Delete,Location and ManageField)  -->
              <template v-slot:item.actions="{ item }">
                <input
                  type="checkbox"
                  @click="toggleCheck(item.nFacilityFieldMapID)"
                  v-model="item.bShow"
                  :value="item.bShow"
                />
              </template>
            </v-data-table>
            <!-- End Facility List DataTable  -->
          </v-card>
          <div class="submit">
            <v-btn type="submit" color="primary">Save</v-btn>
            <v-btn to="/facility" type="submit" color="primary">Cancel</v-btn>
          </div>
        </form>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      search: "",
      headers: [
        {
          text: "Name",
          align: "start",
          value: "sFieldName",
          width: "60%"
        },
        { text: "Order", value: "nFieldOrder", width: "20%", sortable: false },
        { text: "Action", value: "actions", width: "20%", sortable: false }
      ],
      gridData: this.getGridData(),
      facilityName: ""
    };
  },
  mounted() {},
  methods: {
    // API to Get all Facilities
    getGridData() {
      this.$http
        .get("facilityfieldmaps/GetFieldsByFacilityID/" + this.$route.params.id)
        .then(
          response => {
            // get body data
            this.gridData = JSON.parse(response.bodyText)["fields"];
            this.facilityName = JSON.parse(response.bodyText)["faciName"];
            //console.log(this.gridData + "    " + this.faciName);
          },
          response => {
            // error callback
            this.gridData = response.body;
          }
        );
    },
    toggleCheck: function(id) {
      for (var i = 0; i < this.gridData.length; i++) {
        if (this.gridData[i].nFacilityFieldMapID == id) {
          this.gridData[i].bShow = !this.gridData[i].bShow;
        }
      }
    },
    onSubmit() {
      var FacilityFieldMapsList = this.gridData.map(function(item) {
        delete item.sFieldName;
        return item;
      });
      this.$http
        .post("facilityfieldmaps/EditFacilityFields/", FacilityFieldMapsList)
        .then(response => {
          if (response.ok == true) {
            this.$router.push("/facility");
          }
        });
    }
  }
};
</script>

<style scoped>
* {
  margin: 5px;
}

#demo {
  margin: 0 125px;
}
button,
a {
  margin-top: 10px;
  margin-right: 20px;
}

.submit {
  text-align: center;
}
</style>