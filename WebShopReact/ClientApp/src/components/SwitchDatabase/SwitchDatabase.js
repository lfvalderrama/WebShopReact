import React, { Component } from 'react'

export class SwitchDatabase extends Component {
    constructor(props) {
        super(props);
        this.state = {
            connections: [],
            current: "",
            loading: true,
            save: false
        };
        this.handleOptionChange = this.handleOptionChange.bind(this);
        fetch('api/connection', {
                headers: new Headers({
                    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwiRGF0YWJhc2UiOiJTcWxTZXJ2ZXIiLCJuYmYiOjE1MzYyNzIyOTYsImV4cCI6MTUzNjg3NzA5NiwiaWF0IjoxNTM2MjcyMjk2fQ.dKOhl93FsnB1YNbvHd3-M3IAPKOEu0Yz0YR-JmN12a8'
                })
            })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    connections: data.connection,
                    current: data.current[0],
                    loading: false
                });
            });
    }

    handleSave(e) {
        e.preventDefault()        
        let form = Element = document.querySelector('#frmSwitchDatabase')
        console.log(JSON.stringify(this.formToJson(form)));
        fetch("/api/connection",
            {
                method: "post",
                headers: new Headers({
                    'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwIiwiRGF0YWJhc2UiOiJTcWxTZXJ2ZXIiLCJuYmYiOjE1MzYyNzIyOTYsImV4cCI6MTUzNjg3NzA5NiwiaWF0IjoxNTM2MjcyMjk2fQ.dKOhl93FsnB1YNbvHd3-M3IAPKOEu0Yz0YR-JmN12a8',
                    'Content-Type': 'application/json' }),
                body: JSON.stringify(this.formToJson(form))
            })
            .then(response => {
                if (response.ok) {
                    this.setState({ save: true, })
                };
                return response.json();
            })
            .then(data => {
                console.log(data);
            })
    }
    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderForm(this.state.connections);
        return (<div>
            {contents}
            {this.state.save == true ? <div><br /><p> DB Context Changed succesfully </p> </div> : null}
        </div>)
    }

    handleOptionChange(changeEvent) {
        if (this.state.current != changeEvent.target.value) {
            this.setState({
                current: changeEvent.target.value
            });
        }
    }

    renderForm(item) {        
        return <form id='frmSwitchDatabase'>
            {this.state.connections.map( item => {
                return (<div className="radio" key={item}>
                   <label>
                       <input type="radio" value={item} name="connection" checked={item == this.state.current} onChange={this.handleOptionChange} />
                        {item}
                    </label>
                </div>)
                })      
            }
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