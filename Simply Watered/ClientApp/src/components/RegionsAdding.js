import React, { Component } from 'react';
import { Redirect } from "react-router-dom";
import authService from './api-authorization/AuthorizeService'
import { useLocation } from 'react-router-dom'
import { Link } from 'react-router-dom'

export class RegionAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: null,
            groupId: this.props.location.state.groupId,
            fields: {},
            errors: {}
        };
    }

    goBack =()=>{
        this.props.history.goBack();
    }

    handleValidation = async () => {
        let fields = this.state.fields;
        let errors = {};
        let formIsValid = true;
    
        
        if (!fields["regionName"]) {
          formIsValid = false;
          errors["regionName"] = "Вкажіть назву ділянки!";
        }

      
    
        this.setState({ errors: errors });
        return formIsValid;
      }

    regionSubmit= async (e) => {
        
        e.preventDefault()
        if  (await this.handleValidation()) {
            await this.onCreateRegion();
        } 
    }

    handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
    }

    onCreateRegion = async () => {

        console.log("кнока нажата");
        let regionModel = {
            RegionName: this.state.fields["regionName"],
            RegionDescription: this.state.fields["description"],
            GroupId: this.state.groupId
        };
        console.log(regionModel);
        console.log("Добавление");
        let token = await authService.getAccessToken();
        console.log(token);
        await fetch('regions/add', {
            method: "POST",
            headers: !token ? {
                'Content-Type': 'application/json'
            } : {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(regionModel)
        }).then(() => { this.setState({ redirect: "/regions" }) });


    }

    render() {
        if (this.state.redirect) {
            return <Redirect  to=
                                {{
                                pathname: this.state.redirect,
                                state: { groupId: this.state.groupId }
                                }}/>
        }
        return (
            <>
            <form
                name="regionForm"
                className="regionForm mt-3"
                onSubmit={async(event)=> {
                    await this.regionSubmit(event);
                }}
                >      
                    <div className="col-md-6">
                    <h2>Введіть дані ділянки</h2>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.RegionName">Назва ділянки*</label>
                                    <input className="form-control valid"
                                        id="Input.RegionName"
                                        ref="regionName"
                                        type="text"
                                        size="30"
                                        placeholder="Назва"
                                        onChange={this.handleChange.bind(this, "regionName")}
                                        value={this.state.fields["regionName"]}
                                    />   
                                    <span className="error-message">{this.state.errors["regionName"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.Description">Опис ділянки</label>
                                    <input className="form-control valid"
                                        id="Input.Description"
                                        ref="description"
                                        type="text"
                                        size="100"
                                        placeholder="Опис"
                                        onChange={this.handleChange.bind(this, "description")}
                                        value={this.state.fields["description"]}
                                    />   
                                    <span className="error-message">{this.state.errors["description"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group ">
                                <input type="submit" value="Додати ділянку" class="btn btn-primary"></input>
                            </div>
                    </div>
            </form>

            <div className="col-md-6">
            <button className="btn btn-secondary" onClick={this.goBack}>Повернутися</button>
            </div>

    
    </>
)
}
}