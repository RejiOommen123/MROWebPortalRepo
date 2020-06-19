import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";

Vue.use(VueSignature);
Vue.use(VueResource);
Vue.config.productionTip = false
Vue.use(Vuelidate)

new Vue({
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
