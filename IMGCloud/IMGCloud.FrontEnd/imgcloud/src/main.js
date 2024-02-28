import { createApp } from "vue";
import App from "./App.vue";
import Buefy from "buefy";
import "buefy/dist/buefy.css";

const app = createApp(App);
app.use(Buefy);
app.mount("#app");
