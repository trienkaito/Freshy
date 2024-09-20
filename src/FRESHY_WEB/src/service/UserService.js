import axios from "./axios/axiosAuth";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const register = async (userName, password, email, phone) => {
    try {
        const response = await axios.post('/user/register', {
            userName: userName,
            email: email,
            password: password,
            phoneNumber: phone,
            roles: ['Customer']
        });
        return response
    } catch (error) {
        toast.error(error.response.data.message);
        throw new Error(error.response.data.message);
    }
};

const login = async (userName, password) => {
    try {
        const response = await axios.post('/user/login', {
            userName: userName,
            password: password
        });
        return response
    } catch (error) {
        console.log(error.res.message);
        toast.error(error.response.data.message);
        throw new Error(error.response.data.message);
    }
};

const loginGoogle = async (idToken) => {
    try {
        const response = await axios.post('/user/google-login', { idToken });
        return response
    } catch (error) {
        toast.error(error.response.data.message);
        throw new Error(error.response.data.message);
    }
};

const CreateCustomerProfile = async (accountId, email, phone, name) => {
    try {
        const response = await axios.post('https://localhost:5001/profile/customer', { accountId, email, phone, name });
        return response
    } catch (error) {
        toast.error(error.response.data.message);
        throw new Error(error.response.data.message);
    }
};

// const CreateEmployeeProfile = async (accountId, email, phone, name) => {
//     try {
//         const response = await axios.post('https://localhost:5001/profile/employee', { accountId, email, phone, name });
//         return response
//     } catch (error) {
//         toast.error(error.response.data.message);
//         throw new Error(error.response.data.message);
//     }
// };

export { register, login, loginGoogle, CreateCustomerProfile };
