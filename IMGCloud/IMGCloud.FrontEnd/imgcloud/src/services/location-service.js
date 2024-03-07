import axios from "axios";
const getAllLocation = async () => {
  return axios.get("https://vn-public-apis.fpo.vn/provinces/getAll?limit=-1");
};

export default getAllLocation;
