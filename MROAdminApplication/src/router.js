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

Vue.use(VueRouter)

const routes = [
  { path: '/index', component: WelcomePage },
  { path: '/signup', component: SignupPage },
  { path: '/signin', component: SigninPage },
    {
        path: '/Facility', component: DashboardPage
        //beforeEnter(to, from, next) {
        //    if (store.state.IsSignIn) {
        //        next()
        //    }
        //    else {
        //        next('/signin');
        //    }
        //}
    },
    { path: '/Locations/:id', component: Locations },
    { path: '/AddLocation/:id', component: AddLocation },
    { path: '/EditLocation/:id', component: EditLocation },
    { path: '/EditFields/:id', component: EditField },
    { path: '/AddFacility', component: AddFacility },
    { path: '/EditFacility/:id', component: EditFacility }
]

export default new VueRouter({mode: 'history', routes})