//Imports
import Vue from 'vue';
import Vuex from 'vuex';
import VuexPersist from 'vuex-persist';

//Using Imports
Vue.use(Vuex);

const vuexPersist = new VuexPersist({
    key: 'MroAuth',
    storage: window.localStorage
});
export default new Vuex.Store({
    state: {
        pageheader: ''
    },
    mutations: {
        mutatepageHeader(state, payload) {
            state.pageheader = payload;
        }
    },
    actions: {

    },
    getters: {

    },
    plugins: [vuexPersist.plugin]
})