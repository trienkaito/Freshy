import NavBarWeb from "../Navbar/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { NavLink } from "react-router-dom";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { Icons } from "react-toastify";
import "../../../assets/cssWeb/style.css";
import Logo from "../../../assets/image/logo.png";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { handleRefreshRedux, handleLogoutRedux } from '../../../redux/user/UserAction';
function HeaderWeb() {
    const dispatch = useDispatch();
    
    const roles = useSelector(state => state.user.account.role);
    const name = useSelector(state => state.user.account.name);
    useEffect(() => {
        dispatch(handleRefreshRedux());
    }, [dispatch]); 

 
    const hasCustomerRole = roles && roles.some(role => role === "Customer");
    const HandleLogout = () => {
        dispatch(handleLogoutRedux());
    }

    return (
        <>
            <header class="header">
                <div class="header__top">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-6 col-md-6">
                                <div class="header__top__left">
                                    <ul>
                                        <li>
                                            <i class="fa fa-envelope"></i>{" "}
                                            xin chào@{name}
                                        </li>
                                        <li>
                                            Miễn phí ship cho tất cả đơn hàng trên 500.000vnđ
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6">
                                <div class="header__top__right">
                                    <div class="header__top__right__social">
                                        <a href="#">
                                        <i class="fa-brands fa-facebook"></i>
                                        </a>
                                        <a href="#">
                                        <i class="fa-brands fa-twitter"></i>
                                        </a>
                                        <a href="#">
                                        <i class="fa-brands fa-linkedin"></i>
                                        </a>
                                        <a href="#">
                                        <i class="fa-brands fa-pinterest"></i>
                                        </a>
                                    </div>{hasCustomerRole ? (
                                            <>
                                    <div class="header__top__right__language">
                                        <img src="img/language.png" alt="" />
                                        <div>{name}</div>  
                                        <span class="arrow_carrot-down"></span>
                                        <ul>
                                            {/* <li>
                                                <NavLink to="/profile"></NavLink>
                                            </li> */}
                                            
                                          <li>
                                             <NavLink to="/profileUser">Thông tin</NavLink></li> </ul></div></>
                                        ) : (
                                            
                                      
                                     <></>
                                        )}
                                    <div className="header__top__right__auth">
                                        {hasCustomerRole ? (
                                            <a onClick={()=> HandleLogout()}>
                                                <i className="fa fa-user"></i> Đăng xuất
                                            </a>
                                        ) : (
                                            <NavLink to="/login">
                                                <i className="fa fa-user"></i> Đăng nhập
                                            </NavLink>
                                        )}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="header__logo">
                                <img id="logo" src={Logo} alt="" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <nav class="header__menu">
                                <ul>
                                    <li class="active">
                                        <NavLink to="/">Trang chủ</NavLink>
                                    </li>
                                    <li>
                                        <NavLink to="/shop-grid">Sản phẩm</NavLink>
                                    </li>
                                    {/* <li>
                                        <NavLink to="/page">Pages</NavLink>
                                        <ul class="header__menu__dropdown">
                                            <li>
                                                <NavLink to="/shop-details">
                                                    Shop Details
                                                </NavLink>
                                            </li>
                                            <li>
                                                <NavLink to="/shoping-cart">
                                                    Shoping Cart
                                                </NavLink>
                                            </li>
                                            <li>
                                                <NavLink to="/checkout">
                                                    Check Out
                                                </NavLink>
                                            </li>
                                            <li>
                                                <NavLink to="/blog-details">
                                                    Blog Details
                                                </NavLink>
                                            </li>
                                        </ul>
                                    </li> */}
                                    <li>
                                        <NavLink to="/blog">Blog</NavLink>
                                    </li>
                                    <li>
                                        <NavLink to="/contact">Liên Hệ</NavLink>
                                    </li>
                                </ul>
                            </nav>
                        </div>
                        <div class="col-lg-3">
                            <div class="header__cart">
                                <ul>
                                    <li>
                                        <NavLink to="/productlike">
                                            <i class="fa fa-heart"></i>{" "}
                                            <span>1</span>
                                        </NavLink>
                                    </li>
                                    <li>
                                        <NavLink to="/cart">
                                            <i class="fa fa-shopping-bag"></i>{" "}
                                            <span>3</span>
                                        </NavLink>
                                    </li>
                                </ul>
                                {/* <div class="header__cart__price">
                                    item: <span>$150.00</span>
                                </div> */}
                            </div>
                        </div>
                    </div>
                    <div class="humberger__open">
                        <i class="fa fa-bars"></i>
                    </div>
                    {/* <div class="header__top__right__auth">
                        <NavLink to="/login">
                            <i class="fa fa-user"></i> Login
                        </NavLink>
                    </div> */}
                </div>
            </header>
        </>
    );
}
export default HeaderWeb;
