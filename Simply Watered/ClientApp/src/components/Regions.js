import { useLocation } from 'react-router-dom'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'

export class RegionList extends Component{
 
    constructor(props){
        super(props)
        this.state={
            regions: [],
            regionGroup: null,
            groupId: this.props.location.state.groupId
        }
    }



    onRemoveRegion= async(region)=>{

        console.log("Проверка группы для удаления");
        let deletemodel={id:region.regionId};
    if (region) {
        console.log("Удаление");
        let token = await authService.getAccessToken();
        console.log(token);
        let response = await fetch('regions/delete', {
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
        let regions=this.state.regions;
        return(
            <>
            {/* <div>{this.state.regionGroup.groupName}</div> */}
            <Link
                 to=
                {{
                pathname: '/regions-add',
                state: { groupId: this.state.groupId }
                }}
                >Додати ділянку</Link>
        
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                    </tr>
                </thead>
                <tbody>

                    {regions.map(region => <tr key={region.regionId}>
                        <td>{region.regionName}</td>
                        <td>{region.regionDescription}</td>
                        <td><button onClick={async () => { await this.onRemoveRegion(region); } }>Видалити</button></td>
                        <td><Link
                        to=
                        {{
                        pathname: '/devices',
                        state: { regionId: region.regionId }
                        }}

                        >
                        Пристрої ділянки
                        </Link>
                        </td>
                    </tr>


                    )}
                </tbody>
            </table></>
            );
    }

    async loadData() {
        const token = await authService.getAccessToken();
        let groupIdModel={id:this.state.groupId}
        console.log(token);
        const response = await fetch('regions/load', {
            method: "POST",
            headers: !token ? { 
                'Content-Type': 'application/json'
             } : {
                  'Content-Type': 'application/json',
                   'Authorization': `Bearer ${token}` 
                },
                body: JSON.stringify(groupIdModel)
        });
        console.log(response);
        const data = await response.json();
        console.log(data);
        this.setState({ regions: data.regions, regionGroup:data.regionGroup, loading: false });
    }
}