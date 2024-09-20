import axios from "axios";
import { toast } from 'react-toastify';
const instance = axios.create({
    baseURL: 'https://localhost:5001',
  });
  instance.interceptors.response.use(function (response) {
    // Any status code that lie within the range of 2xx cause this function to trigger
    // Do something with response data
    return response;
  }, function (error) {

    let res ={} 
    // Any status codes that falls outside the range of 2xx cause this function to trigger
    // Do something with response error
    if (error.response) {
      // The request was made and the server responded with a status code
      // that falls out of the range of 2xx
      res.data =  console.log("res.error",error.response);
      // toast.error(error.response.data);
     res.status = console.log(error.response.status);
      res.headers = console.log(error.response.headers);
      // res.message = console.log(error.res.message);
    } else if (error.request) {
      // The request was made but no response was received
      // `error.request` is an instance of XMLHttpRequest in the browser 
      // and an instance of http.ClientRequest in node.js
      console.log(error.request);
    } else {
      // Something happened in setting up the request that triggered an Error
      console.log('Error', error.message);
    }
    return res;
  });



  export default instance;