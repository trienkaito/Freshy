import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { NavLink } from "react-router-dom";
import  "./login.css";
import "./popuo-box.css";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { handleLogin, handleLoginGoogle } from "../../../redux/user/UserAction";

function Login() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    //const role = useSelector(state => state.);

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [errors, setErrors] = useState({});

    const handleLoginCallbackResponse = (response) => {
        console.log("Encode JWT ID TOKEN: " + response.credential);
        dispatch(handleLoginGoogle(response.credential, navigate));
       // dispatch(handleCreateCustomerProfile(userId, email, phone, name));
    };

    useEffect(() => {
        /* global google */
        google.accounts.id.initialize({
            client_id:
                "491766819439-6m0kucv6vsoh274go1cqf1mpa4n7lbbo.apps.googleusercontent.com",
            callback: handleLoginCallbackResponse,
        });

        google.accounts.id.renderButton(document.getElementById("signInDiv"), {
            theme: "outline",
            size: "large",
        });
    }, []);

    const HandleUserNameChange = (event) => {
        setUserName(event.target.value);
    };

    const HandlePasswordChange = (event) => {
        setPassword(event.target.value);
    };

    const handleLoginSubmit = async (event) => {
        event.preventDefault();

        // Kiểm tra validation trước khi đăng nhập
        if (validateForm()) {
            // Gọi hàm xử lý đăng nhập
            dispatch(handleLogin(userName, password, navigate));
           // dispatch(handleCreateCustomerProfile(userId, email, phone, name));
        }
    };

    const validateForm = () => {
        let errors = {};
        let isValid = true;

        if (!userName) {
            errors.userName = "Vui lòng nhập tên đăng nhập";
            isValid = false;
        }

        if (!password) {
            errors.password = "Vui lòng nhập mật khẩu";
            isValid = false;
        } else if (password.length < 8) {
            errors.password = "Mật khẩu phải có ít nhất 8 ký tự";
            isValid = false;
        } else if (!/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&*])[A-Za-z\d@#$%^&*]/.test(password)) {
            errors.password = "Mật khẩu phải chứa ít nhất một ký tự chữ hoa, một ký tự chữ thường, một ký tự số, và một ký tự đặc biệt";
            isValid = false;
        }

        setErrors(errors);
        return isValid;
    };

    return (
        <div className="body-login">
            <div className="login-formbody">
                <div className="w3layoutscontaineragileits">
                    <h2>ĐĂNG NHẬP</h2>
                    <form onSubmit={handleLoginSubmit}>
                        <input
                         className="textlogin"
                            type="text"
                            name="Username"
                            placeholder="UserName"
                            required=""
                            onChange={HandleUserNameChange}
                        />
                        {errors.userName && (
                            <p className="error">{errors.userName}</p>
                        )}
                        <input
                            type="password"
                            name="Password"
                            placeholder="MẬT KHẨU"
                            required=""
                            onChange={HandlePasswordChange}
                        />
                        {errors.password && (
                            <p className="error">{errors.password}</p>
                        )}
                        <ul className="agileinfotickwthree">
                            <li>
                                <input type="checkbox" id="brand1" value="" />
                                <label htmlFor="brand1">
                                    <span></span>Lưu mật khẩu
                                </label>
                                <NavLink to="/forgotPassword">
                                    Quên mật khẩu?
                                </NavLink>
                            </li>
                        </ul>
                        <div className="aitssendbuttonw3ls">
                            <input type="submit" value="ĐĂNG NHẬP" />
                            <div className="social w3layouts">
                                <div className="heading">
                                    <h5>Hoặc đăng nhập</h5>
                                </div>

                                <div id="signInDiv"></div>
                            </div>
                            <p className="p-dk">
                                Đăng kí tạo tài khoản mới <span>→</span>{" "}
                                <NavLink
                                    to="/register"
                                    className="w3_play_icon1 "
                                >
                                    {" "}
                                    Đăng kí
                                </NavLink>
                            </p>
                            <div className="clear"></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Login;
