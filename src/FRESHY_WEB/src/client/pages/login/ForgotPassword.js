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
    const handleForgetPasswordSubmit = () => {
        const handleLoginSubmit = async (event) => {
            event.preventDefault();

            // Kiểm tra validation trước khi đăng nhập
            if (validateForm()) {
                // Gọi hàm xử lý đăng nhập

            }
        };

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
                    <p>Kiểm tra OTP ở trong email của bạn</p>
                    <h2>QUÊN MẬT KHẨU</h2>
                    <form onSubmit={handleForgetPasswordSubmit}  >
                        <input type="text" name="OTP" placeholder="OTP" required=""
                            onChange={HandleOTPChange}
                        />
                        {errors.OTP && <p className="error">{errors.OTP}</p>}
                        <input type="password" name="Password" placeholder="MẬT KHẨU MỚI" required=""
                            onChange={HandleNewPasswordChange}
                        />
                        {errors.newPass && <p className="error">{errors.newPass}</p>}
                        <div className="aitssendbuttonw3ls">
                            <input type="submit" value="xác nhận" />
                            <div className="clear"></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default ForgotPassword;
