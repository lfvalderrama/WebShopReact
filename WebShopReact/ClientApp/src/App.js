import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Products } from './components/Products/Index';
import { ShoppingCart } from './components/ShoppingCart/Index';
import { SwitchDatabase } from './components/SwitchDatabase/SwitchDatabase';
import { customer } from './components/User/Details';
import { Login } from './components/Login/Login';
import { Logout } from './components/Logout/Logout';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
        <Layout>
            <Route exact path='/' component={Products} />
            <Route exact path='/ShoppingCart' component={ShoppingCart} />
            <Route exact path='/SwitchDatabase' component={SwitchDatabase} />
            <Route exact path='/User' component={customer} />
            <Route exact path='/Login' component={Login} />
            <Route exact path='/logout' component={Logout} />
      </Layout>
    );
  }
}
