import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import { NavLink } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import logo from '../../../assets/image/logo.png'

function NavBarWeb (){
    return(<>
        <Navbar collapseOnSelect expand="lg" className="">
      <Container>
      <div className="logo">
        <img src={logo} alt=""/>
      </div>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link ><NavLink to='/hih'>HOME</NavLink></Nav.Link>
            <Nav.Link ><NavLink to='/category'>ALL PRODUCT</NavLink></Nav.Link>
            <NavDropdown title="PRODUCT" id="collapsible-nav-dropdown">
              <NavDropdown.Item href="#">VEGETABLE</NavDropdown.Item>
              <NavDropdown.Item href="#">
               MEAT
              </NavDropdown.Item>
              <NavDropdown.Item href="#">FISH</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#">
              FRUIT
              </NavDropdown.Item>
            </NavDropdown>
            <Nav.Link ><NavLink to='/cart'>CART</NavLink></Nav.Link>
            <Nav.Link ><NavLink to='/contactUs'>CONTACT US</NavLink></Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
    </>);

}

export default NavBarWeb;