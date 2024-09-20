import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import './product.css';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { useEffect } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';


import { useDispatch, useSelector } from 'react-redux';
import { UpdateProduct, addProducts, resetState } from '../../../redux/product/ProductAction';
import { uploadImage } from '../../../service/ProductService';
function ModalProduct(props) {
  const [unitImages, setUnitImages] = useState([]);


  const addSuccess = useSelector(state => state.cart.AddSuccess);
  const addFailed = useSelector(state => state.cart.AddFailed);
  const updateSuccess = useSelector(state => state.cart.UPDATESUCCESS);
  const updateFailed = useSelector(state => state.cart.UPDATEFAILED);

  const { handleUpdateTable, isUpdating, handleClose, show, handleShow, dataUpdate, listSupplier, listType } = props;
  const [loading, setLoading] = useState(false);
  const dispatch = useDispatch();
  const [listUnitType, setListUnitType] = useState([]);
  const [Name, setName] = useState("");
  const [FeatureImage, setFeatureImage] = useState("");
  const [Descriptions, setDescriptions] = useState("");
  const [Type, setType] = useState({
    id: '',
    name: '',
  });
  const [ImportPrice, setImportPrice] = useState("");
  const [Price, setPrice] = useState("");
  const [Supplier, setSupplier] = useState("");
  const [DOM, setDOM] = useState("");
  const [ExpiryDate, setExpiryDate] = useState("");
  const product = useSelector((state) => state.cart.product);
  const handleDomChange = (date) => {
    const currentdate = new Date();
    if (date > currentdate) {
      toast.error("Ngày phải bé hơn hiện tại  !!!");
    }
    else {
      setDOM(date);
    }
  };

  const handleExpiryDateChange = (date) => {
    const currentdate = new Date();
    if (date <= currentdate) {
      toast.error("Ngày phải lớn hơn hiện tại và phải bé hơn DOM !!!");
    }
    else if (date < DOM) {
      toast.error("Ngày phải lớn hơn hiện tại và phải bé hơn DOM !!!");
    }
    else {
      setExpiryDate(date);
    }
  };
  const handleSetType = (event) => {
    console.log(event);
    setType({
      id: event
    });
  };
  const [rows, setRows] = useState([{ id: 1, inputs: ['', '', '', '', '', ''] }]);
  const handleUpload = async (event) => {
    setLoading(true);

    const imageFile = event.target.files[0]; // Truy cập vào file hình ảnh đã chọn
    console.log('Selected image:', imageFile);

    try {
      // Tạo một FormData object và thêm file hình ảnh vào đó
      const formData = new FormData();
      formData.append('file', imageFile);

      // Tải lên hình ảnh và đợi phản hồi
      const res = await uploadImage(formData);
      console.log(res.link);

      setFeatureImage(res.link);
      setLoading(false);
    } catch (error) {
      console.error('Error uploading image:', error);
    }
  };

  const handleUploadUnit = async (event, rowIndex) => {
    setLoading(true);

    const imageFile = event.target.files[0]; // Access the selected file
    console.log('Selected image:', imageFile);

    try {
      const formData = new FormData();
      formData.append('file', imageFile);
      const res = await uploadImage(formData);
      const updatedImages = [...unitImages];
      updatedImages[rowIndex] = res.link;
      console.log("u", updatedImages)
      setUnitImages(updatedImages);
      console.log("settt", unitImages);

      setLoading(false);
    } catch (error) {
      console.error('Error uploading image:', error);
    }
  };


  useEffect(() => {
    // Call handleSubmit whenever rows change
    handleSubmit();
  }, [rows, unitImages]);


  const handleSave = () => {
    console.log(listUnitType)
    if (!Name) {
      toast.error("Thiếu thuộc tính tên sản phẩm");
    } else if (!Descriptions) {
      toast.error("Thiếu mô tả sản phẩm");
    } else if (!Type.id) {
      toast.error("Thiếu danh mục sản phẩm");
    } else if (!Supplier) {
      toast.error("Thiếu nhà cung cấp sản phẩm");
    } else if (!DOM) {
      toast.error("Thiếu ngày sản xuất");
    } else if (!ExpiryDate) {
      toast.error("Thiếu ngày hết hạn");
    } else if (listUnitType.length === 0) {
      toast.error("Thiếu đơn vị sản phẩm");
    } else {
      dispatch(addProducts(Name, FeatureImage, Descriptions, Type.id, Supplier, DOM, ExpiryDate, true, "employeeId", listUnitType));
    }


  }

  useEffect(() => {
    if (addSuccess) {
      handleClose();
      // Đặt các giá trị thành rỗng khi ở chế độ "Add New"
      setName('');
      setFeatureImage('');
      setDescriptions('');
      setImportPrice('');
      setPrice('');
      setSupplier('');
      setType('');
      setDOM('');
      setExpiryDate('');
    }


  }, [addSuccess]);
  useEffect(() => {
    if (updateSuccess) {
      handleClose();
      // Đặt các giá trị thành rỗng khi ở chế độ "Add New"
      setName('');
      setFeatureImage('');
      setDescriptions('');
      setImportPrice('');
      setPrice('');
      setSupplier('');
      setType('');
      setDOM('');
      setExpiryDate('');
    }

  }, [updateSuccess]);


  const handleUpdateProduct = async () => {

    dispatch(UpdateProduct(dataUpdate.productId, Name, FeatureImage, Descriptions, Type.id, ImportPrice, Price, dataUpdate.supplier.supplierId, DOM, ExpiryDate, dataUpdate.isShowToCustomer
      , "employeeId"));
  }
  const handleSetSupllier = (event) => {
    setSupplier(event);
  }

  useEffect(() => {

    if (show) {
      if (isUpdating) {

        // Nạp dữ liệu từ dataUpdate khi ở chế độ "Update"
        setName(dataUpdate.name);
        setFeatureImage(dataUpdate.featureImage);
        setDescriptions(dataUpdate.description);

        setSupplier(dataUpdate.supplier.name);
        setType({
          name: dataUpdate.type.name,
          id: dataUpdate.type.typeId
        });
        setDOM(dataUpdate.dom);
        setExpiryDate(dataUpdate.expiryDate);


      } else {
        // Đặt các giá trị thành rỗng khi ở chế độ "Add New"
        setName('');
        setFeatureImage('');
        setDescriptions('');
        setImportPrice('');
        setPrice('');
        setSupplier('');
        setType('');
        setDOM('');
        setExpiryDate('');

      }
    }
  }, [dataUpdate, show, isUpdating]);



  const handleSubmit = () => {
    const units = getUnitsFromRows();
    const unitsWithImages = units.map((unit, index) => ({
      ...unit,
      unitFeatureImage: unitImages[index],
    }));
    setListUnitType(unitsWithImages);
    // Dispatch or handle the units data as needed
  };


  // Function to get units from rows
  const getUnitsFromRows = () => {
    return rows.map(row => {
      return {
        unitType: row.inputs[0],
        unitValue: parseInt(row.inputs[1]),
        quantity: parseInt(row.inputs[2]),
        importPrice: parseFloat(row.inputs[3]),
        sellPrice: parseFloat(row.inputs[4]),
        unitFeatureImage: row.inputs[5]
      };
    });
  };

  // Function to add a new row
  const addRow = () => {
    setRows(prevRows => [...prevRows, { id: prevRows.length + 1, inputs: ['', '', '', '', '', ''] }]);
  };

  // Function to remove a row
  const removeRow = rowId => {
    setRows(prevRows => prevRows.filter(row => row.id !== rowId));
  };

  // Function to handle input change in a row
  const handleInputChange = (rowId, index, value) => {
    setRows(prevRows =>
      prevRows.map(row => {
        if (row.id === rowId) {
          const newInputs = [...row.inputs];
          newInputs[index] = value;
          return { ...row, inputs: newInputs };
        }
        return row;
      })
    );
    console.log(rows);
  };
  return (
    <>

      <Modal
        className='ModalProduct'
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          {isUpdating ? (<Modal.Title>Cập Nhật Sản Phẩm</Modal.Title>) : (<Modal.Title>Thêm Sản Phẩm Mới</Modal.Title>)}
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="row">
              <div className="col-md-12">
                <div className="mb-3">
                  <label htmlFor="" className="form-label">
                    Tên Sản Phẩm
                  </label>
                  <input
                    value={Name}
                    type="text"
                    className="form-control"
                    onChange={(event) => setName(event.target.value)}
                  />
                </div>
              </div>
              <div className="col-md-6">
                <div className="mb-3">
                  <label className="form-label">Hình Ảnh</label>
                  <input
                    class="form-control form-control-sm"
                    type="file"
                    className="form-control"
                    onChange={handleUpload}
                  />
                </div>
                <div className="mb-3">
                  <label className="form-label">
                    Danh Mục
                  </label>
                  <select class="form-select" aria-label="Default select example" value={Type.id} onChange={(event) => handleSetType(event.target.value)}>
                    <option value="" selected>Chọn danh mục sản phẩm</option>
                    {listType && listType.map((item, index) => (
                      <option key={index} value={item.id}>{item.name}</option>
                    ))}
                  </select>
                </div>

                <div className="mb-3">

                  {!isUpdating ? (
                    <>
                      <label className="form-label">
                        Nhà Cung Cấp
                      </label>
                      <select class="form-select" aria-label="Default select example" value={Supplier} onChange={(event) => handleSetSupllier(event.target.value)}>
                        <option value="" selected> - Chọn nhà cung cấp -</option>
                        {listSupplier && listSupplier.map((item, index) => (
                          <option key={index} value={item.id}>{item.name}</option>
                        ))}
                      </select>
                    </>
                  ) : (
                    <>
                      <label className="form-label">
                        Nhà Cung Cấp
                      </label>
                      <input
                        value={Supplier}
                        type="text"
                        className="form-control"
                        readOnly
                      />
                    </>
                  )
                  }
                </div>
              </div>
              <div class="col-md-6 mt-4">
                {loading ? (
                  <div className="mb-3"><span class="loader"></span>
                  </div>
                ) : (<></>)}
                {FeatureImage ? (
                  
                    <img class="img-thumbnail" src={FeatureImage} alt="Uploaded Image" />

               
                ) : (
                  <></>
                )}
              </div>
              <div className='col-md-12'>
                {isUpdating ? (
                  <div></div>
                ) : (
                  <div className='row'>
                    <label className="form-label">
                      Đơn Vị Sản Phẩm
                    </label>
                    <div id="modelContainer" className="custom-container">
                      {rows.map((row, rowIndex) => (
                        <div key={row.id} style={{ display: 'flex', justifyContent: 'space-between' }}>
                          <form>
                            <div className="row input-group">
                              <div className='col-md-4'>
                                <label>Đơn Vị</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="text"
                                  value={row.inputs[0]}
                                  onChange={e => handleInputChange(row.id, 0, e.target.value)}
                                />
                              </div>
                              <div className='col-md-4'>
                                <label>SL Đơn vị</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="text"
                                  value={row.inputs[1]}
                                  onChange={e => handleInputChange(row.id, 1, e.target.value)}
                                />
                              </div>
                              <div className='col-md-4'>
                                <label>SL Sản phẩm</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="text"
                                  value={row.inputs[2]}
                                  onChange={e => handleInputChange(row.id, 2, e.target.value)}
                                />
                              </div>
                            </div>
                            <div className="row input-group">
                              <div className='col-md-4'>
                                <label>Giá Nhập</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="text"
                                  value={row.inputs[3]}
                                  onChange={e => handleInputChange(row.id, 3, e.target.value)}
                                />
                              </div>
                              <div className='col-md-4'>
                                <label>Giá Bán</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="text"
                                  value={row.inputs[4]}
                                  onChange={e => handleInputChange(row.id, 4, e.target.value)}
                                />
                              </div>
                              <div className='col-md-4'>
                                <label>Hình Ảnh</label>
                                <input
                                  className="form-control form-control-sm"
                                  type="file"
                                  onChange={(event) => handleUploadUnit(event, rowIndex)}
                                />
                              </div>

                            </div>
                            <hr></hr>
                          </form>
                          <span className="remove-button" onClick={() => removeRow(row.id)}>
                            <i className="fa-solid fa-minus" style={{ color: 'red' }}></i>
                          </span>
                        </div>
                      ))}
                      <span className="add-button" onClick={() => addRow()}>
                        <i className="fa-solid fa-plus" style={{ color: 'green' }}></i>
                      </span>
                    </div>
                  </div>

                )}
              </div>
              <div className="d-flex">
              {!isUpdating && unitImages.map((item, index) => (
                <img key={index} src={item}></img>

              ))} 
              </div>
              
              <div className="col-md-12">
                <div className='row'>
                  <div className='col-md-6'>
                    <div className="mb-4">
                      <label className="form-label" htmlFor="datePicker">Ngày Sản Xuất</label>
                      <br />
                      <DatePicker
                        className="form-control form-control-sm"
                        id="datePicker"
                        selected={DOM}
                        onChange={handleDomChange}
                        dateFormat="MM/dd/yyyy"
                        placeholderText="Nhập Ngày Sản Xuất"
                      />
                    </div>
                  </div>
                  <div className='col-md-6'>
                    <div className="mb-4">
                      <label className="form-label" htmlFor="datePicker">Ngày Hết Hạn</label>
                      <br />
                      <DatePicker
                        className="form-control form-control-sm"
                        id="datePicker"
                        selected={ExpiryDate}
                        onChange={handleExpiryDateChange}
                        dateFormat="MM/dd/yyyy"
                        placeholderText="Nhập Ngày Hết Hạn"
                      />
                    </div>
                  </div>
                </div>
                <div className="mb-3">
                  <label className="form-label">
                    Mô Tả
                  </label>
                  <div className="form-floating" style={{ paddingTop: 0 }}>
                    <textarea value={Descriptions} className="form-control" placeholder="Leave a comment here" id="floatingTextarea2" style={{ height: '60px', paddingTop: 10 }} onChange={(event) => setDescriptions(event.target.value)}></textarea>
                  </div>
                </div>
              </div>

            </div>



          </form>
        </Modal.Body>


        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          {isUpdating ? (<Button variant="primary" onClick={handleUpdateProduct}>Comfirm</Button>) : (<Button variant="primary" onClick={handleSave}>SAVE</Button>)}
        </Modal.Footer>
      </Modal>

    </>
  );
}
export default ModalProduct;