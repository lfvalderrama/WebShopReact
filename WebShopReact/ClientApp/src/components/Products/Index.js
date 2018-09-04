import React, { Component } from 'react';
import Modal from 'react-modal';
import { ProductCreateEdit } from './CreateEdit'
import { ProductDetails } from './Details'

Modal.setAppElement('#root')

export class Products extends Component {
    constructor(props) {
        super(props);
        this.state = {
            product: [],
            loading: true,
            showCreate: false,
            showDetails: false,
            showUpdate: false,
            showModal: true,
            activeId: 0
        };
        fetch('api/products')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    product: data,
                    loading: false,
                    showCreate: false,
                    showDetails: false,
                    showUpdate: false,
                    showModal: true,
                    activeId: 0
                });
            });
        this.closeModal = this.closeModal.bind(this);
    }

    fetchIndex() {
        fetch('api/products')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    product: data,
                    loading: false,
                    showCreate: false,
                    showDetails: false,
                    showUpdate: false,
                    showModal: true,
                    activeId: 0
                });
            });
    }

    handleCreate() {
        this.setState({ showCreate: true, showDetails: false, showUpdate: false })
    }

    handleUpdate(id) {
        this.setState({ showUpdate: true, showDetails: false, showCreate: false, activeId: id })
    }

    handleDetails(id) {
        this.setState({ showCreate: false, showDetails: true, showUpdate: false, activeId: id })
    }

    handleDelete(id) {
        if (!window.confirm("Are you sure to delete this item?"))
            return
        fetch('api/products/' + id, { method: 'delete' })
            .then(data => {
                this.setState({
                    product: this.state.product.filter((rec) => {
                        return rec.productId !== id;
                    })

                })
            })

    }

    renderTable(product) {
        return (<table className='table'>
            <thead>
                <tr>
                    <th></th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Price</th>
                    <th>Stock</th>
                </tr>
            </thead>
            <tbody>
                {product.map(item =>
                    <tr key={item.productId}>
                        <td>
                            <button className="action" onClick={() => this.handleDelete(item.productId)}>X</button>
                            <button className="action" onClick={() => this.handleUpdate(item.productId)}>Update</button>
                            <button className="action" onClick={(productId) => this.handleDetails(item.productId)}>Details</button>
                        </td>
                        <td>{item.name}</td>
                        <td>{item.description}</td>
                        <td>{item.price}</td>
                        <td>{item.quantity}</td>
                    </tr>
                )}
            </tbody>
        </table>);
    }

    renderPopup() {
        if (!this.state.showCreate && !this.state.showDetails && !this.state.showUpdate)
            return
        return (<Modal
            isOpen={true}
            contentLabel="Crawl"
            onRequestClose={this.closeModal}>
            <button onClick={this.closeModal} className="action" title="close">X</button>
            {this.renderPopupContent()}
        </Modal>);
    }

    renderPopupContent() {
        if (this.state.showCreate) {
            console.log(this.state.showCreate);
            return <ProductCreateEdit productId={null} dbaction="create"
                onSave={this.handlePopupSave.bind(this)} />
        }

        if (this.state.showDetails) {
            return <div><ProductDetails productId={this.state.activeId} /></div>
        }
        if (this.state.showUpdate) {
            return <ProductCreateEdit productId={this.state.activeId} dbaction="edit"
                onSave={this.handlePopupSave.bind(this)} />
        }
    }

    closeModal() {
        this.setState({ showDetails: false, showCreate: false, showUpdate: false, showModal: false })
    }

    handlePopupSave(success) {
        if (success) {
            this.setState({ showCreate: false, showUpdate: false })
            this.fetchIndex()
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.product);

        return (<div>
            <h1>Product</h1>
            <button className="action" onClick={this.handleCreate.bind(this)}>Create</button>
            {contents}
            {this.renderPopup()}
        </div>);

    }
}