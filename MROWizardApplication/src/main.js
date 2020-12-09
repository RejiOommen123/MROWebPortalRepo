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
  var jsErrObj={
    Error:message,
    Description:{
      Detail:error,
      Source:source,
      Line:line,
      Column:column
    },
    RequesterInfo:store.state.requestermodule
  }
  console.log('Complete Object-',jsErrObj);
  //vueInstance.$appInsights.trackEvent({name:"Javascript_Error"}, { value: jsErrObj});
}

Vue.config.errorHandler = function(err, vm, info) {
  var errObj={
    Error:err,
    Description:info,
    RequesterInfo:vm.$store.state.requestermodule
  }
  console.log('Complete Object-',errObj);
}
Vue.config.warnHandler = function(msg, vm, trace) {
  var warnObj={
    Error:msg,
    Description:trace,
    RequesterInfo:vm.$store.state.requestermodule
  }
  console.log('Complete Object-',warnObj);
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
//var vueInstance=
new Vue({
    vuetify,
    store,
    render: h => h(App)
}).$mount('#app')