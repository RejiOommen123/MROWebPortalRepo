<!--<template>
 <div class="center">
     <br>
     <div class="alert alert-success">You have Successfully Completed the Wizard</div>
     <div class="alert alert-success">Please Check your EmaiID for the Completely filled Form</div>

    <div class="alert alert-info alert-dismissible">
  <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Click &times;</strong> on Top Right Corner to Exit
</div>
     </div>

</template>-->
<!--<script>
export default {
    name:"ninthPage",
    data(){
        return{}
    }
}
</script>
<style scoped>
.center {
  text-align: center;
}
</style>-->
<!--Signature Pad Code Start-->
<template>
    <div id="app">
        <div class="container">
            <div class="row">
                <div class="col-12 mt-2">
                    <VueSignaturePad id="signature"
                                     width="100%"
                                     height="500px"
                                     ref="signaturePad"
                                     :options="options" />
                </div>
            </div>
            <div class="row">
                <div class="col-3 mt-2">
                    <button class="btn btn-outline-secondary" @click="undo">Undo</button>
                </div>
                <div class="col-3 mt-2">
                    <button class="btn btn-outline-primary" @click="save">Save</button>
                </div>
                <div class="col-3 mt-2">
                    <button class="btn btn-outline-primary" @click="change">Change</button>
                </div>
                <div class="col-3 mt-2">
                    <button class="btn btn-outline-primary" @click="resume">Resume</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "App",
        data: () => ({
            options: {
                penColor: "#c0f"
            }
        }),
        methods: {
            undo() {
                this.$refs.signaturePad.undoSignature();
            },
            save() {
                const { data } = this.$refs.signaturePad.saveSignature();
                this.$store.commit("appmodule/mutatesignature", data);
                alert("Open DevTools see the save data.");
                //console.log(isEmpty);
                //console.log(data);
                var patient = {
                    selectedLocation: this.$store.state.appmodule.selectedLocation,
                    notPatient: this.$store.state.appmodule.notPatient,
                    rname: this.$store.state.appmodule.rname,
                    relationToPatient: this.$store.state.appmodule.relationToPatient,
                    emailID: this.$store.state.appmodule.emailID,

                    confirmReport: this.$store.state.appmodule.confirmReport,
                    fname: this.$store.state.appmodule.fname,
                    minitial: this.$store.state.appmodule.minitial,
                    lname: this.$store.state.appmodule.lname,
                    isPatientDeceased: this.$store.state.appmodule.isPatientDeceased,
                    postalCode: this.$store.state.appmodule.postalCode,
                    streetArea: this.$store.state.appmodule.streetArea,
                    bDay: this.$store.state.appmodule.bDay,
                    imgdata: this.$store.state.appmodule.imgdata
                };
                this.$http
                    .post('http://localhost:57364/api/PDF/GeneratePDF/', patient, { responseType: "arraybuffer" })
                    .then(response => {
                        let blobFile = new Blob([response.data], { type: "application/pdf" });
                        //let link = document.createElement('a');
                        var fileURL = URL.createObjectURL(blobFile);
                        window.open(fileURL, "_blank", false);
                        // window.open(fileURL, "popup", "width=600,height=600");

                        //Options are:
                        //_blank - URL is loaded into a new window, or tab.This is default
                        //_parent - URL is loaded into the parent frame
                        //_self - URL replaces the current page
                        //_top - URL replaces any framesets that may be loaded
                        //true - URL replaces the current document in the history list
                        //false - URL creates a new entry in the history list
                    })
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
            }
        }
    };
</script>

<style>
    #signature {
        border: double 3px transparent;
        border-radius: 5px;
        background-image: linear-gradient(white, white), radial-gradient(circle at top left, #4bc5e8, #9f6274);
        background-origin: border-box;
        background-clip: content-box, border-box;
    }
</style>
<!--Signature Pad Code End-->

<template>
    <div>
        All Finish Reviewing<button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Sign</button>

        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog" >
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Modal Header</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="row">
                                <div class="col-12 mt-2">
                                    <VueSignaturePad id="signature"
                                                     width="100%"
                                                     height="500px"
                                                     ref="signaturePad"
                                                    :options="{onBegin: () => {$refs.signaturePad.resizeCanvas()}}" />
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
                                <div class="col-3 mt-2">
                                    <button class="btn btn-outline-primary" @click="change" >Change</button>
                                </div>
                                <div class="col-3 mt-2">
                                    <button class="btn btn-outline-primary" @click="resume">Resume</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
        <pdf :src=this.pdf>
            @num-pages="pageCount = $event"
            @page-loaded="currentPage = $event">
        </pdf>
    </div>
