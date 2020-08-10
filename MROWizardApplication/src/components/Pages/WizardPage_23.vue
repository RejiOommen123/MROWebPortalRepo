<template>
  <div>
    <h3 class="page-title">Almost done! Review and sign when ready. (<a @click="previous">Edit Request</a>)</h3>
    <v-dialog v-model="dialog" max-width="400px" style="backgroundColor:white">
      <v-card id="signatureCard">
        <v-btn class="wizardClose" icon @click="dialog = false">
            X
          </v-btn>
        <v-card-title >Draw your signature below</v-card-title>

        <v-card-text>
          <div class="row">
            <v-col cols="12" sm="3">
              <VueSignaturePad
                id="signature"
                width="350px"
                height="120px"
                ref="signaturePad"
                :options="{onBegin: () => {$refs.signaturePad.resizeCanvas()}}"
              />
               <!-- width="350px"
                " -->
              <!--<VueSignaturePad :options="{onBegin: () => {$refs.signaturePad.resizeCanvas()}}" ref="signaturePad" />-->
            </v-col>
          </div>
          <v-row>
            <v-col cols="3" sm="3">
              <button class="btn btn-outline-secondary" @click="undo">Undo</button>
                          
            </v-col>
            <v-col cols="6" offset-sm="3" sm="6">
              <button class="btn btn-outline-primary" style="align right" @click="save" data-dismiss="modal">Add my signature</button>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>

    <!-- <iframe id="pdfViewer" :src="this.pdf" width="100%" height="1200px"></iframe> -->
<!-- 
    <pdf :src="this.pdfURL" :page="1">
      <template slot="loading">
        loading content here...
      </template>
    </pdf> -->
    <div>
		<pdf
			v-for="i in numPages"
			:key="i"
			:src="src"
			:page="i"
			class="pdfViewer"
		></pdf>
	</div>

    <div id="modalOpener">
      <div v-if="bFormSigned==false">
        <p id="review">All finished reviewing?</p>
        <v-btn id="signHere" @click="showDialog" color="white">Sign request</v-btn>
      </div>
      <div v-else>
        <v-btn id="submitRequest" @click="next" class="next">Submit Request</v-btn>
    </div>
    <!-- Loader dialog -->
    <v-dialog v-model="dialogLoader" persistent width="300">
          <v-card color="primary" dark>
            <v-card-text>
              Generating PDF
              <v-progress-linear indeterminate color="white" class="mb-0"></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
    <!-- Onload Disclaimer -->
    <v-dialog
          v-model="onLoadDialog"
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
            @click="onLoadDialog = false"
          >
            Ok
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </div>
  </div>
</template>

<script>
import pdf from "vue-pdf";
export default {
  components: {
    pdf
  },
  data() {
    return {
      src: undefined,
			numPages: undefined,
      dialog: false,
      onLoadDialog:true,
      options: {
        penColor: "#FFF"
      },
      currentPage: 0,
      pageCount: 0,
      pdfURL: null,
      bFormSigned:false,
      dialogLoader:false,
      disclaimer : this.$store.state.ConfigModule.apiResponseDataByFacilityGUID.wizardHelper.Wizard_23_disclaimer01,
    };
  },
  activated(){
    this.$vuetify.theme.dark = false;
    this.dialogLoader=true;
    this.$http
      .post("wizards/GeneratePDF/", 
      this.$store.state.requestermodule,
      {
        responseType: "arraybuffer"
      })
      .then(response => {
        let blobFile = new Blob([response.data], { type: "application/pdf" });
        var fileURL = URL.createObjectURL(blobFile);
        this.pdfURL = fileURL;
        this.src = pdf.createLoadingTask(this.pdfURL);
        this.src.promise.then(pdf => {

            this.numPages = pdf.numPages;
          });
        this.dialogLoader=false;
      });
  },
  methods: {
    undo() {
      this.$refs.signaturePad.undoSignature();
    },
    save() {
      this.dialog = false;
      this.dialogLoader=true;
      const { data } = this.$refs.signaturePad.saveSignature();
      this.$store.commit("requestermodule/sSignatureData", data);

      this.$http
      
        .post(
          "wizards/GeneratePDF/",
          this.$store.state.requestermodule,
          { responseType: "arraybuffer" }
        )
        .then(response => {
          let blobFile = new Blob([response.data], { type: "application/pdf" });
          //let link = document.createElement('a');
          this.bFormSigned=true;
          var fileURL = URL.createObjectURL(blobFile);
          this.pdfURL = fileURL;
          this.src = pdf.createLoadingTask(this.pdfURL);
          this.src.promise.then(pdf => {

              this.numPages = pdf.numPages;
            });
          this.dialogLoader=false;
          console.log(JSON.stringify(this.$store.state.requestermodule));          
        });
    },
    change() {
      this.options = {
        penColor: "#00f"
      };
    },
    resume() {
      this.options = {
        penColor: "#c0f"
      };
    },
    previous(){
        this.pdfURL=null;
        if( this.$refs.signaturePad!=null)
        {
          this.$refs.signaturePad.clearSignature();
        }
        this.bFormSigned=false;
        this.$store.commit("requestermodule/sSignatureData", '');
        this.$store.commit("ConfigModule/mutatedialogMinWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxHeight", '653px');
        this.$vuetify.theme.dark = true;
        this.$store.commit("ConfigModule/mutatePreviousIndex");
       
    },
    showDialog(){
      this.dialog=true;
    },
    next(){      
      this.$store.commit("requestermodule/bRequestorFormSubmitted", true)
        this.$http
      .post("Wizards/GenerateXML/", 
      this.$store.state.requestermodule,
      )
        this.$store.commit("ConfigModule/mutatedialogMinWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxHeight", '653px');
        this.$vuetify.theme.dark = true;
        
       this.$store.commit("ConfigModule/mutateNextIndex");
    }
  }
};
</script>
<style>

</style>