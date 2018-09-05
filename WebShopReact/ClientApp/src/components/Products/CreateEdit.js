import React, { Component } from 'react'

export class ProductCreateEdit extends Component {
    constructor(props) {
        super(props);
        if (this.props.dbaction == "edit") {
            this.state = { product: null, loading: true, save: false }
            fetch('api/products/' + this.props.productId, { method: 'get' })
                .then(response => response.json())
                .then(data => {
                    this.setState({ product: data, loading: false })
                })
        } else {
            this.state = { product: null, loading: false, save: false }
        }
    }

    handleSave(e) {
        e.preventDefault()
        let meth = (this.props.dbaction == "edit" ? "put" : "post")
        let form = Element = document.querySelector('#frmCreateEdit')
        let url = (this.props.dbaction == "edit" ? 'api/Products/' + this.props.productId : 'api/Products/')

        fetch(url,
            {
                method: meth,
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(this.formToJson(form))
            })
            .then(data => {
                this.setState({ save: false });
                this.props.onSave(true);
            })
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.product);
        return (<div>
            <h1>{this.props.dbaction == "edit" ? "Edit product" : "Create product"}</h1>
            {contents}
        </div>)
    }

    renderForm(item) {
        console.log("this.props.dbaction " + this.props.dbaction)
        if (this.props.dbaction != "edit")
            item = { Name: '', Description: '', Price: 0, Stock: 0 }
        return <form id='frmCreateEdit'>
            {this.props.dbaction == 'edit' ? <input id='productId' name='productId' type='hidden' value={item.productId} />
                : null}
            <label>Name</label><br/>
            <input id='Name' name='Name' type="text" defaultValue={item.name != null ? (item.name + '') : ''} />
            <br/>
            <label>Description</label><br />
            <input id='Description' name='Description' type="text" defaultValue={item.description != null ? (item.description + '') : ''} />
            <br />
            <label>Price</label><br />
            <input id='Price' name='Price' type="number" defaultValue={item.price != null ? (item.price + '') : ''} />
            <br />
            <label>Stock</label><br />
            <input id='Quantity' name='Quantity' type="number" defaultValue={item.quantity != null ? (item.quantity + '') : ''} />
            <br />
            <button onClick={this.handleSave.bind(this)}>submit</button>
        </form>
    }

    isValidElement = element => {
        return element.name && element.value;
    };

    isValidValue = element => {
        return (['checkbox', 'radio'].indexOf(element.type) == -1 || element.checked);
    };

    formToJson = elements => [].reduce.call(elements, (data, element) => {
        console.log('formToJson()', element)
        if (this.isValidElement(element) && this.isValidValue(element)) {
            data[element.name] = element.value;
        }
        return data;
    }, {});
}