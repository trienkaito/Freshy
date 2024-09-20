import * as actionTypes from "./UserType";

const INITIAL_STATE = {
    account: {
        name: "",
        userId: "",
        role: [],
        token: "",
        employeeId: "",
        email: "",
        avatar: "",
        phone: ""
    },

    customer: {
        id: "",
        name: "",
        avatar: "",
        phone: "",
        email: "",
    },

    loginSuccess: false,
    loginFailure: false,
    RegisterSucceed: null,
};

const UserReducer = (state = INITIAL_STATE, action) => {
    switch (action.type) {
        case actionTypes.FETCH_USER_LOGIN:
            return {
                ...state,
                account: {
                    name: action.name,
                    userId: action.userId,
                    role: action.roles,
                    token: action.token,
                },
                loginSuccess: true,
            };

        case actionTypes.FETCH_USER_LOGIN_SUCCEED:
            return {
                ...state,
                customer: {
                    id: action.customerId,
                    name: action.name ,
                    avatar: action.avatar ,
                    phone: action.phone ,
                    email: action.email 
                },
                loginSuccess: false,
            };

            case actionTypes.CUSTOMER_PROFILE_EXITING :
            return {
                ...state,
                customer: {
                    id: action.customerId,
                },
                loginSuccess: false,
            };
        case actionTypes.FETCH_USER_SUCCESS:
            return {
                ...state,
                
            };
        case actionTypes.FETCH_ADMIN_LOGIN:
            return {
                ...state,
                account: {
                    userId : action.userId,
                    name: action.name,
                    role: action.roles,
                    token: action.token,
                }
                
            };
        case actionTypes.FETCH_USER_ERROR:
            return {
                ...state,
            };

        case actionTypes.FETCH_USER_LOGOUT:
            localStorage.removeItem("user");
            localStorage.removeItem("customer");
            //   localStorage.removeItem('email');
            return {
                ...state,
                account: {
                    name: "",
                    userId: "",
                    role: [],
                    token: "",
                },
                customer: {
                    id: "",
                    name: "",
                    avatar: "",
                    phone: "",
                    email: "",
                },
                loginFailure: true,
            };

        case actionTypes.USER_REFRESH:
            let user = JSON.parse(localStorage.getItem("user"));
            let cus = JSON.parse(localStorage.getItem("customer"));
            console.log(user);
            return {
                ...state,
                account: {
                    name: user.name,
                    userId: user.userId,
                    role: user.roles,
                    token: user.token,
                },
                customer : {
                    id: cus.customerId,
                    name: cus.name,
                    avatar: cus.avatar,
                    phone: cus.phone,
                    email: cus.email,
                }
            };
        case actionTypes.USER_REGISTER_SUCCEED:
            return {
                ...state,
                RegisterSucceed: true,
            };

        case actionTypes.USER_REGISTER_FINISH:
            return {
                ...state,

                RegisterSucceed: null,
            };

        default:
            return state;
    }
};

export default UserReducer;
