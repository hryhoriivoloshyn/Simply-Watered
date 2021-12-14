import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService';
import PropTypes from "prop-types";
import { withRouter } from "react-router";


export class AccountCreation extends React.Component {


      
    constructor(props) {
        super(props);

        this.state = {
            resourcepath: "/admin/users",
            modes: this.props.modes,
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
    

        // if(fields["minHumidity"]&&!fields["maxHumidity"]){
        //     formIsValid = false;
        //     errors["maxHumidity"] = "Для зміни рівнів вологості введіть максимально допустиму вологість!";
        // }
    
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
            Email: this.state.fields["email"],
            Password: this.state.fields["password"]
        };

      

        let token = await authService.getAccessToken();

        await fetch(`api${this.state.resourcepath}`, {
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
            this.setState({errorServerMessage:"Такий користувач вже існує!",errors:{}})
        });

       
        
    }

    render() {
       
        let modes=this.state.modes;
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
                        <h3>Додати користувача</h3>
                      

                            <div className="form-group">
                                    <label for="Input.email">Пошта</label>
                                    <input className="form-control valid  w-25"
                                        id="Input.email"
                                        ref="email"
                                        type="text"
                                        min="0"
                                        max="100"
                                        onChange={this.handleHumidityChange.bind(this, "email")}
                                        value={this.state.fields["email"]}
                                    />   
                              
                                    <span className="error-message">{this.state.errors["email"]}</span>
                            </div>
                            <div className="form-group">
                                    <label for="Input.password">Пароль</label>
                                    <input className="form-control valid  w-25"
                                        disabled={this.state.maxHumidityDisabled}
                                        id="Input.password"
                                        ref="password"
                                        type="text"
                                        min={this.state.minHumidity}
                                        max="100"
                                        onChange={this.handleChange.bind(this, "password")}
                                        value={this.state.fields["password"]}
                                    />   
                                    <div className="error-message"> { this.state.errorServerMessage } </div> 
                                    <span className="error-message">{this.state.errors["password"]}</span>
                             </div>
                            
                            <div className="form-group ">
                                <input type="submit" value="Додати користувача" class="btn btn-success"></input>
                            </div>
                            
                    </div>
                </form>
               
            

    </>
)
}
}
