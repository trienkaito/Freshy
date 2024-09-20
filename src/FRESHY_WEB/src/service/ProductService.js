import customize from "./axios/axios";
const fetchAllProduct = (pageNumber, pageSize) => {
    return customize.get(
        `/catalogue/products?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
};

export const fetchAllTypeOfProduct = () =>{
  return customize.get("/catalogue/types");
}
export const fetchAllSupplier = () =>{
  return customize.get("/partner/suplliers");
}
const updateProduct = (id, name, featureImage, description, typeId, importPrice, price, supplierId, dom, expiryDate, isShowToCustomer, employeeId,units) => 
    customize.put(`/catalogue/product/${id}`, {
        name,
        featureImage,       
        description,
        typeId,
        importPrice: { value: importPrice },
        price: { value: price },
        supplierId,
        dom,
        expiryDate,
        isShowToCustomer,
        employeeId: "44102A6A-36CE-4D66-834F-8907A7277C5A",
        units: [
            {
                unitType: "kg",
                unitValue: { value: 5 },
                quantity: { value: 100 },
            },
            {
                unitType: "kg",
                unitValue: { value: 2 },
                quantity: { value: 130 },
            },
        ],
    });

   


const createProduct = async (name, featureImage, description, typeId,supplierId, dom, expiryDate, isShowToCustomer, employeeId,units) => {
  return customize.post("/catalogue/product", {
    name,
    featureImage,
    description,
    typeId,
    supplierId,
    dom,
    expiryDate,
    isShowToCustomer:true,
    employeeId: "4DCF98E5-87DF-4CFD-95A6-A1BE603FAF21",
    units
  });
};
const deleteProduct = (id) => {
    return customize.delete(`/catalogue/product/${id}`, {
        headers: {
            "Content-Type": "application/json",
        },
        data: JSON.stringify({ id: id }),
    });
};
const searchProduct = (
    productname,
    suppliername,
    typename,
    pageNumber,
    pageSize
) => {
    return customize.get(
        `/catalogue/SearchProduct?ProductName=${productname}&SupplierName=${suppliername}&TypeName=${typename}&pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
};
export const SearchProductOrderOfflineService = (
    productname

) => {
    return customize.get(`/catalogue/SearchProductOrderOffline/${productname}`)
        
   
};

const productDetail = (productId) => {
    return customize.get(`/catalogue/product/${productId}`);
};

export const getAllUnitsService = (

) => {
    return customize.get(`/catalogue/getallunits`)
        
   
};
export const getUnitById=(id)=>{
    return customize.get(`/catalogue/getunitbyid/${id}`)
}
const addToCart = async (customerId, productId, productUnitId, boughtQuantity) => {
    return customize.post(`/profile/customer/cart`, {
        customerId: customerId,
        productId,
        productUnitId,
        boughtQuantity,
    });
};

const getCartItems = (id) => {
    return customize.get(`/profile/customer/${id}/cart`);
};
const deleteItemCart = (id, cartId) => {
    return customize.delete(`/profile/customer/${id}/item/${cartId}`);
};

const addProductLike = async (productId, customerId) => {
    return customize.post(`/profile/customer/productlike`, {
        productId,
        customerId,
    });
};

const getAllProductLike = (id) => {
    return customize.get(`/profile/customer/${id}/productlike`);
};

const deleteProductLike = (customerId, productId) => {
    return customize.delete(
        `/profile/customer/${customerId}/product/${productId}`
    );
};


export const uploadImage = async (formData) => {
    try {
        const response = await customize.post(`/image`, formData);
        return response.data; // Trả về dữ liệu từ phản hồi (response) nếu cần
    } catch (error) {
        throw new Error('Error uploading image: ' + error.message);
    }
};

export const updateProductUnit=(id,unitid,unitType,unitValue,quantity,importPrice,sellPrice,unitFeatureImage)=>{
    return customize.put(`/catalogue/product/${id}/unit/${unitid}`,{unitType,unitValue,quantity,importPrice,sellPrice,unitFeatureImage,id,unitid })
}
export const addProductUnit = (id, unitType, unitValue, quantity, importPrice, sellPrice, unitFeatureImage) => {
    return customize.post(`/catalogue/addunit/${id}/${unitType}/${unitValue}/${quantity}/${importPrice}/${sellPrice}`, { unitFeatureImage });
};
export const deleteProductUnit = (id) => {
    return customize.delete(`/catalogue/deleteunit/${id}`);
};



export {
    fetchAllProduct,
    createProduct,
    updateProduct,
    deleteProduct,
    searchProduct,
    productDetail,
    addToCart,
    getCartItems,
    deleteItemCart,
    addProductLike,
    getAllProductLike,
    deleteProductLike,
};