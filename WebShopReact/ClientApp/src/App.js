import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Products } from './components/Products/Index';
import { ShoppingCart } from './components/ShoppingCart/Index';
import { SwitchDatabase } from './components/SwitchDatabase/SwitchDatabase';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
        <Layout>
            <Route exact path='/' component={Products} />
            <Route exact path='/ShoppingCart' component={ShoppingCart} />
            <Route exact path='/SwitchDatabase' component={SwitchDatabase} />
      </Layout>
    );
  }
}
