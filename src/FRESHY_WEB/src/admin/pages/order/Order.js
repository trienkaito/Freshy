
import React, { useEffect, useState } from "react";
import { Form, Table } from "react-bootstrap";
import "./order.css";
import { useDispatch, useSelector } from "react-redux";
import {
    SearchProduct,
    SearchProductOrderOffline,
    getFullProducts,
    resetState,
} from "../../../redux/product/ProductAction";
import ModalOrder from "./ModalOrder";
import { SearchCustomer } from "../../../service/CustomerService";
import ModalOrderDiscount from "./ModalOrderDiscount";
import { getUnitById } from "../../../service/ProductService";
import { toast } from "react-toastify";

function Order() {
    const dispatch = useDispatch();
    const [show, setShow] = useState(false);
    const [dataOrder, setDataOrder] = useState("");
    const [listProductOrder, setListProductOrder] = useState([]);
    const listProduct = useSelector((state) => state.cart.products);
    const [searchTerm, setSearchTerm] = useState("");
    const [searchCustomer, setSearchCustomer] = useState("");
    const [infoCustomer, setInfoCustomer] = useState("");
    const [quantities, setQuantities] = useState([]);
    const [listCustomer, setListCustomer] = useState([]);
    const [showModalDisCount, setShowModalDisCount] = useState(false);
    const [isTypeOfPayment, setIsTypeOfPayment] = useState(false);
    const discount = useSelector((state) => state.cart.discount);
    const paid_success = useSelector((state) => state.cart.paid_success);
    const productunits = useSelector((state) => state.cart.productunits);
    const paidtype = useSelector((state) => state.cart.paid_type);
    useEffect(() => {
        if (paid_success) {
            setListProductOrder([]);
            setDataOrder([]);
            setSearchTerm("");
            handleClose();
            setInfoCustomer("");
            dispatch(resetState());
        }
    }, paid_success);
    useEffect(() => {
        dispatch(
            SearchProductOrderOffline("hjavjhfbvsdkjbàhjadjhfbbjdgbsdjkfbjksdb")
        );
    }, []);

    const handleInputChange = (event) => {
        dispatch(getFullProducts());
        setSearchTerm(event);
        if (event) {
            dispatch(SearchProductOrderOffline(event));
        } else {
            dispatch(
                SearchProductOrderOffline(
                    "hjavjhfbvsdkjbàhjadjhfbbjdgbsdjkfbjksdb"
                )
            );
        }
    };

    const handleInputCustomerChange = async (event) => {
        setSearchCustomer(event);
        if (event) {
            let res = await SearchCustomer(event);
            setListCustomer(res.data);
        } else {
            let res = await SearchCustomer(
                "jhfdgj43243325245345fsdjbfjhsdkfhjksdhkfhks"
            );
            setListCustomer(res.data.data);
        }
    };

    const handleAddCustomer = async (item) => {
        setInfoCustomer(item);
        setSearchCustomer("");
        let res = await SearchCustomer(
            "jhfdgj43243325245345fsdjbfjhsdkfhjksdhkfhks"
        );
        setListCustomer(res.data.data);
    };
    const handleClose = () => {
        setShow(false);
        setShowModalDisCount(false);
    };
    const handleShow = () => {
        setShow(true);
    };
    const handleShowModalDisCount = () => {
        setShowModalDisCount(true);
    };
    const handleAddItem = (item) => {
        console.log("item", item);
        setSearchTerm("");
        if (item.quantity == 0) {
            toast.error("Sản phẩm đã hết !!!");
        } else {
            if (listProductOrder.length == 0) {
                toast.success("Thêm sản phẩm vào giỏ hàng thành công !!!");

                setListProductOrder((prevList) => [
                    ...prevList,
                    { ...item, quantity: 1 },
                ]);
                setQuantities((prevQuantities) => [...prevQuantities, 1]);
            } else {
                if (
                    listProductOrder.some(
                        (product) =>
                            product.productName === item.productName &&
                            product.unitType === item.unitType &&
                            product.unitValue === item.unitValue
                    )
                ) {
                    toast.warn("Sản phẩm đã có trong giỏ hàng !!!");
                } else if (
                    listProductOrder.some(
                        (product) =>
                            product.productName === item.productName &&
                            product.unitType !== item.unitType
                    )
                ) {
                    toast.success("Thêm sản phẩm vào giỏ hàng thành công !!!");
                    setListProductOrder((prevList) => [
                        ...prevList,
                        { ...item, quantity: 1 },
                    ]);
                    setQuantities((prevQuantities) => [...prevQuantities, 1]);
                } else {
                    toast.success("Thêm sản phẩm vào giỏ hàng thành công !!!");

                    setListProductOrder((prevList) => [
                        ...prevList,
                        { ...item, quantity: 1 },
                    ]);
                    setQuantities((prevQuantities) => [...prevQuantities, 1]);
                }
            }
            dispatch(SearchProduct("sàiudufhjsdhfhsdfhksd", "", "", 1, 100));
        }
    };
    const handlePayment = () => {
        if (paidtype) {
            const orderDetails = listProductOrder.map((product, index) => ({
                ...product,
                quantity: quantities[index] || 1, // Số lượng mặc định là 1 nếu không có trong mảng quantities
            }));
            setDataOrder(orderDetails);

            setInfoCustomer(infoCustomer);
            handleShow();
        } else if (!listProductOrder.length > 0) {
            toast.error("Chưa có sản phẩm trong giỏ hàng !!!");
        } else {
            toast.error("Vui lòng chọn phương thức thanh toán !!!");
        } // Thực hiện các hành động khác liên quan đến thanh toán ở đây
    };

    const calculateTotalAmount = () => {
        // Tính tổng thành tiền của từng sản phẩm và lưu vào biến totalAmount
        const totalAmount = listProductOrder.reduce((total, product, index) => {
            const productTotal = product.price * (quantities[index] || 1); // Số lượng mặc định là 1 nếu không có trong mảng quantities
            return total + productTotal;
        }, 0);

        return totalAmount;
    };

    const handleDeleteItem = (index) => {
        setSearchTerm("");
        setListProductOrder((prevList) =>
            prevList.filter((product, i) => i !== index)
        );
        setQuantities((prevQuantities) =>
            prevQuantities.filter((quantity, i) => i !== index)
        );
    };
    const handleDecrease = (index) => {
        const updatedQuantities = [...quantities];
        if (updatedQuantities[index] > 1) {
            updatedQuantities[index] -= 1;
            setQuantities(updatedQuantities);
        }
    };
    const handleIncrease = async (index) => {
        const orderDetails = listProductOrder.map((product, index) => ({
            ...product,
            quantity: quantities[index] || 1, // Số lượng mặc định là 1 nếu không có trong mảng quantities
        }));
        let res = await getUnitById(orderDetails[index].unitID.value);
        console.log(">>>>", res.data.quantity);
        if (orderDetails[index].quantity < res.data.quantity) {
            const updatedQuantities = [...quantities];
            updatedQuantities[index] += 1;
            setQuantities(updatedQuantities);
        } else {
            toast.error("Sản phẩm đã hết !!!");
        }
    };
    const handleSale = () => {
        setIsTypeOfPayment(false);
        console.log(">>>", isTypeOfPayment);

        handleShowModalDisCount();
    };
    const handleChooseTypeOfPayment = () => {
        setIsTypeOfPayment(true);
        console.log(">>>", isTypeOfPayment);
        handleShowModalDisCount();
    };

    return (
        <div className="sideBar">
            <ModalOrderDiscount
                handleShow={handleShowModalDisCount}
                handleClose={handleClose}
                show={showModalDisCount}
                isTypeOfPayment={isTypeOfPayment}
            ></ModalOrderDiscount>
            <ModalOrder
                handleShow={handleShow}
                handleClose={handleClose}
                show={show}
                data={dataOrder}
                infoCustomer={infoCustomer}
                totalAmount={(
                    calculateTotalAmount() -
                    (calculateTotalAmount() * discount) / 100
                ).toLocaleString()}
                discount={discount}
            ></ModalOrder>
            <div className="contai">
                <div className="row">
                    <div className="search col-md-8 col-sm-8">
                        <Form className="d-flex search-form">
                            <i className="fa-solid fa-magnifying-glass search-icon"></i>
                            <Form.Control
                                value={searchTerm}
                                type="search"
                                placeholder="Nhập tên hoặc Mã sản phẩm cần tìm ..."
                                className="ser"
                                aria-label="Search"
                                onChange={(e) =>
                                    handleInputChange(e.target.value)
                                }
                            />
                        </Form>
                        <ul className="suggestions">
                            {listProduct &&
                                listProduct.map((item, index) => (
                                    <li
                                        key={index}
                                        onMouseDown={() => handleAddItem(item)}
                                    >
                                        {item.productName} - {item.unitValue}{" "}
                                        {item.unitType}
                                    </li>
                                ))}
                        </ul>

                        <div className="main-order">
                            <div className="table-responsive">
                                <Table striped bordered hover>
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Tên Sản Phẩm</th>
                                            <th>Đơn Vị</th>
                                            <th>Số Lượng</th>
                                            <th>Đơn Giá (vnđ)</th>
                                            <th>Thành Tiền</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {listProductOrder &&
                                            listProductOrder.map(
                                                (item, index) => (
                                                    <tr key={index}>
                                                        <td>{index + 1}</td>
                                                        <td>
                                                            {item.productName}
                                                        </td>
                                                        <td>
                                                            {item.unitValue}{" "}
                                                            {item.unitType}
                                                        </td>
                                                        <td>
                                                            <span
                                                                onClick={() =>
                                                                    handleDecrease(
                                                                        index
                                                                    )
                                                                }
                                                            >
                                                                -
                                                            </span>
                                                            {quantities[
                                                                index
                                                            ] || 1}
                                                            <span
                                                                onClick={() =>
                                                                    handleIncrease(
                                                                        index
                                                                    )
                                                                }
                                                            >
                                                                +
                                                            </span>
                                                        </td>
                                                        <td>{item.price}</td>
                                                        <td>
                                                            {quantities[index] *
                                                                item.price}
                                                        </td>
                                                        <th>
                                                            <i
                                                                className="fa-regular fa-trash-can fa-xl"
                                                                onClick={() =>
                                                                    handleDeleteItem(
                                                                        index
                                                                    )
                                                                }
                                                            ></i>
                                                        </th>
                                                    </tr>
                                                )
                                            )}
                                    </tbody>
                                </Table>
                            </div>
                        </div>
                    </div>
                    <div className="sidebar-icon col-md-4  col-sm-4">
                        <Form className="d-flex search-form">
                            {/* <i className="fa-solid fa-magnifying-glass search-icon"></i> */}

                            <Form.Control
                                value={searchCustomer}
                                type="search"
                                placeholder="Nhập tên hoặc sdt hoặc email khách hàng ..."
                                className="ser"
                                aria-label="Search"
                                onChange={(e) =>
                                    handleInputCustomerChange(e.target.value)
                                }
                            />
                        </Form>
                        <ul className="suggestions">
                            {listCustomer &&
                                listCustomer.map((item, index) => (
                                    <li
                                        key={index}
                                        onMouseDown={() =>
                                            handleAddCustomer(item)
                                        }
                                    >
                                        {item.name} - {item.phone}
                                    </li>
                                ))}
                        </ul>

                        <div className="pay-ing">
                            {infoCustomer ? (
                                <div>
                                    <h6>Khách Hàng : {infoCustomer.name}</h6>
                                    <h6>SDT : {infoCustomer.phone}</h6>
                                </div>
                            ) : (
                                <div>
                                    <h6>Khách Hàng : DEFAULT CUSTOMER</h6>
                                    <h6>SDT : 0000000000</h6>
                                </div>
                            )}

                            <h6>
                                Thành Tiền :{" "}
                                {calculateTotalAmount().toLocaleString()} VNĐ
                            </h6>
                            <h6>
                                Chiết khấu {discount ? `${discount}%` : "0%"} :{" "}
                                {discount
                                    ? `${(
                                          (calculateTotalAmount() * discount) /
                                          100
                                      ).toLocaleString()}đ`
                                    : "0đ"}
                            </h6>
                            <h6>
                                Tổng cộng :{" "}
                                {(
                                    calculateTotalAmount() -
                                    (calculateTotalAmount() * discount) / 100
                                ).toLocaleString()}{" "}
                                VNĐ
                            </h6>

                            <h6>Thuế và các loại phí khác : 0 VNĐ</h6>

                            <h6>
                                Phương thức thanh toán :{" "}
                                {paidtype ? paidtype : null}
                            </h6>
                        </div>
                        <div className="pay-submit">
                            <div className="combo-cacular">
                                <div className="row">
                                    <div className="col-md-6 combo-pay">
                                        <input
                                            className="pay-button"
                                            type="submit"
                                            value={"Chiết Khấu"}
                                            placeholder=""
                                            onClick={handleSale}
                                        />
                                    </div>
                                    <div className="col-md-6 combo-pay">
                                        <input
                                            className="pay-button ptth"
                                            type="submit"
                                            value={"Phương thức thanh toán"}
                                            onClick={handleChooseTypeOfPayment}
                                            placeholder=""
                                        />
                                    </div>
                                </div>
                            </div>
                            <div className="row combo-paying-button">
                                <div className="col-md-8 paying-button">
                                    <input
                                        className="pay-button"
                                        type="submit"
                                        value={"Thanh Toán"}
                                        onClick={handlePayment}
                                        placeholder=""
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Order;
