export default class AuthService {
    constructor() {
        this.login = this.login.bind(this)
        this.setToken = this.setToken.bind(this)
    }

    login(formInfo) {
        console.log(formInfo);
        return fetch('api/customers/authenticate',
            {
                method: 'post',
                headers: { 'Content-Type': 'application/json' },
                body: formInfo
            })
            .then(response => response.json())
            .then(data => {
                if (data.error != null) {
                    this.setState({
                        error: true
                    })
                }
                else {
                    this.setToken(data);
                }
                return data;
            })
    }

    setToken(data) {
        localStorage.setItem('token', data.token)
        localStorage.setItem('customerId', data.customerId)
    }

    getToken() {
        if (localStorage.getItem('token') == null) return null
        return 'Bearer ' + localStorage.getItem('token')
    }

    getCustomerId() {
        return localStorage.getItem('customerId')
    }

    logout() {
        // Clear user token and profile data from localStorage
        localStorage.removeItem('token');
        localStorage.removeItem('customerId')
    }

}