import customize from "./axios/axios";

export const fetchAllEmployee = (pageNumber, pageSize) => {
  return customize.get(`/profile/employees?pageNumber=${pageNumber}&pageSize=${pageSize}`);
}

export const searchEmployee=(name,hometown,phone,pageNumber,pageSize)=>{
    return customize.get(`/profile/SearchEmployee?Fullname=${name}&HomeTown=${hometown}&PhoneNumber=${phone}&pageNumber=${pageNumber}&pageSize=${pageSize}`)
  
  }