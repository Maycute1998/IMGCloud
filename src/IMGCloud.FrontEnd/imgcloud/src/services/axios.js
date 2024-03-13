import axios from "axios";
const instance = axios.create({
  baseURL: "https://localhost:7202/api",
});

instance.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    return Promise.reject(error);
  }
);

export default instance;
