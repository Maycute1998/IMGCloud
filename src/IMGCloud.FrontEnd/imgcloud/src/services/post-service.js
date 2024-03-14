import axios from "./axios";

const getAllPosts = async (
) => {
  return axios.get("/posts/all-post");
};

export { getAllPosts };

