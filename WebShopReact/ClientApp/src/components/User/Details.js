import React from 'react';
import Modal from 'react-modal';
import { CustomerCreateEdit } from './CreateEdit'

Modal.setAppElement('#root')

export class customer extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            customer: null,
            loading: true,
            customerId: 1,
            customerLoaded: false,
            showCreate: false,
            showUpdate: false,
            showModal: false,
            activeId: 1
        };
        this.closeModal = this.closeModal.bind(this);

        fetch('api/customers/' + this.state.customerId, { method: 'get' })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    customer: data,
                    loading: false,
                    customerLoaded: true
                })
            })
            .catch(error => {  
                this.setState({
                    showCreate: true
                })
            })
    }

    renderPopup() {
        if (!this.state.showCreate && !this.state.showUpdate)
            return
        console.log("modal");
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
            return <CustomerCreateEdit customerId={null} dbaction="create"
                onSave={this.handlePopupSave.bind(this)} />
        }

        if (this.state.showUpdate) {
            return <CustomerCreateEdit customerId={this.state.activeId} dbaction="edit"
                onSave={this.handlePopupSave.bind(this)} />
        }
    }

    closeModal() {
        if (this.customerLoaded)
            this.setState({  showCreate: false, showUpdate: false, showModal: false })
    }

    handlePopupSave(success) {
        if (success) {
            fetch('api/customers/' + this.state.customerId, { method: 'get' })
                .then(response => response.json())
                .then(data => {
                    this.setState({
                        customer: data,
                        loading: false,
                        customerLoaded: true,
                        showCreate: false,
                        showUpdate: false 
                    })
                })
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderDetails(this.state.customer);
        return (<div>
            <h1>Customer Details</h1>
            {contents}
            {this.renderPopup()}
        </div>);
    }

    handleUpdate(id) {
        this.setState({ showUpdate: true, showCreate: false, activeId: id })
    }

    renderDetails(customer) {
        return (
            <div className="details">
                <label>First Name</label>
                <div>{customer.firstName}</div>
                <label>Last Name</label>
                <div>{customer.lastName}</div>
                <label>email</label>
                <div>{customer.email}</div>
                <label>Age</label>
                <div>{customer.age}</div>
                <br/>
                <button className="action" onClick={() => this.handleUpdate(customer.customerId)}>Update</button>
            </div>);
    }
}