import React, { useEffect, useState } from "react";
import "../../../assets/cssWeb/style.css";
import { useParams } from "react-router-dom";
import {
    productDetail,
    addToCart,
    addProductLike,
    deleteProductLike,
} from "../../../service/ProductService";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { toast } from "react-toastify";
import { useDispatch, useSelector } from "react-redux";
function ProductDetail() {
    const customerId = useSelector(state => state.user.customer.id);

    const [activeTab, setActiveTab] = useState("description");
    const [selectedPrice, setSelectedPrice] = useState(null);
    const [selectedUnit, setSelectedUnit] = useState(null);
    const [currentImage, setCurrentImage] = useState(null);
    const [isLiked, setIsLiked] = useState(false);
    const { productId } = useParams();
    const [productDetails, setProductDetails] = useState(null);
    // Hàm để thay đổi tab hiện tại
    const handleTabClick = (tab) => {
        setActiveTab(tab);
    };
    const handleUnitClick = (unitOrImageSrc) => {
        if (typeof unitOrImageSrc === "string") {
            setCurrentImage(unitOrImageSrc);
        } else {
            // Existing logic for handling a unit object
            setSelectedPrice(unitOrImageSrc.sellPrice);
            setSelectedUnit(unitOrImageSrc);
            setCurrentImage(unitOrImageSrc.unitFeatureImage);
        }
    };

    // Assuming handleUnitClick is modified to optionally take a unit or an image src
    const handleSliderImageClick = (image) => {
        if (image.isPrimary) {
            setCurrentImage(image.src); // Directly set the main image if it's the primary image
        } else {
            // Find the unit that matches the clicked image (if necessary)
            const unit = productDetails.units.find(
                (unit) => unit.unitFeatureImage === image.src
            );
            if (unit) handleUnitClick(unit);
            else setCurrentImage(image.src); // Fallback to just setting the image if no unit matches
        }
    };

    // State để theo dõi số lượng
    const [quantity, setQuantity] = useState(1);

    // Hàm tăng số lượng
    const incrementQuantity = () => {
        setQuantity((prevQuantity) => prevQuantity + 1);
    };

    // Hàm giảm số lượng
    const decrementQuantity = () => {
        setQuantity((prevQuantity) =>
            prevQuantity > 1 ? prevQuantity - 1 : 1
        );
    };

    const settings = {
        infinite: true,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 3,
        // Consider adding additional settings such as adaptiveHeight if your images have different heights
        adaptiveHeight: true,
    };

    const handleAddToCart = async () => {

        if(!customerId){
            toast.error("Đăng nhập để thêm vào giỏ hàng!!!")
        }else if (selectedUnit && quantity) {
            
            console.log(customerId);
            try {
                const response = await addToCart(
                    customerId,
                    productId,
                    selectedUnit.unitId,
                    quantity
                );
                console.log(response);
                toast.success("Thêm vào giỏ hàng thành công !!!");
            } catch (error) {
                toast.error(error.message);

                // Xử lý lỗi, ví dụ: thông báo lỗi cho người dùng
            }
        } else {
            // Thông báo cho người dùng nếu chưa chọn đơn vị sản phẩm hoặc số lượng
            toast.error("Vui lòng chọn đơn vị sản phẩm và số lượng !!!");
        }
    };
    const toggleProductLike = async () => {
        try {
            if (isLiked) {
                // Assume removeProductLike is your service function to remove a like
                const response = await deleteProductLike(customerId, productId);
                console.log("Product like removed successfully:", response);
                setIsLiked(false); // Update the state to reflect the change
            } else {
                const response = await addProductLike(productId, customerId); // Replace "USER_ID" with actual user ID
                console.log("Product like added successfully:", response);
                setIsLiked(true); // Update the state to reflect the change
            }
            // Handle UI notification about success (if necessary)
        } catch (error) {
            console.error("Error toggling product like:", error);
            // Handle error notification (if necessary)
        }
    };

    useEffect(() => {
        const fetchProductDetails = async () => {
            try {
                const response = await productDetail(productId);
                console.log(">>>", response);
                if (response && response.data && response.data.data) {
                    const data = response.data.data;
                    const prices = data.units.map((unit) => unit.sellPrice);
                    const minPrice = Math.min(...prices);
                    const maxPrice = Math.max(...prices);
                    setCurrentImage(data.featureImage || "default_image_path");

                    // New: Prepare slider images, starting with the primary product image
                    const sliderImages = [
                        {
                            src: data.featureImage || "default_image_path",
                            isPrimary: true,
                        },
                        ...data.units.map((unit) => ({
                            src:
                                unit.unitFeatureImage ||
                                "default_thumbnail_path",
                            isPrimary: false,
                        })),
                    ];
                    setProductDetails({
                        ...data,
                        minPrice,
                        maxPrice,
                        sliderImages,
                    });
                    // Set the default image once the product details are loaded
                    // setCurrentImage(
                    //     data.featureImage || "đường_dẫn_hình_ảnh_mặc_định"
                    // );
                }
            } catch (error) {
                console.error("Error fetching product details:", error);
            }
        };

        fetchProductDetails();
    }, [productId]);

    return (
        <>
            {productDetails ? (
                <section className="product-details spad">
                    <div className="container">
                        <div className="row">
                            <div className="col-lg-6 col-md-6">
                                <div className="product__details__pic">
                                    <div className="product__details__pic__item">
                                        <img
                                            src={
                                                currentImage ||
                                                productDetails?.featureImage ||
                                                "default_image_path"
                                            }
                                            alt="Product"
                                        />
                                    </div>
                                    <div className="product__details__pic__slider">
                                        <Slider {...settings}>
                                            {productDetails?.sliderImages.map(
                                                (image, index) => (
                                                    <div key={index}>
                                                        <img
                                                            src={image.src}
                                                            alt={`Thumbnail ${index}`}
                                                            onClick={() =>
                                                                handleSliderImageClick(
                                                                    image
                                                                )
                                                            }
                                                            className={
                                                                image.isPrimary
                                                                    ? "primary-image"
                                                                    : ""
                                                            }
                                                        />
                                                    </div>
                                                )
                                            )}
                                        </Slider>
                                    </div>
                                </div>
                            </div>
                            <div className="col-lg-6 col-md-6">
                                <div className="product__details__text">
                                    <h3>{productDetails.name}</h3>
                                    <div className="product__details__rating">
                                        <i className="fa fa-star" />
                                        <i className="fa fa-star" />
                                        <i className="fa fa-star" />
                                        <i className="fa fa-star" />
                                        <i className="fa fa-star-half-o" />
                                        <span>(18 reviews)</span>
                                    </div>
                                    <div className="product__details__price">
                                        {selectedPrice
                                            ? `${selectedPrice}Đ`
                                            : `${productDetails.minPrice}Đ - ${productDetails.maxPrice}Đ`}
                                    </div>

                                    <p>{productDetails.description}</p>
                                    {productDetails && productDetails.units && (
                                        <div className="d-flex combo-unit">
                                            <div className="unit-product">
                                                <p>Đơn vị: </p>
                                            </div>
                                            <div className="d-flex item-unit">
                                                {productDetails.units.map(
                                                    (unit, index) => (
                                                        <button
                                                            key={index}
                                                            type="button"
                                                            className={`btn btn-custom ${
                                                                selectedUnit ===
                                                                unit
                                                                    ? "selected"
                                                                    : ""
                                                            }`}
                                                            onClick={() =>
                                                                handleUnitClick(
                                                                    unit
                                                                )
                                                            }
                                                        >
                                                            <img
                                                                src={
                                                                    unit.unitFeatureImage
                                                                }
                                                                alt="Product"
                                                            />
                                                            {unit.unitValue}{" "}
                                                            {unit.unitType}
                                                        </button>
                                                    )
                                                )}
                                            </div>
                                        </div>
                                    )}
                                    <div className="action-detail">
                                        <div className="product__details__quantity">
                                            <div className="quantity">
                                                <div className="pro-qty">
                                                    <button
                                                        onClick={
                                                            decrementQuantity
                                                        }
                                                        style={{
                                                            marginRight: "10px",
                                                            border: "none",
                                                        }}
                                                    >
                                                        -
                                                    </button>
                                                    <input
                                                        type="text"
                                                        value={quantity}
                                                        onChange={(e) =>
                                                            setQuantity(
                                                                Number(
                                                                    e.target
                                                                        .value
                                                                )
                                                            )
                                                        }
                                                    />
                                                    <button
                                                        onClick={
                                                            incrementQuantity
                                                        }
                                                        style={{
                                                            marginLeft: "10px",
                                                            border: "none",
                                                        }}
                                                    >
                                                        +
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="addToCart-Button">
                                            <a
                                                href="#"
                                                className="primary-btn"
                                                onClick={handleAddToCart}
                                            >
                                                ADD TO CART
                                            </a>
                                        </div>
                                        <div className="icon-heart">
                                            {/* Update the onClick handler and className based on isLiked */}
                                            {isLiked ? (
                                                <i
                                                    className="fa-solid fa-heart"
                                                    onClick={toggleProductLike}
                                                ></i>
                                            ) : (
                                                <i
                                                    className="fa-regular fa-heart"
                                                    onClick={toggleProductLike}
                                                ></i>
                                            )}
                                        </div>
                                    </div>
                                    <ul>
                                        <li>
                                            <b>Availability</b>{" "}
                                            <span>In Stock</span>
                                        </li>
                                        <li>
                                            <b>Share on</b>
                                            <div className="share">
                                                <a href="#">
                                                    <i className="fa fa-facebook" />
                                                </a>
                                                <a href="#">
                                                    <i className="fa fa-twitter" />
                                                </a>
                                                <a href="#">
                                                    <i className="fa fa-instagram" />
                                                </a>
                                                <a href="#">
                                                    <i className="fa fa-pinterest" />
                                                </a>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div className="col-lg-12">
                                <div className="product__details__tab">
                                    <ul className="nav nav-tabs" role="tablist">
                                        {/* Sử dụng hàm handleTabClick để thay đổi trạng thái */}
                                        <li
                                            className="nav-item"
                                            style={{ cursor: "pointer" }}
                                        >
                                            <a
                                                className={`nav-link ${
                                                    activeTab === "description"
                                                        ? "active"
                                                        : ""
                                                }`}
                                                onClick={() =>
                                                    handleTabClick(
                                                        "description"
                                                    )
                                                }
                                            >
                                                Description
                                            </a>
                                        </li>
                                        <li
                                            className="nav-item"
                                            style={{ cursor: "pointer" }}
                                        >
                                            <a
                                                className={`nav-link ${
                                                    activeTab === "information"
                                                        ? "active"
                                                        : ""
                                                }`}
                                                onClick={() =>
                                                    handleTabClick(
                                                        "information"
                                                    )
                                                }
                                            >
                                                Information
                                            </a>
                                        </li>
                                        <li
                                            className="nav-item"
                                            style={{ cursor: "pointer" }}
                                        >
                                            <a
                                                className={`nav-link ${
                                                    activeTab === "reviews"
                                                        ? "active"
                                                        : ""
                                                }`}
                                                onClick={() =>
                                                    handleTabClick("reviews")
                                                }
                                            >
                                                Reviews <span>(1)</span>
                                            </a>
                                        </li>
                                    </ul>
                                    {/* Hiển thị nội dung tương ứng dựa vào trạng thái activeTab */}
                                    <div className="tab-content">
                                        <div
                                            className={`tab-pane ${
                                                activeTab === "description"
                                                    ? "active"
                                                    : ""
                                            }`}
                                            id="tabs-1"
                                            role="tabpanel"
                                        >
                                            {productDetails.description}
                                        </div>
                                        <div
                                            className={`tab-pane ${
                                                activeTab === "information"
                                                    ? "active"
                                                    : ""
                                            }`}
                                            id="tabs-2"
                                            role="tabpanel"
                                        >
                                            CDF
                                            {/* Nội dung của Information */}
                                        </div>
                                        <div
                                            className={`tab-pane ${
                                                activeTab === "reviews"
                                                    ? "active"
                                                    : ""
                                            }`}
                                            id="tabs-3"
                                            role="tabpanel"
                                        >
                                            ABCD
                                            {/* Nội dung của Reviews */}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            ) : (
                <p>Loading product details...</p>
            )}
            <section className="related-product">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="section-title related__product__title">
                                <h2>Related Product</h2>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-3 col-md-4 col-sm-6">
                            <div className="product__item">
                                <div
                                    className="product__item__pic set-bg"
                                    data-setbg="img/product/product-1.jpg"
                                >
                                    <ul className="product__item__pic__hover">
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-heart" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-retweet" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-shopping-cart" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="product__item__text">
                                    <h6>
                                        <a href="#">Crab Pool Security</a>
                                    </h6>
                                    <h5>$30.00</h5>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-4 col-sm-6">
                            <div className="product__item">
                                <div
                                    className="product__item__pic set-bg"
                                    data-setbg="img/product/product-2.jpg"
                                >
                                    <ul className="product__item__pic__hover">
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-heart" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-retweet" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-shopping-cart" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="product__item__text">
                                    <h6>
                                        <a href="#">Crab Pool Security</a>
                                    </h6>
                                    <h5>$30.00</h5>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-4 col-sm-6">
                            <div className="product__item">
                                <div
                                    className="product__item__pic set-bg"
                                    data-setbg="img/product/product-3.jpg"
                                >
                                    <ul className="product__item__pic__hover">
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-heart" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-retweet" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-shopping-cart" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="product__item__text">
                                    <h6>
                                        <a href="#">Crab Pool Security</a>
                                    </h6>
                                    <h5>$30.00</h5>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-4 col-sm-6">
                            <div className="product__item">
                                <div
                                    className="product__item__pic set-bg"
                                    data-setbg="img/product/product-7.jpg"
                                >
                                    <ul className="product__item__pic__hover">
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-heart" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-retweet" />
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <i className="fa fa-shopping-cart" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="product__item__text">
                                    <h6>
                                        <a href="#">Crab Pool Security</a>
                                    </h6>
                                    <h5>$30.00</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
}

export default ProductDetail;
