import axios from "axios";
import { useSelector } from "react-redux";
import { toast } from "react-toastify";
import { format } from "date-fns";

import {
    FETCH_PRODUCTS,
    ADD_PRODUCTS_SUCCESS,
    ADD_PRODUCTS_FAILED,
    DELETE_PRODUCT,
    UPDATE_PRODUCT_SUCCESS,
    UPDATE_PRODUCT_FAILED,
    PRODUCT_SAVE,
    SEARCH_PRODUCT,
    DELETE_PRODUCT_SUCCESS,
    RESET_STATE,
    GET_NAME_CATEGORY,
    SAVE_IMAGE_UNITS,
    PRODUCT_DETAIL,
    CREATE_ORDER_ONLINE,
    PRODUCT_DISCOUNT,
    SEARCH_PRODUCT_ORDER_OFFLINE,
    PAID_SUCCESS,
    FETCH_PRODUCTS_FULL,
    SET_PAID_TYPE
} from "./ProductType.js";
import {
    SearchProductOrderOfflineService,
    createProduct,
    deleteProduct,
    fetchAllProduct,
    getAllUnitsService,
    productDetail,
    searchProduct,
    updateProduct,
} from "../../service/ProductService.js";
import { update } from "lodash";

export const getProducts = (page, pagesize) => {
    return async (dispatch) => {
        try {
            const response = await fetchAllProduct(page, pagesize);
            dispatch({
                type: FETCH_PRODUCTS,
                products: response.data.data,
                totalPages: response.data.totalPage,
            });
        } catch (error) {
            console.log(error);
        }
    };
};

export const  addProducts =  (name, featureImage, description, typeId,supplierId, dom, expiryDate, isShowToCustomer, employeeId,units) => {
    return async (dispatch) => {
        try {
            const response = await createProduct(name, featureImage, description, typeId,supplierId, dom, expiryDate, isShowToCustomer, employeeId,units);
            console.log("check add",response)
            if(response.status==200){
            
            dispatch({
                type: ADD_PRODUCTS_SUCCESS,
                product : response.data,
                AddSuccess : true,
            });
            toast.success("Add Product Success !!!")
        }
        else{
            
         dispatch({
            type: ADD_PRODUCTS_FAILED,
            product : null,
        })
        }
        
        } catch (error) {
            console.log(error);
        }
    };
};
export const UpdateProduct = (
    id,
    name,
    featureImage,
    description,
    typeId,
    importPrice,
    price,
    supplierId,
    dom,
    expiryDate,
    isShowToCustomer,
    employeeId
) => {
    return async (dispatch) => {
        try {
            const response = await updateProduct(
                id,
                name,
                featureImage,
                description,
                typeId,
                importPrice,
                price,
                supplierId, // Sử dụng giá trị của Supplier thay vì supplierId
                format(new Date(dom), "dd/MM/yyyy"),
                format(new Date(expiryDate), "dd/MM/yyyy"),
                isShowToCustomer,
                employeeId
            );

            console.log("check upDATE...", response);
            if (response.status == 200) {
                toast.success("UPDATE Product Success !!!");

                dispatch({
                    type: UPDATE_PRODUCT_SUCCESS,
                    product: response.data,
                    UPDATESUCCESS: true,
                    UPDATEFAILED: false,
                });
            } else {
                toast.error("UPDATE Product Failed !!!");

                dispatch({
                    type: UPDATE_PRODUCT_FAILED,
                    product: response.data,
                    UPDATESUCCESS: false,
                    UPDATEFAILED: true,
                });
            }
        } catch (error) {
            console.log(error);
        }
    };
};

export const DeleteProduct = (id) => {
    return async (dispatch) => {
        try {
            const response = await deleteProduct(id);
            dispatch({
                type: DELETE_PRODUCT_SUCCESS,
                product: response.data,
                StatusDelete: true,
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const SearchProduct = (
    productname,
    suppliername,
    typename,
    pageNumber,
    pageSize
) => {
    return async (dispatch) => {
        try {
            if (pageNumber == null) {
                pageNumber = 1;
            }
            const response = await searchProduct(
                productname,
                suppliername,
                typename,
                pageNumber,
                pageSize
            );
            console.log("se",response.data.data)
            dispatch({
                type: SEARCH_PRODUCT,
                products: response.data.data,
                totalPages: response.data.totalPage,
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const SearchProductOrderOffline = (
    productname,
) => {
    return async (dispatch) => {
        try {
         
            const response = await SearchProductOrderOfflineService(
                productname,
            );
            dispatch({
                type: SEARCH_PRODUCT_ORDER_OFFLINE,
                products: response.data
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const resetState = () => {
    return async (dispatch) => {
        try {
            dispatch({
                type: PRODUCT_SAVE,
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const getNameCategory = (name1) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: GET_NAME_CATEGORY,
                name: name1,
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const getProductById = (productId) => {
    return async (dispatch) => {
        try {
            const response = await productDetail(productId);
            dispatch({
                type: PRODUCT_DETAIL,
                product: response.data.data,
            });
        } catch (error) {
            console.log(error);
        }
    };
};

export const selectedCartItem = (selectedCarts) => {
    return async (dispatch) => {
        try {
           
            dispatch({
                type: CREATE_ORDER_ONLINE,
                selectedCartItem: selectedCarts,
            });
        } catch (error) {
            console.log(error);
        }
    };
};

export const setDiscountRedux = (sale) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: PRODUCT_DISCOUNT,
                discount: sale,
            });
        } catch (error) {
            console.log(error);
        }
    };
};

export const setPaidSuccess = () => {
    return async (dispatch) => {
        try {
            dispatch({
                type: PAID_SUCCESS,
                paid_success:true,
            });
        } catch (error) {
            console.log(error);
        }
    };
};

export const getFullProducts = () => {
    return async (dispatch) => {
        try {
            const response = await getAllUnitsService();
            dispatch({
                type: FETCH_PRODUCTS_FULL,
                productunits: response.data,
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const setPaidType = (type) => {
    return async (dispatch) => {
        try {
            dispatch({
                type: SET_PAID_TYPE,
                paid_type:type
            });
        } catch (error) {
            console.log(error);
        }
    };
};
export const saveImageUnits = (list) => {
    return async (dispatch) => {
        try {
            dispatch({
                type:SAVE_IMAGE_UNITS,
                image_units_save:list
            });
        } catch (error) {
            console.log(error);
        }
    };
};