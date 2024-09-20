import React, { useState, useEffect } from 'react';
import ReactPaginate from 'react-paginate';
import { debounce } from "lodash";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './Employee.css'
import _ from "lodash"
import Table from 'react-bootstrap/Table';
import { format } from 'date-fns';

import { useDispatch, useSelector } from 'react-redux';
import { fetchAllEmployee, searchEmployee } from '../../../service/EmployeeService';
//import { searchEmployees, resetState } from '../../../redux/employee/EmployeeAction';
// Import các service và component cần thiết
//import { fetchAllSupplier, fetchAllTypeOfProduct } from '../../../service/EmployeeService';
//import ModalEmployee from './ModalEmployee';
//import ModalDeleteEmployee from './ModalDeleteEmployee';

const Employee = (props) => {
  const [show, setShow] = useState(false);
  const [dataDelete, setDataDelete] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  //const [listSupplier, setListSupplier] = useState([]);
  //const [listTypeEmployee, setListTypeEmployee] = useState([]);
  const [dataUpdate, setDataUpdate] = useState('');
  const [isUpdating, setIsUpdating] = useState(false);
  const [isShowModalDelete, setIsShowModalDelete] = useState(false);

  // Các state để lưu giá trị tìm kiếm
  const [nameSearch, setNameSearch] = useState('');
  const [phoneSearch, setPhoneSearch] = useState('');
  const [listEmployee, setListEmployee] = useState([]);
  const [hometownSearch, setHometownSearch] = useState('');

  // Lấy các dữ liệu từ Redux store
  const dispatch = useDispatch();
  //const employees = useSelector(state => state.employee.employees);
  // const totalPage = useSelector(state => state.employee.totalPages);



  const handleUpdateButtonClick = (product) => {
    setIsUpdating(true);
    setDataUpdate(product);
    setShow(true);
  }
  const handleDeleteButtonClick = (product) => {
    setDataDelete(product);
    // handShowModalDelete();
  }
  const handlePageClick = async (event) => {
    console.log(event);
    setPage(event.selected + 1)
    let res = await searchEmployee(nameSearch, hometownSearch,phoneSearch, event.selected + 1, 3);
    console.log(res);
    setListEmployee(res.data.data);
  }

  const handleClose = () => {
    setShow(false);
    console.log(show);
    setIsShowModalDelete(false);
  }
  const handleShow = () => {
    setShow(true);
    console.log(show);
    setIsUpdating(false);
  }

  useEffect(() => {
    const fetchData = async () => {
      try {
       let res = await fetchAllEmployee(1, 3);
        console.log(res.data.data);
        setListEmployee(res.data.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);


  // Hàm xử lý tìm kiếm
  const handleSearch = async () => {
    try {
      let res = await searchEmployee(nameSearch, hometownSearch,phoneSearch, 1, 3);
      console.log(res);
      setListEmployee(res.data.data);
    } catch (error) {
      console.error("Error searching employees:", error);
    }
  }

  // Sử dụng useEffect để gọi hàm handleSearch khi có sự thay đổi trong các state tìm kiếm
  useEffect(() => {
    handleSearch();
  }, [nameSearch, phoneSearch, hometownSearch]);

  // Xử lý sự kiện khi thay đổi giá trị của các state tìm kiếm
  const handleNameSearchChange = (event) => {
    setNameSearch(event.target.value);
  }

  const handlePhoneSearchChange = (event) => {
    setPhoneSearch(event.target.value);
  }

  const handleHometownSearchChange = (event) => {
    setHometownSearch(event.target.value);
  }

  // Trong JSX, bạn có thể sử dụng các hàm xử lý sự kiện này trong các phần tử HTML input

  return (
    <>
    <h2>Employee Manage</h2>
      {/* Phần tìm kiếm */}
      <div className="filter">
        <div className="search-box">
          <input
            type="text"
            placeholder="Tên nhân viên"
            value={nameSearch}
            onChange={(event) => setNameSearch(event.target.value)}
          />
          <input
            type="text"
            placeholder="Số điện thoại"
            value={phoneSearch}
            onChange={(event) => setPhoneSearch(event.target.value)}
          />
          <input
            type="text"
            placeholder="Quê quán"
            value={hometownSearch}
            onChange={(event) => setHometownSearch(event.target.value)}
          />
          <button className="btn btn-primary" >
            Tìm kiếm
          </button>
        </div>
      </div>

      {/* Phần hiển thị danh sách nhân viên */}
      <div className="table-responsive">
        <table className="table table-striped table-bordered">
          <thead>
            <tr>
              <th>AccountId</th>
              <th>EmployeeId</th>
              <th>Fullname</th>
              <th>Avatar</th>
              <th>PhoneNumber</th>
              <th>Hometown</th>
              <th>SSN</th>
            </tr>
          </thead>
          <tbody>
            {listEmployee && listEmployee.map((employee, index) => (
              <tr key={index}>
                <td>{employee.accountId}</td>
                <td>{employee.employeeId}</td>
                <td>{employee.fullname}</td>
                <td><img src={employee.avatar} alt="Avatar" /></td>
                <td>{employee.phoneNumber}</td>
                <td>{employee.hometown}</td>
                <td>{employee.ssn.value}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>


      {/* Phân trang */}
      <div className="pagination">
        <ReactPaginate
          nextLabel=" >"
          onPageChange={handlePageClick}
          pageRangeDisplayed={3}
          marginPagesDisplayed={2}
          pageCount={3 || 0}
          previousLabel="< "
          containerClassName="pagination"
          activeClassName="active"
          renderOnZeroPageCount={null}
        />
      </div>

      {/* Modal */}
      {/* <ModalEmployee
        handleUpdateTable={dispatch(searchEmployees(nameSearch, phoneSearch, hometownSearch, page, pageSize))}
        isUpdating={isUpdating}
        handleClose={handleClose}
        show={show}
        handleShow={handleShow}
        dataUpdate={dataUpdate}
        listSupplier={listSupplier}
        listType={listTypeEmployee}
      />
      <ModalDeleteEmployee
        show={isShowModalDelete}
        handleShow={handShowModalDelete}
        handleClose={handleClose}
        dataDelete={dataDelete}
        handleUpdateTable={dispatch(searchEmployees(nameSearch, phoneSearch, hometownSearch, page, pageSize))}
      /> */}
    </>
  );
};

export default Employee;
