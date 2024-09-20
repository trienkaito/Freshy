import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { useState, useEffect } from 'react';
import { deleteProductUnit, updateProductUnit, uploadImage } from '../../../service/ProductService';
import { useDispatch, useSelector } from 'react-redux';
import { toast } from 'react-toastify';
import './modalupdateunit.css'
function ModalUpdateUnit(props) {
  const {show, handleClose, item,productid,isDelete} = props;
  const [unitType, setUnitType] = useState('');
  const [unitValue, setUnitValue] = useState('');
  const [quantity, setQuantity] = useState('');
  const [importPrice, setImportPrice] = useState('');
  const [sellPrice, setSellPrice] = useState('');
  const [unitFeatureImage, setUnitFeatureImage] = useState('');
  const [loading, setLoading] = useState(false);
    const dataup = useSelector(state => state.cart.product);

    const handleUpdate = async () => {
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
          const res = await updateProductUnit(dataup.id, item.unitId, unitType, unitValue, quantity, importPrice, sellPrice, unitFeatureImage);
          if (res.status == 200) {
            toast.success("Cập nhật đơn vị sản phẩm thành công !!!");
            handleClose();
          } else {
            toast.error("Cập nhật đơn vị thất bại !!!")
          }
        }
        // Xử lý logic sau khi cập nhật thành công, ví dụ hiển thị thông báo, đóng modal, vv.
      } catch (error) {
        console.error('Error updating product unit:', error);
        // Xử lý logic khi có lỗi, ví dụ hiển thị thông báo lỗi
      }
    };
    
  
  
const handleDelete = async () => {
  try {
    
    const res = await deleteProductUnit(item.unitId);
    if(res.status==200){
      toast.success("Xóa đơn vị sản phẩm thành công !!!");
      handleClose();
    }
    else{
      toast.error("Xóa đơn vị thất bại !!!")
      
    }
    // Xử lý logic sau khi cập nhật thành công, ví dụ hiển thị thông báo, đóng modal, vv.
  } catch (error) {
    console.error('Error updating product unit:', error);
    // Xử lý logic khi có lỗi, ví dụ hiển thị thông báo lỗi
  }
};
  useEffect(() => {
    setUnitType(item.unitType);
    setUnitValue(item.unitValue);
    setQuantity(item.quantity);
    setImportPrice(item.importPrice);
    setSellPrice(item.sellPrice);
    setUnitFeatureImage(item.unitFeatureImage);
  }, [item]);
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
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{!isDelete?"Cập Nhật Đơn Vị Sản Phẩm":"Xóa Đơn Vị Sản Phẩm"}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {!isDelete?(
          <div className='row input-container'>
            <div className='col-md-2'>
              <label>Loại Đơn Vị:</label>
              <input
                type="text"
                value={unitType}
                onChange={(e) => setUnitType(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Trị:</label>
              <input
                type="text"
                value={unitValue}
                onChange={(e) => setUnitValue(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Số Lượng:</label>
              <input
                type="text"
                value={quantity}
                onChange={(e) => setQuantity(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Nhập:</label>
              <input
                type="text"
                value={importPrice}
                onChange={(e) => setImportPrice(e.target.value)}
              />
            </div>
            <div className='col-md-2'>
              <label>Giá Bán:</label>
              <input
                type="text"
                value={sellPrice}
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
                  </div> ):(
<img src={unitFeatureImage} alt="Unit Feature" />)}
           
            </div>

          </div>
          ):(<div>
            Bạn có muốn xóa {unitType}


          </div>)}
        </Modal.Body>
        <Modal.Footer>
          {!isDelete?(
          <Button variant="primary" onClick={handleUpdate}>Cập nhật</Button>
          ):(<Button variant="primary" onClick={handleDelete}>Xóa</Button>)}
          <Button variant="secondary" onClick={handleClose}>Không</Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default ModalUpdateUnit;
