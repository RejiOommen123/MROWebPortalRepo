<template>
  <div class="center">
    <v-row>
      <v-col cols="12" sm="12" v-if="sStatus=='CapturingImg'">
        <h2 style="color:white">Camera</h2>
        <!-- <v-col v-if="this.devices.length>1" cols="12" sm="6" offset-sm="3"> -->
             <!-- <select v-model="camera">
                            <option>-- Select Device --</option>
                            <option
                                v-for="device in devices"
                                :key="device.deviceId"
                                :value="device.deviceId"
                            >{{ device.label }}</option>
                        </select> -->
            <!-- <v-select v-model="camera" label="Select Device" :items="devices" item-text="label" item-value="deviceId"></v-select>
          </v-col> -->
        <!-- <code v-if="device">{{ device.label }}</code> -->
        <v-col cols="12" sm="10" offset-sm="1">
        <div  class="border camBorder">
          <vue-web-cam
            ref="webcam"
            :device-id="deviceId"
            width="100%"
            height="100%"
            @started="onStarted"
            @stopped="onStopped"
            @error="onError"
            @cameras="onCameras"
            @camera-change="onCameraChange"
          />
        </div>
        </v-col>
        <v-row>
          <v-col cols="4"  sm="4">
            <v-tooltip bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn :disabled="diableCamera || !cameraStarted" @click="onCapture" fab dark color="teal" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-camera</v-icon>
                </v-btn>
              </template>
              <span>Capture Image</span>
            </v-tooltip>
          </v-col>
          <!-- v-if="devices.length>1" -->
          <v-col v-if="devices.length<=1" cols="4" sm="4">
          </v-col>
          <v-col v-if="devices.length>1" cols="4" sm="4">
             <v-tooltip bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn :disabled="diableCamera || !cameraStarted" @click="switchCamera" fab dark color="teal" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-cached</v-icon>
                </v-btn>
              </template>
              <span>Switch Camera</span>
            </v-tooltip>
          </v-col>
          <v-col cols="4" sm="4">
            <v-tooltip bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  fab
                  dark
                  color="teal"
                  @click="sStatus = 'UploadImg'; camera=null;"
                  v-bind="attrs"
                  v-on="on"
                >
                  <v-icon dark>mdi-attachment</v-icon>
                </v-btn>
              </template>
              <span>Upload Image</span>
            </v-tooltip>
          </v-col>
          <v-col cols="12" sm="12">
            <v-btn @click.prevent="skipPage" class="next">Skip</v-btn>
          </v-col>
          <!-- <v-col cols="12" sm="12"> -->
             <!-- <select v-model="camera">
                            <option>-- Select Device --</option>
                            <option
                                v-for="device in devices"
                                :key="device.deviceId"
                                :value="device.deviceId"
                            >{{ device.label }}</option>
                        </select> -->
            <!-- <v-select v-model="camera" label="Select Device" :items="devices" item-text="label" item-value="deviceId"></v-select>
          </v-col> -->
          <!-- :value="device.deviceId" -->
        </v-row>
      </v-col>
    </v-row>
    <v-row v-if="sStatus=='ImgCaptured'">
      <v-col cols="12" sm="10" offset-sm="1">
        <h2 style="color:white">Captured Image</h2>
        <figure class="figure" >
          <img :src="sIdentityImage" style="width:100%" class="camBorder" />
        </figure>
      </v-col>
      <v-col cols="6" offset-sm="2" sm="3">
        <v-btn class="next" @click="sStatus='CapturingImg'">Retake</v-btn>
      </v-col>
      <v-col cols="6" offset-sm="2" sm="3">
        <v-btn class="next" @click="nextPage">Next</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" sm="12" v-if="sStatus=='UploadImg'">
        <h2 style="color:white">Upload Identity Document Image</h2>
        <form>
          <div v-show="bShowImage!=''">
            <v-img
              class="identityUpload"
              width="50%"
              height="50%"
              style="cursor:pointer;"
              v-if="sIdentityImage"
              :src="sIdentityImage"
            ></v-img>
            <br />
          </div>
          <v-file-input
            ref="clearInput"
            v-model="fileInput"
            show-size
            color="white"
            dense
            hint="Upload Identity Document Image"
            rounded
            label="IDENTITY DOCUMENT"
            filled
            prepend-icon="mdi-camera"
            @change="onFileChanged"
            accept="image/png, image/jpeg, image/jpg, image/bmp"  
            :rules="rules"   
          >
              <!-- :error-messages="fileInputErrors"
            required
            @input="$v.fileInput.$touch()"
            @blur="$v.fileInput.$touch()" -->
            <template v-slot:selection="{ text }">
                  <v-chip
                    small
                    label
                    color="primary"
                  >
                    {{ text }}
                  </v-chip>
                </template>
            <v-tooltip slot="append" top>
              <template v-slot:activator="{ on }">
                <v-icon style="cursor:pointer" v-on="on" color="white" top>mdi-information</v-icon>
              </template>
              <span >Please upload identity image document.</span>
            </v-tooltip>
          </v-file-input>
          <v-row>
          <v-col cols="6" sm="4">
            <v-btn type="button" :disabled="diableCamera" @click="sStatus='CapturingImg'" class="next">Take Picture</v-btn>
          </v-col>
          <v-col cols="6" sm="4">
            <v-btn type="button" :disabled="fileInput==null" class="next" @click.once="nextPage" :key="buttonKey">Save & Next</v-btn>
          </v-col>           
          <v-col cols="12" sm="4">
            <v-btn @click.prevent="skipPage" class="next">Skip</v-btn>
          </v-col>
          </v-row>
        </form>
      </v-col>
    </v-row>
    <v-dialog
          v-model="dialog"
          max-width="290"
          light
        >
      <v-card>
        <v-card-text style="padding-top:20px">
          {{disclaimer}}
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green darken-1"
            text
            @click="dialog = false"
          >
            Ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
       <!-- Unsupported Format -->
    <v-dialog v-model="unsupported" width="360px" light max-width="350px">
      <v-card>
        <v-card-title class="headline">Info</v-card-title>
        <v-card-text>Select JPG/JPEG/PNG/BMP File Only</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="unsupported=false">Ok</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import { WebCam } from "vue-web-cam";
