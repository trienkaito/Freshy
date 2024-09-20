import React, { useEffect, useState } from 'react';
import './orderhistory.css';
import { fetchAllCustomerOrder, fetchAllCustomerOrderItems } from '../../../service/OrderService';

import { useDispatch, useSelector } from 'react-redux';
const OrderHistory = () => {
    const customerId = useSelector(state => state.user.customer.id);
    const [orderCustomer, setOrderCustomer] = useState([]);
    const [orderItemsMap, setOrderItemsMap] = useState({});
    const [orderDetails, setOrderDetails] = useState({});
    const toggleDetails = (index, orderId) => {
        handleOrderItems(orderId);
        console.log("Toggled details for order index:", index);
        setOrderCustomer(prevOrders => prevOrders.map((order, i) => {
            if (i === index) {
                return { ...order, showDetails: !order.showDetails };
            } else {
                return order;
            }
        }));
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            [orderId]: !prevDetails[orderId]
        }));
    };
    function handleOrderItems(orderId) {
        const fetchData = async () => {
            try {
                let res = await fetchAllCustomerOrderItems(orderId);
                console.log("items data: ", res.data.data);
                setOrderItemsMap(prevOrderItems => ({
                    ...prevOrderItems,
                    [orderId]: res.data.data // Lưu trữ danh sách mặt hàng với orderId là key
                }));
            }
            catch (error) {
                console.error("Error fetching data:", error);
            }
        }
        fetchData();
    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                let res = await fetchAllCustomerOrder(1, 5, customerId);
                console.log("data: ", res.data.data);
                setOrderCustomer(res.data.data);
            }
            catch (error) {
                console.error("Error fetching data:", error);
            }
        }
        fetchData();
    }, []);

    return (
        <section className="container-fluid p-4">
            <div className="row">
                <div className="col-lg-12 col-md-12 col-12">
                    {/* Page header */}
                    <div className="border-bottom pb-3 mb-3 ">
                        <div className="mb-2 mb-lg-0">
                            {/* <h1 className="mb-0 h2 fw-bold">Lịch Sử Đặt Hàng </h1> */}
                            {/* Breadcrumb */}
                            {/* <nav aria-label="breadcrumb">
                                <ol className="breadcrumb">
                                    <li className="breadcrumb-item">
                                        <a href="admin-dashboard.html">Dashboard</a>
                                    </li>
                                    <li className="breadcrumb-item">
                                        <a href="#">Ecommerce </a>
                                    </li>
                                    <li className="breadcrumb-item active" aria-current="page">
                                        Order History
                                    </li>
                                </ol>
                            </nav> */}
                        </div>
                    </div>
                </div>
            </div>
            {/* row */}
            <div className="row">
                <div className="ccc col-xxl-10 col-12">
                    {/* card */}
                    <div className="card">
                        {/* card body*/}
                        <div className="card-body ">
                            <div className="mb-6 head-title">
                                <h4 className="mb-0">Đơn Hàng Của Bạn</h4>
                                <p>Kiểm tra trạng thái của các đơn đặt hàng gần đây, quản lý trả lại và khám phá các sản phẩm tương tự.</p>
                            </div>
                            {orderCustomer && orderCustomer.length > 0 ? orderCustomer.map((order, index) => (

                                <div className="mb-8" key={order.orderId}>
                                    {/* text */}
                                    <div className="title border-bottom mb-3 pb-3 d-lg-flex align-items-center justify-content-between ">
                                        <div className="d-flex align-items-center justify-content-between">
                                            <h5 className="mb-0">Đơn Hàng #{index}</h5>
                                            <span className="ms-2">{order.createdDate}</span>
                                        </div>
                                        <div className="d-flex align-items-center justify-content-between">
                                            {/* link */}
                                            <button className="btn btn-link" onClick={() => toggleDetails(index, order.orderId)}>
                                                {orderDetails[order.orderId] ? 'Ẩn bớt' : 'Xem chi tiết'}
                                            </button>
                                            <a href="#" className="ms-6">Xem Hóa Đơn</a>
                                        </div>
                                    </div>
                                    {/* row */}
                                    {orderDetails[order.orderId] && (
                                        <div>
                                            {orderItemsMap[order.orderId] && orderItemsMap[order.orderId].orderItems.map((item, itemIndex) => (
                                                <div key={order.orderId}>
                                                    <div className="title-content row justify-content-between align-items-center">
                                                        <div className="col-lg-8 col-12">
                                                            <div className="d-md-flex">
                                                                <div>
                                                                    <img src={item.featureImage} alt="" className="img-4by3-xl rounded" />
                                                                </div>
                                                                <div className="ms-md-4 mt-2 mt-lg-0">
                                                                  
                                                                    <h5 className="mb-1">{item.name}</h5>
                                                                  
                                                                    <span>Danh Mục: <span className="text-dark">{item.typeName}</span></span>
                                                                
                                                                    <div className="mt-3">
                                                                        <h4>{new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                            item.totalPrice
                                                        )}</h4>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        {/* Nút "Buy again" */}
                                                        <div className="col-lg-3 col-12 d-grid buy-again">
                                                            <button href="products.html" className="mb-2">Mua Lại</button>
                                                        </div>
                                                    </div>
                                                    <hr className="my-3" />
                                                </div>
                                            ))}
                                            {orderItemsMap[order.orderId] && (
                                                <div className="money">
                                                    <h5>Tổng Tiền: {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                            orderItemsMap[order.orderId].productsAmount 
                                                        )} </h5>
                                                </div>
                                                 )}
                                        </div>
                                    )}

                                </div>
                            )) : <p>no no</p>}

                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export default OrderHistory;