</template>

<script>

    import pdf from 'vue-pdf'

    export default {
        components: {
            pdf
        },
        data() {
            return {
                options: {
                    penColor: "#c0f"
                },
                currentPage: 0,
                pageCount: 0,
                pdf : null
            }
        },
        mounted() {
            var patient = {
                selectedLocation: this.$store.state.appmodule.selectedLocation,
                notPatient: this.$store.state.appmodule.notPatient,
                rname: this.$store.state.appmodule.rname,
                relationToPatient: this.$store.state.appmodule.relationToPatient,
                emailID: this.$store.state.appmodule.emailID,

                confirmReport: this.$store.state.appmodule.confirmReport,
                fname: this.$store.state.appmodule.fname,
                minitial: this.$store.state.appmodule.minitial,
                lname: this.$store.state.appmodule.lname,
                isPatientDeceased: this.$store.state.appmodule.isPatientDeceased,
                postalCode: this.$store.state.appmodule.postalCode,
                streetArea: this.$store.state.appmodule.streetArea,
                bDay: this.$store.state.appmodule.bDay
            };
            this.$http
                .post('http://localhost:57364/api/PDF/GeneratePDF/', patient, { responseType: "arraybuffer" })
                .then(response => {
                    let blobFile = new Blob([response.data], { type: "application/pdf" });
                    //let link = document.createElement('a');
            
                    var fileURL = URL.createObjectURL(blobFile);
                    this.pdf = fileURL;
                    //window.open(fileURL, "_blank", false);
                    // window.open(fileURL, "popup", "width=600,height=600");

                    //Options are:
                    //_blank - URL is loaded into a new window, or tab.This is default
                    //_parent - URL is loaded into the parent frame
                    //_self - URL replaces the current page
                    //_top - URL replaces any framesets that may be loaded
                    //true - URL replaces the current document in the history list
                    //false - URL creates a new entry in the history list
                })
        },
        methods: {
            undo() {
                this.$refs.signaturePad.undoSignature();
            },
            save() {
                const { data } = this.$refs.signaturePad.saveSignature();
                this.$store.commit("appmodule/mutatesignature", data);
                alert("Open DevTools see the save data.");
                //console.log(isEmpty);
                //console.log(data);
                var patient = {
                    selectedLocation: this.$store.state.appmodule.selectedLocation,
                    notPatient: this.$store.state.appmodule.notPatient,
                    rname: this.$store.state.appmodule.rname,
                    relationToPatient: this.$store.state.appmodule.relationToPatient,
                    emailID: this.$store.state.appmodule.emailID,

                    confirmReport: this.$store.state.appmodule.confirmReport,
                    fname: this.$store.state.appmodule.fname,
                    minitial: this.$store.state.appmodule.minitial,
                    lname: this.$store.state.appmodule.lname,
                    isPatientDeceased: this.$store.state.appmodule.isPatientDeceased,
                    postalCode: this.$store.state.appmodule.postalCode,
                    streetArea: this.$store.state.appmodule.streetArea,
                    bDay: this.$store.state.appmodule.bDay,
                    imgdata: this.$store.state.appmodule.imgdata
                };
                this.$http
                    .post('http://localhost:57364/api/PDF/GeneratePDF/', patient, { responseType: "arraybuffer" })
                    .then(response => {
                        let blobFile = new Blob([response.data], { type: "application/pdf" });
                        //let link = document.createElement('a');
                        var fileURL = URL.createObjectURL(blobFile);
                        this.pdf = fileURL;
                        // window.open(fileURL, "popup", "width=600,height=600");

                        //Options are:
                        //_blank - URL is loaded into a new window, or tab.This is default
                        //_parent - URL is loaded into the parent frame
                        //_self - URL replaces the current page
                        //_top - URL replaces any framesets that may be loaded
                        //true - URL replaces the current document in the history list
                        //false - URL creates a new entry in the history list
                    })
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
            }
        }
    }


</script>
<style>
    #signature {
        border: double 3px transparent;
        border-radius: 5px;
        background-image: linear-gradient(white, white), radial-gradient(circle at top left, #4bc5e8, #9f6274);
        background-origin: border-box;
        background-clip: content-box, border-box;
    }
</style>