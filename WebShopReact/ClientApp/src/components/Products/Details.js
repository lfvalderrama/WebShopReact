import React from 'react';
import AuthService from '../../services/AuthService';

export class ProductDetails extends React.Component {
    constructor(props) {
        super(props);
        this.Auth = new AuthService();
        this.state = {
            product: null,
            loading: true,
            addedToCart: false,
            inputValue: 0,
            addedToCartUnautorized: false,
            token: this.Auth.getToken()
        };

        this.updateInputValue = this.updateInputValue.bind(this);
        fetch('api/products/' + this.props.productId, {
            headers: new Headers({
                'Authorization': this.state.token
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ product: data, loading: false })
            })
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderDetails(this.state.product);
        return (<React.Fragment>
            <h1>product Details</h1>
            {contents}
        </React.Fragment>);
    }

    handleAddToCart(product) {
        let oldQuantity = product.quantity;
        product.quantity = this.state.inputValue;
        if (this.state.inputValue > 0) {
            fetch("api/shoppingCart/",
                {
                    method: "post",
                    headers: new Headers({
                        'Authorization': this.state.token,
                        'Content-Type': 'application/json' }),
                    body: JSON.stringify(product)
                })
                .then(data => {
                    if (data.status != 401) {
                        product.quantity = oldQuantity;
                        this.setState({
                            addedToCart: true,
                            product: product,
                            addedToCartUnautorized: false
                        });
                    }
                    else {
                        this.setState({
                            addedToCartUnautorized: true
                        })
                    }
                })
        }
    }

    updateInputValue(event) {
        this.setState({
            inputValue: event.target.value
        });
    }

    renderDetails(product) {
        return (
            <div className="details">
                <label>Name</label>
                <div>{product.name}</div>
                <label>Description</label>
                <div>{product.description}</div>
                <label>Price</label>
                <div>{product.price}</div>
                <label>Stock</label>
                <div>{product.quantity}</div>
                <br />

                <div>
                    <label>Quantity</label>
                    <br/>
                    <input id='CartQuantity' name='CartQuantity' type="number" defaultValue="0" onChange={this.updateInputValue} />
                    <br/>
                <button className="action" onClick={() => this.handleAddToCart(product)}>Add To Cart</button>
                <br/>
                    {this.state.addedToCart == true ? <div><br /><p> Item Added to Cart </p> </div> : null}
                    {this.state.addedToCartUnautorized == true ? <div><br /><p> Please login to add items to your shopping cart </p> </div> : null}
                </div>
            </div>);
    }
}