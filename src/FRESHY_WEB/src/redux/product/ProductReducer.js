import {
    FETCH_PRODUCTS,
    ADD_PRODUCTS_SUCCESS,
    ADD_PRODUCTS_FAILED,
    UPDATE_PRODUCT_SUCCESS,
    UPDATE_PRODUCT_FAILED,
    DELETE_PRODUCT_FAILED,
    DELETE_PRODUCT_SUCCESS,
    SEARCH_PRODUCT,
    PRODUCT_SAVE,
    GET_NAME_CATEGORY,
    PRODUCT_DETAIL,
    CREATE_ORDER_ONLINE,
    SEARCH_PRODUCT_ORDER_OFFLINE,
    PRODUCT_DISCOUNT,
    PAID_SUCCESS,
    FETCH_PRODUCTS_FULL,
    SET_PAID_TYPE,
    SAVE_IMAGE_UNITS
} from "./ProductType.js";

const initialState = {
    loading: true,
    productunits:[],
    products: [],
    cartItems: [],
    product: {},
    selectedCarts: [],
    totalPages: null,
    AddSuccess: null,
    AddFailed: null,
    StatusDelete: null,
    UPDATESUCCESS: null,
    UPDATEFAILED: null,
    DELETESUCCESS: null,
    NAMECATEGORY: "THIT",
    discount:null,
    paid_success:null,
    paid_type:null,
    image_units_save:[],
};

const productsReducer = (state = initialState, action) => {
    switch (action.type) {
        case FETCH_PRODUCTS:
            return {
                ...state,
                loading: false,
                products: action.products,
                totalPages: action.totalPages,
            };
        case ADD_PRODUCTS_SUCCESS:
            return {
                ...state,
                loading: true,
                product: action.product,
                AddSuccess: action.AddSuccess,
            };
        case ADD_PRODUCTS_FAILED:
            return {
                ...state,
                loading: false,
                product: action.product,
                AddFailed: true,
            };
        case DELETE_PRODUCT_SUCCESS:
            return {
                ...state,
                loading: false,
                product: action.product,
                DELETESUCCESS: true,
            };
        case SEARCH_PRODUCT:
            return {
                ...state,
                loading: false,
                products: action.products,
                totalPages: action.totalPages,
            };
        case SEARCH_PRODUCT_ORDER_OFFLINE: 
        return {
            ...state,
            loading: false,
            products: action.products,
            totalPages: action.totalPages,
        };
        case UPDATE_PRODUCT_SUCCESS:
            return {
                ...state,
                loading: true,
                product: action.product,
                UPDATESUCCESS: true,
            };
        case ADD_PRODUCTS_FAILED:
            return {
                ...state,
                loading: false,
                product: action.product,
                UPDATEFAILED: true,
            };
        case PRODUCT_SAVE:
            return {
                ...state,
                AddSuccess: null,
                AddFailed: null,
                UPDATESUCCESS: null,
                UPDATEFAILED: null,
                DELETESUCCESS: null,
                discount:null,
                paid_success:null,
                paid_type:null

            };
        case GET_NAME_CATEGORY:
            return {
                ...state,
                NAMECATEGORY: action.name,
            };
        case PRODUCT_DETAIL:
            return {
                ...state,
                loading: false,
                product: action.product,
            };
        case PRODUCT_DISCOUNT:
                return {
                    ...state,
                    loading: false,
                    discount: action.discount,
                };
        case PAID_SUCCESS:
                    return {
                        ...state,
                        loading: false,
                        paid_success:action.paid_success,
                    };
        case FETCH_PRODUCTS_FULL:
                        return {
                            ...state,
                            productunits:action.productunits
                        };
        case SET_PAID_TYPE:
            return {
                ...state,
                paid_type:action.paid_type
            };
            case SAVE_IMAGE_UNITS:
                return {
                    ...state,
                    image_units_save:action.image_units_save
                };
                case CREATE_ORDER_ONLINE:
            return {
                ...state,
                selectedCarts : action.selectedCartItem

            } 
        default:
            return state;
    }
};

export default productsReducer;
