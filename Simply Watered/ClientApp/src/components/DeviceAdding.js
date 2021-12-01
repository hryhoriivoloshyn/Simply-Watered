import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class DeviceAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            path: this.props.path,
            errorServerMessage: '',
            fields: {},
            errors: {}
        };
    }
    
    handleErrors =(response)=> {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
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
        let deviceModel = {
            SerialNumber: this.state.fields["serialNumber"]
        };

      

        let token = await authService.getAccessToken();

        await fetch(`api${this.state.path}`, {
            method: "PUT",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(deviceModel)
        })
        .then(this.handleErrors)
        .then(async ()=>await this.props.loadData() )
        .then(this.setState({errors:{}, errorServerMessage: ''}))
        .catch(error=>{
            this.setState({errorServerMessage:"Номер пристрою невірний, або він вже використовується", regionId: this.state.regionId,errors:{}})
        });

       
        
    }

    render() {
       
        return (
            <>
            
                <form
                name="contactform"
                className="contactform mt-3"
                onSubmit={async(event)=> {
                    await this.deviceSubmit(event);
                }}
                >      
                    <div className="col-md-6">
                        <h3>Додати пристрій</h3>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.SerialNumber">Введіть серійний номер пристрою</label>
                                    <input className="form-control valid"
                                        id="Input.SerialNumber"
                                        ref="serialNumber"
                                        type="text"
                                        size="30"
                                        placeholder="Серійний номер"
                                        onChange={this.handleChange.bind(this, "serialNumber")}
                                        value={this.state.fields["serialNumber"]}
                                    />   
                                    
                                    <div className="error-message"> { this.state.errorServerMessage } </div> 
                                    <span className="error-message">{this.state.errors["serialNumber"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group ">
                                <input type="submit" value="Додати" class="btn btn-primary"></input>
                            </div>
                            
                    </div>
                </form>
                <div className="col-md-6">
                <button className="btn btn-secondary" onClick={this.props.goBack}>Повернутися</button>
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