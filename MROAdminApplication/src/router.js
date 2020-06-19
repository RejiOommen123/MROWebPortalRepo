import Vue from 'vue'
import VueRouter from 'vue-router'

import WelcomePage from './components/welcome/welcome.vue'
import DashboardPage from './components/dashboard/dashboard.vue'
import SignupPage from './components/auth/signup.vue'
import SigninPage from './components/auth/signin.vue'
import EditField from './components/facility/Fields/EditFields.vue';
import AddFacility from './components/facility/AddFacility.vue';
import EditFacility from './components/facility/EditFacility.vue';
import Locations from './components/facility/Locations/Locations.vue';
import AddLocation from './components/facility/Locations/AddLocation.vue';
import EditLocation from './components/facility/Locations/EditLocation.vue';
import store from './store'
Vue.use(VueRouter)
Vue.use(store)

const routes = [
  {path:'',component:DashboardPage,
  beforeEnter(to, from, next) {
       store.commit("mutatepageHeader","Administration Module" );
       next()
    }
  },
  { path: '/index', component: WelcomePage,
  beforeEnter(to, from, next) {
       store.commit("mutatepageHeader","Administration Module" );
       next()
    }
 },
  { path: '/signup', component: SignupPage },
  { path: '/signin', component: SigninPage },
    {
        path: '/Facility', component: DashboardPage,
        beforeEnter(to, from, next) {
          store.commit("mutatepageHeader","Manage Facilities" );
          next()
       }
    },
    { path: '/Locations/:id', component: Locations,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Manage Locations" );
      next()
   } },
    { path: '/AddLocation/:id', component: AddLocation,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Add Location" );
      next()
   } },
    { path: '/EditLocation/:id', component: EditLocation,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Edit Location" );
      next()
   } },
    { path: '/EditFields/:id', component: EditField,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Edit Fields" );
      next()
   } },
    { path: '/AddFacility', component: AddFacility,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Add Facility" );
      next()
   } },
    { path: '/EditFacility/:id', component: EditFacility,beforeEnter(to, from, next) {
      store.commit("mutatepageHeader","Edit Facility" );
      next()
   } }
]

export default new VueRouter({mode: 'history', routes})