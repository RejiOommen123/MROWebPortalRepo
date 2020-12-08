import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";
import VueAppInsights from 'vue-application-insights'

Vue.use(VueAppInsights, {
    id: '00000000-0000-0000-0000-000000000000',
    appInsights: window.appInsights,
    trackInitialPageView: false
    //,
    //router
  });

Vue.use(VueSignature);
Vue.use(VueResource);

Vue.http.options.root = process.env.VUE_APP_ROOT_URL;
Vue.config.productionTip = false;

window.onerror = function(message, source, line, column, error) {
  console.log('Message:', message);
  console.log('Source:', source);
  console.log('Line:', line);
  console.log('Column:', column);
  console.log('Error:', error);
  console.log('Vm:',Vm);
  console.log('Requester:',Vm.$store.state.requestermodule);
}

Vue.config.errorHandler = function(err, vm, info) {
  console.log('Err:', err);
  console.log('Vm:', vm);
  console.log('Info:', info);
}
Vue.config.warnHandler = function(msg, vm, trace) {
  console.log('Msg:', msg);
  console.log('Vm:', vm);
  console.log('Trace:', trace);
  //console.log(`Warn Hello: ${msg}\nTrace: ${trace}`);
  //vm.$appInsights.trackEvent({name:"Test Warning main"}, { value: `Warn Hello: ${msg}\nTrace: ${trace}`});
  //console.log(`Warn Hello: ${msg}\nTrace: ${trace} \nStore:${JSON.stringify(vm.$store.state.requestermodule)}`);
}

//Vuetify API Secret Key - Common Code for Adding Header
Vue.http.interceptors.push((request, next) => {
    if(request.url.indexOf('ringcaptcha') == -1){
        request.headers.set('sRequestorID', (store.state.requestermodule.nRequesterID).toString());
        request.headers.set('sGUID', store.state.requestermodule.sGUID);
    }
    next(function(response){
        if(response.status == 401) {
            store.commit("ConfigModule/bUnauthorized",true);
          }
    });
})

Vue.use(Vuelidate);
var Vm=new Vue({
    vuetify,
    store,
    render: h => h(App)
}).$mount('#app')