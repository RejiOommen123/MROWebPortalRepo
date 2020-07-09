import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";

Vue.use(VueSignature);
Vue.use(VueResource);
//REplace Root - TODO: Reji
//Razor-QA
//Vue.http.options.root = "https://devmroportalapi.azurewebsites.net/api";

//MRO DEV
//Vue.http.options.root = "https://mroadmindevapi.azurewebsites.net/api";

//MRO UAT
Vue.http.options.root = "https://mroadminuatapi.azurewebsites.net/api";
//Vue.http.options.root = "https://devmroportalapi-dev.azurewebsites.net/api";
//Vue.http.options.root = "http://localhost:57364/api";
Vue.config.productionTip = false;
Vue.use(Vuelidate);

new Vue({
    vuetify,
    store,
    render: h => h(App)
}).$mount('#app')