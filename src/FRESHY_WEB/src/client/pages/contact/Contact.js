import "../../../assets/cssWeb/style.css";

function Contact() {
    return (
        <>
            <section className="contact spad">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-3 col-md-3 col-sm-6 text-center">
                            <div className="contact__widget">
                                <span className="icon_phone" />
                                <h4>Số Điện Thoại</h4>
                                <p>+01-3-8888-6868</p>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-3 col-sm-6 text-center">
                            <div className="contact__widget">
                                <span className="icon_pin_alt" />
                                <h4>Địa chỉ</h4>
                                <p>60-49 Road 11378 New York</p>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-3 col-sm-6 text-center">
                            <div className="contact__widget">
                                <span className="icon_clock_alt" />
                                <h4>Giờ Mở Cửa</h4>
                                <p>10:00 am to 23:00 pm</p>
                            </div>
                        </div>
                        <div className="col-lg-3 col-md-3 col-sm-6 text-center">
                            <div className="contact__widget">
                                <span className="icon_mail_alt" />
                                <h4>Email</h4>
                                <p>freshy@gmail.com</p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <>
                {/* Map Begin */}
                <div className="map">
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15342.69504288798!2d108.2436184256751!3d15.978404442719928!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x314210f2d038af0f%3A0x51c64b1130497f99!2zRlBUIENvbXBsZXggxJDDoCBO4bq1bmc!5e0!3m2!1svi!2s!4v1711593810807!5m2!1svi!2s"
                        height={500}
                        style={{ border: 0 }}
                        allowFullScreen=""
                        aria-hidden="false"
                        tabIndex={0}
                    />
                    <div className="map-inside">
                        <div className="inside-widget">
                            <h4>Việt Nam</h4>
                            <ul>
                                <li>Phone: +12-345-6789</li>
                                <li>
                                    Add: 01 Nam Kỳ Khởi Nghĩa, Hòa Hải, Ngũ Hành
                                    Sơn, Đà Nẵng
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                {/* Map End */}
                {/* Contact Form Begin */}
                <div className="contact-form spad">
                    <div className="container">
                        <div className="row">
                            <div className="col-lg-12">
                                <div className="contact__form__title">
                                    <h2>Leave Message</h2>
                                </div>
                            </div>
                        </div>
                        <form action="#">
                            <div className="row">
                                <div className="col-lg-6 col-md-6">
                                    <input
                                        type="text"
                                        placeholder="Tên của bạn    "
                                    />
                                </div>
                                <div className="col-lg-6 col-md-6">
                                    <input
                                        type="text"
                                        placeholder="Email của bạn"
                                    />
                                </div>
                                <div className="col-lg-12 text-center">
                                    <textarea
                                        placeholder="Tin nhắn của bạn"
                                        defaultValue={""}
                                    />
                                    <button type="submit" className="site-btn">
                                        Xác Nhận
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                {/* Contact Form End */}
            </>
        </>
    );
}

export default Contact;
