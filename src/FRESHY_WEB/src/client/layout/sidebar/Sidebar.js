import React, { useEffect, useState } from "react";
import "../../../assets/cssWeb/style.css";
import { useDispatch, useSelector } from "react-redux";
import { fetchAllTypeOfProduct } from "../../../service/ProductService";
import { getNameCategory } from "../../../redux/product/ProductAction";
import { useNavigate } from "react-router-dom";

function SidebarClient() {
    const [showCategories, setShowCategories] = useState(false);
    const dispatch = useDispatch();
    const [categories, SetCategories] = useState([]);
    const toggleCategories = (shouldClose) => {
        if (shouldClose === true) {
            setShowCategories(false);
        } else {
            setShowCategories(!showCategories);
        }
    };

    const navigate = useNavigate();

    const hanldeLoading = () => {
        navigate("/category");
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                let res = await fetchAllTypeOfProduct();
                SetCategories(res.data.data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData(); // Call the async function
    }, []);

    // Define the list of categories

    return (
        <>
            <section className="hero">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-3">
                            <div className="hero__categories">
                                <div
                                    className="hero__categories__all"
                                    onClick={toggleCategories}
                                >
                                    <i className="fa fa-bars"></i>
                                    <span>Danh mục sản phẩm</span>
                                </div>
                                <ul
                                    className={`categories-list ${
                                        showCategories ? "show" : ""
                                    }`}
                                >
                                    {categories &&
                                        categories.map((item, index) => (
                                            <li key={index}>
                                                <a
                                                    onClick={() => {
                                                        dispatch(
                                                            getNameCategory(
                                                                item.name
                                                            )
                                                        );
                                                        toggleCategories(true);
                                                        hanldeLoading();
                                                    }} // Đóng danh sách khi danh mục được chọn
                                                >
                                                    {item.name}
                                                </a>
                                            </li>
                                        ))}
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-9">
                            <div class="hero__search">
                                <div class="hero__search__form">
                                    <form action="#">
                                        <input
                                              className="ser"
                                            type="text"
                                            placeholder="Bạn cần gì hôm nay?"
                                        />
                                        <button type="submit" class="site-btn">
                                            Tìm kiếm
                                        </button>
                                    </form>
                                </div>
                                <div class="hero__search__phone">
                                    <div class="hero__search__phone__icon">
                                        <i class="fa fa-phone"></i>
                                    </div>
                                    <div class="hero__search__phone__text">
                                        <h5>123 456 789</h5>
                                        <span>Hỗ trợ 24/7</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
}

export default SidebarClient;
