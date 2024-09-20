import React from 'react';
import "./footer.css"
const Footer = () => {
    return (
        <footer className="footer-59391">
            <div className="border-bottom pb-5 mb-4">
                <div className="container">
                    <div className="row align-items-center">
                        <div className="col-lg-3">
                            <form action="#" className="subscribe mb-4 mb-lg-0">
                                {/* <div className="form-group">
                                    <input type="email" className="form-control" placeholder="Enter your email" />
                                    <button><span className="icon-keyboard_backspace"></span></button>
                                </div> */}
                            </form>
                        </div>
                        <div className="col-lg-6 text-lg-center">
                            <ul className="list-unstyled nav-links nav-left mb-4 mb-lg-0">
                                <li><a href="#">Đặc trưng</a></li>
                                <li><a href="#">Blog</a></li>
                                <li><a href="#">Định giá</a></li>
                                <li><a href="#">Dịch vụ</a></li>
                            </ul>
                        </div>
                        <div className="col-lg-3">
                            {/* <ul className="list-unstyled nav-links social nav-right text-lg-right">
                                <li><a href="#"><span className="icon-twitter"></span></a></li>
                                <li><a href="#"><span className="icon-instagram"></span></a></li>
                                <li><a href="#"><span className="icon-facebook"></span></a></li>
                                <li><a href="#"><span className="icon-pinterest"></span></a></li>
                            </ul> */}
                        </div>
                    </div>
                </div>
            </div>

            <div className="container">
                <div className="row align-items-center">
                    <div className="col-lg-4 text-lg-center site-logo order-1 order-lg-2 mb-3 mb-lg-0">
                        <a href="#" className="m-0 p-0">FRESHY</a>
                    </div>
                    <div className="col-lg-4 order-2 order-lg-1 mb-3 mb-lg-0">
                        <ul className="list-unstyled nav-links m-0 nav-left">
                            <li><a href="#">Điều kiện</a></li>
                            <li><a href="#">Về chúng tôi</a></li>
                            <li><a href="#">Riêng tư</a></li>
                            <li><a href="#">Liên hệ</a></li>
                        </ul>
                    </div>
                    <div className="col-lg-4 text-lg-right order-3 order-lg-3">
                        <p className="m-0 text-muted"><small>&copy; FRESHY ƯU TIÊN SỰ LỰA CHỌN CHO BẠN</small></p>
                    </div>
                </div>
            </div>
        </footer>
    );
}

export default Footer;
