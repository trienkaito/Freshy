import React, { useState, useEffect } from "react";
import "../../../assets/cssWeb/style.css";
import { getCartItems, deleteItemCart } from "../../../service/ProductService";
import {fetchAllCart} from "../../../service/OnlinePaymentService";
import {selectedCartItem} from "../../../redux/product/ProductAction"
import { NavLink } from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
function Cart() {
    const dispatch = useDispatch();
    const customerId = useSelector(state => state.user.customer.id);
    //
    const [cartItems, setCartItems] = useState([]);
    // State to manage the select-all checkbox
    const [selectAll, setSelectAll] = useState(false);
    const [totalPrice, setTotalPrice] = useState(0);
    //-------------------
    const [selectedItems, setSelectedItems] = useState([]);
    //------------------
    const updateQuantity = (cartItemId, amount) => {
        setCartItems((currentItems) =>
            currentItems.map((item) => {
                if (item.cartItemId === cartItemId) {
                    // Đảm bảo rằng số lượng không rơi vào giá trị âm
                    const updatedQuantity = Math.max(
                        1,
                        item.boughtQuantity + amount
                    );
                    return { ...item, boughtQuantity: updatedQuantity };
                }
                return item;
            })
        );
    };
    const handlePriceTotal = (price, quantity) => {
        return price * quantity
    }
    const incrementQuantity = (cartItemId) => {
        updateQuantity(cartItemId, 1);
        calculateTotalPrice(cartItems);
    };

    const decrementQuantity = (cartItemId) => {
        updateQuantity(cartItemId, -1);
        calculateTotalPrice(cartItems);
    };

    // Hàm xóa một item khỏi giỏ hàng
    const removeItem = async (cartItemId) => {
        try {
            await deleteItemCart(customerId, cartItemId);
            setCartItems((currentItems) =>
                currentItems.filter((item) => item.cartItemId !== cartItemId)
            );
        } catch (error) {
            console.error("Error removing item:", error);
        }
    };

    const toggleSelectAll = () => {
        const newSelectAll = !selectAll;
        setSelectAll(newSelectAll);

        const updatedItems = cartItems.map((item) => ({
            ...item,
            isSelected: newSelectAll,
        }));

        setCartItems(updatedItems);
        calculateTotalPrice(updatedItems);
        setSelectedItems(updatedItems);
    };

    const handleCheckboxChange = (cartItemId) => {
        const updatedItems = cartItems.map((item) => {
            if (item.cartItemId === cartItemId) {
                return { ...item, isSelected: !item.isSelected };
            }
            return item;
        });

        const areAllSelected = updatedItems.every((item) => item.isSelected);

        setSelectAll(areAllSelected);

        setCartItems(updatedItems);
        calculateTotalPrice(updatedItems);
        const selectedItems = updatedItems.filter((item) => item.isSelected);
        setSelectedItems(selectedItems);


    };
    const calculateTotalPrice = (items) => {
        const newTotal = items.reduce(
            (sum, item) => sum + (item.isSelected ? (item.productUnit.sellPrice*item.boughtQuantity) : 0),
            0
        );
        setTotalPrice(newTotal);
    };
    const handleSelectedCarts= () =>{
        dispatch(selectedCartItem(selectedItems));
    }
console.log("selected", selectedItems);
const fetchCartItems = async () => {
    try {
        console.log("customerID",customerId);
        let response = await fetchAllCart(
            customerId
        );
        console.log(">>> Cart Item", response); // Kiểm tra dữ liệu trả về
        setCartItems(response.data); // Giả định rằng dữ liệu trả về nằm trong `data`
        if (response.data && Array.isArray(response.data.data)) {
            setCartItems(response.data.data); // Cập nhật state với mảng
        } else {
            console.error("Dữ liệu nhận được không phải là một mảng.");
        }
    } catch (error) {
        console.error("Error fetching cart items:", error);
    }
};


useEffect(() => {
    if (customerId) {
        fetchCartItems(customerId);
    }
}, []);

    return (
        <>       
            <section className="shoping-cart spad">
                <div className="container centered-container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="shoping__cart__table">
                                <table>
                                    <thead>
                                        <tr>
                                            <th className="shoping__product">
                                                <div className="custom-checkbox">
                                                    <input
                                                        type="checkbox"
                                                        id="selectAllCheckbox"
                                                        checked={selectAll}
                                                        onChange={
                                                            toggleSelectAll
                                                        } // Attach the handler here
                                                    />
                                                    <label
                                                        htmlFor="selectAllCheckbox"
                                                        className="checkmark"
                                                    ></label>
                                                </div>
                                                Sản phẩm
                                            </th>
                                            <th>Phân loại hàng</th>
                                            <th>Đơn Giá</th>
                                            <th>Số lượng</th>
                                            <th>Số tiền</th>
                                            <th />
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {Array.isArray(cartItems) &&
                                            cartItems.map((item, index) => (
                                                <tr key={item.cartItemId}>
                                                    <td className="shoping__cart__item">
                                                        <div className="custom-checkbox">
                                                            <input
                                                                type="checkbox"
                                                                id={`checkbox-${item.cartItemId}`} // Unique ID for each checkbox
                                                                checked={
                                                                    item.isSelected ||
                                                                    false
                                                                } // Controlled by the item's isSelected property
                                                                onChange={() =>
                                                                    
                                                                    handleCheckboxChange(
                                                                        item.cartItemId
                                                                    )
                                                                } // Implement this handler
                                                            />
                                                            <label
                                                                htmlFor={`checkbox-${item.cartItemId}`}
                                                                className="checkmark"
                                                            ></label>
                                                        </div>
                                                        <img
                                                            src={
                                                                item.productUnit
                                                                    .unitFeatureImage
                                                            }
                                                            alt={
                                                                item.productName
                                                            }
                                                            style={{
                                                                width: "50px",
                                                                height: "50px",
                                                                marginRight:
                                                                    "10px",
                                                            }}
                                                        />
                                                        <h5>
                                                            {item.productName}
                                                        </h5>
                                                    </td>
                                                    <td className="shoping__cart_unit">
                                                        {`${item.productUnit.value} ${item.productUnit.type} `}
                                                    </td>
                                                    <td className="shoping__cart__price">
                                                        {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                            item.productUnit
                                                                .sellPrice
                                                        )}
                                                    </td>
                                                    <td className="shoping__cart__quantity">
                                                        <div className="quantity">
                                                            <div className="pro-qty">
                                                                <button
                                                                    onClick={() =>
                                                                        decrementQuantity(
                                                                            item.cartItemId
                                                                        )
                                                                    }
                                                                    style={{
                                                                        marginRight:
                                                                            "10px",
                                                                        border: "none",
                                                                        backgroundColor:
                                                                            "#F5F5F5",
                                                                    }}
                                                                >
                                                                    -
                                                                </button>
                                                                <input
                                                                    type="text"
                                                                    value={
                                                                        item.boughtQuantity
                                                                    }
                                                                    readOnly
                                                                />
                                                                <button
                                                                    onClick={() =>
                                                                        incrementQuantity(
                                                                            item.cartItemId
                                                                        )
                                                                        
                                                                    }
                                                                    style={{
                                                                        marginLeft:
                                                                            "10px",
                                                                        border: "none",
                                                                        backgroundColor:
                                                                            "#F5F5F5",
                                                                    }}
                                                                >
                                                                    +
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td className="shoping__cart__total">
                                                        {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                            handlePriceTotal(Number(item.productUnit.sellPrice),Number(item.boughtQuantity))
                                                        )}
                                                    </td>
                                                    <td className="shoping__cart__item__close">
                                                        <span
                                                            className="icon_close"
                                                            onClick={() =>
                                                                removeItem(
                                                                    item.cartItemId
                                                                )
                                                            }
                                                        >
                                                            X
                                                        </span>
                                                    </td>
                                                </tr>
                                            ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="shoping__cart__btns">
                                <NavLink to="/" className="primary-btn cart-btn">
                                    QUAY LẠI MUA SẮM
                                </NavLink>
                                {/* <a
                                    href="#"
                                    className="primary-btn cart-btn cart-btn-right"
                                >
                                    <span className="icon_loading" />
                                    Upadate Cart
                                </a> */}
                            </div>
                        </div>

                        <div className="col-lg-6">
                            <div className="shoping__checkout">
                                <h5>Tổng giỏ hàng</h5>
                                <ul>
                                    {/* <li>
                                        Subtotal <span>$454.98</span>
                                    </li> */}
                                    <li>
                                        Tổng thanh toán:{" "}
                                        <span>
                                            {new Intl.NumberFormat("vi-VN", {
                                                style: "currency",
                                                currency: "VND",
                                            }).format(totalPrice)}
                                        </span>
                                    </li>
                                </ul>
                                <NavLink to="/online-payment" onClick={handleSelectedCarts} className="primary-btn" >
                                    PROCEED TO CHECKOUT
                                </NavLink>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
}

export default Cart;
