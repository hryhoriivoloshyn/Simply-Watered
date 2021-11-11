import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'


export class GroupList extends Component {
    static displayName = GroupList.name;

    constructor(props) {
        super(props);
        this.state = {
            regiongroups: [],
            loading: true,
            message: ''
        }
    }

    onClick(regiongroup){
            this.onRemoveGroup(regiongroup);
    }

     onRemoveGroup= async(regiongroup)=>{

         console.log("Проверка группы для удаления");
         let deletemodel={id:regiongroup.regionGroupId}
        
        if (regiongroup) {
            console.log("Удаление");
            let token = await authService.getAccessToken();
            console.log(token);
            let response = await fetch('regiongroups/delete', {
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


    render() {

        let regiongroups=this.state.regiongroups;
        return (
            <><Link to="/region-groups-add">Додати групу</Link>
        
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                    </tr>
                </thead>
                <tbody>

                    {regiongroups.map(regiongroup => <tr key={regiongroup.regionGroupId}>
                        <td>{regiongroup.groupName}</td>
                        <td>{regiongroup.regionGroupDescription}</td>
                        <td><button onClick={async () => { await this.onRemoveGroup(regiongroup); } }>Видалити</button></td>
                        <td><Link
                        to=
                        {{
                        pathname: '/regions',
                        state: { groupId: regiongroup.regionGroupId }
                        }}

                        >
                         Ділянки групи
                        </Link>
                        </td>
                    </tr>


                    )}
                </tbody>
            </table></>
            );
    }

    //Загрузка данных
    async loadData() {
        const token = await authService.getAccessToken();
        console.log(token);
        const response = await fetch('regiongroups', {
            headers: !token ? { 'Content-Type': 'application/json' } : { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` }
        });
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ regiongroups: data, loading: false });
    }


   

}


