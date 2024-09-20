import React, { useState, useEffect } from 'react';
import ReactPaginate from 'react-paginate';
import { debounce } from "lodash";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import _ from "lodash"
import Table from 'react-bootstrap/Table';
import { format } from 'date-fns';
import './online-payment.css'; // Đảm bảo rằng bạn có một file CSS để tùy chỉnh giao diện
import { fetchCheckAndGetVoucher, fetchAllCart, fetchAddOrder,fetchAllShipping } from '../../../service/OnlinePaymentService';
import {getOrderAddressByCustomerId} from '../../../service/CustomerService'
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { parsePath } from 'history';
const OnlinePayment = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const customerId = useSelector(state => state.user.customer.id);

  const customer = useSelector(state => state.user.account);
  const [discount, setDiscount] = useState(0); // Sử dụng useState để lưu trữ giá trị giảm giá
  const [error, setError] = useState('');
  const [vouchercode, setVoucherCode] = useState('');
  const [validvouchercode, setValidVoucherCode] = useState('');
  const cartData = useSelector(state => state.cart.selectedCarts);
  
  const [shippingList, setShippingList] = useState([]);
  const [shippingId, setShippingId] = useState('');

  const handleSelectChange = (event) => {
    const selectedId = event.target.value;
    setShippingId(selectedId);
  };
  console.log("data",cartData);

  const productsPrice = cartData.reduce((total, item) => total + item.totalPrice, 0);
  console.log("p",productsPrice)
  const shippingFee = 10000;
  const totalDiscount = (discount * productsPrice);
  const handleCheckVoucher = async () => {
    console.log(vouchercode);
    const response = await fetchCheckAndGetVoucher(vouchercode);
    console.log("hi:", response);

    if (response && response.data) {

      let discountValue = response.data;
      setDiscount(discountValue);
      setError('');
      setValidVoucherCode(response.data);
    }
    else {
      setError('Mã voucher không hợp lệ.');
    }
  };
  const getAllShipping = async () => {
    let res = await fetchAllShipping();
    console.log("shipping",res);
    setShippingList(res.data.data);
  }
  const getOrderAddress = async (customerId) => {
    let res = await getOrderAddressByCustomerId(customerId);
    console.log("liAddress", res);
  }

  useEffect(() =>{
    getAllShipping();
    getOrderAddress(customerId);
  },[])
  console.log("shipid", shippingId);
  function handlePlaceOrder() {
    // Tạo đối tượng dữ liệu để gửi đi
    const orderItems = cartData.map(item => ({
      productId: item.productId,
      unitId: item.productUnitId,
      boughtQuantity: item.boughtQuantity
    }));

    var requestData = {
      customerId: customerId,
      orderAddress: "Quang tri",
      orderItems: orderItems,
      paymentType: "COD",
      shippingId: shippingId,
      voucherCode: {
        value: validvouchercode
      }
    };
    console.log(requestData);
    try {
       let res = fetchAddOrder(requestData);
    console.log("sssssssssssssssssss",res);
    toast.success("Đặt Hàng Thành Công!!!")
    navigate("/");
    } catch (error) {
    toast.error(error);  
    }
  }
  return (
    <div className='container'>
    <div className="card">
      <div className="row">
        <div className="col-md-8 cart">
          <div className="title">
            <div className="row">
              <div className="col"><h4><b>THANH TOÁN</b></h4></div>
              <div className="col align-self-center text-right text-muted"></div>
            </div>
          </div>
          {/* Your existing rows */}
          {/* For brevity, I'm omitting the existing row components */}
          {/* Please ensure to add your rows here */}
          <div className='list-order-a'>
            {cartData && cartData.length > 0 ? (
              cartData.map(item => (
                <div key={item.cartItemId} className="row border-top border-bottom">
                  <div className="row main align-items-center">
                    <div className="col-2"><img className="img-fluid" src={item.productUnit.unitFeatureImage} alt="{item.productName}" /></div>
                    <div className="col">
                      <div className="row text-muted">{item.productName}</div>
                      <div className="row">{item.productUnit.type}</div>
                    </div>
                    <div className="col">
                      <a className='a-order' href="#">-</a><a href="#" className="border">{item.boughtQuantity}</a><a className='a-order' href="#">+</a>
                    </div>
                    <div className="col"> {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                          item.totalPrice
                                                        )}<span className="close"></span></div>
                  </div>
                </div>
              ))
            ) : (
              <div className="text-muted">Không có mục nào trong giỏ hàng.</div>
            )}

          </div>
        </div>
        <div className="col-md-4 summary">
          <div><h5><b>Thanh toán</b></h5></div>
          <hr />
          <form>
            <div>
              <label for="phone">Tên khách hàng:</label>
              <input type="text" id="customer-name" value={customer.name} style={{ pointerEvents: 'none', margin: '4px 0', width: '100%' }} readonly />
            </div>
            <div>
              <label for="address">Chọn Đơn Vị Vận Chuyển:</label>
              <select id="address" className="text-muted" style={{ margin: '4px 0' }} onChange={handleSelectChange} >
                {shippingList && 
              shippingList.map(item => (
                    <option key={item.id} value={item.id}>{item.name}</option>
              )
                ) }
              </select>
            </div>
            <div>
              <label>Voucher</label>
              <div className="input-group">
                <input id="code" className="form-control" type="text" placeholder="Nhập mã voucher"
                  style={{ margin: '4px 0' }} value={vouchercode}
                  onChange={(event) => setVoucherCode(event.target.value)}
                />
                <div className="input-group-append" style={{ margin: '4px 0' }}>
                  <button className="btn btn-outline-secondary" type="button" onClick={handleCheckVoucher}>
                    <i className="fas fa-check"></i>
                  </button>
                </div>
              </div>
              {error && <div className="text-danger">{error}</div>}
            </div>

          </form>
          <div className="row" style={{ borderTop: '1px solid rgba(0,0,0,.1)', padding: '4px 0' }}>
            <div className="col">Sản phẩm</div>
            <div className="col text-right"> {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                           productsPrice
                                                        )}</div>
          </div>
          <div className="row">
            <div className="col">Phí giao hàng</div>
            <div className="col text-right"> {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                          shippingFee
                                                        )} </div>
          </div>
          <div className="row" style={{ padding: '4px 0' }}>
            <div className="col" >Giảm giá</div>
            <div className="col text-right"> {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                          totalDiscount
                                                        )}</div>
          </div>
          <div className="row" style={{ borderTop: '1px solid rgba(0,0,0,.1)', padding: '2vh 0', margin: '4px 0' }}>
            <div className="col">Tổng tiền</div>
            <div className="col text-right"> {new Intl.NumberFormat(
                                                            "vi-VN",
                                                            {
                                                                style: "currency",
                                                                currency: "VND",
                                                            }
                                                        ).format(
                                                          productsPrice + shippingFee - totalDiscount
                                                        )} </div>
          </div>
          <button className="btn-order" style={{ margin: '5px 0' }} onClick={handlePlaceOrder}>Đặt hàng</button>
        </div>
      </div>
    </div >
    </div>
  );
};

export default OnlinePayment;
