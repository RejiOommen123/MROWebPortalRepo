<template>
    <div id="signin">
        <h1 style="text-align:center">Sign In</h1>
        <div class="signin-form">
            <form @submit.prevent="onSubmit">
                <div class="input">
                    <label for="email">Mail</label>
                    <input type="email"
                           placeholder="Enter Email"
                           id="email"
                           v-model="email">
                </div>
                <div class="input">
                    <label for="password">Password</label>
                    <input type="password"
                           placeholder="Enter Password"
                           id="password"
                           v-model="password">
                </div>
                <div class="submit">
                    <button type="submit">Submit</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
  export default {
    data () {
      return {
        email: '',
        password: ''
      }
    },
    methods: {
      onSubmit () {
      var user = {
          "AdminUserId": 0,
          "Email": this.email,
          "Password": this.password
      }
            this.$http.post('http://localhost:57364/api/auth/SignIn', user)
          .then(response => {
              if (response.ok == true) {
                  this.$store.commit("mutateIsSignIn", true);
                  this.$router.push('/dashboard')
              }
          });
      }
    }
  }
</script>

<style scoped>
  .signin-form {
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

  .input input {
    font: inherit;
    width: 100%;
    padding: 6px 12px;
    box-sizing: border-box;
    border: 1px solid #ccc;
  }

  .input input:focus {
    outline: none;
    border: 1px solid #521751;
    background-color: #eee;
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