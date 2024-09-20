import NavBar from '../navBar/NavBar';
import '../../../assets/css/header.css'
import { NavLink } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from "react-redux";
import { handleRefreshRedux, handleLogoutRedux } from '../../../redux/user/UserAction';
import { Dropdown } from 'react-bootstrap';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import logo from '../../../assets/image/logo.png'
function Header() {
  const dispatch = useDispatch();

  const roles = useSelector(state => state.user.account.role);
  const name = useSelector(state => state.user.account.name);
  
  useEffect(()=>{

  },[])

  useEffect(() => {
    dispatch(handleRefreshRedux());
  }, [dispatch]);

  const hasAdminRole = roles && roles.some(role => role === "Admin");
  const hasCustomerRole = roles && roles.some(role => role === "Employee");
  const handleLogout = () => {
    dispatch(handleLogoutRedux());
  }

  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };
  
  return (
    <>
      <div className="header">
        <div className="container">
          <div className='row'>
            <div className='col-md-10 col-sm-10'>
            <Navbar collapseOnSelect expand="lg" className="">
      <Container>
      <div className="logo">
        <NavLink to='/'><img src={logo} alt=""/></NavLink>
      </div>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
          <NavDropdown title="Ca Làm Việc" id="collapsible-nav-dropdown">
              <NavDropdown.Item > <NavLink to='/admin/order'>Quản lí ca</NavLink> </NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title="Mặt Hàng" id="collapsible-nav-dropdown">
              <NavDropdown.Item > <NavLink to='/admin/product'>Quản lý Mặt Hàng</NavLink> </NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title="Hóa đơn" id="collapsible-nav-dropdown">
              {/* <NavDropdown.Item > <NavLink to='product'>Chi Tiết Hóa Đơn</NavLink> </NavDropdown.Item> */}
            </NavDropdown>
            <NavDropdown title="Thống kê" id="collapsible-nav-dropdown">
              {/* <NavDropdown.Item > <NavLink to='product'>Thống Kê Doanh Thu</NavLink> </NavDropdown.Item>
              <NavDropdown.Item > <NavLink to='product2'>Thông kê Mặt Hàng</NavLink> </NavDropdown.Item> */}
            </NavDropdown>
            {hasAdminRole ? (<>
            <NavDropdown title="Nhân Sự" id="collapsible-nav-dropdown">
              <NavDropdown.Item > <NavLink to='/admin/employee'>Quản lý nhân viên</NavLink> </NavDropdown.Item>
              {/* <NavDropdown.Item > <NavLink to='product2'>Thông kê Mặt Hàng</NavLink> </NavDropdown.Item> */}
            </NavDropdown>
              </> ):(<></>)}


          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
            </div>
            <div className="phone col-md-2 col-sm-2 mt-4 align-items-center d-flex">
              <Dropdown>
                <Dropdown.Toggle variant="success" id="dropdown-basic">
                  <i className="fa-solid fa-user fa-2xl" style={{ color: "#51a26f" }} ></i>
                </Dropdown.Toggle>
                <Dropdown.Menu>
                  {/* <Dropdown.Item href="#/profile">Profile</Dropdown.Item>
                  <Dropdown.Item href="#/settings">Settings</Dropdown.Item> */}
                  <Dropdown.Item onClick={handleLogout}>Logout</Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
              <div>
                <h6>Nhân Viên : A</h6>
              </div>

            </div> 
          </div>

        </div>
      </div>
    </>
  );
}

export default Header;
