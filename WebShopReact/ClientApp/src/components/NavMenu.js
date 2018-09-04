import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  displayName = NavMenu.name

  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={'/'}>WebShopReact</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={'/'} exact>
              <NavItem>
                <Glyphicon glyph='th-list' /> Products
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/shoppingCart'}>
              <NavItem>
                <Glyphicon glyph='shopping-cart' /> Shopping Cart
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/user'}>
              <NavItem>
                <Glyphicon glyph='user' /> User
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/switchDatabase'}>
                <NavItem>
                    <Glyphicon glyph='floppy-save' /> Switch Database
                </NavItem>
            </LinkContainer>

          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
