import { useLocation } from 'react-router-dom'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'

export class ReadingList extends Component{
 
    constructor(props){
        super(props)
        this.state={
            readings: [],
            device: {
                    serialNumber: "",
                    deviceType: {
                        deviceName: ""
                    }
                    },
            deviceId: this.props.location.state.deviceId
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    onRemoveReading= async(reading)=>{

        console.log("Проверка группы для удаления");
        let deletemodel={id:reading.readingId};
    if (reading) {
        console.log("Удаление");
        let token = await authService.getAccessToken();
        console.log(token);
        let response = await fetch('readings/delete', {
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
        console.log(response);
    }
    }
    componentDidMount() {
        this.loadData();
    }

    render(){
        //const state= this.props.location.state;
        let readings=this.state.readings;
        let deviceName=this.state.device.deviceType.deviceName;

        return(
            <>
          
            <h2 className="text-center">Показники пристрою "{deviceName}"</h2>
            <hr />
            
            <h5 className="mt-1">Серійний номер пристрою: {this.state.device.serialNumber}</h5>
            <table className='table table-striped text-center my-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Дата запису</th>
                        <th>Час запису</th>
                        <th>Температура</th>
                        <th>Вологість</th>

                    </tr>
                </thead>
                <tbody>

                    {readings.map(reading => <tr key={reading.readingId}>
                        <td >{reading.normalizedDate}</td>
                        <td>{reading.normalizedTime}</td>
                        <td>{reading.readingTemp}&#8451;</td>
                        <td>{reading.readingHumidity}%</td>
            
                       
                    </tr>


                    )}
                </tbody>
            </table>
                <button className="btn btn-secondary btn-lg ml-1" onClick={this.goBack}>Повернутися</button>
            </>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
        let deviceIdModel={id:this.state.deviceId}
        console.log(token);
        const response = await fetch('readings/load', {
            method: "POST",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },
                body: JSON.stringify(deviceIdModel)
        });
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ readings: data.readings, device:data.device, loading: false });
    }
}