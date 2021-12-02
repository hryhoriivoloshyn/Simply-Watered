import { useLocation } from 'react-router-dom'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'

export class IrrigationHistory extends Component{
 
    constructor(props){
        super(props)
        this.state={
            history: [],
            device: {
                    serialNumber: "",
                    deviceType: {
                        deviceName: ""
                    }
                    },
            pathname: this.props.location.pathname
        }
    }

    goBack =()=>{
        this.props.history.goBack();
    }

  
    componentDidMount() {
        this.loadData();
    }

    render(){
        //const state= this.props.location.state;
        let history=this.state.history;
        let deviceName=this.state.device.deviceType.deviceName;

        return(
            <>
          
            <h2 className="text-center">Історія зрошення пристрою "{deviceName}"</h2>
            <hr />
            
            <h5 className="mt-1">Серійний номер пристрою: {this.state.device.serialNumber}</h5>
            <table className='table table-striped text-center my-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Дата зрошення</th>
                        <th>Час початку</th>
                        <th>Час завершення</th>
                        <th>Початкова температура</th>
                        <th>Кінцева температура</th>
                        <th>Початкова вологість</th>
                        <th>Кінцева вологість</th>

                    </tr>
                </thead>
                <tbody>

                    {history.map(entry => <tr key={entry.id}>
                        <td >{entry.normalizedStartdDate}</td>
                        <td>{entry.normalizedStartTime}</td>
                        <td>{entry.normalizedEndTime}</td>
                        <td>{entry.readingStartTemp}&#8451;</td>
                        <td>{entry.readingEndTemp}&#8451;</td>
                        <td>{entry.readingStartHumidity}%</td>
                        <td>{entry.readingEndHumidity}%</td>
            
                       
                    </tr>


                    )}
                </tbody>
            </table>
                <button className="btn btn-secondary btn-lg ml-1" onClick={this.goBack}>Повернутися</button>
            </>
            );
    }

    async loadData() {
        let token = await authService.getAccessToken();
        
        console.log(token);
        let response = await fetch(`api${this.state.pathname}`, {
            method: "GET",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },
              
        });
        console.log(response);
        let data = await response.json();
        console.log(data);
        this.setState({ history: data.irrigationHistories, device:data.device, loading: false });
    }
}