import Vue from 'vue'
import App from './App.vue'

import router from './router'
import store from './store'
import VueResource from 'vue-resource'

Vue.use(VueResource);
Vue.use(store);

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
