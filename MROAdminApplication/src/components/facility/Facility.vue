<template>
    <div id="demo">
        <h1>Facilities List</h1>
        <form id="search">
            <div>
            Search <input name="query" v-model="searchQuery" /> 
            <button class="btn btn-primary">Search</button>
                </div><br>
            <router-link class="btn btn-success" to='/AddFacility' tag='button'>Add Facility</router-link>
        </form>
        <demo-grid id="griddemo"
                   :heroes="gridData"
                   :columns="gridColumns"
                   :filter-key="searchQuery">
        </demo-grid>
    </div>
</template>

<script>
    import GridComp from "./FacilitiesGrid.vue";
    export default {
        components: {
            'demo-grid': GridComp
        },
        data() {
            return {
                searchQuery: "",
                gridColumns: ["sFacilityName", "sDescription", "bActiveStatus"],
                gridData: null
            }
        },
        mounted() {
            this.$http.get('http://localhost:57364/api/facility/GetFacility').then(response => {
              
                // get body data
                this.gridData = JSON.parse(response.bodyText);

            }, response => {
                // error callback
                    this.gridData = response.body;
            });
        }
    }
</script>

<style scoped>
    * {
        margin: 10px;
    }
    #demo {
        margin: 0 100px;
        padding: 20px;
    }
    #search {
        padding-bottom:30px;
    }
</style>