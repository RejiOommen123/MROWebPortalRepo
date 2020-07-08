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
             //Razor-Locahost
            // // 'common' (multi-tenant gateway) or Azure AD Tenant ID
            // tenant: 'caremypet.in',

            // // Application ID
            // clientId: '8e1b4af1-468d-4ef8-accc-e38e134c1573',

            // // Host URI
            // redirectUri: 'http://localhost:8080',

            // cacheLocation: 'localStorage'
			
			// This config gets passed along to Adal, so all settings available to adal can be used here.
            //Razor-QA
            //'common' (multi-tenant gateway) or Azure AD Tenant ID
             tenant: '16f73e0f-bf1b-4216-b0ee-625248f9193d',

             // Application ID
             clientId: '3237e160-c245-481e-86e4-659199e0898f',
 
             // Host URI
             //redirectUri: 'https://devmroportalcust2.azurewebsites.net',
             redirectUri: 'https://devmroportalcust3.azurewebsites.net',
 
             cacheLocation: 'localStorage'


            //MRO DEV
            // 'common' (multi-tenant gateway) or Azure AD Tenant ID
            // tenant: '808444af-4011-40d5-9b0a-a9a5c95f88e9',

            // // Application ID
            // clientId: '834b7324-d4a7-4208-a911-7e7d3369479e',

            // // Host URI
            // redirectUri: 'https://mroadmindev.azurewebsites.net',

            // cacheLocation: 'localStorage'

            
             // // 'common' (multi-tenant gateway) or Azure AD Tenant ID
            //  tenant: '808444af-4011-40d5-9b0a-a9a5c95f88e9',

            //  // Application ID
            //  clientId: 'a9516f8c-7429-42fc-a4e3-dfecba59f0a5',
 
            //  // Host URI
            //  redirectUri: 'https://mroadminuat.azurewebsites.net',
 
            //  cacheLocation: 'localStorage'



        },

        // Set this to true for authentication on startup
        requireAuthOnInitialize: true,

        // Pass a vue-router object in to add route hooks with authentication and role checking
        router: router
    })
    //HTTP Calls 
    //Change URL as per Environment
    //Ensure that there is No '/' at the end of the URL
    //Razor-QA
    Vue.http.options.root = "https://devmroportalapi.azurewebsites.net/api";
    
    //Vue.http.options.root = "https://devmroportalapi-dev.azurewebsites.net/api";
    //Vue.http.options.root = "http://localhost:57364/api";

    //MRO DEV
    //Vue.http.options.root = "https://mroadmindevapi.azurewebsites.net/api";
    //MRO UAT
    //Vue.http.options.root = "https://mroadminuatapi.azurewebsites.net/api";

Vue.config.productionTip = false;

// //Vuetify API Secret Key - Common Code for Adding Header
// Vue.http.interceptors.push((request, next) => {
//     var sAPIKey = "MROSecretKey@007"
//     request.headers.set('sAPIKey', sAPIKey)
//     next()
// })



//Vue Instance
new Vue({
    vuetify,
    router,
    store,
    render: h => h(App)
}).$mount('#app')