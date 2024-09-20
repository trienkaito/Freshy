import { useEffect, useState } from "react";
import "../../../assets/cssWeb/style.css";
import { fetchAllTypeOfProduct, searchProduct, fetchAllProduct } from "../../../service/ProductService";
import { Link } from "react-router-dom";
function Home() {
    const [activeCategory, setActiveCategory] = useState("*");
    const [categories, SetCategories] = useState([]);
    const handleCategoryClick = (category) => {
        setActiveCategory(category);
    };
    const [products, setProducts] = useState([]); // State cho sản phẩm

    useEffect(() => {
        const fetchData = async () => {
            try {
                let res = await fetchAllTypeOfProduct();
                let resProduct = await fetchAllProduct(1,100);
                setProducts(resProduct.data.data);
                SetCategories(res.data.data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData(); // Call the async function
    }, []);
    
    return (
        <>
            <div class="hero__item set-bg">
                <div class="hero__text">
                    <span>SẢN PHẨM HÔM NAY</span>
                    <h2>
                        Vegetable <br />
                        100% Organic
                    </h2>
                  
                    <a href="#" class="primary-btn">
                        Mua Sắm Ngay
                    </a>
                </div>
            </div>
            <section class="featured spad">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="section-title">
                                <h2>Sản Phẩm</h2>
                            </div>
                            <div className="featured__controls">
                                <ul>
                                    <li
                                        className={
                                            activeCategory === "*"
                                                ? "active"
                                                : ""
                                        }
                                        data-filter="*"
                                        onClick={() => handleCategoryClick("*")}
                                    >
                                
                                    </li>
                                 
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div className="row featured__filter">
                        {products && products.map((product, index) => (
                            <div key={index} className="col-lg-3 col-md-4 col-sm-6 mix">
                                <div className="featured__item">
                                    <div className="featured__item__pic">
                                    <Link
                                            to={`/product-detail/${product.productId}`}
                                        >
                                        <img src={product.featureImage || "đường_dẫn_hình_ảnh_mặc_định"} alt="Product" />
                                        </Link>
                                        <ul className="featured__item__pic__hover">
                                            <li><a href="#"><i className="fa fa-heart"></i></a></li>
                                            <li><a href="#"><i className="fa fa-retweet"></i></a></li>
                                            <li><a href="#"><i className="fa fa-shopping-cart"></i></a></li>
                                        </ul>
                                    </div>
                                    <div className="featured__item__text">
                                        <h6> <Link
                                            to={`/product-detail/${product.productId}`}
                                        >{product.name}</Link></h6>
                                        <h5>{product.price}</h5>
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </section>

            <div class="banner">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <div class="banner__pic">
                                <img src="img/banner/banner-1.jpg" alt="" />
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <div class="banner__pic">
                                <img src="img/banner/banner-2.jpg" alt="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* <section class="latest-product spad">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-4 col-md-6">
                            <div class="latest-product__text">
                                <h4>Sản phẩm mới nhất</h4>
                                <div class="latest-product__slider owl-carousel">
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6">
                            <div class="latest-product__text">
                                <h4>Sản phẩm tiêu biểu</h4>
                                <div class="latest-product__slider owl-carousel">
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6">
                            <div class="latest-product__text">
                                <h4>Sản phẩm được yêu thích</h4>
                                <div class="latest-product__slider owl-carousel">
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="latest-prdouct__slider__item">
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-1.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-2.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                        <a
                                            href="#"
                                            class="latest-product__item"
                                        >
                                            <div class="latest-product__item__pic">
                                                <img
                                                    src="img/latest-product/lp-3.jpg"
                                                    alt=""
                                                />
                                            </div>
                                            <div class="latest-product__item__text">
                                                <h6>Crab Pool Security</h6>
                                                <span>$30.00</span>
                                            </div>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section> */}
        </>
    );
}
export default Home;
