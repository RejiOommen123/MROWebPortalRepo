import 'babel-polyfill'
import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import VueResource from 'vue-resource';
import vuetify from './plugins/vuetify';
import Vuelidate from 'vuelidate';

//Using Imported Items
Vue.use(Vuelidate);
Vue.use(VueResource);
Vue.use(store);

//HTTP Calls 
//Change URL as per Environment
//Ensure that there is No '/' at the end of the URL
//Vue.http.options.root = "https://devmroportalapi.azurewebsites.net/api";
//Vue.http.options.root = "https://devmroportalapi-dev.azurewebsites.net/api";
Vue.http.options.root = "http://localhost:57364/api";

Vue.config.productionTip = false;

//Vue Instance
new Vue({
    vuetify,
    router,
    store,
    render: h => h(App)
}).$mount('#app')