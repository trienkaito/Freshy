import React, { useState, useEffect } from 'react';
import ReactPaginate from 'react-paginate';
import 'react-toastify/dist/ReactToastify.css';
import './product.css';
import _ from "lodash";
import Table from 'react-bootstrap/Table';
import { format } from 'date-fns';
import '../../../assets/css/product.css'


import { useDispatch, useSelector } from 'react-redux';
import { DeleteProduct, SearchProduct, UpdateProduct, addProducts, getProductById, getProducts, resetState } from '../../../redux/product/ProductAction';
import ModalProduct from './ModalProduct';
import { fetchAllProduct, fetchAllSupplier, fetchAllTypeOfProduct } from '../../../service/ProductService';
import ModalDeleteProduct from './ModalDeleteProduct';
const Product = (props) => {
  const [show, setShow] = useState(false);
  const [dataDelete, setDataDelete] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [listSupplier, setListSupplier] = useState([]);
  const [listTypeProduct, setListTypeProduct] = useState([]);
  const [dataUpdate, setDataUpdate] = useState('');
  const [isUpdating, setIsUpdating] = useState(false);
  const [listProduct, setListProduct] = useState([]);
  const [isShowModalDelete, setIsShowModalDelete] = useState(false);
  const addSuccess = useSelector(state => state.cart.AddSuccess);
  const addFailed = useSelector(state => state.cart.AddFailed);
  const updateSuccess = useSelector(state => state.cart.UPDATESUCCESS);
  const updateFailed = useSelector(state => state.cart.UPDATEFAILED);
  const products = useSelector(state => state.cart.products);
  const totalPage = useSelector(state => state.cart.totalPages);
  // const deleteSuccess=useSelector(state=>state.cart.DELETESUCCESS);
  const [typeSearch, setTypeSearch] = useState('');
  const [nameSearch, setNameSearch] = useState('');
  const [supplierSearch, setSupplierSearch] = useState('');
  const [listProductSearch, setListProductSearch] = useState('');
  const dispatch = useDispatch();


  const handShowModalDelete = (product) => {
    setIsShowModalDelete(true);
    setDataDelete(product);
  }
  const handleUpdateButtonClick = (product) => {
    setIsUpdating(true);
    setDataUpdate(product);
    setShow(true);
  }
  const handleIsShowToCustomer = (item) => {

    dispatch(
      UpdateProduct(
        item.productId,           // id
        item.name,                 // name
        item.featureImage,                    // featureImage (tạm thời là chuỗi "test")
        item.description,              // description
        item.type.typeId,                   // typeId
        item.importPrice,               // importPrice
        item.price,                     // price
        item.supplier.supplierId,  // supplierId
        item.dom,                       // dom
        item.expiryDate,                // expiryDate
        !item.isShowToCustomer,                      // isShowToCustomer
        "employeeId"                 // employeeId
      )
    );


  }

  // const handleDeleteButtonClick=(product)=>{
  //   setDataDelete(product);
  //   handShowModalDelete();
  // }
  const handlePageClick = (event) => {
    console.log("clicl", event);
    setPage(event.selected + 1)

  }

  const handleClose = () => {
    setShow(false);

    setIsShowModalDelete(false);
  }
  const handleShow = () => {
    setShow(true);
    console.log(show);
    setIsUpdating(false);
  }

  const HandleGetAllSupplier = async () => {
    let res = await fetchAllSupplier();
    if (res && res.data) {
      setListSupplier(res.data.data);
    }

  }
  const HandleGetAllTypeOfProduct = async () => {
    let res = await fetchAllTypeOfProduct();
    if (res && res.data) {
      setListTypeProduct(res.data.data)
    }

  }
  useEffect(() => {
    if (products && listSupplier && listTypeProduct) {
      HandleGetAllProduct(page, pageSize);
      HandleGetAllSupplier();
      HandleGetAllTypeOfProduct();
    }
  }, [])
  useEffect(() => {

    if (updateSuccess || addSuccess) {
      dispatch(resetState());
      setPage(page);
      setPageSize(pageSize);
      setNameSearch('');
      setSupplierSearch('');
      setTypeSearch('');
    }
    if (nameSearch || supplierSearch || typeSearch) {
      dispatch(SearchProduct(nameSearch, supplierSearch, typeSearch, page, pageSize));
    }
    else {
      HandleGetAllProduct(page, pageSize);
    }
  }, [page, pageSize, addSuccess, updateSuccess])


  const HandleGetAllProduct = async (page, pageSize) => {
    dispatch(getProducts(page, pageSize));
  }

  const handleSearch = async (productname, suppliername, typename, pageNumber, pageSize) => {
    if (productname || suppliername || typename) {
      dispatch(SearchProduct(productname, suppliername, typename, pageNumber, pageSize));
    }
    else {
      HandleGetAllProduct(1, pageSize)
    }
  }
  useEffect(() => {
    handleSearch(nameSearch, supplierSearch, typeSearch, 1, pageSize);
  }, [nameSearch, supplierSearch, typeSearch])


  return (
    <>

      <div className="container ">
        <div className="filter ">

          <div className="custom-combobox">
            <h6>Nhà Cung Cấp</h6>

            <select className="select-box" value={supplierSearch} onChange={(event) => setSupplierSearch(event.target.value)}>
              <option value="">- Chọn nhà cung cấp -</option>

              {listSupplier && listSupplier.map((item, index) => (
                <option key={index} value={item.name}>{item.name}</option>
              ))}

            </select>
          </div>
          <div className="custom-combobox">
            <h6>Danh Mục Sản Phẩm</h6>
            <select className="select-box" value={typeSearch} onChange={(event) => setTypeSearch(event.target.value)}>
              <option value="" > - Chọn danh mục sản phẩm -</option>
              {listTypeProduct && listTypeProduct.map((item, index) => (
                <option key={index} value={item.name}>{item.name}</option>
              ))}
            </select>
          </div>
          <div>
            <h6>Tìm Kiếm Sản Phẩm</h6>
            <div className='bt-search'>
              <input type="text" value={nameSearch}
                onChange={(event) => { setNameSearch(event.target.value) }}
                class="form-control" placeholder="Nhập tên hoặc Mã sản phẩm cần tìm" aria-label="Recipient's username" aria-describedby="basic-addon2" />
              <button type="button" class="btn btn-success custom-btn">Tìm Kiếm</button>
            </div>
          </div>
        </div>
        <div style={{ marginTop: '20px' }} className='filter-2 d-flex justify-content-between'>
          <div>
            <ReactPaginate
              nextLabel="-->"
              onPageChange={handlePageClick}
              pageRangeDisplayed={3}
              marginPagesDisplayed={2}
              pageCount={totalPage || 0}
              previousLabel="<--"
              pageClassName="page-item"
              pageLinkClassName="page-link"
              previousClassName="page-item"
              previousLinkClassName="page-link"
              nextClassName="page-item"
              nextLinkClassName="page-link"
              breakLabel="..."
              breakClassName="page-item"
              breakLinkClassName="page-link"
              containerClassName="pagination"
              activeClassName="active"
              renderOnZeroPageCount={null}
            />

          </div>
          <div style={{ marginLeft: '130px' }}>
            <button type="button" class="btn btn-success custom-btn" onClick={() => handleShow()}  >
              Thêm sản phẩm </button>

          </div>
          <div className='select-filter-2'>
            <div className='thanhphan'>
              <h5>Hiển thị </h5>
            </div>
            <div className='thanhphan'>
              <select onChange={(event) => setPageSize(event.target.value)} value={pageSize}>
                <option value="5">5</option>
                <option value="8">8</option>
                <option value="10">10</option>
                <option value="15">15</option>
                <option value="20">20</option>
              </select>
            </div>
            <div className='thanhphan'>
              <h5> Sản phẩm</h5>

            </div>
          </div>

        </div>
        <ModalDeleteProduct show={isShowModalDelete}
          handleShow={handShowModalDelete}
          handleClose={handleClose}
          dataDelete={dataDelete}
        />
        <ModalProduct handleUpdateTable={HandleGetAllProduct} isUpdating={isUpdating} handleClose={handleClose} show={show} handleShow={handleShow}
          dataUpdate={dataUpdate}
          listSupplier={listSupplier}
          listType={listTypeProduct}
        />

        <hr></hr>
        <div className="table-responsive">
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Tên Sản Phẩm</th>
                <th>Hình Ảnh</th>
                <th>Danh Mục</th>
                <th>Nhà Cung Cấp</th>
                <th>Ngày Sản Xuất</th>
                <th>Ngày Hết Hạn</th>
                <th>Hành Động</th>
              </tr>
            </thead>
            <tbody>

              {products && products.map((item, index) => (
                <tr key={index}>
                  <td>
                    <a onClick={() => {
                      handShowModalDelete(item);
                      dispatch(getProductById(item.productId));
                    }} href='#' className='a-product'>
                      {item.name}
                    </a>
                  </td>
                  <td style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <img
                      src={item.featureImage}
                      alt="Product Image"
                      style={{ width: '60px', height: '60px' }} // Thiết lập kích thước ảnh
                    />
                  </td>




                  <td>{item.type.name}</td>


                  <td>{item.supplier.name}</td>
                  <td>{format(new Date(item.dom), 'MM/dd/yyyy')}</td>
                  <td>{format(new Date(item.expiryDate), 'MM/dd/yyyy')}</td>





                  <td>
                    <div className="d-flex">
                      <button className="btn btn-primary me-1 update-button" >
                        <i class="fa-solid fa-pen-to-square"
                          onClick={() => handleUpdateButtonClick(item)}

                        ></i>
                      </button>
                      {/* <button className="btn btn-danger me-1">
<i class="fa-solid fa-trash-can"
onClick={()=>handleDeleteButtonClick(item)}></i>                    
</button> */}
                      <button className="btn btn-light  border border-danger"

                      >
                        {item.isShowToCustomer ? (
                          <i className="fa-regular fa-eye-slash" onClick={() => handleIsShowToCustomer(item)}></i>
                        ) : (
                          <i className="fa-regular fa-eye" onClick={() => handleIsShowToCustomer(item)}></i>
                        )}

                      </button>


                    </div>
                  </td>
                </tr>
              ))}

            </tbody>

          </Table>

        </div>
      </div>
    </>
  );
}

export default Product;