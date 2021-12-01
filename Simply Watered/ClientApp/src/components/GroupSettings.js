import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';

export class GroupSettings extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            groupId: this.props.groupId,
            irrigationModes: this.props.irrigationModes,
            errorServerMessage: '',
            maxHumidityDisabled: true,
            minHumidity: '',
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
    

        if(fields["minHumidity"]&&!fields["maxHumidity"]){
            formIsValid = false;
            errors["maxHumidity"] = "Для зміни рівнів вологості введіть максимально допустиму вологість!";
        }
    
        this.setState({ errors: errors });
        return formIsValid;
      }

    settingSubmit= async (e) => {
        
        e.preventDefault()
        if  (await this.handleValidation()) {
            await this.onChangeSetting();
        } 
    }

    handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
    }

    handleHumidityChange(field, e){
        let fields = this.state.fields;
    fields[field] = e.target.value;
    if(e.target.value){
        this.setState({ fields: fields, maxHumidityDisabled: false, minHumidity:e.target.value });
    }else{
        this.setState({ fields: fields,maxHumidityDisabled: true  });
    }
    }

    onChangeSetting = async () => {
        let settingModel = {
            RegionId: this.state.groupId,
            
        };

      

        let token = await authService.getAccessToken();

        await fetch('settings/add', {
            method: "POST",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(settingModel)
        })
        .then(this.handleErrors)
        .then(async ()=>await this.props.loadData() )
        .then(this.setState({errors:{}, errorServerMessage: ''}))
        .catch(error=>{
            this.setState({errorServerMessage:"Номер пристрою невірний, або він вже використовується", groupId: this.state.groupId,errors:{}})
        });

       
        
    }

    render() {
       
        let irrigationModes=this.state.irrigationModes;
        return (
            <>
            
                <form
                name="contactform"
                className="contactform mt-3"
                onSubmit={async(event)=> {
                    await this.settingSubmit(event);
                }}
                >      
                    <div className="col-md-6">
                        <h3>Налаштування</h3>
                        {irrigationModes.map(mode =>
                            <div className="form-check">
                                
                                    
                            <input className="form-check-input"
                                id="Input.IrrigationMode"
                                name="irrigationMode"
                                ref="irrigationMode"
                                type="radio"
                                onChange={this.handleChange.bind(this, "irrigationMode")}
                                value={mode.irrigModeId}
                            />   
                            <label for="Input.IrrigationMode1">{mode.modeName}</label>
                            <div className="error-message"> { this.state.errorServerMessage } </div> 
                            <span className="error-message">{this.state.errors["irrigationMode"]}</span>
                    </div>
                     )}
{/*                             
                            <div className="form-check">
                                    
                                    <input className="form-check-input"
                                        id="Input.IrrigationMode2"
                                        name="irrigationMode"
                                        ref="irrigationMode"
                                        type="radio"
                                        onChange={this.handleChange.bind(this, "irrigationMode")}
                                        value={2}
                                    /> 
                                    <label for="Input.IrrigationMode2">Зрошення при низькій вологості</label> 
                                    <div className="error-message"> { this.state.errorServerMessage } </div> 
                                    <span className="error-message">{this.state.errors["irrigationMode"]}</span>
                            </div> */}
                            <div className="form-group">
                                    <label for="Input.MinHumidity">Встановіть мінімальну вологість</label>
                                    <input className="form-control valid  w-25"
                                        id="Input.MinHumidity"
                                        ref="minHumidity"
                                        type="number"
                                        min="0"
                                        max="100"
                                        onChange={this.handleHumidityChange.bind(this, "minHumidity")}
                                        value={this.state.fields["minHumidity"]}
                                    />   
                                    <div className="error-message"> { this.state.errorServerMessage } </div> 
                                    <span className="error-message">{this.state.errors["minHumidity"]}</span>
                            </div>
                            <div className="form-group">
                                    <label for="Input.MaxHumidity">Встановіть максимальну вологість</label>
                                    <input className="form-control valid  w-25"
                                        disabled={this.state.maxHumidityDisabled}
                                        id="Input.MaxHumidity"
                                        ref="maxHumidity"
                                        type="number"
                                        min={this.state.minHumidity}
                                        max="100"
                                        onChange={this.handleChange.bind(this, "maxHumidity")}
                                        value={this.state.fields["maxHumidity"]}
                                    />   
                                    <div className="error-message"> { this.state.errorServerMessage } </div> 
                                    <span className="error-message">{this.state.errors["maxHumidity"]}</span>
                             </div>
                            
                            <div className="form-group ">
                                <input type="submit" value="Додати" class="btn btn-primary"></input>
                            </div>
                            
                    </div>
                </form>
                <div className="col-md-6">
                <button className="btn btn-secondary" onClick={this.props.goBack}>Повернутися</button>
                </div>
            

    </>
)
}
}