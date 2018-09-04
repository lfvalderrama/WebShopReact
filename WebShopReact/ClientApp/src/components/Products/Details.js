import React from 'react';

export class ProductDetails extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            product: null,
            loading: true,
            addedToCart: false,
            inputValue: 0
        };

        this.updateInputValue = this.updateInputValue.bind(this);
        fetch('api/products/' + this.props.productId, { method: 'get' })
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
        if (this.state.inputValue > 0) {
            product.quantity = this.state.inputValue;
            fetch("api/shoppingCart/",
                {
                    method: "post",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(product)
                })
                .then(data => {
                    this.setState({ addedToCart: true });
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
                <label>Quantity</label>
                <div>{product.quantity}</div>
                <br />

                <div>
                    <input id='CartQuantity' name='CartQuantity' type="number" defaultValue="0" onChange={this.updateInputValue}/>
                <button className="action" onClick={() => this.handleAddToCart(product)}>Add To Cart</button>
                <br/>
                    {this.state.addedToCart == true ? <div><br /><p> Item On Cart </p> </div> : null}
                </div>
            </div>);
    }
}