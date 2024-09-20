import React, { useState } from "react";
import { toast } from "react-toastify";
import { NavLink } from "react-router-dom";
import "../../../client/pages/login/login.css";
import { useNavigate } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { handleLogin, handleLoginAdmin } from "../../../redux/user/UserAction";

function Login() {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    //const role = useSelector(state => state.);

    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [errors, setErrors] = useState({});

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
            dispatch(handleLoginAdmin(userName, password, navigate));
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
        }

        setErrors(errors);
        return isValid;
    };

    return (
        <div className="body-login">
            <div className="login-formbody">
                <div className="w3layoutscontaineragileits">
                    <h2>ADMIN ĐĂNG NHẬP</h2>
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
                                <a href="#">Quên mật khẩu?</a>
                            </li>
                        </ul>
                        <div className="aitssendbuttonw3ls">
                            <input type="submit" value="ĐĂNG NHẬP" />
                            {/* <div className="social w3layouts">
                                <div className="heading">
                                    <h5>Hoặc đăng nhập</h5>
                                </div>
                                <div className="icons">
                                    <a href="#">
                                        <i
                                            className="fa-brands fa-google fa-xl"
                                            style={{ color: "#0967ae" }}
                                        ></i>
                                    </a>
                                    <a href="#">
                                        <i
                                            className="fa-solid fa-envelope fa-xl"
                                            style={{ color: "#0967ae" }}
                                        ></i>
                                    </a>
                                </div>
                            </div> */}
                            <div className="clear"></div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Login;
