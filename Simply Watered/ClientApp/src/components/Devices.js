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
            regionId: this.props.location.state.regionId
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    onRemoveDevice= async(device)=>{


        let deletemodel={id:device.deviceId};
    if (device) {

        let token = await authService.getAccessToken();
        let response = await fetch('devices/delete', {
            method: "POST",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(deletemodel)
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

            <DeviceAdding regionId={this.state.regionId} loadData={this.loadData.bind(this)} goBack={this.goBack}></DeviceAdding>
           
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
                        pathname: '/readings',
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
        let regionIdModel={id:this.state.regionId}
        
        const response = await fetch('devices/load', {
            method: "POST",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },
                body: JSON.stringify(regionIdModel)
        });
        const data = await response.json();
        this.setState({ devices: data.devices, region:data.region, loading: false });
    }
}

