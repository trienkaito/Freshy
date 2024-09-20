import customize from "./axios/axios";



export const fetchAllCustomerOrder = (pageNumber, pageSize, customerId) => {
  return customize.get("/order/customerorder",
  {
    params: {
      pageNumber: pageNumber,
      pageSize: pageSize,
      customerId: customerId
    }
  });
}

export const fetchAllCustomerOrderItems = (orderId) => {
  return customize.get("/order/customerorderitems",
  {
    params: {
      orderId : orderId
    }
  });
}

export const createOrderOffline =  (customerId, orderItems, paymentType, voucherCode, employeeId) => {
    return customize.post("/order/offline", {
      customerId: customerId,
      orderItems: orderItems,
      paymentType: paymentType,
      voucherCode: 
    { value : voucherCode},
      employeeId: employeeId
    });
  };
  
