import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'
import {withRouter} from "react-router-dom"


export class AdminDevicesList extends Component {
    static displayName = AdminDevicesList.name;

    constructor(props) {
        super(props);
        this.state = {
            devices: [],
            loading: true,
            message: '',
            pathname: this.props.location.pathname
        }
    }


    onClick(device){
            this.onChangeDeviceState(device);
    }

    onChangeDeviceState= async(device)=>{

      
        let deviceId= device.deviceId;
        if (device) {

            let token = await authService.getAccessToken();
            console.log(token);
            let response = await fetch(`api/AdminDevices/${deviceId}`, {
                method: "PUT",
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

        let devices=this.state.devices;
        return (
            <>
            <h2 className="text-center">Всі пристрої</h2>
            <hr />


            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Серійний номер</th>
                        <th colSpan="3">Дії</th>
                    </tr>
                </thead>
                <tbody>

                    {devices.map(device => <tr key={device.id}>
                        <td>{device.deviceType.deviceName}</td>
                        <td>{device.serialNumber}</td>
                        
                       {device.active
                            ? <td><button className="btn btn-danger" onClick={async () => { await this.onChangeDeviceState(device); } }>Деактивувати</button></td>
                            : <td><button className="btn btn-success" onClick={async () => { await this.onChangeDeviceState(device); } }>Активувати</button></td>
                        }

                    </tr>


                    )}
                </tbody>
            </table></>
            );
    }

    //Загрузка данных
    async loadData() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/admindevices', {
            headers: !token ? { 'Content-Type': 'application/json' } : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ devices: data, loading: false });
    }


   

}


