<template>
  <div>
    <h3 class="page-title">Almost done! Review and sign when ready. (<a @click="previous">Edit Request</a>)</h3>
    <v-dialog v-model="dialog" max-width="400px" style="backgroundColor:white">
      <v-card id="signatureCard">
        <v-btn style="fontColor:black, textAlign:right" icon @click="dialog = false">
            X
          </v-btn>
        <v-card-title class="headline">Draw your signature below</v-card-title>

        <v-card-text>
          <div class="row">
            <div class="col-12 mt-2">
              <VueSignaturePad
                id="signature"
                width="350px"
                height="120px"
                ref="signaturePad"
                :options="{onBegin: () => {$refs.signaturePad.resizeCanvas()}}"
              />
              <!--<VueSignaturePad :options="{onBegin: () => {$refs.signaturePad.resizeCanvas()}}" ref="signaturePad" />-->
            </div>
          </div>
          <div class="row">
            <div class="col-3 mt-2">
              <button class="btn btn-outline-secondary" @click="undo">Undo</button>
            </div>
            <div class="col-3 mt-2">
              <button class="btn btn-outline-primary" @click="save" data-dismiss="modal">Save</button>
            </div>
          </div>
        </v-card-text>
      </v-card>
    </v-dialog>

    <iframe :src="this.pdf" width="100%" height="1200px"></iframe>

    <div id="modalOpener">
      <div v-if="bFormSigned==false">
        <p id="review">All finished reviewing?</p>
        <v-btn id="signHere" @click="showDialog" color="white">Sign request</v-btn>
      </div>
      <div v-else>
        <v-btn id="submitRequest" @click="next" color="success">Submit Request</v-btn>
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
    </div>
  </div>
</template>

<script>

export default {
  data() {
    return {
      dialog: false,
      options: {
        penColor: "#FFF"
      },
      currentPage: 0,
      pageCount: 0,
      pdf: null,
      bFormSigned:false,
      dialogLoader:false
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
        this.pdf = fileURL;
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
          this.pdf = fileURL;
          this.dialogLoader=false;
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
        this.pdf=null;
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
        this.$http
      .post("Wizards/GenerateXML/", 
      this.$store.state.requestermodule,
      )
      .then(response => {
        console.log(response.body);
      });
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
#signature {
  border: double 3px transparent;
  border-radius: 5px;
  background-image: linear-gradient(white, white),
    radial-gradient(circle at top left, #4bc5e8, #9f6274);
  /* background-image: url('https://image.shutterstock.com/image-illustration/abstract-gray-background-260nw-508982365.jpg'); */
  background-origin: border-box;
  background-clip: content-box, border-box;
}
#modalOpener {
  position: fixed;
  bottom: 150px;
  left: 50%;
  transform: translate(-50%, 0%);
  border-color: #53b958;
  background-color: #53b958;
  border-radius: 5px;
  -moz-border-radius: 5px;
  -webkit-border-radius: 5px;
  color: #ffffff;
  white-space: nowrap;
}
#review {
  font-size: 18px;
  display: inline-block;
  padding: 10px;
  margin-right: 5px;
  margin-left: 5px;
  margin-top: 16px ;
}
#signHere {
  background-color: #ffffff;
  border: 1px solid #ffffff;
  color: #333333;
  font-size: 18px;
  display: inline-block;
  border-radius: 3px;
  -moz-border-radius: 3px;
  -webkit-border-radius: 3px;
  padding-top: 3px !important;
  padding-bottom: 3px !important;
  padding-left: 15px !important;
  padding-right: 15px !important;
  width: auto !important;
  text-decoration: none;
  margin-right: 15px;
}
.page-title{
    color:black;
    text-align: center;
}
</style>