export default {
  name: "App",
  components: {
    "vue-web-cam": WebCam
  },
  data() {
    return {
      sIdentityImage: null,
      camera: null,
      deviceId: null,
      devices: [],
      sStatus: "CapturingImg",
      fileInput: null,
      bShowImage: "",
      dialog:true,
      diableCamera:false,
      unsupported:false,
      rules: [
         value => !value || value.size < 10485760 || 'Uploaded file is greater than 10 MB',
      ],
      cameraStarted:false,
      buttonKey:1,
    };
  },
  // mixins: [validationMixin],
  // validations: {
  //   fileInput: {
  //     required
  //   }
  // },
  activated(){
    // this.sStatus="";
    // this.sStatus="CapturingImg";
    this.buttonKey++;
    var ua = window.navigator.userAgent;
    var isIE = /MSIE|Trident/.test(ua);

    if (isIE && this.sStatus != "UploadImg") {
      alert("Camera doesn't work in this browser. No worries you can upload image.");
      this.sStatus = "UploadImg";
      this.diableCamera=true;
    }
    // console.log(this.devices);
    if(this.devices.length!=0){
      if(this.$refs.webcam!=undefined){
        this.$refs.webcam.start();
      }
    }
  },
  deactivated(){
    // console.log(this.devices);
    if(this.devices.length!=0){
      if(this.$refs.webcam!=undefined){
        this.$refs.webcam.stop();
      }
    }
  },
  created() {
    this.$vuetify.theme.dark = true;
  },
  computed: {
    device: function() {
      return this.devices.find(n => n.deviceId === this.deviceId);
    },
    ...mapState({
      facilityForceCompliance : state => state.ConfigModule.bForceCompliance,
      disclaimer : state => state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_22_disclaimer01
    }),
  },
  watch: {
    camera: function(id) {
      this.deviceId = id;
      // console.log("watch camera - deviceId - "+this.deviceId);
    },
    devices: function() {
      // Once we have a list select the first one
       const [first] = this.devices;
      //let first = head(this.devices)
      if (first) {
        this.camera = first.deviceId;
        this.deviceId = first.deviceId;
      }
      // console.log("watch devices - deviceId - " + this.deviceId);
    }
  },
  methods: {
    // onCapture() {
    //   var self = this;
    //   this.$refs.webcam.capture().then(function(defs) {
    //     self.sIdentityImage = defs;
    //   });
    isEmpty(val){
      return (val === undefined || val == null) ? '' : val;
    },
    switchCamera(){
       if(this.devices.length!=0){
        var index = this.devices.findIndex(x => x.deviceId === this.deviceId);
        index = index + 1; // increase i by one
        index = index % this.devices.length; // if we've gone too high, start from `0` again
        this.onCameraChange(this.devices[index].deviceId);
        }
      // console.log("List of devices");
      // console.log(this.devices[0]);
      // console.log("Device Length");
      // console.log(this.devices.length);
    },
    async onCapture () {
      this.sIdentityImage = await this.$refs.webcam.capture();
      this.sStatus = "ImgCaptured";
      this.camera=null;
    },
    onStarted(stream) {
      this.cameraStarted=true;
      console.log("On Started Event", stream);
    },
    onStopped(stream) {
      this.cameraStarted=false;
      console.log("On Stopped Event", stream);
    },
    onStop() {
      this.$refs.webcam.stop();
      // console.log("On Cameras start ");
    },
    onStart() {
      this.$refs.webcam.start();
      // console.log("On Cameras stop ");
    },
    onError(error) {
      if (error.name == "NotFoundError") {
        alert("Camera Not Found. Redirecting to upload file page.");
        this.sStatus = "UploadImg";
      }
      var camErrObj={
        Error:this.isEmpty(error?.name),    
        Description:this.isEmpty(error?.message),
        BrowserInfo:{
          Name:this.isEmpty(this?.$browserDetect?.meta?.name),
          Version:this.isEmpty(this?.$browserDetect?.meta?.version),
          UserAgent:this.isEmpty(this?.$browserDetect?.meta?.ua)
        },
        RequesterInfo:this.isEmpty(this.$store?.state?.requestermodule)
      }
      //console.log('Complete Object-',camErrObj);
      this.$appInsights.trackEvent({name:"Camera Error"}, { value: camErrObj});
    },
    onCameras(cameras) {
      this.devices = cameras;
      // console.log("On Cameras " +cameras);
    },
    onCameraChange(deviceId) {
      this.deviceId = deviceId;
      this.camera = deviceId;
      //  console.log('On Camera Change Event', deviceId)
    },
    skipPage(){
      this.$store.commit("ConfigModule/bIdentitySkiped", true);
      this.$store.commit("requestermodule/sIdentityIdName", '');   
      this.$store.commit("requestermodule/sIdentityImage", '');
      this.sIdentityImage='';
      this.fileInput=null;
      this.sStatus="CapturingImg";
      if(!this.$store.state.requestermodule.bPhoneNoVerified && !this.$store.state.requestermodule.bEmailVerified){
        if(this.facilityForceCompliance)
        {
          this.$store.commit("requestermodule/bForceCompliance", false);
        }
      }
      this.UploadIdentityImage();
      this.continue();
    },
    nextPage() {
      this.$store.commit("ConfigModule/bIdentitySkiped", false);
      this.$store.commit("requestermodule/sIdentityImage", this.sIdentityImage);
      this.$store.commit("ConfigModule/bReturnedForCompliance",false);
      if(this.facilityForceCompliance)
      {
        this.$store.commit("requestermodule/bForceCompliance", true);
      }
      this.UploadIdentityImage();
      this.continue();
    },
    continue(){
      //Partial Requester Data Save Start
      this.$store.dispatch('requestermodule/partialAddReq');
      // console.log(JSON.stringify(this.$store.state.requestermodule));
      // this.$store.commit("ConfigModule/mutatedialogMinWidth", "100%");
      // this.$store.commit("ConfigModule/mutatedialogMaxWidth", "100%");
      // this.$store.commit("ConfigModule/mutatedialogMaxHeight", "100%");
      // this.$vuetify.theme.dark = false;
      this.$store.commit("ConfigModule/mutateNextIndex");
    },
    onFileChanged(file) {
      if (file) {
        var file_name_array = file.name.split(".");
        var file_extension = file_name_array[file_name_array.length - 1];
        if(file_extension == "jpg"||file_extension == "png"||file_extension == "jpeg"||file_extension == "bmp"){
          const reader = new FileReader();
          reader.addEventListener("load", () => {
            this.sIdentityImage = reader.result; //base64encoded string
          });
          reader.readAsDataURL(file);
          this.bShowImage = file.name;
        }
        else{
          this.fileInput = null;
          this.bShowImage = "";
          this.$refs.clearInput.clearableCallback();
          this.unsupported=true;
        }
      } else {
        this.fileInput = null;
        this.bShowImage = "";
      }
    },
    UploadIdentityImage(){
      var identityDocObj = {
        nRequesterID: this.$store.state.requestermodule.nRequesterID,
        nFacilityID: this.$store.state.requestermodule.nFacilityID,
        sIdentityImage: this.$store.state.requestermodule.sIdentityImage,
        sWizardName: this.$store.state.ConfigModule.selectedWizard
      };
      this.$http.post("requesters/UpdateIdentityDoc/",identityDocObj)
        .then(response => {
          this.$store.commit("requestermodule/nRequesterID", response.body);
      });  
    }
  }  
};
</script>
<style>
.v-tooltip__content{
  color: black;
  background: white;
}
</style>