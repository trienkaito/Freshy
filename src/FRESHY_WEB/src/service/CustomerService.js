import customize from "./axios/axios";
export const SearchCustomer = (content) => {
    return customize.get(`/profile/SearchCustomer/${content}`);
}
export const getCustomerIdByAccountId = (accountId) => {
    return customize.get(`/profile/customer/${accountId}`);
}

export const getOrderAddressByCustomerId = (customerId) => {
    return customize.get(`/order/address/${customerId}`);
}