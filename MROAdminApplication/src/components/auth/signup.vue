<template>
    <div id="signup">
        <h1 style="text-align:center">Sign Up</h1>
        <div class="signup-form">
            <form @submit.prevent="onSubmit">
                <div class="input">
                    <label for="email">Mail</label>
                    <input type="email"
                           placeholder="Enter Email"
                           id="email"
                           v-model="email">
                </div>
                <!--<div class="input">
              <label for="age">Your Age</label>
              <input
                      type="number"
                      id="age"
                      v-model.number="age">
            </div>-->
                <div class="input">
                    <label for="password">Password</label>
                    <input type="password"
                           placeholder="Enter Password"
                           id="password"
                           v-model="password">
                </div>
                <div class="input">
                    <label for="confirm-password">Confirm Password</label>
                    <input type="password"
                           placeholder="Re-enter Password"
                           id="confirm-password"
                           v-model="confirmPassword">
                </div>
                <!--<div class="input">
              <label for="country">Country</label>
              <select id="country" v-model="country">
                <option value="usa">USA</option>
                <option value="india">India</option>
                <option value="uk">UK</option>
                <option value="germany">Germany</option>
              </select>
            </div>
            <div class="hobbies">
              <h3>Add some Hobbies</h3>
              <button @click="onAddHobby" type="button">Add Hobby</button>
              <div class="hobby-list">
                <div
                        class="input"
                        v-for="(hobbyInput, index) in hobbyInputs"
                        :key="hobbyInput.id">
                  <label :for="hobbyInput.id">Hobby #{{ index }}</label>
                  <input
                          type="text"
                          :id="hobbyInput.id"
                          v-model="hobbyInput.value">
                  <button @click="onDeleteHobby(hobbyInput.id)" type="button">X</button>
                </div>
              </div>
            </div>-->
                <div class="input inline">
                    <input type="checkbox" id="terms" v-model="terms">
                    <label for="terms">Accept Terms of Use</label>
                </div>
                <div class="submit">
                    <button type="submit">Submit</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
  //import axios from '../../axios-auth';

    export default {
        data() {
            return {
                email: '',
                //age: null,
                password: '',
                confirmPassword: '',
                //country: 'usa',
                //hobbyInputs: [],
                terms: false
            }
        },
        methods: {

            onSubmit() {
                const user = {
                    "AdminUserId": 0,
                    "Email": this.email,
                    "Password": this.password
                }
                this.$http.post('http://localhost:57364/api/auth/SignUp', user)
                    .then(response => {
                        if (response.ok == true) {
                            this.$router.push('/signin')
                        }
                    });
                //}
            }
        }
    }
</script>

<style scoped>
  .signup-form {
    width: 400px;
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