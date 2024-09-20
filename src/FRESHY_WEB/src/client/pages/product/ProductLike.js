import React, { useEffect, useState } from "react";
import "../../../assets/cssWeb/style.css";
import { getAllProductLike } from "../../../service/ProductService";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
function ProductLike() {
    const [products, setProductLike] = useState([]);
    const customerId = useSelector(state => state.user.customer.id);

    useEffect(() => {
        // Gọi hàm getCartItems với ID cụ thể và xử lý dữ liệu trả về
        const fetchProductLikes = async () => {
            try {
                const response = await getAllProductLike(customerId);
                console.log(">>>", response.data); // Kiểm tra dữ liệu trả về
                // setCartItems(response.data); // Giả định rằng dữ liệu trả về nằm trong `data`
                if (response.data && Array.isArray(response.data.data)) {
                    setProductLike(response.data.data); // Cập nhật state với mảng
                } else {
                    console.error("Dữ liệu nhận được không phải là một mảng.");
                }
            } catch (error) {
                console.error("Error fetching cart items:", error);
            }
        };

        fetchProductLikes();
    }, []);
    return (
        <>
            <div className="latest-product__text">
                <div className="section-title">
                    <h2>Lượt thích</h2>
                </div>
                <div className="latest-product__slider owl-carousel">
                    <div className="row latest-prdouct__slider__item">
                        {Array.isArray(products) &&
                            products.map((product) => (
                                <a
                                    href="#"
                                    className="col-md-4 latest-product__item"
                                    key={product.id}
                                >
                                    <Link to={`/product-detail/${product.id}`}>
                                        <div className="latest-product__item__pic">
                                            <img
                                                src={product.featureImage}
                                                alt={product.name}
                                            />
                                        </div>
                                    </Link>

                                    <div className="latest-product__item__text">
                                        <Link
                                            to={`/product-detail/${product.id}`}
                                        >
                                            <h5>{product.name}</h5>
                                        </Link>

                                        <Link
                                            to={`/product-detail/${product.id}`}
                                        >
                                            <span>
                                                {new Intl.NumberFormat(
                                                    "vi-VN",
                                                    {
                                                        style: "currency",
                                                        currency: "VND",
                                                    }
                                                ).format(product.lowestPrice)}
                                            </span>
                                        </Link>
                                    </div>
                                </a>
                            ))}
                    </div>
                </div>
            </div>
        </>
    );
}

export default ProductLike;
