import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { useState, useEffect } from 'react';
import { addProductUnit, updateProductUnit, uploadImage } from '../../../service/ProductService';
import { useDispatch, useSelector } from 'react-redux';
import { toast } from 'react-toastify';
import './modalupdateunit.css'
function ModalAddUnit(props) {
  const { show, handleClose} = props;
  const [unitType, setUnitType] = useState('');
  const [unitValue, setUnitValue] = useState('');
  const [quantity, setQuantity] = useState('');
  const [importPrice, setImportPrice] = useState('');
  const [sellPrice, setSellPrice] = useState('');
  const [unitFeatureImage, setUnitFeatureImage] = useState('');
  const [loading, setLoading] = useState(false);
    const dataup = useSelector(state => state.cart.product);
    useEffect(() => {
        setUnitType("");
        setUnitValue("");
        setQuantity("");
        setImportPrice("");
        setSellPrice("");
        setUnitFeatureImage("");
      }, []);
const handleAdd = async () => {
    try {
      
      if (!unitType) {
        toast.error("Thiếu Tên Đơn Vị Sản Phẩm !!!");
      } else if (!unitValue) {
        toast.error("Thiếu Giá Trị Đơn Vị !!!");
    } else if (!quantity) {
        toast.error("Thiếu Số Lượng !!!");
    } else if (!importPrice) {
        toast.error("Thiếu Giá Nhập Hàng !!!");
    } else if (!sellPrice) {
        toast.error("Thiếu Giá Bán !!!");
    } else if (!unitFeatureImage) {
        toast.error("Thiếu Ảnh Sản Phẩm !!!");
    } else {
        // If all validations pass, make the API call
        const res = await addProductUnit(dataup.id, unitType, unitValue, quantity, importPrice, sellPrice, unitFeatureImage);
    
    
      if(res.status==200){
        toast.success("Cập nhật đơn vị sản phẩm thành công !!!");
        handleClose();
      }
      else{
        toast.error("Cập nhật đơn vị thất bại !!!")
      }
    }
      // Xử lý logic sau khi cập nhật thành công, ví dụ hiển thị thông báo, đóng modal, vv.
    } catch (error) {
      console.error('Error updating product unit:', error);
      // Xử lý logic khi có lỗi, ví dụ hiển thị thông báo lỗi
    }
  };
  

  const handleUpload = async (event) => {
    setLoading(true);
  
    const imageFile = event.target.files[0]; // Truy cập vào file hình ảnh đã chọn
    console.log('Selected image:', imageFile);
  
    try {
      // Tạo một FormData object và thêm file hình ảnh vào đó
      const formData = new FormData();
      formData.append('file', imageFile);
  
      const res = await uploadImage(formData);
      console.log(res);
  
      setUnitFeatureImage(res.link);
      setLoading(false);
    } catch (error) {
      console.error('Error uploading image:', error);
      setUnitFeatureImage(''); // Đặt lại giá trị của unitFeatureImage nếu không thành công
      setLoading(false);
    }
  };
  
  return (
    <>
      <Modal className='custom-modal-add' show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Thêm Đơn Vị Sản Phẩm</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='row input-container'>
            <div className='col-md-2'>
              <label>Loại Đơn Vị:</label>
              <input
                type="text"
                onChange={(e) => setUnitType(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Trị:</label>
              <input
                type="text"
                onChange={(e) => setUnitValue(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Số Lượng:</label>
              <input
                type="text"
                onChange={(e) => setQuantity(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Nhập:</label>
              <input
                type="text"
                onChange={(e) => setImportPrice(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Bán:</label>
              <input
                type="text"
                onChange={(e) => setSellPrice(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Hình Ảnh:</label>
              <input
  type="file"
  onChange={handleUpload} // Sử dụng hàm handleUpload khi có thay đổi
/>
{loading ?(<div className="mb-3"><span class="loader"></span>
                  </div> ):( unitFeatureImage&&
<img src={unitFeatureImage} alt="Unit Feature" />)}
           
            </div>

          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="primary" onClick={handleAdd}>Thêm</Button>
          <Button variant="secondary" onClick={handleClose}>Không</Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalAddUnit;
