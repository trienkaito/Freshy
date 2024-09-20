import { useEffect, useState } from "react";
import "../../../assets/cssWeb/style.css";
import {
    searchProduct,
    addProductLike,
    deleteProductLike,
} from "../../../service/ProductService";
import { useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

function ProductCategory() {
    const customerId = useSelector(state => state.user.customer.id);

    const [products, setProducts] = useState([]);
    const [pageSize, setPageSize] = useState(4); 
    const namecate = useSelector((state) => state.cart.NAMECATEGORY);
    console.log(">>>", namecate);

    useEffect(() => {
        console.log(">>>", namecate);
       
        const fetchProductsByCategory = async (namecate) => {
            try {
                const resProduct = await searchProduct(
                    "",
                    "",
                    namecate,
                    1,
                    pageSize
                );
                setProducts(resProduct.data.data);
            } catch (error) {
                console.error("Error fetching products:", error);
            }
        };

        fetchProductsByCategory(namecate);
    }, [namecate, pageSize]); 

    const handleLoadMore = async () => {
        const newPageSize = pageSize + 4; 
        setPageSize(newPageSize);
    };
    const [likedProducts, setLikedProducts] = useState({}); 

    const toggleProductLike = async (productId) => {
        const isProductLiked = likedProducts[productId];
        try {
            if (isProductLiked) {
                const response = await deleteProductLike(
                    customerId,
                    productId
                );
                console.log("Product like removed successfully:", response);
               
                setLikedProducts((prev) => ({ ...prev, [productId]: false }));
            } else {
                const response = await addProductLike(
                    productId,
                    customerId
                );
                console.log("Product like added successfully:", response);
              
                setLikedProducts((prev) => ({ ...prev, [productId]: true }));
            }
        } catch (error) {
            console.error("Error toggling product like:", error);
        }
    };

    return (
        <>
            <section className="featuredd">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="section-title">
                                <h2>{namecate}</h2>
                            </div>
                        </div>
                    </div>
                    <div className="row featured__filter">
                        {products && products.map((product, index) => (
                            <div
                                key={index}
                                className="col-lg-3 col-md-4 col-sm-6 mix"
                            >
                                <div className="featured__item">
                                    <div className="featured__item__pic">
                                        <Link
                                            to={`/product-detail/${product.productId}`}
                                        >
                                            <img
                                                src={
                                                    product.featureImage ||
                                                    "đường_dẫn_hình_ảnh_mặc_định"
                                                }
                                                alt="Product"
                                            />
                                        </Link>

                                        <ul className="featured__item__pic__hover">
                                            <li
                                                onClick={() =>
                                                    toggleProductLike(
                                                        product.productId
                                                    )
                                                }
                                            >
                                                <i
                                                    className={`fa fa-heart ${
                                                        likedProducts[
                                                            product.productId
                                                        ]
                                                            ? "liked"
                                                            : "unliked"
                                                    }`}
                                                ></i>
                                            </li>
                                            {/* <li>
                                                <a href="#">
                                                    <i className="fa fa-retweet"></i>
                                                </a>
                                            </li> */}
                                            {/* <li>
                                                <a href="#">
                                                    <i className="fa fa-shopping-cart"></i>
                                                </a>
                                            </li> */}
                                        </ul>
                                    </div>
                                    <div className="featured__item__text">
                                        <h6>
                                            <Link
                                                to={`/product-detail/${product.productId}`}
                                            >
                                                {product.name}
                                            </Link>
                                        </h6>
                                        <h5>{product.price}</h5>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                    {products &&
                        products.length !== 0 &&
                        products.length % pageSize === 0 && (
                            <div className="bt-searchh">
                                <button
                                    type="button"
                                    className="btn btn-success custom-btn"
                                    onClick={handleLoadMore}
                                >
                                    Xem Thêm
                                </button>
                            </div>
                        )}
                </div>
            </section>
        </>
    );
}

export default ProductCategory;
