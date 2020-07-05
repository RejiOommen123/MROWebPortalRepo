import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import VueResource from 'vue-resource';
import vuetify from './plugins/vuetify';
import Vuelidate from 'vuelidate';
import Adal from 'vue-adal'

//Using Imported Items
Vue.use(Vuelidate);
Vue.use(VueResource);
Vue.use(store);
Vue.use(Adal, {
        // This config gets passed along to Adal, so all settings available to adal can be used here.
        config: {
            // 'common' (multi-tenant gateway) or Azure AD Tenant ID
            tenant: 'caremypet.in',

            // Application ID
            clientId: '8e1b4af1-468d-4ef8-accc-e38e134c1573',

            // Host URI
            redirectUri: 'http://localhost:8080',

            cacheLocation: 'localStorage'

        },

        // Set this to true for authentication on startup
        requireAuthOnInitialize: true,

        // Pass a vue-router object in to add route hooks with authentication and role checking
        router: router
    })
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