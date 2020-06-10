<template>
  <div id="demo">
    <!-- <h2>Manage Fields For Facility - {{faciName}}</h2>
        <h2></h2>
        <form id="search">
            <div>
                Search <input name="query" v-model="searchQuery" />
                <button class="btn btn-primary">Search</button>
            </div><br>
        </form>
        <demo-grid id="griddemo"
                   :heroes="gridData"
                   :columns="gridColumns"
                   :filter-key="searchQuery">
    </demo-grid>-->
            <form @submit.prevent="onSubmit">

    <v-card>
      <v-card-title>
        Manage Fields For Facility - {{facilityName}}
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
      <v-data-table :headers="headers" :items="gridData" :search="search">
        <!-- Facility List Actions (Edit,Delete,Location and ManageField)  -->
        <template v-slot:item.actions="{ item }">
         <input type="checkbox" @click="toggleCheck(item.nFieldID)" v-model="item.bShow" :value="item.bShow" />
        </template>
      </v-data-table>
      <!-- End Facility List DataTable  -->
    </v-card>
    <div class="col-md-6 offset-md-3 submit">
            <v-btn type="submit" color="primary" >Save</v-btn>
            <v-btn to="/facility" type="submit" color="primary" >Cancel</v-btn>
    </div>
            </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      search: "",
      headers: [
        {
          text: "Field Name",
          align: "start",
          value: "sFieldName"
        },
        { text: "Actions", value: "actions", sortable: false }
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
        .get(
          "http://localhost:57364/api/facilityfieldmaps/GetFieldsByFacilityID/" +
            this.$route.params.id
        )
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
    toggleCheck: function(id){
                for (var i = 0; i < this.gridData.length; i++) {
                    if (this.gridData[i].nFieldID == id) {
              
                        this.gridData[i].bShow = !this.gridData[i].bShow;
                    }
                }
            },
    onSubmit() {
        
        var FacilityFieldMapsList = this.gridData.map(function (item) {
            delete item.sFieldName;
            return item;
        });
        this.$http.post('http://localhost:57364/api/facilityfieldmaps/EditFacilityFields/', FacilityFieldMapsList)
            .then(response => {
                if (response.ok == true) {
                    this.$router.push('/dashboard')
                }
            });
    }
  }
};
</script>

<style scoped>
* {
  margin: 10px;
}

#demo {
  margin: 0 100px;
}
</style>