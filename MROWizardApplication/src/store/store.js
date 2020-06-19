import Vue from 'vue';
import Vuex from 'vuex';
// import VuexPersist from 'vuex-persist'
import requestermodule from './modules/RequesterModule'
import ConfigModule from './modules/ConfigModule'
Vue.use(Vuex);

// const vuexPersist = new VuexPersist({
//     key: 'my-answers',
//     storage: window.localStorage
// });


export const store = new Vuex.Store({

    modules: {
        requestermodule,
        ConfigModule
    }
    //plugins: [vuexPersist.plugin]

});
