import { NavLink } from "react-router-dom";
import "./profile.css";
import { useDispatch, useSelector } from 'react-redux';
export default function Profile() {
    const customer = useSelector(state => state.user.customer);
    return (
        <section className="section about-section gray-bg" id="about">
            <div className="container">
                <div className="row align-items-center flex-row-reverse">
                    <div className="col-lg-6">
                        <div className="about-text go-to">
                            <h3 className="dark-color">Thông tin của bạn</h3>
                            <h6 className="theme-color lead">
                                Tên : {customer.name}
                            </h6>
                            <p>
                                {/* I <mark>design and develop</mark> services for
                                customers of all sizes, specializing in creating
                                stylish, modern websites, web services, and
                                online stores. My passion is to design digital
                                user experiences through the bold interface and
                                meaningful interactions. */}
                            </p>
                            <div className="row about-list">
                                <div className="col-md-6">
                                    <div className="media">
                                        <label>Ngày sinh</label>
                                        <p>4th April 1998</p>
                                    </div>
                                    <div className="media">
                                        <label>Tuổi</label>
                                        <p>22 Yr</p>
                                    </div>
                                    <div className="media">
                                        <label>Quốc tịch</label>
                                        <p>Viet Nam</p>
                                    </div>
                                    <div className="media">
                                        <label>Địa chỉ</label>
                                        <p>California, USA</p>
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <div className="media">
                                        <label>E-mail</label>
                                        <p>{customer.email}</p>
                                    </div>
                                    <div className="media">
                                        <label>SĐT</label>
                                        <p>{customer.phone}</p>
                                    </div>
                                    {/* <div className="media">
                                        <label>Skype</label>
                                        <p>skype.0404</p>
                                    </div>
                                    <div className="media">
                                        <label>Freelance</label>
                                        <p>Available</p>
                                    </div> */}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <div className="about-avatar">
                            <img className="img-profile"
                                src={customer.avatar}
                                title=""
                                alt=""
                            />
                        </div>
                    </div>
                </div>
                <div className="counter">
                    <div className="row">
                        <div className="col-6 col-lg-3">
                            <div className="count-data text-center">
                                <h6
                                    className="count h2"
                                    data-to="500"
                                    data-speed="500"
                                >
                                    500
                                </h6>
                                <NavLink to='/orderHistory'>
                                <p className="m-0px font-w-600">
                                    Lịch sử đơn hàng
                                </p></NavLink>
                            </div>
                        </div>
                        <div className="col-6 col-lg-3">
                            <div className="count-data text-center">
                                <h6
                                    className="count h2"
                                    data-to="150"
                                    data-speed="150"
                                >
                                    150
                                </h6>
                                <p className="m-0px font-w-600">
                                    Xếp hạng thành viên
                                </p>
                            </div>
                        </div>
                        <div className="col-6 col-lg-3">
                            <div className="count-data text-center">
                                <h6
                                    className="count h2"
                                    data-to="850"
                                    data-speed="850"
                                >
                                    850
                                </h6>
                                <p className="m-0px font-w-600">
                                    Thông tin chi tiết
                                </p>
                            </div>
                        </div>
                        <div className="col-6 col-lg-3">
                            <div className="count-data text-center">
                                <h6
                                    className="count h2"
                                    data-to="190"
                                    data-speed="190"
                                >
                                    190
                                </h6>
                                <p className="m-0px font-w-600">
                                    Xem thêm
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}
