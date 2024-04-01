import axios from "./axios";

const getAllPosts = async () => {
  return axios.get("/posts/all-posts");
};

const getAllCollections = async () => {
  return axios.get("/posts/all-collections");
};

const getByCollectionId = async (id) => {
  return axios.get("/posts/collection-id?id="+id);
};


export { getAllCollections, getAllPosts, getByCollectionId };

