import React, { Component } from 'react'

export class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
             email: '',
             password: '',
            error: false,
            token: ''
        }
        this.emailChanged = this.emailChanged.bind(this);
        this.passChanged = this.passChanged.bind(this);
    }

    handleSave(e) {
        e.preventDefault()
        if (this.state.password != '' && this.state.email != '') {
            let form = Element = document.querySelector('#frmLogin')
            fetch('api/customers/authenticate',
                {
                    method: 'post',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(this.formToJson(form))
                })
                .then(response => response.json())
                .then(data => {
                    if (data.error != null) {
                        this.setState({
                            error: true
                        })
                    }
                    else {
                        this.setState({
                            token: data.token
                        })
                        this.handleToken();
                    }
                })
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.customer);
        return (<div>
            <h1>{this.props.dbaction == "edit" ? "Edit User" : "Create User"}</h1>
            {contents}
            <br/>
            <div>
                {this.state.error == true ? <p> The Email or password are incorrect </p> : null}
            </div>
        </div>)
    }

    handleToken() {
        console.log("token " + this.state.token);
    }

    emailChanged(evt) {
        this.setState({
            email: evt.target.value
        })
    }

    passChanged(evt) {
        this.setState({
            password: evt.target.value
        })        
    }

    renderForm(item) {
        return (<form id='frmLogin'>            
                <label>Email</label><br />
            <input id='Email' name='Email' type="text" defaultValue='' onChange={this.emailChanged} />
                <br /> <br />
                <label>Password</label><br />
            <input id='Password' name='Password' type="password" defaultValue='' onChange={this.passChanged} />
                <br /> <br />
                <button onClick={this.handleSave.bind(this)}>submit</button>
            </form>
        )

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