import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService';
import {DeviceAdding} from './DeviceAdding';




export class DeviceList extends Component{
 
    constructor(props){
        super(props)
        this.state={
            devices: [],
            region: {RegionName:"",
                          RegionDescription: ""
                         },
            path: this.props.location.pathname
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    onRemoveDevice= async(device)=>{


       
    if (device) {

        let token = await authService.getAccessToken();
        let response = await fetch(`api${this.state.path}/${device.deviceId}`, {
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

    }
    }
    componentDidMount() {
        this.loadData();
    }

    render(){
        //const state= this.props.location.state;
        let devices=this.state.devices;
        let regionName=this.state.region.regionName;
        return(
            <>
            
            <h2 className="text-center">Пристрої ділянки "{regionName}"</h2>
            <hr />

            <DeviceAdding path={this.state.path} loadData={this.loadData.bind(this)} goBack={this.goBack}></DeviceAdding>
           
            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                        <th>Серійний номер</th>
                        <th colSpan="2">Дії</th>
                    </tr>
                </thead>
                <tbody>

                    {devices.map(device => <tr key={device.deviceId}>
                        <td>{device.deviceType.deviceName}</td>
                        <td>{device.deviceType.deviceDescription}</td>
                        <td>{device.serialNumber}</td>
                        <td><button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveDevice(device); } }>Видалити</button></td>
                        <td><Link className="btn btn-outline-primary" role="button"
                        to=
                        {{
                        pathname: `${this.state.path}/${device.deviceId}/readings`,
                        state: { deviceId: device.deviceId }
                        }}

                        >
                        Переглянути показники пристрою
                        </Link>
                        </td>
                    </tr>


                    )}
                </tbody>
            </table>
         
            </>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
        
        
        const response = await fetch(`api${this.state.path}`, {
            method: "GET",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },
        });
        const data = await response.json();
        this.setState({ devices: data.devices, region:data.region, loading: false });
    }
}

