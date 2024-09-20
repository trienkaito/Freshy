import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import { NavLink } from 'react-router-dom';
import "./register.css"
import "./popuo-box.css"
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { handleRegister, handleNavigateLogin } from '../../../redux/user/UserAction'

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [userName, setUserName] = useState("");
  const [phone, setPhone] = useState("");
  const [errors, setErrors] = useState({});

  const RegisterSucceed = useSelector(state => state.user.RegisterSucceed);

  const navigate = useNavigate();
  const dispatch = useDispatch();


  const HandleRegister = (e) => {
    e.preventDefault(); // Ngăn chặn gửi mặc định của form
    if (validateForm()) {
      dispatch(handleRegister(userName, password, confirmPassword, email, phone, navigate))
    }
  }

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
    if (password !== confirmPassword) {
        errors.confirmPassword = "Mật khẩu xác nhận không khớp";
        isValid = false;
    }
    if (!email) {
        errors.email = "Vui lòng nhập địa chỉ email";
        isValid = false;
    } else if (!/\S+@\S+\.\S+/.test(email)) {
        errors.email = "Địa chỉ email không hợp lệ";
        isValid = false;
    } 
    if (!phone) {
        errors.phone = "Vui lòng nhập số điện thoại";
        isValid = false;
    } else if (!/^\d{10}$/.test(phone)) {
        errors.phone = "Số điện thoại không hợp lệ";
        isValid = false;
    }

    setErrors(errors);
    return isValid;
};


  const HandleEmailChange = (event) => {
    setEmail(event.target.value);
  };

  const HandlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const HandleConfirmPasswordChange = (event) => {
    setConfirmPassword(event.target.value);
  };

  const HandleUserNameChange = (event) => {
    setUserName(event.target.value);
  };

  const HandlePhoneChange = (event) => {
    setPhone(event.target.value);
  };

  return (
    <div className='body-login'>
      <div className="w3layoutscontaineragileits-register">
        <h2>Đăng Ký</h2>
        <form onSubmit={HandleRegister} className='row'>
          <div className='col-md-6'>
            <input
              className='textregister'
              type="text"
              placeholder="TÊN ĐĂNG NHẬP"
              required=""
              onChange={HandleUserNameChange}
            />
            {errors.userName && <p className="error">{errors.userName}</p>}
            <input
              type="password"
              placeholder="MẬT KHẨU"
              required=""
              onChange={HandlePasswordChange}
            />
            {errors.password && <p className="error">{errors.password}</p>}
            <input
              type="password"
              placeholder="XÁC NHẬN MẬT KHẨU"
              required=""
              onChange={HandleConfirmPasswordChange}
            />
            {errors.confirmPassword && <p className="error">{errors.confirmPassword}</p>}
          </div>
          <div className='col-md-6'>
            <input
              className='textregister'
              type="text"
              placeholder="SỐ ĐIỆN THOẠI"
              required=""
              onChange={HandlePhoneChange}
            />
            {errors.phone && <p className="error">{errors.phone}</p>}
            <input
              type="email"
              placeholder="EMAIL"
              required=""
              onChange={HandleEmailChange}
            />
            {errors.email && <p className="error">{errors.email}</p>}
          </div>
          <ul className="agileinfotickwthree">
            <li>
              <input
                style={{ display: 'none' }}
                type="checkbox"
                id="brand1"
                value=""
              />

              <label htmlFor="brand1"  style={{ display: 'none' }}>
                 
                <span></span>
              </label>
              <a href="#"></a>
            </li>
          </ul>
          <div className="aitssendbuttonw3ls">
            <input
              type="submit"
              value="Đăng Ký"
            />
            <div className="social w3layouts">
              <h5>Hoặc đăng ký bằng</h5>
              <div className="icons">
                <a href="#"><i className="fa-brands fa-facebook fa-xl" style={{ color: "#0967ae", }}></i></a>
                <a href="#"><i className="fa-solid fa-envelope fa-xl" style={{ color: "#0967ae", }}></i></a>
              </div>
            </div>
            <p>
              Đã có tài khoản? <span >→</span>{' '}
              <NavLink to='/login' className="w3_play_icon1" >
                {' '}
                Đăng Nhập
              </NavLink>
            </p>
            <div className="clear"></div>
          </div>
        </form>

      </div>
    </div>

  );
}

export default Register;
