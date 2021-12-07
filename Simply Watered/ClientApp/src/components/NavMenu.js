import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService'

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
      admin: false,

    };
  }

  componentDidMount(){
    this.loadUser();
  }

  async loadUser() {
    const token = await authService.getAccessToken();
    const response = await fetch(`api/admin`, {
        method: "GET",
        headers: !token ? { 
            'Content-Type': 'application/json'
         } : {
              'Content-Type': 'application/json',
               'Authorization': `Bearer ${token}` 
            },
    });
    const data = await response.json();
    this.setState({ admin: data});
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
 

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">Simply Watered</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">

                <NavItem>
                <NavLink tag={Link} className="text-dark" to="/regiongroups">Переглянути ділянки</NavLink>
                </NavItem>
                {this.state.admin && 
                <NavItem>
                <NavLink tag={Link} className="text-dark" to="/users">Переглянути користувачів</NavLink>
                </NavItem>
                }
                <LoginMenu>
                </LoginMenu>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
