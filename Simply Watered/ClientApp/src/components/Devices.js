import { useLocation } from 'react-router-dom'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import { Redirect } from "react-router-dom";
import authService from './api-authorization/AuthorizeService'

function handleErrors(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}


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



    onRemoveDevice= async(device)=>{

        console.log("Проверка группы для удаления");
        let deletemodel={id:device.deviceId};
    if (device) {
        console.log("Удаление");
        let token = await authService.getAccessToken();
        console.log(token);
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
        console.log(response);
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
            
            <h2>Назва області: {regionName}</h2>
            <DeviceAdding regionId={this.state.regionId} loadData={this.loadData.bind(this)}></DeviceAdding>
        
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                    </tr>
                </thead>
                <tbody>

                    {devices.map(device => <tr key={device.deviceId}>
                        <td>{device.deviceName}</td>
                        <td>{device.deviceDescription}</td>
                        <td><button onClick={async () => { await this.onRemoveDevice(device); } }>Видалити</button></td>
                    </tr>


                    )}
                </tbody>
            </table></>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
        let regionIdModel={id:this.state.regionId}
        console.log(token);
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
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ devices: data.devices, region:data.region, loading: false });
    }
}


export class DeviceAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            regionId: this.props.regionId,
            errorServerMessage: '',
            fields: {},
            errors: {}
        };
    }
    
    handleValidation = async () => {
        let fields = this.state.fields;
        let errors = {};
        let formIsValid = true;
    
        //Name
        if (!fields["serialNumber"]) {
          formIsValid = false;
          errors["serialNumber"] = "Для додавання пристрою введіть його серійний номер.";
        }
    
        this.setState({ errors: errors });
        return formIsValid;
      }

    deviceSubmit= async (e) => {
        
        e.preventDefault()
        if  (await this.handleValidation()) {
            await this.onCreateDevice();
        } 
    }

    handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
    }

    onCreateDevice = async () => {
        console.log("кнока нажата");
        let deviceModel = {
            RegionId: this.state.regionId,
            SerialNumber: this.state.fields["serialNumber"]
        };

      
        console.log(deviceModel);
        console.log("Добавление");
        let token = await authService.getAccessToken();
        console.log(token);
        await fetch('devices/add', {
            method: "POST",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(deviceModel)
        })
        .then(handleErrors).then(response=>console.log("ok") )
        .then(async ()=>await this.props.loadData() )
        .catch(error=>{
            console.log(error);
            this.setState({errorServerMessage:"Номер пристрою невірний, або він вже використовується", regionId: this.state.regionId})
        });

    }

    render() {
       
        return (
            <>
            <div>
            <form
            name="contactform"
            className="contactform"
            onSubmit={async(event)=> {
                await this.deviceSubmit(event);
            }}
            >      
             <div className="col-md-6">
                    <div class="form-group">
                        <h5>Введіть серійний номер пристрою</h5>
                    </div>
                    <div class="form-group">
                        <fieldset>
                            <input class="form-control valid"
                                ref="serialNumber"
                                type="text"
                                size="30"
                                placeholder="Серійний номер"
                                onChange={this.handleChange.bind(this, "serialNumber")}
                                value={this.state.fields["serialNumber"]}
                            />   
                            { this.state.errorServerMessage &&
                            <div class="error-message"> { this.state.errorServerMessage } </div> }
                            <span class="error-message">{this.state.errors["serialNumber"]}</span>
                        </fieldset>
                    </div>
                    <div class="form-group">
                        <input type="submit" value="Додати пристрій" class="btn btn-primary"></input>
                    </div>
                </div>
                
                </form>
            </div>
{/* 
            <div>
                <h2>Введіть серійний номер пристрію</h2>
                
                <p>
                <label>Номер пристрію<input type="text" ref="SerialNumber"></input></label>
                </p>
                <button onClick={async () => { await this.onCreateDevice(); }}>Створити ділянку</button>
            </div> */}
    </>
)
}
}