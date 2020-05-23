<template>
    <div id="demo">
        <h2>Manage Fields For Customer - {{custName}}</h2>
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
        </demo-grid>
    </div>
</template>

<script>
    import GridComp from "./FieldsGrid.vue";
    export default {
        components: {
            'demo-grid': GridComp
        },
        data() {
            return {
                searchQuery: "",
                gridColumns: ["fieldName"],
                gridData: null,
                custName : ''
            }
        },
        mounted() {
            this.$http.get('http://localhost:57364/api/customer/EditFields/' + this.$route.params.id).then(response => {

                // get body data
                this.gridData = JSON.parse(response.bodyText)["wizards"];
                this.custName = JSON.parse(response.bodyText)["custName"];

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
    }
</style>