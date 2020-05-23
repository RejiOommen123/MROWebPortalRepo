import Vue from 'vue';
import Vuex from 'vuex';
// import VuexPersist from 'vuex-persist'
import appmodule from './modules/appmodule'
import answermodule from './modules/answermodule'
Vue.use(Vuex);

// const vuexPersist = new VuexPersist({
//     key: 'my-answers',
//     storage: window.localStorage
// });


export const store = new Vuex.Store({

    modules: {
        appmodule,
        answermodule
    }
    //plugins: [vuexPersist.plugin]

});
