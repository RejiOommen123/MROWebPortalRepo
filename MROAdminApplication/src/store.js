import Vue from 'vue'
import Vuex from 'vuex'
import VuexPersist from 'vuex-persist'
Vue.use(Vuex)

 const vuexPersist = new VuexPersist({
     key: 'MroAuth',
     storage: window.localStorage
 });
export default new Vuex.Store({
  state: {
        IsSignIn: false
  },
  mutations: {
      mutateIsSignIn(state, payload) {
          state.IsSignIn = payload;
      }
  },
  actions: {

  },
  getters: {

    },
    plugins: [vuexPersist.plugin]
})