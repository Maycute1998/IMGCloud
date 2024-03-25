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

const getUserDetailsByName = (userName) => axios.get("/users?name=" + userName);
const createUserInfo = async (userInfo) => {
  return axios.post("users/create", userInfo);
};

const forgotPassword = async (email) => {
  return axios.post("users/forgot-password?email="+ email);
};

const resetPassword = async (resetPasswordRequest) => {
  return axios.post("users/reset-password", resetPasswordRequest);
};

export { createUserInfo, forgotPassword, getUserDetailsByName, login, register, resetPassword };

