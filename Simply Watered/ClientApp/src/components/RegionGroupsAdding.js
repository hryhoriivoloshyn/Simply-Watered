import React, { Component } from 'react';
import { Redirect } from "react-router-dom";
import authService from './api-authorization/AuthorizeService'

export class GroupAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: null,
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
    
        
        if (!fields["groupName"]) {
          formIsValid = false;
          errors["groupName"] = "Вкажіть назву групи!";
        }

      
    
        this.setState({ errors: errors });
        return formIsValid;
      }

    groupSubmit= async (e) => {
        
        e.preventDefault()
        if  (await this.handleValidation()) {
            await this.onCreateGroup();
        } 
    }

    handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields: fields });
    }

    onCreateGroup=async()=>{
      
        console.log("кнока нажата");
        let groupModel={
              GroupName:this.state.fields["groupName"],
              RegionGroupDescription:this.state.fields["description"],
            };
        console.log(groupModel);
            console.log("Добавление");
            let token = await authService.getAccessToken();
            console.log(token);
            let response = await fetch('regiongroupsadding/add', {
                method: "POST",
                headers: !token ? {
                    'Content-Type': 'application/json'
                } : {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(groupModel)
            }).then(()=>{this.setState({redirect:"/region-groups"})});
            
        
        }

    render(){
        if(this.state.redirect){
            return <Redirect to={this.state.redirect}/>
        }
        return(
            <>
                 <form
                name="regionGroupForm"
                className="regionGroupForm mt-3"
                onSubmit={async(event)=> {
                    await this.groupSubmit(event);
                }}
                >      
                    <div className="col-md-6">
                    <h2>Введіть дані групи ділянок</h2>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.GroupName">Назва групи ділянок*</label>
                                    <input className="form-control valid"
                                        id="Input.GroupName"
                                        ref="groupName"
                                        type="text"
                                        size="30"
                                        placeholder="Назва"
                                        onChange={this.handleChange.bind(this, "groupName")}
                                        value={this.state.fields["groupName"]}
                                    />   
                                    <span className="error-message">{this.state.errors["groupName"]}</span>
                                </fieldset>
                            </div>
                            <div className="form-group">
                                <fieldset>
                                    <label for="Input.Description">Опис групи</label>
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

        //   <div>
        //     <h2>Введіть дані групи</h2>
        //     <p>
        //       <label>Назва групи: <input type="text" ref="GroupName"></input></label>
        //     </p>
        //     <p>
        //       <label>Опис групи: <input type="text" ref="Description"></input></label>
        //     </p>
        //     <button onClick={async () => { await this.onCreateGroup(); }}>Створити групу</button>
        //     </div>
        )
      }
}