import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import VueResource from 'vue-resource';
import vuetify from './plugins/vuetify';
import Vuelidate from 'vuelidate';
import Adal from 'vue-adal'
import excel from 'vue-excel-export'
 

//Using Imported Items
Vue.use(Vuelidate);
Vue.use(VueResource);
Vue.use(store);
Vue.use(excel);
Vue.use(Adal, {
    // This config gets passed along to Adal, so all settings available to adal can be used here.
    config: {
        // 'common' (multi-tenant gateway) or Azure AD Tenant ID
        tenant: process.env.VUE_APP_TENANT,

        // Application ID
        clientId: process.env.VUE_APP_CLIENTID,

        // Host URI
        redirectUri: process.env.VUE_APP_REDIRECTURI,

        cacheLocation: 'localStorage'

    },

    // Set this to true for authentication on startup
    requireAuthOnInitialize: true,

    // Pass a vue-router object in to add route hooks with authentication and role checking
    router: router
})

Vue.http.options.root = process.env.VUE_APP_ROOT_URL;
Vue.config.productionTip = false;

//Vue Instance
new Vue({
    vuetify,
    router,
    store,
    render: h => h(App)
}).$mount('#app')