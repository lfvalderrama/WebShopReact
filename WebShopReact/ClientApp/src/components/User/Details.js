import React from 'react';
import Modal from 'react-modal';
import { CustomerCreateEdit } from './CreateEdit';
import AuthService from '../../services/AuthService';

Modal.setAppElement('#root')

export class customer extends React.Component {
    constructor(props) {
        super(props);
        this.closeModal = this.closeModal.bind(this);
        this.Auth = new AuthService();
        this.fetchUser = this.fetchUser.bind(this);
        this.state = {
            customer: null,
            loading: true,
            customerId: null,
            customerLoaded: false,
            showCreate: false,
            showUpdate: false,
            showModal: false,
            token: this.Auth.getToken()
        };
    }

    componentWillMount() {
        if (this.state.token == null) {
            this.setState({
                showCreate: true
            })
        }
        else {
            this.fetchUser();
        }
    }

    fetchUser() {
        fetch('api/customers/details',  {
            method: 'get',
            headers: new Headers({
                'Authorization': this.state.token
            })
        })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    customer: data,
                    loading: false,
                    customerLoaded: true,
                    customerId: data.customerId,
                    customerLoaded: true
                })
            })    
    }

    renderPopup() {
        if (!this.state.showCreate && !this.state.showUpdate)
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
            return <CustomerCreateEdit customerId={null} dbaction="create"
                onSave={this.handlePopupSave.bind(this)} />
        }

        if (this.state.showUpdate) {
            return <CustomerCreateEdit token={this.state.token} dbaction="edit"
                onSave={this.handlePopupSave.bind(this)} />
        }
    }

    closeModal() {
        if (this.customerLoaded)
            this.setState({  showCreate: false, showUpdate: false, showModal: false })
    }

    handlePopupSave(success) {
        if (success) {
            if (this.state.showCreate == true) {
                alert("User Registered!");
                this.props.history.replace('/login');
            }
            else {
                this.setState({
                    showUpdate: false
                })
                this.fetchUser();
            }
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