import axios from "./axios";

const login = async (username, password) => {
  return axios.post("/login", {
    username,
    password,
  });
};

const register = async (email, username, password) => {
  return axios.post("/register", {
    email,
    username,
    password,
  });
};

const getUserByName = (username) => axios.get("/user?userName=" + username);

export { login, register, getUserByName };
