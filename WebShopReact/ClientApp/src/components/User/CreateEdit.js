﻿import React, { Component } from 'react'

export class CustomerCreateEdit extends Component {
    constructor(props) {
        super(props);
        if (this.props.dbaction == "edit") {
            this.state = {
                customer: null,
                loading: true,
            }
            fetch('api/customers/' + this.props.customerId, {
                method: 'get',
                headers: new Headers({
                    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwiRGF0YWJhc2UiOiJTcWxTZXJ2ZXIiLCJuYmYiOjE1MzYyNzIyOTYsImV4cCI6MTUzNjg3NzA5NiwiaWF0IjoxNTM2MjcyMjk2fQ.dKOhl93FsnB1YNbvHd3-M3IAPKOEu0Yz0YR-JmN12a8'
                })
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ customer: data, loading: false })
                })
        } else {
            this.state = { customer: null, loading: false }
        }
    }

    handleSave(e) {
        e.preventDefault()
        let meth = (this.props.dbaction == "edit" ? "put" : "post")
        let form = Element = document.querySelector('#frmCreateEdit')
        let url = (this.props.dbaction == "edit" ? 'api/customers/' + this.props.customerId : 'api/customers/')
        fetch(url,
            {
                method: meth,
                headers: new Headers({
                    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwiRGF0YWJhc2UiOiJTcWxTZXJ2ZXIiLCJuYmYiOjE1MzYyNzIyOTYsImV4cCI6MTUzNjg3NzA5NiwiaWF0IjoxNTM2MjcyMjk2fQ.dKOhl93FsnB1YNbvHd3-M3IAPKOEu0Yz0YR-JmN12a8',
                    'Content-Type': 'application/json' }),
                body: JSON.stringify(this.formToJson(form))
            })
            .then(data => {
                this.props.onSave(true);
            })
    }    

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.customer);
        return (<div>
            <h1>{this.props.dbaction == "edit" ? "Edit User" : "Create User"}</h1>
            {contents}
        </div>)
    }


    renderPassword(){
        return (<div>
            <label>Password</label> <br />
            <input id='Password' name='Password' type="password" defaultValue='' />
            < br /> <br />
            </div>
           )
    }

    renderForm(item) {
        let content = null;
        if (this.props.dbaction != "edit") {
            item = { FirstName: '', LastName: '', Email: 0, Age: 0 }
            content = this.renderPassword();
        }
        return <form id='frmCreateEdit'>
            {this.props.dbaction == 'edit' ? <input id='customerId' name='customerId' type='hidden' value={item.customerId} />
                : null}
            <label>First Name</label><br/>
            <input id='FirstName' name='FirstName' type="text" defaultValue={item.firstName != null ? (item.firstName + '') : ''} />
            <br /> <br />
            <label>Last Name</label><br />
            <input id='LastName' name='LastName' type="text" defaultValue={item.lastName != null ? (item.lastName + '') : ''} />
            <br /> <br />
            <label>Email</label><br />
            <input id='Email' name='Email' type="text" defaultValue={item.email != null ? (item.email + '') : ''} />
            <br /> <br />
            <label>Age</label><br />
            <input id='Age' name='Age' type="number" defaultValue={item.age != null ? (item.age + '') : ''} />
            <br /> <br />
            {content}
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
        if (this.isValidElement(element) && this.isValidValue(element)) {
            data[element.name] = element.value;
        }
        return data;
    }, {});
}