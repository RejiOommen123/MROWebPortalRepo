<template>
  <div class="uploadCard">
    <v-card
      @drop.prevent="handleDropEvent($event)"
      @dragover.prevent="dragover = true"
      @dragenter.prevent="dragover = true"
      @dragleave.prevent="dragover = false"
      :class="{ 'grey lighten-2': dragover }"
    >
      <v-card-text>
        <v-row class="d-flex flex-column" align="center" justify="center">
          <!-- <v-icon :class="[dragover ? 'mt-2, mb-6' : 'mt-5']" size="35">
              mdi-cloud-upload
            </v-icon> -->
          <p @click.stop="openFileDialog" class="mt-1" v-html="message"></p>
        </v-row>
        <v-virtual-scroll
          v-if="uploadedFiles.length > 0"
          :items="uploadedFiles"
          :height="scrollHeight"
          item-height="50"
        >
          <template v-slot:default="{ item }">
            <v-list-item :key="item.name">
              <v-list-item-content>
                <v-list-item-title>
                  {{ item.name }}
                  <span class="ml-3 text--secondary">
                    {{ (item.size / 1000000).toFixed(2) }} MB</span
                  >
                </v-list-item-title>
              </v-list-item-content>

              <v-list-item-action>
                <v-btn @click.stop="removeFile(item.name)" icon>
                  <v-icon> mdi-close-circle </v-icon>
                </v-btn>
              </v-list-item-action>
            </v-list-item>
          </template>
        </v-virtual-scroll>
      </v-card-text>

      <input
        accept="image/*, application/pdf"
        @change="handleSelectEvent"
        type="file"
        ref="myFile"
        id="fileUploadInputId"
        hidden
        :multiple="multiple"
      />
    </v-card>
  </div>
</template>

<script>
export default {
  name: "Upload",
  props: {
    scrollHeight: {
      type: Number,
      default: 70,
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    message: {
      type: String,
      default: "Drag & drop or <a href='#'>Browse</a>",
    },
    fileSizeLimit: {
      type: Number,
      default: 5,
    },
    noOfFilesLimit: {
      type: Number,
      default: 3,
    },
  },
  data() {
    return {
      dragover: false,
      uploadedFiles: [],
    };
  },
  methods: {
    clearAll() {
      this.uploadedFiles = [];
      let clearAllFiles = {
        files: [],
        bRemoved: false,
      };
      this.$emit("filesUploaded", clearAllFiles);
    },

    openFileDialog() {
      let fileInputElement = this.$refs.myFile;
      fileInputElement.click();
    },
    handleDropEvent(e) {
      if (e.dataTransfer.files > 0) {
        this.handleFileValidationsAndEmit(e.dataTransfer.files);
      }
    },
    handleSelectEvent(e) {
      if (e.target.files.length > 0) {
        this.handleFileValidationsAndEmit(e.target.files);
        // clearing target value so same file can be accessed again on second & ahead onchanges
        e.target.value = "";
      }
    },
    handleFileValidationsAndEmit(listOfFiles) {
      let bValidFiles = true;
      this.dragover = false;
      // If there are already uploaded files remove them
      if (this.uploadedFiles.length > 0) {
        this.uploadedFiles = [];
      }
      // If user has uploaded multiple files but the component is not multiple throw error
      // No of Files LOgic
      if (listOfFiles.length > this.noOfFilesLimit) {
        let ErrorDialogForFileLimit = {
          visible: true,
          title: "Error",
          body: "You can upload only " + this.noOfFilesLimit + " files",
        };
        this.$store.commit("ConfigModule/ErrorDialog", ErrorDialogForFileLimit);
        let oUploadEmitForFileLimit = {
          files: [],
          bRemoved: false,
        };
        this.$emit("filesUploaded", oUploadEmitForFileLimit);
        bValidFiles = false;
        // return;
      }

      // Size Logic
      listOfFiles.forEach((singleFile) => {
        if (singleFile.size / (1000 * 1000) > this.fileSizeLimit) {
          let ErrorDialogForFileSizeLimit = {
            visible: true,
            title: "Error",
            body: "File size cannot exceed " + this.fileSizeLimit + " MB.",
          };
          this.$store.commit(
            "ConfigModule/ErrorDialog",
            ErrorDialogForFileSizeLimit
          );
          let oUploadEmitForFileSizeLimit = {
            files: [],
            bRemoved: false,
          };
          this.$emit("filesUploaded", oUploadEmitForFileSizeLimit);
          bValidFiles = false;
          // return;
        }
      });
      if (!this.multiple && listOfFiles.length > 1) {
        var ErrorDialogForSingleFile = {
          visible: true,
          title: "Error",
          body: "Only one file can be uploaded.",
        };
        this.$store.commit(
          "ConfigModule/ErrorDialog",
          ErrorDialogForSingleFile
        );
        let oUploadEmitForSingleFile = {
          files: [],
          bRemoved: false,
        };
        this.$emit("filesUploaded", oUploadEmitForSingleFile);
        bValidFiles = false;
        // return;
      }
      // Add each file to the array of uploaded files, if all validatioons pass
      if (bValidFiles) {
        listOfFiles.forEach((element) => this.uploadedFiles.push(element));
        let oUploadEmit = {
          files: this.uploadedFiles,
          bRemoved: false,
        };
        this.$emit("filesUploaded", oUploadEmit);
      }
    },
    removeFile(fileName) {
      // Find the index of the
      const index = this.uploadedFiles.findIndex(
        (file) => file.name === fileName
      );
      // If file is in uploaded files remove it
      if (index > -1) this.uploadedFiles.splice(index, 1);
      let oUploadEmit = {
        files: this.uploadedFiles,
        bRemoved: true,
      };
      this.$emit("filesUploaded", oUploadEmit);
    },
  },
};
</script>
<style scoped>
.uploadCard {
  border-width: 2px;
  border-color: black;
  border-style: dashed;
}
</style>
