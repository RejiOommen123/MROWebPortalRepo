<template>
  <header id="header">
    <div class="logo">
      <h3>
        <router-link to="/">MRO Portal Administration</router-link>
      </h3>
    </div>
    <div class="pageheader">
      <h1 id="pageHeaderH1">{{this.pageHeader}}</h1>
    </div>
    <nav>
      <!-- Nav Bar Starts From Here -->
      <ul>
        <li>
          <router-link to="/index">Dashboard</router-link>
        </li>
        <li>
          <router-link to="/facility">Manage Facilities</router-link>
        </li>
        <!-- TODO: Add avatar here -->
        <!-- <li>
                <button @click="onLogout" class="logout">Logout</button>
        </li>-->
      </ul>
    </nav>
  </header>
</template>

<script>
export default {
  computed: {
    pageHeader() {
      return this.$store.state.pageheader;
    }
  },
  methods: {
    onLogout() {
      //call for logout
      // this.$store.commit("mutateIsSignIn", false);
      // this.$router.push('/')
    }
  }
};
</script>

<style scoped>
#pageHeaderH1 {
  color: white;
}
#header {
  height: 70px;
  display: flex;
  flex-flow: row;
  justify-content: space-between;
  align-items: center;
  background-color: rgb(0, 91, 168);
  padding: 0 20px;
}

.logo {
  font-weight: bold;
  color: white;
}

.logo a {
  text-decoration: none;
  color: white;
}

nav {
  height: 100%;
}

ul {
  list-style: none;
  margin: 0;
  padding: 0;
  height: 100%;
  display: flex;
  flex-flow: row;
  align-items: center;
}

li {
  margin: 0 16px;
}

li a {
  text-decoration: none;
  color: white;
}

li a:hover,
li a:active,
li a.router-link-active {
  color: #fba437;
}
.logout {
  background-color: transparent;
  border: none;
  font: inherit;
  color: white;
  cursor: pointer;
}
#pageheader {
  width: 62%;
  text-align: center;
}
#pagheader h1 {
  color: white !important;
}
</style>