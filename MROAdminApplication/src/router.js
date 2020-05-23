import Vue from 'vue'
import VueRouter from 'vue-router'

import WelcomePage from './components/welcome/welcome.vue'
import DashboardPage from './components/dashboard/dashboard.vue'
import SignupPage from './components/auth/signup.vue'
import SigninPage from './components/auth/signin.vue'
import EditField from './components/customer/Fields/EditFields.vue';
import AddCustomer from './components/customer/AddCustomer.vue';
import EditCustomer from './components/customer/EditCustomer.vue';
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
    { path: '/AddCustomer', component: AddCustomer },
    { path: '/EditCustomer/:id', component: EditCustomer }
]

export default new VueRouter({mode: 'history', routes})