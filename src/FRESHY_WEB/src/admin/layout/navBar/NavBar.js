import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import '../../../assets/css/navBar.css'
import logo from '../../../assets/image/logo.png'
function NavBar (){
    // return(<>
    //     <Navbar collapseOnSelect expand="lg" className="">
    //   <Container>
    //   <div className="logo">
    //     <NavLink to='/'><img src={logo} alt=""/></NavLink>
    //   </div>
    //     <Navbar.Toggle aria-controls="responsive-navbar-nav" />
    //     <Navbar.Collapse id="responsive-navbar-nav">
    //       <Nav className="me-auto">
    //       <NavDropdown title="Ca Làm Việc" id="collapsible-nav-dropdown">
    //           <NavDropdown.Item > <NavLink to='/admin/order'>Quản lí ca</NavLink> </NavDropdown.Item>
    //         </NavDropdown>
    //         <NavDropdown title="Mặt Hàng" id="collapsible-nav-dropdown">
    //           <NavDropdown.Item > <NavLink to='/admin/product'>Quản lý Mặt Hàng</NavLink> </NavDropdown.Item>
    //         </NavDropdown>
    //         <NavDropdown title="Hóa đơn" id="collapsible-nav-dropdown">
    //           {/* <NavDropdown.Item > <NavLink to='product'>Chi Tiết Hóa Đơn</NavLink> </NavDropdown.Item> */}
    //         </NavDropdown>
    //         <NavDropdown title="Thống kê" id="collapsible-nav-dropdown">
    //           {/* <NavDropdown.Item > <NavLink to='product'>Thống Kê Doanh Thu</NavLink> </NavDropdown.Item>
    //           <NavDropdown.Item > <NavLink to='product2'>Thông kê Mặt Hàng</NavLink> </NavDropdown.Item> */}
    //         </NavDropdown>
    //         <NavDropdown title="Nhân Sự" id="collapsible-nav-dropdown">
    //           <NavDropdown.Item > <NavLink to='/admin/employee'>Quản lý nhân viên</NavLink> </NavDropdown.Item>
    //           {/* <NavDropdown.Item > <NavLink to='product2'>Thông kê Mặt Hàng</NavLink> </NavDropdown.Item> */}
    //         </NavDropdown>
    //       </Nav>
    //     </Navbar.Collapse>
    //   </Container>
    // </Navbar>
    // </>);
}

export default NavBar;