import { useLocation } from 'react-router-dom'
import React, { Component } from 'react';
import { Link } from 'react-router-dom'
import authService from './api-authorization/AuthorizeService'

export class RegionList extends Component{
 
    constructor(props){
        super(props)
        this.state={
            regions: [],
            regionGroup: {GroupName:"",
                          GroupDescription: ""
                         },
            groupId: this.props.location.state.groupId
        }
    }

    goBack =()=>{
        this.props.history.goBack();
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
        let groupName=this.state.regionGroup.groupName;
        return(
            <>
            
            <h2 className="text-center">Ділянки групи "{groupName}"</h2>
            <hr />

            <Link
                className="btn btn-primary mx-3"
                role="button"
                to=
                {{
                pathname: '/regions-add',
                state: { groupId: this.state.groupId }
                }}
                >Додати ділянку</Link>

            <button className="btn btn-secondary" onClick={this.goBack}>Повернутися</button>

            <table className='table table-striped text-center mt-3' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                        <th colSpan="2">Дії</th>
                    </tr>
                </thead>
                <tbody>

                    {regions.map(region => <tr key={region.regionId}>
                        <td>{region.regionName}</td>
                        <td>{region.regionDescription}</td>
                        <td><button className="btn btn-outline-dark" onClick={async () => { await this.onRemoveRegion(region); } }>Видалити</button></td>
                        <td>
                        <Link
                            className="btn btn-outline-primary"
                            role="button"
                            to=
                                {{
                                pathname: '/devices',
                                state: { regionId: region.regionId }
                                }}

                            >Переглянути пристрої</Link>
                        </td>
                    </tr>


                    )}
                </tbody>
            </table>
            
            </>
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