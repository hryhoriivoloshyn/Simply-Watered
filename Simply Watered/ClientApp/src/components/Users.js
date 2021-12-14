import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'
import {withRouter} from "react-router-dom"
import { AccountCreation } from './AccountCreation';

export class UserList extends Component {
    static displayName = UserList.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            loading: true,
            message: '',
            pathname: this.props.location.pathname
        }
    }


    onClick(user){
            this.onRemoveUser(user);
    }

    onRemoveUser= async(user)=>{

      
        let userId= user.id;
        if (user) {

            let token = await authService.getAccessToken();
            console.log(token);
            let response = await fetch(`api${this.state.pathname}/${userId}`, {
                method: "DELETE",
                headers: !token ? {
                    'Content-Type': 'application/json'
                } : {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                
            }).then(async ()=>{
                await this.loadData();
            });
            console.log(response);
        }
    }

    componentDidMount() {
        this.loadData();
    }


    render() {

        let users=this.state.users;
        return (
            <>
            <h2 className="text-center">Користувачі</h2>
            <hr />
 
            <AccountCreation pathname={this.state.pathname} loadData={this.loadData.bind(this)}></AccountCreation>
            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Поштова скринька</th>
                        <th colSpan="3">Дії</th>
                    </tr>
                </thead>
                <tbody>

                    {users.map(user => <tr key={user.id}>
                        <td>{user.email}</td>
                       
                        <td><button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveUser(user); } }>Видалити</button></td>

                    </tr>


                    )}
                </tbody>
            </table></>
            );
    }

    //Загрузка данных
    async loadData() {
        const token = await authService.getAccessToken();
        const response = await fetch(`api${this.state.pathname}`, {
            headers: !token ? { 'Content-Type': 'application/json' } : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }


   

}


