import { parsePath } from "history";
import customize from "./axios/axios";

export const fetchAllCart = async (customerId) => {
  try {
    console.log("fetch cart : ",customerId);
    const response = await customize.get(`/profile/customer/${customerId}/cart`);
    console.log("re",response.data.data);
    return response; // Trả về dữ liệu trong phản hồi thay vì phản hồi đầy đủ
  } catch (error) {
    console.error('Error fetching cart:', error);
    throw error;
  }
};

export const fetchCheckAndGetVoucher = async (code) => {
  try {
      const response = await customize.get('/special/discountValue', {
          params: {
              code: code
          }
      });
      return response.data;
  } catch (error) {
      console.error('Error checking and getting voucher:', error);
      throw error;
  }
};

export const fetchAddOrder = async (resp) => {
  try{
    const response = await customize.post('/order/online', resp);
    return response;
  } catch (error) {
    console.error('Error checking and getting voucher:', error);
    throw error;
}
}

export const fetchAllShipping = async () => {
  try {
    const response = await customize.get('/special/shipping');
    return response;
  } catch (error) {
    
  }
}