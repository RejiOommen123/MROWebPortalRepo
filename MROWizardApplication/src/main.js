import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import { store } from './store/store'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import VueSignature from "vue-signature-pad";
import VueAppInsights from 'vue-application-insights'
import browserDetect from "vue-browser-detect-plugin";

Vue.use(VueAppInsights, {
    id: '00000000-0000-0000-0000-000000000000',
    appInsights: window.appInsights,
    trackInitialPageView: false
    //,
    //router
  });

Vue.use(VueSignature);
Vue.use(VueResource);
Vue.use(browserDetect);

Vue.http.options.root = process.env.VUE_APP_ROOT_URL;
Vue.config.productionTip = false;

window.onerror = function(message, source, line, column, error) {
  var jsErrObj={
    Error:isEmpty(message),
    Description:{
      Detail:isEmpty(error.stack),
      Source:isEmpty(source),
      Line:isEmpty(line),
      Column:isEmpty(column)
    },
    BrowserInfo:{
      Name:isEmpty(vueInstance?.$browserDetect?.meta?.name),
      Version:isEmpty(vueInstance?.$browserDetect?.meta?.version),
      UserAgent:isEmpty(vueInstance?.$browserDetect?.meta?.ua)
    },
    RequesterInfo:isEmpty(store.state.requestermodule)
  }
  console.log('Complete Object- JS',jsErrObj);
  //vueInstance.$appInsights.trackEvent({name:"Javascript_Error"}, { value: jsErrObj});
}

Vue.config.errorHandler = function(err, vm, info) {
  var errObj={
    Error:isEmpty(err.stack),
    Description:isEmpty(info),
    BrowserInfo:{
      Name:isEmpty(vueInstance?.$browserDetect?.meta?.name),
      Version:isEmpty(vueInstance?.$browserDetect?.meta?.version),
      UserAgent:isEmpty(vueInstance?.$browserDetect?.meta?.ua)
    },
    RequesterInfo:isEmpty(vm.$store.state.requestermodule)
  }
  console.log('Complete Object- Vue Error',errObj);
  //vueInstance.$appInsights.trackEvent({name:"Vue_Error"}, { value: errObj});
}
Vue.config.warnHandler = function(msg, vm, trace) {
  var warnObj={
    Error:isEmpty(msg),
    Description:isEmpty(trace),
    BrowserInfo:{
      Name:isEmpty(vueInstance?.$browserDetect?.meta?.name),
      Version:isEmpty(vueInstance?.$browserDetect?.meta?.version),
      UserAgent:isEmpty(vueInstance?.$browserDetect?.meta?.ua)
    },
    RequesterInfo:isEmpty(vm.$store.state.requestermodule)
  }
  console.log('Complete Object- Vue Warning',warnObj);
  //vueInstance.$appInsights.trackEvent({name:"Vue_Warning"}, { value: warnObj});
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
        BrowserInfo:{
          Name:isEmpty(vueInstance?.$browserDetect?.meta?.name),
          Version:isEmpty(vueInstance?.$browserDetect?.meta?.version),
          UserAgent:isEmpty(vueInstance?.$browserDetect?.meta?.ua)
        },
        RequesterInfo:isEmpty(store.state.requestermodule)
      }
      console.log('Complete Object- API',apiObj);
      //vueInstance.$appInsights.trackEvent({name:"API Error"}, { value: apiObj});
    }
      
  });
})

Vue.use(Vuelidate);
var vueInstance=
new Vue({
    vuetify,
    store,
    render: h => h(App)
}).$mount('#app')

function isEmpty(val){
  return (val === undefined || val == null) ? '' : val;
}