import React, { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { NavLink } from 'react-router-dom';
import "./login.css"
import "./popuo-box.css"
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { handleLogin, handleLoginGoogle } from '../../../redux/user/UserAction'

function ForgotPassword() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    //const role = useSelector(state => state.);
    const [email, setEmailForgot] = useState(); 
    const [OTP, setOTP] = useState("");
    const [newPass, setNewPassword] = useState("");
    const [errors, setErrors] = useState("");

    const HandleOTPChange = (event) => {
        setOTP(event.target.value);
    };
    const HandleEmailChange = (event) => {
        setEmailForgot(event.target.value);
    }
    const HandleNewPasswordChange = (event) => {
        setNewPassword(event.target.value);
    };
    const handleSendEmail = async (event) =>{
            event.preventDefault();
            // Kiểm tra validation trước khi đăng nhập
            if (validateForm()) {
              // Gọi hàm xử lý đăng nhập
             
              
            }
    }

    const validateForm = () => {
        let errors = {};
        let isValid = true;

        if (!OTP) {
            errors.OTP = "Vui lòng nhập OTP";
            isValid = false;
        }

        if (!newPass) {
            errors.newPass = "Vui lòng nhập mật khẩu mới";
            isValid = false;
        }

        setErrors(errors);
        return isValid;
    };

    return (
        <div className='body-login'>
            <div className='login-formbody'>
                <div className="w3layoutscontaineragileits">
                    <h2>VUI LÒNG NHẬP EMAIL</h2>
                    <form onSubmit={handleSendEmail}>
                        <input type="email" name="Email" placeholder="Email" required=""
                            onChange={HandleEmailChange}
                        />
                        {errors.email && <p className="error">{errors.email}</p>}
                        <input type="submit" value="GỬI" />           
                    </form>   
                </div>
            </div>
        </div>
    );
}

export default ForgotPassword;
