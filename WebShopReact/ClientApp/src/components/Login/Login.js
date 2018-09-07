import React, { Component } from 'react'
import AuthService from '../../services/AuthService';

export class Login extends Component {
    constructor(props) {
        super(props);
        this.emailChanged = this.emailChanged.bind(this);
        this.passChanged = this.passChanged.bind(this);
        this.Auth = new AuthService();
        this.state = {
             email: '',
             password: '',
            error: false,
            token: this.Auth.getToken()
        }
    }

    componentWillMount() {
        if (this.state.token != null) {
            this.props.history.replace('/user');
        }
    }

    handleSave(e) {
        e.preventDefault()
        if (this.state.password != '' && this.state.email != '') {
            let form = Element = document.querySelector('#frmLogin')
            let formInfo = JSON.stringify(this.formToJson(form));
            this.Auth.login(formInfo)
                .then(res => {
                    if (res.error != null) {
                        this.setState({
                            error: true
                        })
                    }
                    else {
                            this.props.history.replace('/user');
                        }
                })            
        }
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.customer);
        return (<div>
            <h1>Login</h1>
            {contents}
            <br/>
            <div>
                {this.state.error == true ? <p> The Email or password are incorrect </p> : null}
            </div>
        </div>)
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