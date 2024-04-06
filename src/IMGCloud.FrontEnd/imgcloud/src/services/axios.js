import axios from "axios";
const instance = axios.create({
  baseURL: "https://localhost:9000",
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
