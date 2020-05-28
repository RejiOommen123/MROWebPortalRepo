<template id="grid-template">
    <div style="overflow-x:auto;">
        <form @submit.prevent="onSubmit">
            <table>
                <thead>
                    <tr>
                        <th v-for="key in columns" :key=key
                            @click="sortBy(key)"
                            :class="{ active: sortKey == key }">
                            {{ key | capitalize | splitCap}}
                            <span class="arrow" :class="sortOrders[key] > 0 ? 'asc' : 'dsc'">
                            </span>
                        </th>
                        <th>
                            Is Enabled
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(entry, index) in filteredHeroes" :key=index>
                        <td v-for="key in columns" :key=key>
                            {{entry[key]}}
                        </td>
                        <td>
                            <input type="checkbox" @click="toggleCheck(entry.fieldId)" v-model="entry.isEnable" :value="entry['isEnable']" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="col-md-4 offset-md-3 submit">
                <button class="btn btn-primary" type="submit">Save</button>
            </div>
        </form>
    </div>
</template>

<script>
    export default {
        name: "demo-grid",
        props: {
            heroes: Array,
            columns: Array,
            filterKey: String
        },
        data: function () {
            var sortOrders = {};
            this.columns.forEach(function (key) {
                sortOrders[key] = 1;
            });
            return {
                sortKey: "",
                sortOrders: sortOrders
            };
        },
        computed: {
            filteredHeroes: function () {
                var sortKey = this.sortKey;
                var filterKey = this.filterKey && this.filterKey.toLowerCase();
                var order = this.sortOrders[sortKey] || 1;
                var heroes = this.heroes;
                if (filterKey) {
                    heroes = heroes.filter(function (row) {
                        return Object.keys(row).some(function (key) {
                            return (
                                String(row[key])
                                    .toLowerCase()
                                    .indexOf(filterKey) > -1
                            );
                        });
                    });
                }
                if (sortKey) {
                    heroes = heroes.slice().sort(function (a, b) {
                        a = a[sortKey];
                        b = b[sortKey];
                        return (a === b ? 0 : a > b ? 1 : -1) * order;
                    });
                }
                return heroes;
            }
        },
        filters: {
            capitalize: function (str) {
                return str.charAt(0).toUpperCase() + str.slice(1);
            },
            splitCap: function (str) {
                return str.match(/[A-Z][a-z]+|[0-9]+/g).join(" ")
            }
        },
        methods: {
            sortBy: function (key) {
                this.sortKey = key;
                this.sortOrders[key] = this.sortOrders[key] * -1;
            },
            toggleCheck: function(id){
                for (var i = 0; i < this.heroes.length; i++) {
                    if (this.heroes[i].fieldId == id) {
              
                        this.heroes[i].isEnable = !this.heroes[i].isEnable;
                    }
                }
            },
            onSubmit() {
                
                var WizardsFacilityMapObjList = this.heroes.map(function (item) {
                    delete item.fieldName;
                    delete item.pageId;
                    return item;
                });
                this.$http.post('http://localhost:57364/api/facility/EditFields/', WizardsFacilityMapObjList)
                    .then(response => {
                        if (response.ok == true) {
                            this.$router.push('/dashboard')
                        }
                    });
            }
        }
    }
</script>

<style scoped>
    * {
        margin:10px;
    }
    body {
        font-family: Helvetica Neue, Arial, sans-serif;
        font-size: 14px;
        color: #444;
    }

    table {
        border: 2px solid #42b983;
        border-radius: 3px;
        background-color: #fff;
    }

    th {
        background-color: #42b983;
        color: rgba(255, 255, 255, 0.66);
        cursor: pointer;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
    }

    td {
        background-color: #f9f9f9;
    }

    th,
    td {
        min-width: 200px;
        padding: 10px 20px;
    }

        th.active {
            color: #fff;
        }

            th.active .arrow {
                opacity: 1;
            }

    .arrow {
        display: inline-block;
        vertical-align: middle;
        width: 0;
        height: 0;
        margin-left: 5px;
        opacity: 0.66;
    }

        .arrow.asc {
            border-left: 4px solid transparent;
            border-right: 4px solid transparent;
            border-bottom: 4px solid #fff;
        }

        .arrow.dsc {
            border-left: 4px solid transparent;
            border-right: 4px solid transparent;
            border-top: 4px solid #fff;
        }
</style>