//Imports
import Vue from 'vue';
import VueRouter from 'vue-router';
import WelcomePage from './components/welcome/welcome.vue';
import DashboardPage from './components/dashboard/dashboard.vue';
import EditField from './components/facility/Fields/EditFields.vue';
import AddFacility from './components/facility/AddFacility.vue';
import EditFacility from './components/facility/EditFacility.vue';
import Locations from './components/facility/Locations/Locations.vue';
import AddLocation from './components/facility/Locations/AddLocation.vue';
import EditLocation from './components/facility/Locations/EditLocation.vue';
import PrimaryReason from './components/master/PrimaryReason/PrimaryReason.vue';
import RecordType from './components/master/RecordType/RecordType.vue';
import AddRecordType from './components/master/RecordType/AddRecordType.vue';
import SensitiveInfo from './components/master/SensitiveInfo/SensitiveInfo.vue';
import ShipmentType from './components/master/ShipmentType/ShipmentType.vue';
import store from './store';

Vue.use(VueRouter);
Vue.use(store);

const routes = [{
        path: '',
        component: DashboardPage,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Manage Facilities");
            next()
        }
    },
    {
        path: '/index',
        component: WelcomePage,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Administration Module");
            next()
        }
    },
    {
        path: '/Facility',
        component: DashboardPage,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Manage Facilities");
            next()
        }
    },
    {
        path: '/Locations/:id',
        component: Locations,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Manage Locations");
            next()
        }
    },
    {
        path: '/AddLocation/:id',
        component: AddLocation,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Location");
            next()
        }
    },
    {
        path: '/EditLocation/:id',
        component: EditLocation,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Location");
            next()
        }
    },
    {
        path: '/EditFields/:id',
        component: EditField,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Fields");
            next()
        }
    },
    {
        path: '/AddFacility',
        component: AddFacility,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Facility");
            next()
        }
    },
    {
        path: '/EditFacility/:id',
        component: EditFacility,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Facility");
            next()
        }
    },
    {
        path: '/Master/PrimaryReason',
        component: PrimaryReason,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Primary Reason");
            next()
        }
    },    
    {
        path: '/Master/RecordType',
        component: RecordType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Record Type");
            next()
        }
    },
    {
        path: '/AddRecordType',
        component: AddRecordType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Record Type");
            next()
        }
    },
    {
        path: '/Master/SensitiveInfo',
        component: SensitiveInfo,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Sensitive Info");
            next()
        }
    },
    {
        path: '/Master/ShipmentType',
        component: ShipmentType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Shipment Type");
            next()
        }
    }
]

export default new VueRouter({ mode: 'history', routes })