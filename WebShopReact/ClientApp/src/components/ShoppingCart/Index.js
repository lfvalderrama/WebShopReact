import React, { Component } from 'react';
import AuthService from '../../services/AuthService';


export class ShoppingCart extends Component {
    constructor(props) {
        super(props);
        this.Auth = new AuthService();
        this.state = {
            shoppingCart: [],
            loading: true,
            token: this.Auth.getToken()

        };
        if (this.state.token == null) {
            this.props.history.replace('/login');
        }
        else {
            fetch('api/shoppingCart',
                {
                    headers: new Headers({
                        'Authorization': this.state.token
                    })
                }
            )
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    this.setState({
                        shoppingCart: data,
                        loading: false
                    });
                });
        }
    }

    handleDelete(id) {
        if (!window.confirm("Are you sure to delete this item?"))
            return
        fetch('api/shoppingCart/' + id, { method: 'delete' })
            .then(data => {
                this.setState({
                    shoppingCart: this.state.shoppingCart.filter((rec) => {
                        return rec.productId !== id;
                    })
                
                })
            })
    }

    renderTable(shoppingCart) {
        console.log(shoppingCart);        
        return (<table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Sub Total</th>
                </tr>
            </thead>
            <tbody>
                {shoppingCart.map(item =>
                    <tr key={item.productId}>
                        <td>
                            <button className="action" onClick={() => this.handleDelete(item.productId)}>X</button>
                        </td>
                        <td>{item.name}</td>
                        <td>{item.description}</td>
                        <td>{item.price}</td>
                        <td>{item.quantity}</td>
                        <td>{item.price * item.quantity}</td>
                    </tr>                    
                )}
            </tbody>
        </table>
        );
    } 

    getTotal(shoppingCart) {
        let total = 0;
        for (var i = 0, _len = shoppingCart.length; i < _len; i++) {
            total += shoppingCart[i].price * shoppingCart[i].quantity;
        }
        return (
            <div>
                <h3> TOTAL: </h3>
                <p> {total} </p>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.shoppingCart);

        return (<div>
            <h1>shoppingCart</h1>
            {contents}
            {this.getTotal(this.state.shoppingCart)}
        </div>);

    }
}