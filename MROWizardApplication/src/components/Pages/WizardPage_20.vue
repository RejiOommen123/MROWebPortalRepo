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
                        width="90%"
                        height="70%"
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
                        <v-btn @click="onCapture" fab dark color="teal">
                          <v-icon dark>mdi-camera</v-icon>
                        </v-btn>    
                        <!-- <button type="button" class="btn btn-primary" @click="onCapture">Capture Photo</button> -->
                    </v-col>
                </v-row>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" sm="12" v-if="sStatus=='ImgCaptured'" >
                <h2>Captured Image</h2>
                <figure class="figure">
                    <img :src="sIdentityImage" width="500px" height="400px" class="img-responsive">
                </figure>
                <div class="col-sm-12">
                        <button type="button" class="btn btn-primary" @click="sStatus='CapturingImg'">Capture Again</button><br/><br/>
                        <button type="button" class="btn btn-success" @click="nextPage">Next</button>
                </div>
            </v-col>
        </v-row>
        <v-row>
         
            <v-col cols="12" sm="12" v-if="sStatus=='UploadImg'">
                <h2>Upload Identity Document Image</h2>
                   <form >
                <v-img class="identityUpload" style="cursor:pointer;" width='300px' height="200px" v-if="sIdentityImage" :src="sIdentityImage"></v-img><br>
                <v-file-input
                    v-model="fileInput"
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
                    :error-messages="fileInputErrors"
                    required
                    @input="$v.fileInput.$touch()"
                    @blur="$v.fileInput.$touch()"
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
                        <button type="button" :disabled="$v.$invalid" class="btn btn-success" @click="nextPage">Save & Next</button>
                </v-col>
                   </form>
            </v-col>
         
        </v-row>
    </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required} from "vuelidate/lib/validators";
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
            sStatus:'CapturingImg',
            fileInput:''
        };
    },
    updated(){
    console.log("Updated Event");
    },
     mixins: [validationMixin],
    validations: {
        fileInput: {
        required,
        }
    },
    created(){ 
        this.$vuetify.theme.dark = true
     },
    computed: {
        device: function() {
            return this.devices.find(n => n.deviceId === this.deviceId);
        },
        fileInputErrors() {
            const errors = [];
            if (!this.$v.fileInput.$dirty) return errors;
            !this.$v.fileInput.required && errors.push("File is required");
            return errors;
        },
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
        this.$refs.webcam.stop();            
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