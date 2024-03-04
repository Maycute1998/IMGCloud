import axios from "./axios";

const getAllProductsPaging = async (
  pageIndex,
  pageSize = 5,
  keyword = "",
  categoryId = null
) => {
  // return await fetch('https://reqres.in/api/users?page=2')
  //     .then(response => response.json())
  //let pageNumber = pageIndex === 0 ? 1 : pageIndex;
  return axios.get("/product/getAllPaging", {
    params: {
      pageIndex,
      pageSize,
      keyword,
      categoryId,
    },
  });
};

const getAllProducts = async () => {
  return axios.get("/product/getAll");
};

export { getAllProductsPaging, getAllProducts };
