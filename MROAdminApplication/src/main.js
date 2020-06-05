import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueResource from 'vue-resource'
import vuetify from './plugins/vuetify';

Vue.use(VueResource);
// TODO
// Vue.http.options.root = "http://localhost:57364/api";
Vue.use(store);

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount('#app')
