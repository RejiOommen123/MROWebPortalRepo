<template>
  <div class="center">
    <v-row>
      <v-col cols="12" sm="12" v-if="sStatus=='CapturingImg'">
        <h2 style="color:white">Camera</h2>
        <!-- <code v-if="device">{{ device.label }}</code> -->
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
          <v-col cols="4" offset-sm="2" sm="3">
            <v-tooltip bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn @click="onCapture" fab dark color="teal" v-bind="attrs" v-on="on">
                  <v-icon dark>mdi-camera</v-icon>
                </v-btn>
              </template>
              <span>Capture Image</span>
            </v-tooltip>
          </v-col>

          <v-col style="margin-top:2%" cols="4" sm="2">
            <h3>OR</h3>
          </v-col>
          <v-col cols="4" sm="3">
            <v-tooltip bottom>
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  fab
                  dark
                  color="teal"
                  @click="sStatus = 'UploadImg'"
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
            <p>Hello</p>
            <select v-model="camera">
              <option>-- Select Device --</option>
              <option v-for="device in devices" :key="device.deviceId">{{ device.label }}</option>
            </select>
            <v-select v-model="camera" :items="devices" item-text="label" item-value="deviceId"></v-select>
          </v-col>
          <!-- :value="device.deviceId" -->
        </v-row>
      </v-col>
    </v-row>
    <v-row v-if="sStatus=='ImgCaptured'">
      <v-col cols="12" sm="12">
        <h2 style="color:white">Captured Image</h2>
        <figure class="figure">
          <img :src="sIdentityImage" width="400px" height="250px" class="img-responsive" />
        </figure>
      </v-col>
      <v-col cols="6" offset-sm="2" sm="3">
        <v-btn class="next" @click="sStatus='CapturingImg'">Capture Again</v-btn>
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
            v-model="fileInput"
            chips
            show-size
            dense
            hint="Upload Identity Document Image"
            rounded
            label="IDENTITY DOCUMENT"
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
                <v-icon style="cursor:pointer" v-on="on" color="rgb(0, 91, 168)" top>mdi-information</v-icon>
              </template>
              <span>Please upload identity image document.</span>
            </v-tooltip>
          </v-file-input>
          <v-col cols="12" sm="12">
            <v-btn type="button" :disabled="$v.$invalid" class="next" @click="nextPage">Save & Next</v-btn>
          </v-col>
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
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";
import { WebCam } from "vue-cam-vision";
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
      fileInput: "",
      bShowImage: "",
      dialog:true,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_22_disclaimer01,
    };
  },
  mixins: [validationMixin],
  validations: {
    fileInput: {
      required
    }
  },
  created() {
    this.$vuetify.theme.dark = true;
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
      this.$refs.webcam.capture().then(function(defs) {
        self.sIdentityImage = defs;
      });
      this.sStatus = "ImgCaptured";
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
      if (error.name == "NotFoundError") {
        alert("Camera Not Found. Redirecting to upload file page.");
        //this.sStatus = "UploadImg";
      }
    },
    onCameras(cameras) {
      this.devices = cameras;
    },
    onCameraChange(deviceId) {
      this.deviceId = deviceId;
      this.camera = deviceId;
    },
    nextPage() {
      this.$store.commit("requestermodule/sIdentityImage", this.sIdentityImage);

      //Partial Requester Data Save Start
      this.$store.commit(
        "requestermodule/sWizardName",
        this.$store.state.ConfigModule.selectedWizard
      );
      if (
        this.$store.state.ConfigModule.apiResponseDataByFacilityGUID
          .wizardsSave[this.$store.state.ConfigModule.selectedWizard] == 1
      ) {
        this.$http
          .post("requesters/AddRequester/", this.$store.state.requestermodule)
          .then(response => {
            this.$store.commit("requestermodule/nRequesterID", response.body);
          });
      }
      //Partial Requester Data Save End
      console.log(JSON.stringify(this.$store.state.requestermodule));   
      this.$store.commit("ConfigModule/mutatedialogMinWidth", "100%");
      this.$store.commit("ConfigModule/mutatedialogMaxWidth", "100%");
      this.$store.commit("ConfigModule/mutatedialogMaxHeight", "100%");
      this.$vuetify.theme.dark = false;
      this.$store.commit("ConfigModule/mutateNextIndex");

    },
    onFileChanged(file) {
      if (file) {
        const reader = new FileReader();
        reader.addEventListener("load", () => {
          this.sIdentityImage = reader.result; //base64encoded string
        });
        reader.readAsDataURL(file);
        this.bShowImage = file.name;
      } else {
        this.bShowImage = "";
      }
    }
  }
};
</script>
