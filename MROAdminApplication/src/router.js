//Imports
import Vue from 'vue';
import VueRouter from 'vue-router';
import WelcomePage from './components/welcome/welcome.vue';
import DashboardPage from './components/dashboard/dashboard.vue';
import EditField from './components/facility/Fields/EditFields.vue';
import EditFacilityData from './components/facility/Fields/EditFacilityData.vue';
import AddFacility from './components/facility/AddFacility.vue';
import EditFacility from './components/facility/EditFacility.vue';
import Locations from './components/facility/Locations/Locations.vue';
import AddLocation from './components/facility/Locations/AddLocation.vue';
import EditLocation from './components/facility/Locations/EditLocation.vue';
import PrimaryReason from './components/master/PrimaryReason/PrimaryReason.vue';
import AddPrimaryReason from './components/master/PrimaryReason/AddPrimaryReason.vue';
import EditPrimaryReason from './components/master/PrimaryReason/EditPrimaryReason.vue';
import RecordType from './components/master/RecordType/RecordType.vue';
import AddRecordType from './components/master/RecordType/AddRecordType.vue';
import EditRecordType from './components/master/RecordType/EditRecordType.vue';
import SensitiveInfo from './components/master/SensitiveInfo/SensitiveInfo.vue';
import AddSensitiveInfo from './components/master/SensitiveInfo/AddSensitiveInfo.vue';
import EditSensitiveInfo from './components/master/SensitiveInfo/EditSensitiveInfo.vue';
import ShipmentType from './components/master/ShipmentType/ShipmentType.vue';
import AddShipmentType from './components/master/ShipmentType/AddShipmentType.vue';
import EditShipmentType from './components/master/ShipmentType/EditShipmentType.vue';
import AuditReport from './components/report/AuditReport.vue';
import FacilityLocationReport from './components/report/FacilityLocationReport.vue';
import FacilityConfigurationReport from './components/report/FacilityConfigurationReport.vue';
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
        path: '/EditFacilityData/:id',
        component: EditFacilityData,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Facility Data");
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
        path: '/PrimaryReason/AddPrimaryReason',
        component: AddPrimaryReason,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Primary Reason");
            next()
        }
    },
    {
        path: '/PrimaryReason/EditPrimaryReason/:id',
        component: EditPrimaryReason,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Primary Reason");
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
        path: '/RecordType/AddRecordType',
        component: AddRecordType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Record Type");
            next()
        }
    },
    {
        path: '/RecordType/EditRecordType/:id',
        component: EditRecordType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Record Type");
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
        path: '/SensitiveInfo/EditSensitiveInfo/:id',
        component: EditSensitiveInfo,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Sensitive Info");
            next()
        }
    },
    {
        path: '/SensitiveInfo/AddSensitiveInfo',
        component: AddSensitiveInfo,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Sensitive Info");
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
    },
    {
        path: '/ShipmentType/EditShipmentType/:id',
        component: EditShipmentType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Edit Shipment Type");
            next()
        }
    },
    {
        path: '/ShipmentType/AddShipmentType',
        component: AddShipmentType,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Add Shipment Type");
            next()
        }
    }
    ,
    {
        path: '/Report/Audit',
        component: AuditReport,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Audit Report");
            next()
        }
    }
    ,
    {
        path: '/Report/FacilityLocation',
        component: FacilityLocationReport,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Facility/Location Report");
            next()
        }
    },
    {
        path: '/Report/FacilityConfiguration',
        component: FacilityConfigurationReport,
        beforeEnter(to, from, next) {
            store.commit("mutatepageHeader", "Facility Configuration Report");
            next()
        }
    }
]

export default new VueRouter({ mode: 'history', routes })