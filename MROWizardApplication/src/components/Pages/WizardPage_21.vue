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
      <div v-if="recordSubmitted==false">
        <p id="review">All finished reviewing?</p>
        <v-btn id="signHere" @click="showDialog" color="white">Sign request</v-btn>
      </div>
      <div v-else>
        <v-btn id="submitRequest" @click="next" color="success">Submit Request</v-btn>
    </div>
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
      recordSubmitted:false
    };
  },
  mounted() {
    this.$http
      .post("PDF/FillAndSendPDF/", 
      this.$store.state.requestermodule,
      {
        responseType: "arraybuffer"
      })
      .then(response => {
        let blobFile = new Blob([response.data], { type: "application/pdf" });
        var fileURL = URL.createObjectURL(blobFile);
        this.pdf = fileURL;
      });
  },
  created(){ 
    this.$vuetify.theme.dark = false
  },
  methods: {
    undo() {
      this.$refs.signaturePad.undoSignature();
    },
    save() {
      this.dialog = false;
      const { data } = this.$refs.signaturePad.saveSignature();
      this.$store.commit("requestermodule/sSignatureData", data);
      console.log(data);
      // var patient = {
      //   sSelectedLocation: this.$store.state.requestermodule.sSelectedLocation,
      //   bAreYouPatient: this.$store.state.requestermodule.bAreYouPatient,
      //   sRelativeName: this.$store.state.requestermodule.sRelativeName,
      //   sRelationToPatient: this.$store.state.requestermodule.sRelationToPatient,
      //   sPatientEmailId: this.$store.state.requestermodule.sPatientEmailId,

      //   bConfirmReport: this.$store.state.requestermodule.bConfirmReport,
      //   sPatientFirstName: this.$store.state.requestermodule.sPatientFirstName,
      //   sPatientMiddleInitial: this.$store.state.requestermodule.sPatientMiddleInitial,
      //   sPatientLastName: this.$store.state.requestermodule.sPatientLastName,
      //   bIsPatientDeceased: this.$store.state.requestermodule.bIsPatientDeceased,
      //   sAddZipCode: this.$store.state.requestermodule.sAddZipCode,
      //   sAddStreetAddress: this.$store.state.requestermodule.sAddStreetAddress,
      //   bDay: this.$store.state.requestermodule.bDay,
      //   imgdata: this.$store.state.requestermodule.sSignatureData
      // };
      //patient.imgdata="data:image/png;base64,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";
      console.log( " Requester Data "+this.$store.state.requestermodule);
      this.$http
      
        .post(
          "PDF/SignAndSendPDF/",
          this.$store.state.requestermodule,
          { responseType: "arraybuffer" }
        )
        .then(response => {
          let blobFile = new Blob([response.data], { type: "application/pdf" });
          //let link = document.createElement('a');
          this.recordSubmitted=true;
          var fileURL = URL.createObjectURL(blobFile);
          this.pdf = fileURL;
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
        this.$store.commit("ConfigModule/mutatedialogMinWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxHeight", '653px');
        this.$store.commit("ConfigModule/mutatePreviousIndex");
        this.$vuetify.theme.dark = true;
    },
    showDialog(){
      this.dialog=true;
    },
    next(){
        this.$store.commit("ConfigModule/mutatedialogMinWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxWidth", '600px');
        this.$store.commit("ConfigModule/mutatedialogMaxHeight", '653px');
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