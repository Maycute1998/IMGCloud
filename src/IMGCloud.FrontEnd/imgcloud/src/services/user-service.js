import axios from "./axios";

const login = async (username, password) => {
  return axios.post("signin", {
    username,
    password,
  });
};

const register = async (email, username, password) => {
  return axios.post("register", {
    email,
    username,
    password,
  });
};

const getUserDetailsByName = (userName) => axios.get("/user?name=" + userName);
const createUserInfo = async (userInfo) => {
  return axios.post("user/create", userInfo);
};

const forgotPassword = async (email) => {
  return axios.post("forgot-password?email="+ email);
};

const resetPassword = async (resetPasswordRequest) => {
  return axios.post("reset-password", resetPasswordRequest);
};

export { createUserInfo, forgotPassword, getUserDetailsByName, login, register, resetPassword };

