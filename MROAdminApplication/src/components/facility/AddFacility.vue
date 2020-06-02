<template>
    <div>
        <h1 style="text-align:center">Add Facility</h1>
        <div class="addfacility-form">
            <form @submit.prevent="onSubmit">
                <div class="form-group row">
                    <label class="col-md-4" for="sFacilityName">Facility Name:</label>
                    <input type="text"
                           id="sFacilityName"
                           class="form-control col-md-6"
                           placeholder="Facility Name"
                           v-model="facility.sFacilityName">
                    <label class="col-md-4" for="sDescription">Facility Description:</label>
                    <input type="text"
                           id="sDescription"
                           class="form-control col-md-6"
                           placeholder="General Description"
                           v-model="facility.sDescription">
                    <label class="col-md-4" for="sSMTPUsername">SMTP Username:</label>
                    <input type="text"
                           id="sSMTPUsername"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP Username"
                           v-model="facility.sSMTPUsername">
                    <label class="col-md-4" for="sSMTPPassword">SMTP Password:</label>
                    <input type="password"
                           id="sSMTPPassword"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP Password"
                           v-model="facility.sSMTPPassword">
                    <label class="col-md-4" for="sSMTPUrl">SMTP URL:</label>
                    <input type="text"
                           id="sSMTPUrl"
                           class="form-control col-md-6"
                           placeholder="Enter SMTP URL"
                           v-model="facility.sSMTPUrl">
                    <label class="col-md-4" for="sFTPUsername">FTP Username:</label>
                    <input type="text"
                           id="sFTPUsername"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Username"
                           v-model="facility.sFTPUsername">
                    <label class="col-md-4" for="sFTPPassword">FTP Password:</label>
                    <input type="password"
                           id="sFTPPassword"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Password"
                           v-model="facility.sFTPPassword">
                    <label class="col-md-4" for="sFTPUrl">FTP URL:</label>
                    <input type="text"
                           id="sFTPUrl"
                           class="form-control col-md-6"
                           placeholder="Enter FTP Password"
                           v-model="facility.sFTPUrl">
                    <label class="col-md-4" for="sOutboundEmail">Outbound Email:</label>
                    <input type="text"
                           id="sOutboundEmail"
                           class="form-control col-md-6"
                           placeholder="Enter Outbound Email"
                           v-model="facility.sOutboundEmail">
                    <div class="col-md-4 offset-md-3 submit">
                        <button type="submit">Submit</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
    export default {
        name: "AddFacility",
        data() {
            return {
                facility: {
                    nROIFacilityID: 0,
                    sFacilityName: '',
                    sDescription: '',
                    sSMTPUsername: '',
                    sSMTPPassword: '',
                    sSMTPUrl: '',
                    sFTPUsername: '',
                    sFTPPassword: '',
                    sFTPUrl: '',
                    sOutboundEmail: '',
                    bActiveStatus: true
                }
            };
        },
        methods: {
            onSubmit() {
                this.$http.post('http://localhost:57364/api/facility/AddFacility', this.facility)
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
    .addfacility-form {
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