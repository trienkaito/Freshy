import { toast } from "react-toastify";
import * as actionTypes from "./UserType";
import { login, register, loginGoogle,CreateCustomerProfile } from "../../service/UserService";
import  {getCustomerIdByAccountId} from "../../service/CustomerService";
import { forEach } from "lodash";
import { type } from "jquery";
import { Toast } from "bootstrap";
///
const isValidEmail = (email) => {
    return email.includes("@");
};

export const handleNavigateLogin = () => {
    return (dispatch, getState) => {
        dispatch({
            type: actionTypes.USER_REGISTER_FINISH,
        });
    };
};

export const handleRegister = (
    userName,
    password,
    confirmPassword,
    email,
    phone,
    navigate
) => {
    return async (dispatch, getState) => {
        try {
            if (password === confirmPassword) {
                let res = await register(userName, password, email, phone);
                console.log(res);
                if (res) {
                    dispatch({
                        type: actionTypes.USER_REGISTER_SUCCEED,
                    });
                    toast.success("ĐĂNG KÝ THÀNH CÔNG!!!");
                    navigate("/login");
                } else {
                }
            } else {
                toast.warning("MẬT KHẨU KHÔNG KHỚP");
            }
        } catch (error) {
            toast.error(error);
        }
    };
};

export const handleLogin = (userName, password, navigate) => {
    return async (dispatch, getState) => {
        try {
            let res = await login(userName, password);
            console.log("login",res.data);
            console.log(res.data.token);
            if (res) {
                if (res.data.roles && !res.data.roles.includes("DEFAULTCUSTOMER")) {
                    // Nếu vai trò không phải là "Customer", hiển thị thông báo và không dispatch action
                    toast.error("Tài khoản không tồn tại");
                } else {
                    // Nếu là "Customer", tiếp tục dispatch action và lưu thông tin vào Local Storage
                    dispatch({
                        type: actionTypes.FETCH_USER_LOGIN,
                        token: res.data.token,
                        name: res.data.name,
                        userId: res.data.userId,
                        roles: res.data.roles,
                    });
                    localStorage.setItem("user", JSON.stringify(res.data));
                    toast.success("ĐĂNG NHẬP THÀNH CÔNG!")
                    navigate("/");
                  //  dispatch(handleCreateCustomerProfile(res.data.userId, res.data.email, res.data.phone,res.data.name));
                }
            } else {
                // Xử lý trường hợp không có phản hồi từ API
            }
        } catch (error) {
            toast.error(error);
        }
    };
};


export const handleLoginGoogle = (idToken, navigate) => {
    return async (dispatch, getState) => {
        try {
            let res = await loginGoogle(idToken);
            console.log(res.data);
            if (res) {
                dispatch({
                    type: actionTypes.FETCH_USER_LOGIN,
                    token: res.data.token,
                    name: res.data.name,
                    userId: res.data.userId,
                    roles: res.data.roles,
                });
                localStorage.setItem("user", JSON.stringify(res.data));   
                toast.success("ĐĂNG NHẬP THÀNH CÔNG!")
                navigate("/");
                console.log(res.data.userId);
                try {
                let cus = await getCustomerIdByAccountId(res.data.userId);
                console.log("CusIdByAccID: ", cus);
                if(cus){
                    console.log("thông tin khách hàng đã có");
                    dispatch( handleCustomerProfileExiting(cus));
                }
                
                } catch (error) {
                    dispatch(handleCreateCustomerProfile(res.data.userId, res.data.email, res.data.phone, res.data.name));
                }
                
                
            } else {
            }
        } catch (error) {
            toast.error(error);
        }
    };
};

export const handleLoginAdmin = (userName, password, navigate) => {
    return async (dispatch, getState) => {
        try {
            let res = await login(userName, password);
            // toast.error(res);
            console.log(res);
            if (res) {
                dispatch({
                    type: actionTypes.FETCH_ADMIN_LOGIN,
                    token: res.data.token,
                    name: res.data.name,
                    userId: res.data.userId,
                    roles: res.data.roles,
                });
                localStorage.setItem("user", JSON.stringify(res.data));
                navigate("/admin/product");


            } else {
            }
        } catch (error) {
            toast.error(error);
        }
    };
};

export const handleRefreshRedux = () => {
    return async (dispatch, getState) => {
        try {
            dispatch({
                type: actionTypes.USER_REFRESH,
            });
        } catch (error) {
            toast.error(error);
        }
    };
};


export const handleLogoutRedux = () => {
    return (dispatch, getState) => {
        try {
            localStorage.removeItem("user");
            localStorage.removeItem("email");
            dispatch({
          type: actionTypes.FETCH_USER_LOGOUT,
       },);
       toast.success("ĐĂNG XUẤT THÀNH CÔNG!")
        } catch (error) {
            
        }
       
    }
 }
 
 export const handleCreateCustomerProfile =  (userId,email, phone,name) => {
    return async (dispatch) =>{
        try {
            let res = await CreateCustomerProfile(userId,email, phone,name);
                console.log("CustomerProfiles",res.data.data);
            var customer = res.data.data;
            console.log("cus", customer);
                if (res) {
                dispatch({
                    type: actionTypes.FETCH_USER_LOGIN_SUCCEED,
                    customerId : customer.customerId,
                    name : customer.name,
                    avatar : customer.avatar,
                    email : customer.email,
                    phone : customer.phone
                });
                localStorage.setItem("customer", JSON.stringify(customer));
                
            } else {
            }


        } catch (error) {
            
        }
    } 
 }

 export const handleCustomerProfileExiting =  (res) => {
    return async (dispatch) =>{
        try {
                if (res) {
                dispatch({
                    type: actionTypes.CUSTOMER_PROFILE_EXITING,
                    customerId : res.data.data.customerId,
                });
             localStorage.setItem("customer", JSON.stringify(res.data.data));
            } else {
            }
        } catch (error) {
            
        }
    } 
 }