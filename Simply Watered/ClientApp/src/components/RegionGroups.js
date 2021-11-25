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
            <>
            <h2 className="text-center">Групи ділянок</h2>
            <hr />

            <Link to="/region-groups-add" className="btn btn-primary mx-3" role="button" >Додати групу</Link>
            
        
            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                        <th colSpan="2">Дії</th>
                    </tr>
                </thead>
                <tbody>

                    {regiongroups.map(regiongroup => <tr key={regiongroup.regionGroupId}>
                        <td>{regiongroup.groupName}</td>
                        <td>{regiongroup.regionGroupDescription}</td>
                        <td><button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveGroup(regiongroup); } }>Видалити</button></td>
                        <td><Link
                        className="btn btn-outline-primary"
                        role="button"
                        to=
                        {{
                        pathname: '/regions',
                        state: { groupId: regiongroup.regionGroupId }
                        }}

                        >
                         Переглянути ділянки
                        </Link>
                        </td>
                        <td><Link
                        className="btn btn-outline-primary"
                        role="button"
                        to=
                        {{
                        pathname: '/schedules',
                        state: { groupId: regiongroup.regionGroupId }
                        }}

                        >
                         Задати розклад зрошення
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


