import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { useDispatch, useSelector } from 'react-redux';
import { DeleteProduct, getProductById } from '../../../redux/product/ProductAction';
import { toast } from 'react-toastify';
import { useEffect, useState } from 'react';
import { updateProductUnit } from '../../../service/ProductService';
import ModalUpdateUnit from './ModalUpdateUnit';
import ModalAddUnit from './ModalAddUnit';

function ModalDeleteProduct(props) {
  const { dataDelete, show, handleClose } = props;
  const dispatch = useDispatch();
  const dataup = useSelector(state => state.cart.product);
const [data,setData]=useState('');
const[showupdateunit,setshowupdateunit]=useState(false);
const[addUnit,setAddUnit]=useState(false);
const[showaddunit,setshowaddunit]=useState(false);
const[isDelete,setisdelete]=useState(false);


const [productid,setProductId]=useState('');
useEffect(() => {
  setProductId(dataup.id);
}, []);
// useEffect(() => {
//   dispatch(getProductById(dataup.id))
//   setProductId(dataup.id);
// }, [dataup.units]);


  const handleUpdateUnit =  (item) => {
  
    setData(item);
    setisdelete(false);
    setshowupdateunit(true)

  };
  const handleDeleteUnit =  (item) => {
  
    setData(item);
    setisdelete(true);

    setshowupdateunit(true)

  };
  const handleAddUnit= ()=>{
    setshowaddunit(true)


  }
  const handleCloseModalUpdate = ()=>{
    setshowupdateunit(false)
    setshowaddunit(false)
    dispatch(getProductById(dataup.id))
    setProductId(dataup.id);

  }

  return (
    <>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Cập Nhật Đơn Vị Sản Phẩm <strong>{dataDelete.name}</strong></Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <ModalUpdateUnit show={showupdateunit}
        handleClose={handleCloseModalUpdate}
        item={data}
        productid={productid}
        isDelete={isDelete}
      
        ></ModalUpdateUnit>
        <ModalAddUnit show={showaddunit}
        handleClose={handleCloseModalUpdate}
     ></ModalAddUnit>

        {dataup && Array.isArray(dataup.units) && dataup.units.map((item, index) => (
          
  <div className='row input-container' key={index}>
    <div className='col-md-2'>
      <label>Loại Đơn Vị:</label>
      <div>{item.unitType}</div>
    </div>
    <div className='col-md-2'>
      <label>Giá Trị:</label>
      <div>{item.unitValue}</div>
    </div>
    <div className='col-md-2'>
      <label>Số Lượng:</label>
      <div>{item.quantity}</div>
    </div>
    <div className='col-md-2'>
      <label>Giá Nhập:</label>
      <div>{item.importPrice}</div>
    </div>
    <div className='col-md-2'>
      <label>Giá Bán:</label>
      <div>{item.sellPrice}</div>
    </div>
    <div className='col-md-2'>
      <label>Hình Ảnh:</label>
      <div>
  <img 
    style={{
      width: '8.5rem',
      height: '6rem',
      objectFit: 'cover'
    }} 
    src={item.unitFeatureImage} 
    alt="Unit Feature" 
  />
</div>
    </div>
    <div className='d-flex flex-row mt-2'>
      
      <i className="fa-solid fa-pen-to-square" onClick={() => handleUpdateUnit(item)}></i>
      <i class="fa-solid fa-trash"  onClick={() => handleDeleteUnit(item)}></i>
    </div>
  </div>
))}
  <button type="button" class="btn btn-success custom-btn" onClick={() => handleAddUnit()}  >
              Thêm đơn vị </button>        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" onClick={handleClose}>Có</Button>
          <Button variant="secondary" onClick={handleClose}>Không</Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalDeleteProduct;

