// Trong component ModalOrderDiscount

import React, { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { useDispatch } from "react-redux";
import {
    setDiscountRedux,
    setPaidType,
} from "../../../redux/product/ProductAction";
import { toast } from "react-toastify";
import "./modalorder.css";

function ModalOrderDiscount(props) {
    const dispatch = useDispatch();

    const { handleShow, handleClose, show, data, isTypeOfPayment } = props;
    const [discount, setDiscount] = useState(0);
    const [typePayment, setTypePayment] = useState("");
    const [selectedPayment, setSelectedPayment] = useState(""); // State lưu trạng thái hình ảnh được chọn

    const handleApply = () => {
        dispatch(setDiscountRedux(discount));
        handleClose();
    };

    const handleOkType = () => {
        if (typePayment) {
            dispatch(setPaidType(typePayment));
            handleClose();
        } else {
            toast.error("Mời bạn chọn phương thức thanh toán !!!");
        }
    };

    const handleSetType = (type) => {
        setTypePayment(type);
    };

    const handleSelectPayment = (type) => {
        setSelectedPayment(type); // Cập nhật state khi hình ảnh được chọn
        setTypePayment(type); // Cập nhật phương thức thanh toán
    };

    const handleDiscountChange = (e) => {
        let value = e.target.value;
        if (value === "") {
            setDiscount(0);
        } else if (
            !isNaN(parseInt(value)) &&
            parseInt(value) >= 0 &&
            parseInt(value) <= 100
        ) {
            setDiscount(parseInt(value));
        }
    };

    return (
        <>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>
                        {isTypeOfPayment
                            ? "Phương thức thanh toán"
                            : "Chiết khấu đơn hàng"}
                    </Modal.Title>
                </Modal.Header>
                {!isTypeOfPayment ? (
                    <Modal.Body>
                        <label htmlFor="discount">Chiết khấu thường (%) </label>
                        <input
                            type="number"
                            id="discount"
                            name="discount"
                            min="0"
                            max="100"
                            step="1"
                            value={discount}
                            onChange={handleDiscountChange}
                        />
                    </Modal.Body>
                ) : (
                    <Modal.Body>
                        <div className="choice-pay">
                            <h6>Tiền mặt</h6>
                            <div
                                className={`choice-hover ${
selectedPayment === "CASH" ? "selected" : ""
                                }`}
                                onClick={() => handleSelectPayment("CASH")}
                            >
                                <img
                                    src="https://cdn-icons-png.flaticon.com/512/2489/2489756.png"
                                    alt="Cash"
                                />
                            </div>
                            <h6>Chuyển khoản</h6>
                            <div
                                className={`choice-hover ${
                                    selectedPayment === "BANK_TRANSFER"
                                        ? "selected"
                                        : ""
                                }`}
                                onClick={() =>
                                    handleSelectPayment("BANK_TRANSFER")
                                }
                            >
                                <img
                                    src="https://cdn-icons-png.flaticon.com/512/6963/6963703.png"
                                    alt="Bank Transfer"
                                />
                            </div>
                        </div>
                    </Modal.Body>
                )}
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Thoát
                    </Button>
                    {isTypeOfPayment ? (
                        <Button variant="primary" onClick={handleOkType}>
                            Áp dụng
                        </Button>
                    ) : (
                        <Button variant="primary" onClick={handleApply}>
                            Áp dụng
                        </Button>
                    )}
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default ModalOrderDiscount;