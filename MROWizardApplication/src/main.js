import Vue from 'vue'
import App from './App.vue'
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";

Vue.use(VueSignature);
Vue.use(VueResource);
Vue.config.productionTip = false

Vue.use(Vuelidate)
// Vue.http.options.root = 'http://localhost:57364/api/';
var vm = new Vue({
  store,
  render: h => h(App),
})

vm.$mount('#app')
