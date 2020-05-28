<template>
    <div>
        <h1 style="text-align:center">Edit Facility</h1>
        <div class="editfacility-form">
            <form @submit.prevent="onSubmit">
                <div class="form-group row">
                    <label class="col-md-4" for="facilityName">Facility Name:</label>
                    <input type="text"
                           id="facilityName"
                           class="form-control col-md-6"
                           placeholder="Facility Name"
                           v-model="facility.facilityName">
                    <label class="col-md-4" for="facilityAddress">Facility Address:</label>
                    <input type="text"
                           id="facilityAddress"
                           class="form-control col-md-6"
                           placeholder="Detail Address"
                           v-model="facility.facilityAddress">
                    <label class="col-md-4" for="description">Facility Description:</label>
                    <input type="text"
                           id="description"
                           class="form-control col-md-6"
                           placeholder="General Description"
                           v-model="facility.description">
                    <label class="col-md-4" for="numOfInstitution">Number of Institute:</label>
                    <input type="text"
                           id="numOfInstitution"
                           class="form-control col-md-6"
                           placeholder="Comma Seperated eg. NYU Hospital, NYU Clinic"
                           v-model="facility.numOfInstitution">
                    <label class="col-md-4" for="smtpUsername">SMTP Username:</label>
                    <input type="text"
                           id="smtpUsername"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP Username"
                           v-model="facility.smtpUsername">
                    <label class="col-md-4" for="smtpPassword">SMTP Password:</label>
                    <input type="text"
                           id="smtpPassword"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP Password"
                           v-model="facility.smtpPassword">
                    <label class="col-md-4" for="smtpUrl">SMTP URL:</label>
                    <input type="text"
                           id="smtpUrl"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP URL"
                           v-model="facility.smtpUrl">
                    <label class="col-md-4" for="ftpUsername">FTP Username:</label>
                    <input type="text"
                           id="ftpUsername"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Username"
                           v-model="facility.ftpUsername">
                    <label class="col-md-4" for="ftpPassword">FTP Password:</label>
                    <input type="text"
                           id="ftpPassword"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Password"
                           v-model="facility.ftpPassword">
                    <label class="col-md-4" for="ftpUrl">FTP URL:</label>
                    <input type="text"
                           id="ftpUrl"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Password"
                           v-model="facility.ftpUrl">
                    <label class="col-md-4" for="configFileUrl">Configuration File URL:</label>
                    <input type="text"
                           id="configFileUrl"
                           class="form-control col-md-6"
                           placeholder="Enter Path to Config File"
                           v-model="facility.configFileUrl">
                    <div class="col-md-4 offset-md-3 submit">
                        <button type="submit">Save</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
    export default {
        name: "EditFacility",
        data() {
            return {
                facility: {
                    facilityId: 0,
                    facilityName: '',
                    facilityAddress: '',
                    description: '',
                    numOfInstitution: '',
                    smtpUsername: '',
                    smtpPassword: '',
                    smtpUrl: '',
                    ftpUsername: '',
                    ftpPassword: '',
                    ftpUrl: '',
                    configFileUrl: '',
                    activeStatus: true
                }
                
            };
        },
        mounted() {
            this.$http.get('http://localhost:57364/api/facility/GetFacility/' + this.$route.params.id).then(response => {
                // get body data
                this.facility = JSON.parse(response.bodyText);

            }, response => {
                // error callback
                this.gridData = response.body;
            });
        },
        methods: {
            onSubmit() {
                this.$http.post('http://localhost:61379/api/facility/EditFacility/' + this.facility.facilityId, this.facility)
                    .then(response => {
                        if (response.ok == true) {
                            this.$router.push('/dashboard')
                        }
                    });
            }
        }
    }
</script>

<style scoped>
    * {
        margin: 10px;
    }

    .editfacility-form {
        width: 700px;
        margin: 30px auto;
        border: 1px solid #eee;
        padding: 20px;
        box-shadow: 0 2px 3px #ccc;
    }

    .input {
        margin: 10px auto;
    }

        .input label {
            display: block;
            color: #4e4e4e;
            margin-bottom: 6px;
        }

        .input.inline label {
            display: inline;
        }

        .input input {
            font: inherit;
            width: 100%;
            padding: 6px 12px;
            box-sizing: border-box;
            border: 1px solid #ccc;
        }

        .input.inline input {
            width: auto;
        }

        .input input:focus {
            outline: none;
            border: 1px solid #521751;
            background-color: #eee;
        }

        .input select {
            border: 1px solid #ccc;
            font: inherit;
        }


    .submit button {
        border: 1px solid #521751;
        color: #521751;
        padding: 10px 20px;
        font: inherit;
        cursor: pointer;
    }

        .submit button:hover,
        .submit button:active {
            background-color: #521751;
            color: white;
        }

        .submit button[disabled],
        .submit button[disabled]:hover,
        .submit button[disabled]:active {
            border: 1px solid #ccc;
            background-color: transparent;
            color: #ccc;
            cursor: not-allowed;
        }
</style>