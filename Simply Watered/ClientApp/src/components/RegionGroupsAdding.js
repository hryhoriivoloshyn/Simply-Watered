import React, { Component } from 'react';
import { Redirect } from "react-router-dom";
import authService from './api-authorization/AuthorizeService'

export class GroupAdding extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirect: null
        };
    }


    onCreateGroup=async()=>{
      
        console.log("кнока нажата");
        let groupModel={
              GroupName:this.refs.GroupName.value,
              RegionGroupDescription:this.refs.Description.value,
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
          <div>
            <h2>Введіть дані групи</h2>
            <p>
              <label>Назва групи: <input type="text" ref="GroupName"></input></label>
            </p>
            <p>
              <label>Опис групи: <input type="text" ref="Description"></input></label>
            </p>
            <button onClick={async () => { await this.onCreateGroup(); }}>Створити групу</button>
            </div>
        )
      }
}