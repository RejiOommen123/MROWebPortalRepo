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
  //error = '';
 // message = null;
 //////////// error.stack needs to be put ///////////////////
  var jsErrObj={
    //Error:(message === undefined || message == null) ? '' : message,
    Error:isEmpty(message),
    Description:{
      Detail:isEmpty(error),
      Source:isEmpty(source),
      Line:isEmpty(line),
      Column:isEmpty(column)
    },
    RequesterInfo:isEmpty(store.state.requestermodule)
  }
  console.log('Complete Object-',jsErrObj);
  //vueInstance.$appInsights.trackEvent({name:"Javascript_Error"}, { value: jsErrObj});
}

Vue.config.errorHandler = function(err, vm, info) {
  var errObj={
    Error:isEmpty(err),
    Description:isEmpty(info),
    RequesterInfo:isEmpty(vm.$store.state.requestermodule)
  }
  console.log('Complete Object-',errObj);
}
Vue.config.warnHandler = function(msg, vm, trace) {
  var warnObj={
    Error:isEmpty(msg),
    Description:isEmpty(trace),
    RequesterInfo:isEmpty(vm.$store.state.requestermodule)
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
    if(response.status != 200) {
      if(response.status == 401) {
        store.commit("ConfigModule/bUnauthorized",true);
      }
      var apiObj={
        Error:isEmpty(response.statusText),
        Description:isEmpty(response),
        RequesterInfo:isEmpty(store.state.requestermodule)
      }
      // Appinsight Call
      //console.log(response);
      //console.log(store.state.requestermodule);
      console.log('Complete Object-',apiObj);
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

function isEmpty(val){
  return (val === undefined || val == null) ? '' : val;
}