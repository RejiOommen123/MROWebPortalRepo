//Imports
import Vue from 'vue';
import Vuex from 'vuex';
// import VuexPersist from 'vuex-persist';

//Using Imports
Vue.use(Vuex);

// const vuexPersist = new VuexPersist({
//     key: 'MroAuth',
//     storage: window.localStorage
// });
export default new Vuex.Store({
    state: {
        pageheader: '',
        adminUserId:0
    },
    mutations: {
        mutatepageHeader(state, payload) {
            state.pageheader = payload;
        },
        adminUserId(state, payload) {
            state.adminUserId = payload;
        }
    },
    actions: {

    },
    getters: {

    }
    // ,
    // plugins: [vuexPersist.plugin]
})