import axios from "./axios";

const getAllPosts = async () => {
  return axios.get("/post/all-posts");
};

const getAllCollections = async () => {
  return axios.get("/post/all-collections");
};

const getByCollectionId = async (id) => {
  return axios.get("/post/collection-id?id="+id);
};


export { getAllCollections, getAllPosts, getByCollectionId };

