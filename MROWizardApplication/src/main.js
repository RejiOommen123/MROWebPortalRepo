import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";

Vue.use(VueSignature);
Vue.use(VueResource);

Vue.http.options.root = process.env.VUE_APP_ROOT_URL;
Vue.config.productionTip = false;
//Vuetify API Secret Key - Common Code for Adding Header
Vue.http.interceptors.push((request, next) => {
    // console.log(request);
    var sAPIKey = "MRO@007"    
    if(request.url.indexOf('ringcaptcha') == -1){
        request.headers.set('sAPIKey', sAPIKey)
    }
    next()
})

Vue.use(Vuelidate);
new Vue({
    vuetify,
    store,
    render: h => h(App)
}).$mount('#app')