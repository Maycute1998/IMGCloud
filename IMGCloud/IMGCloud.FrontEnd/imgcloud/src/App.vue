<template>
  <main>
    <Navigation />
    <div class="container">
      <Home />
    </div>
  </main>
</template>

<script>
import Navigation from "./components/Navigation.vue";
//import SignIn from "./components/SignIn.vue";
import Home from "./components/Home.vue";

export default {
  name: "App",
  components: {
    Navigation,
    Home,
  },
};
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
#app {
  font-family: "Poppins", sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  transition: 0.4s;
}
main {
  min-height: 100vh;
  padding: 25px;
}
</style>
