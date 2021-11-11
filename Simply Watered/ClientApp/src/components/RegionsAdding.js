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
            groupId: this.props.location.state.groupId
        };
    }
    onCreateRegion = async () => {

        console.log("кнока нажата");
        let regionModel = {
            RegionName: this.refs.RegionName.value,
            RegionDescription: this.refs.Description.value,
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
            <div>
            <h2>Введіть дані ділянки</h2>
            <p>
            <label>Назва ділянки: <input type="text" ref="RegionName"></input></label>
            </p>
            <p>
            <label>Опис ділянки: <input type="text" ref="Description"></input></label>
            </p>
            <button onClick={async () => { await this.onCreateRegion(); }}>Створити ділянку</button>
    </div>
)
}
}