<template>
    <div class="center">
        <v-row>
            <v-col cols="12" sm="12" v-if="sStatus=='CapturingImg'" >
                <h2>Camera</h2>
                <code v-if="device">{{ device.label }}</code>
                <div class="border">
                    <vue-web-cam
                        ref="webcam"
                        :device-id="deviceId"
                        width="100%"
                        height="80%"
                        @started="onStarted"
                        @stopped="onStopped"
                        @error="onError"
                        @cameras="onCameras"
                        @camera-change="onCameraChange"
                    />
                </div>

                <v-row>
                    <v-col cols="12" sm="12">
                        <select v-model="camera">
                            <option>-- Select Device --</option>
                            <option
                                v-for="device in devices"
                                :key="device.deviceId"
                                :value="device.deviceId"
                            >{{ device.label }}</option>
                        </select>
                    </v-col>
                    <v-col cols="12" sm="12">
                        <button type="button" class="btn btn-primary" @click="onCapture">Capture Photo</button>
                        <button type="button" class="btn btn-danger" @click="onStop">Stop Camera</button>
                        <button type="button" class="btn btn-success" @click="onStart">Start Camera</button>
                    </v-col>
                </v-row>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" sm="12" v-if="sStatus=='ImgCaptured'" >
                <h2>Captured Image</h2>
                <figure class="figure">
                    <img :src="sIdentityImage" class="img-responsive">
                </figure>
                <div class="col-sm-12">
                        <button type="button" class="btn btn-primary" @click="sStatus='CapturingImg'">Capture Again</button>
                        <button type="button" class="btn btn-success" @click="nextPage">Next</button>
                </div>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" sm="12" v-if="sStatus=='UploadImg'">
                <h2>Upload Identity Document Image</h2>
                <v-img class="identityUpload" style="cursor:pointer;" v-if="sIdentityImage" :src="sIdentityImage"></v-img><br>
                <v-file-input
                    chips
                    show-size
                    dense
                    hint="Upload Identity Document Image"
                    rounded                    
                    label="Identity Documnet"
                    filled
                    prepend-icon="mdi-camera"
                    @change="onFileChanged"
                    accept="image/png, image/jpeg, image/bmp"
                >
                <v-tooltip slot="append" top>
                    <template v-slot:activator="{ on }">
                        <v-icon v-on="on" color='rgb(0, 91, 168)' top>
                        mdi-information
                        </v-icon>
                    </template>
                    <span>Please upload identity image document.</span>
                </v-tooltip>
                </v-file-input>
                <v-col cols="12" sm="12">
                        <button type="button" class="btn btn-success" @click="nextPage">Next</button>
                </v-col>
            </v-col>
        </v-row>
    </div>
</template>

<script>
import { WebCam } from 'vue-cam-vision'
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
            sStatus:'CapturingImg'
        };
    },
    computed: {
        device: function() {
            return this.devices.find(n => n.deviceId === this.deviceId);
        }
    },
    watch: {
        camera: function(id) {
            this.deviceId = id;
        },
        devices: function() {
            // Once we have a list select the first one
            const [first] = this.devices;
            if (first) {
                this.camera = first.deviceId;
                this.deviceId = first.deviceId;
            }
        }
    },
    methods: {
        onCapture() {
            var self = this;
            this.$refs.webcam.capture().then(function (defs){
                self.sIdentityImage = defs;
            });
        console.log(this.sIdentityImage);
        this.sStatus="ImgCaptured";
            
        },
        onStarted(stream) {
            console.log("On Started Event", stream);
        },
        onStopped(stream) {
            console.log("On Stopped Event", stream);
        },
        onStop() {
            this.$refs.webcam.stop();
        },
        onStart() {
            this.$refs.webcam.start();
        },
        onError(error) {
            console.log("On Error Event", error.name);
            if(error.name=="NotFoundError")
                {
                    alert("Camera Not Found");
                     this.sStatus="UploadImg";
                }
        },
        onCameras(cameras) {
            this.devices = cameras;
            console.log("On Cameras Event", cameras);
        },
        onCameraChange(deviceId) {
            this.deviceId = deviceId;
            this.camera = deviceId;
            console.log("On Camera Change Event", deviceId);
        },
        nextPage(){
            this.$store.commit("requestermodule/sIdentityImage", this.sIdentityImage);
            this.$store.commit("ConfigModule/mutatedialogMinWidth", "100%");
            this.$store.commit("ConfigModule/mutatedialogMaxWidth", "100%");
            this.$store.commit("ConfigModule/mutatedialogMaxHeight", "100%");
            this.$vuetify.theme.dark = false;
            this.$store.commit("ConfigModule/mutateNextIndex");
        },
        onFileChanged(file) {
        if (file) {
            const reader = new FileReader();        
            reader.addEventListener("load",() => {
            this.sIdentityImage=reader.result; //base64encoded string
            })
            reader.readAsDataURL(file);
            // this.sIdentityImage=file.name;
        }
        },
    }
};
</script>
<style scoped>
.identityUpload {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 80%;
  height: 40%;
}
</style>>