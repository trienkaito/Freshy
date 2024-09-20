import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { createOrderOffline } from '../../../service/OrderService';
import { toast } from 'react-toastify';
import { useDispatch, useSelector } from 'react-redux';
import { setPaidSuccess } from '../../../redux/product/ProductAction';
import "./modalorder.css";
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.13.1/css/all.min.css" integrity="sha256-2XFplPlrFClt0bIdPgpz8H7ojnk10H69xRqd9+uTShA=" crossorigin="anonymous" />

function ModalOrder(props) {
const {handleShow,handleClose,show,data,infoCustomer,totalAmount,discount}=props
const dispatch=useDispatch();
const[total,setTotal]=useState(0);
const paidtype=useSelector(state=>state.cart.paid_type);

useEffect(() => {
  let total = 0;
  
  {data&&data.forEach(item => {
    total += item.price * item.quantity;
  });
  setTotal(total);}
}, [data]);


const handlePayment = async (list) => {
    // Extracting specific properties from each item in the list
    const orderItems = data.map(item => ({
        productId: item.productId.value, // Assuming productId is an object with a 'value' property
        unitId: item.unitID.value, // Assuming unitId is an object with a 'value' property
        boughtQuantity: item.quantity
      }));
      console.log("data",data)
      console.log("orerItem",orderItems)
      
    try {
      if(infoCustomer){
      let res = await createOrderOffline(
        infoCustomer.id.value,
        orderItems, 
        paidtype,
        "BOHPI2ZQT",
        "4DCF98E5-87DF-4CFD-95A6-A1BE603FAF21"
      );
      if(res.status==200){
        handleClose();
        dispatch(setPaidSuccess());

        toast.success("Thanh toán thành công !!!");
      }
      }
      else{
        let res = await createOrderOffline(
          "3C4D8346-739D-4B66-9F26-73BA64AE7605",
          orderItems, 
          paidtype,
          "BOHPI2ZQT",
          "4DCF98E5-87DF-4CFD-95A6-A1BE603FAF21"
        );
        if(res.status==200){
          handleClose();
          dispatch(setPaidSuccess());
  
          toast.success("Thanh toán thành công !!!");
        }
      }
     
    } catch (error) {
      console.error("Error:", error);
    }
  };
  
  return (
    
<>
      <Modal show={show} onHide={handleClose} className="custom-modal">
        <Modal.Header closeButton>
          <Modal.Title>Hóa Đơn</Modal.Title>
        </Modal.Header>
        <Modal.Body>


  <div className="container">
    <div className="row">
      <div className="col-lg-12">
        <div className="card">
          <div className="card-body">
            <div className="invoice-title">
            <img className="float-start" src="/static/media/logo.bae6299231130497bb2a.png" alt=""/>
              {/* <h4 className="float-end">Hóa đơn #DS0204 </h4> */}
              <div className="mb-1">
                <h3 className="mb-1 text-muted">HÓA ĐƠN THANH TOÁN</h3>
              </div>
              <div className="text-muted">
                <p className="mb-1">Nam Kỳ Khởi Nghĩa, FPT Complex</p>
                <p className="mb-1"> freshy@gmail.com</p>
                <p className="mb-1"> 012-345-6789</p>
              </div>
            </div>

            <hr className="my-2"/>

            <div className="row">
              <div className="col-sm-12">
                {infoCustomer ? (
                  <>
                    <div className="text-muted">
                        <div className="d-flex info">
                            <h5>Invoice Date:</h5>
                            <p>12 Oct, 2020</p>
                        </div>
                        <div className="d-flex info">
                            <h5>Khách Hàng:</h5>
                            <p> {infoCustomer.name}</p>
                        </div>
                                
                        <div className="d-flex info">
                            <h5>Điện thoại:</h5>
                            <p>{infoCustomer.phone}</p>
                        </div>
                    </div>
                  </>
                ) : (  <>
                  <div className="text-muted">
                      <div className="d-flex info">
                          <h5>Invoice Date:</h5>
                          <p>12 Oct, 2020</p>
                      </div>
                      <div className="d-flex info">
                          <h5>Khách Hàng:</h5>
                          <p> DEFAULT CUSTOMER</p>
                      </div>
                              
                      <div className="d-flex info">
                          <h5>Điện thoại:</h5>
                          <p>0000000000</p>
                      </div>
                  </div>
                </>)}
              </div>
            </div>

            <div className="py-2">
              {/* <h5 className="font-size-15">Order Summary</h5> */}
              <div className="table-responsive">
                <table className="table align-middle table-nowrap table-centered mb-0">
                  <thead>
                    <tr className="tr-pay">
                      <th>STT</th>
                      <th style={{width: '180px'}}>Tên SP</th>
                      <th style={{width: '70px'}}>Đơn giá</th>
                      <th>SL</th>
                      <th className="text-end" style={{width: '90px'}}>Thành tiền</th>
                    </tr>
                  </thead>
                  <tbody>
                    {data && data.map((item, index) => (
                      <tr key={index}>
                        <td>{index + 1}</td>
                        <td>{item.productName}</td>
                        <td>{item.price}</td>
                        <td>{item.quantity}</td>
                        <td className="text-end">{item.price * item.quantity}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
              <hr className="my-4"/>
              <div>
                  <div className="total-pay">
                      <h5>Cộng tiền hàng:</h5>
                       <p>{total} VND</p>
                  </div>
                  <div className="total-pay">
                      <h5>Chiết khấu ({discount ? discount + "%" : "0%"})</h5>
                       
                       <p>{(total - totalAmount).toFixed(2)} VND</p>
                  </div>
                  <div className="total-pay">
                      <h5>Tổng thanh toán:</h5>
                       <p>{totalAmount} VND</p>
                  </div>
              </div>
              
              
                      
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose} style={{ padding: '2px 13px' }}>
            HỦY
          </Button>
        <Button variant="primary" onClick={handlePayment} style={{ padding: '3px 13px', marginRight: '20px'  }}>
  IN
</Button>

        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalOrder;