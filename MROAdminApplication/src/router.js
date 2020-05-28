import Vue from 'vue'
import VueRouter from 'vue-router'

import WelcomePage from './components/welcome/welcome.vue'
import DashboardPage from './components/dashboard/dashboard.vue'
import SignupPage from './components/auth/signup.vue'
import SigninPage from './components/auth/signin.vue'
import EditField from './components/facility/Fields/EditFields.vue';
import AddFacility from './components/facility/AddFacility.vue';
import EditFacility from './components/facility/EditFacility.vue';
import store from './store'

Vue.use(VueRouter)

const routes = [
  { path: '/', component: WelcomePage },
  { path: '/signup', component: SignupPage },
  { path: '/signin', component: SigninPage },
    {
        path: '/dashboard', component: DashboardPage,
        beforeEnter(to, from, next) {
            if (store.state.IsSignIn) {
                next()
            }
            else {
                next('/signin');
            }
        }
    },
    { path: '/EditFields/:id', component: EditField },
    { path: '/AddFacility', component: AddFacility },
    { path: '/EditFacility/:id', component: EditFacility }
]

export default new VueRouter({mode: 'history', routes})