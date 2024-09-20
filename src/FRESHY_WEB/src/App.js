import "./App.css";
import { BrowserRouter } from "react-router-dom";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import HeaderAdmin from "./admin/layout/header/Header";
import Footer from "./admin/layout/footer/Footer";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import HeaderClient from "./client/layout/header/Header";
import Login from "./client/pages/login/Login";
import { useState, useEffect } from "react";
import NotFound from "./NotFoud";
import Searchbar from "./client/layout/searchbar/Searchbar";
import SidebarClient from "./client/layout/sidebar/Sidebar";
import ProductDetail from "./client/pages/product/ProductDetail";
import Contact from "./client/pages/contact/Contact";
import ProductCategory from "./client/pages/product/ProductCategory";
import Cart from "./client/pages/cart/Cart";
import Register from "./client/pages/login/Register";
import OrderAdmin from "./admin/pages/order/Order";
import Order from "./client/pages/order/Order";
import Product from "./admin/pages/product/Product";
import Home from "./client/pages/home/Home";
import LoginAdmin from "./admin/pages/login/Login";
import ForgotPassword from "./client/pages/login/ForgotPassword";
import SendEmail from "./client/pages/login/SendEmail";
import ProductLike from "./client/pages/product/ProductLike";
import ProfileUser from "./client/pages/profile/Profile";
import { handleRefreshRedux } from "./redux/user/UserAction";
import { useDispatch } from "react-redux";
import FooterClient from "./client/layout/footer/Footer"
import Employee from './admin/pages/employee/employee';
import OnlinePayment from './client/pages/online-payment/online-payment';
import OrderHistory from "./client/pages/profile/OrderHistory";
function App() {
    console.log("app", window.location.pathname);
    const dispatch = useDispatch();
    useEffect(() => {
        if (localStorage.getItem("token")) {
            dispatch(handleRefreshRedux());
        }
    });
    return (
        <div className="App">
            <BrowserRouter>
                <Routes>
                    <Route
                        path="/"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <Home />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />

                    <Route
                        path="/category"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <ProductCategory />
                                <FooterClient></FooterClient>
                            </>
                        }
                    ></Route>
                    <Route
                        path="/admin/order"
                        element={
                            <>
                                <HeaderAdmin></HeaderAdmin>
                                <OrderAdmin />
                            </>
                        }
                    />
                    {/* {/* <Route exact path='/' element={<Home />} /> * */}
                    <Route
                        path="/admin/product"
                        element={
                            <>
                                <HeaderAdmin></HeaderAdmin>
                                <Product />
                            </>
                        }
                    />
                    <Route
                        path="/product-detail/:productId"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <ProductDetail />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/cart"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <Cart />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/productlike"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <ProductLike />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/contact"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <Contact />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />

                    <Route
                        path="/login"
                        element={
                            <>
                                {" "}
                                <HeaderClient />
                                <Login />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/register"
                        element={
                            <>
                                {" "}
                                <HeaderClient />
                                <Register />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/order"
                        element={
                            <>
                                <HeaderClient />
                                <SidebarClient />
                                <Order />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                    <Route
                        path="/profileUser"
                        element={
                            <>
                                <HeaderClient />
                                <ProfileUser />
                                <FooterClient></FooterClient>
                            </>
                        }
                    />
                          <Route path="*" element={<><HeaderClient /><NotFound /><FooterClient></FooterClient></>} />
                    <Route path="/contact" element={<Contact />} />

                    <Route path="/login" element={<>
                    <HeaderClient></HeaderClient>
                    <Login />
                    </>} />
                    <Route path='/register' element={
                    <><HeaderClient></HeaderClient><Register/></>
                    } /> 
                    <Route path='/order' element={<Order/>} /> 
                    <Route path="/online-payment" element={ <>
                                <HeaderClient />
                                <SidebarClient /><OnlinePayment /></> }/>
                    <Route path="/admin/employee" element={<>
                    <HeaderAdmin></HeaderAdmin>
                    <Employee />
                    </>} />
                    <Route path="/orderhistory" element={ <>
                                <HeaderClient />
                                <SidebarClient /><OrderHistory /></>} />
                </Routes>
          

                <Footer></Footer>
            </BrowserRouter>
            <ToastContainer
                position="top-right"
                autoClose={500}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme="light"
            />
        </div>
    );
}

export default App;