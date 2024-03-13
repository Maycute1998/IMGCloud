import axios from "./axios";

const login = async (username, password) => {
  return axios.post("auth/login", {
    username,
    password,
  });
};

const register = async (email, username, password) => {
  return axios.post("auth/register", {
    email,
    username,
    password,
  });
};

const getUserById = (id) => axios.get("/users?id=" + id);
const createUserInfo = async (userInfo) => {
  return axios.post("users/create", userInfo);
};

export { createUserInfo, getUserById, login, register };

