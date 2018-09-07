import React, { Component } from 'react'
import AuthService from '../../services/AuthService';

export class Logout extends Component {
    constructor(props) {
        super(props);
        this.Auth = new AuthService();
        this.state = {
            token: this.Auth.getToken()
        }
    }

    componentWillMount() {
        if (this.state.token == null) {
            this.props.history.replace('/Login');
        }
    }

    render() {
        this.Auth.logout();
        return (<div>
            <p>User logged out succesfully!</p>
            </div>
            )
    }

}